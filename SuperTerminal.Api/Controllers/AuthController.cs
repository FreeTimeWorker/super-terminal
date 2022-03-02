using Microsoft.AspNetCore.Mvc;
using SuperTerminal.Const;
using SuperTerminal.JWT;
using SuperTerminal.Model;
using SuperTerminal.Service.Interfaces;
using System.Collections.Generic;

namespace SuperTerminal.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwt _jwt;
        public AuthController(IJwt jwt, IUserService userService)
        {
            _userService = userService;
            _jwt = jwt;
        }
        /// <summary>
        ///  登录
        /// </summary>
        /// <param name="userLogin">passWord</param>
        /// <returns></returns>
        [HttpGet]
        public BoolModel GetToken(ViewUserLogin viewUserLogin)
        {
            var result = _userService.CheckLogin(viewUserLogin);
            if (result.Successed) 
            {
                string token = _jwt.GetToken(new Dictionary<string, object>()
                {
                    { HttpItem.UserId,result.Data}
                });
                result.Data = token;
                return result;
            }
            return result;
        }
    }
}
