using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Utility
{
    /// <summary>
    /// 属性值动态获取和赋值（get、set）
    /// </summary>
    public class PropertyUtil
    {
        /// <summary>
        /// 判断属性是否存在
        /// </summary>
        /// <returns><c>true</c>, if exist was ised, <c>false</c> otherwise.</returns>
        /// <param name="obj">Object.</param>
        /// <param name="propertyName">Property name.</param>
        public static bool isExist(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if(propertyInfo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 反射获取对象的属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object ReflectGetter(object obj, string propertyName)
        {
			Type type = obj.GetType();
			PropertyInfo propertyInfo = type.GetProperty(propertyName);
			var propertyValue = propertyInfo.GetValue(obj, null);
            return propertyValue;
        }


        /// <summary>
        /// 反射设置对象的属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        public static void ReflectSetter(object obj, string propertyName, object propertyValue)
        {
			Type type = obj.GetType();
			PropertyInfo propertyInfo = type.GetProperty(propertyName);
			propertyInfo.SetValue(obj, propertyValue, null);
        }

        public static string ReflectDescription(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            object[] objs = propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (objs.Length > 0)
            {
                return ((DescriptionAttribute)objs[0]).Description;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 表达式获取对象的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Func<T, object> ExpresionGetter<T>(string propertyName)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);


            //// 对象实例
            var parameterExpression = Expression.Parameter(typeof(object), "obj");


            //// 转换参数为真实类型
            var unaryExpression = Expression.Convert(parameterExpression, type);


            //// 调用获取属性的方法
            var callMethod = Expression.Call(unaryExpression, property.GetGetMethod());
            var expression = Expression.Lambda<Func<T, object>>(callMethod, parameterExpression);


            return expression.Compile();
        }


        /// <summary>
        /// 表达式设置对象的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Action<T, object> ExpresionSetter<T>(string propertyName)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);


            var objectParameterExpression = Expression.Parameter(typeof(object), "obj");
            var objectUnaryExpression = Expression.Convert(objectParameterExpression, type);


            var valueParameterExpression = Expression.Parameter(typeof(object), "val");
            var valueUnaryExpression = Expression.Convert(valueParameterExpression, property.PropertyType);


            //// 调用给属性赋值的方法
            var body = Expression.Call(objectUnaryExpression, property.GetSetMethod(), valueUnaryExpression);
            var expression = Expression.Lambda<Action<T, object>>(body, objectParameterExpression, valueParameterExpression);


            return expression.Compile();
        }


        /// <summary>
        /// Emit获取对象的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
//        public static Func<T, object> EmitGetter<T>(string propertyName)
//        {
//            var type = typeof(T);
//
//
//            var dynamicMethod = new DynamicMethod("get_" + propertyName, typeof(object), new[] { type }, type);
//            var iLGenerator = dynamicMethod.GetILGenerator();
//            iLGenerator.Emit(OpCodes.Ldarg_0);
//
//
//            var property = type.GetProperty(propertyName);
//            iLGenerator.Emit(OpCodes.Callvirt, property.GetMethod);
//
//
//            if (property.PropertyType.IsValueType)
//            {
//                // 如果是值类型，装箱
//                iLGenerator.Emit(OpCodes.Box, property.PropertyType);
//            }
//            else
//            {
//                // 如果是引用类型，转换
//                iLGenerator.Emit(OpCodes.Castclass, property.PropertyType);
//            }
//
//
//            iLGenerator.Emit(OpCodes.Ret);
//
//
//            return dynamicMethod.CreateDelegate(typeof(Func<T, object>)) as Func<T, object>;
//        }


        /// <summary>
        /// Emit设置对象的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
//        public static Action<T, object> EmitSetter<T>(string propertyName)
//        {
//            var type = typeof(T);
//
//
//            var dynamicMethod = new DynamicMethod("EmitCallable", null, new[] { type, typeof(object) }, type.Module);
//            var iLGenerator = dynamicMethod.GetILGenerator();
//
//
//            var callMethod = type.GetMethod("set_" + propertyName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
//            var parameterInfo = callMethod.GetParameters()[0];
//            var local = iLGenerator.DeclareLocal(parameterInfo.ParameterType, true);
//
//
//            iLGenerator.Emit(OpCodes.Ldarg_1);
//            if (parameterInfo.ParameterType.IsValueType)
//            {
//                // 如果是值类型，拆箱
//                iLGenerator.Emit(OpCodes.Unbox_Any, parameterInfo.ParameterType);
//            }
//            else
//            {
//                // 如果是引用类型，转换
//                iLGenerator.Emit(OpCodes.Castclass, parameterInfo.ParameterType);
//            }
//
//
//            iLGenerator.Emit(OpCodes.Stloc, local);
//            iLGenerator.Emit(OpCodes.Ldarg_0);
//            iLGenerator.Emit(OpCodes.Ldloc, local);
//
//
//            iLGenerator.EmitCall(OpCodes.Callvirt, callMethod, null);
//            iLGenerator.Emit(OpCodes.Ret);
//
//
//            return dynamicMethod.CreateDelegate(typeof(Action<T, object>)) as Action<T, object>;
//        }
    }
}