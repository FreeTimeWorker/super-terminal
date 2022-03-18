using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal
{
    public class BoolModel
    {
        public BoolModel()
        {

        }
        public BoolModel(bool successed, string message)
        {
            Successed = successed;
            Message = message;
        }
        public BoolModel(bool successed, string message, object Data)
        {
            Successed = successed;
            Message = message;
            this.Data = Data;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Successed { get; set; }
        private string _message { get; set; }
        /// <summary>
        /// 成功-成功提示，失败-失败提示
        /// </summary>
        public string Message
        {
            get
            {
                if (_message == null)
                {
                    return Successed ? "操作成功!" : "操作失败";
                }
                else
                {
                    return _message;
                }
            }
            set => _message = value;
        }
        /// <summary>
        /// 附带信息
        /// </summary>
        public dynamic Data { get; set; }
    }

    public class BoolModel<T>
    {

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Successed { get; set; }
        private string _message { get; set; }
        /// <summary>
        /// 成功-成功提示，失败-失败提示
        /// </summary>
        public string Message
        {
            get
            {
                if (_message == null)
                {
                    return Successed ? "操作成功!" : "操作失败";
                }
                else
                {
                    return _message;
                }
            }
            set => _message = value;
        }
        public BoolModel()
        {

        }
        public BoolModel(bool successed, string message)
        {
            Successed = successed;
            Message = message;
        }
        public BoolModel(bool successed, string message, T Data)
        {
            Successed = successed;
            Message = message;
            this.Data = Data;
        }
        /// <summary>
        /// 附带信息
        /// </summary>
        public T Data { get; set; }

        public static implicit operator BoolModel<T>(T data)
        {
            return new BoolModel<T> { Successed = true, Data = data };
        }
    }
}
