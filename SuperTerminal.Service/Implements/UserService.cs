using AutoMapper;
using SuperTerminal.Data;
using SuperTerminal.Data.SqlSugarContent;
using SuperTerminal.MiddleWare;
using SuperTerminal.Model;
using SuperTerminal.Service.Interfaces;
using SuperTerminal.Utity;

namespace SuperTerminal.Service.Implements
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IDbContext dbContext, IMapper mapper, IHttpParameter httpParameter) : base(dbContext, mapper, httpParameter)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BoolModel CheckLogin(ViewUserLogin viewUserLogin)
        {
            var entity = _dbContext.Queryable<SysUser>().Where(o => o.UserName == viewUserLogin.UserName).First();
            if (entity == null)
            {
                return new BoolModel(false,"用户名不存在");
            }
            if (!viewUserLogin.Password.MD5().Equals(entity.PassWord))
            {
                return new BoolModel(false, "密码错误");
            }
            return new BoolModel(true, "成功",entity.Id);
        }

    }
}
