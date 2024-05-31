using MentalaisGidsAPI.Domain;
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
    }
}