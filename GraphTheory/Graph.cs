using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// The base type for weighted and unweighted graphs.
    /// </summary>
    /// <typeparam name="T">Type for nodes.</typeparam>
    /// <typeparam name="E">Type of edge.</typeparam>
    public abstract class GraphDiagram<T, E> : Diagram<GraphNode<T, E>, E>
        where E : IEdge
    {
        /// <summary>
        /// Initializes a GraphDiagram with a specific id and custom comparer to be used by the data tracker.
        /// </summary>
        /// <param name="id">The id for the graph.</param>
        /// <param name="comparer">The comparer used by the data tracker (Dictionary object).</param>
        protected GraphDiagram(GraphId id, IEqualityComparer<T> comparer)
            : base(id, new Dictionary<NodeId, GraphNode<T, E>>(), new Dictionary<EdgeId, E>())
        {
            this.connected = new Dictionary<NodeId, ISet<EdgeId>>();

            if (null == comparer)
                this.tracker = new Dictionary<T, ISet<NodeId>>();
            else
                this.tracker = new Dictionary<T, ISet<NodeId>>(comparer);
        }

        private readonly IDictionary<NodeId, ISet<EdgeId>> connected;
        private readonly IDictionary<T, ISet<NodeId>> tracker;

        /// <summary>
        /// Selects all the edges connected to a specified node by id.
        /// </summary>
        /// <param name="id">The specified node id.</param>
        public override ISet<EdgeId> SelectConnectedTo(NodeId id)
        {
            if (null == id)
                throw new ArgumentNullException();

            return this.connected[id];
        }

        /// <summary>
        /// Selects a node by value (first instance only).
        /// </summary>
        /// <param name="data">The value to lookup the node by.</param>
        public GraphNode<T, E> Select(T data)
        {
            if (null == data)
                throw new ArgumentNullException();

            if (!this.tracker.ContainsKey(data))
                throw new Exception();

            NodeId id = this.tracker[data].First();

            return Select(id);
        }

        /// <summary>
        /// Selects nodes by value (first instance only).
        /// </summary>
        /// <param name="data">The values to lookup the node by.</param>
        public IEnumerable<GraphNode<T, E>> Select(params T[] data)
        {
            foreach (T item in data)
                yield return Select(item);
        }

        /// <summary>
        /// Selects all nodes that have a specific value.
        /// </summary>
        /// <param name="data">The value to lookup the nodes by.</param>
        /// <returns></returns>
        public IEnumerable<GraphNode<T, E>> SelectAll(T data)
        {
            if (null == data)
                throw new ArgumentNullException();

            if (!this.tracker.ContainsKey(data))
                throw new Exception();

            foreach (NodeId id in this.tracker[data])
                yield return Select(id);
        }

        /// <summary>
        /// Selects all nodes that have a specific value.
        /// </summary>
        /// <param name="data">The values to lookup the nodes by.</param>
        public IEnumerable<GraphNode<T, E>> SelectAll(params T[] data)
        {
            if (null == data)
                throw new ArgumentNullException();

            foreach (T item in data)
            {
                if (!this.tracker.ContainsKey(item))
                    throw new Exception();
            }

            foreach (T item in data)
            {
                foreach (NodeId id in this.tracker[item])
                    yield return Select(id);
            }
        }

        /// <summary>
        /// Inserts new nodes into the graph by specifying values.
        /// </summary>
        /// <param name="data">The values to create nodes by.</param>
        public IEnumerable<GraphNode<T, E>> Insert(params T[] data)
        {
            if (null == data)
                throw new ArgumentNullException();

            var nodes = new List<GraphNode<T, E>>();

            foreach (T item in data)
                nodes.Add(Insert(new GraphNode<T, E>(item, this)));

            return nodes;
        }


        /// <summary>
        /// Inserts a new node into the graph.
        /// </summary>
        /// <param name="node">The node to be added to the graph.</param>
        public GraphNode<T, E> Insert(GraphNode<T, E> node)
        {
            if (null == node)
                throw new ArgumentNullException();

            if (this.nodes.ContainsKey(node.Id))
                throw new Exception();

            this.nodes.Add(node.Id, node);
            this.connected.Add(node.Id, new HashSet<EdgeId>());

            if (!this.tracker.ContainsKey(node.Value))
                this.tracker.Add(node.Value, new HashSet<NodeId>());

            this.tracker[node.Value].Add(node.Id);

            return node;
        }

        /// <summary>
        /// Inserts a new edge into the graph.
        /// </summary>
        /// <param name="edge">The edge to be added to the graph.</param>
        public E Insert(E edge)
        {
            if (null == edge)
                throw new ArgumentNullException();

            if (this.edges.ContainsKey(edge.Id))
                throw new Exception();

            foreach (NodeId nodeId in edge.NodeIds())
            {
                if (!this.nodes.ContainsKey(nodeId))
                    throw new Exception();
            }

            this.edges.Add(edge.Id, edge);

            foreach (NodeId nodeId in edge.NodeIds())
                this.connected[nodeId].Add(edge.Id);

            return edge;
        }

        /// <summary>
        /// Removes nodes by id.
        /// </summary>
        /// <param name="ids">The ids to remove nodes by.</param>
        public void Remove(params NodeId[] ids)
        {
            if (null == ids)
                throw new ArgumentNullException();

            foreach (NodeId id in ids)
            {
                if (!this.nodes.ContainsKey(id))
                    throw new Exception();
            }

            foreach (NodeId id in ids)
            {
                foreach (EdgeId edgeId in SelectConnectedTo(id))
                    Remove(edgeId);

                GraphNode<T, E> node = Select(id);
                this.tracker[node.Value].Remove(id);
                this.nodes.Remove(id);
            }
        }

        /// <summary>
        /// Removes edges by id.
        /// </summary>
        /// <param name="ids">The ids to remove edges by.</param>
        public void Remove(params EdgeId[] ids)
        {
            if (null == ids)
                throw new ArgumentNullException();

            foreach (EdgeId id in ids)
            {
                if (!this.edges.ContainsKey(id))
                    throw new Exception();
            }

            foreach (EdgeId id in ids)
            {
                foreach (NodeId nodeId in SelectConnectedTo(id))
                    this.connected[nodeId].Remove(id);

                this.edges.Remove(id);
            }
        }
    }

    /// <summary>
    /// Represents an unweighted graph.
    /// </summary>
    /// <typeparam name="T">Type for nodes.</typeparam>
    public class Graph<T> : GraphDiagram<T, UndirectedEdge>
    {
        /// <summary>
        /// Initializes a new unweighted graph with a random id.
        /// </summary>
        public Graph()
            : this(new GraphId())
        {
        }

        /// <summary>
        /// Initializes a new unweighted graph with a specified id.
        /// </summary>
        /// <param name="id">The id used by the graph.</param>
        public Graph(GraphId id)
            : this(id, null)
        {
        }

        /// <summary>
        /// Initializes a new unweighted graph with a specific id and custom comparer to be used by the data tracker.
        /// </summary>
        /// <param name="id">The id for the graph.</param>
        /// <param name="comparer">The comparer used by the data tracker (Dictionary object).</param>
        public Graph(GraphId id, IEqualityComparer<T> comparer)
            : base(id, comparer)
        {
        }
    }

    /// <summary>
    /// Represents a weighted graph.
    /// </summary>
    /// <typeparam name="T">Type for nodes.</typeparam>
    /// <typeparam name="W">Type for edge weights.</typeparam>
    public class Graph<T, W> : GraphDiagram<T, UndirectedEdge<W>>
        where W: IComparable<W>, IEquatable<W>
    {
        /// <summary>
        /// Initializes a new weighted graph with a random id.
        /// </summary>
        public Graph()
            : this(new GraphId())
        {
        }

        /// <summary>
        /// Initializes a new weighted graph with a specified id.
        /// </summary>
        /// <param name="id">The id used by the graph.</param>
        public Graph(GraphId id)
            : this(id, null)
        {
        }

        /// <summary>
        /// Initializes a new weighted graph with a specific id and custom comparer to be used by the data tracker.
        /// </summary>
        /// <param name="id">The id for the graph.</param>
        /// <param name="comparer">The comparer used by the data tracker (Dictionary object).</param>
        public Graph(GraphId id, IEqualityComparer<T> comparer)
            : base(id, comparer)
        {
        }
    }
}
