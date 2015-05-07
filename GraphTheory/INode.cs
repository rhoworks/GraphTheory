using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Represents a node.
    /// </summary>
    public interface INode : IIdentifiable<NodeId>
    {
    }

    /// <summary>
    /// Represents a node.
    /// </summary>
    /// <typeparam name="V">Specified type for the node's value.</typeparam>
    public interface INode<V> : INode
    {
        /// <summary>
        /// Represents the value of the current node.
        /// </summary>
        V Value { get; }
    }
}
