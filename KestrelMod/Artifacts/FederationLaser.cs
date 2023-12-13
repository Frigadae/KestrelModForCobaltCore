using KestrelMod.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KestrelMod.Artifacts
{
    internal class FederationLaser : Artifact
    {
        //three shots - 1 DMG each
        //builds up charge for three turns, on full charge burst laser can be fired for 0 energy. If not fired, retain

        public override string Description()
        {
            return "Builds up charge for three turns. When charged, fire three shots dealing 1 dmg each.";
        }

        public override string Name()
        {
            return "Federation Laser";
        }

        public override void OnTurnStart(State state, Combat combat)
        {
            base.OnTurnStart(state, combat);
        }

        public override List<Tooltip>? GetExtraTooltips()
        {
            List<Tooltip> extraTooltips = new List<Tooltip>();
            extraTooltips.Add((Tooltip)new TTCard()
            {
                card = (Card)new FireBurstLaser()
            });
            return extraTooltips;
        }
    }
}
