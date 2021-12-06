using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Products
{
    class Storage : IStorage
    {
        readonly Dictionary<IProduct, int> _products = new();
        public int Count => _products.Sum(pair => pair.Value);
        public int UniqueCount => _products.Count;
        public bool IsReadOnly => false;

        public event EventHandler<IProduct> OnProductAddition;

        public event EventHandler<IProduct> OnProductRemoval;

        public void Add(IProduct item)
        {
            if (_products.ContainsKey(item)) _products[item]++;
            else _products[item] = 1;
            OnProductAddition?.Invoke(this, item);
        }

        public void Clear()
        {
            foreach (IProduct product in this)
                Remove(product);
        }

        public bool Contains(IProduct item) => _products.ContainsKey(item);

        public void CopyTo(IProduct[] array, int arrayIndex)
        {
            IProduct[] source = this.ToArray();
            Array.Copy(source, 0, array, arrayIndex, source.Length);
        }

        public bool Remove(IProduct item)
        {
            if (!_products.ContainsKey(item) || _products[item] == 0) return false;

            if (_products[item] == 1) _products.Remove(item);
            else _products[item]--;

            OnProductRemoval?.Invoke(this, item);
            return true;
        }

        public IEnumerator<IProduct> GetEnumerator()
        {
            foreach (var pair in _products)
                for (int i = 0; i < pair.Value; ++i)
                    yield return pair.Key;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
