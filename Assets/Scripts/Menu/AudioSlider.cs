using UnityEngine;
using UnityEngine.Audio;

namespace Menu
{

    public sealed class AudioSlider : SettingsSliderBase
    {

        [SerializeField]
        private AudioMixer mixer;

        [SerializeField]
        private string parameter;

        [SerializeField]
        private float defaultValue;

        protected override float Value
        {
            set => mixer.SetFloat(Label, Mathf.Approximately(0, value) ? -80 : 20 * Mathf.Log10(value));
        }

        protected override float DefaultValue => defaultValue;

        protected override string Label => parameter;

    }

}
