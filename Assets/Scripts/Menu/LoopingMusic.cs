using UnityEngine;

namespace Menu
{

    public sealed class LoopingMusic : MonoBehaviour
    {

        private bool _started;

        private float _delay = 1;

        private bool _aStarted;

        private bool _bStarted;

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
                b.PlayDelayed(loopDelay);
                _started = true;
                return;
            }

            if (a.isPlaying)
                _aStarted = false;
            if (b.isPlaying)
                _bStarted = false;
            if (!_bStarted && a.clip && a.time >= loopTime - Time.deltaTime * 2)
            {
                b.PlayDelayed(loopTime - a.time - 0.01f);
                _bStarted = true;
            }
            else if (!_aStarted && b.clip && b.time >= loopTime - Time.deltaTime * 2)
            {
                a.clip = loop;
                a.PlayDelayed(loopTime - b.time - 0.01f);
                _aStarted = true;
            }
        }

    }

}
