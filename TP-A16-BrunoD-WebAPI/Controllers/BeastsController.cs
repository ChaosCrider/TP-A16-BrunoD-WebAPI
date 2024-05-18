using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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

        // GET: api/Beasts/Details/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<string>> GetBeastDetails(int id) 
        {
            Beast beast = await _context.Beast.FindAsync(id);

            if (beast == null)
            {
                return NotFound();
            }

            StringBuilder abilitiesDetails = new StringBuilder();
            List<Ability> abilities = _context.Ability
                .Where(x => x.Id == id).ToList();

            foreach (Ability a in abilities)
            {
                abilitiesDetails.Append(a);
            }


            return String.Format("{0}{1}", beast, abilitiesDetails);

        }


        // GET: api/Beast/Sources
        [HttpGet("Source")]
        public async Task<ActionResult<List<String>>> GetSources() 
        {
            List<String> sources = await _context.Beast
                .Select(x => x.Source)
                .Distinct()
                .ToListAsync();

            return sources;
        }


        // GET: api/Beasts/Sources/<source>
        [HttpGet("Sources/{source}")]
        public async Task<ActionResult<List<Beast>>> GetBeastsBySource(string Source) 
        {
            List<Beast> beasts = _context.Beast
                            .Where(b => b.Source == Source).ToList();
            return new List<Beast>();
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
