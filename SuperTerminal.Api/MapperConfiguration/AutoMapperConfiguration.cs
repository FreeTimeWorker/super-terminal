using AutoMapper;
using SuperTerminal.Data.Entitys;
using SuperTerminal.Model;
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
        }
    }
}
