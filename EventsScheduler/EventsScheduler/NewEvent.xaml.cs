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
        String name;
        DateTime begin;
        DateTime end;
        int freePlaces;
        Location location;
        List<User> participants;

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

        private void createButton_Click(object sender, RoutedEventArgs e)
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
                end = beginDatePicker.SelectedDate.Value;
                TimeSpan beginTime = new TimeSpan();
                TimeSpan endTime = new TimeSpan();
                if (TimeSpan.TryParseExact(beginTextBox.Text, @"hh\:mm", null, out beginTime) == false
                    || TimeSpan.TryParseExact(endTextBox.Text, @"hh\:mm", null, out endTime) == false)
                {
                    MessageBox.Show("Invalid time!\nTime format is HH:MM");
                }
                begin = begin.Add(beginTime);
                end = end.Add(endTime);
                if (int.TryParse(placesTextBox.Text, out freePlaces) == false)
                {
                    MessageBox.Show("Invalid number of participants!");
                }
                string locationAddress = locationComboBox.SelectedValue.ToString();

                location = new Location();
                using (var dataManager = new UnitOfWork(new AppDbContext()))
                {
                    location = dataManager.Locations.GetLocationByAddress(
                        locationAddress);
                }

                var result = App.Controller.CreateEvent(
                    name,
                    begin,
                    end,
                    freePlaces,
                    location,
                    App.Controller.CurrentUser,
                    null
                    );

                if (result)
                {
                    MessageBox.Show("Event created successfully!",
                        "Creation Completed!",
                        MessageBoxButton.OK);
                    Close();
                }
                else
                {
                    MessageBox.Show("Can not create event!",
                        "Creation Failed!",
                        MessageBoxButton.OK);
                }
            }
        }

        private void addParticipantsButton_Click(object sender, RoutedEventArgs e)
        {
            AddParticipants addParticipants = new AddParticipants();
            addParticipants.Owner = this;
            addParticipants.SelectedUsers = participants;
            bool? result = addParticipants.ShowDialog();
            if(result == true)
            {
                participants = addParticipants.SelectedUsers;
            }

        }
    }
}
