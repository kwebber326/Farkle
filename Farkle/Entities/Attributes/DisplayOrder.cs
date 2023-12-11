using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.Attributes
{
    public class DisplayOrder : Attribute
    {
        public int Order { get; set; }
    }
}
