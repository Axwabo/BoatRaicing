using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Boat))]
public sealed class ManualBoatControl : MonoBehaviour
{

    public static ManualBoatControl Current { get; private set; }

    public Boat Boat { get; private set; }

    private void Awake()
    {
        Current = this;
        Boat = GetComponent<Boat>();
    }

    private void FixedUpdate() => Boat.Row(InputSystem.actions["Move"].ReadValue<Vector2>());

}
