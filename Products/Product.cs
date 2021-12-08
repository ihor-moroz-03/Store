using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Store.Products
{
    class Product : IProduct, IEquatable<Product>
    {
        readonly Dictionary<string, IDetail> _details;

        public Product(ISet<IDetail> details)
        {
            _details = details.ToDictionary(detail => detail.Name);
        }

        string this[string detail]
        {
            get => _details[detail].Value;
            set => _details[detail].Value = value;
        }

        public string Name { get => this["Name"]; set => this["Name"] = value; }

        public double Price
        {
            get => double.Parse(this["Price"]);
            set => this["Price"] = value.ToString();
        }

        public string Category { get => this["Category"]; set => this["Category"] = value; }

        public bool HasDetail(string detail) => _details.ContainsKey(detail);

        public bool TryGetDetail(string detail, out string value)
        {
            try
            {
                value = this[detail];
                return true;
            }
            catch (Exception)
            {
                value = "";
                return false;
            }
        }

        public override bool Equals(object obj) => obj is Product && Equals(obj as Product);

        public bool Equals(IProduct other) => Equals(other as object);

        public bool Equals(Product other)
        {
            foreach (string detail in _details.Keys)
                if (!other.HasDetail(detail) || this[detail] != other[detail])
                    return false;
            return true;
        }

        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() => string.Join(", ", _details).ToString();

        public IEnumerator<IDetail> GetEnumerator() => _details.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _details.Values.GetEnumerator();
    }
}
