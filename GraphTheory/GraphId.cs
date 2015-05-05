using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Object used to uniquely identify graphs.
    /// </summary>
    public class GraphId : Id
    {
        public GraphId()
            : this(Guid.NewGuid())
        {
        }

        public GraphId(ulong value)
            : base(value)
        {
        }

        public GraphId(Guid value)
            : base(value)
        {
        }
    }
}
