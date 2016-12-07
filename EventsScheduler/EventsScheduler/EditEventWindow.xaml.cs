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
    /// Interaction logic for EditEventWindow.xaml
    /// </summary>
    public partial class EditEventWindow : Window
    {
        public Entities.Event oldEvent { get; set; }
        public Entities.Event newEvent { get; set; }

        public EditEventWindow(Entities.Event choosedEvent)
        {
            InitializeComponent();

            DeleteParticipantButton.IsEnabled = false;

            DatePickerStart.SelectedDate = choosedEvent.StartTime;
            DatePickerEnd.SelectedDate = choosedEvent.EndTime;

            DatePickerStart.DisplayDate = choosedEvent.StartTime;
            DatePickerEnd.DisplayDate = choosedEvent.EndTime;

            beginTimePicker.Value = choosedEvent.StartTime;
            beginTimePicker.Text = beginTimePicker.Value.Value.ToShortTimeString();

            endTimePicker.Value = choosedEvent.EndTime;
            endTimePicker.Text = endTimePicker.Value.Value.ToShortTimeString();

            CreatorLabel.Content = choosedEvent.Creator.Name;

            locationComboBox.SelectedValue = choosedEvent.EventLocation.Address;

            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                foreach (var location in dataManager.Locations.GetAll())
                {
                    locationComboBox.Items.Add(location.Address);
                }
            }

            EventNameTextBox.Text = choosedEvent.Name;

            foreach (var i in choosedEvent.Participants)
            {
                ParticipantsListBox.Items.Add(i.Name);
            }

            if (ParticipantsListBox.Items.Count == 0)
            {
                DeleteParticipantButton.Visibility = Visibility.Collapsed;
            }

            oldEvent = new Entities.Event();

            oldEvent.Creator = choosedEvent.Creator;
            oldEvent.EventLocation = choosedEvent.EventLocation;
            oldEvent.FreePlaces = choosedEvent.FreePlaces;
            oldEvent.Id = choosedEvent.Id;
            oldEvent.StartTime = choosedEvent.StartTime;
            oldEvent.EndTime = choosedEvent.EndTime;
            oldEvent.Participants = choosedEvent.Participants;
            oldEvent.Name = choosedEvent.Name;

            newEvent = choosedEvent;
        }

        private void DeleteParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            var itemForDelete = ParticipantsListBox.SelectedItem;

            if (itemForDelete != null)
            {
                ParticipantsListBox.Items.Remove(itemForDelete);

                Entities.User participForDel = new Entities.User();

                foreach (var i in newEvent.Participants)
                {
                    if (i.Name == itemForDelete.ToString())
                    {
                        participForDel = i;
                        break;
                    }
                }


                newEvent.Participants.Remove(participForDel);
            }
            else
            {
                MessageBox.Show("No participants choosed");
            }
        }

        private void ParticipantsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteParticipantButton.IsEnabled = true;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (EventNameTextBox.Text == "")
            {
                MessageBox.Show("Please, input name.");
            }
            else if (DatePickerStart.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Please, select date.");
            }
            else if (DatePickerEnd.SelectedDate.HasValue == false)
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
            else if (locationComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please, select location.");
            }
            else
            {
                newEvent.Name = EventNameTextBox.Text;
                newEvent.EventLocation.Address = locationComboBox.SelectedValue.ToString();

                App.Controller.UpdateEvent(oldEvent, newEvent);
            }
        }
    }
}