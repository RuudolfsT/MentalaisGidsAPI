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

        // GET: api/Lomas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Raksts>> GetLoma(int id)
        {
            var loma = await _rakstsManager.FindById(id);
            if (loma == null)
            {
                return NotFound();
            }

            return loma;
        }
    }
}
