using EventsScheduler.Entities;

namespace EventsScheduler
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByLogin(string login);
        User GetUserAdmin();
        User GetUserGuest();
    }
}
