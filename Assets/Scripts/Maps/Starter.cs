using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Maps
{

    public sealed class Starter : MonoBehaviour
    {

        public const int Waiting = 10;
        public const int Countdown = 3;

        public static float TimeToStart { get; private set; }

        private Phase _phase;

        private int _index = -1;

        private float _delay;

        [SerializeField]
        private Transform cam;

        [SerializeField]
        private CutsceneSequence[] cutscenes;

        private void Awake() => TimeToStart = cutscenes.Sum(e => e.duration) + Waiting + Countdown;

        private void Start() => ManualBoatControl.Current.enabled = false;

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
                    ManualBoatControl.Current.Mount(cam);
                    Finish.Current.Hide();
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

        private void UpdateCutscene()
        {
            var sequence = cutscenes[_index];
            sequence.from.GetPositionAndRotation(out var fromPos, out var fromRot);
            sequence.to.GetPositionAndRotation(out var toPos, out var toRot);
            cam.SetPositionAndRotation(
                Vector3.Lerp(toPos, fromPos, _delay / sequence.duration),
                Quaternion.Lerp(toRot, fromRot, _delay / sequence.duration)
            );
            if (!InputSystem.actions["Jump"].WasPressedThisFrame())
                return;
            ManualBoatControl.Current.Mount(cam);
            _phase = Phase.CountingDown;
            _delay = TimeToStart = Countdown;
        }

        private enum Phase
        {

            Cutscenes,
            Waiting,
            CountingDown

        }

    }

}
