using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Products
{
    public interface IProductConstructionService
    {
        IDictionary<string, IDetailFormat> Formats { get; }

        IDictionary<string, ICategory> Categories { get; }

        bool ParseFormat(string line);

        bool NewFormat(string name, DValidator validator);

        bool NewFormat(string name, DValidator validator, string description);

        void LoadFormatsFromFile(string path);

        bool ParseCategory(string line);

        bool NewCategory(string name, IEnumerable<IDetailFormat> formats, ICategory parent);

        void LoadCategoriesFromFile(string path);

        IDetail BuildDetail(IDetailFormat format, string value);

        IProduct BuildProduct(ISet<IDetail> details);
    }
}
