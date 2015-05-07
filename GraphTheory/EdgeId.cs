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
        /// <summary>
        /// Initializes a new unique edge id value.
        /// </summary>
        public EdgeId()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new unique edge id value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        public EdgeId(ulong value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new unique edge id value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        public EdgeId(Guid value)
            : base(value)
        {
        }
    }
}
