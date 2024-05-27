using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class RakstsManager : BaseManager<Raksts>, IRakstsManager
    {
        private MentalaisGidsContext _context;

        public RakstsManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RakstsDto> Get(int id)
        {
            var raksts = await _context.Raksts
                .Include(raksts => raksts.LietotajsRakstsKomentars)
                .ThenInclude(komentars => komentars.Lietotajs)
                .Include(raksts => raksts.Specialists)
                .FirstAsync(raksts => raksts.RakstsID == id);

            if (raksts != null)
            {
                var averageVertejums = await _context.LietotajsRakstsVertejums
                    .Where(lrv => lrv.RakstsID == id)
                    .Select(lrv => (int?)lrv.Balles)
                    .DefaultIfEmpty()
                    .AverageAsync();

                var dto = new RakstsDto
                {
                    RakstsID = raksts.RakstsID,
                    SpecialistsID = raksts.SpecialistsID,
                    SpecialistsVards = raksts.Specialists.Vards,
                    SpecialistsUzvards = raksts.Specialists.Uzvards,
                    Virsraksts = raksts.Virsraksts,
                    Saturs = raksts.Saturs,
                    DatumsUnLaiks = raksts.DatumsUnLaiks,
                    Vertejums = (int?)averageVertejums,
                    Komentari = raksts.LietotajsRakstsKomentars
                    .Select(komentars => new KomentarsDto
                    {
                        LietotajsID = komentars.LietotajsID,
                        Vards = komentars.Lietotajs.Vards,
                        Uzvards = komentars.Lietotajs.Uzvards,
                        Saturs = komentars.Saturs,
                        DatumsUnLaiks = komentars.DatumsUnLaiks
                    }).ToList()
                };

                return dto;
            }

            return null;
        }


        //public BaseManager(ILomaRepository configurationRepository)
        //{
        //    _lomaRepository = configurationRepository;
        //}

        public async Task<List<RakstsDto>> GetAll()
        {

            var dtos = await _context.Raksts
            .Include(raksts => raksts.LietotajsRakstsKomentars)
            .ThenInclude(komentars => komentars.Lietotajs)
            .Include(raksts => raksts.Specialists)
            .Select(raksts => new RakstsDto
            {
                RakstsID = raksts.RakstsID,
                SpecialistsID = raksts.SpecialistsID,
                SpecialistsVards = raksts.Specialists.Vards,
                SpecialistsUzvards = raksts.Specialists.Uzvards,
                Virsraksts = raksts.Virsraksts,
                Saturs = raksts.Saturs,
                DatumsUnLaiks = raksts.DatumsUnLaiks,
                Vertejums = (int?) _context.LietotajsRakstsVertejums
                    .Where(lrv => lrv.RakstsID == raksts.RakstsID)
                    .Select(lrv => (int?)lrv.Balles)
                    .DefaultIfEmpty()
                    .Average() ?? null, // Calculate average here
                Komentari = raksts.LietotajsRakstsKomentars
                .Select(komentars => new KomentarsDto
                {
                    LietotajsID = komentars.LietotajsID,
                    Vards = komentars.Lietotajs.Vards,
                    Uzvards = komentars.Lietotajs.Uzvards,
                    Saturs = komentars.Saturs,
                    DatumsUnLaiks = komentars.DatumsUnLaiks
                }).ToList()
            }).ToListAsync();

            return dtos;

        }
    }
}
