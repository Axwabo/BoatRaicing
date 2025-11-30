using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{

    [RequireComponent(typeof(Slider))]
    public abstract class SettingsSliderBase : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI text;

        protected abstract float Value { set; }
        protected abstract float DefaultValue { get; }
        protected abstract string Label { get; }

        private void Start()
        {
            var slider = GetComponent<Slider>();
            UpdateText(slider.value = Value = PlayerPrefs.GetFloat(Label, DefaultValue));
            slider.onValueChanged.AddListener(Change);
        }

        private void Change(float value)
        {
            Value = value;
            PlayerPrefs.SetFloat(Label, value);
            UpdateText(value);
        }

        private void UpdateText(float value) => text.text = $"{Label}: {value / DefaultValue:P0}";

    }

}
