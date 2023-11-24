using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.Interfaces
{
    public interface IFarkleHand
    {
        string Name { get; }

        int Value { get; set; }

        int NumberOfDiceInvolved { get; }

        bool IsHandInDice(List<Dice> dice);
    }
}
