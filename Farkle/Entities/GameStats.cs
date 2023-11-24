using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities
{
    public class GameStats
    {
        public TimeSpan GameTime { get; set; } 

        public List<PlayerStats> PlayerStats { get; set; }

        public void ManageGameStats(string playerName, int turnScore, bool banked)
        {

        }
    }
}
