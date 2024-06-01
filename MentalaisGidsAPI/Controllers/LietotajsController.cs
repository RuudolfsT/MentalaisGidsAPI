using DomainLayer.Auth;
using DomainLayer.Enum;
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
        private IUserService _userService;

        public LietotajsController(ILietotajsManager manager, IUserService userService)
        {
            _manager = manager;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _manager.Authenticate(model);

            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            var response = _manager.Register(model);

            // TODO - te derētu kādu validāciju - vai db nobruka, vai lietotājs kaut ko ne tā
            // šeit visdrīzāk lietotājs kaut ko ne tā
            if (response == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // GET: api/Lietotajs
        [Authorize]
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

        [Authorize(Roles = RoleUtils.Admins)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lietotajs>> DeleteLietotajs(int id)
        {
            var lietotajs = await _manager.FindById(id);

            if (lietotajs == null)
            {
                return NotFound();
            }

            await _manager.Delete(lietotajs);

            return Ok();
        }
    }
}
