using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphTheory.Extensions;

namespace GraphTheory
{
    /// <summary>
    /// Identifier used by graphs, nodes, edges, etc. to uniquely identify objects.
    /// </summary>
    public abstract class Id : IComparable<Id>, IEquatable<Id>
    {
        /// <summary>
        /// Initializes a new instance of an Id.
        /// </summary>
        /// <param name="value">Specifies the value of this Id.</param>
        protected Id(Guid value)
        {
            if (Guid.Empty.Equals(value))
                throw new ArgumentException(); // todo: indicate argument name -- wait for C# 6

            this.value = value.ToUlong();
        }

        /// <summary>
        /// Initializes a new instance of an Id.
        /// </summary>
        /// <param name="value">Specifies the value of this Id.</param>
        protected Id(ulong value)
        {
            this.value = value;
        }

        private readonly ulong value;

        /// <summary>
        /// This is the value used by dictionaries and hash tables to identify corresponding objects.
        /// </summary>
        public ulong Value { get { return this.value; } }

        /// <summary>
        /// Compares this instance to a specified Id and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">An Id to compare.</param>
        public int CompareTo(Id other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException(); // todo: indicate the argument name -- wait for C# 6

            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        public bool Equals(Id other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return Value.Equals(other.Value);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as Id);
        }


        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="lhs">The left hand side.</param>
        /// <param name="rhs">The right hand side.</param>
        public static bool operator ==(Id lhs, Id rhs)
        {
            if (object.ReferenceEquals(lhs, rhs)) return true;
            if (object.ReferenceEquals(lhs, null)) return false;
            if (object.ReferenceEquals(rhs, null)) return false;

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether the specified objects are not equal.
        /// </summary>
        /// <param name="lhs">The left hand side.</param>
        /// <param name="rhs">The right hand side.</param>
        public static bool operator !=(Id lhs, Id rhs)
        {
            return !(lhs == rhs);
        }
    }
}
