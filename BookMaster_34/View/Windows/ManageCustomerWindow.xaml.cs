using BookMaster_34.AppData;
using BookMaster_34.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace BookMaster_34.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ManageCustomerWindow.xaml
    /// </summary>
    public partial class ManageCustomerWindow : Window
    {
        

        private List<City> _cities;

        public ManageCustomerWindow()
        {
            InitializeComponent();
            _cities = App.GetContext().Cities.ToList();

            LoadCities();
            Title = "Добаление читателя";
            Visibility= Visibility.Visible;
            EditBtn.Visibility= Visibility.Collapsed;
            CustomerIDTb.Text = GenerateId();
         
        }

        public ManageCustomerWindow(Customer selectedCustomer)
        {
            InitializeComponent();
            _cities = App.GetContext().Cities.ToList();
            LoadCities();
            Title = "Редактировать читателя";
            SaveBtn.Visibility = Visibility.Collapsed;
            EditBtn.Visibility = Visibility.Visible;
            CustomerIDTb.Text = selectedCustomer.Id;

            DataContext = selectedCustomer;
        }
      
        

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
           
          
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer();
        }

        private void AddCustomer()
        {
            try
            {
                // Проверяем заполнение всех полей
                if (string.IsNullOrWhiteSpace(ClientNameTb.Text) ||
              string.IsNullOrWhiteSpace(AddressClientTb.Text) ||
              string.IsNullOrWhiteSpace(EmailCustomerTb.Text) ||
               string.IsNullOrWhiteSpace(PhoneCustomerTb.Text))
                {
                    FeedbackServise.Warning("Заполните все поля!");
                }
                else
                {
                    // При заполнении всех полей реализуем добавление.
                    Customer newCustomer = new Customer()
                    {
                        Id = IDclientTb.Text,
                        Name = ClientNameTb.Text,
                        Address = AddressClientTb.Text,
                        CityId = (int)CityCmb.SelectedValue,
                        Phone = PhoneCustomerTb.Text,
                        Email = EmailCustomerTb.Text,
                        Zip = indexTb.Text
                    };
                    App.GetContext().Customers.Add(newCustomer);

                    App.GetContext().SaveChanges();
                    FeedbackServise.Information("Читатель успешно добавлен!");
                    DialogResult = true;
                }

            }
            catch (Exception exception) 
            {
                FeedbackServise.Error(exception);
            }
        }

        private void LoadCities()
        {
            CityCmb.ItemsSource = _cities;
        }
        private void EditBtnClick(object sender, EventArgs e)
        {
            EditBtnClick(sender, e);
            try
            {
                App.GetContext().SaveChanges();
                FeedbackServise.Information("Данные читателя успешно изменены!");
            }
            catch (Exception ex)
            {
                FeedbackServise.Error(ex);
            }
            
        }

        private string GenerateId()
        {
            int lastId = Convert.ToInt32(App.GetContext().Customers.Max(x => x.Id).Substring(1));
            //=> "C1015" => "1015"=>1015

            ++lastId;// =>1015 +1 +>1016
            return $"C{lastId}";//"C1016"
        }
        
    }
}
