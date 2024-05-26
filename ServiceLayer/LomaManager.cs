using DomainLayer;
using MentalaisGidsAPI.Domain;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class LomaManager : ILomaManager
    {
        ILomaRepository _lomaRepository;

        public LomaManager()
        {
            _lomaRepository = IocContainer.Resolve<ILomaRepository>();
        }

        public LomaManager(ILomaRepository configurationRepository)
        {
            _lomaRepository = configurationRepository;
        }

        public async Task<Loma> GetLoma(int id)
        {
            return await _lomaRepository.FindById(id);
        }
    }
}
