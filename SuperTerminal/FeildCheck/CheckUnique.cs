using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.FeildCheck
{
    /// <summary>
    /// 验证唯一
    /// </summary>
    
    public class CheckUnique: FeildCheckAttribute
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 所在数据表的表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 唯一标识，一般是ID
        /// </summary>
        public string IdentityFeild { get; set; } = "Id";

        /// <summary>
        /// 提交时判断唯一
        /// </summary>
        /// <param name="tableName"></param>
        public CheckUnique(string errorMsg,string tableName)
        {
            ErrorMsg = errorMsg;
            TableName = tableName;
        }
    }
}
