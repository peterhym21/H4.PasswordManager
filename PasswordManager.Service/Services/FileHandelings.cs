using PasswordManager.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Service.Services
{
    public class FileHandelings
    {
        public static void SavePassword(PasswordDTO password)
        {
            if (File.Exists(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + password.Website + ".txt") == false)
            {
                File.WriteAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + password.Website + ".txt",
                      password.Website + Environment.NewLine
                    + Convert.ToBase64String(password.HashedPassword) + Environment.NewLine
                    + Convert.ToBase64String(password.Salt) + Environment.NewLine);
            }

        }


        public static void DeletePassword(string delPassword, string website)
        {
            List<PasswordDTO> passwordDTOs = new List<PasswordDTO>();

            string passwords = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + website + ".txt");
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
                if (password.SequenceEqual(passwordDTO.HashedPassword) && website == passwordDTO.Website)
                {
                    File.Delete(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + website + ".txt");
                    Console.WriteLine("Dit Password Er Nu slettet og din PasswordManger er Updateret");
                    break;
                }
                else
                {
                    Console.WriteLine("Denne side og password exsistere ikke i din PasswordManager");
                    Console.WriteLine("Eller du Har skrevet et forkert password");
                    break;
                }
            }

        }



        public static void ShowAllHashedPasswords()
        {


            List<PasswordDTO> passwordDTOs = new List<PasswordDTO>();
            DirectoryInfo filer = new DirectoryInfo(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\");
            foreach (FileInfo f in filer.GetFiles())
            {
                if (f.Extension == ".txt")
                {
                    string data = File.ReadAllText(f.FullName);
                    X509Certificate2 myCert = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");

                    string decrypted = Certificate.Decrypt(myCert, data);

                    string[] pasword = decrypted.Split(Environment.NewLine);
                    passwordDTOs.Add(new PasswordDTO
                    {
                        Website = pasword[0],
                        HashedPassword = Convert.FromBase64String(pasword[1]),
                        Salt = Convert.FromBase64String(pasword[2])
                    });
                }
            }

            foreach (var item in passwordDTOs)
            {
                Console.WriteLine("Website : " + item.Website);
                Console.WriteLine("Hashed Password : " + Convert.ToBase64String(item.HashedPassword));
                Console.WriteLine("Salt : " + Convert.ToBase64String(item.Salt));
                Console.WriteLine();
            }





        }



        public static void EncryptFile(string website)
        {
            #region Symmetric
            //var aes = new EncryptDecrypt();
            //encryptedPacket.EncryptedSessionKey = aes.GenerateRandomNumber(32);
            //encryptedPacket.Iv = aes.GenerateRandomNumber(16);

            //var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt");

            //var encrypted = aes.Crypto(Encoding.UTF8.GetBytes(lines), encryptedPacket.EncryptedSessionKey, encryptedPacket.Iv, true);


            #endregion

            #region Asymmetric

            //var rsaParams = new EncryptDecryptAsymetric();
            //const string original = "Text to encrypt";

            //rsaParams.AssignNewKey();
            //var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt");

            //var encryptedRsa = rsaParams.EncryptData(Encoding.UTF8.GetBytes(lines));

            #endregion


            X509Certificate2 myCert = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");

            var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + website + ".txt");
            if (lines != "")
            {
                string encrypted = Certificate.Encrypt(myCert, lines);
                SaveEncryptedFiles(encrypted, website);
            }
        }


        public static void DecryptFile(string website)
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

            //var rsaParams = new EncryptDecryptAsymetric();
            //var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\Passwords.txt");
            //var decryptedRsaParams = rsaParams.DecryptData(Encoding.UTF8.GetBytes(lines));

            #endregion

            X509Certificate2 myCert = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");

            var lines = File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + website + ".txt");
            if (lines != "")
            {
                string decrypted = Certificate.Decrypt(myCert, lines);
                SaveDecryptedFiles(decrypted, website);
            }



        }

        public static void SaveEncryptedFiles(string encryptDecrypt, string website)
        {
            if (File.ReadAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + website + ".txt") != "")
            {
                File.WriteAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + website + ".txt", encryptDecrypt);
            }

        }


        public static void SaveDecryptedFiles(string Decrypt, string website)
        {
            File.WriteAllText(@"C:\skole\eux\H4\SOFTWARETEST OG -SIKKERHED del 2\PasswordManager\SecretFiles\" + website + ".txt", Decrypt);
        }


    }
}
