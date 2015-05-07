using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphTheory.Extensions;

namespace GraphTheory.Tests
{
    [TestFixture]
    public class IdTests
    {
        [Test]
        public void GraphId_New_Works()
        {
            var id = new GraphId();
            Assert.That(id.Value, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GraphId_New_Fails_When_Value_Set_To_Empty_Guid()
        {
            Assert.That(() => new GraphId(Guid.Empty), Throws.ArgumentException);
        }

        [Test]
        [TestCase(123456789UL)]
        public void GraphId_New_Works_When_Value_Set_To(ulong expected)
        {
            var id = new GraphId(expected);
            Assert.That(id.Value, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{909D5DAB-FE4F-4EB0-8A8B-2F07AD283D20}")]
        public void GraphId_New_Works_When_Value_Set_To(string expected)
        {
            var value = Guid.Parse(expected);
            var id = new GraphId(value);
            Assert.That(id.Value, Is.EqualTo(value.ToUlong()));
        }

        [Test]
        public void GraphId_CompareTo_Works()
        {
            var id = new GraphId();
            var other = new GraphId();

            Assert.That(id, Is.Not.EqualTo(other));
            Assert.That(id.Value, Is.Not.EqualTo(other.Value));
        }

        [Test]
        public void GraphId_CompareTo_Fails_When_Passing_Null()
        {
            var id = new GraphId();
            Assert.That(() => id.CompareTo(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase("{909D5DAB-FE4F-4EB0-8A8B-2F07AD283D20}")]
        public void GraphId_CompareTo_Works_When_Value_Set_To(string expected)
        {
            var value = Guid.Parse(expected);
            var id = new GraphId(value);
            var other = new GraphId(value);

            Assert.True(id.CompareTo(other) == 0);
        }

        [Test]
        public void GraphId_LogicalEqualsOp_Works()
        {
            GraphId lhs = new GraphId();
            GraphId rhs = new GraphId();

            Assert.False(lhs == rhs);
        }

        [Test]
        public void GraphId_LogicalEqualsOp_Works_For_Equal_References()
        {
            GraphId x = new GraphId();
            
            #pragma warning disable 1718
            Assert.True(x == x);
            #pragma warning restore 1718
        }

        [Test]
        public void GraphId_LogicalNotEqualsOp_Works_For_Equal_References()
        {
            GraphId x = new GraphId();

            #pragma warning disable 1718
            Assert.False(x != x);
            #pragma warning restore 1718
        }

        [Test]
        public void GraphId_LogicalNotEqualsOp_Works()
        {
            GraphId lhs = new GraphId();
            GraphId rhs = new GraphId();

            Assert.True(lhs != rhs);
        }

        [Test]
        public void GraphId_LogicalEqualsOp_Fails_When_rhs_Set_To_Null()
        {
            GraphId lhs = new GraphId();
            GraphId rhs = null;

            Assert.False(lhs == rhs);
        }

        [Test]
        public void GraphId_LogicalEqualsOp_Fails_When_lhs_Set_To_Null()
        {
            GraphId lhs = null;
            GraphId rhs = new GraphId();

            Assert.False(lhs == rhs);
        }

        [Test]
        public void GraphId_LogicalNotEqualsOp_Fails_When_rhs_Set_To_Null()
        {
            GraphId lhs = new GraphId();
            GraphId rhs = null;

            Assert.True(lhs != rhs);
        }

        [Test]
        public void GraphId_LogicalNotEqualsOp_Fails_When_lhs_Set_To_Null()
        {
            GraphId lhs = null;
            GraphId rhs = new GraphId();

            Assert.True(lhs != rhs);
        }

        [Test]
        [TestCase("{909D5DAB-FE4F-4EB0-8A8B-2F07AD283D20}")]
        public void GraphId_Equals_Works_When_Value_Set_To(string expected)
        {
            var value = Guid.Parse(expected);
            var id = new GraphId(value);
            var other = new GraphId(value);

            Assert.True(id.Equals(other));
            Assert.That(id.Value, Is.EqualTo(other.Value));
            Assert.That(id.GetHashCode(), Is.EqualTo(other.GetHashCode()));
        }

        [Test]
        public void GraphId_Equals_Fails()
        {
            var id = new GraphId();
            var other = new GraphId();

            Assert.False(id.Equals(other));
        }

        [Test]
        public void GraphId_Equals_Fails_When_Passsing_Null_Object()
        {
            var id = new GraphId();
            object x = null;

            Assert.False(id.Equals(x));
        }

        [Test]
        public void GraphId_Equals_Fails_When_Passing_Null()
        {
            var id = new GraphId();

            Assert.False(id.Equals(null));
        }

        [Test]
        public void NodeId_New_Works()
        {
            var id = new NodeId();
            Assert.That(id.Value, Is.Not.EqualTo(0));
        }

        [Test]
        public void EdgeId_New_Works()
        {
            var id = new EdgeId();
            Assert.That(id.Value, Is.Not.EqualTo(0));
        }

        [Test]
        [TestCase(123456UL)]
        public void EdgeId_New_Works_For(ulong value)
        {
            var id = new EdgeId(value);
            Assert.That(id.Value, Is.EqualTo(value));
        }

        [Test]
        [TestCase(123456UL)]
        public void NodeId_New_Works_For(ulong value)
        {
            var id = new NodeId(value);
            Assert.That(id.Value, Is.EqualTo(value));
        }

        [Test]
        public void GraphId_Equals_Works_For_Same_Object()
        {
            var id = new GraphId();
            var idObj = (object)id;
            Assert.That(id.Equals(idObj), Is.EqualTo(true));
        }
    }
}
