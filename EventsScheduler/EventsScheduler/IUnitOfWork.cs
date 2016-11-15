using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsScheduler
{
    interface IUnitOfWork
    {
        IEventRepository Events { get; }
        ILocationRepository Locations { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
