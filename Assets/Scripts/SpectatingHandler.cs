using Maps;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class SpectatingHandler : MonoBehaviour
{

    public static bool IsSelf { get; private set; }

    private bool _everClicked;

    private int _manualIndex;

    private int _spectating;

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private TextMeshProUGUI large;

    [SerializeField]
    private TextMeshProUGUI main;

    private void Start()
    {
        IsSelf = true;
        for (var i = 0; i < Boat.Boats.Count; i++)
        {
            if (Boat.Boats[i] != ManualBoatControl.Current.Boat)
                continue;
            _spectating = _manualIndex = i;
            break;
        }
    }

    private void Update()
    {
        if (Timer.QualifiedAt == 0)
            return;
        main.text = IsSelf ? "Left click to spectate" : $"Spectating Bot {_spectating + 1}";
        main.enabled = true;
        if (Overlay.IsOpen || !InputSystem.actions["Attack"].WasPressedThisFrame())
            return;
        if (!_everClicked)
            large.enabled = false;

        _everClicked = true;
        if (++_spectating >= Boat.Boats.Count)
            _spectating = 0;
        IsSelf = _spectating == _manualIndex;
        ManualBoatControl.Current.enabled = IsSelf;
        foreach (var boat in Boat.Boats)
            boat.Unmount(cam);
        Boat.Boats[_spectating].Mount(cam);
        if (IsSelf)
            Timer.Current.ShowQualificationTime();
    }

}
