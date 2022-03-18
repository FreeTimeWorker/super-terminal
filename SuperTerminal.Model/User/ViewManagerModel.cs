using SuperTerminal.Const;
using SuperTerminal.FeildCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Model
{
    public class ViewManagerModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [CheckUnique("用户名不能重复", "SysUser", "UserName")]
        [CheckByRegular("用户名必须大于5位", Rules.Requird, Rules.StringThanLength5)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [CheckByRegular("密码不能为空", Rules.Requird)]
        [CheckByRegular("密码必须大于5位", Rules.StringThanLength5,Rules.StringLength50)]
        public string Password { get; set; }
    }
}
