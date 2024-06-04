using DomainLayer.Enum;
using MentalaisGidsAPI.Domain.dto;
using MentalaisGidsAPI.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Interface;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodarbibaController : ControllerBase
    {
        private INodarbibaManager _nodarbibaManager;
        private IUserService _userService;

        public NodarbibaController(INodarbibaManager nodarbibaManager, IUserService userService)
        {
            _nodarbibaManager = nodarbibaManager;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<NodarbibaDto>> Get(int id)
        {
            return await _nodarbibaManager.Get(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<NodarbibaDto>>> GetAll()
        {
            return await _nodarbibaManager.GetAll();
        }

        [Authorize(Roles = RoleUtils.Specialists)]
        [HttpGet]
        [Route("GetAll/my-posts")]
        public async Task<ActionResult<List<NodarbibaDto>>> GetAllSpecialistsPosts()
        {
            var user_id = _userService.GetUserId();
            return await _nodarbibaManager.GetAll(user_id);
        }

        [Authorize(Roles = RoleUtils.Specialists)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(NodarbibaCreateDto new_nodarbiba_dto)
        {
            var user_id = _userService.GetUserId();
            var response = await _nodarbibaManager.Create(new_nodarbiba_dto, user_id);

            return Ok(response);
        }

        [Authorize(Roles = RoleUtils.Specialists)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user_id = _userService.GetUserId();

            var success = await _nodarbibaManager.Delete(id, user_id);

            if (success)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
        [Authorize(Roles = RoleUtils.Specialists)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, NodarbibaUpdateDto updated_nodarbiba)
        {
            var user_id = _userService.GetUserId();

            var success = await _nodarbibaManager.Update(id, user_id, updated_nodarbiba);

            if (success)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs)]
        [HttpPost("{id}/apply")]
        public async Task<IActionResult> Apply(int id)
        {
            var user_id = _userService.GetUserId();
            var success = await _nodarbibaManager.ApplyToNodarbibaAsync(id, user_id);

            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs)]
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var user_id = _userService.GetUserId();
            var success = await _nodarbibaManager.CancelNodarbibaAsync(id, user_id);

            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
