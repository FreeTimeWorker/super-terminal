using SuperTerminal.Model;
using SuperTerminal.Model.User;

namespace SuperTerminal.Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="viewUserLogin"></param>
        /// <returns></returns>
        BoolModel<(int, int)> CheckLogin(ViewUserLogin viewUserLogin);
        /// <summary>
        /// 管理员注册
        /// </summary>
        /// <param name="viewUserLogin"></param>
        /// <returns></returns>
        BoolModel RegistManager(ViewManagerModel viewUserLogin);
        /// <summary>
        /// 设备注册
        /// </summary>
        /// <param name="viewUserLogin"></param>
        /// <returns></returns>
        BoolModel<int> RegistEquipment(ViewEquipmentModel viewUserLogin);
    }
}
