using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Boat))]
public sealed class ManualBoatControl : MonoBehaviour
{

    public static ManualBoatControl Current { get; private set; }

    private Boat _boat;

    private void Awake()
    {
        Current = this;
        _boat = GetComponent<Boat>();
    }

    private void FixedUpdate() => _boat.Row(InputSystem.actions["Move"].ReadValue<Vector2>());

    public void Mount(Transform cam)
    {
        _boat.Player.enabled = false;
        cam.parent = _boat.Player.transform;
        cam.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        cam.gameObject.AddComponent<CameraRotor>();
    }

}
