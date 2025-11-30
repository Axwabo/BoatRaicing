namespace Menu
{

    public sealed class SensitivitySlider : SettingsSliderBase
    {

        protected override float Value
        {
            set => CameraRotor.Sensitivity = value;
        }

        protected override float DefaultValue => CameraRotor.DefaultSensitivity;

        protected override string Label => "Camera Sensitivity";

        protected override float Display(float value) => value / CameraRotor.DefaultSensitivity;

    }

}
