using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory.Extensions
{
    /// <summary>
    /// Provides extension methods for the Guid type.
    /// </summary>
    public static class GuidExtensions
    {
        private static ulong prime = 16777619;

        /// <summary>
        /// Translates the current Guid to a ulong. 
        /// This will result in data loss due to the resolution difference between a Guid structure and a ulong structure.
        /// </summary>
        /// <param name="source">The current Guid object.</param>
        public static ulong ToUlong(this Guid source)
        {
            if (null == source)
                throw new ArgumentNullException(); // todo: specify parameter

            unchecked
            {
                ulong value = prime;

                foreach (byte b in source.ToByteArray())
                    value = value * prime + (ulong)b.GetHashCode();

                return value;
            }
        }
    }
}
