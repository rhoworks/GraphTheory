using GraphTheory.FluentExpressions;
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

    /// <summary>
    /// Extension methods for GraphNode objects.
    /// </summary>
    public static class GraphNodeExtensions
    {
        /// <summary>
        /// Selects the nodes adjacent to this node.
        /// </summary>
        /// <typeparam name="T">Type for nodes.</typeparam>
        /// <param name="source">The source node.</param>
        public static IEnumerable<GraphNode<T, UndirectedEdge>> SelectAdjacent<T>(this GraphNode<T, UndirectedEdge> source)
        {
            if (null == source)
                throw new ArgumentNullException();

            ISet<NodeId> adjacentIds = source.Graph.SelectAdjacentTo(source.Id);
            return source.Graph.Select(adjacentIds.ToArray());
        }

        /// <summary>
        /// Selects the edges connected to this node.
        /// </summary>
        /// <typeparam name="T">Type for nodes.</typeparam>
        /// <param name="source">The source node.</param>
        public static IEnumerable<UndirectedEdge> SelectEdges<T>(this GraphNode<T, UndirectedEdge> source)
        {
            if (null == source)
                throw new ArgumentNullException();

            ISet<EdgeId> edges = source.Graph.SelectConnectedTo(source.Id);
            return source.Graph.Select(edges.ToArray());
        }

        /// <summary>
        /// Connects the current source node to destination nodes selected by value.
        /// </summary>
        /// <typeparam name="T">Type for nodes.</typeparam>
        /// <param name="source">The source node.</param>
        /// <param name="targets">The destination nodes selected by value.</param>
        public static ConnectToExpression<T, UndirectedEdge> ConnectTo<T>(this GraphNode<T, UndirectedEdge> source, params T[] targets)
        {
            if (null == source)
                throw new ArgumentNullException();

            if (null == targets)
                throw new ArgumentNullException();

            var factory = new ConnectionFactory();
            var edgeIds = factory.OneToMany(source, targets);

            var edges = source.Graph.Select(edgeIds.ToArray());

            var targetNodeIds = new HashSet<NodeId>();
            foreach (UndirectedEdge edge in edges)
                targetNodeIds.UnionWith(edge.NodeIds());

            targetNodeIds.Remove(source.Id);

            var targetNodes = source.Graph.Select(targetNodeIds.ToArray());
            return new UndirectedConnectToExpression<T>(source.Graph, targetNodes.AsEnumerable());
        }

        /// <summary>
        /// Connects each source node to each destination node selected by value.
        /// </summary>
        /// <typeparam name="T">Type for nodes.</typeparam>
        /// <param name="source">The source nodes.</param>
        /// <param name="targets">The destination nodes selected by value.</param>
        public static ConnectToExpression<T, UndirectedEdge> ConnectTo<T>(this IEnumerable<GraphNode<T, UndirectedEdge>> source, params T[] targets)
        {
            if (null == source)
                throw new ArgumentNullException();

            if (null == targets)
                throw new ArgumentNullException();

            var factory = new ConnectionFactory();
            var edgeIds = factory.ManyToMany(source, targets);

            var graph = source.First().Graph;

            var edges = graph.Select(edgeIds.ToArray());

            var targetNodeIds = new HashSet<NodeId>();
            
            foreach (UndirectedEdge edge in edges)
                targetNodeIds.UnionWith(edge.NodeIds());

            foreach (GraphNode<T, UndirectedEdge> node in source)
                targetNodeIds.Remove(node.Id);

            var targetNodes = source.First().Graph.Select(targetNodeIds.ToArray());
            return new UndirectedConnectToExpression<T>(graph, targetNodes.AsEnumerable());
        }
    }
}
