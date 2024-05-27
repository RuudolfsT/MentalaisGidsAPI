using DomainLayer.Auth;
using MentalaisGidsAPI.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _manager.Authenticate(model);
            return Ok(response);
        }

        public IActionResult Register(RegisterRequest model)
        {
            _manager.Register(model);
            return Ok();
        }

        // GET: api/Lietotajs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lietotajs>>> GetLietotajs()
        {
            var lietotaji = _manager.GetAll();
            return Ok(lietotaji);
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
    }
}
