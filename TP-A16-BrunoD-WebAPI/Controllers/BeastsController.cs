using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP_A16_BrunoD_WebAPI.Data;
using TP_A16_BrunoD_WebAPI.Models;

namespace TP_A16_BrunoD_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeastsController : ControllerBase
    {
        private readonly TP_A16_BrunoD_WebAPIContext _context;

        public BeastsController(TP_A16_BrunoD_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Beasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beast>>> GetBeast()
        {
            return await _context.Beast.ToListAsync();
        }

        // GET: api/Beasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beast>> GetBeast(int id)
        {
            var beast = await _context.Beast.FindAsync(id);

            if (beast == null)
            {
                return NotFound();
            }

            return beast;
        }

        // PUT: api/Beasts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeast(int id, Beast beast)
        {
            if (id != beast.ID)
            {
                return BadRequest();
            }

            _context.Entry(beast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeastExists(id))
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

        // POST: api/Beasts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beast>> PostBeast(Beast beast)
        {
            _context.Beast.Add(beast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeast", new { id = beast.ID }, beast);
        }

        // DELETE: api/Beasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeast(int id)
        {
            var beast = await _context.Beast.FindAsync(id);
            if (beast == null)
            {
                return NotFound();
            }

            _context.Beast.Remove(beast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeastExists(int id)
        {
            return _context.Beast.Any(e => e.ID == id);
        }
    }
}
