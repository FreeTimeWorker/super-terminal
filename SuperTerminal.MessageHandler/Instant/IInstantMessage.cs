using SuperTerminal.Model.InstantMessage;
using System.Threading.Tasks;

namespace SuperTerminal.MessageHandler.Instant
{
    /// <summary>
    /// 约束类型
    /// 为保证各功能之间的隔离，人为将消息分类，便于后期维护
    /// </summary>
    public interface IInstantMessage
    {
        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="noticeMessage"></param>
        /// <returns></returns>
        Task ReceiveNotice(NoticeMessage noticeMessage);
        /// <summary>
        /// 打开终端消息
        /// </summary>
        /// <param name="noticeMessage"></param>
        /// <returns></returns>
        Task ReceiveOpenTerminal(OpenTerminalMessage noticeMessage);
        /// <summary>
        /// 打开终端的情况下执行命令
        /// </summary>
        /// <param name="executeTerminalCommandMessage"></param>
        /// <returns></returns>
        Task ReceiveExecTerminalCmd(ExecuteTerminalCommandMessage executeTerminalCommandMessage);
        /// <summary>
        /// 接收关闭终端消息
        /// </summary>
        /// <param name="closeTerminalMessage"></param>
        /// <returns></returns>
        Task ReceiveCloseTerminal(CloseTerminalMessage closeTerminalMessage);
    }
}
