using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;

namespace ServiceLayer.Manager
{
    public class ZinaManager : BaseManager<Zina>, IZinaManager
    {
        private readonly MentalaisGidsContext _context;

        public ZinaManager(MentalaisGidsContext context) : base(context)
        {
        }
    }
}
