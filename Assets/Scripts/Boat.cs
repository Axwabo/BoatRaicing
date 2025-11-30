using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public sealed class Boat : MonoBehaviour
{

    private static readonly List<Boat> Instances = new();

    private const float AngleMultiplier = 10;
    private const float CircumferenceModulus = 360 / AngleMultiplier;

    public static int Walls => LayerMask.GetMask("Default", "Silent");

    public static IReadOnlyCollection<Boat> Boats => Instances;

    private Rigidbody _rb;

    private Transform _t;

    private float _rightTarget;

    private float _rightAngle;

    private float _leftTarget;

    private float _leftAngle;

    [field: SerializeField]
    public MeshRenderer Player { get; private set; }

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

    [SerializeField]
    private Transform rightOar;

    [SerializeField]
    private Transform leftOar;

    public Vector3 LinearVelocity => _rb.linearVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _t = transform;
        Instances.Add(this);
    }

    public void Row(Vector2 move)
    {
        var forwards = Math.Sign(move.y);
        var sideways = Math.Sign(move.x);
        if (sideways != 0)
            Turn(sideways);
        if (forwards == 0)
            return;
        _rightTarget += forwards * 0.5f;
        _leftTarget += forwards * 0.5f;
        AdjustOarsForward(forwards, sideways);
        if (!IsGrounded)
            return;
        var linear = _rb.linearVelocity;
        var multiplier = Math.Sign(_t.InverseTransformDirection(linear).z) == forwards
            ? Mathf.Min(1, linear.MagnitudeIgnoreY() / accelerationMultiplierTarget) * accelerationMultiplierRatio + 1
            : decelerationRatio;
        _rb.AddRelativeForce(0, 0, forwards * force * multiplier * Time.fixedDeltaTime);
    }

    private void Turn(int sideways)
    {
        if (IsGrounded)
            _rb.AddTorque(0, sideways * torque * Time.fixedDeltaTime, 0);
        if (sideways < 0)
        {
            _rightTarget++;
            _leftTarget--;
        }
        else
        {
            _leftTarget++;
            _rightTarget--;
        }
    }

    private void AdjustOarsForward(int forwards, int sideways)
    {
        if (sideways != 0)
            return;
        var difference = _leftTarget - _rightTarget;
        var mod = Mathf.Abs(difference) % CircumferenceModulus;
        if (mod is < 1 or > CircumferenceModulus - 1)
            return;
        if (Random.value < 0.5f)
            _leftTarget += mod * forwards;
        else
            _rightTarget += mod * forwards;
    }

    private void Update()
    {
        _rightAngle = Mathf.Lerp(_rightAngle, _rightTarget, Time.deltaTime * 3);
        _leftAngle = Mathf.Lerp(_leftAngle, _leftTarget, Time.deltaTime * 3);
        rightOar.localEulerAngles = new Vector3(_rightAngle * AngleMultiplier, 0, 0);
        leftOar.localEulerAngles = new Vector3(_leftAngle * AngleMultiplier, 0, 0);
    }

    private void OnDestroy() => Instances.Remove(this);

    private bool IsGrounded => Raycast(_t.position)
                               || Raycast(_t.TransformPoint(new Vector3(leftOar.localPosition.x, 0, 0)))
                               || Raycast(_t.TransformPoint(new Vector3(rightOar.localPosition.x, 0, 0)));

    private static bool Raycast(Vector3 position)
        => Physics.Raycast(position, Vector3.down, 0.1f, Walls);

}
