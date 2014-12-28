using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;


namespace Eagle.Common.Extensions
{
    public static class SelectListItemHelper
    {      
        static readonly string[] FormattingRuleStyles = new[] {
            "Tahoma Bold", "Dark Red", "Light Red", "Dark Blue", "Light Blue", "Dark Green", "Light Green"
        };

        public static IEnumerable<SelectListItem> Generate<T>(T selectedValue)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            foreach (Enum value in Enum.GetValues(typeof(T)))
                yield return new SelectListItem()
                {
                    Text = converter.ConvertToInvariantString(value),
                    Selected = Enum.Equals(value, selectedValue)
                };
        }

        public static IEnumerable<SelectListItem> Generate(string[] values, int selectedIndex)
        {
            for (int i = 0; i < values.Length; i++)
                yield return new SelectListItem
                {
                    Value = i.ToString(),
                    Text = values[i],
                    Selected = i == selectedIndex
                };
        }
        
        public static IEnumerable<SelectListItem> GetFormattingRuleStyleItems(int defaultValue)
        {
            return SelectListItemHelper.Generate(FormattingRuleStyles, defaultValue);
        }

        public class ParameterDictionaryBinder : DefaultModelBinder
        {
            static IEnumerable<string> GetValueProviderKeys(ControllerContext context)
            {
                return context.HttpContext.Request.Params.AllKeys;
            }
            static object ConvertType(string stringValue, Type type)
            {
                return TypeDescriptor.GetConverter(type).ConvertFrom(stringValue);
            }

            public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                Type modelType = bindingContext.ModelType;
                Type idictType = modelType.GetInterface("System.Collections.Generic.IDictionary`2");

                if (idictType != null)
                {
                    Type[] argumetTypes = idictType.GetGenericArguments();

                    object result = null;
                    IModelBinder valueBinder = Binders.GetBinder(argumetTypes[1]);

                    foreach (string key in GetValueProviderKeys(controllerContext))
                    {
                        if (!key.StartsWith(bindingContext.ModelName, StringComparison.InvariantCultureIgnoreCase))
                            continue;

                        object dictKey;
                        string parameterName = key.Substring(bindingContext.ModelName.Length);
                        try
                        {
                            dictKey = ConvertType(parameterName, argumetTypes[0]);
                        }
                        catch (NotSupportedException)
                        {
                            continue;
                        }

                        ModelBindingContext innerBindingContext = new ModelBindingContext()
                        {
                            ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, argumetTypes[1]),
                            ModelName = key,
                            ModelState = bindingContext.ModelState,
                            PropertyFilter = bindingContext.PropertyFilter,
                            ValueProvider = bindingContext.ValueProvider
                        };
                        object newPropertyValue = valueBinder.BindModel(controllerContext, innerBindingContext);

                        if (result == null)
                            result = CreateModel(controllerContext, bindingContext, modelType);

                        if (!(bool)idictType.GetMethod("ContainsKey").Invoke(result, new object[] { dictKey }))
                            idictType.GetProperty("Item").SetValue(result, newPropertyValue, new object[] { dictKey });
                    }
                    return result;
                }
                return new DefaultModelBinder().BindModel(controllerContext, bindingContext);
            }
        }
    }
}
