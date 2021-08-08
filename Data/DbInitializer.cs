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
                new Event{title="event2", eventStart=DateTime.Parse("02/22/2022 02:20"), eventEnd=DateTime.Parse("02/22/2022 18:00"), bandId=3, maxNumberOfSeats = 23, currentNumberOfSeats=0, description="testing 1", ticketPrice=3.50},
                new Event{title="event1", eventStart=DateTime.Parse("02/22/2022"), eventEnd=DateTime.Parse("02/22/2022 02:00"), bandId=1, maxNumberOfSeats = 23, currentNumberOfSeats=0, ticketPrice=3.50},
                new Event{title="We're back", eventStart=DateTime.Parse("03/04/2021"), eventEnd=DateTime.Parse("03/04/2021 18:00"), bandId=2, maxNumberOfSeats = 50, currentNumberOfSeats=15, ticketPrice=7.50},
                new Event{title="We're back2", eventStart=DateTime.Parse("04/04/2021"), eventEnd=DateTime.Parse("04/04/2021 12:00"), bandId=1, maxNumberOfSeats = 100, currentNumberOfSeats=100, ticketPrice=10f},
                new Event{title="We're back3", eventStart=DateTime.Parse("05/20/2021 01:30"), eventEnd=DateTime.Parse("05/20/2021 03:30"), bandId=3, maxNumberOfSeats = 100, currentNumberOfSeats=100, description="testing 5", ticketPrice=10f},
                new Event{title="We're back4", eventStart=DateTime.Parse("05/20/2021 05:30"), eventEnd=DateTime.Parse("05/20/2021 10:30"), bandId=1, maxNumberOfSeats = 20, currentNumberOfSeats=0, description="testing 6", ticketPrice=10f}
            };
            foreach(Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();

            var admins = new Admin[]
            {
                new Admin{name="Derek", email="schunicd@gmail.com", phoneNumber="1234567890", accessLevel=1},
                new Admin{name="Harpreet", email="preet.ghuman911@gmail.com", phoneNumber="5551112318", accessLevel=1},
                new Admin{name="Mohammed", email="mohammed.a.r.musleh@gmail.com", phoneNumber="3321568974", accessLevel=1},
            };
            foreach (Admin a in admins)
            {
                context.Admins.Add(a);
            }
            context.SaveChanges();

        }
    }
}
