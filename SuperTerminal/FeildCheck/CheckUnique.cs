namespace SuperTerminal.FeildCheck
{
    /// <summary>
    /// 验证唯一
    /// </summary>

    public class CheckUnique : FeildCheckAttribute
    {
        /// <summary>
        /// 所在数据表的表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string FeildName { get; set; }
        /// <summary>
        /// 唯一标识，一般是ID
        /// </summary>
        public string IdentityFeild { get; set; } = "Id";

        /// <summary>
        /// 提交时判断唯一
        /// </summary>
        /// <param name="tableName"></param>
        public CheckUnique(string errorMsg, string tableName, string feildName)
        {
            ErrorMsg = errorMsg;
            TableName = tableName;
            FeildName = feildName;
        }
    }
}
