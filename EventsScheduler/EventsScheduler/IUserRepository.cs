using EventsScheduler.Entities;

namespace EventsScheduler
{
    interface IUserRepository : IRepository<User>
    {
        User GetUserByLogin(string login);
        User GetUserAdmin();
        User GetUserGuest();
    }
}
