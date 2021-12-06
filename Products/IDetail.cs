using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Products
{
    public interface IDetail
    {
        string Name { get; }
        string Value { get; set; }
        IDetailFormat Format { get; }
    }
}
