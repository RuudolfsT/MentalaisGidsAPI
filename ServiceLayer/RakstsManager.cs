using DomainLayer;
using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
