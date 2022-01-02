using StoreLogic.Products;

namespace StoreLogic.Users
{
    class Admin : User, IAdmin
    {
        public Admin(
            string username,
            int passwordHash,
            IProductConstructionService productConstructionService,
            IStorage storage,
            IUserData userData,
            IUserFactory userFactory)
            : base(username, passwordHash)
        {
            ProductConstructinService = productConstructionService;
            Storage = storage;
            UserData = userData;
            UserFactory = userFactory;
        }

        public IProductConstructionService ProductConstructinService { get; set; }

        public IStorage Storage { get; set; }

        public IUserData UserData { get; set; }

        public IUserFactory UserFactory { get; set; }

        public void SetCustomerStatus(ICustomer customer, Status newStatus)
            => (customer as Customer).Status = newStatus;
    }
}
