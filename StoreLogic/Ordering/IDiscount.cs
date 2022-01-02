using StoreLogic.Products;
using StoreLogic.Users;

namespace StoreLogic.Ordering
{
    public interface IDiscount
    {
        Status TargetStatus { get; }

        ICategory TargetCategory { get; }

        double Rate { get; }
    }

    record Discount(Status TargetStatus, ICategory TargetCategory, double Rate) : IDiscount;
}
