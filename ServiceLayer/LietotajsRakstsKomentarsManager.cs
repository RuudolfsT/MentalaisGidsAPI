using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.Dto;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class LietotajsRakstsKomentarsManager : BaseManager<LietotajsRakstsKomentars>, ILietotajsRakstsKomentarsManager
    {
        private readonly MentalaisGidsContext _context;

        public LietotajsRakstsKomentarsManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        // TODO: Pēc datu bāzes, lietotājam var būt tikai 1 komentārs uz rakstu.
        // Vai komentāru updateot (kā tas ir šobrīd), vai arī vnk neatļaut veidot komentāru?
        public async Task<bool> CreateOrUpdate(RakstsKomentarsCreateDto komentars, int user_id, int raksts_id)
        {
            var user = await _context.Lietotajs.FindAsync(user_id);
            var raksts = await _context.Raksts.FindAsync(raksts_id);

            if (user == null || raksts == null)
            {
                return false;
            }

            await SaveOrUpdate(new LietotajsRakstsKomentars
            {
                LietotajsID = user_id,
                RakstsID = raksts_id,
                Saturs = komentars.Saturs,
                DatumsUnLaiks = DateTime.Now
            });

            return true;
        }
    }
}