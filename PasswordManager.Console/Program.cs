using PasswordManager.Service.Services;
using System;
using System.Text;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {


        }



        static void EncryptMessageSymetric()
        {
            var aes = new EncryptDecrypt();
            var key = aes.GenerateRandomNumber(32);
            var iv = aes.GenerateRandomNumber(16);
            const string original = "Fuck Your Life. BING BONG!!!!!";

            var encrypted = aes.Crypto(Encoding.UTF8.GetBytes(original), key, iv, true);
            //var decrypted = aes.Crypto(encrypted, key, iv, false);

            //var decryptedMessage = Encoding.UTF8.GetString(decrypted);


            Console.WriteLine("Key = " + Convert.ToBase64String(key));
            Console.WriteLine("IVector = " + Convert.ToBase64String(iv));
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
            //Console.WriteLine("Decrypted Text = " + decryptedMessage);

            Console.ReadLine();
        }

    }
}
