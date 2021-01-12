using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHand
{
    interface IRank
    {
        Tuple<int, double> Score(int order);
    }


    public abstract class Rank : IRank
    {

        public static readonly IDictionary<char, int> Mapper = new Dictionary<char, int> {
             {'1',1}, {'2',2}, {'3',3}, {'4',4}, {'5',5}, {'6',6}, {'7',7}, {'8',8}, {'9',9}, {'T',10}, {'J',11}, {'Q',12}, {'K',13}, {'A',14}
        };

        protected readonly IEnumerable<string> cards = null;

        public Rank(IEnumerable<string> cards)
        {
            this.cards = cards;
        }

        protected abstract bool Match();

        protected IEnumerable<IGrouping<int, int>> GetGroups(int level)
        {
            return cards.GroupBy(c => Mapper[c[0]], c => 1).Where(g => g.Count() == level).OrderBy(c => c.Key);
        }

        protected virtual double Remainder
        {
            get
            {
                return 0;
            }
        }

        public virtual Tuple<int, double> Score(int order)
        {
            if (Match() == false)
            {
                return new Tuple<int, double>(0, 0);
            }

            return new Tuple<int, double>(order, Remainder);
        }
    }

    public class Royal_Flush : Straight_Flush
    {
        public Royal_Flush(IEnumerable<string> cards) : base(cards)
        {

        }

        protected override bool Match()
        {
            return base.Match() && GetGroups(1).Max(c => c.Key) == Mapper['A'];
        }

    }

    public class Straight_Flush : Straight
    {
        public Straight_Flush(IEnumerable<string> cards) : base(cards)
        {

        }

        protected override bool Match()
        {
            return base.Match() && cards.Select(c => c[1]).Distinct().Count() == 1;
        }

        protected override double Remainder
        {
            get
            {
                return GetGroups(1).Max(c => c.Key);
            }
        }

    }

    public class Four_Of_Kinds : Rank
    {

        public Four_Of_Kinds(IEnumerable<string> cards) : base(cards)
        {

        }

        protected override bool Match()
        {
            return GetGroups(4).Count() == 1;
        }

        protected override double Remainder
        {
            get
            {
                return GetGroups(4).First().Key * 15
                + GetGroups(1).First().Key;
            }

        }
    }

    public class Full_House : Rank
    {
        public Full_House(IEnumerable<string> cards) : base(cards)
        {

        }
        protected override bool Match()
        {

            return cards.GroupBy(c => Mapper[c[0]], c => 1).Count(g => g.Count() == 3) == 1
                && cards.GroupBy(c => Mapper[c[0]], c => 1).Count(g => g.Count() == 2) == 1;
        }

        protected override double Remainder
        {
            get
            {
                return GetGroups(3).First().Key * 15
                + GetGroups(2).First().Key;

            }
        }
    }

    public class Flush : Rank
    {

        public Flush(IEnumerable<string> cards) : base(cards)
        {

        }
        protected override bool Match()
        {
            return cards.Select(c => c[1]).Distinct().Count() == 1;
        }

        protected override double Remainder
        {
            get
            {
                return cards.Select(c => Mapper[c[0]]).OrderBy(c => c).Select((c, idx) => c * Math.Pow(15, idx)).Sum();

            }
        }
    }
    public class Straight : Rank
    {
        public Straight(IEnumerable<string> cards) : base(cards)
        {

        }
        protected override bool Match()
        {
            return cards.Select(c => Mapper[c[0]]).Max() * 5 == cards.Select(c => Mapper[c[0]]).OrderBy(c => c).Select((v, idx) => v + idx).Sum();
        }

        protected override double Remainder
        {
            get
            {
                return cards.Select(c => Mapper[c[0]]).Max();
            }

        }
    }

    public class Three_Of_Kinds : Rank
    {
        public Three_Of_Kinds(IEnumerable<string> cards) : base(cards)
        {

        }
        protected override bool Match()
        {
            return GetGroups(3).Count() == 1 && GetGroups(1).Count() == 2;
        }

        protected override double Remainder
        {
            get
            {
                return
                GetGroups(3).Select(c => c.Key).First() * Math.Pow(15, 3) +
                GetGroups(1).Select((c, idx) => c.Key * Math.Pow(15, idx)).Sum();
            }
        }
    }

    public class Two_Pairs : Rank
    {
        public Two_Pairs(IEnumerable<string> cards) : base(cards)
        {

        }
        protected override bool Match()
        {
            return GetGroups(2).Count() == 2;
        }

        protected override double Remainder
        {
            get
            {
                return GetGroups(2).Select((c, idx) => c.Key * Math.Pow(15, idx + 2)).Sum()
             + GetGroups(1).Select((c, idx) => c.Key * Math.Pow(15, idx)).Sum();
            }

        }
    }

    public class One_Pair : Rank
    {
        public One_Pair(IEnumerable<string> cards) : base(cards)
        {

        }
        protected override bool Match()
        {
            var groups = cards.GroupBy(c => Mapper[c[0]], c => 1);
            return GetGroups(3).Count() == 0 && GetGroups(2).Count() == 1;
        }

        protected override double Remainder
        {
            get
            {
                return GetGroups(2).Select(c => c.Key * Math.Pow(100, 3)).First()
             + GetGroups(1).Select((c, idx) => c.Key * Math.Pow(100, idx)).Sum();
            }

        }
    }

    public class High_Card : Rank
    {

        public High_Card(IEnumerable<string> cards) : base(cards)
        {

        }
        protected override bool Match()
        {
            return true;
        }

        public override Tuple<int, double> Score(int order)
        {
            var t = GetGroups(1).Select((c, idx) => c.Key * Math.Pow(15, idx)).Sum();
            return new Tuple<int, double>(order, t);
        }
    }
}
