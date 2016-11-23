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
    /// Interaction logic for NewLocation.xaml
    /// </summary>
    public partial class NewLocation : Window
    {
        public NewLocation()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(addressTextBox.Text == "")
            {
                MessageBox.Show("Please, input location address.");
            }
            else
            {
                var result = Controller.Instance.AddLocation(addressTextBox.Text);
                if (result)
                {
                    MessageBox.Show("Location added successfully!",
                        "Addition Completed!",
                        MessageBoxButton.OK);
                    Close();
                }
                else
                {
                    MessageBox.Show("Can not add location!",
                        "Addition Failed!",
                        MessageBoxButton.OK);
                }
            }

        }
    }
}
