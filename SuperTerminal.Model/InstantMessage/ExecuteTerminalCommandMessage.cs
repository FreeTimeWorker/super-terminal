using System;
using System.Collections.Generic;
using System.Text;

namespace SuperTerminal.Model.InstantMessage
{
    /// <summary>
    /// 在打开的终端中执行命令
    /// </summary>
    public class ExecuteTerminalCommandMessage:IMessage
    {
        /// <summary>
        /// 执行的命令
        /// </summary>
        public string Content { get; set; }
        public int Sender { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public string SenderName { get; set; }
        /// <summary>
        /// 接收消息的用户
        /// </summary>
        public int Receiver { get; set; }
        /// <summary>
        /// 是否需要回复
        /// </summary>
        public bool NeedReply { get; set; } = false;

        public override string ToString()
        {
            return this.Content;
        }
    }
}
