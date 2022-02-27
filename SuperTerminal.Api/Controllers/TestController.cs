using Microsoft.AspNetCore.Mvc;
using SuperTerminal.Data.Entitys;
using SuperTerminal.Filter;
using System.Collections.Generic;
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
        [Validate(typeof(TestModel))]
        [HttpPost]
        public string DelDataCache(List<TestModel> models)
        {
            var result = "清理";
            return result;
        }
    }
}
