using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public interface IJobService
    {
        void SubscribeToArtistJob(int artistApiId, int userId);
        void SubscribeToCityJob(int cityApiId, int userId);
        void SubscribeToArtistAndCityJob(int artistApiId, int cityApiId, int userId);
        void NotifyUsersAboutEventsJob();
    }
}
