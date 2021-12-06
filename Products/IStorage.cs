using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Products
{
    public interface IStorage : ICollection<IProduct>
    {
        event EventHandler<IProduct> OnProductAddition;

        event EventHandler<IProduct> OnProductRemoval;

        int UniqueCount { get; }
    }
}
