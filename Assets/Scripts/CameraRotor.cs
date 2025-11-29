using UnityEngine;
using UnityEngine.InputSystem;

public sealed class CameraRotor : MonoBehaviour
{

    private const float Sensitivity = 0.1f;

    private Transform _camera;

    private Transform _player;

    private float _yaw;
    private float _pitch;

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
        _yaw = Mathf.Clamp(_yaw + delta.x * Sensitivity, -120, 120);
        _pitch = Mathf.Clamp(_pitch - delta.y * Sensitivity, -90, 90);
        _camera.localEulerAngles = new Vector3(_pitch, 0, 0);
        _player.localEulerAngles = new Vector3(0, _yaw, 0);
    }

}
