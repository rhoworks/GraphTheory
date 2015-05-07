using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    public abstract class GraphDiagram<T, E> : Diagram<GraphNode<T, E>, E>
        where E : IEdge
    {
        protected GraphDiagram()
            : this(new GraphId())
        {
        }

        protected GraphDiagram(GraphId id)
            : this(id, new List<GraphNode<T, E>>())
        {
        }

        protected GraphDiagram(GraphId id, IEnumerable<GraphNode<T, E>> nodes)
            : this(id, nodes, new List<E>())
        {
        }

        protected GraphDiagram(GraphId id, IEnumerable<GraphNode<T, E>> nodes, IEnumerable<E> edges)
            : base(id, new Dictionary<NodeId, GraphNode<T, E>>(), new Dictionary<EdgeId, E>())
        {
            if (null == nodes)
                throw new ArgumentNullException();

            if (null == edges)
                throw new ArgumentNullException();

            this.connected = new Dictionary<NodeId, ISet<EdgeId>>();
            this.lookup = new Dictionary<T, ISet<NodeId>>();

            foreach (var node in nodes)
                Insert(node);

            foreach (var edge in edges)
                Insert(edge);
        }

        private readonly IDictionary<NodeId, ISet<EdgeId>> connected;
        private readonly IDictionary<T, ISet<NodeId>> lookup;

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

        public IEnumerable<GraphNode<T, E>> Select(T datum, params T[] data)
        {
            yield return Select(datum);

            foreach (T arg in data)
                yield return Select(arg);
        }

        public GraphNode<T, E> Select(T data)
        {
            if (null == data)
                throw new ArgumentNullException();

            if (!this.lookup.ContainsKey(data))
                throw new Exception();

            NodeId id = this.lookup[data].First();

            return Select(id);
        }

        public IEnumerable<GraphNode<T, E>> Insert(T data, params T[] more)
        {
            yield return Insert(data);

            foreach (T arg in more)
                yield return Insert(arg);
        }

        public GraphNode<T, E> Insert(T data)
        {
            return Insert(new GraphNode<T, E>(data, this));
        }

        public IEnumerable<GraphNode<T, E>> Insert(GraphNode<T, E> node, params GraphNode<T, E>[] more)
        {
            yield return Insert(node);

            foreach (GraphNode<T, E> arg in more)
                yield return Insert(arg);
        }

        public GraphNode<T, E> Insert(GraphNode<T, E> node)
        {
            if (null == node)
                throw new ArgumentNullException();

            if (this.nodes.ContainsKey(node.Id))
                throw new Exception();

            this.nodes.Add(node.Id, node);
            this.connected.Add(node.Id, new HashSet<EdgeId>());

            if (!this.lookup.ContainsKey(node.Value))
                this.lookup.Add(node.Value, new HashSet<NodeId>());

            this.lookup[node.Value].Add(node.Id);

            return node;
        }

        public void Remove(NodeId id, params NodeId[] more)
        {
            Remove(id);

            foreach (NodeId arg in more)
                Remove(arg);
        }

        public void Remove(NodeId id)
        {
            if (null == id)
                throw new ArgumentNullException();

            foreach (EdgeId edgeId in SelectConnectedTo(id))
                Remove(edgeId);

            GraphNode<T, E> node = Select(id);
            this.lookup[node.Value].Remove(id);
            this.nodes.Remove(id);
        }

        public IEnumerable<E> Insert(E edge, params E[] more)
        {
            yield return Insert(edge);

            foreach (E arg in more)
                yield return Insert(arg);
        }

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

        public void Remove(EdgeId id, params EdgeId[] more)
        {
            Remove(id);

            foreach (EdgeId arg in more)
                Remove(arg);
        }

        public void Remove(EdgeId id)
        {
            if (null == id)
                throw new ArgumentNullException();

            foreach (NodeId nodeId in SelectConnectedTo(id))
                this.connected[nodeId].Remove(id);

            this.edges.Remove(id);
        }
    }

    public class Graph<T> : GraphDiagram<T, UndirectedEdge>
    {
        public Graph()
            : this(new GraphId())
        {
        }

        public Graph(GraphId id)
            : this(id, new List<GraphNode<T, UndirectedEdge>>())
        {
        }

        public Graph(GraphId id, IEnumerable<GraphNode<T, UndirectedEdge>> nodes)
            : this(id, nodes, new List<UndirectedEdge>())
        {
        }

        public Graph(GraphId id, IEnumerable<GraphNode<T, UndirectedEdge>> nodes, IEnumerable<UndirectedEdge> edges)
            : base(id, nodes, edges)
        {
        }
    }

    public class Graph<T, W> : GraphDiagram<T, UndirectedEdge<W>>
        where W: IComparable<W>, IEquatable<W>
    {
        public Graph()
            : this(new GraphId())
        {
        }

        public Graph(GraphId id)
            : this(id, new List<GraphNode<T, UndirectedEdge<W>>>())
        {
        }

        public Graph(GraphId id, IEnumerable<GraphNode<T, UndirectedEdge<W>>> nodes)
            : this(id, nodes, new List<UndirectedEdge<W>>())
        {
        }

        public Graph(GraphId id, IEnumerable<GraphNode<T, UndirectedEdge<W>>> nodes, IEnumerable<UndirectedEdge<W>> edges)
            : base(id, nodes, edges)
        {
        }
    }
}
