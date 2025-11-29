using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Boat : MonoBehaviour
{

    private Rigidbody _rb;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    public void Row(bool forwards, bool backwards, bool left, bool right)
    {
        if (forwards && backwards && left && right)
            return;
        Debug.Log($"{forwards} {backwards} {left} {right}");
        var leftAmount = left ? 1 : 0;
        var rightAmount = right ? 1 : 0;
        // TODO
    }

}
