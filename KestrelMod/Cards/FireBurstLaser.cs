using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KestrelMod.Cards
{
    internal class FireBurstLaser : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            //initialise card action list
            List<CardAction> actionList = new List<CardAction>();

            //TODO: burst laser should only fire when fully charged
            //if not fully charged, do nothing
            var attack = new AAttack();
            attack.targetPlayer = false;
            attack.damage = this.GetDmg(s, 1);
            attack.fast = true;

            actionList.Add((CardAction) attack);
            actionList.Add((CardAction) attack);
            actionList.Add((CardAction) attack);

            return actionList;
        }

        public override CardData GetData(State state)
        {
            CardData cardData = new CardData();
            cardData.cost = 2;

            return cardData;
        }

        public override string Name()
        {
            return "Fire Burst Laser";
        }
    }
}
