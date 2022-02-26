using Microsoft.EntityFrameworkCore;
using SuperTerminal.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace SuperTerminal.Data
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Comment("用户表")]
    public class SysUser : BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Comment("用户名"), MaxLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Comment("密码"), MaxLength(50)]
        public string PassWord { get; set; }
        /// <summary>
        /// 用户类型,用户区分终端设备和登录管理端
        /// </summary>
        [Comment("用户类型"), DefaultValue(0)]
        public int UserType { get; set; }
        /// <summary>
        /// 公共Ip
        /// </summary>
        [Comment("公网IP")]
        public string PubIp { get; set; }
        /// <summary>
        /// 内网Ip
        /// </summary>
        [Comment("内网IP")]
        public string PrivIp { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        [Comment("系统名称")]
        public string OSDescription { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        [Comment("系统类型")]
        public OSPlatformEnum OSPlatform { get; set; }
        /// <summary>
        /// 系统架构
        /// </summary>
        [Comment("系统架构")]
        public Architecture OSArchitecture { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        [Comment("别名")]
        public string NickName { get; set; }
    }
}
