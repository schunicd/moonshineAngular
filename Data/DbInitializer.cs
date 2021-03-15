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

            if (context.Events.Any())
            {
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
                new Event{EventDate=DateTime.Parse("02/22/2021 02:20"), BandID=3, MaxNumberOfSeats = 23, CurrentNumberOfSeats=0, Description="testing 1", TicketPrice=3.50},
                new Event{EventDate=DateTime.Parse("02/22/2021"), BandID=1, MaxNumberOfSeats = 23, CurrentNumberOfSeats=0, Description="testing 2", TicketPrice=3.50},
                new Event{EventDate=DateTime.Parse("03/04/2021"), BandID=2, MaxNumberOfSeats = 50, CurrentNumberOfSeats=15, Description="testing 3", TicketPrice=7.50},
                new Event{EventDate=DateTime.Parse("04/04/2021"), BandID=1, MaxNumberOfSeats = 100, CurrentNumberOfSeats=100, Description="testing 4", TicketPrice=10f},
                new Event{EventDate=DateTime.Parse("20/05/2021 01:30"), BandID=3, MaxNumberOfSeats = 100, CurrentNumberOfSeats=100, Description="testing 5", TicketPrice=10f},
                new Event{EventDate=DateTime.Parse("20/05/2021 05:30"), BandID=1, MaxNumberOfSeats = 20, CurrentNumberOfSeats=0, Description="testing 6", TicketPrice=10f}
            };
            foreach(Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();
            
        }
    }
}
