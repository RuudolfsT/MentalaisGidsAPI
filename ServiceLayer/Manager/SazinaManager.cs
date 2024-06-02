using DomainLayer.dto;
using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Manager
{
    public class SazinaManager : BaseManager<Dialogs>, ISazinaManager
    {
        private readonly MentalaisGidsContext _context;
        private readonly IUserService _userService;

        public SazinaManager(MentalaisGidsContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public async Task<bool> StopDialogue(int dialogueId)
        {
            var dialogue = await _context.Dialogs.FirstOrDefaultAsync(x => x.DialogsID == dialogueId);
            if (dialogue == null)
            {
                return false;
            }

            var userId = _userService.GetUserId();
            bool userIsInDialogue = await _context.Dialogs.AnyAsync(x => x.DialogsID == dialogueId && x.LietotajsID == userId);
            if (!userIsInDialogue)
            {
                return false;
            }

            List<Zina> zinas = await _context.Zina.Where(x => x.DialogsID == dialogueId).ToListAsync();

            _context.Zina.RemoveRange(zinas);
            _context.Dialogs.Remove(dialogue);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> StartDialogue(int receiverId)
        {
            var userId = _userService.GetUserId();
            var dialogue = await _context.Dialogs.FirstOrDefaultAsync(x => 
                                                                    (x.LietotajsID == userId && x.SpecialistsID == receiverId)
                                                                    ||
                                                                    x.LietotajsID == receiverId && x.SpecialistsID == userId);
            // Ja dialogs jau eksistē, tad nav nepieciešams veidot jaunu
            if (dialogue != null)
            {
                return false;
            }

            var isSpecialist = await _context.LietotajsLoma.AnyAsync(x => x.LietotajsID == receiverId && x.LomaNosaukums == RoleUtils.Specialists);

            // Ja saņēmējs nav speciālists, tad dialogs netiek izveidots
            if (isSpecialist == false)
            {
                return false;
            }

            var newDialogue = new Dialogs
            {
                LietotajsID = userId,
                SpecialistsID = receiverId
            };

            await SaveOrUpdate(newDialogue);
            return true;
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
        public async Task<bool> PostZina(int dialogsId, string zina)
        {
            var senderId = _userService.GetUserId();
            var dialogue = await _context.Dialogs.FirstOrDefaultAsync(x => x.DialogsID == dialogsId);

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
