using TMPro;
using UnityEngine;

namespace Maps
{

    public sealed class Countdown : MonoBehaviour
    {

        private int _previousTime;

        [SerializeField]
        private TextMeshProUGUI small;

        [SerializeField]
        private TextMeshProUGUI large;

        [SerializeField]
        private TextMeshProUGUI skip;

        [SerializeField]
        private AudioSource source;

        [SerializeField]
        private AudioClip beep;

        [SerializeField]
        private AudioClip start;

        private void Update()
        {
            if (Starter.TimeToStart <= -1)
            {
                Destroy(this);
                large.enabled = false;
                return;
            }

            var time = Mathf.CeilToInt(Starter.TimeToStart);
            if (time == _previousTime)
                return;
            _previousTime = time;
            if (time == Starter.Waiting + Starter.Countdown)
                skip.enabled = false;
            if (time == Starter.Countdown)
            {
                large.enabled = true;
                small.enabled = false;
                skip.enabled = false;
            }

            if (time > Starter.Countdown)
            {
                small.text = time.ToString();
                return;
            }

            source.PlayOneShot(time == 0 ? start : beep);
            large.text = time == 0 ? "ROW!" : time.ToString();
        }

    }

}
