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
    /// Interaction logic for MyEvents.xaml
    /// </summary>
    public partial class MyEvents : Window
    {
        List<Entities.Event> events;

		public MyEvents()
		{
			InitializeComponent();
			if (App.Controller.CurrentUser != null)
			{
				using (var uOW = new UnitOfWork(new AppDbContext()))
				{
                    events = new List<Entities.Event>(uOW.Events.GetCurrentUserEvents(App.Controller.CurrentUser));
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
                Entities.Event ev = events.Find(
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
                Entities.Event ev = events.Find(i =>
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
    }
}
