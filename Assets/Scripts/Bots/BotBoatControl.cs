using Maps;
using UnityEngine;

namespace Bots
{

    [RequireComponent(typeof(Boat))]
    public sealed class BotBoatControl : MonoBehaviour
    {

        private Boat _boat;

        private Transform _t;

        private int _index;

        [SerializeField]
        private float angleThreshold = 3;

        [SerializeField]
        private float distanceThreshold = 1;

        [SerializeField]
        private float smallDistanceThreshold = 0.5f;

        [SerializeField]
        private float brakingThreshold = 1;

        private void Awake()
        {
            _boat = GetComponent<Boat>();
            _t = transform;
        }

        private void FixedUpdate()
        {
            if (Starter.TimeToStart > 0 || _index >= Starter.TargetPoints.Count)
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
            Row(angle, ref rowing);

            var distanceFromNextPosition = DistanceToFacing(target, position + _boat.LinearVelocity * Time.fixedDeltaTime);
            var distanceFromCurrent = DistanceToFacing(target, position);
            var distance = Mathf.Abs(distanceFromCurrent - distanceFromNextPosition);
            if (distance < smallDistanceThreshold / Mathf.Min(distanceThreshold, distanceFromCurrent))
                rowing.y = 1;
            else if (distance > brakingThreshold / Mathf.Min(distanceThreshold, distanceFromCurrent))
                rowing.y = -1;

            _boat.Row(rowing);
        }

        private void Row(float angle, ref Vector2 rowing)
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
