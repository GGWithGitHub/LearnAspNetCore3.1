using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Learn_core_mvc.Core.ExtensionMethods
{
    //public static class EnumExtensions
    //{
    //    public static string ToTitleString(this Enum e)
    //    {
    //        return e.ToString().SplitCamelCase();
    //    }
    //}
    //public class Enums<T>
    //{
    //    public IList<T> GetValues(Enum value)
    //    {
    //        var enumValues = new List<T>();

    //        foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
    //        {
    //            enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
    //        }
    //        return enumValues;
    //    }

    //    public T Parse(string value)
    //    {
    //        return (T)Enum.Parse(typeof(T), value, true);
    //    }

    //    public IList<string> GetNames(Enum value)
    //    {
    //        return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
    //    }

    //    public IList<string> GetDisplayValues(Enum value)
    //    {
    //        return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
    //    }

    //    private static string lookupResource(Type resourceManagerProvider, string resourceKey)
    //    {
    //        foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
    //        {
    //            if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
    //            {
    //                System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
    //                return resourceManager.GetString(resourceKey);
    //            }
    //        }

    //        return resourceKey; // Fallback with the key name
    //    }

    //    public string GetDisplayValue(T value)
    //    {
    //        var fieldInfo = value.GetType().GetField(value.ToString());

    //        var descriptionAttributes = fieldInfo.GetCustomAttributes(
    //            typeof(DisplayAttribute), false) as DisplayAttribute[];

    //        if (descriptionAttributes[0].ResourceType != null)
    //            return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

    //        if (descriptionAttributes == null) return string.Empty;
    //        return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
    //    }
    //}
}
