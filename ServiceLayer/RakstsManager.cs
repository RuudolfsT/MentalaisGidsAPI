using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class RakstsManager : BaseManager<Raksts>, IRakstsManager
    {
        private readonly MentalaisGidsContext _context;

        public RakstsManager(MentalaisGidsContext context) : base(context)
        {
        }


        //public BaseManager(ILomaRepository configurationRepository)
        //{
        //    _lomaRepository = configurationRepository;
        //}
    }
}
