using System;
using System.Collections.Generic;
using System.Windows;

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
        private List<User> participants = new List<User>();


        public List<User> Participants
        {
            get
            {
                return participants;
            }

            set
            {
                participants = value;
            }
        }

        public NewEvent(DateTime beginDate)
        {
            InitializeComponent();
            beginDatePicker.DisplayDateStart = beginDatePicker.DisplayDate;
            endDatePicker.DisplayDateStart = endDatePicker.DisplayDate;
            locationComboBox.Items.Clear();

            if (beginDate != new DateTime())
            {
                begin = beginDate;
                beginDatePicker.Text = begin.ToString();
            }

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
            else if (beginTimePicker.Value.HasValue == false)
            {
                MessageBox.Show("Please, input begin time.");
            }
            else if (endTimePicker.Value.HasValue == false)
            {
                MessageBox.Show("Please, input end time.");
            }
            else if (numberOfParticipantsTextBox.Text == "")
            {
                MessageBox.Show("Please, input number of places.");
            }
            else if (Convert.ToInt32(numberOfParticipantsTextBox.Text) < 1)
            {
                MessageBox.Show("That count of participants is not possible.");
            }
            else if (locationComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please, select location.");
            }
            else
            {
                name = nameTextBox.Text;

                if (begin == new DateTime())
                {
                    begin = beginDatePicker.SelectedDate.Value;
                }

                beginTime = beginTimePicker.Value.Value.ToShortTimeString();
                end = beginDatePicker.SelectedDate.Value;
                endTime = endTimePicker.Value.Value.ToShortTimeString();
                freePlaces = numberOfParticipantsTextBox.Text;
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
                catch (ArgumentException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Can not create event!",
                        MessageBoxButton.OK);
                }
            }
        }

        private void addParticipantsButton_Click(object sender, RoutedEventArgs e)
        {
            AddParticipants addParticipants = new AddParticipants();
            if (participants != null)
            {
                addParticipants.SelectedUsers = participants;
            }
            addParticipants.Owner = this;
            bool? result = addParticipants.ShowDialog();

            //if (result == true)
            //{
            //    participants = addParticipants.SelectedUsers;
            //}
        }
    }
}
