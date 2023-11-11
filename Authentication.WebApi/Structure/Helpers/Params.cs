using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Authentication.WebApi.Structure.Helpers;

public static class Params
{
    public static string GetRoute(Type typeParams, string prefix)
    {
        var translate = string.Empty;

        var properties = typeParams.GetProperties()
            .OrderBy(p => p.GetCustomAttributes(typeof(DisplayAttribute), true)
            .Cast<DisplayAttribute>()
            .Select(a => a.Order)
            .DefaultIfEmpty(int.MaxValue)
            .First());
        var parameters = new List<string>();

        properties.ToList().ForEach(prop =>
        {
            var attr = prop.GetCustomAttributes<FromRouteAttribute>()?.FirstOrDefault();
            if (attr != null)
            {
                parameters.Add(string.Concat("{", attr.Name, "}"));
            }
        });

        translate = string.Join("/", parameters);

        return $"{prefix}/{translate}";
    }

    public static string GetRoute(Type typeParams)
    {
        var translate = string.Empty;

        var properties = typeParams.GetProperties()
          .OrderBy(p => p.GetCustomAttributes(typeof(DisplayAttribute), true)
          .Cast<DisplayAttribute>()
          .Select(a => a.Order)
          .DefaultIfEmpty(int.MaxValue)
          .First());
        var parameters = new List<string>();

        properties.ToList().ForEach(prop =>
        {
            var attr = prop.GetCustomAttributes<FromRouteAttribute>()?.FirstOrDefault();
            if (attr != null)
            {
                parameters.Add(string.Concat("{", attr.Name, "}"));
            }
        });

        translate = string.Join("/", parameters);

        return $"{translate}";
    }

    public static string GetRoute<T>()
    {
        Type typeParams = typeof(T);
        var translate = string.Empty;

        var properties = typeParams.GetProperties()
          .OrderBy(p => p.GetCustomAttributes(typeof(DisplayAttribute), true)
          .Cast<DisplayAttribute>()
          .Select(a => a.Order)
          .DefaultIfEmpty(int.MaxValue)
          .First());

        var parameters = new List<string>();

        properties.ToList().ForEach(prop =>
        {
            var attr = prop.GetCustomAttributes<FromRouteAttribute>()?.FirstOrDefault();
            if (attr != null)
            {
                parameters.Add(string.Concat("{", attr.Name, "}"));
            }
        });

        translate = string.Join("/", parameters);

        return $"{translate}";
    }

    public static string GetRoute<T>(string prefix)
    {
        Type typeParams = typeof(T);

        var translate = string.Empty;

        var properties = typeParams.GetProperties();
        var parameters = new List<string>();

        properties.ToList().ForEach(prop =>
        {
            var attr = prop.GetCustomAttributes<FromRouteAttribute>()?.FirstOrDefault();
            if (attr != null)
            {
                parameters.Add(string.Concat("{", attr.Name, "}"));
            }
        });

        translate = string.Join("/", parameters);

        return $"{prefix}/{translate}";
    }
}
