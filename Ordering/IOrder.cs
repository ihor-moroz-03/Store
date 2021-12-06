using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Products;
using Store.Users;

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
