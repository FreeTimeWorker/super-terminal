using AutoMapper;
using SuperTerminal.Data;
using SuperTerminal.Data.SqlSugarContent;
using SuperTerminal.MiddleWare;
using SuperTerminal.Model;
using SuperTerminal.Model.User;
using SuperTerminal.Service.Interfaces;
using SuperTerminal.Utity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using SqlSugar;
using SuperTerminal.MessageHandler.Instant;

namespace SuperTerminal.Service.Implements
{
    public class UserService : BaseService, IUserService
    {
        private readonly OsHelper _osHelper;
        public UserService(IDbContext dbContext, IMapper mapper, IHttpParameter httpParameter, OsHelper osHelper) : base(dbContext, mapper, httpParameter)
        {
            _osHelper = osHelper;
        }
        public Page<SysUser> GetPage()
        {
            return _dbContext.Queryable<SysUser>().ToPage(_httpParameter);
        }
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <returns></returns>
        public BoolModel<(int,int)> CheckLogin(ViewUserLogin viewUserLogin)
        {
            //如果是管理员登录 解密数据
            if (viewUserLogin.IsManager)
            {
                RSA rsa = GetRsa();
                if (rsa == null)
                {
                    return new BoolModel<(int,int)>(false, "服务端需要安装证书",(0,0));
                }
                viewUserLogin.UserName = viewUserLogin.UserName.RSADecrypt(rsa);
                viewUserLogin.Password = viewUserLogin.Password.RSADecrypt(rsa);
                if (viewUserLogin.UserName == "-1" || viewUserLogin.Password == "-1")
                {
                    return new BoolModel<(int,int)>(false,
                        @$"数据校验失败,请在本地安装证书后重试,
                    服务端为Windows    证书安装位置为:cert;CurrentUser/My
                    服务端为非windows  请将 SuperTerminal.Pem文件置于根目录 公钥
                    ",(0,0));
                }
            }
            SysUser entity = _dbContext.Queryable<SysUser>().Where(o => o.UserName == viewUserLogin.UserName).First();
            if (entity == null)
            {
                return new BoolModel<(int, int)>(false, "用户名不存在");
            }
            if (!viewUserLogin.Password.Equals(entity.PassWord))
            {
                return new BoolModel<(int, int)>(false, "密码错误");
            }
            return new BoolModel<(int, int)>(true, "成功", (entity.Id, entity.UserType));
        }
        /// <summary>
        /// 注册管理员
        /// </summary>
        /// <param name="viewUserLogin"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public BoolModel RegistManager(ViewManagerModel viewUserLogin)
        {
            /*
             * 1,获取本机私钥
             * 2,解密传递过来的用户名和密码
             * 3,注册
             * **/
            RSA rsa = GetRsa();
            if (rsa == null)
            {
                return new BoolModel(false, "服务端需要安装证书");
            }
            viewUserLogin.UserName = viewUserLogin.UserName.RSADecrypt(rsa);
            viewUserLogin.Password = viewUserLogin.Password.RSADecrypt(rsa);
            if (viewUserLogin.UserName == "-1" || viewUserLogin.Password == "-1")
            {
                return new BoolModel(false,
                    @$"数据校验失败,请在本地安装证书后重试,
                    服务端为Windows    证书安装位置为:cert;CurrentUser/My
                    服务端为非windows  请将 SuperTerminal.Pem文件置于根目录 公钥
                    ");
            }
            string[] ignoreNames = new string[] { "admin", "Administrator", "root" };
            if (ignoreNames.Contains(viewUserLogin.UserName))
            {
                return new BoolModel(false, "用户名不能使用admin,Administrator,root");
            }
            if (viewUserLogin.UserName.Length < 5)
            {
                return new BoolModel(false, "用户名必须大于5");
            }
            if (viewUserLogin.Password.Length < 5)
            {
                return new BoolModel(false, "密码长度必须大于5");
            }
            if (_dbContext.Queryable<SysUser>().Any(o => o.UserName == viewUserLogin.UserName))
            {
                return new BoolModel(false, "用户名出现重复,注册失败");
            }
            SysUser entity = _mapper.Map<SysUser>(viewUserLogin);
            entity.UserType = 999;//管理员的类型只能是999
            entity.PassWord = entity.PassWord.MD5();//密码单向加密
            int result = _dbContext.Insertable(entity).ExecuteCommand();
            return new BoolModel(result > 0, result > 0 ? "注册成功" : "注册失败");
        }
        private RSA GetRsa()
        {
            RSA rsa;
            if (_osHelper.OSPlatformEnum == Enum.OSPlatformEnum.Windows)
            {
                //windows系统需要从证书中心的localMathine取私钥 用私钥解密
                rsa = RSAStore.GetRSAFromX509(Enum.RSAKeyType.PriKey, "SuperTerminal", StoreLocation.LocalMachine);
            }
            else
            {
                //非windows系统的读取根目录下的私钥
                rsa = RSAStore.GetRSAFromPem("SuperTerminal.key");//读取通过openssl生成的私钥
                if (rsa == null)
                {
                    rsa = RSAStore.GetRSAFromCustomFile("SuperTerminal.key", Enum.RSAKeyType.PriKey);//读取自生成的私钥
                }
            }
            return rsa;
        }
        /// <summary>
        /// 设备注册
        /// </summary>
        /// <param name="viewEquipmentModel"></param>
        /// <returns></returns>
        public BoolModel<int> RegistEquipment(ViewEquipmentModel viewEquipmentModel)
        {
            SysUser entity = _mapper.Map<SysUser>(viewEquipmentModel);
            entity.UserType = 0;
            entity.PassWord = entity.PassWord.MD5();
            int result = _dbContext.Insertable(entity).ExecuteReturnIdentity();
            return new BoolModel<int>(result > 0, result > 0 ? "注册成功" : "注册失败", result);
        }
        /// <summary>
        /// 根据条件获取设备信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ViewEquipmentModel> GetClients(string keyword)
        {
            if (_httpParameter.UserType == 999)
            {
                var result = _dbContext.Queryable<SysUser>()
                .Where(o => o.UserType == 0)
                .Where(o => o.NickName.Contains(keyword) || o.PubIp.Contains(keyword) || o.PrivIp.Contains(keyword))
                .Select<ViewEquipmentModel>().ToList();
                foreach (var item in result)
                {
                    item.PassWord = "********";
                    if (InstantMessage.Mapping.Keys.Any(o => o == item.Id))
                    {
                        item.OnLine = true;
                    }
                }
                return result;
            }
            else
            { 
                return new List<ViewEquipmentModel>();
            }
        }
    }
}
