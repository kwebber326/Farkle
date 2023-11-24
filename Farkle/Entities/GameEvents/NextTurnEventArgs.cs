using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.GameEvents
{
    public class NextTurnEventArgs : EventArgs
    {
        public int Score { get; set; }
    }
}
