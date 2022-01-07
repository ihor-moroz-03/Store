using StoreLogic.Ordering;
using StoreLogic.Products;
using StoreLogic.Users;
using System;
using System.Collections.Generic;

using StoreDB;

namespace StoreLogic
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

        readonly Db _db;

        public Store()
        {
            _db = new("Data Source = StoreDB.db");
            _userFactory = new UserFactory(this);

            foreach (UserModel userModel in _db.GetUsers())
            {
                _userData.Add(_userFactory.CreateUser(userModel));
            }

            IAdmin admin = SignIn<IAdmin>("admin", "admin");
            GuestCustomer = _userFactory.CreateUser<ICustomer>("guest", "guest".GetDeterministicHashCode());
            admin.SetCustomerStatus(GuestCustomer, Status.Guest);
        }

        ICollection<IOrder> IStoreHandles.Orders => _orders;
        ICollection<IDiscount> IStoreHandles.Discounts => _discounts;
        IProductConstructionService IStoreHandles.ProductConstuctionService => _productConstuctionService;
        IStorage IStoreHandles.Storage => _storage;
        IUserData IStoreHandles.UserData => _userData;

        public T SignIn<T>(string username, string password) where T : class, IUser
        {
            if (_userData.TryGet(username, password.GetDeterministicHashCode(), out T user))
                return user;
            throw new ArgumentException("Incorrect username or password");
        }

        public ICustomer SignUp(string username, string password)
        {
            ICustomer customer = _userFactory.CreateUser<ICustomer>(username, password.GetDeterministicHashCode());
            _userData.Add(customer);
            return customer;
        }
    }
}
