using Maps;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{

    public sealed class MapSelector : ButtonBase
    {

        private bool _laps;

        [SerializeField]
        private Image image;

        [SerializeField]
        private TextMeshProUGUI text;

        protected override void Click()
        {
            Finish.RequiredLaps = _laps ? Finish.LapCount : 1;
            SceneManager.LoadScene(text.text);
        }

        public void Apply(MapDescriptor descriptor)
        {
            image.sprite = descriptor.Image;
            text.text = descriptor.name;
            _laps = descriptor.Laps;
        }

    }

}
