using MentalaisGidsAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ServiceLayer.Manager;

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
        public async Task<ActionResult<Zina>> GetZina(int id)
        {
            var zina = await _zinaManager.FindById(id);
            if (zina == null)
            {
                return NotFound();
            }

            return zina;
        }

    }
}
