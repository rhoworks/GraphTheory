using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Represents an edge.
    /// </summary>
    public interface IEdge : IIdentifiable<EdgeId>
    {
        /// <summary>
        /// Selects the unordered node ids associated with an edge.
        /// </summary>
        ISet<NodeId> NodeIds();
    }

    /// <summary>
    /// Represents a weighted edge.
    /// </summary>
    /// <typeparam name="W">Specified type for the edge.</typeparam>
    public interface IEdge<W> : IEdge
        where W: IComparable<W>, IEquatable<W>
    {
        /// <summary>
        /// Represents the weight of this edge.
        /// </summary>
        W Weight { get; }
    }
}
