using SuperTerminal.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Manager
{
    public class Common
    {
        public RSA GetRSA()
        {
           return  RSAStore.GetRSAFromX509(Enum.RSAKeyType.PubKey, "SuperTerminal", StoreLocation.CurrentUser)
                ?? RSAStore.GetRSAFromPem("SuperTerminal.key") ??
                RSAStore.GetRSAFromCustomFile("SuperTerminal.key", Enum.RSAKeyType.PubKey);
        }
    }
}
