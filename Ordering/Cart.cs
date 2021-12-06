using Store.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Ordering
{
    class Cart : ICart
    {
        readonly IStorage _storage;
        readonly List<IProduct> _contents = new();

        public Cart(IStorage storage)
        {
            _storage = storage;
        }

        public double OverallPrice => _contents.Sum(product => product.Price);

        public void ReturnAll()
        {
            foreach (IProduct product in _contents)
                Return(product);
        }

        public void Return(IProduct product)
        {
            if (_contents.Contains(product))
            {
                _storage.Add(product);
                _contents.Remove(product);
            }
        }

        public void Take(IProduct product)
        {
            if (_storage.Contains(product))
            {
                _contents.Add(product);
                _storage.Remove(product);
            }
        }

        public IEnumerator<IProduct> GetEnumerator() => _contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
