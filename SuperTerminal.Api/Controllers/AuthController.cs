using Microsoft.AspNetCore.Mvc;
using SuperTerminal.Const;
using SuperTerminal.Filter;
using SuperTerminal.JWT;
using SuperTerminal.Model;
using SuperTerminal.Model.User;
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
        ///  获取token
        /// </summary>
        /// <param name="userLogin">passWord</param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<BoolModel<string>> GetToken(ViewUserLogin viewUserLogin)
        {
            var result=new BoolModel<string>();
            BoolModel<(int,int)> chekLogin = _userService.CheckLogin(viewUserLogin);
            if (chekLogin.Successed)
            {
                string token = _jwt.GetToken(new Dictionary<string, object>()
                {
                    { HttpItem.UserId,chekLogin.Data.Item1},
                    { HttpItem.UserType,chekLogin.Data.Item2}
                });
                result.Successed = true;
                result.Message = chekLogin.Message;
                result.Data = token;
                return result;
            }
            else
            {
                result.Message = chekLogin.Message;
            }
            return result;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="viewUserLogin"></param>
        /// <returns></returns>
        [Validate(typeof(ViewManagerModel))]
        [HttpPost]
        public ResponseModel<BoolModel> RegistManager(ViewManagerModel viewUserLogin)
        {
            return _userService.RegistManager(viewUserLogin);
        }

        /// <summary>
        /// 注册设备
        /// </summary>
        /// <param name="viewUserLogin"></param>
        /// <returns></returns>
        [Validate(typeof(ViewEquipmentModel))]
        [HttpPost]
        public ResponseModel<BoolModel<int>> RegistEquipment(ViewEquipmentModel viewUserLogin)
        {
            return _userService.RegistEquipment(viewUserLogin);
        }

        /// <summary>
        /// 检查本地token是否过期
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<int> GetUserId()
        {
            return int.Parse(HttpContext.Items[HttpItem.UserId].ToString());
        }


        /// <summary>
        /// 检查本地token是否过期
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<bool> Check()
        {
            if (int.Parse(HttpContext.Items[HttpItem.UserId].ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
