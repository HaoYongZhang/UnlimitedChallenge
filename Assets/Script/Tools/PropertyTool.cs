using System;
using System.Reflection;
using System.ComponentModel;

/// <summary>
/// 属性值动态获取和赋值（get、set）
/// </summary>
public class PropertyTool
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
        if (propertyInfo != null)
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
        //注意propertyName必须要有get;set;
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


    /// <summary>
    /// 反射获取对象的中文描述
    /// </summary>
    /// <returns>The description.</returns>
    /// <param name="obj">Object.</param>
    /// <param name="propertyName">Property name.</param>
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
}