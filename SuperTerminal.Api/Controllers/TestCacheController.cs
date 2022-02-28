using Microsoft.AspNetCore.Mvc;
using SuperTerminal.Data.Entitys;
using SuperTerminal.Filter;
using SuperTerminal.Model;
using System.Collections.Generic;
using System.Threading;

namespace SuperTerminal.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestCacheController : ControllerBase
    {
        [DataCache(nameof(TestCacheController))]
        [HttpGet]
        public string GetData()
        {
            var result = "123";
            Thread.Sleep(3000);
            return result;
        }
        [ClearDataCache(nameof(TestCacheController))]
        [HttpGet]
        public string DelDataCache()
        {
            var result = "清理";
            return result;
        }
    }
}
