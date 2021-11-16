using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public interface IJobService
    {
        void NotifyUsersAboutEventsJob();
        void NotifyUsersAboutEventsListJob();
    }
}
