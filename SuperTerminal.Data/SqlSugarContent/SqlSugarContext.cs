using SqlSugar;
using SuperTerminal.MiddleWare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace SuperTerminal.Data.SqlSugarContent
{
    public class SqlSugarContext : IDbContext
    {
        private readonly ISqlSugarClient _sqlSugarScope;
        private readonly IHttpParameter _httpParameter;
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
        private void SetBaseData<T>(T entity, int id = 0) where T : IModel
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
            Dictionary<string, object> dict = new();
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
        public IInsertable<T> Insertable<T>(List<T> insertObjs) where T : class, IModel, new()
        {
            foreach (T entity in insertObjs)
            {
                SetBaseData(entity);
            }
            return _sqlSugarScope.Insertable(insertObjs);
        }
        public IInsertable<T> Insertable<T>(T insertObj) where T : class, IModel, new()
        {
            SetBaseData(insertObj);
            return _sqlSugarScope.Insertable(insertObj);
        }
        public IInsertable<T> Insertable<T>(T[] insertObjs) where T : class, IModel, new()
        {
            foreach (T item in insertObjs)
            {
                SetBaseData(item);
            }
            return _sqlSugarScope.Insertable(insertObjs);
        }
        #endregion
        #region 更新
        public IUpdateable<T> Updateable<T>() where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>().IgnoreColumns("CreateOn", "CreateBy", "IsDeleted");
        }
        public IUpdateable<T> Updateable<T>(Dictionary<string, object> dict) where T : class, IModel, new()
        {
            if (!dict.ContainsKey("UpdateOn"))
            {
                dict.Add("UpdateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("UpdateBy"))
            {
                dict.Add("UpdateBy", _httpParameter.UserId);
            }
            return _sqlSugarScope.Updateable<T>(dict).IgnoreColumns("CreateOn", "CreateBy", "IsDeleted");
        }

        public IUpdateable<T> Updateable<T>(dynamic updateDynamicObject) where T : class, IModel, new()
        {
            Dictionary<string, object> dict = new();
            foreach (System.Reflection.PropertyInfo p in updateDynamicObject.GetType().GetProperties())
            {
                dict[p.Name] = p.GetValue(updateDynamicObject, null);
            }
            if (!dict.ContainsKey("UpdateOn"))
            {
                dict.Add("UpdateOn", DateTime.Now);
            }
            if (!dict.ContainsKey("UpdateBy"))
            {
                dict.Add("UpdateBy", _httpParameter.UserId);
            }
            return _sqlSugarScope.Updateable<T>(updateDynamicObject).IgnoreColumns("CreateOn", "CreateBy", "IsDeleted");
        }

        public IUpdateable<T> Updateable<T>(Expression<Func<T, bool>> columns) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>(columns).IgnoreColumns("CreateOn", "CreateBy", "IsDeleted");
        }

        public IUpdateable<T> Updateable<T>(Expression<Func<T, T>> columns) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>(columns).IgnoreColumns("CreateOn", "CreateBy", "IsDeleted");
        }

        public IUpdateable<T> Updateable<T>(List<T> UpdateObjs) where T : class, IModel, new()
        {
            foreach (T item in UpdateObjs)
            {
                SetBaseData(item, item.Id);
            }
            return _sqlSugarScope.Updateable<T>(UpdateObjs).IgnoreColumns("CreateOn", "CreateBy", "IsDeleted");
        }

        public IUpdateable<T> Updateable<T>(T UpdateObj) where T : class, IModel, new()
        {
            SetBaseData(UpdateObj, UpdateObj.Id);
            return _sqlSugarScope.Updateable<T>(UpdateObj).IgnoreColumns("CreateOn", "CreateBy", "IsDeleted");
        }

        public IUpdateable<T> Updateable<T>(T[] UpdateObjs) where T : class, IModel, new()
        {
            foreach (T item in UpdateObjs)
            {
                SetBaseData(item, item.Id);
            }
            return _sqlSugarScope.Updateable<T>(UpdateObjs).IgnoreColumns("CreateOn", "CreateBy", "IsDeleted");
        }
        #endregion
        #region 删除
        public int Delete<T>(int id) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>().SetColumns(item => new T() { IsDeleted = true }).Where(o => o.Id == id).ExecuteCommand();
        }

        public int Delete<T>(int[] ids) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>().SetColumns(item => new T() { IsDeleted = true }).Where(o => ids.Contains(o.Id)).ExecuteCommand();
        }

        public int Delete<T>(Expression<Func<T, bool>> expression) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>().SetColumns(item => new T() { IsDeleted = true }).Where(expression).ExecuteCommand();
        }

        public int Delete<T>(List<int> ids) where T : class, IModel, new()
        {
            return _sqlSugarScope.Updateable<T>().SetColumns(item => new T() { IsDeleted = true }).Where(o => ids.Contains(o.Id)).ExecuteCommand();
        }

        public int Delete<T>(List<T> deleteObjs) where T : class, IModel, new()
        {
            foreach (T item in deleteObjs)
            {
                item.IsDeleted = true;
            }
            return _sqlSugarScope.Updateable(deleteObjs).ExecuteCommand();
        }

        public int Delete<T>(T deleteObj) where T : class, IModel, new()
        {
            deleteObj.IsDeleted = true;
            return _sqlSugarScope.Updateable(deleteObj).ExecuteCommand();
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
            return _sqlSugarScope.Queryable(joinQueryable1, joinQueryable2, joinExpression);
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

        public ISugarQueryable<T> Queryable<T>(ISugarQueryable<T> queryable) where T : class, IModel, new()
        {
            return _sqlSugarScope.Queryable<T>(queryable);
        }

        public ISugarQueryable<T> Queryable<T>(string shortName)
        {
            return _sqlSugarScope.Queryable<T>(shortName);
        }
        #endregion

        #region Storageable
        public IStorageable<T> Storageable<T>(List<T> dataList) where T : class, IModel, new()
        {
            List<int> ids = dataList.Where(o => o.Id > 0).Select(o => o.Id).ToList();
            List<T> entitys = _sqlSugarScope.Queryable<T>().Where(item => ids.Contains(item.Id)).ToList();
            foreach (T item in dataList)
            {
                if (item.Id > 0)
                {
                    SetBaseData(item, item.Id);
                    item.CreateOn = entitys.FirstOrDefault(o => o.Id == item.Id).CreateOn;
                    item.CreateBy = entitys.FirstOrDefault(o => o.Id == item.Id).CreateBy;
                }
                else
                {
                    SetBaseData(item);
                }
            }
            return _sqlSugarScope.Storageable(dataList);
        }

        public IStorageable<T> Storageable<T>(T data) where T : class, IModel, new()
        {
            SetBaseData(data, data.Id);
            if (data.Id > 0)
            {
                T entity = _sqlSugarScope.Queryable<T>().First(item => item.Id == data.Id);
                data.CreateOn = entity.CreateOn;
                data.CreateBy = entity.CreateBy;
            }
            else
            {
                SetBaseData(data);
            }
            return _sqlSugarScope.Storageable(data);
        }
        /// <summary>
        /// 比较少用,不写了
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StorageableDataTable Storageable(DataTable data)
        {
            data.Columns.Add("CreateOn", typeof(DateTime));
            data.Columns.Add("UpdateOn", typeof(DateTime));
            data.Columns.Add("CreateBy", typeof(int));
            data.Columns.Add("UpdateBy", typeof(int));
            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i]["UpdateOn"] = DateTime.Now;
                data.Rows[i]["UpdateBy"] = _httpParameter.UserId;
                data.Rows[i]["CreateOn"] = DateTime.Now;
                data.Rows[i]["CreateBy"] = _httpParameter.UserId;
            }
            return _sqlSugarScope.Storageable(data);
        }
        #endregion
    }
}
