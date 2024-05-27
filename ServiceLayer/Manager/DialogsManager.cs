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
    public class DialogsManager : BaseManager<Dialogs>, IDialaogsManager
    {
        private readonly MentalaisGidsContext _context;

        public DialogsManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> StopDialogue(int dialogueId)
        {
            var dialogue = await _context.Dialogs.FirstOrDefaultAsync(x => x.DialogsID == dialogueId);
            List<Zina> zinas = await _context.Zina.Where(x => x.DialogsID == dialogueId).ToListAsync();

            if (dialogue == null)
            {
                return false;
            }

            foreach (var zina in zinas)
            {
                _context.Zina.Remove(zina);
            }

            _context.Dialogs.Remove(dialogue);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
