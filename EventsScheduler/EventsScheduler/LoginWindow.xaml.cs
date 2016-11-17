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

namespace EventsScheduler
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.Controller.SignIn(LoginTextBox.Text, PasswordBox.Password))
            {
                MessageBox.Show("Invalid login or password!");
            }
            else 
            {
                Close();
            }
        }

        private void BackItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
