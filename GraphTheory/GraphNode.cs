using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Represents a generic graph node.
    /// </summary>
    /// <typeparam name="T">The specified node value type.</typeparam>
    /// <typeparam name="E">The specified edge type.</typeparam>
    public class GraphNode<T, E> : INode<T>
        where E : IEdge
    {
        /// <summary>
        /// Initializes a new graph node with a unique id.
        /// </summary>
        /// <param name="value">The specified node value.</param>
        /// <param name="graph">The graph that tracks this node.</param>
        public GraphNode(T value, GraphDiagram<T, E> graph)
            : this(new NodeId(), value, graph)
        {
        }

        /// <summary>
        /// Initializes a new graph node.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        /// <param name="value">The specified node value.</param>
        /// <param name="graph">The graph that tracks this node.</param>
        public GraphNode(NodeId id, T value, GraphDiagram<T, E> graph)
        {
            if (null == id)
                throw new ArgumentNullException();

            if (null == value)
                throw new ArgumentNullException();

            if (null == graph)
                throw new ArgumentNullException();

            this.id = id;
            this.value = value;
            this.graph = graph;
        }

        private readonly NodeId id;
        private readonly T value;
        private readonly GraphDiagram<T, E> graph;

        /// <summary>
        /// Selects the id associated with this node.
        /// </summary>
        public NodeId Id { get { return this.id; } }

        /// <summary>
        /// Selects the value associated with this node.
        /// </summary>
        public T Value { get { return this.value; } }

        /// <summary>
        /// Selects the graph associated with this node.
        /// </summary>
        public GraphDiagram<T, E> Graph { get { return this.graph; } }
    }
}
