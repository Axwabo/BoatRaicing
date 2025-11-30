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
            rowing.y = 1;
            _boat.Row(rowing);
        }

        private void Row(float angle, ref Vector2 rowing)
        {
            var angularVelocity = _boat.AngularVelocity;
            if (angularVelocity > 0.0000004f)
                return;
            if (angle < -angleThreshold)
                rowing.x = 1;
            else if (angle > angleThreshold)
                rowing.x = -1;
        }

    }

}
