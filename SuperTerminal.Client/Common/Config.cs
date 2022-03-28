using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Client
{
    /// <summary>
    /// 保存到配置文件中的信息
    /// </summary>
    public class Config
    {
        public int Id { get; set; }
        public string SuperTerminal_UserName { get; set; }
        public string SuperTerminal_PassWord { get; set; }
        public string NickName { get; set; }
        public string Address { get; set; }
    }
}
