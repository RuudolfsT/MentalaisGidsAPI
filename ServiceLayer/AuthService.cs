using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ServiceLayer
{
    internal class AuthService
    {
        private readonly string _secret;

        public AuthService(IConfiguration configuration)
        {
            var key = configuration.GetSection("Jwt")["Secret"];

            // sus????
            if (string.IsNullOrEmpty(_secret))
            {
                throw new ConfigurationErrorsException($"No secret key found!");
            }
        }
    }
}
