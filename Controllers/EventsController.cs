using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheMoonshineCafe.Data;
using TheMoonshineCafe.Models;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authorization;
using Event = Google.Apis.Calendar.v3.Data.Event;

namespace TheMoonshineCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly MoonshineCafeContext _context;
        string jsonFile = "GCalServiceAccountCredentials.json";
        string calendarId = @"schunicd@gmail.com";
        static string[] Scopes = { CalendarService.Scope.Calendar };
        private ServiceAccountCredential credential;
        public CalendarService service;

        public void SetService(ServiceAccountCredential cred)
        {
            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });
        }

        public EventsController(MoonshineCafeContext context)
        {
            _context = context;

            using (var stream =
                new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(confg.ClientEmail)
                   {
                       Scopes = Scopes
                   }.FromPrivateKey(confg.PrivateKey));
                SetService(credential);
            }
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        [HttpGet("Calendar")]
        public List<Event> GetCalendarEvents()
        {
            //Console.WriteLine("Your api function can be called");
            List<Event> calendarEvents = new List<Event>();

            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            foreach(Event e in events.Items)
            {
                calendarEvents.Add(e);
            }
            return calendarEvents;
        }

        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<Models.Event>>> GetUpcomingEvents()
        {
            List<Models.Event> events = await _context.Events.ToListAsync();
            List<Models.Event> upcoming = new List<Models.Event>();
            List<Models.Event> sortedUpcoming = new List<Models.Event>();
            DateTime now = DateTime.Now;

            foreach(Models.Event e in events)
            {
                if(e.eventStart.Date >= now.Date)
                {
                    upcoming.Add(e);
                }
            }

            sortedUpcoming = BubbleSort(upcoming, upcoming.Count);

            if (upcoming.Count > 4)
            {
                return sortedUpcoming.GetRange(0, 4);
            }
            else
            {
                return sortedUpcoming;
            }
        }

        private List<Models.Event> BubbleSort(List<Models.Event> toSort, int listLength)
        {
            Console.WriteLine("I've run" + listLength);
            if(listLength == 1)
            {
                return toSort;
            }

            List<Models.Event> sorted = new List<Models.Event>();
            for(int i = 0; i < listLength - 1; i++)
            {
                if(toSort[i].eventStart > toSort[i + 1].eventStart)
                {
                    Models.Event temp = toSort[i];
                    toSort[i] = toSort[i + 1];
                    toSort[i + 1] = temp;
                }
            }

            BubbleSort(toSort, listLength - 1);

            return toSort;
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Event>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }


        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Event>> PostEvent(Models.Event @event)
        {
            string bandLink = "";
            if (@event.bandLink != null)
            {
                bandLink = "<a href=\"" + @event.bandLink + "\"> "+ @event.bandName + "</a> \n";
            }
            //creating new event object based off of the Google API Event type
            Event newEvent = new Event() {
                //assigning values for events
                Summary = (@event.bandName + " " + @event.eventStart.ToShortTimeString() + "-" + @event.eventEnd.ToShortTimeString() + " $" + @event.ticketPrice).ToUpper(), 
                Location = "137 Kerr St., Oakville, Ontario L6Z 3A6",
                Description = bandLink + " " + @event.description,
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse(@event.eventStart.ToString())
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse(@event.eventEnd.ToString())
                },
            };

            //Building request to insert the new event in the primary (default) calendar
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            //Executes the request and assigns the response value to a variable to be used later
            
            try
            {
                Event createdEvent = request.Execute();
                @event.googleCalID = createdEvent.Id;
                _context.Events.Add(@event);
            }
            catch (Exception)
            {
                try
                {
                    service.Events.Update(newEvent, calendarId, newEvent.Id).Execute();
                    Console.WriteLine("Insert/Update new Event ");

                }
                catch (Exception)
                {
                    Console.WriteLine("can't Insert/Update new Event ");

                }
            }

            //Overwriting the default google Calendar ID that was assigned in admin-crud-event.components.ts (101,20)
            
            //Adding the new event with the official Google Calendar ID from GOOGLE to the Events
            
            //Save the new event
            await _context.SaveChangesAsync();

            //displaying the link to the created event for troubleshooting purposes
            //Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);
            //returns status code for event creation
            return CreatedAtAction("GetEvent", new { id = @event.id }, @event);
        }

        [HttpGet("EventsWithRes")]
        public async Task<ActionResult<IEnumerable<Models.Event>>> GetEventsWithReservations()
        {
            List<Models.Event> events = await _context.Events.ToListAsync();
            List<Models.Event> eventsWithRes = new List<Models.Event>();

            foreach(Models.Event e in events){
                if(e.currentNumberOfSeats > 0)
                {
                    eventsWithRes.Add(e);
                }
            }

            return eventsWithRes;
        }

/*
        // PUT: api/Events/5
        // POST: api/Events No Google API
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*        [HttpPost]
                public async Task<ActionResult<Models.Event>> PostEvent(Models.Event @event)
                {
                    _context.Events.Add(@event);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetEvent", new { id = @event.id }, @event);
                }*/

        [HttpPut("{id}")]
        //public async Task<ActionResult> PutEvent(Models.Event model)
        public async Task<ActionResult> PutEvent(Models.Event model)
        {
            if (!EventExists(model.googleCalID))
            {
                return NotFound();
            }

            Models.Event foundEvent = _context.Events.Where(e => e.googleCalID == model.googleCalID).FirstOrDefault();
            //var existedEvent = await _context.Set<Models.Event>().FirstOrDefaultAsync(i=> i.id==model.id);
            
            if (foundEvent == null)
            {
                return BadRequest();
            }

            string bandLink = "";
            if (model.bandLink != null)
            {
                bandLink = "<a href=\"" + model.bandLink + "\"> " + model.bandName + "</a> \n";
            }

            Console.WriteLine(model.eventStart.ToString());

            Event newEvent = new Event()
            {
                //assigning values for events

                Summary = (model.bandName + " " + model.eventStart.ToShortTimeString() + "-" + model.eventEnd.ToShortTimeString() + " $" + model.ticketPrice).ToUpper(),
                Location = "137 Kerr St., Oakville, Ontario L6Z 3A6",
                Description = bandLink + " " + model.description,
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse(model.eventStart.ToString())
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse(model.eventEnd.ToString())
                },
            };

            try
            {

                service.Events.Update(newEvent, calendarId, foundEvent.googleCalID).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete on Google Calendar");
                return BadRequest();
            }

            _context.Entry(foundEvent).CurrentValues.SetValues(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(model.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
       

        // DELETE: api/Events/5
/*        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        //DELETE: api/Events/calID
        [HttpDelete("{calID}")]
        public async Task<IActionResult> DeleteEventByCalID(String calID)
        {
            if (!EventExists(calID))
            {
                return NotFound();
            }

            Models.Event foundEvent = _context.Events.Where(e => e.googleCalID == calID).FirstOrDefault();
            var @event = await _context.Events.FindAsync(foundEvent.id);
            if (@event == null)
            {
                return NotFound();
            }

            try
            {
                service.Events.Delete(calendarId, calID).Execute();
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to Delete on Google Calendar");
                return BadRequest();
            }

            _context.Events.Remove(@event);

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("Failed to update database");
                return BadRequest();
            }
                      

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any((System.Linq.Expressions.Expression<Func<Models.Event, bool>>)(e => e.id == id));
        }

        private bool EventExists(string calID)
        {
            return _context.Events.Any((System.Linq.Expressions.Expression<Func<Models.Event, bool>>)(e => e.googleCalID == calID));
        }
    }
}
