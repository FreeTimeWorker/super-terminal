using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Client
{
    public class Codebook
    {
        /// <summary>
        /// 生成一个512位的密码本
        /// </summary>
        /// <returns></returns>
        public void GenratePassFile()
        {
            Random r = new Random();
            byte[] bt = new byte[512];
            for (int i = 0; i < 512; i++)
            {
                bt[i] = (byte)r.Next(0, 255);
            }
            if (!File.Exists("pass.dll"))
            {
                using (FileStream fs = new FileStream("pass.dll", FileMode.Create))
                {
                    fs.Write(bt, 0, bt.Length);
                }
            }
        }
        /// <summary>
        /// 拿到IV和key
        /// </summary>
        /// <returns></returns>
        public(string, string) GetIVandKey()
        {
            var buffer = new byte[512];
            Span<byte> bt = new Span<byte>(buffer);
            using (FileStream fs = new FileStream("pass.dll", FileMode.Open))
            {
                fs.Read(bt);
            }
            //IV 16 Key :32
            byte[] iv = new byte[16];
            byte[] key = new byte[32];
            bt.Slice(Convert.ToInt32(bt[0]), 16).CopyTo(iv);
            bt.Slice(Convert.ToInt32(bt[1]), 32).CopyTo(key);
            return (Convert.ToBase64String(iv), Convert.ToBase64String(key));
        }
    }
}
