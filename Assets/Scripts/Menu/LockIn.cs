namespace Menu
{

    public sealed class LockIn : Checkbox
    {

        public static bool Locked { get; private set; }

        protected override bool Value
        {
            set => Locked = value;
        }

        protected override bool DefaultValue => false;
        protected override string Key => "Lock In";

    }

}
