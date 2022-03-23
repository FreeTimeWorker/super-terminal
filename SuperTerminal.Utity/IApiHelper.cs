using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Utity
{
    public interface IApiHelper
    {
        /// <summary>
        /// Domain
        /// </summary>
        public string Domain { get; }
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public string GetToken();
        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T Post<T>(string url, object obj);
        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public T Get<T>(string url);
    }
}
