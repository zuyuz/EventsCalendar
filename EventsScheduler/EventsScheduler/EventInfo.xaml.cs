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


        public EventInfo()
        {
            InitializeComponent();

        }

        public EventInfo(Entities.Event choosedEvent, DateTime choosedDate)
        {
            InitializeComponent();
            

            CreatorLabel.Content = choosedEvent.Creator;
            LocationTextBox.Text = choosedEvent.EventLocation.ToString();

            foreach (var i in choosedEvent.Participants)
            {
                ParticipantsListBox.Items.Add(i.Name);
            }

            DatePicker.DisplayDate = choosedDate;



        }
    }
}