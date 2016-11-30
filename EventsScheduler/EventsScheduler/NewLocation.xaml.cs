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
				var result = await Controller.Instance.AddLocationAsync(addressTextBox.Text);
				if (result)
				{
                    locationsListBox.Items.Add(addressTextBox.Text);
				}
				else
				{
					MessageBox.Show("Can not add location!",
						"Addition Failed!",
						MessageBoxButton.OK);
				}
			}

		}

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                if (locationsListBox.SelectedValue.ToString() != "")
                {
                    Entities.Location location = dataManager.Locations.
                    GetLocationByAddress(locationsListBox.SelectedValue.ToString());
                    dataManager.Locations.Remove(location);
                    locationsListBox.Items.Remove(location.Address);
                }
            }

        }
    }
}
