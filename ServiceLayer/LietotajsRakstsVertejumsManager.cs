using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Domain.dto;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class LietotajsRakstsVertejumsManager : BaseManager<LietotajsRakstsVertejums>, ILietotajsRakstsVertejumsManager
    {
        private readonly MentalaisGidsContext _context;

        public LietotajsRakstsVertejumsManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        /*
         * Darījumprasības:
         * BLOG_RATE
         */
        public async Task<RakstsRateResponseDto> CreateOrUpdate(RakstsRateDto rating, int user_id, int id)
        {
            var user = await _context.Lietotajs.FindAsync(user_id);
            var raksts = await _context.Raksts.FindAsync(id);

            if (user == null || raksts == null)
            {
                return null;
            }

            await SaveOrUpdate(new LietotajsRakstsVertejums
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
    }
}