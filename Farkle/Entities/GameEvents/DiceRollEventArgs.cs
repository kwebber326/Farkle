using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.GameEvents
{
    public class DiceRollEventArgs : EventArgs
    {
        public int FaceValue { get; set; }
    }
}
