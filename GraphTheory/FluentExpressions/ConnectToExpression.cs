using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory.FluentExpressions
{
    /// <summary>
    /// Represents the base type for "ConnectTo" expressions.
    /// </summary>
    public abstract class ConnectToExpression<T, E>
        where E: IEdge
    {
        /// <summary>
        /// Extends the fluent syntax for edge creation.
        /// </summary>
        /// <param name="data">The target data (to be converted to nodes).</param>
        public abstract ConnectToExpression<T, UndirectedEdge> ThenTo(params T[] data);
    }

    /// <summary>
    /// Represents the Undirected Edge type for "ConnectTo" expressions.
    /// </summary>
    public class UndirectedConnectToExpression<T> : ConnectToExpression<T, UndirectedEdge>
    {
        /// <summary>
        /// Initializes a new instance of the UndirectedConnectToExpression object.
        /// </summary>
        /// <param name="graph">Reference to the graph that contains all nodes in the set of sources.</param>
        /// <param name="sources">The set of sources.</param>
        public UndirectedConnectToExpression(GraphDiagram<T, UndirectedEdge> graph, IEnumerable<GraphNode<T, UndirectedEdge>> sources)
        {
            if (null == graph)
                throw new ArgumentNullException();

            if (null == sources)
                throw new ArgumentNullException();

            this.graph = graph;
            this.sources = sources;
        }

        private readonly GraphDiagram<T, UndirectedEdge> graph;
        private readonly IEnumerable<GraphNode<T, UndirectedEdge>> sources;

        /// <summary>
        /// Extends the fluent syntax for edge creation.
        /// </summary>
        /// <param name="data">The target data (to be converted to nodes).</param>
        public override ConnectToExpression<T, UndirectedEdge> ThenTo(params T[] data)
        {
            if (null == data)
                throw new ArgumentNullException();

            var result = new HashSet<NodeId>();
            
            foreach (var source in sources)
                result.UnionWith(ThenTo(source, data));

            var targetNodes = this.graph.Select(result.ToArray());

            return new UndirectedConnectToExpression<T>(this.graph, targetNodes);
        }

        private IEnumerable<NodeId> ThenTo(GraphNode<T, UndirectedEdge> source, params T[] targets)
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

            return targetNodeIds;
        }
    }
}
