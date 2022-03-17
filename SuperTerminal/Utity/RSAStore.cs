using SuperTerminal.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Utity
{
    public static class RSAStore
    {
        /// <summary>
        ///  通过文件路径加载RSA
        ///  RSA通过Base64编码，写入文件
        ///  公钥仅用于加密，私钥可用于解密
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="RSAKeyType">密钥类型，公钥私钥</param>
        /// <returns></returns>
        public static RSA GetRSAFromFile(string filePath, RSAKeyType RSAKeyType)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        var base64Str = sr.ReadToEnd();
                        var result = RSA.Create();
                        switch (RSAKeyType)
                        {
                            case RSAKeyType.PubKey:
                                result.ImportRSAPublicKey(Convert.FromBase64String(base64Str), out int bytesread1);
                                break;
                            case RSAKeyType.PriKey:
                                result.ImportRSAPrivateKey(Convert.FromBase64String(base64Str), out int bytesread2);
                                break;
                            default:
                                break;
                        }
                        return result;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 在windows的证书中心读取 
        /// 公钥仅用于加密，私钥可用于解密
        /// </summary>
        /// <param name="RSAKeyType"></param>
        /// <param name="storeLocation"></param>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        public static RSA GetRSAFromX509(RSAKeyType RSAKeyType,string subjectName, StoreLocation storeLocation = StoreLocation.CurrentUser)
        {
            X509Store store = new X509Store(storeLocation);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectName, subjectName, false);
                if (signingCert.Count == 0)
                {
                    return null;
                }
                switch (RSAKeyType)
                {
                    case RSAKeyType.PubKey:
                        return signingCert[0].GetRSAPublicKey();
                    case RSAKeyType.PriKey:
                        return signingCert[0].GetRSAPrivateKey();
                    default:
                        return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                store.Close();
            }
        }
    }
}
