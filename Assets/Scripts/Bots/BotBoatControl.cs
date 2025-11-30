using Maps;
using UnityEngine;

namespace Bots
{

    [RequireComponent(typeof(Boat))]
    public sealed class BotBoatControl : MonoBehaviour
    {

        private const float StuckTime = 2;

        private Boat _boat;

        private Transform _t;

        private int _index;

        private float _stuck;

        private float _unstuck;

        [SerializeField]
        private float angleThreshold = 3;

        [SerializeField]
        private float distanceThreshold = 1;

        [SerializeField]
        private float smallDistanceThreshold = 0.5f;

        [SerializeField]
        private float brakingThreshold = 1;

        [SerializeField]
        private float stuckThreshold = 0.6f;

        private void Awake()
        {
            _boat = GetComponent<Boat>();
            _t = transform;
        }

        private void FixedUpdate()
        {
            if (Starter.TimeToStart > 0 || _index >= Starter.TargetPoints.Count || TryUnstuck())
                return;
            var target = Starter.TargetPoints[_index];
            var position = _t.position;

            var toTarget = position - target.Position;
            var facingOffset = target.Facing;
            toTarget.y = facingOffset.y = 0;

            var angle = Vector3.SignedAngle(_t.forward, toTarget, Vector3.up);

            var dot = Vector3.Dot(toTarget, facingOffset);
            if (dot > 0.1f)
                _index++;

            var rowing = new Vector2();
            Turn(angle, ref rowing);

            var distanceFromNextPosition = DistanceToFacing(target, position + _boat.LinearVelocity * Time.fixedDeltaTime);
            var distanceFromCurrent = DistanceToFacing(target, position);
            var distance = Mathf.Abs(distanceFromCurrent - distanceFromNextPosition);
            if (distance < smallDistanceThreshold / Mathf.Min(distanceThreshold, distanceFromCurrent))
                rowing.y = 1;
            else if (distance > brakingThreshold / Mathf.Min(distanceThreshold, distanceFromCurrent))
                rowing.y = -1;

            _boat.Row(rowing);
        }

        private bool TryUnstuck()
        {
            if ((_unstuck -= Time.fixedDeltaTime) > 0)
            {
                _boat.Row(new Vector2(0, -1));
                return true;
            }

            if (_boat.LinearVelocity.magnitude > Time.fixedDeltaTime * 10
                || !Physics.Raycast(_t.TransformPoint(new Vector3(0, 0, stuckThreshold)), _t.forward, 0.1f, Boat.Walls))
            {
                _stuck = 0;
                return false;
            }

            if ((_stuck += Time.fixedDeltaTime) > StuckTime)
                _unstuck = StuckTime;
            return false;
        }

        private void Turn(float angle, ref Vector2 rowing)
        {
            if (angle < -angleThreshold)
                rowing.x = 1;
            else if (angle > angleThreshold)
                rowing.x = -1;
        }

        private float DistanceToFacing(TargetPoint target, Vector3 origin)
        {
            var targetPosition = target.Position + target.Facing;
            var offset = targetPosition - origin;
            var front = _t.TransformPoint(Vector3.forward * offset.MagnitudeIgnoreY());
            return Vector3.Distance(targetPosition, front);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.TransformPoint(Vector3.forward * stuckThreshold), 0.1f);
            if (_index >= Starter.TargetPoints.Count)
                return;
            Gizmos.color = Color.red;
            var target = Starter.TargetPoints[_index];
            var targetPosition = target.Position + target.Facing;
            var offset = targetPosition - _t.position;
            var front = _t.TransformPoint(Vector3.forward * offset.MagnitudeIgnoreY());
            Gizmos.DrawSphere(front, 0.1f);
        }

    }

}
