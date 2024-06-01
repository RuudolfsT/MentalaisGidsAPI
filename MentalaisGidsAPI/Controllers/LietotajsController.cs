using DomainLayer;
using DomainLayer.Auth;
using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.Dto;
using MentalaisGidsAPI.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using System.Security.Claims;

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
        private ILietotajsSpecialistsVertejumsManager _lietotajsSpecialistsVertejumsManager;

        public LietotajsController(ILietotajsManager manager, IUserService userService, ILomaManager lomaManager, ILietotajsLomaManager lietotajsLomaManager, ILietotajsSpecialistsVertejumsManager lietotajsSpecialistsVertejumsManager)
        {
            _lietotajsManager = manager;
            _userService = userService;
            _lomaManager = lomaManager;
            _lietotajsLomaManager = lietotajsLomaManager;
            _lietotajsSpecialistsVertejumsManager = lietotajsSpecialistsVertejumsManager;
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

        [Authorize(Roles = RoleUtils.Visi)]
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
        [HttpPost("AssignRole")]
        public async Task<ActionResult<Status>> AssignRole(LietotajsLomaDto lietotajsLomaDto)
        {
            var status = new Status(true);

            var userId = lietotajsLomaDto.LietotajsId;
            var isValidUser = await _lietotajsManager.UserExists(userId);
            if (!isValidUser)
            {
                status.AddError(Resources.UserDoesntExist);
                return status;
            }

            var roleName = lietotajsLomaDto.LomasNosaukums;
            var isValidRole = await _lomaManager.RoleExists(roleName);
            if (!isValidRole)
            {
                status.AddError(Resources.RoleDoesntExist);
                return status;
            }

            var lietotajsLoma = new LietotajsLoma
            {
                LietotajsID = userId,
                LomaNosaukums = roleName
            };

            await _lietotajsLomaManager.SaveOrUpdate(lietotajsLoma);
            return Ok(status);
        }

        [Authorize(Roles = RoleUtils.Visi)]
        [HttpPost("EditUser")]
        public async Task<Status> EditUser(LietotajsEditDto lietotajsEditDto)
        {
            var status = new Status(true);
            var currentUserId = _userService.GetUserId();

            if (currentUserId != lietotajsEditDto.LietotajsId)
            {
                var isUserAdmin = User.FindAll(ClaimTypes.Role).Select(r => r.Value).Any(x => x == RoleUtils.Admins);
                if (!isUserAdmin)
                {
                    status.AddError(Resources.InvalidPermissionsToEdit);
                    return status;
                }
            }

            var userId = lietotajsEditDto.LietotajsId;
            var user = await _lietotajsManager.FindById(userId);
            if (user == null)
            {
                status.AddError(Resources.UserDoesntExist);
                return status;
            }

            if (!string.IsNullOrEmpty(lietotajsEditDto.Vards))
            {
                user.Vards = lietotajsEditDto.Vards;
            }

            if (!string.IsNullOrEmpty(lietotajsEditDto.Uzvards))
            {
                user.Uzvards = lietotajsEditDto.Uzvards;
            }

            if (!string.IsNullOrEmpty(lietotajsEditDto.Lietotajvards))
            {
                var usernameExists = await _lietotajsManager.UsernameExists(lietotajsEditDto.Lietotajvards);
                if (usernameExists)
                {
                    status.AddError(Resources.UsernameAlreadyExists);
                    return status;
                }

                user.Lietotajvards = lietotajsEditDto.Lietotajvards;
            }

            user.Dzimums = lietotajsEditDto.Dzimums ?? user.Dzimums;

            if (!string.IsNullOrEmpty(lietotajsEditDto.Epasts))
            {
                if (!lietotajsEditDto.Epasts.Contains("@"))
                {
                    status.AddError(Resources.InvalidEmail);
                    return status;
                }

                user.Epasts = lietotajsEditDto.Epasts;
            }

            if (!string.IsNullOrEmpty(lietotajsEditDto.DzimsanasGads))
            {
                var format = "dd-MM-yyyy";
                DateTime date;
                var success = DateTime.TryParseExact(lietotajsEditDto.DzimsanasGads, format, null, System.Globalization.DateTimeStyles.None, out date);
                if (!success)
                {
                    status.AddError(Resources.WrongDateFormat);
                }

                user.DzimsanasGads = date;
            }

            if (!string.IsNullOrEmpty(lietotajsEditDto.Parole))
            {
                if (lietotajsEditDto.Parole.Length < 8)
                {
                    status.AddError(Resources.PasswordTooShort);
                    return status;
                }

                var newPassword = _lietotajsManager.HashPassword(lietotajsEditDto.Parole, user);
                user.Parole = newPassword;
            }

            _lietotajsManager.SaveOrUpdate(user);
            return status;
        }

        [Authorize(Roles = RoleUtils.Visi)]
        [HttpPost("Rate")]
        public async Task<Status> Rate(SpecialistsRateDto specialistsRateDto)
        {
            var status = new Status(true);
            var rating = specialistsRateDto.Vertejums;
            if (rating <= 0 || rating >= 11)
            {
                status.AddError(Resources.InvalidRating);
                return status;
            }

            var specialistId = specialistsRateDto.SpecialistaId;
            var isValidSpecialistId = await _lietotajsManager.UserExists(specialistId);
            if (!isValidSpecialistId)
            {
                status.AddError(Resources.UserDoesntExist);
                return status;
            }

            var userId = _userService.GetUserId();
            status = await _lietotajsSpecialistsVertejumsManager.CreateOrUpdate(rating, userId, specialistId);

            return status;
        }

        [Authorize(Roles = RoleUtils.Visi)]
        [HttpGet("GetAllRates")]
        public async Task<List<LietotajsSpecialistsVertejums>> GetAllRates()
        {
            var userId = _userService.GetUserId();
            var allUserRates = await _lietotajsSpecialistsVertejumsManager.GetAllUserRates(userId);

            return allUserRates;
        }
    }
}
