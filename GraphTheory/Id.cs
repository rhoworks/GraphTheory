using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphTheory.Extensions;

namespace GraphTheory
{
    /// <summary>
    /// Identifier that uses GUIDs for uniqueness.
    /// Used by graphs, nodes, edges, etc. to uniquely identify objects.
    /// </summary>
    public abstract class Id : IComparable<Id>, IEquatable<Id>
    {
        protected Id(Guid value)
        {
            if (Guid.Empty.Equals(value))
                throw new ArgumentException(); // todo: indicate argument name -- wait for C# 6

            this.value = value.ToUlong();
        }

        protected Id(ulong value)
        {
            this.value = value;
        }

        private readonly ulong value;

        /// <summary>
        /// This is the value used by dictionaries and hash tables to identify corresponding objects.
        /// </summary>
        public ulong Value { get { return this.value; } }

        public int CompareTo(Id other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException(); // todo: indicate the argument name -- wait for C# 6

            return Value.CompareTo(other.Value);
        }

        public bool Equals(Id other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as Id);
        }

        public static bool operator ==(Id lhs, Id rhs)
        {
            if (object.ReferenceEquals(lhs, rhs)) return true;
            if (object.ReferenceEquals(lhs, null)) return false;
            if (object.ReferenceEquals(rhs, null)) return false;

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Id lhs, Id rhs)
        {
            if (object.ReferenceEquals(lhs, rhs)) return false;
            if (object.ReferenceEquals(lhs, null)) return true;
            if (object.ReferenceEquals(rhs, null)) return true;

            return !lhs.Equals(rhs);
        }
    }
}
