using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Services.SubscriptionService;

namespace Core.Interfaces
{
    public interface ISubscriptionService
    {
        Task<int> AddEventsFromApi(int entityId, int totalCount, EntityFilter filter);
    }
}
