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

        /// <summary>
        /// Identificates, authentificates and authorizates user 
        /// by login and password
        /// </summary>
        /// <param name="login">user's login</param>
        /// <param name="password">user's password</param>
        /// <returns>whether user is signed in</returns>
        public bool SignIn(string login, string password)
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
