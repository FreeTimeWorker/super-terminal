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
        /// <summary>
        /// 时间范围
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <param name="min">Time Str</param>
        /// <param name="max">Time Str</param>
        public CheckByRange(string errorMsg, string min, string max)
        {
            ErrorMsg = errorMsg;
            Max = DateTime.Parse(min);
            Min = DateTime.Parse(max);
        }
    }
}
