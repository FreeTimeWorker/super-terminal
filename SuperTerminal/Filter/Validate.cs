using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SuperTerminal.FeildCheck;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperTerminal.Filter
{
    /// <summary>
    ///  <para> Model验证过滤器 例如：</para>
    /// <para> [Validate(typeof(ViewModelUserToken))]</para>
    /// <para> public IActionResult ConfirmUserToken(ViewModelUserToken formCollection)</para>
    /// <para>目前仅可以在mvc模式下使用，若要在外部调用使用过Validate标记的Action,请将请求头Content-Type设置为:"application/x-www-form-urlencoded"</para>
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
            System.Reflection.PropertyInfo[] properties = Type.GetProperties();//当前待验证Model的属性值集合
            //1，单model
            KeyValuePair<string, object> commitedModel = context.ActionArguments.Where(o => o.Value != null).FirstOrDefault(o => o.Value.GetType().ToString() == Type.FullName);//找到参数中对应的对象
            if (commitedModel.Value != null)
            {
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    object currentItemvalue = property.GetValue(commitedModel.Value);//当前项属性的值
                    object[] validateFieldItems = property.GetCustomAttributes(typeof(FeildCheckAttribute), true);
                    if (Vaildthis(currentItemvalue,property.PropertyType, validateFieldItems, context))
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
                foreach (object item in vvv)
                {
                    foreach (System.Reflection.PropertyInfo property in properties)
                    {
                        object currentItemvalue = property.GetValue(item);//当前项属性的值
                        object[] validateFieldItems = property.GetCustomAttributes(typeof(FeildCheck.FeildCheckAttribute), true);
                        if (Vaildthis(currentItemvalue, property.PropertyType, validateFieldItems, context))
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
        private bool Vaildthis(object currentItemvalue,Type currentItemType, object[] validateFieldItems, ActionExecutingContext context)
        {
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
                            break;
                        }
                    }
                    else if (item.GetType() == typeof(CheckUnique))
                    {
                        if (ValidUnique(currentItemvalue, currentItemType, (CheckUnique)item, context))
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 验证单个值在数据库中是否存在
        /// </summary>
        /// <param name="currentItemvalue"></param>
        /// <param name="currentItemType"></param>
        /// <param name="item"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ValidUnique(object currentItemvalue,Type currentItemType, CheckUnique item, ActionExecutingContext context)
        {
            
            throw new NotImplementedException();
        }
        /// <summary>
        /// 范围值默认通过
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
                        StatusCode = 0,
                        StatusMessage = "操作成功",
                        Data = new BoolModel() { Successed = false, Message = string.Concat(item.ErrorMsg) }
                    };
                    context.Result = new JsonResult(responseMode, new JsonSerializerSettings() { });
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
                    StatusCode = 0,
                    StatusMessage = "操作成功",
                    Data = new BoolModel() { Successed = false, Message = string.Concat(validateattr.ErrorMsg) }
                };
                context.Result = new JsonResult(responseMode, new JsonSerializerSettings() { });
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
