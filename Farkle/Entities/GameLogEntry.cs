using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities
{
    public class GameLogEntry
    {
        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        public string EventType { get; set; }
    }
}
