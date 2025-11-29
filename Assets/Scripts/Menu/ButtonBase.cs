using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{

    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : MonoBehaviour
    {

        private void Awake() => GetComponent<Button>().onClick.AddListener(Click);

        protected abstract void Click();

    }

}
