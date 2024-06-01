using DomainLayer.Auth;
using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.Dto;
using MentalaisGidsAPI.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LietotajsController : ControllerBase
    {
        private ILietotajsManager _lietotajsManager;
        private IUserService _userService;
        private ILomaManager _lomaManager;
        private ILietotajsLomaManager _lietotajsLomaManager;


        public LietotajsController(ILietotajsManager manager, IUserService userService, ILomaManager lomaManager, ILietotajsLomaManager lietotajsLomaManager)
        {
            _lietotajsManager = manager;
            _userService = userService;
            _lomaManager = lomaManager;
            _lietotajsLomaManager = lietotajsLomaManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _lietotajsManager.Authenticate(model);

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
            var response = _lietotajsManager.Register(model);

            // TODO - te derētu kādu validāciju - vai db nobruka, vai lietotājs kaut ko ne tā
            // šeit visdrīzāk lietotājs kaut ko ne tā
            if (response == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = RoleUtils.Admins)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lietotajs>>> GetLietotajs()
        {
            var lietotaji = _lietotajsManager.GetAll();
            return Ok(lietotaji);
        }

        // GET: api/Lietotajs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lietotajs>> GetLietotajs(int id)
        {
            var lietotajs = await _lietotajsManager.FindById(id);

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
            var lietotajs = await _lietotajsManager.FindById(id);

            if (lietotajs == null)
            {
                return NotFound();
            }

            await _lietotajsManager.Delete(lietotajs);

            return Ok();
        }

        [Authorize(Roles = RoleUtils.Admins)]
        [HttpPost]
        [Route("AssignRole")]
        public async Task<ActionResult<Lietotajs>> AssignRole(LietotajsLomaDto lietotajsLomaDto)
        {
            var userId = lietotajsLomaDto.LietotajsId;
            var isValidUser = await _lietotajsManager.UserExists(userId);
            if (!isValidUser)
            {
                return BadRequest(Resources.UserDoesntExist);
            }

            var roleName = lietotajsLomaDto.LomasNosaukums;
            var isValidRole = await _lomaManager.RoleExists(roleName);
            if (!isValidRole)
            {
                return BadRequest(Resources.RoleDoesntExist);
            }

            var lietotajsLoma = new LietotajsLoma
            {
                LietotajsID = userId,
                LomaNosaukums = roleName
            };

            await _lietotajsLomaManager.SaveOrUpdate(lietotajsLoma);
            return Ok();
        }
    }
}
