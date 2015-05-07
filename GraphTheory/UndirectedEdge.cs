using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Represents an undirected edge.
    /// </summary>
    public class UndirectedEdge : IEdge
    {
        /// <summary>
        /// Unique Id for the current edge.
        /// </summary>
        public EdgeId Id { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Selects the unordered node ids associated with an edge.
        /// </summary>
        public ISet<NodeId> NodeIds()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents a weighted undirected edge.
    /// </summary>
    /// <typeparam name="W">The specified type of weight.</typeparam>
    public class UndirectedEdge<W> : UndirectedEdge, IEdge<W>
        where W: IComparable<W>, IEquatable<W>
    {
        /// <summary>
        /// Represents the weight of this edge.
        /// </summary>
        public W Weight { get { throw new NotImplementedException(); } }
    }
}
