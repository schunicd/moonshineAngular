using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheMoonshineCafe.Data;
using TheMoonshineCafe.Models;

namespace TheMoonshineCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly MoonshineCafeContext _context;

        public ReservationsController(MoonshineCafeContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            reservation.timeResMade = DateTime.Parse("" + reservation.timeResMade.Year + "-" + 
                                                        ((reservation.timeResMade.Month < 10) ? ("0" + reservation.timeResMade.Month) : reservation.timeResMade.Month) + "-" + 
                                                        ((reservation.timeResMade.Day < 10) ? ("0" + reservation.timeResMade.Day) : reservation.timeResMade.Day) + "T" + 
                                                        ((reservation.timeResMade.Hour < 10) ? ("0" + reservation.timeResMade.Hour) : reservation.timeResMade.Hour) + ":" + 
                                                        ((reservation.timeResMade.Minute < 10) ? ("0" + reservation.timeResMade.Minute) : reservation.timeResMade.Minute) + ":" + 
                                                        ((reservation.timeResMade.Second < 10) ? ("0" + reservation.timeResMade.Second) : reservation.timeResMade.Second) + "+5:00");

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.id }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("eId={eId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationByEventID(int eId)
        {
            List<Reservation> reservations = await _context.Reservations.ToListAsync();

            foreach(Reservation r in reservations.ToList())
            {
                if(r.resEventid != eId)
                {
                    reservations.Remove(r);
                }
            }

            return reservations;
        }


        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.id == id);
        }
    }
}
