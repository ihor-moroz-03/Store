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

using StoreLogic.Products;

namespace StoreUI
{
    /// <summary>
    /// Interaction logic for CreateProductWindow.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {
        readonly IProductConstructionService _pcs;
        readonly Dictionary<IDetailFormat, TextBox> _details = new();

        public CreateProductWindow(IProductConstructionService pcs)
        {
            InitializeComponent();
            _pcs = pcs;

            CategoryComboBox.ItemsSource = _pcs.Categories.Select(pair => pair.Key);
        }

        public event EventHandler<IProduct> OnProductCreation;

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _details.Clear();
            FormStackPanel.Children.Clear();

            foreach (IDetailFormat format in _pcs.Categories[CategoryComboBox.SelectedItem.ToString()])
            {
                TextBox textBox = new();
                textBox.Margin = new Thickness(15, 5, 15, 10);
                textBox.Padding = new Thickness(5);

                _details[format] = textBox;

                Label label = new();
                label.Content = format.Name + (format.Description == "" ? "" : $" ({format.Description})");
                label.Margin = new Thickness(15, 10, 15, 0);
                label.Padding = new Thickness(5);

                FormStackPanel.Children.Add(label);
                FormStackPanel.Children.Add(_details[format]);
            }

            _details[_pcs.Formats["Category"]].Text = CategoryComboBox.SelectedItem.ToString();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IProduct product = _pcs.BuildProduct(
                    new HashSet<IDetail>(_details.Select(pair => _pcs.BuildDetail(pair.Key, pair.Value.Text)))
                    );

                OnProductCreation?.Invoke(this, product);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
