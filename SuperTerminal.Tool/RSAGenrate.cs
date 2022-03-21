using System;
using System.Security.Cryptography;

namespace SuperTerminal.Tool
{
    internal class RSAGenrate
    {
        public static void Genreate()
        {
            Console.WriteLine("生成Ras非对称加密密钥对-用于设备认证");
            RSA rsa = RSA.Create();
            Console.WriteLine("公钥");
            Console.WriteLine(Convert.ToBase64String(rsa.ExportRSAPublicKey()));
            Console.WriteLine("密钥");
            Console.WriteLine(Convert.ToBase64String(rsa.ExportRSAPrivateKey()));
            Console.WriteLine("公钥需放到SuperTerminal.key文件置于Server端根目录下,私钥需放到SuperTerminal.pem置于管理端根目录下,，非特殊情况不建议用这种方式");
            Console.WriteLine("windows下使用Win11.ps1生成密钥对,linux使用 linux.sh生成密钥对，linux的密钥保存位置对于上述方式相同" +
                "windows下的生成结束后，如果是在服务器执行的，只需将公钥置于管理端根目录下，如果是在个人电脑执行，" +
                "修改Win11.ps1中的脚本，导出私钥后，在windows服务器上将私钥置于LocalMachine\\My节点下,cer文件需要安装到管理端所在计算机上的 currentUser/My 下");
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
