using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    public class ConnectionFactory
    {
        public IEnumerable<EdgeId> OneToMany<T>(GraphNode<T, UndirectedEdge> source, params T[] targets)
        {
            if (null == source)
                throw new ArgumentNullException();

            if (null == targets)
                throw new ArgumentNullException();

            ISet<EdgeId> results = new HashSet<EdgeId>();
            ISet<NodeId> adjacent = source.Graph.SelectAdjacentTo(source.Id);

            foreach (var node in source.Graph.Select(targets))
            {
                if (!adjacent.Contains(node.Id))
                {
                    var edge = new UndirectedEdge(new HashSet<NodeId> { source.Id, node.Id });
                    source.Graph.Insert(edge);
                    results.Add(edge.Id);
                }
            }

            return results;
        }

        public IEnumerable<EdgeId> ManyToMany<T>(IEnumerable<GraphNode<T, UndirectedEdge>> sources, params T[] targets)
        {
            var result = new HashSet<EdgeId>();

            foreach (var source in sources)
                result.UnionWith(OneToMany(source, targets));

            return result;
        }
    }
}
