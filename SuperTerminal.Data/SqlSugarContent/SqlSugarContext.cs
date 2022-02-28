using SqlSugar;
using SuperTerminal.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Data.SqlSugarContent
{
    public class SqlSugarContext:IDbContext
    {
        private ISqlSugarClient _sqlSugarScope;
        public SqlSugarContext(ISqlSugarClient sqlSugarScope)
        {
            _sqlSugarScope = sqlSugarScope;
        }
        public IInsertable<T> Insertable<T>(Dictionary<string, object> dict) where T : class, new()
        {
            if (!dict.ContainsKey("CreateOn"))
            {
                dict.Add("CreateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("CreateBy"))
            {
                dict.Add("CreateBy", 1);
            }
            if (!dict.ContainsKey("UpdateOn"))
            {
                dict.Add("UpdateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("UpdateBy"))
            {
                dict.Add("UpdateBy", 1);
            }
            if (!dict.ContainsKey("IsDeleted"))
            {
                dict.Add("IsDeleted", 0);
            }
            return _sqlSugarScope.Insertable<T>(dict);
        }
        public IInsertable<T> Insertable<T>(dynamic insertDynamicObject) where T : class, new()
        {
            if (insertDynamicObject is null)
            {
                throw new ArgumentNullException(nameof(insertDynamicObject));
            }
            var dict = new Dictionary<string, object>();
            foreach (System.Reflection.PropertyInfo p in insertDynamicObject.GetType().GetProperties())
            {
                dict[p.Name] = p.GetValue(insertDynamicObject, null);
            }
            if (!dict.ContainsKey("CreateOn"))
            {
                dict.Add("CreateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("CreateBy"))
            {
                dict.Add("CreateBy", 1);
            }
            if (!dict.ContainsKey("UpdateOn"))
            {
                dict.Add("UpdateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("UpdateBy"))
            {
                dict.Add("UpdateBy", 1);
            }
            if (!dict.ContainsKey("IsDeleted"))
            {
                dict.Add("IsDeleted", 0);
            }
            return _sqlSugarScope.Insertable<T>(dict);
        }
        public IInsertable<T> Insertable<T>(List<T> insertObjs) where T :class,IModel, new()
        {

            return _sqlSugarScope.Insertable(insertObjs);
        }
        public IInsertable<T> Insertable<T>(T insertObj) where T : class,IModel, new()
        {
            return _sqlSugarScope.Insertable(insertObj);
        }
        public IInsertable<T> Insertable<T>(T[] insertObjs) where T : class, IModel,new()
        {
            return _sqlSugarScope.Insertable(insertObjs);
        }
        public ISugarQueryable<T> Queryable<T>() where T : IModel
        {
            return _sqlSugarScope.Queryable<T>().Where(o => o.IsDeleted == false);
        }
    }
}
