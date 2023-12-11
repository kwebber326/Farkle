using Farkle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.CustomEventArgs
{
    public class GameActionEventArgs
    {
        public GameLogEntry LogEntryData { get; set; }

        public int TurnScore { get; set; }
        public int DiceRemaining { get; set; }
        public string Hand { get; set; }

        public double? ExpectedValueOfRoll { get; set; }
    }
}
