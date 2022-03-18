namespace SuperTerminal.FeildCheck
{
    /// <summary>
    /// 通过正则表达式验证
    /// </summary>
    public class CheckByRegular : FeildCheckAttribute
    {
        /// <summary>
        /// 规则
        /// </summary>
        public string[] Rules { get; set; }
        /// <summary>
        /// 规则的验证方式，true表示所有的规则有一条通过检测就通过，否则不通过
        /// </summary>
        public bool Or { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <param name="or">true满足一个条件就算通过</param>
        /// <param name="rules"></param>
        public CheckByRegular(string errorMsg, params string[] rules)
        {
            ErrorMsg = errorMsg;
            Rules = rules;
        }
    }
}
