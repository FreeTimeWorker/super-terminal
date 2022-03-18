using AutoMapper;
using SuperTerminal.Data;
using SuperTerminal.Data.Entitys;
using SuperTerminal.Model;
using SuperTerminal.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperTerminal.Api
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<ViewTestModel, TestModel>();
            CreateMap<ViewManagerModel, SysUser>().ReverseMap();
            CreateMap<ViewEquipmentModel, SysUser>().ReverseMap();
            CreateMap<ViewUserLogin, SysUser>().ReverseMap();
        }
    }
}
