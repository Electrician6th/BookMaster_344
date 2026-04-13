using BookMaster_34.AppData;
using BookMaster_34.Models;
using System.Windows;

namespace BookMaster_34.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                Administrator administrator = App.GetContext().Administrators.FirstOrDefault(administrator => administrator.Username == LoginTb.Text && administrator.Password == PasswordPb.Password);
                
                if (administrator != null)
                {
                    if (RememberMrCmb.IsChecked == true) CredentialsService.SaveCredentials(LoginTb.Text,PasswordPb.Password);

                    FeedbackServise.Information("Успешная авторизация.");

                  //DialogResult возвращает результат работы диалогового окна
                    DialogResult = true;
                }
                else
                {
                    FeedbackServise.Error("Пользователь не найден.");
                }

                CredentialsService.Administrator=administrator;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
         private bool Validate()
         {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                FeedbackServise.Warning("Введите логин.");
                LoginTb.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                FeedbackServise.Warning("Введите пароль.");
                PasswordPb.Focus();
                return false;
            }

            return true;
         }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CredentialsService.AutoLogin && !string.IsNullOrWhiteSpace(CredentialsService.SavedLogin))
            {
                LoginTb.Text = CredentialsService.SavedLogin;
                PasswordPb.Password = CredentialsService.SavedPassword;
                RememberMrCmb.IsChecked = CredentialsService.AutoLogin;
            }
        }
    }
}
