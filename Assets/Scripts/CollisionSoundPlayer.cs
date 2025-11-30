using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public sealed class CollisionSoundPlayer : MonoBehaviour
{

    private AudioSource _source;

    private float _delay = 1;

    [SerializeField]
    private AudioClip[] clips;

    private void Awake() => _source = GetComponent<AudioSource>();

    private void Update() => _delay -= Time.deltaTime;

    private void OnCollisionEnter(Collision other)
    {
        if (_delay < 0 && other.gameObject.layer != LayerMask.NameToLayer("Silent"))
            _source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

}
