using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KestrelMod.CardActions;

namespace KestrelMod.Cards
{
    [CardMeta(rarity = Rarity.common, dontOffer = true)]
    internal class LaunchFederationMissile : Card
    {
        
        public override List<CardAction> GetActions(State s, Combat c)
        {
            //initialise card action list
            List<CardAction> actionList = new List<CardAction>();

            ASpawn aspawn = new ASpawn();
            //TODO: there's no piercing flag for missiles, find how to make missile pierce shields
            Missile missile = new Missile();
            missile.yAnimation = 0.0;
            missile.missileType = MissileType.normal;

            aspawn.thing = (StuffBase) missile;
            actionList.Add((CardAction) aspawn);

            //TODO: we don't want cannon shot to pierce, we want missile to pierce
            //remove this once we figure out how to make missile objects pierce
            actionList.Add((CardAction) new AAttack()
            {
                targetPlayer = false,
                piercing = true,
                damage = this.GetDmg(s, 1)
            });

            return actionList;
        }

        public override CardData GetData(State state)
        {
            CardData cardData = new CardData();
            cardData.cost = 1;
            cardData.exhaust = true;

            return cardData;
        }

        public override string Name()
        {
            return "Federation Missile Shot";
        }
    }
}
