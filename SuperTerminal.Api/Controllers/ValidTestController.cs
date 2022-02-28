using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using SuperTerminal.Data.Entitys;
using SuperTerminal.Filter;
using SuperTerminal.Model;
using System.Collections.Generic;
using System.Threading;

namespace SuperTerminal.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ValidTestController : ControllerBase
    {
        private readonly ISqlSugarClient _DbContext;
        private readonly IMapper _mapper;
        public ValidTestController(ISqlSugarClient DbContext, IMapper mapper)
        {
            _DbContext = DbContext;
            _mapper = mapper;
        }
        [Validate(typeof(ViewTestModel))]
        [HttpPost]
        public ResponseModel<BoolModel> PostData(List<ViewTestModel> testModels)
        {
            var result = _DbContext.Insertable(_mapper.Map<List<TestModel>>(testModels)).ExecuteCommand();
            return new BoolModel() { Successed=true,Message="保存成功" };
        }
    }
}
