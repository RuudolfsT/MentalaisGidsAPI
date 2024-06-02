using DomainLayer.Auth;
using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Interface;
using System.Security.Claims;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SajutuNovertejums : ControllerBase
    {
        private ISajutuNovertejumsManager _manager;
        // TODO:
        // private INodarbibaManager _nodarbibaManager;
        // ^ nodarbības tiks uzskaitītas Nodarbības Controllerī, nevis šeit
        //   vai atsevišķā "Kalendars" Controllerī. Liekas nevajadzīgi veidot
        //   atsevišķu Controlleri tikai nodarbību uzskaitei.
        private IUserService _userService;

        public SajutuNovertejums(ISajutuNovertejumsManager manager, IUserService userService)
        {
            _manager = manager;
            _userService = userService;
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs + "," + RoleUtils.Specialists)]
        // get with user id
        [HttpGet("getall/{id}")]
        public async Task<ActionResult<List<SajutuNovertejumsDto>>> GetAll(int id)
        {
            var user_roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            var user_id = _userService.GetUserId();
            return await _manager.GetAll(user_id, id, user_roles);
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs + "," + RoleUtils.Specialists)]
        // get SajutuNovertejums ieraksta ID
        [HttpGet("get/{id}")]
        public async Task<ActionResult<SajutuNovertejumsDto>> Get(int id)
        {
            var user_roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            var user_id = _userService.GetUserId();
            return await _manager.Get(id, user_id, user_roles);
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs)]
        [HttpPost]
        public async Task<IActionResult> Create(SajutuNovertejumsCreateDto novertejums)
        {
            var user_id = _userService.GetUserId();
            var success = await _manager.Create(novertejums, user_id);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user_id = _userService.GetUserId();
            var success = await _manager.Delete(id, user_id);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, SajutuNovertejumsUpdateDto novertejums)
        {
            var user_id = _userService.GetUserId();
            var success = await _manager.Update(id, user_id, novertejums);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

    }


 }