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
using System.Windows.Navigation;
using System.Windows.Shapes;

using StoreLogic.Users;

namespace StoreUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly StoreLogic.Store _store = new();

        public MainWindow()
        {
            InitializeComponent();

            IAdmin admin = _store.SignIn<IAdmin>("admin", "admin");
            admin.ProductConstructinService.LoadFormatsFromFile(@"detailFormats.txt");
            admin.ProductConstructinService.LoadCategoriesFromFile(@"categories.txt");
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (UserComboBox.SelectedItem.ToString().Split(' ')[1])
                {
                    case "Admin":
                        new AdminWindow(_store.SignIn<IAdmin>(UsernameTextBox.Text, PasswordBox.Password)).Show();
                        break;

                    case "Customer":
                        throw new NotImplementedException();

                    case "Moderator":
                        throw new NotImplementedException();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NotImplementedException)
            {
                MessageBox.Show("Coming soon...");
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _store.SignUp(UsernameTextBox.Text, PasswordBox.Password);
                //new customer window would open here as well...
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GuestSignIn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming soon...");
        }
    }
}
