using Menu;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class CameraRotor : MonoBehaviour
{

    public const float DefaultSensitivity = 0.1f;
    public const string SensitivityKey = "Sensitivity";

    public static float Sensitivity { get; set; } = DefaultSensitivity;

    private Transform _camera;

    private Transform _player;

    private float _yaw;
    private float _pitch;

    private void Awake()
    {
        _camera = transform;
        _player = _camera.parent;
        Sensitivity = PlayerPrefs.GetFloat(SensitivityKey, DefaultSensitivity);
    }

    private void OnEnable() => Cursor.lockState = CursorLockMode.Locked;

    private void Update()
    {
        if (Overlay.IsOpen)
            return;
        var delta = InputSystem.actions["Look"].ReadValue<Vector2>();
        _yaw = Mathf.Clamp(_yaw + delta.x * Sensitivity, -120, 120);
        _pitch = Mathf.Clamp(_pitch - delta.y * Sensitivity, -90, 90);
        _camera.localEulerAngles = new Vector3(_pitch, 0, 0);
        _player.localEulerAngles = new Vector3(0, _yaw, 0);
    }

    private void OnDestroy() => Cursor.lockState = CursorLockMode.None;

}
