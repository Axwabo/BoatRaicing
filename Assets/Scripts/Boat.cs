using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Boat : MonoBehaviour
{

    private Rigidbody _rb;

    [SerializeField]
    private float force = 1;

    [SerializeField]
    private float torque = 1;

    [SerializeField]
    private float accelerationMultiplierTarget = 10;

    [SerializeField]
    private float accelerationMultiplierRatio = 0.5f;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    public void Row(Vector2 move)
    {
        var forwards = Math.Sign(move.y);
        var sideways = Math.Sign(move.x);
        if (forwards != 0)
        {
            var multiplier = Mathf.Min(1, _rb.linearVelocity.MagnitudeIgnoreY() / accelerationMultiplierTarget) * accelerationMultiplierRatio + 1;
            _rb.AddRelativeForce(0, 0, forwards * force * multiplier * Time.fixedDeltaTime);
        }

        if (sideways != 0)
            _rb.AddTorque(0, sideways * torque * Time.fixedDeltaTime, 0);
    }

}
