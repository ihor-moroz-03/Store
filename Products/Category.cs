using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Products
{
    class Category : ICategory
    {
        readonly HashSet<IDetailFormat> _formats;

        public Category(string name, IEnumerable<IDetailFormat> formats, ICategory parent)
        {
            Children = new HashSet<ICategory>();
            Name = name;
            _formats = new(formats);
            if (parent != null)
            {
                Parent = parent;
                (Parent.Children as ISet<ICategory>).Add(this);
            }
        }

        public string Name { get; set; }

        public ICategory Parent { get; }

        public IReadOnlySet<ICategory> Children { get; }

        public override bool Equals(object obj)
            => obj is Category && Name == (obj as Category).Name;

        public override int GetHashCode() => Name.GetHashCode();

        public IEnumerator<IDetailFormat> GetEnumerator()
        {
            if (Parent != null)
                foreach (IDetailFormat format in Parent)
                    yield return format;

            foreach (IDetailFormat format in _formats)
                yield return format;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
