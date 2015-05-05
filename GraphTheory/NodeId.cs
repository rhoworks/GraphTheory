using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Object used to uniquely identify nodes in graphs.
    /// </summary>
    public class NodeId : Id
    {
        public NodeId()
            : this(Guid.NewGuid())
        {
        }

        public NodeId(ulong value)
            : base(value)
        {
        }

        public NodeId(Guid value)
            : base(value)
        {
        }
    }
}
