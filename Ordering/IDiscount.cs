using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Users;
using Store.Products;

namespace Store.Ordering
{
    public interface IDiscount
    {
        Status TargetStatus { get; }

        ICategory TargetCategory { get; }

        double Rate { get; }
    }

    record Discount(Status TargetStatus, ICategory TargetCategory, double Rate) : IDiscount;
}
