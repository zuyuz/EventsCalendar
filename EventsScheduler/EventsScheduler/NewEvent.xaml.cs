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

using EventsScheduler.Entities;

namespace EventsScheduler
{
    /// <summary>
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class NewEvent : Window
    {
        private string name;
        private DateTime begin;
        private string beginTime;
        private DateTime end;
        private string endTime;
        private string freePlaces;
        private string locationAddress;
        private List<User> participants;
        public NewEvent()
        {
            InitializeComponent();
            beginDatePicker.DisplayDateStart = beginDatePicker.DisplayDate;
            endDatePicker.DisplayDateStart = endDatePicker.DisplayDate;
            locationComboBox.Items.Clear();
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                foreach (var location in dataManager.Locations.GetAll())
                {
                    locationComboBox.Items.Add(location.Address);
                }
            }

        }

		private  void createButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = this.Owner as MainWindow;

			if (nameTextBox.Text == "")
			{
				MessageBox.Show("Please, input name.");
			}
			else if (beginDatePicker.SelectedDate.HasValue == false)
			{
				MessageBox.Show("Please, select date.");
			}
			else if (beginTextBox.Text == "")
			{
				MessageBox.Show("Please, input begin time.");
			}
			else if (endTextBox.Text == "")
			{
				MessageBox.Show("Please, input end time.");
			}
			else if (placesTextBox.Text == "")
			{
				MessageBox.Show("Please, input number of places.");
			}
			else if (locationComboBox.SelectedValue == null)
			{
				MessageBox.Show("Please, select location.");
			}
			else
			{
				name = nameTextBox.Text;
				begin = beginDatePicker.SelectedDate.Value;
                beginTime = beginTextBox.Text;
				end = beginDatePicker.SelectedDate.Value;
				endTime = endTextBox.Text;
                freePlaces = placesTextBox.Text;
				locationAddress = locationComboBox.SelectedValue.ToString();

                try
                {
                    Controller.Instance.CreateEvent(
                        name,
                        begin,
                        beginTime,
                        end,
                        endTime,
                        freePlaces,
                        locationAddress,
                        App.Controller.CurrentUser.Login,
                        participants);

                    Close();
                }
                catch(ArgumentException ex)
				{
					MessageBox.Show("Can not create event!",
						ex.Message,
						MessageBoxButton.OK);
				}
			}
		}

		private void addParticipantsButton_Click(object sender, RoutedEventArgs e)
        {
            AddParticipants addParticipants = new AddParticipants();
            addParticipants.Owner = this;
            addParticipants.ShowDialog();
        }

        private void CloseItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
