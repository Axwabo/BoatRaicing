using TMPro;
using UnityEngine;

public sealed class SpeedDisplay : MonoBehaviour
{


    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Rigidbody rb;

    private void Update() => text.text = $"{rb.linearVelocity.MagnitudeIgnoreY():F} m/s";

}
