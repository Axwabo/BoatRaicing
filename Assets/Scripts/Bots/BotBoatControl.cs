using Maps;
using UnityEngine;

namespace Bots
{

    [RequireComponent(typeof(Boat))]
    public sealed class BotBoatControl : MonoBehaviour
    {

        private Boat _boat;

        private Transform _t;

        private int _index;

        private void Awake()
        {
            _boat = GetComponent<Boat>();
            _t = transform;
        }

        private void FixedUpdate()
        {
            if (Starter.TimeToStart > 0 || _index >= Starter.TargetPoints.Count)
                return;
            var target = Starter.TargetPoints[_index];
        }

    }

}
