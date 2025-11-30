using UnityEngine;

namespace Menu
{

    public sealed class BoatCollision : Checkbox
    {

        protected override bool Value
        {
            set => Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Boat"), LayerMask.NameToLayer("Boat"), !value);
        }

        protected override bool DefaultValue => false;
        protected override string Key => "Boat Collision";

    }

}
