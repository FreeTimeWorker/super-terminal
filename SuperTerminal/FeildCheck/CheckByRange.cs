using System;

namespace SuperTerminal.FeildCheck
{
    public class CheckByRange : FeildCheckAttribute
    {
        /// <summary>
        /// 包含
        /// </summary>
        public IComparable Min { get; set; }
        /// <summary>
        /// 不包含
        /// </summary>
        public IComparable Max { get; set; }
        public CheckByRange(string errorMsg, int min, int max)
        {
            ErrorMsg = errorMsg;
            Max = max;
            Min = min;
        }
        public CheckByRange(string errorMsg, decimal min, decimal max)
        {
            ErrorMsg = errorMsg;
            Max = max;
            Min = min;
        }
        public CheckByRange(string errorMsg, double min, double max)
        {
            ErrorMsg = errorMsg;
            Max = max;
            Min = min;
        }
        public CheckByRange(string errorMsg, float min, float max)
        {
            ErrorMsg = errorMsg;
            Max = max;
            Min = min;
        }
    }
}
