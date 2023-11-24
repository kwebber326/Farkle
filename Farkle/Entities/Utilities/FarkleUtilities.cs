using Farkle.Entities.FarkleHands;
using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.Utilities
{
    public static class FarkleUtilities
    {
        public static string GetPositionText(int positionNum)
        {
            string pos = positionNum.ToString();
            string suffix = "th";
            if (pos.EndsWith("1"))
            {
                suffix = "st";
            }
            else if (pos.EndsWith("2"))
            {
                suffix = "nd";
            }
            else if (pos.EndsWith("3"))
            {
                suffix = "rd";
            }
            string retVal = $"Position: {positionNum}{suffix}";
            return retVal;
        }

        public static double GetFarkleProbability(int diceLeft)
        {
            switch (diceLeft)
            {
                case 6:
                    return .0231;
                case 5:
                    return .0772;
                case 4:
                    return .1574;
                case 3:
                    return .2778;
                case 2:
                    return .4444;
                case 1:
                    return .6667;
            }
            return 0;
        }

        public static double GetNOfAKindProbability(int diceLeft, int n)
        {
            switch (diceLeft)
            {
                case 6:
                    switch (n)
                    {
                        case 6:
                            return .000129;
                        case 5:
                            return .00386;
                        case 4:
                            return .0482;
                        case 3:
                            return .3086;
                    }
                    break;
                case 5:
                    switch (n)
                    {
                        case 6:
                            return 0;
                        case 5:
                            return .000772;
                        case 4:
                            return .0193;
                        case 3:
                            return .193;
                    }
                    break;
                case 4:
                    switch (n)
                    {
                        case 6:
                        case 5:
                            return 0;
                        case 4:
                            return .0046;
                        case 3:
                            return .0926;
                    }
                    break;
                case 3:
                    switch (n)
                    {
                        case 6:
                        case 5:
                        case 4:
                            return 0;
                        case 3:
                            return .0278;
                    }
                    break;
            }
            return 0;
        }

        public static double GetSpecialHandProbability(string handName)
        {
            switch (handName)
            {
                case CommonConstants.FULL_HOUSE:
                    return .077;
                case CommonConstants.THREE_PAIR:
                    return .0386;
                case CommonConstants.STRAIGHT:
                    return .0154;
                case CommonConstants.TRIPLETS:
                    return .0064;
            }
            return 0;
        }

        public static double GetGameTheoryExpectedValue(int currentScore, int diceLeft, List<IFarkleHand> hands, FarkleRuleSet ruleSet)
        {
            var possibleHands = hands.Where(h => h.NumberOfDiceInvolved <= diceLeft).ToList();

            List<string> specialHands = new List<string>()
            {
                CommonConstants.FULL_HOUSE,
                CommonConstants.THREE_PAIR,
                CommonConstants.TRIPLETS,
                CommonConstants.STRAIGHT
            };

            var specialHandsPossibleProbabilities = (from hand in possibleHands
                                        join sh in specialHands on
                                        hand.Name equals sh
                                        select new { Hand = hand, Probability = GetSpecialHandProbability(sh) }).ToList();

            var xOfAKindHandsPossible = possibleHands.OfType<XOfAKindHand>();
            var xOfAKindProbabilities = xOfAKindHandsPossible.Any() ?
                                            from hand in xOfAKindHandsPossible
                                            select new { Hand = hand, Probability = GetNOfAKindProbability(diceLeft, hand.NumberOfDiceInvolved) }
                                            : null;
            var averageSingleHandScore = (ruleSet.Single5Score + ruleSet.SingleOneScore) / 2;
            var farkleProbability = GetFarkleProbability(diceLeft);
            var noFarkleProbability = 1 - farkleProbability;

            double expectedValue = noFarkleProbability * (averageSingleHandScore + currentScore);

            if (xOfAKindProbabilities != null)
            {
                foreach (var hand in xOfAKindProbabilities)
                {
                    double handEV = hand.Probability * hand.Hand.Value;
                    expectedValue += handEV;
                }
            }
            if (specialHandsPossibleProbabilities.Any())
            {
                foreach (var hand in specialHandsPossibleProbabilities)
                {
                    double handEV = hand.Probability * hand.Hand.Value;
                    expectedValue += handEV;
                }
            }

            return expectedValue;
        }
    }
}
