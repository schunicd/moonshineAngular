﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheMoonshineCafe.Data;
using TheMoonshineCafe.Models;

namespace TheMoonshineCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    public class AdminHomeController : ControllerBase
    {
        private readonly MoonshineCafeContext _context;

        public AdminHomeController(MoonshineCafeContext context)
        {
            _context = context;
        }

/*        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        [HttpGet("email={email}")]
        public async Task<ActionResult<Admin>> GetAdminByEmail(string email)
        {
            var admins = await _context.Admins.ToListAsync();
            Admin admin = null;

            foreach (var a in admins)
            {
                if (a.email.Equals(email))
                {
                    admin = a;
                }
            }

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.id)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.id }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.id == id);
        }*/
    }
}
