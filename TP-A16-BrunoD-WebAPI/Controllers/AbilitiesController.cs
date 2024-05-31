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
            ///<summary>
            /// returns all the ability in the database.
            /// </summary>
            /// <returns>a list of beast entit</returns>
            return await _context.Ability.ToListAsync();
        }

        // GET: api/Abilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ability>> GetAbilityByID(int id)
        {
            ///<summary>
            /// returns the ability that matches the provided ID.
            /// </summary>
            /// <param name="id">the Id of the ability to be returned</param>
            /// <returns>a beast entity</returns>

            var ability = await _context.Ability.FindAsync(id);

            if (ability == null)
            {
                return NotFound();
            }

            return ability;
        }

        //GET: api/Abilities/Beast/5
        [HttpGet("Beast/{id}")]
        public async Task<ActionResult<List<Ability>>> GetAbilitiesByBeast(int id) 
        {
            //LINQ
            /// <summary>
            /// uses the URL to look up all abilitied tied to a Beast ID
            /// then returns a lsit of all abilitiy from that beast.
            /// <summary>
            /// <param name="id"> the ID of the Beast for which all the ability are requested</param>
            /// <returns>returns the ability updated</returns>

            List<Ability> abilities = await _context.Beast
                .Where(b => b.ID == id)
                .SelectMany(b => b.Abilities).ToListAsync();
            return abilities;
        }


        // PUT: api/Abilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Ability>> PutAbility(int id, Ability ability)
        {

            /// <summary>
            /// updates an ability by receiving an ID and an ability,
            /// expected to receive the ability as a JSON
            /// if the ID of the ability provided matches the Id of the beast, 
            /// updates the ability with the new value.
            /// </summary>
            /// <param name="id"> the ID of the ability to be updated</param>
            /// <param name="ability"> the new version of the ability</param>
            /// <returns>returns the updated ability</returns>

            if (id != ability.Id)
            {
                return BadRequest();
            }

            _context.Entry(ability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return ability;
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

            /// <summary>
            /// Adds the received ability to the database.
            /// </summary>
            /// <param name="ability"> the ability to be added</param>
            /// <returns>returns the ability back</returns>

            _context.Ability.Add(ability);
            await _context.SaveChangesAsync();

            return ability;
        }

        // DELETE: api/Abilities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ability>> DeleteAbility(int id)
        {

            /// <summary>
            /// Removes an ability from the database based on received id.
            /// </summary>
            /// <param name="id"> the id of the ability to be removed</param>
            /// <returns>returns the deleted ability back</returns>

            var ability = await _context.Ability.FindAsync(id);
            if (ability == null)
            {
                return NotFound();
            }
            Ability deletedAbility = ability as Ability;
            _context.Ability.Remove(ability);
            await _context.SaveChangesAsync();

            return deletedAbility;
        }

        private bool AbilityExists(int id)
        {
            return _context.Ability.Any(e => e.Id == id);
        }
    }
}
