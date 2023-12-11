using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities
{
    public class HandValidator
    {
        private readonly FarkleRuleSet _ruleSet;
        private List<IFarkleHand> _hands = new List<IFarkleHand>();
        private Dictionary<string, IFarkleHand> _scoreSheet = new Dictionary<string, IFarkleHand>();

        public HandValidator(FarkleRuleSet ruleSet)
        {
            _ruleSet = ruleSet;

            InitializeRuleSet();

            InitializeScoreSheet();
        }

        private void InitializeScoreSheet()
        {
            foreach (var hand in _hands)
            {
                _scoreSheet.Add(hand.Name, hand);
            }
        }

        private void InitializeRuleSet()
        {
            if (_ruleSet == null)
                return;

            var fiveHand = FarkleHandCreationFactory.CreateHand(CommonConstants.SINGLE_FIVE, _ruleSet.Single5Score);
            var oneHand = FarkleHandCreationFactory.CreateHand(CommonConstants.SINGLE_ONE, _ruleSet.SingleOneScore);

            var ones = FarkleHandCreationFactory.CreateHand(CommonConstants.ONES, _ruleSet.OnesScore);
            var twos = FarkleHandCreationFactory.CreateHand(CommonConstants.TWOS, _ruleSet.TwosScore);
            var threes = FarkleHandCreationFactory.CreateHand(CommonConstants.THREES, _ruleSet.ThreesScore);
            var fours = FarkleHandCreationFactory.CreateHand(CommonConstants.FOURS, _ruleSet.FoursScore);
            var fives = FarkleHandCreationFactory.CreateHand(CommonConstants.FIVES, _ruleSet.FivesScore);
            var sixes = FarkleHandCreationFactory.CreateHand(CommonConstants.SIXES, _ruleSet.SixesScore);

            var threePair = FarkleHandCreationFactory.CreateHand(CommonConstants.THREE_PAIR, _ruleSet.ThreePairScore);
            var fullHouse = FarkleHandCreationFactory.CreateHand(CommonConstants.FULL_HOUSE, _ruleSet.FullHouseScore);
            var straight = FarkleHandCreationFactory.CreateHand(CommonConstants.STRAIGHT, _ruleSet.StraightScore);

            var triplets = FarkleHandCreationFactory.CreateHand(CommonConstants.TRIPLETS, _ruleSet.DualTripletsScore);

            var fourKind = FarkleHandCreationFactory.CreateHand(CommonConstants.FOUR_OF_A_KIND, _ruleSet.FourOfAKindScore);
            var fiveKind = FarkleHandCreationFactory.CreateHand(CommonConstants.FIVE_OF_A_KIND, _ruleSet.FiveOfAKindScore);
            var sixKind = FarkleHandCreationFactory.CreateHand(CommonConstants.SIX_OF_A_KIND, _ruleSet.SixOfAKindScore);

            _hands = new List<IFarkleHand>()
            {
                fiveHand,
                oneHand,
                ones,
                twos,
                threes,
                fours,
                fives,
                sixes,
                threePair,
                fullHouse,
                straight,
                triplets,
                fourKind,
                fiveKind,
                sixKind
            }.OrderByDescending(h => h.Value).ToList();
        }

        public List<IFarkleHand> Hands
        {
            get
            {
                return _hands;
            }
        }

        public FarkleRuleSet RuleSet
        {
            get
            {
                return _ruleSet;
            }
        }

        public Dictionary<string, IFarkleHand> ScoreSheet
        {
            get
            {
                return _scoreSheet;
            }
        }

        public bool ValidateHand(List<Dice> dice, out int score)
        {
            score = -1;
            List<string> foundHands = new List<string>();
            foreach (var d in dice)
            {
                bool isValid = ValidateIndividualDice(d.FaceValue, dice, out List<string> handsHit);
                if (!isValid)
                {
                    return false;
                }
                else
                {
                    foreach(var hand in handsHit)
                    {
                        if (hand == CommonConstants.SINGLE_ONE || hand == CommonConstants.SINGLE_FIVE || !foundHands.Contains(hand))
                        foundHands.Add(hand);
                    }
                }
            }
            score = 0;
            foreach (var hand in foundHands)
            {
                var handObj = _hands.FirstOrDefault(h => string.Compare(hand, h.Name, true) == 0);
                if (handObj != null)
                {
                    score += handObj.Value;
                }
            }
            return true;
        }

        public bool HasAnyThing(List<Dice> dice)
        {
            bool hasAnything = _hands.Any(h => h.IsHandInDice(dice));
            return hasAnything;
        }

        public bool ValidateIndividualDice(int diceValue, List<Dice> rolledDice, out List<string> handsHit)
        {
            handsHit = new List<string>();

            var threePair = _hands.FirstOrDefault(h => h.Name == CommonConstants.THREE_PAIR)?.IsHandInDice(rolledDice) ?? false;
            var triplets = _hands.FirstOrDefault(h => h.Name == CommonConstants.TRIPLETS)?.IsHandInDice(rolledDice) ?? false;
            var straight = _hands.FirstOrDefault(h => h.Name == CommonConstants.STRAIGHT)?.IsHandInDice(rolledDice) ?? false;
            var fullHouse = _hands.FirstOrDefault(h => h.Name == CommonConstants.FULL_HOUSE)?.IsHandInDice(rolledDice) ?? false;
            var sixKind = _hands.FirstOrDefault(h => h.Name == CommonConstants.SIX_OF_A_KIND)?.IsHandInDice(rolledDice) ?? false;
            bool hasThreeOfAKind = rolledDice.Count(d => d.FaceValue == diceValue) >= 3;
            bool hasFourOfAKind = rolledDice.Count(d => d.FaceValue == diceValue) >= 4;
            bool hasFiveOfAKind = rolledDice.Count(d => d.FaceValue == diceValue) >= 5;

            bool singleDiceOverride = threePair || triplets || straight || fullHouse || sixKind || hasThreeOfAKind;
            bool threeKindOverride = hasFourOfAKind || threePair || triplets || straight || fullHouse || sixKind;
            bool fourKindOverride = hasFiveOfAKind || threePair || triplets || straight || fullHouse || sixKind;
            bool fiveKindOverride = sixKind;
            if (diceValue == 1 && !singleDiceOverride)
                handsHit.Add(CommonConstants.SINGLE_ONE);
            if (diceValue == 5 && !singleDiceOverride)
                handsHit.Add(CommonConstants.SINGLE_FIVE);
            if (hasThreeOfAKind && !threeKindOverride)
            {
                switch (diceValue)
                {
                    case 1:
                        handsHit.Add(CommonConstants.ONES);
                        break;
                    case 2:
                        handsHit.Add(CommonConstants.TWOS);
                        break;
                    case 3:
                        handsHit.Add(CommonConstants.THREES);
                        break;
                    case 4:
                        handsHit.Add(CommonConstants.FOURS);
                        break;
                    case 5:
                        handsHit.Add(CommonConstants.FIVES);
                        break;
                    case 6:
                        handsHit.Add(CommonConstants.SIXES);
                        break;
                }
            }
            if (hasFourOfAKind && !fourKindOverride)
                handsHit.Add(CommonConstants.FOUR_OF_A_KIND);
            if (hasFiveOfAKind && !fiveKindOverride)
                handsHit.Add(CommonConstants.FIVE_OF_A_KIND);
            if (threePair)
                handsHit.Add(CommonConstants.THREE_PAIR);
            if (triplets)
                handsHit.Add(CommonConstants.TRIPLETS);
            if (straight)
                handsHit.Add(CommonConstants.STRAIGHT);
            if (fullHouse)
                handsHit.Add(CommonConstants.FULL_HOUSE);
            if (sixKind)
                handsHit.Add(CommonConstants.SIX_OF_A_KIND);            

            return (diceValue == 1 || diceValue == 5 || threePair || triplets || straight || fullHouse || sixKind || hasThreeOfAKind);
        }
    }
}
