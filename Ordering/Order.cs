using Store.Products;
using Store.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Ordering
{
    class Order : IOrder
    {
        readonly ICart _cart;

        public Order(ICustomer placer, ICart cart, bool isForDelivery, Address address, string paymentInfo)
        {
            _cart = cart; Placer = placer; IsForDelivery = isForDelivery; Address = address; PaymentInfo = paymentInfo;
        }

        public ICustomer Placer { get; }

        public bool IsForDelivery { get; }

        public Address Address { get; }

        public string PaymentInfo { get; }

        public void Cancel() => _cart.ReturnAll();

        public string Process()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IProduct> GetEnumerator() => _cart.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
