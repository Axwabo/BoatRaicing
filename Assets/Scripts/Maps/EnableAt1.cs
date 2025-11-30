using UnityEngine;

namespace Maps
{

    public sealed class EnableAt1 : MonoBehaviour
    {

        [SerializeField]
        private GameObject target;

        private void Update()
        {
            if (Starter.TimeToStart > 1)
                return;
            target.SetActive(true);
            Destroy(this);
        }

    }

}
