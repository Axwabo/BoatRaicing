namespace Menu
{

    public sealed class AutoPause : Checkbox
    {

        public static bool Enabled { get; private set; } = true;

        protected override bool Value
        {
            set => Enabled = value;
        }

        protected override bool DefaultValue => true;
        protected override string Key => "Auto Pause";

    }

}
