using DomainLayer.dto;
using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer.Manager
{
    public class ZinaManager : BaseManager<Zina>, IZinaManager
    {
        private readonly MentalaisGidsContext _context;
        private readonly IUserService _userService;


        public ZinaManager(MentalaisGidsContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<List<ZinaDto>?> GetZinas(int dialogsId)
        {
            var zinas = await _context.Zina
                          .Include(x => x.Autors)
                          .Where(x => x.DialogsID == dialogsId)
                          .ToListAsync();

            if (zinas == null)
            {
                return null;
            }

            var dtoList = zinas.Select(zina => new ZinaDto
            {
                DatumsUnLaiks = zina.DatumsUnLaiks,
                Saturs = zina.Saturs,
                AutoraVards = zina.Autors.Vards,
                AutoraUzvards = zina.Autors.Uzvards
            }).ToList();

            return dtoList;
        }

        public async Task<bool> PostZina(int receiverId, string zina)
        {
            var senderId = _userService.GetUserId();
            var dialogue = await _context.Dialogs
                                         .Include(d => d.Lietotajs)
                                         .Include(d => d.Specialists)
                                         .FirstOrDefaultAsync(x => (x.LietotajsID == senderId && x.SpecialistsID == receiverId)
                                                                || (x.LietotajsID == receiverId && x.SpecialistsID == senderId));

            if (dialogue == null)
            {
                return false;
            }


            var zinaObj = new Zina
            {
                DialogsID = dialogue.DialogsID,
                AutorsID = senderId,
                DatumsUnLaiks = DateTime.Now,
                Saturs = zina
            };


            await _context.Zina.AddAsync(zinaObj);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
