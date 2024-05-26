using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class LomaManager : BaseManager<Loma>, ILomaManager
    {
        private readonly MentalaisGidsContext _context;

        public LomaManager(MentalaisGidsContext context) : base(context)
        {
        }


        //public BaseManager(ILomaRepository configurationRepository)
        //{
        //    _lomaRepository = configurationRepository;
        //}
    }
}
