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
                new Band{bandName="Test1", website="testSite", bandInfo="They are a test"},
                new Band{bandName="Test2", website="testSite2", bandInfo="They are a test2"},
                new Band{bandName="Test3", website="testSite3", bandInfo="They are a test3"}
            };
            foreach (Band b in bands)
            {
                context.Bands.Add(b);
            }
            context.SaveChanges();

            var events = new Event[]
            {
                new Event{eventDate=DateTime.Parse("02/22/2021 02:20"), bandId=3, maxNumberOfSeats = 23, currentNumberOfSeats=0, description="testing 1", ticketPrice=3.50},
                new Event{eventDate=DateTime.Parse("02/22/2021"), bandId=1, maxNumberOfSeats = 23, currentNumberOfSeats=0, ticketPrice=3.50},
                new Event{eventDate=DateTime.Parse("03/04/2021"), bandId=2, maxNumberOfSeats = 50, currentNumberOfSeats=15, ticketPrice=7.50},
                new Event{eventDate=DateTime.Parse("04/04/2021"), bandId=1, maxNumberOfSeats = 100, currentNumberOfSeats=100, ticketPrice=10f},
                new Event{eventDate=DateTime.Parse("05/20/2021 01:30"), bandId=3, maxNumberOfSeats = 100, currentNumberOfSeats=100, description="testing 5", ticketPrice=10f},
                new Event{eventDate=DateTime.Parse("05/20/2021 05:30"), bandId=1, maxNumberOfSeats = 20, currentNumberOfSeats=0, description="testing 6", ticketPrice=10f}
            };
            foreach(Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();
            
        }
    }
}
