using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LietotajsController : ControllerBase
    {
        private ILietotajsManager _manager;

        public LietotajsController(ILietotajsManager manager)
        {
            _manager = manager;
        }

        // GET: api/Lietotajs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lietotajs>>> GetLietotajs()
        {
            if (_manager.Lietotajs == null)
            {
                return NotFound();
            }
            return await _manager.Lietotajs.ToListAsync();
        }

        // GET: api/Lietotajs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lietotajs>> GetLietotajs(int id)
        {
            var lietotajs = await _manager.FindById(id);

            if (lietotajs == null)
            {
                return NotFound();
            }

            return lietotajs;
        }

        // PUT: api/Lietotajs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLietotajs(int id, Lietotajs lietotajs)
        {
            if (id != lietotajs.LietotajsID)
            {
                return BadRequest();
            }

            _manager.Entry(lietotajs).State = EntityState.Modified;

            try
            {
                await _manager.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LietotajsExists(id))
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

        // POST: api/Lietotajs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lietotajs>> PostLietotajs(Lietotajs lietotajs)
        {
            if (_manager.Lietotajs == null)
            {
                return Problem("Entity set 'MentalaisGidsContext.Lietotajs'  is null.");
            }
            _manager.Lietotajs.Add(lietotajs);
            await _manager.SaveChangesAsync();

            return CreatedAtAction("GetLietotajs", new { id = lietotajs.LietotajsID }, lietotajs);
        }

        // DELETE: api/Lietotajs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLietotajs(int id)
        {
            if (_manager.Lietotajs == null)
            {
                return NotFound();
            }
            var lietotajs = await _manager.Lietotajs.FindAsync(id);
            if (lietotajs == null)
            {
                return NotFound();
            }

            _manager.Lietotajs.Remove(lietotajs);
            await _manager.SaveChangesAsync();

            return NoContent();
        }

        //public async Task<IActionResult> Authenticate(AuthenticateRequest authRequest)
        //{
        //    //var response
        //}

        private bool LietotajsExists(int id)
        {
            return (_manager.Lietotajs?.Any(e => e.LietotajsID == id)).GetValueOrDefault();
        }
    }
}
