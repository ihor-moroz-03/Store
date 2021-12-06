using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Products;

namespace Store.Users
{
    public interface IAdmin : IUser
    {
        IProductConstructionService ProductConstructinService { get; }

        IStorage Storage { get; }

        IUserData UserData { get; }

        IUserFactory UserFactory { get; }

        void SetCustomerStatus(ICustomer customer, Status newStatus)
            => (customer as Customer).Status = newStatus;
    }
}
