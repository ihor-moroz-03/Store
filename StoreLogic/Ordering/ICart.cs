using StoreLogic.Products;
using System.Collections.Generic;

namespace StoreLogic.Ordering
{
    public interface ICart : IEnumerable<IProduct>
    {
        double OverallPrice { get; }

        void Take(IProduct product);

        void Return(IProduct product);

        void ReturnAll();
    }
}
