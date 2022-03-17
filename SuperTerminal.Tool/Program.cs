using System;
using System.Security.Cryptography;

namespace SuperTerminal.Tool
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("生成Ras非对称加密密钥对:");
            RSA rsa = RSA.Create();
            Console.WriteLine("公钥");
            Console.WriteLine(Convert.ToBase64String(rsa.ExportRSAPublicKey()));
            Console.WriteLine("密钥");
            Console.WriteLine(Convert.ToBase64String(rsa.ExportRSAPrivateKey()));
            /*
             * 生成自签名证书,用于windows服务器
             * New-SelfSignedCertificate -Type Custom -Subject "CN=SuperTerminal" -FriendlyName "超级终端管理端证书" -KeyAlgorithm RSA -KeyLength 2048 -CertStoreLocation "Cert:\LocalMachine\My"
             * 私钥用于服务器解密用户名密码
             * 公钥用于管理端注册登录时的安全认证和字段加密。
             * 管理端获取证书的路径->  windows证书服务中心 ->本地文件夹下特定文件 
             * 服务端证书获取路径 ->windows证书服务中心 ->项目文件夹下特定文件
             * 
             * **/

            //不同的平台通过工具生成不同的 密钥
        }
    }
}
