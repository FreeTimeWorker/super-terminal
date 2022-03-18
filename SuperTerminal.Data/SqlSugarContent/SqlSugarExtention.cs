using SqlSugar;
using SuperTerminal.MiddleWare;

namespace SuperTerminal.Data.SqlSugarContent
{
    public static class SqlSugarExtention
    {
        public static Page<TSource> ToPage<TSource>(this ISugarQueryable<TSource> source, IHttpParameter httpParameter)
        {
            int totalNumber = 0;
            int totalPage = 0;
            Page<TSource> result = new()
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
