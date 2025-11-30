using Menu;
using UnityEngine;

public sealed class LockInHandler : MonoBehaviour
{

    private MeshRenderer[] _renderers;

    private AudioSource[] _sources;

    private bool _isLocked;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>();
        _sources = GetComponentsInChildren<AudioSource>();
    }

    private void Update()
    {
        if (_isLocked == LockIn.Locked)
            return;
        _isLocked = LockIn.Locked;
        foreach (var meshRenderer in _renderers)
            meshRenderer.enabled = !_isLocked;
        foreach (var source in _sources)
            source.mute = _isLocked;
    }

}
