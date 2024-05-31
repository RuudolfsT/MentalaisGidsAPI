using DomainLayer.dto;
using DomainLayer.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZinaController : ControllerBase
    {
        private IZinaManager _zinaManager;
        public ZinaController(IZinaManager zinaManager)
        {
            _zinaManager = zinaManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ZinaDto>>> GetZinas(int dialogsId)
        {
            var zinas = await _zinaManager.GetZinas(dialogsId);
            if (zinas == null)
            {
                return NotFound();
            }

            return zinas;
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs + "," + RoleUtils.Specialists)]
        [HttpPost("post")]
        public async Task<IActionResult> PostZina(int receiverId, string zina)
        {
            var result = await _zinaManager.PostZina(receiverId, zina);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
