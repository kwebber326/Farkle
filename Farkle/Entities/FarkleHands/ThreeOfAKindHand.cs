using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.FarkleHands
{
    public class ThreeOfAKindHand : IFarkleHand
    {
        private readonly int _faceValueToCheckFor;
        private readonly string _name;
        private int _pointValue;

        public ThreeOfAKindHand(int faceValue, string name)
        {
            _faceValueToCheckFor = faceValue;
            _name = name;
        }

        public string Name => _name;

        public int Value { get => _pointValue; set => _pointValue = value; }

        public int NumberOfDiceInvolved => 3;

        public bool IsHandInDice(List<Dice> dice)
        {
            if (dice == null || dice.Count < 3)
                return false;

            int numberCount = 0;
            foreach (var di in dice)
            {
                if (di.FaceValue == _faceValueToCheckFor)
                {
                    numberCount++;
                    if (numberCount == 3)
                        return true;
                }
            }
            return false;
        }
    }
}
