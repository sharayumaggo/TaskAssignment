using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHand
{
    class Hand
    {
        readonly IEnumerable<IRank> _ranks = null;
        readonly IEnumerable<string> _cards = null;

        public Hand(IEnumerable<string> cards)
        {
            if (cards?.Count() != 5)
            {
                throw new Exception();
            }
            _cards = cards;

            _ranks = new List<IRank> {
                new High_Card(cards),
                new One_Pair(cards),
                new Two_Pairs(cards),
                new Three_Of_Kinds(cards),
                new Straight(cards),
                new Flush(cards),
                new Full_House(cards),
                new Four_Of_Kinds(cards),
                new Straight_Flush(cards),
                new Royal_Flush(cards)
            };
        }

        public Tuple<int, double> Weight
        {
            get
            {
                return _ranks.Select((r, order) => r.Score(order))
                .Where(r => r.Item2 > 0)
                .OrderBy(r => r.Item1)
                .Last();
            }
        }

        public override string ToString()
        {
            return String.Join(" ", _cards.OrderBy(c => Rank.Mapper[c[0]]).ToArray()) + " : " + this.Weight.Item1 + " : " + this.Weight.Item2;
        }

    }
}
