using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ServiceLayer
{
    internal class UserService
    {
        private string _secret;

        public UserService(IConfiguration configuration)
        {
            var key = configuration.GetSection("Jwt")["Secret"];

            // sus????
            if (string.IsNullOrEmpty(key))
            {
                throw new ConfigurationErrorsException($"No secret key found!");
            }

            _secret = key;
        }
    }
}
