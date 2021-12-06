using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Products
{
    public interface IDetailFormat
    {
        string Name { get; }

        string Description { get; }
    }
}
