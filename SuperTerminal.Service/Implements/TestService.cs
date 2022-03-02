using AutoMapper;
using SuperTerminal.Data.Entitys;
using SuperTerminal.Data.SqlSugarContent;
using SuperTerminal.MiddleWare;
using SuperTerminal.Model;
using SuperTerminal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Service.Implements
{
    public class TestService : BaseService, ITestService
    {
        public TestService(IDbContext dbContext, IMapper mapper, IHttpParameter httpParameter) : base(dbContext, mapper, httpParameter)
        {
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <returns></returns>
        public Page<ViewTestModel> Page()
        {
            return _dbContext.Queryable<TestModel>().Select<ViewTestModel>().ToPage(_httpParameter);
        }
    }
}
