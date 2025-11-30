using Menu;
using UnityEngine;

namespace Maps
{

    [RequireComponent(typeof(AudioSource))]
    public sealed class IntroMusic : MonoBehaviour
    {

        private const float FadeOutStart = Starter.Waiting + Starter.Countdown;

        private float _volume = 1;

        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            if (!AlwaysSkipCutscenes.Skip)
                _source.Play();
        }

        private void Update()
        {
            if (Starter.TimeToStart > FadeOutStart)
                return;
            if ((_volume -= Time.deltaTime) <= 0)
                Destroy(gameObject);
            _source.volume = _volume;
        }

    }

}
