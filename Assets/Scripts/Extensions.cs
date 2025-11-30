using UnityEngine;
using UnityEngine.Audio;

public static class Extensions
{

    public static float MagnitudeIgnoreY(this Vector3 vector3) => Mathf.Sqrt(vector3.x * vector3.x + vector3.z * vector3.z);

    public static void Set(this AudioMixer mixer, string param, float percentage)
        => mixer.SetFloat(param, Mathf.Approximately(0, percentage) ? -80 : 20 * Mathf.Log10(percentage));

}
