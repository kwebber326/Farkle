using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.FarkleHands
{
    public class XOfAKindHand : IFarkleHand
    {
        private readonly string _name;
        private readonly int _number;
        private int _pointValue;

        public XOfAKindHand(int numberOfAkind, string name)
        {
            _name = name;
            _number = numberOfAkind;
        }

        public string Name => _name;

        public int Value { get => _pointValue; set => _pointValue = value; }

        public int NumberOfDiceInvolved => _number;

        public bool IsHandInDice(List<Dice> dice)
        {
            if (dice == null || dice.Count < _number)
                return false;

            int[] faceValueCounts = new int[6];

            foreach (var di in dice)
            {
                faceValueCounts[di.FaceValue - 1]++;
                if (faceValueCounts[di.FaceValue - 1] == _number)
                    return true;
            }

            return false;
        }
    }
}
