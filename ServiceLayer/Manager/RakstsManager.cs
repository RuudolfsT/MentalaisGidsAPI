using DomainLayer.Enum;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace ServiceLayer.Manager
{
    public class RakstsManager : BaseManager<Raksts>, IRakstsManager
    {
        private readonly MentalaisGidsContext _context;

        public RakstsManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        /*
         * Darījumprasības:
         * BLOG_VIEW
         */
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
                    }).ToList(),
                    KomentariCount = null
                };

                return dto;
            }

            return null;
        }

        /*
         * Darījumprasības:
         * BLOG_LIST_VIEW
         * 
         * Iespēja gan skatīt visus rakstus, gan pēc speciālista ID (kas atbilst darījumprasībām)
         */
        public async Task<List<RakstsDto>> GetAll(int? specialistsId = null)
        {
            var query = _context.Raksts.AsQueryable();
            if (specialistsId != null)
            {
                query = query.Where(raksts => raksts.SpecialistsID == specialistsId.Value);
            }

            var dtos = await query
            .Include(raksts => raksts.LietotajsRakstsKomentars)
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
                    .Average() ?? null,
                Komentari = null,
                KomentariCount = raksts.LietotajsRakstsKomentars.Count
            }).ToListAsync();

            return dtos;

        }

        /*
         * Darījumprasības:
         * BLOG_CREATE
         */
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
                DatumsUnLaiks = DateTime.Now
            });

            return new RakstsCreateResponseDto()
            {
                RakstsID = new_raksts.RakstsID
            };
        }

        /*
         * Darījumprasības:
         * BLOG_DEL
         * ADMIN_BLOG_DEL
         */
        public async Task<bool> Delete(int raksts_id, int user_id, List<string> user_roles)
        {
            var raksts = await FindById(raksts_id);

            if (raksts == null)
            {
                return false;
            }

            if (user_roles.Contains(RoleUtils.Admins) || raksts.SpecialistsID == user_id)
            {
                await Delete(raksts);
                return true;
            }

            return false;

        }

        /*
         * Darījumprasības:
         * BLOG_EDIT
         * ADMIN_BLOG_EDIT
         */
        public async Task<bool> Update(int id, int user_id, List<string> user_roles, RakstsUpdateDto updated_raksts)
        {
            var raksts = await FindById(id);

            if (raksts == null)
            {
                return false;
            }

            if (user_roles.Contains(RoleUtils.Admins) || raksts.SpecialistsID == user_id)
            {
                if (updated_raksts.Virsraksts != null) raksts.Virsraksts = updated_raksts.Virsraksts;
                if (updated_raksts.Saturs != null) raksts.Saturs = updated_raksts.Saturs;
                await SaveOrUpdate(raksts);
                return true;
            }

            return false;
        }
    }
}
