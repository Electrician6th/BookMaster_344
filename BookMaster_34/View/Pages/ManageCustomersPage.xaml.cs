using BookMaster_34.Models;
using BookMaster_34.View.Windows;
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

namespace BookMaster_34.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManageCustomersPage.xaml
    /// </summary>
    public partial class ManageCustomersPage : Page
    {
       private List<Customer> _customers;
        private Customer _selectedCustomer;
    
        public ManageCustomersPage()
        {
            InitializeComponent();

            _customers = App.GetContext().Customers.ToList();
        }
        private void LoadData(List<Customer> customersList)
        {
            CustomerLv.ItemsSource = customersList;
        }
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerLv.Visibility =Visibility.Visible;

            string CustomerId = CustomerIDTb.Text;
            string customerName = NameTb.Text;

            if (string.IsNullOrWhiteSpace(CustomerId) && string.IsNullOrWhiteSpace(customerName))
            {
                LoadData(_customers);
            }
            else
            {
                List<Customer> filterCustomers = _customers.Where(customers => customers.Id.Contains(CustomerId, StringComparison.OrdinalIgnoreCase)&& customers.Name.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToList();

                LoadData(filterCustomers);
            }

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerWindow manageCustomerWindow = new ManageCustomerWindow();
            {
                CustomerLv.ItemsSource =_customers= App.GetContext().Customers.ToList();
            }



        }
    }
    
}
