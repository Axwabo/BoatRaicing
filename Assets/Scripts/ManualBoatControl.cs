using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public sealed class ManualBoatControl : MonoBehaviour
{

    private Rigidbody _rb;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        var move = InputSystem.actions["Move"].ReadValue<Vector2>();
        if (move.y != 0)
            _rb.AddRelativeForce((move.y < 0 ? Vector3.back : Vector3.forward) * 5);
        if (move.x < 0)
            _rb.AddRelativeTorque(0, -3, 0);
        else if (move.x > 0)
            _rb.AddRelativeTorque(0, 3, 0);
    }

}
