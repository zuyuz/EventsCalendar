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
            using (var db = new AppDbContext())
            {
                /*
                db.Users.Add(new User { Name = "Daun", UserRole = User.Role.Admin });
                for (int i = 0; i < 10; ++i)
                    db.Users.Add(new User
                    {
                        Name = $"user{i}",
                        UserRole = User.Role.User
                    });
                */

                db.SaveChanges();
                /*
                StringBuilder s = new StringBuilder();
                foreach (var item in db.Users)
                {
                    s.AppendLine($"{item.Id} {item.Name} {item.UserRole}");
                }
                txtTest.Text = s.ToString();
                */
            }
            

        }

        private void SignInItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            loginWindow.ShowDialog();

            if (DataManager.StaticLogin != null)
            {
                SignOutItem.Visibility = Visibility.Visible;
                SignInItem.Visibility = Visibility.Collapsed;
                SignUpItem.Visibility = Visibility.Collapsed;
            }
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
