using System;
using System.Reflection;
using System.ComponentModel;

public class EnumTool
{
    /// <summary>
    /// 获取枚举类型的值
    /// </summary>
    /// <returns>The enum.</returns>
    /// <param name="enumValue">Enum value.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T GetEnum<T>(string enumValue)
    {
        T enumObj = (T)Enum.Parse(typeof(T), enumValue);

        return enumObj;
    }



    /// <summary>
    /// 获取一个枚举值的中文描述
    /// </summary>
    /// <param name="obj">枚举值</param>
    /// <returns></returns>
    public static string GetEnumDescription(Enum obj)
    {
        FieldInfo fi = obj.GetType().GetField(obj.ToString());
        DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return arrDesc[0].Description;
    }
}
