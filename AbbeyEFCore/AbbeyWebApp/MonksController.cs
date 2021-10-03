using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AbbeyClasses.Models;

namespace AbbeyWebApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonksController : ControllerBase
    {
        private readonly BuckfastabbeyContext _context;

        public MonksController(BuckfastabbeyContext context)
        {
            _context = context;
        }

        // GET: api/Monks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monk>>> GetMonk()
        {
            return await _context.Monk.ToListAsync();
        }

        // GET: api/Monks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monk>> GetMonk(int id)
        {
            var monk = await _context.Monk.FindAsync(id);

            if (monk == null)
            {
                return NotFound();
            }

            return monk;
        }

        // PUT: api/Monks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonk(int id, Monk monk)
        {
            if (id != monk.MonkId)
            {
                return BadRequest();
            }

            _context.Entry(monk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonkExists(id))
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

        // POST: api/Monks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Monk>> PostMonk(Monk monk)
        {
            _context.Monk.Add(monk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMonk", new { id = monk.MonkId }, monk);
        }

        // DELETE: api/Monks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Monk>> DeleteMonk(int id)
        {
            var monk = await _context.Monk.FindAsync(id);
            if (monk == null)
            {
                return NotFound();
            }

            _context.Monk.Remove(monk);
            await _context.SaveChangesAsync();

            return monk;
        }

        private bool MonkExists(int id)
        {
            return _context.Monk.Any(e => e.MonkId == id);
        }
    }
}
