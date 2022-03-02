using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SuperTerminal.FeildCheck;
using SuperTerminal.GlobalService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace SuperTerminal.Filter
{
    /// <summary>
    /// 验证
    /// </summary>
    public class Validate : ActionFilterAttribute
    {
        public Type Type { get; set; }
        /// <summary>
        /// 验证的传入model
        /// </summary>
        /// <param name="ModelType"></param>
        public Validate(Type ModelType)
        {
            Type = ModelType;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            PropertyInfo[] properties = Type.GetProperties();//当前待验证Model的属性值集合
            //1，单model
            KeyValuePair<string, object> commitedModel = context.ActionArguments.Where(o => o.Value != null).FirstOrDefault(o => o.Value.GetType().ToString() == Type.FullName);//找到参数中对应的对象
            if (commitedModel.Value != null)
            {
                foreach (PropertyInfo property in properties)
                {
                    object currentItemvalue = property.GetValue(commitedModel.Value);//当前项属性的值
                    object[] validateFieldItems = property.GetCustomAttributes(typeof(FeildCheckAttribute), true);
                    if (Vaildthis(commitedModel.Value, currentItemvalue,property.PropertyType, validateFieldItems, context))
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            //2,list model
            KeyValuePair<string, object> commitedModels = context.ActionArguments.Where(o => o.Value != null).FirstOrDefault(o => o.Value.GetType().ToString() == string.Concat("System.Collections.Generic.List`1[", Type.FullName, "]"));//找到参数中对应的对象
            if (commitedModels.Value != null)
            {
                IEnumerable vvv = commitedModels.Value as IEnumerable;
                if (!ValidUniqueInList(vvv, properties, context))
                {
                    return;
                }
                foreach (object item in vvv)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        object currentItemvalue = property.GetValue(item);//当前项属性的值
                        object[] validateFieldItems = property.GetCustomAttributes(typeof(FeildCheckAttribute), true);
                        if (Vaildthis(item,currentItemvalue, property.PropertyType, validateFieldItems, context))
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            base.OnActionExecuting(context);
        }
        /// <summary>
        /// 列表中验证字段唯一
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private bool ValidUniqueInList(IEnumerable lst,PropertyInfo[] properties, ActionExecutingContext context)
        {
            bool valid = true;
            Dictionary<string, HashSet<object>> tempData = new Dictionary<string, HashSet<object>>();
            foreach (var item in lst)//遍历列表
            {
                if (valid)//验证通过继续执行
                {
                    foreach (var property in properties)//遍历属性
                    {
                        if (!tempData.ContainsKey(property.Name))
                        {
                            tempData.Add(property.Name, new HashSet<object>());
                        }
                        object currentItemvalue = property.GetValue(item);//当前项属性的值
                        object[] validateFieldItems = property.GetCustomAttributes(typeof(CheckUnique), true);
                        if (validateFieldItems.Length > 0)//当前属性有判断唯一的特性
                        {
                            if (!tempData[property.Name].Contains(currentItemvalue))
                            {
                                tempData[property.Name].Add(currentItemvalue);
                            }
                            else //存在重复数据
                            {
                                valid = false;
                                ResponseModel responseMode = new ResponseModel()
                                {
                                    Status = 200,
                                    StatusMsg = "操作成功",
                                    Data = new BoolModel() { Successed = false, Message = (validateFieldItems[0] as CheckUnique).ErrorMsg }
                                };
                                context.Result = new JsonResult(responseMode);
                                break;
                            }
                        }
                    }
                }
            }
            return valid;
        }
        private bool Vaildthis(object currentItem, object currentItemvalue,Type currentItemType, object[] validateFieldItems, ActionExecutingContext context)
        {
            bool valid = true;
            if (validateFieldItems.Length > 0)
            {
                foreach (var item in validateFieldItems)
                {
                    if (item.GetType() == typeof(CheckByRegular))
                    {
                        if (ValidRegular(currentItemvalue, currentItemType, (CheckByRegular)item, context))
                        {
                            continue;
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                    else if (item.GetType() == typeof(CheckByRange))
                    {
                        if (ValidRange(currentItemvalue, currentItemType, (CheckByRange)item, context))
                        {
                            continue;
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                    else if (item.GetType() == typeof(CheckUnique))
                    {
                        if (ValidUnique(currentItem,currentItemvalue, currentItemType, (CheckUnique)item, context))
                        {
                            continue;
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
            return valid;
        }
        /// <summary>
        /// 验证单个值在数据库中是否存在重复
        /// </summary>
        /// <param name="currentItemvalue"></param>
        /// <param name="currentItemType"></param>
        /// <param name="item"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ValidUnique(object currentItem, object currentItemvalue,Type currentItemType, CheckUnique item, ActionExecutingContext context)
        {
            //数据库中判断唯一
            //var dbContext = context.HttpContext.RequestServices.GetService<ISqlSugarClient>();//数据库上下文对象
            var dbContext = ServiceAgent.Provider.GetService<ISqlSugarClient>();//数据库上下文对象
            var identityFeild = Type.GetProperties().FirstOrDefault(o => o.Name == item.IdentityFeild);
            if (identityFeild != null)
            {
                if (identityFeild.PropertyType == typeof(int))
                {
                    var id = identityFeild.GetValue(currentItem) as int?;
                    if (id.HasValue && id.Value > 0)
                    {
                        //修改判断唯一
                        var count = dbContext.SqlQueryable<dynamic>($"SELECT * FROM `{item.TableName}` where {item.FeildName}='{currentItemvalue}' and {item.IdentityFeild}!={id} and IsDeleted=0").Count();
                        if (count > 0)
                        {
                            ResponseModel responseMode = new ResponseModel()
                            {
                                Status = 200,
                                StatusMsg = "操作成功",
                                Data = new BoolModel() { Successed = false, Message = item.ErrorMsg }
                            };
                            context.Result = new JsonResult(responseMode);
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return ValidUniqueInAdd(currentItemvalue, currentItemType, item, dbContext, context);
                    }
                }
                else
                {
                    return ValidUniqueInAdd(currentItemvalue, currentItemType, item, dbContext, context);
                }
            }
            else //没有ID属性的
            {
                return ValidUniqueInAdd(currentItemvalue, currentItemType, item, dbContext, context);
            }
        }
        /// <summary>
        /// 添加模式下判断唯一
        /// </summary>
        /// <param name="currentItemvalue"></param>
        /// <param name="currentItemType"></param>
        /// <param name="item"></param>
        /// <param name="dbContext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ValidUniqueInAdd(object currentItemvalue, Type currentItemType, CheckUnique item, ISqlSugarClient dbContext, ActionExecutingContext context)
        {
            var count = dbContext.SqlQueryable<dynamic>($"SELECT * FROM `{item.TableName}` where {item.FeildName}='{currentItemvalue}' and IsDeleted=0").Count();
            if (count > 0)
            {
                ResponseModel responseMode = new ResponseModel()
                {
                    Status = 200,
                    StatusMsg = "操作成功",
                    Data = new BoolModel() { Successed = false, Message = item.ErrorMsg }
                };
                context.Result = new JsonResult(responseMode);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 范围值
        /// </summary>
        /// <param name="currentItemvalue"></param>
        /// <param name="currentItemType"></param>
        /// <param name="item"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ValidRange(object currentItemvalue,Type currentItemType, CheckByRange item, ActionExecutingContext context)
        {
            if (currentItemvalue == null)
            {
                currentItemvalue = 0;
            }
            if (typeof(IComparable).IsAssignableFrom(currentItemType))
            {
                if ((currentItemvalue as IComparable).CompareTo(item.Max) < 0 && (currentItemvalue as IComparable).CompareTo(item.Min) >= 0)
                {
                    return true;
                }
                else
                {
                    ResponseModel responseMode = new ResponseModel()
                    {
                        Status = 200,
                        StatusMsg = "操作成功",
                        Data = new BoolModel() { Successed = false, Message = string.Concat(item.ErrorMsg) }
                    };
                    context.Result = new JsonResult(responseMode);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        /// <param name="currentItemvalue"></param>
        /// <param name="validateFieldItems"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ValidRegular(object currentItemvalue,Type currentItemType, CheckByRegular validateattr, ActionExecutingContext context)
        {
            if (currentItemvalue == null)
            {
                currentItemvalue = "";
            }
            Regex regex;
            bool passed = false;
            if (validateattr.Or)
            {
                foreach (string item in validateattr.Rules)
                {
                    regex = new Regex(item);
                    if (regex.Match(currentItemvalue.ToString()).Success)
                    {
                        passed = true;
                        break;
                    }
                }
            }
            else
            {
                List<bool> listpass = new List<bool>();
                foreach (string item in validateattr.Rules)
                {
                    regex = new Regex(item);
                    if (!regex.Match(currentItemvalue.ToString()).Success)
                    {
                        listpass.Add(false);
                    }
                    else
                    {
                        listpass.Add(true);
                    }
                }
                passed = listpass.Count == listpass.Where(o => o).Count();
            }
            if (!passed)
            {
                ResponseModel responseMode = new ResponseModel()
                {
                    Status = 200,
                    StatusMsg = "操作成功",
                    Data = new BoolModel() { Successed = false, Message = string.Concat(validateattr.ErrorMsg) }
                };
                context.Result = new JsonResult(responseMode);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
