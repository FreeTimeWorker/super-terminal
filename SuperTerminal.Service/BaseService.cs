using AutoMapper;
using SuperTerminal.Data.SqlSugarContent;
using SuperTerminal.MiddleWare;

namespace SuperTerminal.Service
{
    public abstract class BaseService
    {
        protected readonly IDbContext _dbContext;
        protected IHttpParameter _httpParameter;
        protected IMapper _mapper;
        public BaseService(IDbContext dbContext, IMapper mapper, IHttpParameter httpParameter)
        {
            _dbContext = dbContext;
            _httpParameter = httpParameter;
            _mapper = mapper;
        }
    }
}
