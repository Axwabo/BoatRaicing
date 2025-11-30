using Maps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{

    public sealed class MapSelector : ButtonBase
    {

        [SerializeField]
        private Image image;

        private MapDescriptor _descriptor;

        protected override void Click() => SceneManager.LoadScene(_descriptor.name);

        public void Apply(MapDescriptor descriptor) => image.sprite = descriptor.Image;

    }

}
