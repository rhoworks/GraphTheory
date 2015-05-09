using GraphTheory.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory.Tests
{
    [TestFixture]
    public class GraphTests
    {
        [Test]
        public void New_Works()
        {
            Assert.That(() => new Graph<int>(), Throws.Nothing);
        }

        [Test]
        [TestCase(1UL)]
        public void New_Works_When_Id_Set_To(ulong expected)
        {
            Assert.That(() => new Graph<int>(new GraphId(expected)), Throws.Nothing);
        }

        [Test]
        public void New_Works_When_Using_Custom_Comparer()
        {
            GraphId id = new GraphId();
            IEqualityComparer<int> comparer = new CustomIntEqualityComparer();
            Assert.That(() => new Graph<int>(id, comparer), Throws.Nothing);
        }

        [Test]
        public void Insert_ConnectTo_Works()
        {
            var g = new Graph<int>();
            g.Insert(3, 4);
            g.Insert(1, 2).ConnectTo(3, 4);

            var one = g.Select(1);
            var two = g.Select(2);
            var three = g.Select(3);
            var four = g.Select(4);

            Assert.That(g.SelectAdjacentTo(one.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(one.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(two.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(two.Id).Contains(four.Id), Is.True);
        }
    }
}
