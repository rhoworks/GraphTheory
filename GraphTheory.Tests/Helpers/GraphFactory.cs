using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory.Tests.Helpers
{
    public class GraphFactory
    {
        public Graph<int> NewSimpleGraph()
        {
            var g = new Graph<int>();
            g.Insert(3, 4);
            g.Insert(1, 2).ConnectTo(3, 4);
            return g;
        }

        public Graph<int> NewPentagonGraph()
        {
            var g = new Graph<int>();
            g.Insert(1, 2, 3, 4, 5);
            g.Select(1).ConnectTo(2);
            g.Select(2).ConnectTo(3);
            g.Select(3).ConnectTo(4);
            g.Select(4).ConnectTo(5);
            g.Select(5).ConnectTo(1);
            return g;
        }

        public Graph<int> NewPentagonStarGraph()
        {
            var g = new Graph<int>();
            g.Insert(1, 2, 3, 4, 5, 6, 7, 8, 9, 0);
            g.Select(1).ConnectTo(2);
            g.Select(2).ConnectTo(3);
            g.Select(3).ConnectTo(4);
            g.Select(4).ConnectTo(5);
            g.Select(5).ConnectTo(1);
            g.Select(1).ConnectTo(6);
            g.Select(2).ConnectTo(7);
            g.Select(3).ConnectTo(8);
            g.Select(4).ConnectTo(9);
            g.Select(5).ConnectTo(0);
            return g;
        }
    }
}
