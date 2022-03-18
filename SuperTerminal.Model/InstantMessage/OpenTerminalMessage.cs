using System.Text;

namespace SuperTerminal.Model.InstantMessage
{
    public class OpenTerminalMessage : IMessage
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public int Sender { get; set; }
        /// <summary>
        /// 
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
        /// 终端名称/路径，为空，打开所在系统的默认终端
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 标准输出编码
        /// </summary>
        public Encoding StandardOutputEncoding { get; set; } = null;
        /// <summary>
        /// 标准输入编码
        /// </summary>
        public Encoding StandardInputEncoding { get; set; } = null;
        /// <summary>
        /// 标准错误编码
        /// </summary>
        public Encoding StandardErrorEncoding { get; set; } = null;

        public override string ToString()
        {
            return $"打开终端,终端路径:{(string.IsNullOrEmpty(Content) ? "默认" : Content)}" +
                   $",标准输入编码:{StandardOutputEncoding?.EncodingName}" +
                   $",标准输出编码:{StandardInputEncoding?.EncodingName}" +
                   $",标准错误编码:{StandardErrorEncoding?.EncodingName}";
        }
    }
}
