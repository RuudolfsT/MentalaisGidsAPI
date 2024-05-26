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
        public static byte[] GenerateSecretKey(int length = 32)
        {
            var key = new byte[length];
            RandomNumberGenerator.Fill(key);
            return Encoding.ASCII.GetBytes(Convert.ToBase64String(key));
        }
    }
}
