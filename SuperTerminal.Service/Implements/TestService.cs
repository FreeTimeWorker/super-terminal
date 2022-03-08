using SqlSugar;
using SuperTerminal.Data.Entitys;
using SuperTerminal.MiddleWare;
using SuperTerminal.Model;
using SuperTerminal.Service.Interfaces;


namespace SuperTerminal.Service.Implements
{
    public class TestService : ITestService
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        private readonly IHttpParameter _httpParameter;
        public TestService(ISqlSugarClient sqlSugarClient, IHttpParameter httpParameter)
        {
            this._sqlSugarClient = sqlSugarClient;
            this._httpParameter = httpParameter;
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <returns></returns>
        public Page<ViewTestModel> Page()
        {
            int totalNumber = 0;
            int totalPage = 0;
            var result = new Page<ViewTestModel>
            {
                Data = _sqlSugarClient.Queryable<TestModel>().Select<ViewTestModel>().ToPageList(_httpParameter.PageIndex, _httpParameter.PageSize, ref totalNumber, ref totalPage),
                Message = $"当前用户Id:{_httpParameter.UserId.ToString()}",
                TotalRecords = totalNumber,
                CurrentPageIndex = _httpParameter.PageIndex,
                TotalPage = totalPage
            };
            return result;
        }
    }
}
