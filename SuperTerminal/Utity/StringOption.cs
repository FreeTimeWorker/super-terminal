using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperTerminal.GlobalService;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SuperTerminal.Utity
{
    public static class StringOption
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">需要加密的字符串</param>
        /// <returns>密文</returns>
        public static string MD5(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            using HashAlgorithm hashAlgorithm = HashAlgorithm.Create(HashAlgorithmName.MD5.Name);
            byte[] result = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(source));
            return Convert.ToBase64String(result);
        }
        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string AesEncrypt(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            using Aes aes = Aes.Create();
            IConfiguration config = ServiceAgent.Provider.GetService<IConfiguration>();
            aes.IV = Convert.FromBase64String(config["Aes:IV"]);
            aes.Key = Convert.FromBase64String(config["Aes:Key"]);
            ICryptoTransform Encryptor = aes.CreateEncryptor();
            //原字符utf8编码获取byte加密后转换成base64字符串
            byte[] enc = Encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(source), 0, source.Length);
            return Convert.ToBase64String(enc);
        }
        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string AesDecrypt(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            using Aes aes = Aes.Create();
            IConfiguration config = ServiceAgent.Provider.GetService<IConfiguration>();
            aes.IV = Convert.FromBase64String(config["Aes:IV"]);
            aes.Key = Convert.FromBase64String(config["Aes:Key"]);
            ICryptoTransform Encryptor = aes.CreateDecryptor();
            //base64字符解码获取原byte[],解密后,通过UTF8编码还原
            byte[] enc = Encryptor.TransformFinalBlock(Convert.FromBase64String(source), 0, source.Length);
            return Encoding.UTF8.GetString(enc);
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rsa"></param>
        /// <returns></returns>
        public static string RSAEncrypt(this string source, RSA rsa)
        {
            try
            {
                if (string.IsNullOrEmpty(source))
                {
                    return string.Empty;
                }
                byte[] enc = rsa.Encrypt(Encoding.UTF8.GetBytes(source), RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(enc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "-1";
            }
        }
        /// <summary>
        ///  RSA解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rsa"></param>
        /// <returns></returns>
        public static string RSADecrypt(this string source, RSA rsa)
        {
            try
            {
                if (string.IsNullOrEmpty(source))
                {
                    return string.Empty;
                }
                byte[] dec = rsa.Decrypt(Convert.FromBase64String(source), RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(dec);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "-1";
            }
        }


    }
}
