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
            CheckPass(position, target);
        }

        private void CheckPass(Vector3 position, TargetPoint target)
        {
            var toTarget = position - target.Position;
            var facingOffset = target.Facing - target.Position;
            toTarget.y = facingOffset.y = 0;
            if (Vector3.Dot(toTarget, facingOffset) > 0.1f)
                _index++;
        }

    }

}
