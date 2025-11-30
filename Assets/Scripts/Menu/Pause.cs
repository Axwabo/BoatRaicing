using TMPro;
using UnityEngine;

namespace Menu
{

    public sealed class Pause : ButtonBase
    {

        private TextMeshProUGUI _text;

        private void OnEnable()
        {
            if (AutoPause.Enabled)
                Set(true);
        }

        private void Start()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _text.text = AudioListener.pause ? "Continue" : "Pause";
        }

        protected override void Click()
        {
            Set(!AudioListener.pause);
            if (AutoPause.Enabled && !AudioListener.pause)
                Overlay.Hide();
        }

        private void Set(bool paused)
        {
            AudioListener.pause = paused;
            Time.timeScale = paused ? 0 : 1;
            if (didStart)
                _text.text = paused ? "Continue" : "Pause";
        }

        private void OnDisable()
        {
            if (AutoPause.Enabled)
                Set(false);
        }

        private void OnDestroy() => Set(false);

    }

}
