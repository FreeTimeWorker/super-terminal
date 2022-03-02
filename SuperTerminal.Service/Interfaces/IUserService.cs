using SuperTerminal.Model;

namespace SuperTerminal.Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="viewUserLogin"></param>
        /// <returns></returns>
        BoolModel CheckLogin(ViewUserLogin viewUserLogin);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="viewUserLogin"></param>
        /// <returns></returns>
        BoolModel Regist(ViewUserLogin viewUserLogin);
    }
}
