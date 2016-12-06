using System.Windows;

namespace EventsScheduler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Controller Controller = Controller.Instance;
    }
}
