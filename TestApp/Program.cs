using Core.Jobs;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AddArtistEventsScheduler.Start(2332047);

            Console.ReadLine();
            Console.WriteLine("Done");
        }
    }
}
