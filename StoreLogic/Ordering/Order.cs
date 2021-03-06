using StoreLogic.Products;
using StoreLogic.Users;
using System.Collections.Generic;
using System.Linq;

namespace StoreLogic.Ordering
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

        public string Process(IEnumerable<IDiscount> discounts)
        {
            discounts = discounts.Where(d => (int)d.TargetStatus <= (int)Placer.Status);
            double price = _cart.Sum(
                product => product.Price * discounts.Where(d => d.TargetCategory.Name.Equals(product.Category)).Max()?.Rate ?? 1
            );

            return $"Order processed with final price of {price}\nProducts:\n{string.Join('\n', _cart)}";
        }

        public IEnumerator<IProduct> GetEnumerator() => _cart.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
