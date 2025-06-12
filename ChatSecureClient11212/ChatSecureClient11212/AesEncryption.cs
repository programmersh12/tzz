using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ChatSecureClient11212
{
    public static class AesEncryption
    {
        private static readonly string KeyString = "thisisasecretkey123"; // 16 символов (128 бит)
        private static readonly string IVString = "thisisaninitvectr";    // 16 символов (128 бит)

        public static string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(KeyString);
            aes.IV = Encoding.UTF8.GetBytes(IVString);

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string encryptedText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(KeyString);
            aes.IV = Encoding.UTF8.GetBytes(IVString);

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(encryptedText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
