using UnityEditor;
using UnityEngine;

namespace Editor
{

    public static class Screenshot
    {

        [MenuItem("Assets/Screenshot")]
        public static void Grab() => ScreenCapture.CaptureScreenshot("Assets/Screenshot.png", 2);

    }

}
