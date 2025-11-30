using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneLoadFadeOut : MonoBehaviour
{

    private static readonly HashSet<string> Names = new();

    [SerializeField]
    private AudioSource[] sources;

    [SerializeField]
    private bool keep;

    private float _volume = -1;

    private string _scene;

    private void Start()
    {
        var go = gameObject;
        _scene = go.scene.name;
        if (keep && !Names.Add(go.name))
        {
            Destroy(go);
            return;
        }

        DontDestroyOnLoad(go);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (_volume < 0)
            return;
        _volume -= Time.unscaledDeltaTime;
        foreach (var source in sources)
            source.volume = _volume;
        if (_volume > 0)
            return;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        var go = gameObject;
        Destroy(go);
        Names.Remove(go.name);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (!keep || arg0.name != _scene)
            _volume = 1;
    }

}
