using Maps;
using UnityEngine;

namespace Menu
{

    public sealed class MapSelectorList : MonoBehaviour
    {

        private static MapDescriptor[] _maps;

        [SerializeField]
        private MapSelector prefab;

        [SerializeField]
        private string directory;

        private void Awake()
        {
            _maps ??= Resources.LoadAll<MapDescriptor>(directory);
            var t = transform;
            foreach (var descriptor in _maps)
                Instantiate(prefab, t).Apply(descriptor);
        }

    }

}
