using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class SecretKeyGenerator
    {
        public static string GenerateSecretKey(int length = 32)
        {
            var key = new byte[length];
            RandomNumberGenerator.Fill(key);
            return Convert.ToBase64String(key);
        }
    }
}
