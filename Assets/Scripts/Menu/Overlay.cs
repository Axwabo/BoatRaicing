using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu
{

    public sealed class Overlay : MonoBehaviour
    {

        public static bool IsOpen { get; private set; }

        public static void Hide()
        {
            if (_current)
                _current.Set(false);
        }

        private static Overlay _current;

        [SerializeField]
        private GameObject menu;

        private void Awake()
        {
            menu.SetActive(false);
            _current = this;
        }

        private void Update()
        {
            if (InputSystem.actions["Menu"].WasPressedThisFrame())
                Set(!IsOpen);
        }

        private void Set(bool isOpen)
        {
            IsOpen = isOpen;
            menu.SetActive(IsOpen);
            Cursor.lockState = IsOpen ? CursorLockMode.None : CursorLockMode.Locked;
        }

        private void OnDestroy() => IsOpen = false;

    }

}
