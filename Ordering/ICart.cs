using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Products;

namespace Store.Ordering
{
    public interface ICart : IEnumerable<IProduct>
    {
        double OverallPrice { get; }

        void Take(IProduct product);

        void Return(IProduct product);

        void ReturnAll();
    }
}
