using EventsScheduler.DAL;
using EventsScheduler.DAL.Entities;
using System.Collections.Generic;
using System.Windows;

namespace EventsScheduler
{
    /// <summary>
    /// Interaction logic for MyEvents.xaml
    /// </summary>
    public partial class MyEvents : Window
    {
        List<Event> events;

		public MyEvents()
		{
			InitializeComponent();
			if (App.Controller.CurrentUser != null)
			{
				using (var uOW = new UnitOfWork(new AppDbContext()))
				{
                    events = new List<Event>(uOW.Events.GetCurrentUserEvents(App.Controller.CurrentUser));
                    foreach (var e in events)
                    {
                        listBoxEvents.Items.Add(e.Name);
                    }
                }
				
			}
		}

        private void buttonShowEvent_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxEvents.SelectedIndex != -1)
            {
                Event ev = events.Find(
                    i =>
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
                    i.Name == listBoxEvents.SelectedItem.ToString());
                if (ev != null)
                {
                    Controller.Instance.RemoveEvent(ev);
                    listBoxEvents.Items.RemoveAt(listBoxEvents.SelectedIndex);
                }
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonEditEvent_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxEvents.SelectedIndex != -1)
            {
                var temp = events.Find(i =>
                     i.Name == listBoxEvents.SelectedItem.ToString());
                EditEventWindow editEventWindow = new EditEventWindow(temp);
                editEventWindow.ShowDialog();
                if(editEventWindow.oldEvent.Name != editEventWindow.newEvent.Name)
                {
                    listBoxEvents.SelectedItem = editEventWindow.newEvent.Name;
                }
            }
        }
    }
}
