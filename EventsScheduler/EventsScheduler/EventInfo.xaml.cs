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
    /// Interaction logic for EventInfo.xaml
    /// </summary>
    public partial class EventInfo : Window
    {
        Entities.Event chEvent;

        public EventInfo()
        {
            InitializeComponent();

        }

        public EventInfo(Entities.Event choosedEvent)
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
            if(choosedEvent.EventLocation != null)
                LocationTextBox.Text = choosedEvent.EventLocation.Address.ToString();

            EventNameLabel.Content = choosedEvent.Name;

            foreach (var i in choosedEvent.Participants)
            {
                ParticipantsListBox.Items.Add(i.Name);
            }

            if (ParticipantsListBox.Items.Count == 0)
            {
                DeleteParticipantButton.Visibility = Visibility.Collapsed;
            }

            chEvent = choosedEvent;
        }

        private void DeleteParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            var itemForDelete = ParticipantsListBox.SelectedItem;

            if (itemForDelete != null)
            {
                ParticipantsListBox.Items.Remove(itemForDelete);

                Entities.User participForDel = new Entities.User();

                foreach (var i in chEvent.Participants)
                {
                    if (i.Name == itemForDelete.ToString())
                    {
                        participForDel = i;
                        break;
                    }
                }


                chEvent.Participants.Remove(participForDel);
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
    }
}