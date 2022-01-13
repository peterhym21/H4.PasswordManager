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

    }
}
