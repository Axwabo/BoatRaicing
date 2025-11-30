using UnityEngine;

namespace Maps
{

    [RequireComponent(typeof(MeshRenderer))]
    public sealed class Finish : MonoBehaviour
    {

        public static Finish Current { get; private set; }

        private int _qualified;

        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            Current = this;
        }

        public void Hide() => _renderer.enabled = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Boat _))
                return;
            _qualified++;
            if (other.TryGetComponent(out ManualBoatControl _))
                Timer.Current.Finish(_qualified);
        }

    }

}
