using UnityEngine;

namespace Menu
{

    public sealed class LoopingMusic : MonoBehaviour
    {

        private bool _started;

        private float _delay = 1;

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

            if (a.clip && a.time >= a.clip.length - Time.deltaTime * 2)
                b.Play();
            else if (b.clip && b.time >= b.clip.length - Time.deltaTime * 2)
            {
                a.clip = loop;
                a.Play();
            }
        }

    }

}
