using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperTerminal.Client
{
    /// <summary>
    /// 必须单例启动
    /// </summary>
    public class LogServer:IDisposable
    {
        /// <summary>
        /// 指示当前类是否已经被销毁
        /// </summary>
        private bool _destroy = false;
        /// <summary>
        /// 日志队列
        /// </summary>
        private ConcurrentQueue<string> _logs = new ConcurrentQueue<string>();
        /// <summary>
        /// 日志保存事件
        /// </summary>
        private event Action<string> SaveLog_Event;

        public LogServer()
        {
            SaveLog_Event += Log_Event_File;
            SaveLog_Event += Log_Event_Console;
            Task.Factory.StartNew(InvokeLogWrite);
        }
        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="obj"></param>
        private void Log_Event_File(string obj)
        {
            string filename = Path.Combine(AppContext.BaseDirectory, string.Concat(DateTime.Now.ToString("yyyy-MM-dd"), "-log.txt"));
            using (FileStream fs = new FileStream(filename, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(obj);
                }
            }
        }
        /// <summary>
        /// 控制台打印
        /// </summary>
        /// <param name="obj"></param>
        private void Log_Event_Console(string obj)
        {
            Console.WriteLine(obj);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        public void Write(string msg)
        {
            this._logs.Enqueue($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ssss")}] {msg}");
        }
        /// <summary>
        /// 在新线程中执行
        /// </summary>
        private void InvokeLogWrite()
        {
            while (_destroy == false)
            {
                if (_logs != null && _logs.Count > 0)
                {
                    if (SaveLog_Event != null)
                    {
                        if (_logs.TryDequeue(out string item))
                        {
                            SaveLog_Event.Invoke(item);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _destroy = true;
            while (_logs != null && _logs.Count > 0)
            {
                if (_logs.TryDequeue(out string item))
                {
                    SaveLog_Event.Invoke(item);
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}
