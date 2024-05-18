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
    public class AbilitiesController : ControllerBase
    {
        private readonly TP_A16_BrunoD_WebAPIContext _context;

        public AbilitiesController(TP_A16_BrunoD_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Abilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ability>>> GetAbility()
        {
            return await _context.Ability.ToListAsync();
        }

        // GET: api/Abilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ability>> GetAbility(int id)
        {
            var ability = await _context.Ability.FindAsync(id);

            if (ability == null)
            {
                return NotFound();
            }

            return ability;
        }

        // PUT: api/Abilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbility(int id, Ability ability)
        {
            if (id != ability.Id)
            {
                return BadRequest();
            }

            _context.Entry(ability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbilityExists(id))
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

        // POST: api/Abilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ability>> PostAbility(Ability ability)
        {
            _context.Ability.Add(ability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbility", new { id = ability.Id }, ability);
        }

        // DELETE: api/Abilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbility(int id)
        {
            var ability = await _context.Ability.FindAsync(id);
            if (ability == null)
            {
                return NotFound();
            }

            _context.Ability.Remove(ability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbilityExists(int id)
        {
            return _context.Ability.Any(e => e.Id == id);
        }
    }
}
