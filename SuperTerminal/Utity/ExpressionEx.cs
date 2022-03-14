using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace SuperTerminal.Utity
{
    public static class ExpressionEx
    {
        /// <summary>
        ///  根据字符串构建条件
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"> condition中的条件格式为 类名.属性名</param>
        /// <returns></returns>
        public static Expression<TDelegate> BuildCondition<TDelegate>(this Expression<TDelegate> source, string condition)
        {
            try
            {
                if (string.IsNullOrEmpty(condition))
                {
                    return source;
                }
                string specchar = "&|()";
                string tempconditon = "";
                int i = 0;
                Stack<string> stack = new Stack<string>();//条件符号，和(
                Stack<Expression> expressions = new Stack<Expression>();
                while (i < condition.Length)
                {
                    if (!specchar.Contains(condition[i]))
                    {
                        tempconditon += condition[i];
                    }
                    else
                    {
                        switch (condition[i])
                        {
                            case '&':
                                i++;
                                stack.Push("&&");
                                if (!string.IsNullOrEmpty(tempconditon))
                                {
                                    expressions.Push(BuildExpression(source, tempconditon));
                                }
                                tempconditon = "";
                                break;
                            case '|':
                                i++;
                                stack.Push("||");
                                if (!string.IsNullOrEmpty(tempconditon))
                                {
                                    expressions.Push(BuildExpression(source, tempconditon));
                                }
                                tempconditon = "";
                                break;
                            case '(':
                                stack.Push("(");
                                tempconditon = "";
                                break;
                            case ')':
                                string top = stack.Pop();
                                while (top != "(")
                                {
                                    if (!string.IsNullOrEmpty(tempconditon))
                                    {
                                        expressions.Push(BuildExpression(source, tempconditon));
                                        tempconditon = "";
                                    }
                                    if (expressions.Count > 1)
                                    {
                                        string smby = top;//不是括号肯定是符号，不是&&就是||
                                        var exp1 = expressions.Pop();
                                        var exp2 = expressions.Pop();
                                        switch (smby)
                                        {
                                            case "&&":
                                                expressions.Push(Expression.And(exp1, exp2));
                                                break;
                                            case "||":
                                                expressions.Push(Expression.Or(exp1, exp2));
                                                break;
                                        }
                                    }
                                    top = stack.Pop();
                                }
                                if (!string.IsNullOrEmpty(tempconditon))
                                {
                                    expressions.Push(BuildExpression(source, tempconditon));
                                    tempconditon = "";
                                }
                                break;
                        }
                    }
                    i++;
                }
                if (!string.IsNullOrWhiteSpace(tempconditon))
                {
                    expressions.Push(BuildExpression(source, tempconditon));
                }
                while (expressions.Count > 1)
                {
                    var exp1 = expressions.Pop();
                    var exp2 = expressions.Pop();
                    string smby = stack.Pop();
                    switch (smby)
                    {
                        case "&&":
                            expressions.Push(Expression.And(exp1, exp2));
                            break;
                        case "||":
                            expressions.Push(Expression.Or(exp1, exp2));
                            break;
                    }
                }
                return Expression.Lambda<TDelegate>(expressions.Pop(), source.Parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Expression BuildExpression<TDelegate>(Expression<TDelegate> source, string condition)
        {
            Regex r = new Regex("([a-zA-Z][a-zA-Z0-9_]*\\.*[a-zA-Z0-9_]*\\s*)([><=]=*|like)(.+)");
            var res = r.Match(condition);
            if (res.Groups.Count < 4)
            {
                throw new Exception("条件错误");
            }
            string left = res.Groups[1].ToString().Trim();
            string symbol = res.Groups[2].ToString().Trim();
            string right = res.Groups[3].ToString().Trim();
            //根据左边表达式找到propertyinfo

            string[] leftgroup = left.Split('.');
            if (leftgroup.Length < 2)
            {
                throw new Exception("表达式错误");
            }
            var parmExp = source.Parameters.ToList().FirstOrDefault(o => o.Type.Name == leftgroup[0].Trim());
            if (parmExp == null)
            {
                throw new Exception("表达式错误,请确保表达式格式为 TableName.FeildName >= 0");
            }
            var propertyInfo = parmExp.Type.GetProperty(leftgroup[1].Trim());
            MemberExpression leftexp = Expression.Property(parmExp, propertyInfo);
            if (symbol.Trim().ToLower() == "like")
            {
                right = right.Trim().TrimStart('\'').TrimEnd('\'').TrimStart('"').TrimEnd('"').Trim();
                string methodName = "StartsWith";
                if (right.StartsWith("%") && right.EndsWith("%"))
                {
                    methodName = "Contains";
                }
                MethodInfo methodInfo = typeof(string).GetMethod(methodName, new Type[] { typeof(string) });
                ConstantExpression c = Expression.Constant(right.TrimStart('%').TrimEnd('%').Trim(), typeof(string));
                return Expression.Call(leftexp, methodInfo, c);
            }
            else
            {
                object rightValue = null;
                if (propertyInfo.PropertyType == typeof(bool))
                {
                    if (right == "0")
                    {
                        rightValue = false;
                    }
                    else
                    {
                        rightValue = true;
                    }
                }
                else if (propertyInfo.PropertyType.BaseType == typeof(System.Enum))
                {
                    if (int.TryParse(right, out int rightintvalue))
                    {
                        rightValue = System.Enum.Parse(propertyInfo.PropertyType, System.Enum.GetName(propertyInfo.PropertyType, rightintvalue));
                    }
                    else
                    {
                        rightValue = System.Enum.Parse(propertyInfo.PropertyType, right);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    if (int.TryParse(right, out int rightintvalue))
                    {
                        rightValue = rightintvalue;
                    }
                    else
                    {
                        throw new Exception($"表达式错误类型转换失败{propertyInfo.Name}");
                    }
                }
                else //string类型的==
                {
                    rightValue = right;
                }
                ConstantExpression rightexp = Expression.Constant(rightValue, propertyInfo.PropertyType);
                switch (symbol)
                {
                    case ">":
                        return Expression.GreaterThan(leftexp, rightexp);
                    case "<":
                        return Expression.LessThan(leftexp, rightexp);
                    case ">=":
                        return Expression.GreaterThanOrEqual(leftexp, rightexp);
                    case "==":
                        return Expression.Equal(leftexp, rightexp);
                    case "<=":
                        return Expression.LessThanOrEqual(leftexp, rightexp);
                    default:
                        throw new Exception($"表达式错误,未定义符号{symbol}");
                }
            }
        }
    }
}
