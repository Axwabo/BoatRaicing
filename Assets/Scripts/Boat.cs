using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Boat : MonoBehaviour
{

    private Rigidbody _rb;

    private Transform _t;

    [SerializeField]
    private float force = 1;

    [SerializeField]
    private float torque = 1;

    [SerializeField]
    private float accelerationMultiplierTarget = 10;

    [SerializeField]
    private float accelerationMultiplierRatio = 0.5f;

    [SerializeField]
    private float decelerationRatio = 0.3f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _t = transform;
    }

    public void Row(Vector2 move)
    {
        var forwards = Math.Sign(move.y);
        var sideways = Math.Sign(move.x);
        if (sideways != 0)
            _rb.AddTorque(0, sideways * torque * Time.fixedDeltaTime, 0);
        if (forwards == 0)
            return;
        var linear = _rb.linearVelocity;
        var multiplier = Math.Sign(_t.InverseTransformDirection(linear).z) == forwards
            ? Mathf.Min(1, linear.MagnitudeIgnoreY() / accelerationMultiplierTarget) * accelerationMultiplierRatio + 1
            : decelerationRatio;
        _rb.AddRelativeForce(0, 0, forwards * force * multiplier * Time.fixedDeltaTime);
    }

}
