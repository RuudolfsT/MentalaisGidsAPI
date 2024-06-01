using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class LomaManager : BaseManager<Loma>, ILomaManager
    {
        private readonly MentalaisGidsContext _context;

        public LomaManager(MentalaisGidsContext context) : base(context) {
            _context = context;
        }

        public async Task<bool> RoleExists(string id)
        {
            return _context.Set<Loma>().Any(x => x.LomaNosaukums == id);
        }
    }
}
