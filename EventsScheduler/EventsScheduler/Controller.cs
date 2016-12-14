using EventsScheduler.DAL;
using EventsScheduler.DAL.Entities;
using EventsScheduler.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Func<IUnitOfWork> NewUnitOfWork { get; set; }

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
                using (var dataManager = NewUnitOfWork())
                {
                    User user = dataManager.Users.GetUserByLogin(login);
                    if (user != null) // identification
                    {
                        if (user.Password == password) // authentification
                        {
                            currentUser = new User()
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Login = user.Login,
                                Password = user.Password,
                                UserRole = user.UserRole,
                                CreatedEvents = null,
                                Events = null
                            }; // authorization
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
                using (var dataManager = NewUnitOfWork())
                {
                    if (dataManager.Users.GetUserByLogin(login) == null)
                    {
                        User registrant = new User();
                        registrant.Login = login;
                        registrant.Name = name;
                        registrant.Password = password;
                        registrant.UserRole = User.Role.User;

                        dataManager.Users.Add(registrant);
                        dataManager.Complete();

                        return true;
                    }

                    return false;
                }
            });
        }

        public void CreateEvent(
            string name,
            DateTime begin,
            string beginTimeStr,
            DateTime end,
            string endTimeStr,
            string freePlacesStr,
            string locationStr,
            string creatorLogin,
            List<User> participants)
        {
            using (var dataManager = NewUnitOfWork())
            {
                TimeSpan beginTime = new TimeSpan();
                TimeSpan endTime = new TimeSpan();
                if (TimeSpan.TryParseExact(beginTimeStr, @"hh\:mm", null, out beginTime) == false
                || TimeSpan.TryParseExact(endTimeStr, @"hh\:mm", null, out endTime) == false)
                {
                    throw new ArgumentException("Invalid time!\nTime format is HH:MM");
                }
                begin = begin.Add(beginTime);
                end = end.Add(endTime);
                if (begin > end)
                {
                    throw new ArgumentException("Invalid period of time");
                }

                int freePlaces;
                if (int.TryParse(freePlacesStr, out freePlaces) == false)
                {
                    throw new ArgumentException("Invalid number of participants!");
                }

                if (dataManager.Events.GetEventsInSpecificPeriod(begin, end).Count() > 0)
                {
                    throw new ArgumentException("Event in such period already exists.");
                }

                Location location = dataManager.Locations.GetLocationByAddress(locationStr);

                List<User> participantsContext = new List<User>();
                foreach (var user in participants)
                {
                    participantsContext.Add(dataManager.Users.GetUserByLogin(user.Login));
                }

                Event createdEvent = new Event()
                {
                    Name = name,
                    StartTime = begin,
                    EndTime = end,
                    FreePlaces = freePlaces,
                    EventLocation = location,
                    Creator = dataManager.Users.GetUserByLogin(creatorLogin),
                    Participants = participantsContext
                };
                dataManager.Events.Add(createdEvent);

                dataManager.Complete();
            }
        }

        public async Task AddLocationAsync(string address)
        {
            await Task.Run(() =>
            {
                using (var dataManager = NewUnitOfWork())
                {
                    if (dataManager.Locations.GetLocationByAddress(address) == null)
                    {
                        Location location = new Location();
                        location.Address = address;

                        dataManager.Locations.Add(location);

                        dataManager.Complete();
                    }
                    else
                    {
                        throw new ArgumentException("Location with such address already exists.");
                    }
                }
            });
        }

        public void UpdateEvent(Event oldEvent, Event newEvent)
        {
            using (var dataManager = NewUnitOfWork())
            {
                var ev = dataManager.Events.GetEventsWithSpecificName(oldEvent.Name).First();
                ev.EndTime = newEvent.EndTime;
                ev.EventLocation = newEvent.EventLocation;
                ev.FreePlaces = newEvent.FreePlaces;
                ev.Name = newEvent.Name;
                ev.Participants = new List<User>(newEvent.Participants);
                ev.StartTime = newEvent.StartTime;

                dataManager.Complete();
            }
        }

        public void RemoveEvent(Event eventToRemove)
        {
            using (var dataManager = NewUnitOfWork())
            {
                var events = dataManager.Events.GetAll();

                //for(int i=1; i<events.Count(); i++)
                //{
                //    string name = dataManager.Events.Get(i).Name;
                //    if (name == eventToRemove.Name)
                //    {
                //        dataManager.Events.Remove(dataManager.Events.Get(i));
                //        dataManager.Complete();
                //        break;
                //    }
                //}
                foreach (var ev in events)
                {
                    if (ev.Name == eventToRemove.Name)
                    {
                        dataManager.Events.Remove(ev);
                        dataManager.Complete();
                        break;
                    }
                }

            }
        }

        public void RemoveLocation(string locationAddress)
        {
            using (var dataManager = NewUnitOfWork())
            {
                var locationToRemove = dataManager.Locations.GetLocationByAddress(locationAddress);
                if (locationToRemove != null)
                {
                    dataManager.Locations.Remove(locationToRemove);
                    dataManager.Complete();
                }
                else
                {
                    throw new ArgumentException("Invalid address");
                }

                //foreach (var ev in events)
                //{
                //    if (ev.Name == eventToRemove.Name)
                //    {
                //        dataManager.Events.Remove(ev);
                //        dataManager.Complete();
                //        break;
                //    }
                //}
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
                    instance.NewUnitOfWork = () => new UnitOfWork(new AppDbContext());
                }

                return instance;
            }
        }        
    }
}
