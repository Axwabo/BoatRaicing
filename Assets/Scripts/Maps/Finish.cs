using UnityEngine;

namespace Maps
{

    [RequireComponent(typeof(MeshRenderer), typeof(AudioSource))]
    public sealed class Finish : MonoBehaviour
    {

        public static Finish Current { get; private set; }

        private int _qualified;

        private MeshRenderer _renderer;

        private AudioSource _source;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _source = GetComponent<AudioSource>();
            Current = this;
        }

        public void Hide() => _renderer.enabled = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Boat _))
                return;
            _qualified++;
            if (!other.TryGetComponent(out ManualBoatControl _))
                return;
            _source.Play();
            Timer.Current.Finish(_qualified);
        }

    }

}
