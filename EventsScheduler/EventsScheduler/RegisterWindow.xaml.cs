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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = this.Owner as MainWindow;

            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Please, input your name.");
            }
            else if (EmailTextBox.Text == "")
            {
                MessageBox.Show("Please, input your email.");
            }
            else if (LoginTextBox.Text == "")
            {
                MessageBox.Show("Please, input your login.");
            }
            else if (PasswordBox.Password == "")
            {
                MessageBox.Show("Please, input your password.");
            }
            else
            {
                var result = Controller.Instance.RegisterUser(
                EmailTextBox.Text,
                NameTextBox.Text,
                LoginTextBox.Text,
                PasswordBox.Password);

                if (result)
                {
                    MessageBox.Show(
                        "Registered successfully!",
                        "Registration Completed",
                        MessageBoxButton.OK);
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Login is already registered!",
                        "Registration Failed",
                        MessageBoxButton.OK);
                }
            }
        }

        private void BackItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
