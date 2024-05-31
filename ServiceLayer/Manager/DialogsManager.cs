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
    }
}
