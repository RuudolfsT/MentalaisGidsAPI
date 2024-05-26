using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentalaisGidsAPI.Domain;
using MentalaisGidsAPI.Models;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    internal class LietotajsManager : ILietotajsManager
    {
        private string _secret;

        public LietotajsManager(IConfiguration configuration)
        {
            var key = configuration.GetSection("Jwt")["Secret"];

            // sus????
            if (string.IsNullOrEmpty(key))
            {
                throw new ConfigurationErrorsException($"No secret key found!");
            }

            _secret = key;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lietotajs> GetAll()
        {
            throw new NotImplementedException();
        }

        public Lietotajs GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
