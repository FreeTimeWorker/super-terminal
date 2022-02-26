namespace SuperTerminal.Const
{
    public class CacheSetting
    {
        /// <summary>
        /// Token过期时间
        /// </summary>
        public const int TimeOut = 60 * 24;//分钟  一天
        /// <summary>
        /// 数据缓存过期时间
        /// </summary>
        public const int DataCacheTimeOut = 3;//分钟
        /// <summary>
        /// 登录重试次数
        /// </summary>
        public const int LoginTime = 3;
    }
}