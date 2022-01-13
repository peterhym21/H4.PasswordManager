using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Service.Services
{
    public class EncryptDecrypt
    {

        public byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        public byte[] Crypto(byte[] data, byte[] key, byte[] iv, bool encrypt = false)
        {
            ICryptoTransform mode;
            using var cryptography = Aes.Create();
            cryptography.Key = key;
            if (encrypt)
            {
                return cryptography.EncryptCbc(data, iv);
            }
            else
            {
                return cryptography.DecryptCbc(data, iv);
            }
        }

    }
}
