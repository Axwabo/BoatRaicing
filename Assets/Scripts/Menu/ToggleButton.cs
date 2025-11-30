using UnityEngine;

namespace Menu
{

    public sealed class ToggleButton : ButtonBase
    {

        [SerializeField]
        private GameObject target;

        protected override void Click() => target.SetActive(!target.activeSelf);

    }

}
