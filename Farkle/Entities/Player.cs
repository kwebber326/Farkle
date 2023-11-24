using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities
{
    public class Player
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public int Position { get; set; }

        public bool IsMyTurn { get; set; }

        public bool IsBrokenIn { get; set; }
    }
}
