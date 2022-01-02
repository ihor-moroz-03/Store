using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StoreLogic.Products
{
    class ProductConstructionService : IProductConstructionService
    {
        readonly Dictionary<string, IDetailFormat> _formats;
        readonly Dictionary<string, ICategory> _categories;

        public ProductConstructionService()
        {
            _formats = new()
            {
                ["Name"] = new DetailFormat("Name", null, "name of product"),
                ["Price"] = new DetailFormat("Price", s => double.TryParse(s, out double d) && d > 0, "price in floating point format"),
                ["Category"] = new DetailFormat("Category", val => _categories.ContainsKey(val))
            };

            _categories = new()
            {
                ["Product"] = new Category("Product", new[] { _formats["Name"], _formats["Price"], _formats["Category"] }, null)
            };
        }

        public IDictionary<string, IDetailFormat> Formats => _formats;

        public IDictionary<string, ICategory> Categories => _categories;

        public bool ParseFormat(string line)
        {
            IDetailFormat result = DetailFormat.Parse(line);
            return AddWithNameCheck(_formats, result.Name, result);
        }

        public bool NewFormat(string name, DValidator validator) =>
            AddWithNameCheck(_formats, name, new DetailFormat(name, validator));

        public bool NewFormat(string name, DValidator validator, string description) =>
            AddWithNameCheck(_formats, name, new DetailFormat(name, validator, description));

        public bool NewCategory(string name, IEnumerable<IDetailFormat> formats, ICategory parent) =>
            AddWithNameCheck(_categories, name, new Category(name, formats, parent));

        public bool ParseCategory(string line)
        {
            Match m = Regex.Match(line, @"(\w+)(?: \((\w+)\))?: (.+)");

            return NewCategory(
                m.Groups[1].Value,
                m.Groups[3]
                    .Value
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => _formats[s]),
                _categories[m.Groups[2].Success ? m.Groups[2].Value : "Product"]
                );
        }

        static bool AddWithNameCheck<T>(IDictionary<string, T> target, string name, T item)
        {
            if (target.ContainsKey(name)) return false;
            target[name] = item;
            return true;
        }

        public void LoadFormatsFromFile(string path)
        {
            foreach (string line in System.IO.File.ReadAllLines(path))
                ParseFormat(line);
        }

        public void LoadCategoriesFromFile(string path)
        {
            foreach (string line in System.IO.File.ReadAllLines(path))
                ParseCategory(line);
        }

        public IDetail BuildDetail(IDetailFormat format, string value) =>
            new Detail(format as DetailFormat, value);

        public IProduct BuildProduct(ISet<IDetail> details) => new Product(details);
    }
}
