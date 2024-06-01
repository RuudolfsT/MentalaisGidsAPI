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
        private IUserService _userService;

        public RakstsController(IRakstsManager rakstsManager, IUserService userService, ILietotajsRakstsVertejumsManager lietotajsRakstsVertejumsManager)
        {
            _rakstsManager = rakstsManager;
            _userService = userService;
            _lietotajsRakstsVertejumsManager = lietotajsRakstsVertejumsManager;
        }

        // GET: hujzin mosk api/Raksts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RakstsDto>> GetRaksts(int id)
        {
            return await _rakstsManager.Get(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<RakstsDto>>> GetAll()
        {
            return await _rakstsManager.GetAll();
        }

        // TODO - jānoskaidro kuras lomas nar novērtēt rakstu
        [Authorize]
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

        [Authorize(Roles = RoleUtils.Specialists)]
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
    }
}
