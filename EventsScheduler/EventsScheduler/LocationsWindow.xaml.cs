using System;
using System.Windows;

namespace EventsScheduler
{
    /// <summary>
    /// Interaction logic for NewLocation.xaml
    /// </summary>
    public partial class LocationsWindow : Window
    {
        public LocationsWindow()
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
                    await Controller.Instance.AddLocationAsync(addressTextBox.Text);
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
