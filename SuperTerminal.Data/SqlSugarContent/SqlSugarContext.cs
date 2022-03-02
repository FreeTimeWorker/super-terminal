using SqlSugar;
using SuperTerminal.Data;
using SuperTerminal.MiddleWare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Data.SqlSugarContent
{
    public class SqlSugarContext : IDbContext
    {
        private ISqlSugarClient _sqlSugarScope;
        private IHttpParameter _httpParameter;
        public SqlSugarContext(ISqlSugarClient sqlSugarScope, IHttpParameter httpParameter)
        {
            _sqlSugarScope = sqlSugarScope;
            _httpParameter = httpParameter;
        }
        /// <summary>
        /// 设置默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="id">实体的ID如果大于0,就不修改创建时间</param>
        private void _setBaseData<T>(T entity,int id=0) where T : IModel
        {
            if (id < 1)
            {
                if (entity.CreateBy == null)
                {
                    entity.CreateBy = _httpParameter.UserId;
                }
                if (entity.CreateOn == null)
                {
                    entity.CreateOn = DateTime.Now;
                }
            }
            if (entity.UpdateBy == null)
            {
                entity.UpdateBy = _httpParameter.UserId;
            }
            if (entity.UpdateOn == null)
            {
                entity.UpdateOn = DateTime.Now;
            }
        }
        #region 添加
        public IInsertable<T> Insertable<T>(Dictionary<string, object> dict) where T : class, new()
        {
            if (!dict.ContainsKey("CreateOn"))
            {
                dict.Add("CreateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("CreateBy"))
            {
                dict.Add("CreateBy", _httpParameter.UserId);
            }
            if (!dict.ContainsKey("UpdateOn"))
            {
                dict.Add("UpdateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("UpdateBy"))
            {
                dict.Add("UpdateBy", _httpParameter.UserId);
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
                dict.Add("CreateBy", _httpParameter.UserId);
            }
            if (!dict.ContainsKey("UpdateOn"))
            {
                dict.Add("UpdateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("UpdateBy"))
            {
                dict.Add("UpdateBy", _httpParameter.UserId);
            }
            if (!dict.ContainsKey("IsDeleted"))
            {
                dict.Add("IsDeleted", 0);
            }
            return _sqlSugarScope.Insertable<T>(dict);
        }
        public IInsertable<T> Insertable<T>(List<T> insertObjs) where T :class,IModel, new()
        {
            foreach (var entity in insertObjs)
            {
                _setBaseData(entity);
            }
            return _sqlSugarScope.Insertable(insertObjs);
        }
        public IInsertable<T> Insertable<T>(T insertObj) where T : class,IModel, new()
        {
            _setBaseData(insertObj);
            return _sqlSugarScope.Insertable(insertObj);
        }
        public IInsertable<T> Insertable<T>(T[] insertObjs) where T : class, IModel,new()
        {
            foreach (var item in insertObjs)
            {
                _setBaseData(item);
            }
            return _sqlSugarScope.Insertable(insertObjs);
        }
        #endregion
        #region 更新
        public IUpdateable<T> Updateable<T>() where T : class, new()
        {
            return _sqlSugarScope.Updateable<T>();
        }
        public IUpdateable<T> Updateable<T>(Dictionary<string, object> dict) where T:class,IModel,new()
        {
            if (!dict.ContainsKey("CreateOn"))
            {
                dict.Add("CreateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("CreateBy"))
            {
                dict.Add("CreateBy", _httpParameter.UserId);
            }
            if (!dict.ContainsKey("UpdateOn"))
            {
                dict.Add("UpdateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("UpdateBy"))
            {
                dict.Add("UpdateBy", _httpParameter.UserId);
            }
            if (!dict.ContainsKey("IsDeleted"))
            {
                dict.Add("IsDeleted", 0);
            }
            return _sqlSugarScope.Updateable<T>(dict);
        }

        public IUpdateable<T> Updateable<T>(dynamic updateDynamicObject) where T : class, IModel, new()
        {
            var dict = new Dictionary<string, object>();
            foreach (System.Reflection.PropertyInfo p in updateDynamicObject.GetType().GetProperties())
            {
                dict[p.Name] = p.GetValue(updateDynamicObject, null);
            }
            if (!dict.ContainsKey("CreateOn"))
            {
                dict.Add("CreateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("CreateBy"))
            {
                dict.Add("CreateBy", _httpParameter.UserId);
            }
            if (!dict.ContainsKey("UpdateOn"))
            {
                dict.Add("UpdateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("UpdateBy"))
            {
                dict.Add("UpdateBy", _httpParameter.UserId);
            }
            if (!dict.ContainsKey("IsDeleted"))
            {
                dict.Add("IsDeleted", 0);
            }
            return _sqlSugarScope.Updateable<T>(updateDynamicObject);
        }

        public IUpdateable<T> Updateable<T>(Expression<Func<T, bool>> columns) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>(columns);
        }

        public IUpdateable<T> Updateable<T>(Expression<Func<T, T>> columns) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>(columns);
        }

        public IUpdateable<T> Updateable<T>(List<T> UpdateObjs) where T : class, IModel, new()
        {
            foreach (var item in UpdateObjs)
            {
                _setBaseData(item,item.Id);
            }
            return _sqlSugarScope.Updateable<T>(UpdateObjs);
        }

        public IUpdateable<T> Updateable<T>(T UpdateObj) where T : class, IModel, new()
        {
            _setBaseData(UpdateObj,UpdateObj.Id);
            return _sqlSugarScope.Updateable<T>(UpdateObj);
        }

        public IUpdateable<T> Updateable<T>(T[] UpdateObjs) where T : class, IModel, new()
        {
            foreach (var item in UpdateObjs)
            {
                _setBaseData(item,item.Id);
            }
            return _sqlSugarScope.Updateable<T>(UpdateObjs);
        }
        #endregion
        #region 删除
        public int Delete<T>(dynamic primaryKeyValue) where T : class, IModel, new()
        {
            primaryKeyValue.IsDeleted = true;
            return _sqlSugarScope.Updateable<T>(primaryKeyValue).ExecuteCommand();
        }

        public int Delete<T>(dynamic[] primaryKeyValues) where T : class, IModel, new()
        {
            foreach (var item in primaryKeyValues)
            {
                item.IsDeleted = true;
            }
            return _sqlSugarScope.Updateable<T>(primaryKeyValues).ExecuteCommand();
        }

        public int Delete<T>(Expression<Func<T, bool>> expression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>(expression).UpdateColumns(item=>item.IsDeleted).ReSetValue(item=>item.IsDeleted=true).ExecuteCommand();
        }

        public int Delete<T>(List<dynamic> pkValue) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>(pkValue).UpdateColumns(item=>item.IsDeleted).ReSetValue(item=>item.IsDeleted=true).ExecuteCommand();
        }

        public int Delete<T>(List<T> deleteObjs) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>(deleteObjs).UpdateColumns(item=>item.IsDeleted).ReSetValue(item=>item.IsDeleted=true).ExecuteCommand();
        }

        public int Delete<T>(T deleteObj) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>(deleteObj).UpdateColumns(item => item.IsDeleted).ReSetValue(item => item.IsDeleted = true).ExecuteCommand();
        }
        #endregion
        #region 查询
        public ISugarQueryable<T> SqlQueryable<T>(string sql) where T : class, IModel, new()
        {
            return _sqlSugarScope.SqlQueryable<T>(sql);
        }

        public ISugarQueryable<ExpandoObject> Queryable(string tableName, string shortName)
        {
            return _sqlSugarScope.Queryable(tableName, shortName);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Queryable<T, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8> Queryable<T, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8> Queryable<T, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7, T8> Queryable<T, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7> Queryable<T, T2, T3, T4, T5, T6, T7>(Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7> Queryable<T, T2, T3, T4, T5, T6, T7>(Expression<Func<T, T2, T3, T4, T5, T6, T7, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6, T7> Queryable<T, T2, T3, T4, T5, T6, T7>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6> Queryable<T, T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6> Queryable<T, T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5, T6> Queryable<T, T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5> Queryable<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5> Queryable<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4, T5> Queryable<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4> Queryable<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4> Queryable<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3, T4> Queryable<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3> Queryable<T, T2, T3>(Expression<Func<T, T2, T3, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3> Queryable<T, T2, T3>(Expression<Func<T, T2, T3, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2, T3> Queryable<T, T2, T3>(Expression<Func<T, T2, T3, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2> Queryable<T, T2>(Expression<Func<T, T2, bool>> joinExpression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2> Queryable<T, T2>(Expression<Func<T, T2, JoinQueryInfos>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2> Queryable<T, T2>(Expression<Func<T, T2, object[]>> joinExpression)
        {
            return _sqlSugarScope.Queryable(joinExpression);
        }

        public ISugarQueryable<T, T2> Queryable<T, T2>(ISugarQueryable<T> joinQueryable1, ISugarQueryable<T2> joinQueryable2, Expression<Func<T, T2, bool>> joinExpression) where T : class, new() where T2 : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinQueryable1,joinQueryable2,joinExpression);
        }

        public ISugarQueryable<T, T2> Queryable<T, T2>(ISugarQueryable<T> joinQueryable1, ISugarQueryable<T2> joinQueryable2, JoinType joinType, Expression<Func<T, T2, bool>> joinExpression) where T : class, new() where T2 : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinQueryable1, joinQueryable2, joinExpression);
        }

        public ISugarQueryable<T, T2, T3> Queryable<T, T2, T3>(ISugarQueryable<T> joinQueryable1, ISugarQueryable<T2> joinQueryable2, ISugarQueryable<T3> joinQueryable3, JoinType joinType1, Expression<Func<T, T2, T3, bool>> joinExpression1, JoinType joinType2, Expression<Func<T, T2, T3, bool>> joinExpression2) where T : class, IModel, new() where T2 : class, IModel, new() where T3 : class, IModel, new()
        {
            return _sqlSugarScope.Queryable(joinQueryable1, joinQueryable2, joinQueryable3, joinType1, joinExpression1, joinType2, joinExpression2);
        }

        public ISugarQueryable<T> Queryable<T>()
        {
            return _sqlSugarScope.Queryable<T>();
        }

        public  ISugarQueryable<T> Queryable<T>(ISugarQueryable<T> queryable) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable<T>(queryable);
        }

        public ISugarQueryable<T> Queryable<T>(string shortName)
        {
            return _sqlSugarScope.Queryable<T>(shortName);
        }
        #endregion

        #region Storageable
        public IStorageable<T> Storageable<T>(List<T> dataList) where T:class,IModel,new()
        {
            foreach (var item in dataList)
            {
                _setBaseData(item, item.Id);
            }
            return _sqlSugarScope.Storageable(dataList);
        }

        public IStorageable<T> Storageable<T>(T data) where T : class, IModel, new()
        {
            _setBaseData(data, data.Id);
            return _sqlSugarScope.Storageable(data);
        }

        public StorageableDataTable Storageable(DataTable data)
        {
            data.Columns.Add("CreateOn",typeof(DateTime));
            data.Columns.Add("UpdateOn", typeof(DateTime));
            data.Columns.Add("CreateBy",typeof(int));
            data.Columns.Add("UpdateBy", typeof(int));
            bool hasId = data.Columns.Contains("Id");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (hasId && data.Rows[i]["Id"] != null && data.Rows[i]["Id"].ToString() != "0")
                {
                    data.Rows[i]["UpdateOn"] = DateTime.Now;
                    data.Rows[i]["UpdateBy"] = _httpParameter.UserId;
                }
                else
                {
                    data.Rows[i]["UpdateOn"] = DateTime.Now;
                    data.Rows[i]["UpdateBy"] = _httpParameter.UserId;
                    data.Rows[i]["CreateOn"] = DateTime.Now;
                    data.Rows[i]["CreateBy"] = _httpParameter.UserId;
                }
            }
            return _sqlSugarScope.Storageable(data);
        }
        #endregion
    }
}
