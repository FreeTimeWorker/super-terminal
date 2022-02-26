using System.Collections.Generic;

namespace SuperTerminal.JWT
{
    /// <summary>
    /// jwt配置
    /// </summary>
    internal class JwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        /// <summary>
        /// 签名公钥
        /// </summary>
        public string SigningPubKey { get; set; }
        /// <summary>
        /// 签名私钥
        /// </summary>
        public string SigningPriKey { get; set; }
        /// <summary>
        /// 加密公钥
        /// </summary>
        public string EncryptPubKey { get; set; }
        /// <summary>
        /// 加密私钥
        /// </summary>
        public string EncryptPriKey { get; set; }

        /// <summary>
        /// 生命周期
        /// </summary>
        public int Lifetime { get; set; }
        /// <summary>
        /// 续期时间
        /// </summary>
        public int RenewalTime { get; set; }
        /// <summary>
        /// 是否验证生命周期
        /// </summary>
        public bool ValidateLifetime { get; set; }
        /// <summary>
        /// 验证头字段
        /// </summary>
        public string HeadField { get; set; }
        /// <summary>
        /// 新Token的Head字段
        /// </summary>
        public string ReTokenHeadField { get; set; }
        /// <summary>
        /// jwt验证前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 忽略验证的url
        /// </summary>
        public List<string> IgnoreUrls { get; set; }
    }
}
