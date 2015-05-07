using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// Specifies that implementor must have an "Id" property of a parameterized type.
    /// </summary>
    /// <typeparam name="T">Type used for the "Id" property.</typeparam>
    public interface IIdentifiable<T>
        where T : Id
    {
        T Id { get; }
    }
}
