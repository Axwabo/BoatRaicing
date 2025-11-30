using UnityEngine;
using UnityEngine.Audio;

namespace Maps
{

    public sealed class LapMusic : MonoBehaviour
    {

        private const float Ratio = 1 / 4f;

        private float _fade;

        [SerializeField]
        private AudioMixer mixer;

        [SerializeField]
        private string main;

        [SerializeField]
        private string intense;

        [SerializeField]
        private GameObject mainParent;

        [SerializeField]
        private GameObject intenseParent;

        private void Awake()
        {
            mixer.Set(main, 1);
            mixer.Set(intense, 0);
            if (Finish.RequiredLaps != Finish.LapCount)
                Destroy(this);
        }

        private void Update()
        {
            if (ManualBoatControl.Current.Boat.Laps != Finish.LapCount - 1)
                return;
            if (_fade == 0)
                ApplyTime();
            _fade += Time.deltaTime * Ratio;
            var progress = Mathf.Clamp01(_fade);
            mixer.Set(main, 1 - progress);
            mixer.Set(intense, progress);
            if (_fade >= 1)
                Destroy(this);
        }

        private void ApplyTime()
        {
            var mainSources = mainParent.GetComponentsInChildren<AudioSource>();
            var intenseSources = intenseParent.GetComponentsInChildren<AudioSource>();
            for (var i = 0; i < mainSources.Length; i++)
                intenseSources[i].timeSamples = mainSources[i].timeSamples;
        }

    }

}
