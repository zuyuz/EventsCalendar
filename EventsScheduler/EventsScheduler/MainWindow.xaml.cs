using EventsScheduler.Entities;
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

namespace EventsScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.GreetingLabel.Visibility = Visibility.Collapsed;
            this.ViewMyEventsItem.Visibility = Visibility.Collapsed;
            this.CreateEventItem.Visibility = Visibility.Collapsed;
            this.AddLocationItem.Visibility = Visibility.Collapsed;
            /*
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                var info = dataManager.Users.GetUserAdmin();
                txtTest.Text = info.Name;

                bool exist = dataManager.Users
                    .Find(u => u.Name == "Vasya")
                    .ToList().Count > 0;
                if (!exist)
                {
                    var newUser = new User()
                    {
                        Login = "pazan",
                        Password = "password",
                        Name = "Vasya",
                        UserRole = User.Role.User
                    };

                    dataManager.Users.Add(newUser);
                }

                dataManager.Complete();
            }
            */
        }

        private void SignInItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            loginWindow.ShowDialog();

            this.SignOutItem.Visibility = Visibility.Visible;
            this.SignInItem.Visibility = Visibility.Collapsed;
            this.SignUpItem.Visibility = Visibility.Collapsed;
            this.GreetingLabel.Content = "Hello user!";
            this.GreetingLabel.Visibility = Visibility.Visible;
            this.CreateEventItem.Visibility = Visibility.Visible;
            this.ViewMyEventsItem.Visibility = Visibility.Visible;
        }

        private void SignUpItem_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Owner = this;
            registerWindow.ShowDialog();
        }

        private void SignOutItem_Click(object sender, RoutedEventArgs e)
        {
            SignOutItem.Visibility = Visibility.Collapsed;
            SignInItem.Visibility = Visibility.Visible;
            SignUpItem.Visibility = Visibility.Visible;
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
