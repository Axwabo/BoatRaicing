namespace Menu
{

    public sealed class AlwaysSkipCutscenes : Checkbox
    {

        public static bool Skip { get; private set; }

        protected override bool Value
        {
            set => Skip = value;
        }

        protected override bool DefaultValue => false;
        protected override string Key => "Always Skip Cutscenes";

    }

}
