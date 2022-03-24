using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperTerminal.Model.User;
using SuperTerminal.Service.Interfaces;
using System.Collections.Generic;

namespace SuperTerminal.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IUserService _userService;
        public EquipmentController( IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<ViewEquipmentModel>> GetClients(string keyword)
        {
            return _userService.GetClients(keyword);
        }
    }
}
