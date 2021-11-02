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

namespace TheMoonshineCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly MoonshineCafeContext _context;
        public EventsController(MoonshineCafeContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<Models.Event>>> GetUpcomingEvents()
        {
            List<Event> events = await _context.Events.ToListAsync();
            List<Event> upcoming = new List<Event>();
            List<Event> sortedUpcoming = new List<Event>();
            DateTime now = DateTime.Now;

            foreach(Event e in events)
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

        private List<Event> BubbleSort(List<Event> toSort, int listLength)
        {
            Console.WriteLine("I've run" + listLength);
            if(listLength == 1)
            {
                return toSort;
            }

            List<Event> sorted = new List<Event>();
            for(int i = 0; i < listLength - 1; i++)
            {
                if(toSort[i].eventStart > toSort[i + 1].eventStart)
                {
                    Event temp = toSort[i];
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
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.id }, @event);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEvent(Event model)
        {
            
            var existedEvent = await _context.Set<Event>().FirstOrDefaultAsync(i=> i.id==model.id);
            
            if (existedEvent == null)
            {
                return BadRequest();
            }

            _context.Entry(existedEvent).CurrentValues.SetValues(model);

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

/*
        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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
*/


        /*        // PUT: api/Events/test
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut("{id}")]
                public async Task<IActionResult> PutEvent(int id, Models.Event @event)
                {
                    if (id != @event.id)
                    {
                        return BadRequest();
                    }

                    _context.Entry(@event).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EventExists(id))
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
        */


        /*        // POST: api/Events
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPost]
                public async Task<ActionResult<Models.Event>> PostEvent(Models.Event @event)
                {
                    //creating new event object based off of the Google API Event type
                    Google.Apis.Calendar.v3.Data.Event newEvent = new Google.Apis.Calendar.v3.Data.Event(){
                        //assigning values for events
                        Summary = @event.bandName + " " + @event.eventStart.Hour + " $" + @event.ticketPrice, 
                        Location = "137 Kerr St., Oakville, Ontario L6Z 3A6",
                        Description = @event.bandName + " " + @event.bandLink + " " + @event.description,
                        Start = new EventDateTime()
                        {
                            DateTime = DateTime.Parse(@event.eventStart.ToLongDateString())
                        },
                        End = new EventDateTime()
                        {
                            DateTime = DateTime.Parse(@event.eventEnd.ToLongDateString())
                        },
                    };

                    //Building request to insert the new event in the primary (default) calendar
                    EventsResource.InsertRequest request = TheMoonshineCafe.Program.service.Events.Insert(newEvent, "primary");
                    //Executes the request and assigns the response value to a variable to be used later
                    Google.Apis.Calendar.v3.Data.Event createdEvent = request.Execute();

                    //Overwriting the default google Calendar ID that was assigned in admin-crud-event.components.ts (101,20)
                    @event.googleCalID = createdEvent.Id;
                    //Adding the new event with the official Google Calendar ID from GOOGLE to the Events
                    _context.Events.Add(@event);
                    //Save the new event
                    await _context.SaveChangesAsync();

                    //displaying the link to the created event for troubleshooting purposes
                    Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);
                    //returns status code for event creation
                    return CreatedAtAction("GetEvent", new { id = @event.id }, @event);
                }*/

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
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
        }
/*
        //DELETE: api/Events/calID
        [HttpDelete("{calID}")]
        public async Task<IActionResult> DeleteEventByCalID(String calID)
        {
            Models.Event foundEvent = _context.Events.Where(e => e.googleCalID == calID).FirstOrDefault();
            var @event = await _context.Events.FindAsync(foundEvent.id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            TheMoonshineCafe.Program.service.Events.Delete("primary", calID).Execute();

            return NoContent();
        }*/

        private bool EventExists(int id)
        {
            return _context.Events.Any((System.Linq.Expressions.Expression<Func<Models.Event, bool>>)(e => e.id == id));
        }

/*        private bool EventExists(string calID)
        {
            return _context.Events.Any((System.Linq.Expressions.Expression<Func<Models.Event, bool>>)(e => e.googleCalID == calID));
        }*/
    }
}
