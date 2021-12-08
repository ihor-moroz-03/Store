using System.Collections;
using System.Collections.Generic;

namespace Store.Products
{
    class Category : ICategory
    {
        readonly HashSet<IDetailFormat> _formats;
        readonly ICategory _parent;

        public Category(string name, IEnumerable<IDetailFormat> formats, ICategory parent)
        {
            Name = name;
            _formats = new(formats);
            _parent = parent;
        }

        public string Name { get; set; }

        public override bool Equals(object obj)
            => obj is Category && Name == (obj as Category).Name;

        public override int GetHashCode() => Name.GetHashCode();

        public IEnumerator<IDetailFormat> GetEnumerator()
        {
            if (_parent != null)
                foreach (IDetailFormat format in _parent)
                    yield return format;

            foreach (IDetailFormat format in _formats)
                yield return format;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
