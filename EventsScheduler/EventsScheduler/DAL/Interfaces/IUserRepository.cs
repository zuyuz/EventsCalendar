using EventsScheduler.DAL.Entities;

namespace EventsScheduler.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByLogin(string login);
        User GetUserAdmin();
        User GetUserGuest();
    }
}
