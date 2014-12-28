using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Report.Common
{
    public class ReportsModel
    {
        public string ReportID { get; set; }
        public XtraReport Report { get; set; }
    }

    public class ReportDesignerModel : ReportsModel
    {
        public string RedirectUrl { get; set; }
    }

    public static class DataSources
    {
        const string DataContextKey = "EntityDataContext";

        public static EntityDataContext DataContext
        {
            get
            {
                if (HttpContext.Current.Items[DataContextKey] == null)
                    HttpContext.Current.Items[DataContextKey] = new EntityDataContext();
                return (EntityDataContext)HttpContext.Current.Items[DataContextKey];
            }
        }
    }

    static class SelectListItemHelper
    {
        static readonly string[] FormattingRuleConditions = new[] {
            "Quantity more than 30", "Quantity more than 60", "Unit price more than 40", "Unit price more than 60",
            "Discount more than 5", "Discount more than 15", "Extended price more than 1000", "Extended price more than 1500"
        };
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

        public static IEnumerable<SelectListItem> GetFormattingRuleConditionItems(int defaultValue)
        {
            return SelectListItemHelper.Generate(FormattingRuleConditions, defaultValue);
        }

        public static IEnumerable<SelectListItem> GetFormattingRuleStyleItems(int defaultValue)
        {
            return SelectListItemHelper.Generate(FormattingRuleStyles, defaultValue);
        }
    }

   public static class ReportsDataProviderHelper {
        static string GetValidColumnName(string columnName, DataTable table) {
            if(string.IsNullOrEmpty(columnName))
                return string.Empty;
            if(table.Columns.Contains(columnName))
                return columnName;
            string[] words = columnName.Split('_');
            if(words == null || words.Length == 0)
                return string.Empty;
            string name = DevExpress.XtraPrinting.StringUtils.Join(" ", words);
            if(table.Columns.Contains(name))
                return name;
            return string.Empty;
        }

        public static void CopyToDataTable<T>(this IEnumerable<T> query, DataTable table) {
            if(query == null)
                return;

            PropertyInfo[] properties = null;

            foreach(T item in query) {
                if(properties == null)
                    properties = ((Type)item.GetType()).GetProperties();

                DataRow row = table.NewRow();

                foreach(PropertyInfo property in properties) {
                    string columnName = GetValidColumnName(property.Name, table);
                    if(string.IsNullOrEmpty(columnName))
                        continue;
                    var propertyValue = property.GetValue(item, null);
                    if(propertyValue is System.Data.Linq.Binary)
                        propertyValue = ((System.Data.Linq.Binary)propertyValue).ToArray();
                    row[columnName] = propertyValue ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            table.AcceptChanges();
        }

        static void AssignDataSource(Parameter parameter, object dataSource) {
            var displayMember = string.Empty;
            var valueMember = string.Empty;
            var dynamicLookUp = parameter.LookUpSettings as DynamicListLookUpSettings;
            if(dynamicLookUp != null) {
                displayMember = dynamicLookUp.DisplayMember;
                valueMember = dynamicLookUp.ValueMember;
            }
            parameter.LookUpSettings = new DynamicListLookUpSettings {
                DataSource = dataSource,
                DisplayMember = displayMember,
                ValueMember = valueMember
            };
        }
    }

   public class ParameterDictionaryBinder : DefaultModelBinder {
        static IEnumerable<string> GetValueProviderKeys(ControllerContext context) {
            return context.HttpContext.Request.Params.AllKeys;
        }
        static object ConvertType(string stringValue, Type type) {
            return TypeDescriptor.GetConverter(type).ConvertFrom(stringValue);
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            Type modelType = bindingContext.ModelType;
            Type idictType = modelType.GetInterface("System.Collections.Generic.IDictionary`2");

            if(idictType != null) {
                Type[] argumetTypes = idictType.GetGenericArguments();

                object result = null;
                IModelBinder valueBinder = Binders.GetBinder(argumetTypes[1]);

                foreach(string key in GetValueProviderKeys(controllerContext)) {
                    if(!key.StartsWith(bindingContext.ModelName, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    object dictKey;
                    string parameterName = key.Substring(bindingContext.ModelName.Length);
                    try {
                        dictKey = ConvertType(parameterName, argumetTypes[0]);
                    } catch(NotSupportedException) {
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

                    if(result == null)
                        result = CreateModel(controllerContext, bindingContext, modelType);

                    if(!(bool)idictType.GetMethod("ContainsKey").Invoke(result, new object[] { dictKey }))
                        idictType.GetProperty("Item").SetValue(result, newPropertyValue, new object[] { dictKey });
                }
                return result;
            }
            return new DefaultModelBinder().BindModel(controllerContext, bindingContext);
        }   
    }
}