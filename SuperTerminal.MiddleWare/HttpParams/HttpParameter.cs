using Microsoft.AspNetCore.Http;
using SuperTerminal.Const;
using System.Collections.Concurrent;

namespace SuperTerminal.MiddleWare
{
    public class HttpParameter : IHttpParameter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpParameter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Items.ContainsKey(HttpItem.UserId))
                {
                    return int.Parse(_httpContextAccessor.HttpContext.Items[HttpItem.UserId].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public int PageIndex
        {
            get
            {
                int page = 1;
                if (_httpContextAccessor.HttpContext.Request.QueryString.HasValue)
                {
                    string[] paramstrs = _httpContextAccessor.HttpContext.Request.QueryString.Value.TrimStart('?').Split('&');
                    if (paramstrs.Length > 0)
                    {
                        ConcurrentDictionary<string, string> prms = new();
                        foreach (string item in paramstrs)
                        {
                            if (!string.IsNullOrEmpty(item) && item.IndexOf('=') > 0)
                            {
                                string[] param = item.Split('=');
                                prms.AddOrUpdate(param[0], param[1], (oldkey, oldvalue) => param[1]);
                            }
                        }
                        if (prms.ContainsKey("current"))
                        {
                            int.TryParse(prms["current"], out page);
                            return page;
                        }
                    }
                }
                if (_httpContextAccessor.HttpContext.Request.ContentType == "application/json")
                {
                    if (_httpContextAccessor.HttpContext.Items.ContainsKey("current"))
                    {
                        int.TryParse(_httpContextAccessor.HttpContext.Items["current"].ToString(), out page);
                    }
                }
                if (_httpContextAccessor.HttpContext.Request.ContentType != null && _httpContextAccessor.HttpContext.Request.ContentType.Contains("application/x-www-form-urlencoded"))
                {
                    if (_httpContextAccessor.HttpContext.Request.Form.Keys.Contains("current"))
                    {
                        int.TryParse(_httpContextAccessor.HttpContext.Request.Form["current"], out page);
                    }
                }
                if (page < 1)
                {
                    page = 1;
                }
                return page;
            }
        }

        public int PageSize
        {
            get
            {
                int limit = 20;
                if (_httpContextAccessor.HttpContext.Request.QueryString.HasValue)
                {
                    string[] paramstrs = _httpContextAccessor.HttpContext.Request.QueryString.Value.TrimStart('?').Split('&');
                    if (paramstrs.Length > 0)
                    {
                        ConcurrentDictionary<string, string> prms = new();
                        foreach (string item in paramstrs)
                        {
                            if (!string.IsNullOrEmpty(item) && item.IndexOf('=') > 0)
                            {
                                string[] param = item.Split('=');
                                prms.AddOrUpdate(param[0], param[1], (oldkey, oldvalue) => param[1]);
                            }
                        }
                        if (prms.ContainsKey("pageSize"))
                        {
                            int.TryParse(prms["pageSize"], out limit);
                            return limit;
                        }
                    }
                }
                if (_httpContextAccessor.HttpContext.Request.ContentType == "application/json")
                {
                    if (_httpContextAccessor.HttpContext.Items.ContainsKey("pageSize"))
                    {
                        int.TryParse(_httpContextAccessor.HttpContext.Items["pageSize"].ToString(), out limit);
                    }
                }
                if (_httpContextAccessor.HttpContext.Request.ContentType != null && _httpContextAccessor.HttpContext.Request.ContentType.Contains("application/x-www-form-urlencoded"))
                {
                    if (_httpContextAccessor.HttpContext.Request.Form.Keys.Contains("pageSize"))
                    {
                        int.TryParse(_httpContextAccessor.HttpContext.Request.Form["pageSize"], out limit);
                    }
                }
                return limit;
            }
        }
    }
}
