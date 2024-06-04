using DomainLayer.dto;
using DomainLayer.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ServiceLayer.Manager;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SazinaController : ControllerBase
    {
        private ISazinaManager _sazinaManager;
        public SazinaController(ISazinaManager sazinaManager)
        {
            _sazinaManager = sazinaManager;
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs + "," + RoleUtils.Specialists)]
        [HttpDelete("delete")]
        public async Task<IActionResult> StopDialogue(int dialogueId)
        {
            var result = await _sazinaManager.StopDialogue(dialogueId);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs)]
        [HttpPost("start")]
        public async Task<IActionResult> StartDialogue(int receiverId)
        {
            var result = await _sazinaManager.StartDialogue(receiverId);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }


        [Authorize]
        [HttpGet("{id}/GetZinas")]
        public async Task<ActionResult<List<ZinaDto>>> GetZinas(int id)
        {
            var zinas = await _sazinaManager.GetZinas(id);
            if (zinas == null)
            {
                return NotFound();
            }

            return zinas;
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs + "," + RoleUtils.Specialists)]
        [HttpPost("{id}/PostZina")]
        public async Task<IActionResult> PostZina(int id, string zina)
        {
            var result = await _sazinaManager.PostZina(id, zina);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
