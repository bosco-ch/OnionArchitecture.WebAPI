using System.Linq.Expressions;
using System.Reflection;

namespace OnionArchitecture.infrastructure
{
    [TestClass]
    public class ExpressionHelper
    {
        public static Expression<Func<TIn, bool>>//返回类型
            ToEquel<TIn, TOther>//方法名<占位符，占位符>
            (Expression<Func<TIn, TOther>> AccessorProp, TOther? Other)//参数
            where TIn : class
            where TOther : class
        {
            //获取 lambda 的参数
            ParameterExpression parameterExpression = AccessorProp.Parameters.Single();
            BinaryExpression? binaryExpressionAll = null;
            PropertyInfo[] properties = typeof(TOther).GetProperties();//获取目标other的所有属性
            //遍历其全部属性
            foreach (var propertyInfo in properties)
            {
                object value = null;
                if (Other != null)
                {
                    value = propertyInfo.GetValue(Other);
                }
                Type type = propertyInfo.GetType();
                //创建属性访问器表达器
                MemberExpression left = Expression.MakeMemberAccess(AccessorProp.Body, propertyInfo);
                //创建常量访问器表达式
                Expression right = Expression.Convert(Expression.Constant(value), type);//因为强类型 所以需要0i强制切换类型

                BinaryExpression binaryExpression =
                    (!type.IsPrimitive) ?
                    Expression.MakeBinary(ExpressionType.Equal, left, right, liftToNull: false, propertyInfo.PropertyType.GetMethod("op_Equality"))
                    : Expression.Equal(left, right);
                //将表达式连接起来
                binaryExpressionAll = (binaryExpression != null) ?
                    Expression.AndAlso(left: binaryExpressionAll, right: binaryExpression)
                    : binaryExpression;
            }
            if (binaryExpressionAll == null)
            {
                throw new ArgumentException("There are at least  a property");
            }

            //dd
            return Expression.Lambda<Func<TIn, bool>>(
                binaryExpressionAll, new ParameterExpression[1] { parameterExpression }
                );
        }
    }

}
