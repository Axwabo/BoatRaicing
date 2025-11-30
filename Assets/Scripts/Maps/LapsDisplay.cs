using TMPro;
using UnityEngine;

namespace Maps
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class LapsDisplay : MonoBehaviour
    {

        private TextMeshProUGUI _text;

        private void Awake() => _text = GetComponent<TextMeshProUGUI>();

        private void Update()
        {
            if (Starter.TimeToStart > 0 || Timer.QualifiedAt != 0 || Finish.RequiredLaps != Finish.LapCount)
                return;
            var laps = ManualBoatControl.Current.Boat.Laps + 1;
            _text.enabled = true;
            _text.text = $"{(laps == Finish.LapCount ? "(Final) " : "")} Lap {laps}";
        }

    }

}
