using Store.Products;
using Store.Users;
using System.Collections.Generic;

namespace Store.Ordering
{
    public record Address(string Name, string Street, string CityDistrict, string Index, string Country, string PhoneNumber);

    public interface IOrder : IEnumerable<IProduct>
    {
        ICustomer Placer { get; }

        bool IsForDelivery { get; }

        Address Address { get; }

        string PaymentInfo { get; }

        string Process(IEnumerable<IDiscount> discounts);

        void Cancel();
    }
}
