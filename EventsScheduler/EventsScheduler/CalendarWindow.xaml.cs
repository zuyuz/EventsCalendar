using EventsScheduler.DAL;
using EventsScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                using (var uOW = new UnitOfWork(new AppDbContext()))
                {
                    var dayEvent = uOW.Events.GetEventsInSpecificPeriod(
                        calendar.SelectedDate.Value, calendar.SelectedDate.Value.AddDays(1));

                    if (dayEvent.Count() == 0)
                    {
                        if (Controller.Instance.CurrentUser != null)
                        {
                            var result = MessageBox.Show("No events for this day. Would you like to create new one?", "Free day", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                NewEvent newEventWindow = new NewEvent(Convert.ToDateTime(calendar.SelectedDate));
                                newEventWindow.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        List<Event> daysEvents = dayEvent.ToList();
                        DayEvents eventWindow = new DayEvents(daysEvents);
                        eventWindow.ShowDialog();
                    }
                        uOW.Dispose();
                }
            }
        }
    }
}
