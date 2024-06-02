using DomainLayer.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ServiceLayer.Manager;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogsController : ControllerBase
    {
        private IDialogsManager _dialogsManager;
        public DialogsController(IDialogsManager dialogsManager)
        {
            _dialogsManager = dialogsManager;
        }

        [Authorize(Roles = RoleUtils.ParastsLietotajs + "," + RoleUtils.Specialists)]
        [HttpDelete("delete")]
        public async Task<IActionResult> StopDialogue(int dialogueId)
        {
            var result = await _dialogsManager.StopDialogue(dialogueId);

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
            var result = await _dialogsManager.StartDialogue(receiverId);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
