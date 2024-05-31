using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

            /// <summary>
            /// returns all the beasts without their abilities.
            /// </summary>
            /// <returns> returns all the beasts withou their abilities </returns>

            return await _context.Beast.ToListAsync();
        }

        // GET: api/Beasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beast>> GetBeast(int id)
        {

            /// <summary>
            /// returns a beast by it's id, but without the abilities.
            /// use GetBeastDetails() url => Api/Beast/Details/{id} to get with abilities.
            /// </summary>
            /// <param name="source"> the source for which all beast must be returned</param>
            /// <returns>returns the beast and all of it's abilities in the form of a string.</returns>

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
            //LINQ
            /// <summary>
            /// returns a beast and all it's abilities, as a string of JSONs.
            /// looks up the beast and then abilities.
            /// " {Beast details}, [{Ability details},{Ability details},{Ability details}] "
            /// </summary>
            /// <param name="source"> the source for which all beast must be returned</param>
            /// <returns>returns the beast and all of it's abilities in the form of a string.</returns>

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


            return String.Format("{0},{1}", beast, abilitiesDetails);

        }

        //GET: api/Beasts/Sources
        [HttpGet("Sources")]
        public async Task<ActionResult<List<String>>> GetAllSource()
        {
            //LINQ
            /// <summary>
            /// returns all sources as an array of strings.
            /// </summary>
            /// <param name="source"> the source for which all beast must be returned</param>
            /// <returns>returns a list of string</returns>

            List<String> sources = _context.Beast
                        .Select(b => b.Source)
                        .Distinct()
                        .ToList();
            return sources;
        }

        //GET: api/Beasts/Sources/source
        [HttpGet("Sources/{source}")]
        public async Task<ActionResult<List<Beast>>> GetBeastsBySource(string source) 
        {   
            //LINQ
            /// <summary>
            /// Looks up for any beast with a matching Source string.
            /// </summary>
            /// <param name="source"> the source for which all beast must be returned</param>
            /// <returns>returns a list of beast</returns>

            List<Beast> beasts = _context.Beast
                        .Where(b => b.Source.Equals(source))
                        .ToList(); 
            return beasts;
        }



        // PUT: api/Beasts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Beast>> PutBeast(int id, Beast beast)
        {

            /// <summary>
            /// updates a beast by receiving an ID and a beast,
            /// expected to receive the beast as a JSON.
            /// if the ID provided matches the Id of the provided beast, 
            /// updates the beast with the new value.
            /// </summary>
            /// <param name="id"> the ID of the beast to be updated</param>
            /// <param name="ability"> the new version of the beast</param>
            /// <returns>returns the updated beast</returns>

            if (id != beast.ID)
            {
                return BadRequest();
            }

            _context.Entry(beast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return beast;
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

            /// <summary>
            /// adds a beast to thge database.
            /// </summary>
            /// <param name="beast"> the new version of the beast</param>
            /// <returns>returns the newly inserted beast</returns>

            _context.Beast.Add(beast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeast", new { id = beast.ID }, beast);
        }

        // DELETE: api/Beasts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Beast>> DeleteBeast(int id)
        {

            /// <summary>
            /// Removes a beast by receiving its ID.
            /// </summary>
            /// <param name="id"> the ID of the beast to be removed</param>
            /// <returns>returns the removed beast</returns>

            var beast = await _context.Beast.FindAsync(id);
            Beast removedBeast = beast;
            if (beast == null)
            {
                return NotFound();
            }
            else 
            {
            _context.Beast.Remove(beast);
            await _context.SaveChangesAsync();

            return removedBeast;
            }

        }

        private bool BeastExists(int id)
        {
            return _context.Beast.Any(e => e.ID == id);
        }
    }
}
