using StoreLogic.Products;

namespace StoreLogic.Users
{
    public interface IAdmin : IUser
    {
        IProductConstructionService ProductConstructinService { get; }

        IStorage Storage { get; }

        IUserData UserData { get; }

        IUserFactory UserFactory { get; }

        void SetCustomerStatus(ICustomer customer, Status newStatus);
    }
}
