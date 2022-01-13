﻿using PasswordManager.Service.DTO;
using PasswordManager.Service.Services;
using System;
using System.Text;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Menu menu = new Menu(new List<string>() { "Tilføj Nyt Password", "Se Alle Hashed Passwords", "Slet Password","Afslut" });
                menu.Select();
                switch (menu.Result())
                {
                    case "Tilføj Nyt Password":
                        Console.Clear();
                        NewPassword();
                        break;
                    case "Se Alle Hashed Passwords":
                        Console.Clear();

                        break;
                    case "Slet Password":
                        Console.Clear();
                        DeletePassword();
                        break;
                    case "Afslut":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            } while (true);

        }


        static void NewPassword()
        {
            PasswordDTO password = new PasswordDTO();
            Console.WriteLine("Tilføj Nyt Password");
            Console.WriteLine("Indtast Siden Passwordet hører til: ");
            password.Website = Console.ReadLine();
            Console.WriteLine("Indtast Dit Nye Password: ");
            string newPas = Console.ReadLine();

            password.Salt = Hashing.GenerateSalt();

            password.HashedPassword = Hashing.HashPasswordWithSalt(Encoding.UTF8.GetBytes(newPas),password.Salt);

            FileHandelings.SavePassword(password);

            Console.WriteLine("Dit Password Er Nu Hashet og gemt i din PasswordManager");
            Console.ReadLine();

        }


        static void DeletePassword()
        {

            Console.WriteLine("Slet et exsisterende Password");
            Console.WriteLine("Indtast Siden Passwordet hører til: ");
            string website = Console.ReadLine();
            Console.WriteLine("Indtast Siden Passwordet hører til: ");
            string delPas = Console.ReadLine();

            FileHandelings.DeletePassword(delPas, website);

            Console.WriteLine("Dit Password Er Nu slettet og din PasswordManger er Updateret");
            Console.ReadLine();

        }




    }
}
