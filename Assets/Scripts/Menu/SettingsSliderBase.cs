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
        protected virtual float Display(float value) => value;

        private void Start()
        {
            var slider = GetComponent<Slider>();
            UpdateText(slider.value = LoadValue());
            slider.onValueChanged.AddListener(Change);
        }

        public float LoadValue() => Value = PlayerPrefs.GetFloat(Label, DefaultValue);

        private void Change(float value)
        {
            Value = value;
            PlayerPrefs.SetFloat(Label, value);
            UpdateText(value);
        }

        private void UpdateText(float value) => text.text = $"{Label}: {Display(value):P0}";

    }

}
