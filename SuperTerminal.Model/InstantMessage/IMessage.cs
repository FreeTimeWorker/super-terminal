namespace SuperTerminal.Model.InstantMessage
{
    public interface IMessage
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
        public bool NeedReply { get; set; }


        public string ToString();
    }
}
