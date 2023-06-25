using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Learn_core_mvc.Core.Security
{
    public static class Secure
    {
        public static string EncodePassword(string pass, string salt) //encrypt password    
        {
            var passBytes = Encoding.Unicode.GetBytes(pass);
            var saltBytes = Encoding.Unicode.GetBytes(salt);
            var dst = new byte[saltBytes.Length + passBytes.Length];
            System.Buffer.BlockCopy(saltBytes, 0, dst, 0, passBytes.Length);
            System.Buffer.BlockCopy(passBytes, 0, dst, saltBytes.Length, passBytes.Length);

            using (var algorithm = HashAlgorithm.Create("SHA1"))
            {
                var inArray = algorithm.ComputeHash(dst);

                var base64 = Convert.ToBase64String(inArray);

                using (var md5 = new MD5CryptoServiceProvider())
                {
                    var originalBytes = ASCIIEncoding.Default.GetBytes(base64);
                    var encodedBytes = md5.ComputeHash(originalBytes);

                    //Convert encoded bytes back to a 'readable' string    
                    return BitConverter.ToString(encodedBytes);
                }
            }
        }

        public static string EncodePassword2(string password, string salt)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            if (String.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentNullException("salt");
            }

            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordAndSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, passwordAndSaltBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, passwordAndSaltBytes, saltBytes.Length, passwordBytes.Length);
            HashAlgorithm s = HashAlgorithm.Create("SHA1");

            byte[] sha1Bytes = s.ComputeHash(passwordAndSaltBytes);

            var sha1Hash = Convert.ToBase64String(sha1Bytes);

            byte[] saltBytes2 = Encoding.UTF8.GetBytes(salt);
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(sha1Hash, saltBytes2, 10000);
            byte[] hashBytes = pbkdf2.GetBytes(20);
            return Convert.ToBase64String(hashBytes);
        }

        public static string GenerateSalt()
        {
            byte[] byteArray = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(byteArray);
            return Convert.ToBase64String(byteArray);
        }
    }
}
