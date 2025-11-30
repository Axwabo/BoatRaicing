using UnityEngine;

namespace Bots
{

    public sealed class TargetPoint : MonoBehaviour
    {

        public Vector3 Position { get; private set; }

        public Vector3 Facing { get; private set; }

        [SerializeField]
        private Transform debug;

        private void Awake()
        {
            var t = transform;
            Position = t.position;
            Facing = t.TransformPoint(Vector3.forward);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var pos = transform.position;
            Gizmos.DrawSphere(pos, 0.1f);
            Gizmos.DrawLine(pos, transform.TransformPoint(Vector3.forward));
        }

        private void OnDrawGizmosSelected()
        {
            if (!debug)
                return;
            Debug.Log(Vector3.Dot(debug.position - transform.position, transform.TransformPoint(Vector3.forward) - transform.position));
        }

    }

}
