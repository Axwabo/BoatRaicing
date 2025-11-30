using System.Collections.Generic;
using System.Linq;
using Bots;
using Menu;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Maps
{

    public sealed class Starter : MonoBehaviour
    {

        private static readonly Vector3 UpOffset = Vector3.up * 0.3f;

        public const int Waiting = 5;
        public const int Countdown = 3;

        public const int Bots = 4;
        public const int TotalBoats = Bots + 1;

        public static float TimeToStart { get; private set; }

        public static IReadOnlyList<TargetPoint> TargetPoints { get; private set; }

        private Phase _phase;

        private int _index = -1;

        private float _delay;

        [SerializeField]
        private Transform cam;

        [SerializeField]
        private CutsceneSequence[] cutscenes;

        [SerializeField]
        private Transform startPoint;

        [SerializeField]
        private float startSpread;

        [SerializeField]
        private Boat manualPrefab;

        [SerializeField]
        private Boat botPrefab;

        [SerializeField]
        private Transform targets;

        private Vector3 Right => startPoint.TransformPoint(new Vector3(startSpread, 0, 0));

        private Vector3 Left => startPoint.TransformPoint(new Vector3(-startSpread, 0, 0));

        private void Awake()
        {
            TargetPoints = targets.GetComponentsInChildren<TargetPoint>();
            TimeToStart = cutscenes.Sum(e => e.duration) + Waiting + Countdown;
            SpawnBoats();
        }

        private void Start()
        {
            ManualBoatControl.Current.enabled = false;
            if (AlwaysSkipCutscenes.Skip)
                Skip();
        }

        private void SpawnBoats()
        {
            const float lerpInterval = 1f / Bots;
            var playerPosition = Random.Range(0, TotalBoats);
            var left = Left;
            var right = Right;
            var rotation = startPoint.rotation;
            for (var i = 0; i < TotalBoats; i++)
            {
                var position = Vector3.Lerp(left, right, lerpInterval * i) + UpOffset;
                Instantiate(i == playerPosition ? manualPrefab : botPrefab, position, rotation);
            }
        }

        private void Update()
        {
            TimeToStart -= Time.deltaTime;
            _delay -= Time.deltaTime;
            switch (_phase)
            {
                case Phase.Cutscenes when _delay <= 0:
                    if (++_index < cutscenes.Length)
                    {
                        _delay = cutscenes[_index].duration;
                        goto case Phase.Cutscenes;
                    }

                    _phase = Phase.Waiting;
                    _delay += Waiting;
                    Prepare();
                    break;
                case Phase.Cutscenes:
                    UpdateCutscene();
                    break;
                case Phase.Waiting when _delay <= 0:
                    _phase = Phase.CountingDown;
                    _delay += Countdown;
                    break;
                case Phase.CountingDown when _delay <= -1:
                    Destroy(this);
                    break;
                case Phase.CountingDown when _delay <= 0:
                    ManualBoatControl.Current.enabled = true;
                    break;
            }
        }

        private void Prepare()
        {
            ManualBoatControl.Current.Mount(cam);
            Finish.Current.Hide();
        }

        private void UpdateCutscene()
        {
            var sequence = cutscenes[_index];
            sequence.from.GetPositionAndRotation(out var fromPos, out var fromRot);
            sequence.to.GetPositionAndRotation(out var toPos, out var toRot);
            cam.SetPositionAndRotation(
                Vector3.Lerp(toPos, fromPos, _delay / sequence.duration),
                Quaternion.Lerp(toRot, fromRot, _delay / sequence.duration)
            );
            if (InputSystem.actions["Jump"].WasPressedThisFrame())
                Skip();
        }

        private void Skip()
        {
            Prepare();
            _phase = Phase.CountingDown;
            _delay = TimeToStart = Countdown;
        }

        private void OnDrawGizmos()
        {
            if (!startPoint)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                Left,
                Right
            );
        }

        private enum Phase
        {

            Cutscenes,
            Waiting,
            CountingDown

        }

    }

}
