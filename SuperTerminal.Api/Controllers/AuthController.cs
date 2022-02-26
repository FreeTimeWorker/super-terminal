using Microsoft.AspNetCore.Mvc;
using SuperTerminal.JWT;

namespace SuperTerminal.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwt _jwt;
        public AuthController(IJwt jwt)
        {
            _jwt = jwt;
        }
        /// <summary>
        ///  登录
        /// </summary>
        /// <param name="userLogin">passWord</param>
        /// <returns></returns>
        [HttpGet]

        public string GetToken()
        {
            return "456";
        }
    }
}
