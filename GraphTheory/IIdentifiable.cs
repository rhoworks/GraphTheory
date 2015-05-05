using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    public interface IIdentifiable<T>
        where T : Id
    {
        T Id { get; }
    }
}
