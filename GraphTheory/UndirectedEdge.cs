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
        /// Initializes a new undirected edge with a random id and specified set of nodes.
        /// </summary>
        /// <param name="nodeIds">The unordered nodes of the edge.</param>
        public UndirectedEdge(ISet<NodeId> nodeIds)
            : this(new EdgeId(), nodeIds)
        {
        }

        /// <summary>
        /// Initializes a new undirected edge with a specified id and set of nodes.
        /// </summary>
        /// <param name="id">The id for the edge.</param>
        /// <param name="nodeIds">The unordered nodes of the edge.</param>
        public UndirectedEdge(EdgeId id, ISet<NodeId> nodeIds)
        {
            if (null == id)
                throw new ArgumentNullException();

            if (null == nodeIds)
                throw new ArgumentNullException();

            this.id = id;
            this.nodeIds = nodeIds;
        }

        private readonly EdgeId id;
        private readonly ISet<NodeId> nodeIds;

        /// <summary>
        /// Unique Id for the current edge.
        /// </summary>
        public EdgeId Id { get { return this.id; } }

        /// <summary>
        /// Selects the unordered node ids associated with an edge.
        /// </summary>
        public ISet<NodeId> NodeIds()
        {
            return this.nodeIds;
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
        /// Initializes a new weighted undirected edge with a random id and specified weight.
        /// </summary>
        /// <param name="weight">The weight for the edge.</param>
        /// <param name="nodeIds">The unordered nodes of the edge.</param>
        public UndirectedEdge(W weight, ISet<NodeId> nodeIds)
            : this(new EdgeId(), weight, nodeIds)
        {
        }

        /// <summary>
        /// Initializes a new weighted undirected edge with a specified id and specified weight.
        /// </summary>
        /// <param name="id">The id for the edge.</param>
        /// <param name="weight">The weight for the edge.</param>
        /// <param name="nodeIds">The unordered nodes of the edge.</param>
        public UndirectedEdge(EdgeId id, W weight, ISet<NodeId> nodeIds)
            : base(id, nodeIds)
        {
            if (null == weight)
                throw new ArgumentNullException();

            this.weight = weight;
        }

        private readonly W weight;

        /// <summary>
        /// Represents the weight of this edge.
        /// </summary>
        public W Weight { get { return this.weight; } }
    }
}
