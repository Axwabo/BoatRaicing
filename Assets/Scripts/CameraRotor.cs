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
    }

    private void Update()
    {
        var delta = InputSystem.actions["Look"].ReadValue<Vector2>();
        _camera.Rotate(-delta.y * Sensitivity, 0, 0);
        _player.Rotate(0, delta.x * Sensitivity, 0);
    }

}
