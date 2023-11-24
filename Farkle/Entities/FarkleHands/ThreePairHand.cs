using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.FarkleHands
{
    public class ThreePairHand : IFarkleHand
    {
        private int _pointValue;

        public string Name => CommonConstants.THREE_PAIR;

        public int Value { get => _pointValue; set => _pointValue = value; }

        public int NumberOfDiceInvolved => 6;

        public bool IsHandInDice(List<Dice> dice)
        {

            if (dice == null || dice.Count < 6)
                return false;

            int[] faceValueCounts = new int[6];

            foreach (var di in dice)
            {
                faceValueCounts[di.FaceValue - 1]++;
            }
            bool hasThreePair = faceValueCounts.Count(f => f == 2) == 3;

            return hasThreePair;
        }
    }
}
