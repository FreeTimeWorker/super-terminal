using SqlSugar;
using SuperTerminal.MiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Data.SqlSugarContent
{
    public static class SqlSugarExtention
    {
        public static Page<TSource> ToPage<TSource>(this ISugarQueryable<TSource> source, IHttpParameter httpParameter)
        {
            int totalNumber = 0;
            int totalPage = 0;
            var result = new Page<TSource>
            {
                Data = source.ToPageList(httpParameter.PageIndex, httpParameter.PageSize, ref totalNumber, ref totalPage),
                Message = "",
                TotalRecords = totalNumber,
                CurrentPageIndex = httpParameter.PageIndex,
                TotalPage = totalPage
            };
            return result;
        }
    }
}
