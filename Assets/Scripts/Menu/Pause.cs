using TMPro;
using UnityEngine;

namespace Menu
{

    public sealed class Pause : ButtonBase
    {

        private TextMeshProUGUI _text;

        private void Start() => _text = GetComponentInChildren<TextMeshProUGUI>();

        protected override void Click() => Set(!AudioListener.pause);

        private void Set(bool paused)
        {
            AudioListener.pause = paused;
            Time.timeScale = paused ? 0 : 1;
            _text.text = paused ? "Continue" : "Pause";
        }

        private void OnDestroy() => Set(false);

    }

}
