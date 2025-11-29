using UnityEngine;
using UnityEngine.InputSystem;

public sealed class CameraRotor : MonoBehaviour
{

    private const float Sensitivity = 0.1f;

    private Transform _camera;

    private Transform _player;

    private void Awake()
    {
        _camera = transform;
        _player = _camera.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (InputSystem.actions["Attack"].WasPressedThisFrame())
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        if (Cursor.lockState != CursorLockMode.Locked)
            return;
        var delta = InputSystem.actions["Look"].ReadValue<Vector2>();
        _camera.Rotate(-delta.y * Sensitivity, 0, 0);
        _player.Rotate(0, delta.x * Sensitivity, 0);
    }

}
