using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{

    [RequireComponent(typeof(Slider))]
    public sealed class SensitivitySlider : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI text;

        private void Start()
        {
            var slider = GetComponent<Slider>();
            UpdateText(slider.value = CameraRotor.Sensitivity);
            slider.onValueChanged.AddListener(Change);
        }

        private void Change(float value)
        {
            CameraRotor.Sensitivity = value;
            PlayerPrefs.SetFloat(CameraRotor.SensitivityKey, value);
            UpdateText(value);
        }

        private void UpdateText(float value) => text.text = $"Camera Sensitivity: {value / CameraRotor.DefaultSensitivity:P0}";

    }

}
