using DomainLayer.Auth;
using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Interface;

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
        [HttpGet]
        public async Task<ActionResult<List<SajutuNovertejumsDto>>> GetAll()
        {
            var user_id = _userService.GetUserId();
            return await _manager.GetAll(user_id);
        }

        // TODO: pārējie route

    }


 }