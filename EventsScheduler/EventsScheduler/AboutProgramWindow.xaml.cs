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
    /// Interaction logic for AboutProgramWindow.xaml
    /// </summary>
    public partial class AboutProgramWindow : Window
    {
        public AboutProgramWindow()
        {
            InitializeComponent();

            aboutListBox.Items.Clear();
            string aboutText = "How many times did you forget about " + "\n" + 
             "an important meeting, a special holiday, " + "\n" + 
             "a significant lecture, someone's birthday " + "\n" +
             "or other unbelievable event that is gonna " + "\n" +
             "to happen in your life soon? It may seem " + "\n" +
             "not such a global case but creating a " + "\n" +
             "small remainder in your calendar would be " + "\n" +
             "a good idea!!!";
            this.aboutListBox.Items.Add(aboutText);
        }
    }
}
