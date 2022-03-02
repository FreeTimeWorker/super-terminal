using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using SuperTerminal.Data.Entitys;
using SuperTerminal.Data.SqlSugarContent;
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
        private readonly IDbContext _DbContext;
        private readonly IMapper _mapper;
        public ValidTestController(IDbContext DbContext, IMapper mapper)
        {
            _DbContext = DbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// 测试列表
        /// </summary>
        /// <param name="testModels"></param>
        /// <returns></returns>
        [Validate(typeof(ViewTestModel))]
        [HttpPost]
        public ResponseModel<BoolModel> PostDataList(List<ViewTestModel> testModels)
        {
            var result = _DbContext.Storageable(_mapper.Map<List<TestModel>>(testModels)).ToStorage();
            var add = result.AsInsertable.ExecuteCommand();
            var update= result.AsUpdateable.ExecuteCommand();
            return new BoolModel() { Successed=true,Message="保存成功" };
        }
        /// <summary>
        /// 测试model
        /// </summary>
        /// <param name="testModel"></param>
        /// <returns></returns>
        [Validate(typeof(ViewTestModel))]
        [HttpPost]
        public ResponseModel<BoolModel> PostDataModel(ViewTestModel testModel)
        {
            var result = _DbContext.Storageable(_mapper.Map<TestModel>(testModel)).ToStorage();
            var add = result.AsInsertable.ExecuteCommand();
            var update = result.AsUpdateable.ExecuteCommand();
            return new BoolModel() { Successed = true, Message = "保存成功" };
        }
    }
}
