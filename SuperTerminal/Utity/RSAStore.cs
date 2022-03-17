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
        ///  通过RSA.Create(),拿到的随机密钥 Base64编码，公钥私钥分别写入文件
        ///  公钥仅用于加密，私钥可用于解密
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="RSAKeyType">密钥类型，公钥私钥</param>
        /// <returns></returns>
        public static RSA GetRSAFromCustomFile(string filePath, RSAKeyType RSAKeyType)
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
        /// powershell --
        /// New-SelfSignedCertificate -Type Custom -Subject "CN=SuperTerminal" -FriendlyName "超级终端管理端证书" -KeyAlgorithm RSA -KeyLength 2048 -CertStoreLocation "Cert:\LocalMachine\My"
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
        /// <summary>
        /// 根据 openssl生成的密钥对创建RSA实例
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static RSA GetRSAFromPem(string filePath)
        {
            /*
             * 生成私钥：openssl genrsa -out privatekey.key 1024
             * 对应公钥：openssl rsa -in privatekey.key -pubout -out pubkey.key
             * **/
            try
            {
                RSA result = RSA.Create();
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string dt = sr.ReadToEnd();
                        result.ImportFromPem(dt);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
