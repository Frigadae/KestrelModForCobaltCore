using KestrelMod.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KestrelMod.Artifacts
{
    #nullable enable
    [ArtifactMeta(owner = Deck.colorless, pools = new ArtifactPool[] { ArtifactPool.EventOnly }, unremovable = true)]
    internal class FederationMissile : Artifact
    {
        //TODO: missile ignores shields (piercing) and deals 2 DMG.
        //this has not been implemented yet

        //current implementation: attack copies Isaac's missile shot but piercing dmg is reduced by 1

        public override string Description()
        {
            return "Fires a missile and a piercing shot. A limited number of missiles can be used in combat.";
        }

        public override string Name()
        {
            return "Artemis Missiles";
        }

        public override List<Tooltip>? GetExtraTooltips()
        {
            List<Tooltip> extraTooltips = new List<Tooltip>();
            extraTooltips.Add((Tooltip) new TTCard()
            {
                card = (Card) new LaunchFederationMissile()
            });
            return extraTooltips;
        }

    }
}
