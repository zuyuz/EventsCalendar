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
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class NewEvent : Window
    {
        public NewEvent()
        {
            InitializeComponent();
            datePicker.DisplayDateStart = datePicker.DisplayDate;
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = this.Owner as MainWindow;

            if(nameTextBox.Text == "")
            {
                MessageBox.Show("Please, input name.");
            }
            else if(datePicker.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Please, select date.");
            }
            else if(beginTextBox.Text == "" )
            {
                MessageBox.Show("Please, input begin time.");
            }
            else if(endTextBox.Text == "")
            {
                MessageBox.Show("Please, input end time.");
            }
            else if(placesTextBox.Text == "")
            {
                MessageBox.Show("Please, input number of places.");
            }
            else if(locationComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please, select location.");
            }
        }
    }
}
