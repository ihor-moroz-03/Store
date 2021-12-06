using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Products
{
    public interface ICategory : IEnumerable<IDetailFormat>
    {
        string Name { get; }

        IReadOnlySet<ICategory> Children { get; }
    }
}
