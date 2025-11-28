using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public sealed class ManualBoatControl : MonoBehaviour
{

    private Rigidbody _rb;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        var move = InputSystem.actions["Move"].ReadValue<Vector2>();
        if (move.y != 0)
            _rb.AddRelativeForce((move.y < 0 ? Vector3.back : Vector3.forward), ForceMode.Acceleration);
        _rb.maxLinearVelocity = 1;
        if (move.x < 0)
            _rb.AddTorque(0, -1, 0, ForceMode.Acceleration);
        else if (move.x > 0)
            _rb.AddTorque(0, 1, 0, ForceMode.Acceleration);
    }

}
