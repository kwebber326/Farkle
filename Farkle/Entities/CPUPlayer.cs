using Farkle.Entities.Interfaces;
using Farkle.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Farkle.Entities
{
    public class CPUPlayer : Player
    {
        private readonly HandValidator _handValidator;
        private int _scorePerDiceFavorableThreshold;

        public CPUPlayer(HandValidator handValidator)
        {
            _handValidator = handValidator;
            int fiveHand = _handValidator.ScoreSheet[CommonConstants.SINGLE_FIVE].Value;
            int oneHand = _handValidator.ScoreSheet[CommonConstants.SINGLE_ONE].Value;
            _scorePerDiceFavorableThreshold = fiveHand < oneHand ? oneHand : fiveHand;
        }

        public void OnRolledNoFarkle(List<Dice> remainingDice, int turnScore, int topScore)
        {
            List<Dice> dicePartOfHand = new List<Dice>();
            HashSet<string> hands = new HashSet<string>();
            foreach (var dice in remainingDice)
            {
                bool isPartOfHand = _handValidator.ValidateIndividualDice(dice.FaceValue, remainingDice, out List<string> handsHit);
                foreach (var hand in handsHit)
                {
                    hands.Add(hand);
                }
                if (isPartOfHand)
                {
                    dicePartOfHand.Add(dice);
                }
            }
            //hot dice logic
            if (dicePartOfHand.Count == remainingDice.Count)
            {
                foreach (var dice in dicePartOfHand)
                {
                    this.DiceClickAction?.Invoke(dice);
                }

                this.SetAsideAction.Invoke();
                //here we just grabbed all the dice if they are all in a hand.
                //Therefore, we roll so long as we haven't won already and/or we haven't broken into the game yet
                if (this.Score + turnScore < _handValidator.RuleSet.PointsToWin || (turnScore < _handValidator.RuleSet.BreakInMin && !this.IsBrokenIn))
                    this.RollAction.Invoke();
                else
                    this.BankAction.Invoke();
            }
            else
            {
                //else execute decision tree
                List<IFarkleHand> farkleHandsByScore =
                    hands.Select(h => _handValidator.ScoreSheet[h])
                    .OrderByDescending(h1 => h1.Value / h1.NumberOfDiceInvolved).ThenBy(h2 => h2.NumberOfDiceInvolved).ToList();

                //if no hands meet the value threshold we want to keep, just take the highest value hand
                //otherwise, get all the hands that meet the threshold
                List<Dice> selectedDice;
                if (farkleHandsByScore.First().Value < _scorePerDiceFavorableThreshold)
                {
                    //Grab top hand only
                    SelectTopHand(dicePartOfHand, farkleHandsByScore.First(), out selectedDice);
                }
                else
                {
                    //Grab all hands that meet the value threshold
                    SelectAllFavorableHands(dicePartOfHand, farkleHandsByScore, out selectedDice);
                }

                //after selecting dice, set aside
                this.SetAsideAction?.Invoke();

                _handValidator.ValidateHand(selectedDice, out int selectedScore);
                if (selectedScore > -1)
                {
                    turnScore += selectedScore;
                }

                //now decide whether to bank or roll
                var diceRemainingAfterSelection = remainingDice.Except(selectedDice).ToList();
                if (ShouldIGoForit(turnScore, topScore, diceRemainingAfterSelection))
                {
                    this.RollAction?.Invoke();
                }
                else
                {
                    this.BankAction.Invoke();
                }
            }
        }

        private double GetRiskAppetite(int topScore, int myScore, int pointsToWin, int turnScore)
        {
            double riskAppetite = 0;
            if (topScore == myScore)
                return riskAppetite;

            int pointsLeftToWin = pointsToWin - topScore;
            int pointDeficit = topScore - myScore;
            int benchmarkAverageTurnScore = 450;//TODO: perhaps machine learning for getting the average turn score can be used.  In the absense of data, 450 is the benchmark for average turn score
            if (pointsLeftToWin <= benchmarkAverageTurnScore)
            {
                riskAppetite = (pointDeficit - turnScore) + 1;//at the finish line, we need to go for it all
            }
            else
            {
                //otherwise, try to pick up the deficit in chunks. 
                int turnsExpectedToWin = (pointsLeftToWin / benchmarkAverageTurnScore) + 1;
                riskAppetite = (((double)pointDeficit - (double)turnScore) / (double)turnsExpectedToWin) + 1;
            }

            return riskAppetite;
        }

        private bool ShouldIGoForit(int turnScore, int topScore, List<Dice> remainingDice)
        {

            int pointsToWin = _handValidator.RuleSet.PointsToWin;
            //if I already won, don't go for it
            if (this.Score + turnScore > topScore && this.Score + turnScore >= pointsToWin)
                return false;

            //otherwise...
            double riskAppetite = GetRiskAppetite(topScore, this.Score, pointsToWin, turnScore);
            int remainingDiceCount = remainingDice.Count;
            double gameTheoryExpectedValue = FarkleUtilities.GetGameTheoryExpectedValue(turnScore, remainingDiceCount, _handValidator.Hands, _handValidator.RuleSet);
            return  (!this.IsBrokenIn && turnScore < _handValidator.RuleSet.BreakInMin)//I am not in the game yet
                   || (gameTheoryExpectedValue + riskAppetite >= turnScore) //the odds say to go for it
                   || (topScore != this.Score && topScore >= pointsToWin) //rebuttal time
                   ;
        }

        private void SelectAllFavorableHands(List<Dice> dicePartOfHand, List<IFarkleHand> farkleHands, out List<Dice> selectedDice)
        {
            selectedDice = new List<Dice>();
            foreach (var dice in dicePartOfHand)
            {
                _handValidator.ValidateIndividualDice(dice.FaceValue, dicePartOfHand, out List<string> handsHit);
                var topHandValue = (from hand in farkleHands
                                    join handName in handsHit on
                                    hand.Name equals handName
                                    select hand.Value).Max();
                if (topHandValue >= _scorePerDiceFavorableThreshold)
                {
                    this.DiceClickAction?.Invoke(dice);
                    selectedDice.Add(dice);
                }
            }
        }

        private void SelectTopHand(List<Dice> dicePartOfHand, IFarkleHand topFarkleHand, out List<Dice> selectedDice)
        {
            selectedDice = new List<Dice>();
            int diceToTake = topFarkleHand.NumberOfDiceInvolved;
            int diceTaken = 0;
            foreach (var dice in dicePartOfHand)
            {
                _handValidator.ValidateIndividualDice(dice.FaceValue, dicePartOfHand, out List<string> handsHit);
                if (handsHit.Contains(topFarkleHand.Name))
                {
                    this.DiceClickAction?.Invoke(dice);
                    selectedDice.Add(dice);
                    diceTaken++;
                    if (diceTaken >= diceToTake)
                        return;
                }
            }
        }

        public Action BankAction { get; set; }

        public Action RollAction { get; set; }

        public Action SetAsideAction { get; set; }

        public Action<Dice> DiceClickAction { get; set; }
    }
}
