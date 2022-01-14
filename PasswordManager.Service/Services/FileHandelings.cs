using PasswordManager.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Service.Services
{
    public class FileHandelings
    {
        public static void SavePassword(PasswordDTO password)
        {
            if (File.Exists(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt") == true)
            {
                File.AppendAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt",
                      password.Website + Environment.NewLine
                    + Convert.ToBase64String(password.HashedPassword) + Environment.NewLine
                    + Convert.ToBase64String(password.Salt) + Environment.NewLine);
            }

        }


        public static void DeletePassword(string delPassword, string website)
        {
            List<PasswordDTO> passwordDTOs = new List<PasswordDTO>();

            string passwords = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt");
            string[] pasword = passwords.Split(Environment.NewLine);
            passwordDTOs.Add(new PasswordDTO
            {
                Website = pasword[0],
                HashedPassword = Convert.FromBase64String(pasword[1]),
                Salt = Convert.FromBase64String(pasword[2])
            });


            foreach (PasswordDTO passwordDTO in passwordDTOs)
            {
                var password = Hashing.HashPasswordWithSalt(Encoding.UTF8.GetBytes(delPassword), passwordDTO.Salt);
                if (password == passwordDTO.HashedPassword && website == passwordDTO.Website)
                {
                    passwordDTOs.Remove(passwordDTO);
                }
                else
                {
                    Console.WriteLine("Denne side og password exsistere ikke i din PasswordManager");
                    break;
                }
            }

            foreach (PasswordDTO item in passwordDTOs)
            {
                if (File.Exists(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt") == true)
                {
                    File.WriteAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt",
                          item.Website + Environment.NewLine
                        + Convert.ToBase64String(item.HashedPassword) + Environment.NewLine
                        + Convert.ToBase64String(item.Salt) + Environment.NewLine);
                }
            }

        }



        public static void EncryptFileSymmetric(EncryptedPacket encryptedPacket)
        {
            #region Symmetric
            //var aes = new EncryptDecrypt();
            //encryptedPacket.EncryptedSessionKey = aes.GenerateRandomNumber(32);
            //encryptedPacket.Iv = aes.GenerateRandomNumber(16);

            //var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt");

            //var encrypted = aes.Crypto(Encoding.UTF8.GetBytes(lines), encryptedPacket.EncryptedSessionKey, encryptedPacket.Iv, true);


            #endregion

            #region Asymmetric

            var rsaParams = new EncryptDecryptAsymetric();
            const string original = "Text to encrypt";

            rsaParams.AssignNewKey();
            var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt");

            var encryptedRsa = rsaParams.EncryptData(Encoding.UTF8.GetBytes(lines));
            #endregion


            SaveEncryptedFiles(encryptedRsa);
        }

        public static void DecryptFileSymmetric(EncryptedPacket encryptedPacket)
        {
            #region Symmetric
            //var aes = new EncryptDecrypt();

            //var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt");

            //var decrypted = aes.Crypto(Encoding.UTF8.GetBytes(lines), encryptedPacket.EncryptedSessionKey, encryptedPacket.Iv, false);

            //var decryptedfile = Encoding.UTF8.GetString(decrypted);
            //Console.WriteLine(decryptedfile);
            //Console.ReadLine();

            #endregion

            #region Asymmetric

            var rsaParams = new EncryptDecryptAsymetric();
            var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt");
            var decryptedRsaParams = rsaParams.DecryptData(Encoding.UTF8.GetBytes(lines));

            #endregion

            SaveDecryptedFiles(Encoding.UTF8.GetString(decryptedRsaParams));
        }




        public static void SaveEncryptedFiles(byte[] encryptDecrypt)
        {
                File.WriteAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt", Convert.ToBase64String(encryptDecrypt));
        }


        public static void SaveDecryptedFiles(string Decrypt)
        {
            File.WriteAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt",Decrypt );
        }


    }
}
