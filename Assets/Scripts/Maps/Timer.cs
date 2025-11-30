using System;
using TMPro;
using UnityEngine;

namespace Maps
{

    public sealed class Timer : MonoBehaviour
    {

        public static Timer Current { get; private set; }

        public static double QualifiedAt { get; private set; }

        private double _startTimestamp;

        [SerializeField]
        private TextMeshProUGUI small;

        [SerializeField]
        private TextMeshProUGUI large;

        private void Awake()
        {
            Current = this;
            QualifiedAt = 0;
        }

        private void Update()
        {
            if (Starter.TimeToStart > 0 || QualifiedAt != 0 && SpectatingHandler.IsSelf)
                return;
            if (_startTimestamp == 0)
            {
                small.enabled = true;
                _startTimestamp = Time.timeSinceLevelLoadAsDouble;
            }

            Show(Time.timeSinceLevelLoadAsDouble - _startTimestamp);
        }

        private void Show(double seconds) => small.text = TimeSpan.FromSeconds(seconds).ToString("mm':'ss'.'fff");

        public void Finish(int place)
        {
            QualifiedAt = Time.timeSinceLevelLoadAsDouble;
            large.enabled = true;
            large.text = $"#{place}";
            small.color = Color.green;
        }

        public void ShowQualificationTime() => Show(QualifiedAt - _startTimestamp);

    }

}
