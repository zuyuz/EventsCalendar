using EventsScheduler.DAL.Entities;
using EventsScheduler.DAL.Interfaces;
using System.Linq;

namespace EventsScheduler.DAL
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) 
            : base(context)
        {
        }

        public AppDbContext AppDbContext
        {
            get { return Context as AppDbContext; }
        }

        public User GetUserAdmin()
        {
            return AppDbContext.Users
                .First(u => u.UserRole == User.Role.Admin);
        }

        public User GetUserByLogin(string login)
        {
            var result = AppDbContext.Users.Where(u => u.Login == login).ToArray();
            return result.Length > 0 ? result[0] : null;
        }

        public User GetUserGuest()
        {
            return AppDbContext.Users.First(u => u.UserRole == User.Role.Guest);
        }
    }
}
