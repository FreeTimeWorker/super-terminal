using SuperTerminal.Model;
namespace SuperTerminal.Service.Interfaces
{
    public interface ITestService
    {
        Page<ViewTestModel> Page();
        Page<ViewTestModel> GetDataByCondition(string condition);
    }
}
