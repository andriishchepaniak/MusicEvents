using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class MyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("My job");
            throw new NotImplementedException("Job ex");
        }
    }
}
