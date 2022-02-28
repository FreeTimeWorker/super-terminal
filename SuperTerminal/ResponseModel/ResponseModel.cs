using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            Status = 200;
        }
        public int Status { get; set; }
        public string StatusMsg { get; set; }
        public object Data { get; set; }
    }
    public class ResponseModel<T>
    {
        public int Status { get; set; }
        public string StatusMsg { get; set; }
        /// <summary>
        /// 响应的数据
        /// </summary>
        public T Data { get; set; }

        public static implicit operator ResponseModel<T>(T data)
        {
            return new ResponseModel<T> { Status = 200, StatusMsg = "请求正确返回", Data = data };
        }
    }
}
