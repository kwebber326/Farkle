using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.FarkleHands
{
    public class StraightHand : IFarkleHand
    {
        private int _pointValue;

        public string Name => CommonConstants.STRAIGHT;

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
            bool hasStraight = faceValueCounts.All(f => f == 1);

            return hasStraight;
        }
    }
}
