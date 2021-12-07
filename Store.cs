using Store.Ordering;
using Store.Products;
using Store.Users;
using System;
using System.Collections.Generic;

namespace Store
{
    public class Store : IStoreHandles
    {
        public readonly ICustomer GuestCustomer;

        readonly IProductConstructionService _productConstuctionService = new ProductConstructionService();
        readonly IStorage _storage = new Storage();
        readonly IUserData _userData = new UserData();
        readonly ICollection<IOrder> _orders = new List<IOrder>();
        readonly ICollection<IDiscount> _discounts = new List<IDiscount>();
        readonly IUserFactory _userFactory;

        public Store()
        {
            _userFactory = new UserFactory(this);
            IAdmin admin = _userFactory.CreateUser<IAdmin>("admin", "admin".GetHashCode());
            _userData.Add(admin);

            GuestCustomer = _userFactory.CreateUser<ICustomer>("guest", "guest".GetHashCode());
            admin.SetCustomerStatus(GuestCustomer, Status.Guest);
        }

        ICollection<IOrder> IStoreHandles.Orders => _orders;
        ICollection<IDiscount> IStoreHandles.Discounts => _discounts;
        IProductConstructionService IStoreHandles.ProductConstuctionService => _productConstuctionService;
        IStorage IStoreHandles.Storage => _storage;
        IUserData IStoreHandles.UserData => _userData;

        public T SignIn<T>(string username, string password) where T : class, IUser
        {
            if (_userData.TryGet(username, password.GetHashCode(), out T user))
                return user;
            throw new ArgumentException("Incorrect username or password");
        }

        public ICustomer SignUp(string username, string password)
        {
            ICustomer customer = _userFactory.CreateUser<ICustomer>(username, password.GetHashCode());
            _userData.Add(customer);
            return customer;
        }
    }
}
