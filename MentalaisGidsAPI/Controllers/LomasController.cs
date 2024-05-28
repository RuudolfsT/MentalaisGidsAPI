using MentalaisGidsAPI.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LomasController : ControllerBase
    {
        private ILomaManager _lomaManager;
        private IRakstsManager _rakstsManager;

        public LomasController(ILomaManager lomaManager, IRakstsManager rakstsManager)
        {
            _lomaManager = lomaManager;
            _rakstsManager = rakstsManager;
        }

        //// GET: api/Lomas
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Loma>>> GetLoma()
        //{
        //    //if (_context.Loma == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return await _context.Loma.ToListAsync();
        //}

        // GET: api/Lomas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Raksts>> GetLoma(int id)
        {
            //if (_context.Loma == null)
            //{
            //    return NotFound();
            //}
            //var loma = await _context.Loma.FindAsync(id);
            var loma = await _rakstsManager.FindById(id);
            if (loma == null)
            {
                return NotFound();
            }

            return loma;
        }

        //// PUT: api/Lomas/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLoma(string id, Loma loma)
        //{
        //    if (id != loma.LomaNosaukums)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(loma).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LomaExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Lomas
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Loma>> PostLoma(Loma loma)
        //{
        //    if (_context.Loma == null)
        //    {
        //        return Problem("Entity set 'MentalaisGidsContext.Loma'  is null.");
        //    }
        //    _context.Loma.Add(loma);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (LomaExists(loma.LomaNosaukums))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetLoma", new { id = loma.LomaNosaukums }, loma);
        //}

        //// DELETE: api/Lomas/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLoma(string id)
        //{
        //    if (_context.Loma == null)
        //    {
        //        return NotFound();
        //    }
        //    var loma = await _context.Loma.FindAsync(id);
        //    if (loma == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Loma.Remove(loma);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool LomaExists(string id)
        //{
        //    return (_context.Loma?.Any(e => e.LomaNosaukums == id)).GetValueOrDefault();
        //}
    }
}
