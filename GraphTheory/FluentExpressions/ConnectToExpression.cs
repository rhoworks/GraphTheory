using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory.FluentExpressions
{
    public abstract class ConnectToExpression<T, E>
        where E: IEdge
    {
        public abstract ConnectToExpression<T, UndirectedEdge> ThenTo(params T[] data);
    }

    public class UndirectedConnectToExpression<T> : ConnectToExpression<T, UndirectedEdge>
    {
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
