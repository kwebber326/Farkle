using Farkle.Entities.FarkleHands;
using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities
{
    public static class FarkleHandCreationFactory
    {
        public static IFarkleHand CreateHand(string name, int pointValue)
        {
            switch (name)
            {
                case CommonConstants.SINGLE_FIVE:
                    return new FiveHand() { Value = pointValue };
                case CommonConstants.SINGLE_ONE:
                    return new OneHand() { Value = pointValue };
                case CommonConstants.FULL_HOUSE:
                    return new FullHouseHand() { Value = pointValue };
                case CommonConstants.STRAIGHT:
                    return new StraightHand() { Value = pointValue };
                case CommonConstants.ONES:
                    return new ThreeOfAKindHand(1, CommonConstants.ONES) { Value = pointValue };
                case CommonConstants.TWOS:
                    return new ThreeOfAKindHand(2, CommonConstants.TWOS) { Value = pointValue };
                case CommonConstants.THREES:
                    return new ThreeOfAKindHand(3, CommonConstants.THREES) { Value = pointValue };
                case CommonConstants.FOURS:
                    return new ThreeOfAKindHand(4, CommonConstants.FOURS) { Value = pointValue };
                case CommonConstants.FIVES:
                    return new ThreeOfAKindHand(5, CommonConstants.FIVES) { Value = pointValue };
                case CommonConstants.SIXES:
                    return new ThreeOfAKindHand(6, CommonConstants.SIXES) { Value = pointValue };
                case CommonConstants.TRIPLETS:
                    return new TripletsHand() { Value = pointValue };
                case CommonConstants.THREE_PAIR:
                    return new ThreePairHand() { Value = pointValue };
                case CommonConstants.FOUR_OF_A_KIND:
                    return new XOfAKindHand(4, CommonConstants.FOUR_OF_A_KIND) { Value = pointValue };
                case CommonConstants.FIVE_OF_A_KIND:
                    return new XOfAKindHand(5, CommonConstants.FIVE_OF_A_KIND) { Value = pointValue };
                case CommonConstants.SIX_OF_A_KIND:
                    return new XOfAKindHand(6, CommonConstants.SIX_OF_A_KIND) { Value = pointValue };

            }
            return null;
        }
    }
}
