using System;
using TMPro;
using UnityEngine;

namespace Maps
{

    public sealed class Timer : MonoBehaviour
    {

        public static Timer Current { get; private set; }

        private double _startTimestamp;

        [SerializeField]
        private TextMeshProUGUI small;

        [SerializeField]
        private TextMeshProUGUI large;

        private void Awake() => Current = this;

        private void Update()
        {
            if (Starter.TimeToStart > 0)
                return;
            if (_startTimestamp == 0)
            {
                small.enabled = true;
                _startTimestamp = Time.timeSinceLevelLoadAsDouble;
            }

            small.text = TimeSpan.FromSeconds(Time.timeSinceLevelLoadAsDouble - _startTimestamp).ToString("mm':'ss'.'fff");
        }

        public void Finish(int place)
        {
            enabled = false;
            large.enabled = true;
            large.text = $"#{place}";
            small.color = Color.green;
        }

    }

}
