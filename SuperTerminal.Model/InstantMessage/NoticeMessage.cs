using System;
using System.Collections.Generic;
using System.Text;

namespace SuperTerminal.Model.InstantMessage
{
    public class NoticeMessage : IMessage
    {
        /// <summary>
        /// 标识，用户接收端区分不同消息
        /// </summary>
        public string Mark { get; set; }
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
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        public override string ToString()
        {
            return this.Content;
        }
    }
    public class NoticeMessageMark
    {
        public const string SuperTerminal = "SuperTerminal";
    }
}
