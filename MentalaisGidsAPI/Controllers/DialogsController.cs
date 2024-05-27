using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ServiceLayer.Manager;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogsController : ControllerBase
    {
        private IDialaogsManager _dialogsManager;
        public DialogsController(IDialaogsManager dialogsManager)
        {
            _dialogsManager = dialogsManager;
        }

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
    }
}
