using System;
using UnityEngine;

namespace Maps
{

    [Serializable]
    public sealed class CutsceneSequence
    {

        public Transform from;

        public Transform to;

        public float duration = 5;

    }

}
