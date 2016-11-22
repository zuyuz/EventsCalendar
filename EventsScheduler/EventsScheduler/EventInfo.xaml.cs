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
        AppDbContext db = new AppDbContext();

        public EventInfo()
        {
            InitializeComponent();
            
        }

        public EventInfo(DateTime choosedDate)
        {
            InitializeComponent();

            var allEvents = db.Events.ToList();

            var currentEvent = allEvents.Find(e =>
            {
                return (e.StartTime.DayOfYear == choosedDate.DayOfYear);
            });

            if (currentEvent == null)
            {
                MessageBox.Show("No events for this day!");
                
            }
            else
            {
                CreatorLabel.Content = currentEvent.Creator;
                LocationTextBox.Text = currentEvent.EventLocation.Address;
                
                foreach(var i in currentEvent.Participants)
                {
                    ParticipantsListBox.Items.Add(i.Name);
                }

                DatePicker.DisplayDate = choosedDate;
            }
            

        }
    }
}