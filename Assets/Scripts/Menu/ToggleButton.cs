using UnityEngine;

namespace Menu
{

    public sealed class ToggleButton : ButtonBase
    {

        [SerializeField]
        private GameObject target;

        [SerializeField]
        private bool deactivateOnLoad;

        private void Start()
        {
            if (!deactivateOnLoad)
                return;
            target.SetActive(false);
            foreach (var slider in target.GetComponentsInChildren<SettingsSliderBase>())
                slider.LoadValue();
        }

        protected override void Click() => target.SetActive(!target.activeSelf);

    }

}
