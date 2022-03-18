using Microsoft.AspNetCore.Mvc;
using SuperTerminal.Filter;
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
            string result = "123";
            Thread.Sleep(3000);
            return result;
        }
        [ClearDataCache(nameof(TestCacheController))]
        [HttpGet]
        public string DelDataCache()
        {
            string result = "清理";
            return result;
        }
    }
}
