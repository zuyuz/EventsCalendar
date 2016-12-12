using EventsScheduler.DAL;
using EventsScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

            allUsersListBox.Items.Clear();

            selectedUsers.Add(App.Controller.CurrentUser);

            CheckBox currentUser = new CheckBox();
            currentUser.Name = App.Controller.CurrentUser.Login;
            currentUser.Content = String.Format("{0} ({1})", App.Controller.CurrentUser.Login,
                App.Controller.CurrentUser.Login);
            selectedUsersListBox.Items.Add(currentUser);

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
                        item.Content = String.Format("{0} ({1})", user.Name, user.Login);
                        allUsersListBox.Items.Add(item);
                    }
                    else if (selectedUsers.Contains(user))
                    {
                        CheckBox item = new CheckBox();
                        item.Name = user.Login;
                        item.Content = String.Format("{0} ({1})", user.Name, user.Login);
                        selectedUsersListBox.Items.Add(item);
                    }
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var owner = this.Owner as NewEvent;

            for (var i = 0; i < allUsersListBox.Items.Count; i++)
            {
                var check = allUsersListBox.Items[i] as CheckBox;

                if (check.IsChecked == true)
                {
                    var newItem = new CheckBox()
                    {
                        Name = check.Name,
                        Content = check.Content
                    };

                    if (selectedUsersListBox.Items.Count + 1 <=
                        Convert.ToInt32(owner.numberOfParticipantsTextBox.Text))
                    {
                        selectedUsersListBox.Items.Add(newItem);
                        allUsersListBox.Items.Remove(check);
                        i--;
                    }
                    else
                    {
                        MessageBox.Show(
                        "Can not add so many participants! Please change the number " +
                        "of people or remove ones!",
                        "Too many participants!",
                        MessageBoxButton.OK);
                    }
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
                        Content = check.Content
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

            if (selectedUsers.Count == 0)
            {
                MessageBox.Show(
                        "You have to add at least one participant to the event.",
                        "No participants!",
                        MessageBoxButton.OK);
            }

            var owner = this.Owner as NewEvent;

            owner.Participants = selectedUsers;

            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}