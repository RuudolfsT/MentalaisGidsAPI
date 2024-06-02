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
    public class DialogsManager : BaseManager<Dialogs>, IDialogsManager
    {
        private readonly MentalaisGidsContext _context;
        private readonly IUserService _userService;

        public DialogsManager(MentalaisGidsContext context, IUserService userService) : base(context)
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
    }
}
