using System;
using System.Data;
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
using StoreLogic.Products;
using System.Collections.ObjectModel;

namespace StoreUI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        readonly IAdmin _admin;
        readonly IProductConstructionService _pcs;

        readonly DataGridIndexerAdapter _storageTable;
        readonly ObservableCollection<IUser> _customers;
        readonly ObservableCollection<IUser> _moderators;
        readonly ObservableCollection<IUser> _admins;

        IUser _selectedUser;

        public AdminWindow(IAdmin admin)
        {
            InitializeComponent();
            _admin = admin;
            _pcs = _admin.ProductConstructinService;

            _admin.Storage.Add(_pcs.BuildProduct(new HashSet<IDetail>
            {
                _pcs.BuildDetail(_pcs.Formats["Name"], "Milk"),
                _pcs.BuildDetail(_pcs.Formats["Price"], "1.5"),
                _pcs.BuildDetail(_pcs.Formats["Category"], "Good"),
                _pcs.BuildDetail(_pcs.Formats["ProductionDate"], "12/07/2021"),
                _pcs.BuildDetail(_pcs.Formats["ConsumeIn"], "10"),
            }));

            _storageTable = new("Name");
            foreach (IProduct product in _admin.Storage)
            {
                _storageTable.AddRow(new Dictionary<string, string>(
                    product.Select(d => KeyValuePair.Create(d.Name, d.Value))
                    ));
            }

            _admin.Storage.OnProductAddition += (sender, product) =>
            {
                _storageTable.AddRow(new Dictionary<string, string>(
                    product.Select(d => KeyValuePair.Create(d.Name, d.Value))
                    ));
            };

            _admin.Storage.OnProductRemoval += (sender, product) =>
            {
                _storageTable.RemoveRow(product.Name);
            };

            StorageDataGrid.ItemsSource = _storageTable.DataView;

            _customers = new(_admin.UserData.OfType<ICustomer>());
            _moderators = new(_admin.UserData.OfType<IModerator>());
            _admins = new(_admin.UserData.OfType<IAdmin>());
            _admin.UserData.OnAddition += (sender, user) =>
            {
                if (user is ICustomer) _customers.Add(user);
                if (user is IModerator) _moderators.Add(user);
                if (user is IAdmin) _admins.Add(user);
            };

            _admin.UserData.OnRemoval += (sender, user) =>
            {
                if (user is ICustomer) _customers.Remove(user);
                if (user is IModerator) _moderators.Remove(user);
                if (user is IAdmin) _admins.Remove(user);
            };

            CustomersDataGrid.ItemsSource = _customers;
            ModeratorsDataGrid.ItemsSource = _moderators;
            AdminsDataGrid.ItemsSource = _admins;

            CustomersDataGrid.SelectionChanged += (sender, e) => _selectedUser = CustomersDataGrid.SelectedItem as IUser;
            ModeratorsDataGrid.SelectionChanged += (sender, e) => _selectedUser = ModeratorsDataGrid.SelectedItem as IUser;
            AdminsDataGrid.SelectionChanged += (sender, e) => _selectedUser = AdminsDataGrid.SelectedItem as IUser;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            CreateProductWindow window = new(_pcs);
            window.OnProductCreation += (sender, product) =>
            {
                _admin.Storage.Add(product);

                StorageDataGrid.ItemsSource = null;
                StorageDataGrid.ItemsSource = _storageTable.DataView;
            };
            window.Show();
        }

        private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            string selected = (StorageDataGrid.SelectedItem as DataRowView)?[0].ToString();
            if (selected != null)
                _admin.Storage.Remove(_admin.Storage.First(p => p.Name == selected));
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow window = new(_admin.UserFactory);
            window.OnUserCreation += (sender, user) =>
            {
                try
                {
                    _admin.UserData.Add(user);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            window.Show();
        }

        private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUser != null)
                _admin.UserData.Remove(_selectedUser);
        }
    }
}
