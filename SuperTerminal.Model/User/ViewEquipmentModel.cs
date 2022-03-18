using SuperTerminal.Const;
using SuperTerminal.Enum;
using SuperTerminal.FeildCheck;
using System.Runtime.InteropServices;

namespace SuperTerminal.Model.User
{
    public class ViewEquipmentModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [CheckUnique("用户名不能重复", "SysUser", "UserName")]
        [CheckByRegular("用户名不能为空", Rules.Requird, Rules.StringLength50)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [CheckByRegular("密码不能为空", Rules.Requird)]
        [CheckByRegular("密码必须大于5位", Rules.StringThanLength5, Rules.StringLength50)]
        public string PassWord { get; set; }
        /// <summary>
        /// 公共Ip
        /// </summary>
        public string PubIp { get; set; }
        /// <summary>
        /// 内网Ip
        /// </summary>
        public string PrivIp { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string OSDescription { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public OSPlatformEnum OSPlatform { get; set; }
        /// <summary>
        /// 系统架构
        /// </summary>
        public Architecture OSArchitecture { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string NickName { get; set; }
    }
}
