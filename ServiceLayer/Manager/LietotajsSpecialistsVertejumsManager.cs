using DomainLayer;
using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;

namespace ServiceLayer.Manager
{
    public class LietotajsSpecialistsVertejumsManager : BaseManager<LietotajsSpecialistsVertejums>, ILietotajsSpecialistsVertejumsManager
    {
        private readonly MentalaisGidsContext _context;

        public LietotajsSpecialistsVertejumsManager(MentalaisGidsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Status> CreateOrUpdate(int rating, int userId, int specialistId)
        {
            var status = new Status(true);
            var lietotajs = await _context.Lietotajs.FindAsync(userId);
            var specialists = await _context.Lietotajs.FindAsync(specialistId);

            if (lietotajs == null || specialists == null)
            {
                status.AddError("No user found");
                return status;
            }

            var vertejums = new LietotajsSpecialistsVertejums
            {
                LietotajsID = userId,
                SpecialistsID = specialistId,
                Balles = (byte)rating
            };

            await SaveOrUpdate(vertejums);
            return status;
        }

        public async Task<List<LietotajsSpecialistsVertejums>> GetAllUserRates(int userId)
        {
            return _context.LietotajsSpecialistsVertejums.Where(x => x.LietotajsID == userId).ToList();
        }
    }
}