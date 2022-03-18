using SuperTerminal.Const;
using SuperTerminal.Enum;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace SuperTerminal.Utity
{
    public class OsHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OsHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }
        /// <summary>
        /// 系统类型
        /// </summary>
        /// <returns></returns>
        public OSPlatformEnum OSPlatformEnum
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return OSPlatformEnum.Windows;
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return OSPlatformEnum.Linux;
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return OSPlatformEnum.OSX;
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                {
                    return OSPlatformEnum.FreeBSD;
                }
                return OSPlatformEnum.Linux;
            }
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        /// <returns></returns>
        public string OSDescription => RuntimeInformation.OSDescription;
        /// <summary>
        /// 系统架构
        /// </summary>
        /// <returns></returns>
        public Architecture OSArchitecture => RuntimeInformation.OSArchitecture;
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        public string LocalIp
        {
            get
            {
                try
                {
                    System.Net.IPAddress[] addressList = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
                    string[] ips = addressList.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            .Select(address => address.ToString()).ToArray();
                    if (ips.Length == 1)
                    {
                        return ips.First();
                    }
                    return ips.Where(address => address != "127.0.0.1").FirstOrDefault() ?? ips.FirstOrDefault();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 获取公网IP
        /// </summary>
        /// <returns></returns>
        public string PubIP => GetIp();
        private string GetIp()
        {
            string html = GetHtml("https://www.ip.cn");
            string result = GetIPFromHtml(html);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            html = GetHtml("http://www.ip138.com/ips138.asp");
            result = GetIPFromHtml(html);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            html = GetHtml("http://www.net.cn/static/customercare/yourip.asp");
            result = GetIPFromHtml(html);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            result = GetHtml("https://icanhazip.com/");
            return result;
        }
        private string GetHtml(string url)
        {
            string pageHtml = string.Empty;
            try
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.74 Safari/537.36");
                pageHtml = client.GetStringAsync(url).Result;
            }
            catch
            {

            }
            return pageHtml;
        }
        public string GetIPFromHtml(string pageHtml)
        {
            //验证ipv4地址
            string ip = "";
            Match m = Regex.Match(pageHtml, Rules.IPv4);
            if (m.Success)
            {
                ip = m.Value;
            }
            if (!string.IsNullOrEmpty(ip))
            {
                return ip;
            }
            m = Regex.Match(pageHtml, Rules.IPv6);//ipv6
            if (m.Success)
            {
                ip = m.Value;
            }
            return ip;
        }
    }
}
