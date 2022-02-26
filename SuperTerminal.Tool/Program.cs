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
        }
    }
}
