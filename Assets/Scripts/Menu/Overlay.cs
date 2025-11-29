using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu
{

    public sealed class Overlay : MonoBehaviour
    {

        public static bool IsOpen { get; private set; }

        [SerializeField]
        private GameObject menu;

        private void Awake() => menu.SetActive(false);

        private void Update()
        {
            if (!InputSystem.actions["Menu"].WasPressedThisFrame())
                return;
            IsOpen = !IsOpen;
            menu.SetActive(IsOpen);
            Cursor.lockState = IsOpen ? CursorLockMode.None : CursorLockMode.Locked;
        }

        private void OnDestroy() => IsOpen = false;

    }

}
