using EventsScheduler.Entities;
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
    /// Interaction logic for AddParticipants.xaml
    /// </summary>
    public partial class AddParticipants : Window
    {
        List<User> selectedUsers = new List<User>();

		public List<User> SelectedUsers
		{
			get
			{
				return selectedUsers;
			}
			set
			{
				selectedUsers = value;
			}
		}

		public AddParticipants()
        {
            InitializeComponent();

            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                foreach (var user in dataManager.Users.GetAll())
                {
                    if (user.UserRole == User.Role.User 
                        && !selectedUsers.Contains(user)
						&& user.Login != Controller.Instance.CurrentUser.Login)
                    {
                        CheckBox item = new CheckBox();
                        item.Name = user.Login;
                        item.Content = String.Format("{0} {1}", user.Name, user.Login);
                        allUsersListBox.Items.Add(item);
                    }
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
			for (var i = 0; i < allUsersListBox.Items.Count; i++)
			{
				var check = allUsersListBox.Items[i] as CheckBox;

				if (check.IsChecked == true)
				{
					var newItem = new CheckBox()
					{
						Name = check.Name,
						Content = String.Format("{0} {1}", check.Content, check.Name)
					};
					selectedUsersListBox.Items.Add(newItem);
					allUsersListBox.Items.Remove(check);
					i--;
				}
			}
		}

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
			for (var i = 0; i < selectedUsersListBox.Items.Count; i++)
			{
				var check = selectedUsersListBox.Items[i] as CheckBox;

				if (check.IsChecked == true)
				{
					var newItem = new CheckBox()
					{
						Name = check.Name,
                        Content = String.Format("{0} {1}", check.Content, check.Name)
					};
					allUsersListBox.Items.Add(newItem);
					selectedUsersListBox.Items.Remove(check);
					i--;
				}
			}
		}

		private void confirmButton_Click(object sender, RoutedEventArgs e)
		{
			using (var dataManager = new UnitOfWork(new AppDbContext()))
			{
				foreach (var user in selectedUsersListBox.Items)
				{
					selectedUsers.Add(
						dataManager.Users.GetUserByLogin((user as CheckBox).Name));
				}
			}
		}

		private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}