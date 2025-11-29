using System.Linq;
using UnityEngine;

namespace Maps
{

    public sealed class Starter : MonoBehaviour
    {

        private const float Waiting = 10;
        private const float Countdown = 3;

        public static float TimeToStart { get; private set; }

        private Phase _phase;

        private int _index = -1;

        private float _delay;

        [SerializeField]
        private Transform cam;

        [SerializeField]
        private CutsceneSequence[] cutscenes;

        private void Start() => TimeToStart = cutscenes.Sum(e => e.duration) + Waiting + Countdown;

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
                    break;
                case Phase.Cutscenes:
                    var sequence = cutscenes[_index];
                    sequence.from.GetPositionAndRotation(out var fromPos, out var fromRot);
                    sequence.to.GetPositionAndRotation(out var toPos, out var toRot);
                    cam.SetPositionAndRotation(
                        Vector3.Lerp(toPos, fromPos, _delay / sequence.duration),
                        Quaternion.Lerp(toRot, fromRot, _delay / sequence.duration)
                    );
                    break;
                case Phase.Waiting when _delay <= 0:
                    _phase = Phase.CountingDown;
                    _delay += Countdown;
                    break;
                case Phase.CountingDown when _delay <= 0:
                    Destroy(this);
                    break;
            }
        }

        private enum Phase
        {

            Cutscenes,
            Waiting,
            CountingDown

        }

    }

}
