using UnityEngine;

public static class Extensions
{

    public static float MagnitudeIgnoreY(this Vector3 vector3) => Mathf.Sqrt(vector3.x * vector3.x + vector3.z * vector3.z);

}
