﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Service.Services
{
    public class Certificate
    {
        public static X509Certificate2 LoadCertificate(StoreLocation storeLocation, string certificateName)
        {
            X509Store store = new(storeLocation);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = store.Certificates;
            X509Certificate2 cert = certCollection.Cast<X509Certificate2>().FirstOrDefault(c => c.Subject == certificateName);
            if (cert == null)
            {
                Console.WriteLine("NO Certificate named " + certificateName + " was found in your certificate store");
            }

            store.Close();
            return cert;
        }

        public static string Encrypt(X509Certificate2 x509, string stringToEncrypt)
        {
            if (x509 == null || string.IsNullOrEmpty(stringToEncrypt))
                throw new Exception("A x509 certificate and string for encryption must be provided");

            RSA rsa = x509.GetRSAPublicKey();

            byte[] bytestoEncrypt = Encoding.ASCII.GetBytes(stringToEncrypt);
            byte[] encryptedBytes = rsa.Encrypt(bytestoEncrypt, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(X509Certificate2 x509, string stringTodecrypt)
        {
            if (x509 == null || string.IsNullOrEmpty(stringTodecrypt))
                throw new Exception("A x509 certificate and string for decryption must be provided");

            if (!x509.HasPrivateKey)
                throw new Exception("x509 certicate does not contain a private key for decryption");

            RSA rsa = x509.GetRSAPrivateKey();

            byte[] bytestodecrypt = Convert.FromBase64String(stringTodecrypt);
            byte[] plainbytes = rsa.Decrypt(bytestodecrypt, RSAEncryptionPadding.Pkcs1);
            ASCIIEncoding enc = new();
            return enc.GetString(plainbytes);
        }
    }
}
