using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.FeildCheck
{
    public class CheckByRange: FeildCheckAttribute
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 包含
        /// </summary>
        public IComparable Min { get; set; }
        /// <summary>
        /// 不包含
        /// </summary>
        public IComparable Max { get; set; }
        public CheckByRange(string errorMsg,int min, int max)
        {
            this.ErrorMsg = errorMsg;
            this.Max = max;
            this.Min = min;
        }
        public CheckByRange(string errorMsg,decimal min, decimal max)
        {
            this.ErrorMsg = errorMsg;
            this.Max = max;
            this.Min = min;
        }
        public CheckByRange(string errorMsg,double min, double max)
        {
            this.ErrorMsg = errorMsg;
            this.Max = max;
            this.Min = min;
        }
        public CheckByRange(string errorMsg, float min, float max)
        {
            this.ErrorMsg = errorMsg;
            this.Max = max;
            this.Min = min;
        }
    }
}
