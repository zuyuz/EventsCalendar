using EventsScheduler.DAL.Entities;
using System.Collections.Generic;
using System.Windows;

namespace EventsScheduler
{
    /// <summary>
    /// Interaction logic for DayEvents.xaml
    /// </summary>
    public partial class DayEvents : Window
    {
        public DayEvents()
        {
            InitializeComponent();
        }

        List<Event> events;

        public DayEvents(List<DAL.Entities.Event> ev)
        {
            InitializeComponent();

            events = new List<Event>(ev);
            foreach (var e in events)
            {
                listBoxEvents.Items.Add(e.Name);
            }
            //this.eventsLabel.Content += " on " + ev[0].StartTime.Date.ToShortDateString() + ":";
        }

        private void CloseItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonShowEvent_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxEvents.SelectedIndex != -1)
            {
                Event ev = events.Find(i =>
                {
                    if (i.Name == listBoxEvents.SelectedItem.ToString())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

                EventInfo eInfoWindow = new EventInfo(ev);
                eInfoWindow.ShowDialog();
            }
        }

        private void buttonDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxEvents.SelectedIndex != -1)
            {
                Event ev = events.Find(i =>
                {
                    if (i.Name == listBoxEvents.SelectedItem.ToString())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
                if (ev != null)
                {
                    if (Controller.Instance.CurrentUser != null &&
                        (Controller.Instance.CurrentUser == ev.Creator ||
                        Controller.Instance.CurrentUser.UserRole == User.Role.Admin))
                    {
                        Controller.Instance.RemoveEvent(ev);
                        listBoxEvents.Items.RemoveAt(listBoxEvents.SelectedIndex);
                    }
                    else
                    {
                        MessageBox.Show("Only event creator or admin are allowed to delete this event.");
                    }
                }
            }
        }
        
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

