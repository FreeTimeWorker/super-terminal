using SuperTerminal.GlobalService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
            using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(HashAlgorithmName.MD5.Name))
            {
                var result = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(source));
                return Convert.ToBase64String(result);
            }
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
            using (Aes aes = Aes.Create())
            {
                var config = ServiceAgent.Provider.GetService<IConfiguration>();
                aes.IV = Convert.FromBase64String(config["Aes:IV"]);
                aes.Key = Convert.FromBase64String(config["Aes:Key"]);
                var Encryptor = aes.CreateEncryptor();
                //原字符utf8编码获取byte加密后转换成base64字符串
                var enc = Encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(source), 0, source.Length);
                return Convert.ToBase64String(enc);
            }
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
            using (Aes aes = Aes.Create())
            {
                var config = ServiceAgent.Provider.GetService<IConfiguration>();
                aes.IV = Convert.FromBase64String(config["Aes:IV"]);
                aes.Key = Convert.FromBase64String(config["Aes:Key"]);
                var Encryptor = aes.CreateDecryptor();
                //base64字符解码获取原byte[],解密后,通过UTF8编码还原
                var enc = Encryptor.TransformFinalBlock(Convert.FromBase64String(source), 0, source.Length);
                return Encoding.UTF8.GetString(enc);
            }
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rsa"></param>
        /// <returns></returns>
        public static string RSAEncrypt(this string source,RSA rsa)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            var enc = rsa.Encrypt(Encoding.UTF8.GetBytes(source), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(enc);
        }
        /// <summary>
        ///  RSA解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rsa"></param>
        /// <returns></returns>
        public static string RSADecrypt(this string source, RSA rsa)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            var dec = rsa.Decrypt(Convert.FromBase64String(source), RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(dec);
        }
    }
}
