namespace SuperTerminal.MiddleWare
{
    public interface IHttpParameter
    {
        int UserId { get; }
        /// <summary>
        /// 读取公共位置的  current
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 读取公共位置的pageSize
        /// </summary>
        int PageSize { get; }
    }
}
