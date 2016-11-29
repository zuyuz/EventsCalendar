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
    /// Interaction logic for DayEvents.xaml
    /// </summary>
    public partial class DayEvents : Window
    {
        public DayEvents()
        {
            InitializeComponent();
        }

        List<Entities.Event> events;

        public DayEvents(List<Entities.Event> ev)
        {
            InitializeComponent();

            events = new List<Entities.Event>(ev);
            foreach(var e in events)
            {
                listBoxEvents.Items.Add(e.Name);
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonShowEvent_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxEvents.SelectedIndex != -1)
            {
                Entities.Event ev = events.Find(i =>
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
            //if (listBoxEvents.SelectedIndex != -1)
            //{
            //    Entities.Event ev = events.Find(i =>
            //    {
            //        if (i.Name == listBoxEvents.SelectedItem.ToString())
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    });
            //    if(ev != null)
            //    {
            //        Controller.Instance.RemoveEvent(ev);
            //        listBoxEvents.Items.RemoveAt(listBoxEvents.SelectedIndex);
            //    }
            //}
            
        }
    }
}
