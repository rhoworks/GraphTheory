using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Object used to uniquely identify edges in graphs.
    /// </summary>
    public class EdgeId : Id
    {
        public EdgeId()
            : this(Guid.NewGuid())
        {
        }

        public EdgeId(ulong value)
            : base(value)
        {
        }

        public EdgeId(Guid value)
            : base(value)
        {
        }
    }
}
