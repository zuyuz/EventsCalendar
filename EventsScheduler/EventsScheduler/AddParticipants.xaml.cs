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

        public AddParticipants()
        {
            InitializeComponent();
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                foreach (var user in dataManager.Users.GetAll())
                {
                    if (user.UserRole == Entities.User.Role.User)
                    {
                        CheckBox item = new CheckBox();
                        item.Name = user.Login;
                        item.Content = $"{user.Name} ({user.Login})";
                        allUsersListBox.Items.Add(item);
                    }
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            List<User> selectedUsersLocal = new List<User>();
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                foreach (var user in allUsersListBox.SelectedItems)
                {
                    selectedUsersLocal.Add(dataManager.Users.GetUserByLogin((user as CheckBox).Name));
                }
            }
            foreach (var user in selectedUsersLocal)
            {
                CheckBox item = new CheckBox();
                item.Name = user.Login;
                item.Content = $"{user.Name} ({user.Login})";
                selectedUsersListBox.Items.Add(item);

                allUsersListBox.Items.Remove(item);
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            List<User> selectedUsersLocal = new List<User>();
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                foreach (var user in selectedUsersListBox.SelectedItems)
                {
                    selectedUsersLocal.Add(dataManager.Users.GetUserByLogin((user as CheckBox).Name));
                }
            }
            foreach (var user in selectedUsersLocal)
            {
                CheckBox item = new CheckBox();
                item.Name = user.Login;
                item.Content = $"{user.Name} ({user.Login})";
                selectedUsersListBox.Items.Remove(item);

                allUsersListBox.Items.Add(item);
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                foreach (var user in selectedUsersListBox.Items)
                {
                    selectedUsers.Add(dataManager.Users.GetUserByLogin((user as CheckBox).Name));
                }
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
