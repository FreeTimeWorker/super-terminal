using Microsoft.AspNetCore.Mvc;
using SuperTerminal.Filter;
using System.Threading;

namespace SuperTerminal.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [DataCache(nameof(TestController))]
        //[DataCache("Test")]
        [HttpGet]
        public string GetData()
        {
            var result = "123";
            Thread.Sleep(3000);
            return result;
        }
        [ClearDataCache(nameof(TestController))]
        //[ClearDataCache("Test")]
        [HttpGet]
        public string DelDataCache()
        {
            var result = "清理";
            return result;
        }
    }
}
