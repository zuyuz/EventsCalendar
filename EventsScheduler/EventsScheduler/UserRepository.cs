using EventsScheduler.Entities;
using System.Linq;

namespace EventsScheduler
{
    class UserRepository : Repository<User>, IUserRepository
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
            return AppDbContext.Users.First(u => u.Login == login);
        }

        public User GetUserGuest()
        {
            return AppDbContext.Users
                .First(u => u.UserRole == User.Role.Guest);
        }
    }
}
