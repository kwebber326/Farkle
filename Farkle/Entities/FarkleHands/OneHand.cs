using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.FarkleHands
{
    public class OneHand : IFarkleHand
    {
        private int _pointValue;

        public string Name => CommonConstants.SINGLE_ONE;

        public int Value { get => _pointValue; set => _pointValue = value; }

        public int NumberOfDiceInvolved => 1;

        public bool IsHandInDice(List<Dice> dice)
        {
            if (dice == null)
                return false;
            bool hasOne = dice.Any(f => f.FaceValue == 1);
            return hasOne;
        }
    }
}
