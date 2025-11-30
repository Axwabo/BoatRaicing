using UnityEngine;

namespace Bots
{

    public sealed class TargetPoint : MonoBehaviour
    {

        public Vector3 Position { get; private set; }

        public Vector3 Facing { get; private set; }

        private void Awake()
        {
            var t = transform;
            Position = t.position;
            Facing = t.TransformPoint(Vector3.forward);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.TransformPoint(Vector3.forward));
        }

    }

}
