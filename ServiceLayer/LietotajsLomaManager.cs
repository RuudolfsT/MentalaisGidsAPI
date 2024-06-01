using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class LietotajsLomaManager : BaseManager<LietotajsLoma>, ILietotajsLomaManager
    {
        private readonly MentalaisGidsContext _context;

        public LietotajsLomaManager(MentalaisGidsContext context)
            : base(context) { }

    }
}
