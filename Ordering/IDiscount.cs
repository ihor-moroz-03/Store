using Store.Products;
using Store.Users;

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
