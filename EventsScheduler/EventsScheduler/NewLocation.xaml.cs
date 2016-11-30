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
    public partial class Locations : Window
    {
        public Locations()
        {
            InitializeComponent();
            locationsListBox.Items.Clear();
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                foreach (var location in dataManager.Locations.GetAll())
                {
                    locationsListBox.Items.Add(location.Address);
                }
            }
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (addressTextBox.Text == "")
            {
                MessageBox.Show("Please, input location address.");
            }
            else
            {
                try
                {
                    Controller.Instance.AddLocation(addressTextBox.Text);
                    locationsListBox.Items.Add(addressTextBox.Text);
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show(ex.Message,
                        "Can not add location!",
                        MessageBoxButton.OK);
                    addressTextBox.Clear();
                }
            }

        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            string locationAddress = locationsListBox.SelectedValue.ToString();
            if (locationAddress != "")
            {
                try
                {
                    Controller.Instance.RemoveLocation(locationAddress);
                    locationsListBox.Items.Remove(locationAddress);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message,
                        "Can not remove location!",
                        MessageBoxButton.OK);
                }
            }
        }
    }
}
