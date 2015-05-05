using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory.Extensions
{
    public static class GuidExtensions
    {
        private static ulong prime = 16777619;

        public static ulong ToUlong(this Guid source)
        {
            if (null == source)
                throw new ArgumentNullException(); // todo: specify parameter

            unchecked
            {
                ulong value = prime;

                foreach (byte b in source.ToByteArray())
                    value ^= prime * (ulong)b.GetHashCode();

                return value;
            }
        }
    }
}
