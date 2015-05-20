using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Creates edges for graph nodes.
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// Creates a connection between the source and each target.
        /// </summary>
        /// <typeparam name="T">Type for nodes.</typeparam>
        /// <param name="source">The source node.</param>
        /// <param name="targets">The target nodes.</param>
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

        /// <summary>
        /// Creates a one-to-many relationship between each source node and each target.
        /// </summary>
        /// <typeparam name="T">Type for nodes.</typeparam>
        /// <param name="sources">The set of source nodes.</param>
        /// <param name="targets">The set of target nodes.</param>
        public IEnumerable<EdgeId> ManyToMany<T>(IEnumerable<GraphNode<T, UndirectedEdge>> sources, params T[] targets)
        {
            var result = new HashSet<EdgeId>();

            foreach (var source in sources)
                result.UnionWith(OneToMany(source, targets));

            return result;
        }
    }
}
