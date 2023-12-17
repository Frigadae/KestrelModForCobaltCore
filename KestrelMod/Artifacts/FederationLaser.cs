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
    internal class FederationLaser : Artifact
    {
        //three shots - 1 DMG each
        //TODO: add one charge each turn, on full charge (three charges) burst laser can be fired for 0 energy.

        //current implementation: fires three shots for two energy

        public static int weaponChargeCountBase = 3;
        public static int weaponChargeCounter = 0;

        public override string Description()
        {
            return "Builds up charge for three turns. When charged, fire three shots dealing 1 dmg each.";
        }

        public override string Name()
        {
            return "Federation Laser";
        }

        public override void OnCombatStart(State state, Combat combat)
        {
            base.OnCombatStart(state, combat);

            //Manifest.WeaponCharge.Id
        }

        public override void OnTurnStart(State state, Combat combat)
        {
            base.OnTurnStart(state, combat);


            //Manifest.WeaponCharge.Id
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
