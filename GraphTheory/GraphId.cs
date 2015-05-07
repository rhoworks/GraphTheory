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
        /// <summary>
        /// Initializes a new unique graph id value.
        /// </summary>
        public GraphId()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new unique graph id value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        public GraphId(ulong value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new unique graph id value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        public GraphId(Guid value)
            : base(value)
        {
        }
    }
}
