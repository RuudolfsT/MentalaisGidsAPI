using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceLayer.Interface;
using System.Net;
using System.Security.Claims;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RakstsController : ControllerBase
    {
        private IRakstsManager _rakstsManager;
        private ILietotajsRakstsVertejumsManager _lietotajsRakstsVertejumsManager;
        private ILietotajsRakstsKomentarsManager _lietotajsRakstsKomentarsManager;
        private IUserService _userService;

        public RakstsController(IRakstsManager rakstsManager,
            IUserService userService,
            ILietotajsRakstsVertejumsManager lietotajsRakstsVertejumsManager,
            ILietotajsRakstsKomentarsManager lietotajsRakstsKomentarsManager)
        {
            _rakstsManager = rakstsManager;
            _userService = userService;
            _lietotajsRakstsVertejumsManager = lietotajsRakstsVertejumsManager;
            _lietotajsRakstsKomentarsManager = lietotajsRakstsKomentarsManager;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<RakstsDto>> GetRaksts(int id)
        {
            return await _rakstsManager.Get(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<RakstsDto>>> GetAll()
        {
            return await _rakstsManager.GetAll();
        }

        [Authorize(Roles = RoleUtils.Specialists)]
        [HttpGet]
        [Route("GetAll/my-posts")]
        public async Task<ActionResult<List<RakstsDto>>> GetAllSpecialistsPosts()
        {
            var user_id = _userService.GetUserId();
            return await _rakstsManager.GetAll(user_id);
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs + "," + RoleUtils.Specialists)]
        [HttpPost("rate/{id}")]
        public async Task<IActionResult> Rate(int id, RakstsRateDto rating)
        {
            if (ModelState.IsValid)
            {
                var user_id = _userService.GetUserId();

                var response = await _lietotajsRakstsVertejumsManager.CreateOrUpdate(rating, user_id, id);

                return Ok(response);
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return StatusCode((int)HttpStatusCode.BadRequest, allErrors);
            }
        }

        [Authorize(Roles = RoleUtils.Specialists + "," + RoleUtils.Admins)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(RakstsCreateDto new_raksts_dto)
        {
            if (ModelState.IsValid)
            {
                var user_id = _userService.GetUserId();

                var response = await _rakstsManager.Create(new_raksts_dto, user_id);

                return Ok(response);
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return StatusCode((int)HttpStatusCode.BadRequest, allErrors);
            }

        }

        [Authorize(Roles = RoleUtils.Specialists + "," + RoleUtils.Admins)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user_id = _userService.GetUserId();
            var user_roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            var success = await _rakstsManager.Delete(id, user_id, user_roles);

            if (success)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [Authorize(Roles = RoleUtils.Specialists + "," + RoleUtils.Admins)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, RakstsUpdateDto updated_raksts)
        {
            if (ModelState.IsValid)
            {
                var user_id = _userService.GetUserId();
                var user_roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                var success = await _rakstsManager.Update(id, user_id, user_roles, updated_raksts);

                if (success)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
            }
            else
                {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return StatusCode((int)HttpStatusCode.BadRequest, allErrors);
            }
        }
        [Authorize]
        [HttpPost("{id}/create-comment")]
        public async Task<IActionResult> CreateKomentars(int id, RakstsKomentarsCreateDto komentars)
        {
            if (ModelState.IsValid)
            {
                var user_id = _userService.GetUserId();

                var success = await _lietotajsRakstsKomentarsManager.CreateOrUpdate(komentars, user_id, id);

                if (success)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return StatusCode((int)HttpStatusCode.BadRequest, allErrors);
            }
        }
    }
}
