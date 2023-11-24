using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities
{
    public static class DiceSpriteSheet
    {
        private static Image[] _diceSprites;

        public static Image[] DiceSprites
        {
            get
            {
                if (_diceSprites == null)
                {
                    _diceSprites = new Image[] 
                    {
                        Properties.Resources.dice_one,
                        Properties.Resources.dice_two,
                        Properties.Resources.dice_three,
                        Properties.Resources.dice_four,
                        Properties.Resources.dice_five,
                        Properties.Resources.dice_six
                    };
                }
                return _diceSprites;
            }
        }
    }
}
