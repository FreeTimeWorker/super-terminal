using SuperTerminal.Model;

namespace SuperTerminal.Service.Interfaces
{
    public interface IUserService
    {
        BoolModel CheckLogin(ViewUserLogin viewUserLogin);
    }
}
