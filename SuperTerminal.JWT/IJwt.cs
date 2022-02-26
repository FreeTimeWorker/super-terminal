using System.Collections.Generic;

namespace SuperTerminal.JWT
{
    public interface IJwt
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="Claims">需要传递到jwt中的自定义信息</param>
        /// <returns></returns>
        string GetToken(IDictionary<string, object> Claims);
        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="Token">待验证的字符串</param>
        /// <param name="Claims">得到传入Claims信息</param>
        /// <returns></returns>
        bool ValidateToken(string Token, out Dictionary<string, object> Claims);
    }
}
