using System;
using System.Collections.Generic;
using System.Text;

namespace SuperTerminal.Model.InstantMessage
{
    /// <summary>
    /// 关闭终端
    /// </summary>
    public class CloseTerminalMessage:IMessage
    {
        /// <summary>
        /// 
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
            return "关闭终端";
        }
    }
}
