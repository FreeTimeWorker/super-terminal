using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using SuperTerminal.Data.Entitys;
using SuperTerminal.Data.SqlSugarContent;
using SuperTerminal.Filter;
using SuperTerminal.Model;
using SuperTerminal.Service.Interfaces;
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
        private readonly ITestService _testService;
        public ValidTestController(IDbContext DbContext, IMapper mapper, ITestService testService)
        {
            _DbContext = DbContext;
            _mapper = mapper;
            _testService = testService;
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
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<Page<ViewTestModel>> Page()
        {
            return _testService.Page();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<Page<ViewTestModel>> GetDataByCondition(string condition)
        {
            return _testService.GetDataByCondition(condition);
        }
    }
}
