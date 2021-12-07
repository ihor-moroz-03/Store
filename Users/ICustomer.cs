using Store.Ordering;
using System.Collections.Generic;

namespace Store.Users
{
    public enum Status { Guest, Free, Premium }

    public interface ICustomer : IUser
    {
        Status Status { get; }
        ICart Cart { get; }
        IEnumerable<IOrder> MyOrders { get; }

        void PlaceOrder(bool isForDelivery, Address address, string paymentInfo);

        void CancelOrder(IOrder order);
    }
}
