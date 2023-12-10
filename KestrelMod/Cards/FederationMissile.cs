using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KestrelMod.Cards
{
    internal class FederationMissile : Card
    {

        public override List<CardAction> GetActions(State s, Combat c)
        {
            var list = new List<CardAction>();

            return list;
        }

        public override CardData GetData(State state) => new CardData
        {
            cost = 0
        };

        public override string Name()
        {
            return "Launch Federation Missile";
        }
    }
}
