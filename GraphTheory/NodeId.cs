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
        /// <summary>
        /// Initializes a new unique node id value.
        /// </summary>
        public NodeId()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new unique node id value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        public NodeId(ulong value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new unique node id value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        public NodeId(Guid value)
            : base(value)
        {
        }
    }
}
