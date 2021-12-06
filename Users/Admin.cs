using Store.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Users
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
    }
}
