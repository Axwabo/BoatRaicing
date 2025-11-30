using UnityEngine;

namespace Bots
{

    [RequireComponent(typeof(MeshRenderer))]
    public sealed class RandomMaterial : MonoBehaviour
    {

        [SerializeField]
        private Material[] materials;

        private void Awake() => GetComponent<MeshRenderer>().sharedMaterial = materials[Random.Range(0, materials.Length)];

    }

}
