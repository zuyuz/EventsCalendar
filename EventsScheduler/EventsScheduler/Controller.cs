using EventsScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsScheduler
{
    /// <summary>
    /// Represents control unit to manage business logic
    /// Singleton
    /// </summary>
    public class Controller
    {
        private static Controller instance;

        private User currentUser;

        private Controller()
        {
        }

        public User CurrentUser 
        {
            get
            {
                return this.currentUser;
            }
        }

        /// <summary>
        /// Identificates, authentificates and authorizates user 
        /// by login and password
        /// </summary>
        /// <param name="login">user's login</param>
        /// <param name="password">user's password</param>
        /// <returns>whether user is signed in</returns>
        public async Task<bool> SignInAsync(string login, string password)
        {
            return await Task.Run(() =>
            {
                using (var dataManager = new UnitOfWork(new AppDbContext()))
                {
                    User user = dataManager.Users.GetUserByLogin(login);
                    if (user != null) // identification
                    {
                        if (user.Password == password) // authentification
                        {
                            currentUser = user; // authorization
                            return true;
                        }
                    }
                }

                return false;
            });
        }

        public void SignOut()
        {
            this.currentUser = null;
        }

        public string GetCurrentUserLogin()
        {
            return this.currentUser.Login;
        }

		/// <summary>
		/// Performs registration and updates DB.
		/// </summary>
		/// <param name="email">User email</param>
		/// <param name="name">User full name</param>
		/// <param name="login">User login</param>
		/// <param name="password">User password</param>
		/// <returns>Whether registration completed successfully</returns>
		public async Task<bool> RegisterUserAsync(
			string email, 
			string name, 
			string login, 
			string password)
		{
            return await Task.Run(() =>
            {
                using (var dataManager = new UnitOfWork(new AppDbContext()))
                {
                    if (dataManager.Users.GetUserByLogin(login) == null)
                    {
                        User registrant = new User();
                        registrant.Login = login;
                        registrant.Name = name;
                        registrant.Password = password;
                        registrant.UserRole = User.Role.User;

                        dataManager.Users.Add(registrant);
                        //remove completion if necessary
                        dataManager.Complete();

                        return true;
                    }

                    return false;
                }
            });
		}

        public bool CreateEvent(
            string name,
            DateTime begin,
            DateTime end,
            int freePlaces,
            Location location,
            User creator,
            List<User> participants)
        {
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                if(dataManager.Events.GetEventsInSpecificPeriod(begin, end).Count() == 0)
                {
                    Event createdEvent = new Event();
                    createdEvent.Name = name;
                    createdEvent.StartTime = begin;
                    createdEvent.EndTime = end;
                    createdEvent.FreePlaces = freePlaces;
                    createdEvent.EventLocation = location;
                    createdEvent.Creator = creator;
                    createdEvent.Participants = participants;

                    dataManager.Events.Add(createdEvent);

                    dataManager.Complete();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AddLocation(string address)
        {
            using (var dataManager = new UnitOfWork(new AppDbContext()))
            {
                if (dataManager.Locations.GetLocationByAddress(address) == null)
                {
                    Location location = new Location();
                    location.Address = address;

                    dataManager.Locations.Add(location);

                    dataManager.Complete();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Getter for singleton instance of <c>Controller</c> type
        /// </summary>
        public static Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controller();
                }

                return instance;
            }
        }
    }
}
