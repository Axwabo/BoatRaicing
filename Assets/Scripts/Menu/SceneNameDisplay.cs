using TMPro;
using UnityEngine;

namespace Menu
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class SceneNameDisplay : MonoBehaviour
    {

        private void Awake() => GetComponent<TextMeshProUGUI>().text = gameObject.scene.name;

    }

}
