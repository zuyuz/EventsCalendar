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

            EventNameLabel.Content = choosedEvent.Name;
            DatePickerStart.SelectedDate = choosedEvent.StartTime;
            DatePickerEnd.SelectedDate = choosedEvent.EndTime;

            startTimeTextBox.Text = choosedEvent.StartTime.TimeOfDay.ToString();
            endTimeTextBox.Text = choosedEvent.EndTime.TimeOfDay.ToString();

            CreatorLabel.Content = choosedEvent.Creator.Name;
            if(choosedEvent.EventLocation != null)
                LocationTextBox.Text = choosedEvent.EventLocation.Address.ToString();

            foreach (var i in choosedEvent.Participants)
            {
                ParticipantsListBox.Items.Add(i.Name);
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

        private void CloseItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}