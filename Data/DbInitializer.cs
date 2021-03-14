using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMoonshineCafe.Models;

namespace TheMoonshineCafe.Data
{
    public class DbInitializer
    {
        public static void Initialize(MoonshineCafeContext context)
        {
            context.Database.EnsureCreated();

            if (context.Admins.Any())
            {
                Console.WriteLine("I'm here");
                return;
            }

            var bands = new Band[]
            {
                new Band{BandName="Test1", Website="testSite", BandInfo="They are a test"},
                new Band{BandName="Test2", Website="testSite2", BandInfo="They are a test2"},
                new Band{BandName="Test3", Website="testSite3", BandInfo="They are a test3"}
            };
            foreach (Band b in bands)
            {
                context.Bands.Add(b);
            }
            context.SaveChanges();

            var events = new Event[]
            {
                new Event{EventDate=DateTime.Parse("02/22/2021"), BandID=1, MaxNumberOfSeats = 23, CurrentNumberOfSeats=0, TicketPrice=3.50},
                new Event{EventDate=DateTime.Parse("03/04/2021"), BandID=2, MaxNumberOfSeats = 50, CurrentNumberOfSeats=15, TicketPrice=7.50},
                new Event{EventDate=DateTime.Parse("04/04/2021"), BandID=1, MaxNumberOfSeats = 100, CurrentNumberOfSeats=100, TicketPrice=10f}
            };
            foreach(Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();
            
        }
    }
}
