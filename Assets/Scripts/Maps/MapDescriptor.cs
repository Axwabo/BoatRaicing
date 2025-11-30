using UnityEngine;

namespace Maps
{

    [CreateAssetMenu(fileName = "Map", menuName = "BoatRaicing/Map Descriptor", order = 0)]
    public sealed class MapDescriptor : ScriptableObject
    {

        [field: SerializeField]
        public Sprite Image { get; private set; }

        [field: SerializeField]
        public bool Laps { get; private set; }

    }

}
