using MentalaisGidsAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RakstsController : ControllerBase
    {
        private IRakstsManager _rakstsManager;

        public RakstsController(IRakstsManager rakstsManager)
        {
            _rakstsManager = rakstsManager;
        }

        // GET: api/Lomas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Raksts>> GetRaksts(int id)
        {
            var raksts = await _rakstsManager.FindById(id);
            if (raksts == null)
            {
                return NotFound();
            }

            return raksts;
        }
    }
}
