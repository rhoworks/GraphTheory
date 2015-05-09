using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Represents a graph.
    /// </summary>
    /// <typeparam name="V">Specified type of node.</typeparam>
    /// <typeparam name="E">Specified type of edge.</typeparam>
    public interface IGraph<V, E> : IIdentifiable<GraphId>
        where V : INode
        where E : IEdge
    {
        /// <summary>
        /// Selects a node by id.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        V Select(NodeId id);

        /// <summary>
        /// Selects nodes by id.
        /// </summary>
        /// <param name="ids">The specified node ids.</param>
        IEnumerable<V> Select(params NodeId[] ids);

        /// <summary>
        /// Selects an edge by id.
        /// </summary>
        /// <param name="id">The specified edge id.</param>
        E Select(EdgeId id);

        /// <summary>
        /// Selects edges by id.
        /// </summary>
        /// <param name="ids">The specified edge ids.</param>
        IEnumerable<E> Select(params EdgeId[] ids);

        /// <summary>
        /// Selects all the nodes adjacent to a specified node by id.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        ISet<NodeId> SelectAdjacentTo(NodeId id);

        /// <summary>
        /// Selects all the nodes connected to a specified edge by id.
        /// </summary>
        /// <param name="id">The specified edge id.</param>
        ISet<NodeId> SelectConnectedTo(EdgeId id);

        /// <summary>
        /// Selects all the edges connected to a specified node by id.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        ISet<EdgeId> SelectConnectedTo(NodeId id);
    }
}
