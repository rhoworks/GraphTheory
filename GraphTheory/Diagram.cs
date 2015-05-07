using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Represents the base type for all graphs.
    /// </summary>
    /// <typeparam name="V">The specified node type.</typeparam>
    /// <typeparam name="E">The specified edge type.</typeparam>
    public abstract class Diagram<V, E> : IGraph<V, E>
        where V : INode
        where E : IEdge
    {
        /// <summary>
        /// Initializes a diagram.
        /// </summary>
        /// <param name="id">The specified graph id.</param>
        /// <param name="nodes">The initial set of nodes.</param>
        /// <param name="edges">The initial set of edges.</param>
        protected Diagram(GraphId id, IDictionary<NodeId, V> nodes, IDictionary<EdgeId, E> edges)
        {
            if (null == id)
                throw new ArgumentNullException();

            if (null == nodes)
                throw new ArgumentNullException();

            if (null == edges)
                throw new ArgumentNullException();

            this.id = id;
            this.nodes = nodes;
            this.edges = edges;
        }

        private readonly GraphId id;
        protected readonly IDictionary<NodeId, V> nodes;
        protected readonly IDictionary<EdgeId, E> edges;

        /// <summary>
        /// Selects all the edges connected to a specified node by id.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        public abstract ISet<EdgeId> SelectConnectedTo(NodeId id);

        /// <summary>
        /// Selects the graph id associated with this diagram.
        /// </summary>
        public GraphId Id { get { return this.id; } }

        /// <summary>
        /// Selects the node by id.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        public V Select(NodeId id)
        {
            if (null == id)
                throw new ArgumentNullException();

            return this.nodes[id];
        }

        /// <summary>
        /// Selects and edge by id.
        /// </summary>
        /// <param name="id">The specified edge id.</param>
        public E Select(EdgeId id)
        {
            if (null == id)
                throw new ArgumentNullException();

            return this.edges[id];
        }

        /// <summary>
        /// Selects all the nodes adjacent to a specified node by id.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        public ISet<NodeId> SelectAdjacentTo(NodeId id)
        {
            if (null == id)
                throw new ArgumentNullException();

            var adjacent = new HashSet<NodeId>();

            foreach (EdgeId edgeId in SelectConnectedTo(id))
                adjacent.UnionWith(Select(edgeId).NodeIds());

            adjacent.Remove(id);

            return adjacent;
        }

        /// <summary>
        /// Selects all the edges connected to a specified node by id.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        public ISet<NodeId> SelectConnectedTo(EdgeId id)
        {
            return Select(id).NodeIds();
        }
    }
}
