using Maps;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{

    public sealed class MapSelector : ButtonBase
    {

        [SerializeField]
        private Image image;

        [SerializeField]
        private TextMeshProUGUI text;

        protected override void Click() => SceneManager.LoadScene(text.text);

        public void Apply(MapDescriptor descriptor)
        {
            image.sprite = descriptor.Image;
            text.text = descriptor.name;
            Finish.RequiredLaps = descriptor.Laps ? 3 : 1;
        }

    }

}
