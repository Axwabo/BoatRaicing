using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Boat))]
public sealed class ManualBoatControl : MonoBehaviour
{

    private Boat _boat;

    private void Awake() => _boat = GetComponent<Boat>();

    private void FixedUpdate() => _boat.Row(InputSystem.actions["Move"].ReadValue<Vector2>());

}
