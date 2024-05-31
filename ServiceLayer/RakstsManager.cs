using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class RakstsManager : BaseManager<Raksts>, IRakstsManager
    {
        private readonly MentalaisGidsContext _context;
        private readonly ILietotajsRakstsVertejumsManager _lietotajsRakstsVertejumsManager;

        public RakstsManager(MentalaisGidsContext context, ILietotajsRakstsVertejumsManager lietotajsRakstsVertejumsManager) : base(context)
        {
            _context = context;
            _lietotajsRakstsVertejumsManager = lietotajsRakstsVertejumsManager;
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
                Vertejums = (int?)_context.LietotajsRakstsVertejums
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

        public async Task<RakstsRateResponseDto> Rate(RakstsRateDto rating, int user_id, int id)
        {
            var user = await _context.Lietotajs.FindAsync(user_id);
            var raksts = await _context.Raksts.FindAsync(id);

            if (user == null || raksts == null)
            {
                return null;
            }

            // call BaseManager method "SaveOrUpdate" to save the rating to LietotajsRakstsVertejums
            await _lietotajsRakstsVertejumsManager.SaveOrUpdate(new LietotajsRakstsVertejums
            {
                LietotajsID = user_id,
                RakstsID = id,
                Balles = rating.Balles
            });
            
            return new RakstsRateResponseDto
            {
                RakstsID = id,
                Balles = rating.Balles
            };
        }

        public async Task<RakstsCreateResponseDto> Create(RakstsCreateDto new_raksts_dto, int user_id)
        {
            var user = await _context.Lietotajs.FindAsync(user_id);

            if (user == null)
            {
                return null;
            }

            // call BaseManager method "SaveOrUpdate" to save the rating to LietotajsRakstsVertejums
            var new_raksts = await SaveOrUpdate(new Raksts
            {
                SpecialistsID = user_id,
                Virsraksts = new_raksts_dto.Virsraksts,
                Saturs = new_raksts_dto.Saturs,
                DatumsUnLaiks = DateTime.UtcNow
            });

            return new RakstsCreateResponseDto()
            {
                RakstsID = new_raksts.RakstsID
            };
        }

    }
}
