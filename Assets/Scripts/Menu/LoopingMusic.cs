using UnityEngine;

namespace Menu
{

    public sealed class LoopingMusic : MonoBehaviour
    {

        private bool _started;

        private float _delay = 1;

        private bool _isA;

        [SerializeField]
        private AudioSource a;

        [SerializeField]
        private AudioSource b;

        [SerializeField]
        private AudioClip start;

        [SerializeField]
        private float loopDelay;

        [SerializeField]
        private AudioClip loop;

        [SerializeField]
        private float loopTime;

        private void Update()
        {
            if ((_delay -= Time.deltaTime) > 0)
                return;
            if (!_started)
            {
                a.PlayOneShot(start);
                b.clip = loop;
                b.PlayDelayed(loopDelay - 0.01f);
                _started = true;
                return;
            }

            var source = _isA ? a : b;
            if (source.time < loopTime - Time.deltaTime * 2)
                return;
            var other = _isA ? b : a;
            other.clip = loop;
            other.PlayDelayed(Mathf.Max(0, loopTime - source.time) - 0.01f);
            _delay = 1;
            _isA = !_isA;
        }

    }

}
