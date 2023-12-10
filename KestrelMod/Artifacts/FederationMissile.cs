using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KestrelMod.Artifacts
{
    internal class FederationMissile : Artifact
    {
        //ignores shields (piercing) and deals 2 DMG.
        //to prevent confusion with the Artemis ship in vanilla, Artemis missiles will be called Federation missiles.
        //lazy workaround - give the player three exhaust cards

        public override string Description()
        {
            return "Ignores shields but only a limited number of missiles can be fired each combat.";
        }

        public override string Name()
        {
            return "Federation Missiles";
        }

    }
}
