using StoreLogic.Ordering;
using StoreLogic.Products;
using System.Collections.Generic;
using System.Linq;

namespace StoreLogic.Users
{
    class Customer : User, ICustomer
    {
        readonly IStorage _storage;
        readonly ICollection<IOrder> _orders;

        public Customer(string username, int passwordHash, Status status, IStorage storage, ICollection<IOrder> orders)
            : base(username, passwordHash)
        {
            Status = status;
            _orders = orders;
            _storage = storage;
            Cart = new Cart(_storage);
        }

        public Status Status { get; set; }
        public ICart Cart { get; private set; }
        public IEnumerable<IOrder> MyOrders => _orders.Where(order => order.Placer == this);

        public void CancelOrder(IOrder order)
        {
            order.Cancel();
            _orders.Remove(order);
        }

        public void PlaceOrder(bool isForDelivery, Address address, string paymentInfo)
        {
            _orders.Add(new Order(this, Cart, isForDelivery, address, paymentInfo));
            Cart = new Cart(_storage);
        }
    }
}
