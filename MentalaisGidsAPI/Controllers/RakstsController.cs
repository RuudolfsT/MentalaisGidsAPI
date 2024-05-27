using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MentalaisGidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RakstsController : ControllerBase
    {
        private IRakstsManager _rakstsManager;

        public RakstsController(IRakstsManager rakstsManager)
        {
            _rakstsManager = rakstsManager;
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
    }
}
