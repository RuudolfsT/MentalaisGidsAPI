using DomainLayer.dto;
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


        [HttpGet("{id}")]
        public async Task<ActionResult<ZinaDto>> GetZina(int id)
        {
            var zina = await _zinaManager.GetZina(id);
            if (zina == null)
            {
                return NotFound();
            }
            return zina;
        }


        [HttpPost("post")]
        public async Task<IActionResult> PostZina(int autorsId, int dialogsId, string saturs)
        {
            var result = await _zinaManager.PostZina(autorsId, dialogsId, saturs);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
