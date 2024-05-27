using DomainLayer.dto;
using MentalaisGidsAPI.Domain;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer.Manager
{
    public class ZinaManager : BaseManager<Zina>, IZinaManager
    {
        private readonly MentalaisGidsContext _context;


        public ZinaManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ZinaDto> GetZina(int zinaId)
        {
            var zina = await _context.Zina.Include(x => x.Autors).FirstOrDefaultAsync(x => x.ZinaID == zinaId);
            if (zina == null)
            {
                return null;
            }
            var dto = new ZinaDto
            {
                DatumsUnLaiks = zina.DatumsUnLaiks,
                Saturs = zina.Saturs,
                AutoraVards = zina.Autors?.Vards,
                AutoraUzvards = zina.Autors?.Uzvards
            };

            return dto;
        }

        public async Task<bool> PostZina(int userId, int dialogueId, string zina)
        {
            var zinaObj = new Zina
            {
                DialogsID = dialogueId,
                AutorsID = userId,
                DatumsUnLaiks = DateTime.Now,
                Saturs = zina
            };

            await _context.Zina.AddAsync(zinaObj);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
