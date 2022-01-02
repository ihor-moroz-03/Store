using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using StoreLogic.Users;

namespace StoreUI
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        readonly IUserFactory _factory;

        public CreateUserWindow(IUserFactory factory)
        {
            InitializeComponent();
            _factory = factory;
        }

        public event EventHandler<IUser> OnUserCreation;

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            IUser user = CreateUser();

            OnUserCreation?.Invoke(this, user);
        }

        private IUser CreateUser() => UserComboBox.SelectedItem.ToString().Split(' ')[1] switch
        {
            "Admin" => _factory.CreateUser<IAdmin>(UsernameTextBox.Text, PasswordBox.Password.GetHashCode()),
            "Customer" => _factory.CreateUser<ICustomer>(UsernameTextBox.Text, PasswordBox.Password.GetHashCode()),
            "Moderator" => _factory.CreateUser<IModerator>(UsernameTextBox.Text, PasswordBox.Password.GetHashCode()),
            _ => throw new NotSupportedException(),
        };
    }
}
