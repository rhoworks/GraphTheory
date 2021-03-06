﻿using GraphTheory.Tests.Helpers;
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
        public void New_Creates_Random_GraphId()
        {
            var g = new Graph<int>();

            Assert.That(g.Id, Is.Not.Null);
        }

        [Test]
        public void Insert_Works()
        {
            var g = new Graph<int>();
            g.Insert(1, 2, 3, 4, 5, 6, 7, 8);

            Assert.That(g.Nodes.Count(), Is.EqualTo(8));
        }

        [Test]
        public void Insert_Works_When_There_Are_Duplicate_Values()
        {
            var g = new Graph<int>();
            g.Insert(1);
            g.Insert(1);
            g.Insert(1);

            Assert.That(g.Nodes.Count(), Is.EqualTo(3));
        }

        [Test]
        public void SelectAll_Works_When_There_Are_Duplicate_Values()
        {
            var g = new Graph<int>();
            g.Insert(1, 1, 1);

            var values = g.SelectAll(1);
            Assert.That(values.Count(), Is.EqualTo(3));
        }

        [Test]
        public void SelectAll_Works_When_There_Are_Duplicate_Values_And_Multiple_Nodes_Are_Specified()
        {
            var g = new Graph<int>();
            g.Insert(1, 1, 1);
            g.Insert(2, 2, 2);
            g.Insert(3, 4, 5);

            var values = g.SelectAll(1, 2).ToList();
            Assert.That(values.Count(), Is.EqualTo(6));
            Assert.That(values.Any(p => p.Value == 1), Is.True);
            Assert.That(values.Any(p => p.Value == 2), Is.True);
        }

        [Test]
        public void Select_Works()
        {
            var g = new Graph<int>();

            g.Insert(1, 2);

            var one = g.Select(1);
            var two = g.Select(2);

            Assert.That(g.Nodes.Count(), Is.EqualTo(2));
            Assert.That(g.Edges.Count(), Is.EqualTo(0));
            Assert.That(one.Value, Is.EqualTo(1));
            Assert.That(two.Value, Is.EqualTo(2));
        }

        [Test]
        public void Select_Works_For_Multiple_Nodes()
        {
            var g = new Graph<int>();
            g.Insert(1, 2, 3);

            var multiple = g.Select(1, 2, 3).ToList();

            Assert.That(multiple.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Select_Works_For_Mulitple_Node_Ids()
        {
            var g = new GraphFactory().NewPentagonStarGraph();

            var nodes = g.Select(1, 2, 3);
            var nodesById = g.Select(nodes.Select(p => p.Id).ToArray());

            Assert.That(nodesById.Count(), Is.EqualTo(nodes.Count()));
        }

        [Test]
        public void Remove_Works()
        {
            var g = new Graph<int>();
            g.Insert(1, 2, 3);

            var nodes = g.Select(1, 2);
            
            g.Remove(nodes.Select(p => p.Id).ToArray());

            Assert.That(g.Nodes.Count(), Is.EqualTo(1));
            Assert.That(g.Nodes.First().Value, Is.EqualTo(3));
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

            Assert.That(g.Nodes.Count(), Is.EqualTo(4));
            Assert.That(g.Edges.Count(), Is.EqualTo(4));
            Assert.That(g.SelectAdjacentTo(one.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(one.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(two.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(two.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(one.Id).Contains(two.Id), Is.False);
            Assert.That(g.SelectAdjacentTo(three.Id).Contains(four.Id), Is.False);
        }

        [Test]
        public void New_Works_For_Weighted_Graph()
        {
            Assert.That(() => new Graph<int, float>(), Throws.Nothing);
        }

        [Test]
        public void SelectConnectedTo_Works_For_Edges()
        {
            var g = new Graph<int>();
            g.Insert(1, 2);
            g.Select(1).ConnectTo(2);

            var one = g.Select(1);
            var two = g.Select(2);

            var edges = g.SelectConnectedTo(one.Id);
            var edge = edges.First();

            var nodes = g.SelectConnectedTo(edge);

            Assert.That(nodes.Contains(two.Id), Is.True);
        }

        [Test]
        public void Remove_Works_For_Multiple_Edges()
        {
            var g = new GraphFactory().NewPentagonStarGraph();
            var one = g.Select(1);

            var edges = g.SelectConnectedTo(one.Id);

            g.Remove(edges.ToArray());

            Assert.That(g.Edges.Count(), Is.EqualTo(7));
            Assert.That(g.Nodes.Count(), Is.EqualTo(10));
        }

        [Test]
        public void Remove_Works_For_Node_With_Multiple_Edges()
        {
            var g = new GraphFactory().NewPentagonStarGraph();
            var one = g.Select(1);

            g.Remove(one.Id);

            Assert.That(g.Nodes.Count(), Is.EqualTo(9));
            Assert.That(g.Edges.Count(), Is.EqualTo(7));
        }

        [Test]
        public void Select_Works_For_Multiple_Ids()
        {
            var g = new GraphFactory().NewPentagonStarGraph();
            var one = g.Select(1);
            var edgeIds = g.SelectConnectedTo(one.Id);
            var edges = g.Select(edgeIds.ToArray());

            Assert.That(edges.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Can_Easily_Create_PentagonStarGraph_With_Fluent_API()
        {
            var g = new Graph<int>();
            g.Insert(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            g.Select(0).ConnectTo(5).ThenTo(1).ThenTo(2).ThenTo(3).ThenTo(4).ThenTo(5);
            g.Select(1).ConnectTo(6);
            g.Select(2).ConnectTo(7);
            g.Select(3).ConnectTo(8);
            g.Select(4).ConnectTo(9);

            var zero = g.Select(0);
            var one = g.Select(1);
            var two = g.Select(2);
            var three = g.Select(3);
            var four = g.Select(4);
            var five = g.Select(5);
            var six = g.Select(6);
            var seven = g.Select(7);
            var eight = g.Select(8);
            var nine = g.Select(9);

            Assert.That(g.Nodes.Count(), Is.EqualTo(10));
            Assert.That(g.Edges.Count(), Is.EqualTo(10));

            Assert.That(g.SelectAdjacentTo(one.Id).Contains(two.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(one.Id).Contains(five.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(one.Id).Contains(six.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(two.Id).Contains(one.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(two.Id).Contains(seven.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(two.Id).Contains(three.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(three.Id).Contains(two.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(three.Id).Contains(eight.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(three.Id).Contains(four.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(four.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(four.Id).Contains(nine.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(four.Id).Contains(five.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(five.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(five.Id).Contains(zero.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(five.Id).Contains(one.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(six.Id).Contains(one.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(seven.Id).Contains(two.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(eight.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(nine.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(zero.Id).Contains(five.Id), Is.True);
        }

        [Test]
        public void Can_Easily_Create_Interconnected_Graph_With_Fluent_API()
        {
            var g = new Graph<int>();
            g.Insert(1, 2, 3, 4, 5, 6, 7);
            g.Select(1, 2, 3).ConnectTo(4, 5, 6).ThenTo(7);

            var one = g.Select(1);
            var two = g.Select(2);
            var three = g.Select(3);
            var four = g.Select(4);
            var five = g.Select(5);
            var six = g.Select(6);
            var seven = g.Select(7);

            Assert.That(g.Nodes.Count(), Is.EqualTo(7));
            Assert.That(g.Edges.Count(), Is.EqualTo(12));

            Assert.That(g.SelectAdjacentTo(one.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(one.Id).Contains(five.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(one.Id).Contains(six.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(two.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(two.Id).Contains(five.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(two.Id).Contains(six.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(three.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(three.Id).Contains(five.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(three.Id).Contains(six.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(four.Id).Contains(one.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(four.Id).Contains(two.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(four.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(four.Id).Contains(seven.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(five.Id).Contains(one.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(five.Id).Contains(two.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(five.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(five.Id).Contains(seven.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(six.Id).Contains(one.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(six.Id).Contains(two.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(six.Id).Contains(three.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(six.Id).Contains(seven.Id), Is.True);

            Assert.That(g.SelectAdjacentTo(seven.Id).Contains(four.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(seven.Id).Contains(five.Id), Is.True);
            Assert.That(g.SelectAdjacentTo(seven.Id).Contains(six.Id), Is.True);
        }
    }
}
