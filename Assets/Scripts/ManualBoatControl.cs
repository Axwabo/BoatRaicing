using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Boat))]
public sealed class ManualBoatControl : MonoBehaviour
{

    private Boat _boat;

    private void Awake() => _boat = GetComponent<Boat>();

    private void FixedUpdate()
    {
        var move = InputSystem.actions["Move"].ReadValue<Vector2>();
        _boat.Row(move.y > 0, move.y < 0, move.x < 0, move.x > 0);
    }

}
