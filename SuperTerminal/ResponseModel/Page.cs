using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal
{
    public class Page<T> 
    {
        public Page()
        {
        }
        public Page(int totalRecords, IList<T> data, string message = "")
        {
            TotalRecords = totalRecords;
            Data = data;
            Message = message;
        }
        public IList<T> Data { get; set; }
        public int TotalRecords { get; set; }
        public string Message { get; set; }
        public int CurrentPageIndex { get; set; } = 1;
        public int TotalPage { get; set; } = 1;
    }
}
