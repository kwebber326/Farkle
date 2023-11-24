using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.Attributes
{
    public class WriteData : Attribute
    {
        public bool WriteToGameStats { get; set; }

        public bool WriteToPlayerStats { get; set; }
    }
}
