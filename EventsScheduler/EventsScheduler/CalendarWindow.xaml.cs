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
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {
        public CalendarWindow()
        {
            InitializeComponent();
        }

        private void Calendar_SelectedDatesChanged(object sender,
            SelectionChangedEventArgs e)
        {
            // ... Get reference.
            var calendar = sender as Calendar;

            // ... See if a date is selected.
            if (calendar.SelectedDate.HasValue)
            {
                AppDbContext db = new AppDbContext();
                UnitOfWork uOW = new UnitOfWork(db);

                var dayEvent = uOW.Events.GetEventsInSpecificPeriod(calendar.SelectedDate.Value, calendar.SelectedDate.Value.AddDays(1));

                if (dayEvent.Count() == 0)
                {
                    MessageBox.Show("No events for this day!");
                }
                else
                {
                    EventInfo eventWindow = new EventInfo(dayEvent.Last());
                    eventWindow.ShowDialog();
                }
                uOW.Dispose();

            }
        }
    }
}
