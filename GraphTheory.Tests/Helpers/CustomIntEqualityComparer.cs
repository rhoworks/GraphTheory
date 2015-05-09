using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory.Tests.Helpers
{
    public class CustomIntEqualityComparer : EqualityComparer<int>
    {
        public override bool Equals(int x, int y)
        {
            return x == y;
        }

        public override int GetHashCode(int obj)
        {
            return obj.GetHashCode();
        }
    }
}
