using UnityEngine;
using UnityEngine.UI;

namespace Menu
{

    [RequireComponent(typeof(Toggle))]
    public abstract class Checkbox : MonoBehaviour
    {

        protected abstract bool Value { set; }
        protected abstract bool DefaultValue { get; }
        protected abstract string Key { get; }

        private void Awake()
        {
            var toggle = GetComponent<Toggle>();
            Value = toggle.isOn = PlayerPrefs.GetInt(Key, DefaultValue ? 1 : 0) == 1;
            toggle.onValueChanged.AddListener(UpdateValue);
        }

        private void UpdateValue(bool selected)
        {
            Value = selected;
            PlayerPrefs.SetInt(Key, selected ? 1 : 0);
        }

    }

}
