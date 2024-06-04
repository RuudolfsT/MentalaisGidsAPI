using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Dto;
using MentalaisGidsAPI.Domain;
using ServiceLayer.Interface;

namespace ServiceLayer.Manager
{
    public class TestsManager : BaseManager<Tests>, ITestsManager
    {
        private readonly MentalaisGidsContext _context;

        public TestsManager(MentalaisGidsContext context)
            : base(context) { _context = context; }

        public async Task<bool> Create(TestsCreateDto test, int userId)
        {
            var user = await _context.Lietotajs.FindAsync(userId);

            if(user == null)
            {
                return null;
            }

            var newTests = await SaveOrUpdate(new Tests { 
                AutorsID = userId,
                Apraksts = test.Apraksts,
                
            });


            throw new NotImplementedException();
        }

        public Task<bool> Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Tests> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tests>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Results(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Submit(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
