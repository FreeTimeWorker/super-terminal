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
            StatusCode = 0;
        }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }
    }
}
