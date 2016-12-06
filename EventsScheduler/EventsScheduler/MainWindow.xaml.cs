using System;
using System.Windows;

namespace EventsScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ViewMyEventsItem.Visibility = Visibility.Collapsed;
            this.CreateEventItem.Visibility = Visibility.Collapsed;
            this.AddLocationItem.Visibility = Visibility.Collapsed;
        }

        private void SignInItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            loginWindow.ShowDialog();

            if (App.Controller.CurrentUser != null)
            {
                this.SignOutItem.Visibility = Visibility.Visible;
                this.SignInItem.Visibility = Visibility.Collapsed;
                this.SignUpItem.Visibility = Visibility.Collapsed;
                this.GreetingLabel.Content = "Hello " + App.Controller.GetCurrentUserLogin() + "!";
                this.CreateEventItem.Visibility = Visibility.Visible;
                this.ViewMyEventsItem.Visibility = Visibility.Visible;

				if (App.Controller.CurrentUser.Login == "admin")
				{
					this.AddLocationItem.Visibility = Visibility.Visible;
				}
			}
        }

        private void SignUpItem_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Owner = this;
            registerWindow.ShowDialog();
        }

        private void SignOutItem_Click(object sender, RoutedEventArgs e)
        {
            this.SignOutItem.Visibility = Visibility.Collapsed;
            this.SignInItem.Visibility = Visibility.Visible;
            this.SignUpItem.Visibility = Visibility.Visible;
            this.GreetingLabel.Content = "";
            App.Controller.SignOut();
            this.ViewMyEventsItem.Visibility = Visibility.Collapsed;
            this.CreateEventItem.Visibility = Visibility.Collapsed;
            this.AddLocationItem.Visibility = Visibility.Collapsed;
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ShowCalendarItem_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow cw = new CalendarWindow();
            cw.ShowDialog();
        }

        private void CreateEventItem_Click(object sender, RoutedEventArgs e)
        {
            DateTime begin = new DateTime();
            NewEvent newEvent = new NewEvent(begin);
            newEvent.ShowDialog();
        }

        private void AddLocationItem_Click(object sender, RoutedEventArgs e)
        {
            LocationsWindow location = new LocationsWindow();
            location.ShowDialog();
        }

		private void ViewMyEventsItem_Click(object sender, RoutedEventArgs e)
		{
			MyEvents myEvents = new MyEvents();
			myEvents.ShowDialog();
		}
	}
}
