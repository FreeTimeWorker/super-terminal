using SuperTerminal.Data;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Data.SqlSugarContent
{
    public interface IDbContext
    {
        public IInsertable<T> Insertable<T>(Dictionary<string, object> dict) where T : class, new();
        public IInsertable<T> Insertable<T>(dynamic insertDynamicObject) where T : class, new();
        public IInsertable<T> Insertable<T>(List<T> insertObjs) where T : class, IModel, new();
        public IInsertable<T> Insertable<T>(T insertObj) where T : class, IModel, new();
        public IInsertable<T> Insertable<T>(T[] insertObjs) where T : class, IModel, new();
        public ISugarQueryable<T> Queryable<T>() where T : IModel;
    }
}
