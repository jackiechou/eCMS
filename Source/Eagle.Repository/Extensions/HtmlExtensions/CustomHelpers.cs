using Eagle.Common.Utilities;
using Eagle.Model.SYS;
using Eagle.Repository.SYS.FileManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.WebPages;



namespace System.Web.Mvc.Html
{
    public static class CustomHelpers
    {

        #region Required LabelFor
        public static MvcHtmlString RequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;

            var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, helper.ViewData);
            string metaDataValue = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));


            TagBuilder label = new TagBuilder("label");
            label.Attributes.Add("for", propertyName);
            label.Attributes.Add("class", "control-label");
            label.InnerHtml += string.Format("{0} (<span class='color-red'>*</span>)", metaDataValue);

            return new MvcHtmlString(label.ToString());
        }

        public static MvcHtmlString RequiredLabel(HtmlHelper helper, string controlName, string metaDataValue, object htmlAttributes = null)
        {
            TagBuilder label = new TagBuilder("label");
            label.Attributes.Add("id", controlName);
            label.Attributes.Add("name", controlName);
            label.Attributes.Add("class", "control-label");
            label.InnerHtml += string.Format("{0} (<span class='color-red'>*</span>)", metaDataValue);
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                label.MergeAttributes(attributes, true);
            }
            label.ToString(TagRenderMode.SelfClosing);
            return new MvcHtmlString(label.ToString());
        }

        #endregion

        #region Datetime Picker

        public static MvcHtmlString MonthPickerNullFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string SelectedDate = null, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;

            string _SelectedDate = string.Empty, SavedDate = string.Empty, SelectedMonthYear = string.Empty;
            DateTime SELECTED_DATE = new DateTime();

            if (SelectedDate == null || SelectedDate == string.Empty)
            {
                if (data.Model != null && data.Model.ToString() != "01/01/0001 00:00:00")
                {
                    SELECTED_DATE = Convert.ToDateTime(data.Model);
                    SelectedMonthYear = SELECTED_DATE.ToString("MM/yyyy");
                    SavedDate = SELECTED_DATE.ToString("MM/dd/yyyy");
                }
                else
                {
                    SavedDate = string.Empty;
                    SelectedMonthYear = string.Empty;
                }
            }
            else
            {
                SavedDate = "01/" + SelectedDate;
                SelectedMonthYear = SELECTED_DATE.ToString("MM/yyyy");
            }

            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName + "Picker");
            input.Attributes.Add("name", propertyName + "Picker");
            input.Attributes.Add("placeholder", "MM/yyyy");
            input.Attributes.Add("type", "text");
            input.Attributes.Add("value", SelectedMonthYear);

            TagBuilder input2 = new TagBuilder("input");
            input2.Attributes.Add("id", propertyName);
            input2.Attributes.Add("name", propertyName);
            input2.Attributes.Add("type", "hidden");
            input2.Attributes.Add("value", SavedDate);

            if (data.IsRequired)
            {
                var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, helper.ViewData);
                string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));

                string Required = string.Format("Please complete the \"{0}\" field.", value);
                input.Attributes.Add("data-val-required", Required);
                input.Attributes.Add("class", "mtz-monthpicker-widgetcontainer monthPicker span12 input-validation-error");
                input.Attributes.Add("data-val", "true");
            }
            else
            {
                input.Attributes.Add("class", "mtz-monthpicker-widgetcontainer monthPicker span12");
            }

            input.Attributes.Add("data-val-data", "The field must be a date.");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            string strHTML = input.ToString(TagRenderMode.SelfClosing) + input2.ToString();
            return new MvcHtmlString(strHTML);
        }

        public static MvcHtmlString MonthPickerFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string SelectedDate = null, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;

            string _SelectedDate = string.Empty, SavedDate = string.Empty, SelectedMonthYear = string.Empty;
            DateTime SELECTED_DATE = new DateTime();

            if (SelectedDate == null || SelectedDate == string.Empty)
            {
                if (data.Model != null && data.Model.ToString() != "01/01/0001 00:00:00")
                {
                    SELECTED_DATE = Convert.ToDateTime(data.Model);
                    SelectedMonthYear = SELECTED_DATE.ToString("MM/yyyy");
                    SavedDate = SELECTED_DATE.ToString("MM/dd/yyyy");
                }
                else
                {
                    DateTime firstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    SavedDate = firstDate.ToString("MM/dd/yyyy");
                    SelectedMonthYear = firstDate.ToString("MM/yyyy");
                }
            }
            else
            {
                SavedDate = "01/" + SelectedDate;
                SelectedMonthYear = SELECTED_DATE.ToString("MM/yyyy");
            }

            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName + "Picker");
            input.Attributes.Add("name", propertyName + "Picker");
            input.Attributes.Add("placeholder", "MM/yyyy");
            input.Attributes.Add("type", "text");
            input.Attributes.Add("value", SelectedMonthYear);

            TagBuilder input2 = new TagBuilder("input");
            input2.Attributes.Add("id", propertyName);
            input2.Attributes.Add("name", propertyName);
            input2.Attributes.Add("type", "hidden");
            input2.Attributes.Add("value", SavedDate);

            if (data.IsRequired)
            {
                var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, helper.ViewData);
                string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));

                string Required = string.Format("Please complete the \"{0}\" field.", value);
                input.Attributes.Add("data-val-required", Required);
                input.Attributes.Add("class", "mtz-monthpicker-widgetcontainer monthPicker span12 input-validation-error");
                input.Attributes.Add("data-val", "true");
            }
            else
            {
                input.Attributes.Add("class", "mtz-monthpicker-widgetcontainer monthPicker span12");
            }

            input.Attributes.Add("data-val-data", "The field must be a date.");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            string strHTML = input.ToString(TagRenderMode.SelfClosing) + input2.ToString();
            return new MvcHtmlString(strHTML);
        }

        public static MvcHtmlString DatepickerFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string Format = null, object htmlAttributes = null, int LanguageId = 10001)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            if (string.IsNullOrEmpty(Format))
            {
                Format = "dd/MM/yyyy";
            }
            string date = "";
            string localdate = "";
            if (data.Model != null)
            {
                try
                {
                    date = ((DateTime)data.Model).ToString(Format);
                    localdate = ((DateTime)data.Model).ToString();
                }
                catch
                {
                    date = ((DateTime)data.Model).ToString("dd/MM/yyyy");
                }
            }

            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "input-group date ignored datetimepicker");
            div.Attributes.Add("data-id", propertyName);
            //div.Attributes.Add("data-date-format", Format);


            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName + "tmp");
            input.Attributes.Add("name", propertyName + "tmp");

            input.Attributes.Add("size", "19");
            input.Attributes.Add("placeholder", Format.ToLower());
            input.Attributes.Add("type", "text");
            input.Attributes.Add("value", date);

            TagBuilder input2 = new TagBuilder("input");
            input2.Attributes.Add("id", propertyName);
            input2.Attributes.Add("name", propertyName);
            input2.Attributes.Add("type", "hidden");
            input2.Attributes.Add("value", localdate);

            var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, helper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));

            if (data.IsRequired)
            {

                //input.Attributes.Add("readonly", "readonly");
                string Required = string.Empty;
                if (LanguageId == 124)
                {
                    Required = string.Format("Please complete the \"{0}\" field.", value);
                }
                else
                {
                    Required = string.Format("Vui lòng nhập thông tin \"{0}\".", value);
                }

                input.Attributes.Add("data-val-required", Required);
                input.Attributes.Add("class", "form-control input-validation-error");
                input.Attributes.Add("data-val", "true");
            }
            else
            {
                input.Attributes.Add("class", "form-control");
            }
            if (LanguageId == 124)
            {
                input.Attributes.Add("data-val-data", string.Format("The field \"{0}\" must be a date.", value));
            }
            else
            {
                input.Attributes.Add("data-val-data", string.Format("Ô \"{0}\" không phải là kiểu ngày.", value));
            }

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            div.InnerHtml += input.ToString()
                + "<span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-calendar\"></span></span>";
            return new MvcHtmlString(div.ToString() + input2.ToString());
        }

        public static MvcHtmlString Datepicker2For<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string Format = null, object htmlAttributes = null, int LanguageId = 10001)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            if (string.IsNullOrEmpty(Format))
            {
                Format = "dd/MM/yyyy";
            }
            string date = "";
            string localdate = "";
            if (data.Model != null)
            {
                try
                {
                    date = ((DateTime)data.Model).ToString(Format);
                    localdate = ((DateTime)data.Model).ToString();
                }
                catch
                {
                    //date = ((DateTime)data.Model).ToString("dd/MM/yyyy");
                }
            }

            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "input-group date datetimepicker2");
            div.Attributes.Add("data-id", propertyName);
            //div.Attributes.Add("data-date-format", Format);


            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName + "tmp");
            input.Attributes.Add("name", propertyName + "tmp");

            input.Attributes.Add("size", "19");
            input.Attributes.Add("placeholder", Format.ToLower());
            input.Attributes.Add("type", "text");
            input.Attributes.Add("value", date);

            TagBuilder input2 = new TagBuilder("input");
            input2.Attributes.Add("id", propertyName);
            input2.Attributes.Add("name", propertyName);
            input2.Attributes.Add("type", "hidden");
            input2.Attributes.Add("value", localdate);

            var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, helper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));

            if (data.IsRequired)
            {

                //input.Attributes.Add("readonly", "readonly");
                string Required = string.Empty;
                if (LanguageId == 124)
                {
                    Required = string.Format("Please complete the \"{0}\" field.", value);
                }
                else
                {
                    Required = string.Format("Vui lòng nhập thông tin \"{0}\".", value);
                }

                input.Attributes.Add("data-val-required", Required);
                input.Attributes.Add("class", "form-control input-validation-error");
                input.Attributes.Add("data-val", "true");
            }
            else
            {
                input.Attributes.Add("class", "form-control");
            }
            if (LanguageId == 124)
            {
                input.Attributes.Add("data-val-data", string.Format("The field \"{0}\" must be a date.", value));
            }
            else
            {
                input.Attributes.Add("data-val-data", string.Format("Ô \"{0}\" không phải là kiểu ngày.", value));
            }

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            div.InnerHtml += input.ToString()
                + "<span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-calendar\"></span></span>";
            return new MvcHtmlString(div.ToString() + input2.ToString());
        }

        public static MvcHtmlString Datepicker3For<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string Format = null, object htmlAttributes = null, int LanguageId = 10001)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            if (string.IsNullOrEmpty(Format))
            {
                Format = "dd/MM/yyyy";
            }
            string date = "";
            string localdate = "";
            if (data.Model != null)
            {
                try
                {
                    var datetime = DateTime.ParseExact(data.Model.ToString(), Format, new System.Globalization.DateTimeFormatInfo());
                    date = datetime.ToString(Format);
                    localdate = datetime.ToString(Format);
                }
                catch
                {
                    //date = ((DateTime)data.Model).ToString("dd/MM/yyyy");
                }
            }

            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "input-group date datetimepicker2");
            div.Attributes.Add("data-id", propertyName);
            //div.Attributes.Add("data-date-format", Format);


            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName + "tmp");
            input.Attributes.Add("name", propertyName + "tmp");

            input.Attributes.Add("size", "19");
            input.Attributes.Add("placeholder", Format.ToLower());
            input.Attributes.Add("type", "text");
            input.Attributes.Add("value", date);

            TagBuilder input2 = new TagBuilder("input");
            input2.Attributes.Add("id", propertyName);
            input2.Attributes.Add("name", propertyName);
            input2.Attributes.Add("type", "hidden");
            input2.Attributes.Add("value", localdate);

            var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, helper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));

            if (data.IsRequired)
            {

                //input.Attributes.Add("readonly", "readonly");
                string Required = string.Empty;
                if (LanguageId == 124)
                {
                    Required = string.Format("Please complete the \"{0}\" field.", value);
                }
                else
                {
                    Required = string.Format("Vui lòng nhập thông tin \"{0}\".", value);
                }

                input.Attributes.Add("data-val-required", Required);
                input.Attributes.Add("class", "form-control input-validation-error");
                input.Attributes.Add("data-val", "true");
            }
            else
            {
                input.Attributes.Add("class", "form-control");
            }
            if (LanguageId == 124)
            {
                input.Attributes.Add("data-val-data", string.Format("The field \"{0}\" must be a date.", value));
            }
            else
            {
                input.Attributes.Add("data-val-data", string.Format("Ô \"{0}\" không phải là kiểu ngày.", value));
            }

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            div.InnerHtml += input.ToString()
                + "<span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-calendar\"></span></span>";
            return new MvcHtmlString(div.ToString() + input2.ToString());
        }

        public static MvcHtmlString DatepickerFromListFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string Format = null, object htmlAttributes = null, int LanguageId = 10001)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            if (string.IsNullOrEmpty(Format))
            {
                Format = "dd/MM/yyyy";
            }
            string date = "";
            string localdate = "";
            if (data.Model != null)
            {
                try
                {
                    date = ((DateTime)data.Model).ToString(Format);
                    localdate = ((DateTime)data.Model).ToString();
                }
                catch
                {
                    //date = ((DateTime)data.Model).ToString("dd/MM/yyyy");
                }
            }

            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "input-group date DatepickerFromList");
            div.Attributes.Add("data-id", propertyName);
            //div.Attributes.Add("data-date-format", Format);


            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName + "tmp");
            input.Attributes.Add("name", propertyName + "tmp");

            input.Attributes.Add("size", "19");
            input.Attributes.Add("placeholder", Format.ToLower());
            input.Attributes.Add("type", "text");
            input.Attributes.Add("value", date);

            TagBuilder input2 = new TagBuilder("input");
            input2.Attributes.Add("id", propertyName);
            input2.Attributes.Add("name", propertyName);
            input2.Attributes.Add("type", "hidden");
            input2.Attributes.Add("value", localdate);

            var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, helper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));

            if (data.IsRequired)
            {

                //input.Attributes.Add("readonly", "readonly");
                string Required = string.Empty;
                if (LanguageId == 124)
                {
                    Required = string.Format("Please complete the \"{0}\" field.", value);
                }
                else
                {
                    Required = string.Format("Vui lòng nhập thông tin \"{0}\".", value);
                }

                input.Attributes.Add("data-val-required", Required);
                input.Attributes.Add("class", "form-control input-validation-error");
                input.Attributes.Add("data-val", "true");
            }
            else
            {
                input.Attributes.Add("class", "form-control");
            }
            if (LanguageId == 124)
            {
                input.Attributes.Add("data-val-data", string.Format("The field \"{0}\" must be a date.", value));
            }
            else
            {
                input.Attributes.Add("data-val-data", string.Format("Ô \"{0}\" không phải là kiểu ngày.", value));
            }

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            div.InnerHtml += input.ToString()
                + "<span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-calendar\"></span></span>";
            return new MvcHtmlString(div.ToString() + input2.ToString());
        }

        public static MvcHtmlString DateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string Format = null, string SelectedDate = null, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            if (string.IsNullOrEmpty(Format))
                Format = "dd/MM/yyyy";

            string _SelectedDate = string.Empty, _SavedDate = string.Empty;
            if (SelectedDate != null && SelectedDate != string.Empty)
            {
                DateTime SELECTED_DATE = new DateTime();
                if (DateTime.TryParseExact(SelectedDate, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out SELECTED_DATE))
                {
                    _SelectedDate = SELECTED_DATE.ToString(Format);
                    _SavedDate = SELECTED_DATE.ToString("MM/dd/yyyy");
                }
                else
                {
                    SELECTED_DATE = Convert.ToDateTime(SelectedDate);
                    _SelectedDate = SELECTED_DATE.ToString(Format);
                    _SavedDate = SELECTED_DATE.ToString("MM/dd/yyyy");
                }
            }
            else
            {
                if (data.Model != null)
                {
                    try
                    {
                        string FormatSelectedDate = ((DateTime)data.Model).ToString(Format);
                        if (FormatSelectedDate != "01/01/0001")
                        {
                            _SelectedDate = ((DateTime)data.Model).ToString(Format);
                            _SavedDate = ((DateTime)data.Model).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            _SelectedDate = string.Empty;
                            _SavedDate = string.Empty;
                        }
                    }
                    catch
                    {
                        _SelectedDate = ((DateTime)data.Model).ToString(Format);
                        _SavedDate = ((DateTime)data.Model).ToString("MM/dd/yyyy");
                    }
                }
            }

            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "input-group date datetimepicker");
            div.Attributes.Add("data-id", propertyName);
            div.Attributes.Add("data-date", _SelectedDate);
            div.Attributes.Add("data-date-format", Format);
            div.Attributes.Add("data-link-field", propertyName);
            div.Attributes.Add("data-link-format", Format);


            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName + "Picker");
            input.Attributes.Add("name", propertyName + "Picker");
            input.Attributes.Add("class", "form-control date ignore");
            input.Attributes.Add("size", "19");
            input.Attributes.Add("placeholder", Format.ToLower());
            input.Attributes.Add("type", "text");
            //input.Attributes.Add("readonly", "true");   
            input.Attributes.Add("value", _SelectedDate);

            TagBuilder input2 = new TagBuilder("input");
            input2.Attributes.Add("id", propertyName);
            input2.Attributes.Add("name", propertyName);
            input2.Attributes.Add("type", "hidden");
            input2.Attributes.Add("value", _SavedDate);

            var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, helper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));

            if (data.IsRequired)
            {
                //input.Attributes.Add("readonly", "readonly");
                string Required = string.Empty;
                Required = string.Format("(*)", value);
                input.Attributes.Add("data-val-required", Required);
                input.AddCssClass("input-validation-error");
                input.Attributes.Add("data-val", "true");
            }

            input.Attributes.Add("data-val-data", string.Format("Invalid", value));
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            div.InnerHtml += input.ToString() + input2.ToString()
                + "<span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-calendar\"></span></span>";
            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString DateTimePicker(this HtmlHelper helper, string controlName, string Format = null, string SelectedDate = null, bool? IsRequired = false, object htmlAttributes = null)
        {
            if (string.IsNullOrEmpty(Format))
                Format = "dd/MM/yyyy";

            string _SelectedDate = string.Empty, _SavedDate = string.Empty;
            if (SelectedDate != null && SelectedDate != string.Empty)
            {
                DateTime SELECTED_DATE = new DateTime();
                if (DateTime.TryParseExact(SelectedDate, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out SELECTED_DATE))
                {
                    _SelectedDate = SELECTED_DATE.ToString(Format);
                    _SavedDate = SELECTED_DATE.ToString("MM/dd/yyyy");
                }
                else
                {
                    SELECTED_DATE = Convert.ToDateTime(SelectedDate);
                    _SelectedDate = SELECTED_DATE.ToString(Format);
                    _SavedDate = SELECTED_DATE.ToString("MM/dd/yyyy");
                }
            }

            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "input-group date ignored datetimepicker");
            div.Attributes.Add("data-id", controlName);
            div.Attributes.Add("data-date", _SelectedDate);
            div.Attributes.Add("data-date-format", Format);
            div.Attributes.Add("data-link-field", controlName);
            div.Attributes.Add("data-link-format", Format);


            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", controlName + "Picker");
            input.Attributes.Add("name", controlName + "Picker");
            input.Attributes.Add("class", "form-control date ignored");
            input.Attributes.Add("size", "19");
            input.Attributes.Add("placeholder", Format.ToLower());
            input.Attributes.Add("type", "text");
            //input.Attributes.Add("readonly", "true");   
            input.Attributes.Add("value", _SelectedDate);

            TagBuilder input2 = new TagBuilder("input");
            input2.Attributes.Add("id", controlName);
            input2.Attributes.Add("name", controlName);
            input2.Attributes.Add("type", "hidden");
            input2.Attributes.Add("value", _SavedDate);

            if (IsRequired != null && IsRequired == true)
            {
                string Required = string.Empty;
                Required = string.Format("(*)", controlName);
                input.Attributes.Add("data-val-required", Required);
                input.AddCssClass("input-validation-error");
                input.Attributes.Add("data-val", "true");
            }

            input.Attributes.Add("data-val-data", string.Format("Invalid", _SelectedDate));
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            div.InnerHtml += input.ToString() + input2.ToString()
                + "<span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-calendar\"></span></span>";
            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString DateOfBirthFor(this HtmlHelper html, string id, int minYear, int maxYear, object htmlAttribute = null)
        {
            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttribute);

            var days = Enumerable.Range(1, 31).Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            });
            var months = Enumerable.Range(1, 12).Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            });
            var years = Enumerable.Range(minYear, maxYear - (minYear - 1)).Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            });

            var mainDivTag = new TagBuilder("div");
            mainDivTag.MergeAttribute("id", id);
            mainDivTag.MergeAttributes(attributes);
            mainDivTag.InnerHtml = string.Concat(
                html.DropDownList("Day", days, new { style = "width : 40px " }).ToHtmlString(),
                html.DropDownList("Month", months, new { style = "width : 40px " }).ToHtmlString(),
                html.DropDownList("Year", years, new { style = "width : 60px " }).ToHtmlString()
            );

            return new MvcHtmlString(mainDivTag.ToString());
        }

        //protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        //{
        //    if (propertyDescriptor.Name == "DateOfBirth")
        //    {
        //        DateTime dob = new DateTime(int.Parse(controllerContext.HttpContext.Request.Form["Year"]), int.Parse(controllerContext.HttpContext.Request.Form["Month"]), int.Parse(controllerContext.HttpContext.Request.Form["Day"]));
        //        propertyDescriptor.SetValue(bindingContext.Model, dob);
        //    }
        //}
        #endregion

        #region Time Picker
        public static MvcHtmlString TimepickerFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string Format = null, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            string SimpleDisplayText = "";
            Format = "hh:mm";
            try
            {
                SimpleDisplayText = ((DateTime)data.Model).ToString("HH:mm");
            }
            catch
            {
                try
                {
                    var timespan = (TimeSpan)data.Model;
                    SimpleDisplayText =
                        ((timespan.Hours < 10) ? "0" + timespan.Hours.ToString() : timespan.Hours.ToString())
                        + ":" +
                        ((timespan.Minutes < 10) ? "0" + timespan.Minutes.ToString() : timespan.Minutes.ToString());
                }
                catch
                {
                }

            }
            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "input-append timepicker");

            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName);
            input.Attributes.Add("name", propertyName);
            input.Attributes.Add("type", "text");
            input.Attributes.Add("class", "input-mini");
            input.Attributes.Add("placeholder", "hh:mm");
            input.Attributes.Add("data-format", Format);
            input.Attributes.Add("value", SimpleDisplayText);

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes, true);
            }

            div.InnerHtml += input.ToString() + "<span class=\"add-on\"><i data-time-icon=\"icon-time\" data-date-icon=\"icon-calendar\"></i></span>";
            return new MvcHtmlString(div.ToString());
        }
        #endregion
        
        #region Image =======================================================================================================================================================
        public static string Image(this HtmlHelper helper, string url, object htmlAttributes)
        {
            string fileNameWithoutExtension = string.Empty;
            if (url == null || url == string.Empty)
            {
                url = "/Files/images/no_image.jpg";
                fileNameWithoutExtension = "no_image";
            }
            else
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(url);

            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("src", url);
            builder.Attributes.Add("alt", fileNameWithoutExtension);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        public static string Image(this HtmlHelper helper, string propertyName, string url, string altText, object htmlAttributes)
        {
            string fileNameWithoutExtension = string.Empty;
            if (url == null || url == string.Empty)
            {
                url = "/Files/images/no_image.jpg";
                fileNameWithoutExtension = "no_image";
            }
            else
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(url);
            string _altText = (altText == null || altText == string.Empty) ? fileNameWithoutExtension : altText;

            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("id", propertyName);
            builder.Attributes.Add("src", url);
            builder.Attributes.Add("alt", _altText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string propertyName, string dirPath, string fileName, string altText, object htmlAttributes = null)
        {
            string _fileName = string.Empty, filePath = string.Empty, fileNameWithoutExtension = string.Empty;
            if (fileName != null && fileName != string.Empty)
            {
                filePath = "/" + dirPath + "/" + fileName;
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filePath);
            }
            else
            {
                filePath = "/Files/images/no_image.jpg";
                fileNameWithoutExtension = "no_image";
            }

            string _altText = (altText == null || altText == string.Empty) ? fileNameWithoutExtension : altText;
            TagBuilder img = new TagBuilder("img");
            img.Attributes.Add("id", propertyName);
            img.Attributes.Add("src", filePath);
            img.Attributes.Add("alt", _altText);
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                img.MergeAttributes(attributes, true);
            }
            return new MvcHtmlString(img.ToString());
        }

        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string propertyName = data.PropertyName;
            var imgUrl = expression.Compile()(htmlHelper.ViewData.Model).ToString();
            TagBuilder tag = new TagBuilder("img");
            tag.Attributes.Add("id", propertyName);
            tag.Attributes.Add("src", imgUrl);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string propertyName = data.PropertyName;
            var imgUrl = expression.Compile()(htmlHelper.ViewData.Model).ToString();
            TagBuilder tag = new TagBuilder("img");
            tag.Attributes.Add("id", propertyName);
            tag.Attributes.Add("src", imgUrl);
            if (htmlAttributes != null)
                tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString ImageFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string dirPath, string fileName, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            string _fileName = string.Empty, filePath = string.Empty, fileNameWithoutExtension = string.Empty;
            if (fileName != null && fileName != string.Empty)
            {
                filePath = "/" + dirPath + "/" + fileName;
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filePath);
                string physicalPath = HttpContext.Current.Server.MapPath(filePath);
                if (System.IO.File.Exists(physicalPath))
                    filePath = "/Files/images/no_image.jpg";
            }
            else
            {
                filePath = "/Files/images/no_image.jpg";
                fileNameWithoutExtension = "no_image";
            }

            TagBuilder img = new TagBuilder("img");
            img.Attributes.Add("id", propertyName);
            img.Attributes.Add("src", filePath);
            img.Attributes.Add("alt", fileNameWithoutExtension);

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                img.MergeAttributes(attributes, true);
            }
            return new MvcHtmlString(img.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString ImageByFileId(this HtmlHelper helper, string propertyName, int? FileId, string altText, object htmlAttributes = null)
        {
            string folderPath = string.Empty, physicalFolderPath = string.Empty, fileName = string.Empty, filePath = string.Empty, physicalFilePath = string.Empty, fileNameWithoutExtension = string.Empty; byte[] fileContent = null;
            if (FileId != null && FileId > 0)
            {
                FileViewModel file_entity = FileRepository.GetDetails(FileId);
                if (file_entity != null)
                {
                    fileName = file_entity.FileName;
                    filePath = file_entity.FileUrl;
                    fileContent = file_entity.FileContent;
                    fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filePath);
                    physicalFilePath = HttpContext.Current.Server.MapPath("~" + filePath);
                    folderPath = "~" + file_entity.FolderPath;
                    physicalFolderPath = HttpContext.Current.Server.MapPath(folderPath);

                    if (file_entity.FolderPath != null && file_entity.FolderPath != string.Empty)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(physicalFolderPath);
                        if (!dirInfo.Exists)
                            dirInfo.Create();
                    }

                    FileInfo fileInfo = new FileInfo(physicalFilePath);
                    if (!fileInfo.Exists)
                        ImageUtils.CreateFileFromByteArray(fileName, folderPath, fileContent);
                }
                else
                {
                    filePath = "/Files/images/no_image.jpg";
                    fileNameWithoutExtension = "no_image";
                }
            }
            else
            {
                filePath = "/Files/images/no_image.jpg";
                fileNameWithoutExtension = "no_image";
            }

            string _altText = (altText == null || altText == string.Empty) ? fileNameWithoutExtension : altText;
            TagBuilder img = new TagBuilder("img");
            img.Attributes.Add("id", propertyName);
            img.Attributes.Add("src", filePath);
            img.Attributes.Add("alt", _altText);
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                img.MergeAttributes(attributes, true);
            }
            return new MvcHtmlString(img.ToString());
        }
        #endregion ==========================================================================================================================================================

        #region Enum DropDownList ===========================================================================================================================================

        public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>();

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = (value.Equals(selectedValue))
                };

            return htmlHelper.DropDownList(
                name,
                items
                );
        }       
  

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            IEnumerable<SelectListItem> items =
                values.Select(value => new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                });

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(
                expression,
                items
                );
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TProperty, TEnum>(this HtmlHelper<TModel> htmlHelper,Expression<Func<TModel, TProperty>> expression, TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem()
                                                {
                                                    Text = value.ToString(),
                                                    Value = value.ToString(),
                                                    Selected = (value.Equals(selectedValue))
                                                };
            return SelectExtensions.DropDownListFor(htmlHelper, expression, items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="isShowOptionText"></param>
        /// <returns><%= Html.EnumDropDownListFor(model => model.Person.FavoriteColor)%></returns>        
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            List<SelectListItem> items =
                values.Select(value => new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                }).ToList();

            if (items.Count ==0)
                items.Insert(0, new SelectListItem() { Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.None), Value = ""});            

            return htmlHelper.DropDownListFor(
                expression,
                items
                );
        }
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, bool? isShowOptionText, object htmlAttributes =null)
        {

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            Type baseEnumType = Enum.GetUnderlyingType(enumType);
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (FieldInfo field in enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {

                string text = field.Name;
                string value = Convert.ChangeType(field.GetValue(null), baseEnumType).ToString();
                bool selected = field.GetValue(null).Equals(metadata.Model);
                foreach (var displayAttribute in field.GetCustomAttributes(true).OfType<DisplayAttribute>())
                {
                    text = displayAttribute.GetName();
                }

                items.Add(new SelectListItem()
                {

                    Text = text,
                    Value = value,
                    Selected = selected
                });
            }

            if (metadata.IsNullableValueType)
            {
                //items.Insert(0, new SelectListItem { Text = "", Value = "" });
                items.Insert(0, new SelectListItem() { Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.None), Value = "" });
            }
            else
            {
                if (isShowOptionText!=null && isShowOptionText == true)
                    items.Insert(0, new SelectListItem() { Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.PleaseSelect), Value = "" });
            }  
            return SelectExtensions.DropDownListFor(htmlHelper, expression, items, htmlAttributes);
        }
        #endregion ==========================================================================================================================================================

        public static MvcHtmlString RadioButtonsForEnum(this HtmlHelper htmlHelper, string controlName, Type enumType, bool isHorizontal = false, int? selectedValue = null, object htmlAttribute = null)
        {
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);

            var sb = new StringBuilder();

            if (names != null)
            {
                sb = sb.AppendFormat("<ul class='radioList' style='list-style:none;'>");

                // Create a radio button for each item in the list
                foreach (var name in names)
                {
                    var label = name;

                    var memInfo = enumType.GetMember(name);

                    if (memInfo != null)
                    {
                        var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

                        if (attributes != null && attributes.Length > 0)
                        {
                            if (((DisplayAttribute)attributes[0]).ResourceType != null)
                                label = ((DisplayAttribute)attributes[0]).GetName();
                            else
                                label = ((DisplayAttribute)attributes[0]).Name;
                        }
                    }

                    var id = name;
                    var value = Convert.ToInt32(names.Where(e => e.StartsWith(name)).Select(e => (int)Enum.Parse(enumType, e)).FirstOrDefault());
                    // Create tag builder
                    var radio = new TagBuilder("input");
                    radio.Attributes.Add("id", id);
                    radio.Attributes.Add("name", controlName);
                    radio.Attributes.Add("type", "radio");
                    radio.Attributes.Add("value", value.ToString());
                    radio.Attributes.Add("style", "margin:-3px 3px 1px 0;");

                    string checkedFlag = "checked", selectedFlag = string.Empty;
                    if (selectedValue != null && selectedValue > 0)
                    {
                        if (value == selectedValue)
                        {
                            checkedFlag = "checked";
                            selectedFlag = "class='selected'";
                            radio.Attributes.Add("checked", checkedFlag.ToString());
                        }
                    }
                    else
                    {
                        if (names.First() == name)
                        {
                            checkedFlag = "checked";
                            selectedFlag = "class='selected'";
                            radio.Attributes.Add("checked", checkedFlag.ToString());
                        }
                    }
                    //var radio = htmlHelper.RadioButton(controlName, value, new { id=id, @checked = checkedFlag }).ToHtmlString();                    
                    // Add attributes                    
                    radio.MergeAttributes(new RouteValueDictionary(htmlAttribute));
                    radio.ToString(TagRenderMode.SelfClosing);
                   
                    if (isHorizontal == true)
                        sb.AppendFormat("<li " + selectedFlag + " style='display:inline;padding-right:10px;'>{0}{1}</li>", radio, HttpUtility.HtmlEncode(label));
                    else
                        sb.AppendFormat("<li " + selectedFlag + ">{0}{1}</li>", radio, HttpUtility.HtmlEncode(label));
                }

                sb = sb.AppendFormat("</ul>");
                //var sbScript = new StringBuilder();
                //sbScript.AppendLine("<script type='text/javascript'>");
                //sbScript.AppendLine("(function ($) {");
                //sbScript.AppendLine("$(document).on('change', 'input[type=radio][name=\"" + controlName + "\"]', function () {");
                //sbScript.AppendLine("$(this).parent('li').addClass('selected').siblings().removeClass('selected');");
                //sbScript.AppendLine("$(this).attr('checked', true).parent('li').siblings().find('input[type=radio]').attr('checked', false)");
                //sbScript.AppendLine("});");
                //sbScript.AppendLine("});");
                //sbScript.AppendLine("</script>");
                //sb.Append(sbScript);
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString RadioButtonsForEnumFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var names = Enum.GetNames(metaData.ModelType);
            var values = Enum.GetValues(metaData.ModelType);

            var sb = new StringBuilder();

            if (names != null)
            {
                sb = sb.AppendFormat("<ul>");

                // Create a radio button for each item in the list
                foreach (var name in names)
                {
                    var label = name;

                    var memInfo = metaData.ModelType.GetMember(name);

                    if (memInfo != null)
                    {
                        var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

                        if (attributes != null && attributes.Length > 0)
                        {
                            if (attributes != null && attributes.Length > 0)
                            {
                                if (((DisplayAttribute)attributes[0]).ResourceType != null)
                                    label = ((DisplayAttribute)attributes[0]).GetName();
                                else
                                    label = ((DisplayAttribute)attributes[0]).Name;
                            }
                        }
                    }

                    //var id = string.Format(
                    //    "{0}_{1}_{2}",
                    //    htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
                    //    metaData.PropertyName,
                    //    name
                    //);
                    var id = name;
                    var value = Convert.ToInt32(names.Where(e => e.StartsWith(name)).Select(e => (int)Enum.Parse(metaData.ModelType, e)).FirstOrDefault());
                    var radio = htmlHelper.RadioButtonFor(expression, value, new { id = id, name = metaData.PropertyName }).ToHtmlString();

                    sb.AppendFormat("<li>{0}{1}</li>", radio, HttpUtility.HtmlEncode(label));
                }

                sb = sb.AppendFormat("</ul>");
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        #region NumericUpDownFor ========================================================================================
        public static MvcHtmlString NumericUpDownFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,
            int? Min = null, int? Max = null, string Step = "any", string SelectedValue = null, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;

            string _SelectedValue = string.Empty;
            if (SelectedValue != null && SelectedValue != string.Empty)
                _SelectedValue = SelectedValue;
            else
            {
                if (data.Model != null)
                    _SelectedValue = data.Model.ToString();
            }

            TagBuilder tagInput = new TagBuilder("input");
            tagInput.Attributes.Add("id", propertyName);
            tagInput.Attributes.Add("name", propertyName);
            tagInput.Attributes.Add("type", "number");

            if (Min != null)
                tagInput.Attributes.Add("Min", Min.ToString());
            if (Max != null)
                tagInput.Attributes.Add("Max", Max.ToString());
            if (Step != null)
                tagInput.Attributes.Add("Step", Step);

            tagInput.Attributes.Add("value", _SelectedValue);

            if (data.IsRequired)
            {
                //input.Attributes.Add("readonly", "readonly");
                string Required = string.Format("Please complete the \"{0}\" field.", data.PropertyName);
                tagInput.Attributes.Add("data-val-required", Required);
            }
            tagInput.Attributes.Add("data-val-data", "The field must be a number.");
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tagInput.MergeAttributes(attributes, true);
            }
            return new MvcHtmlString(tagInput.ToString());
        }
        #endregion NumericUpDownFor =====================================================================================

        #region DropDownList ====================================================================================
        /// <summary>
        /// This HTML helper will create a Dropdownlist.
        /// </summary>
        /// <param name="helper">HTMLHelper</param>
        /// <param name="DataSource">Datasource to create the Dropdownlist.</param>
        /// <param name="DataValueProperty">Property name , which is used to populate the value field for Dropdownlist</param>
        /// <param name="DataTextProperty">Property name , which is used to populate the Text field for Dropdownlist</param>
        /// <param name="selectedValue">Property name , which is used to identify the selected item for Dropdownlist</param>
        /// <param name="HtmlAttributes">Additional htmlAttributes.</param>
        /// <returns></returns>
        public static string DataBoundDropDownList(this HtmlHelper helper, object DataSource, string DataValueProperty, string DataTextProperty, string selectedValue, object HtmlAttributes)
        {
            TagBuilder selectTag = CreateDropDownlist(DataSource, DataValueProperty, DataTextProperty, selectedValue);
            selectTag.MergeAttributes(new RouteValueDictionary(HtmlAttributes));
            return (selectTag.ToString());
        }

        public static string DataBoundDropDownList(this HtmlHelper helper, object DataSource, string DataValueProperty, string DataTextProperty, string selectedValue)
        {
            TagBuilder selectTag = CreateDropDownlist(DataSource, DataValueProperty, DataTextProperty, selectedValue);
            return (selectTag.ToString());
        }

        private static TagBuilder CreateDropDownlist(object DataSource, string DataValueProperty, string DataTextProperty, string selectedValue)
        {
            #region "Create the DropdownList"
            TagBuilder selectBuilder = new TagBuilder("select");
            StringBuilder optionStringBuilder = new StringBuilder();
            #endregion

            #region "Create OptionList"
            IEnumerable ddlSource = DataSource as IEnumerable;
            if (ddlSource == null)
            {
                throw new Exception("DataSource must implement IEnumerable Interface.");
            }
            IEnumerator item = ddlSource.GetEnumerator();
            while (item.MoveNext())
            {
                string strText = DataBinder.GetPropertyValue(item.Current, DataTextProperty).ToString();
                string strVal = DataBinder.GetPropertyValue(item.Current, DataValueProperty).ToString();
                TagBuilder optionBuilder = new TagBuilder("option");
                optionBuilder.MergeAttribute("value", strVal);
                optionBuilder.InnerHtml = strText;
                if (strVal == selectedValue)
                {
                    optionBuilder.MergeAttribute("selected", "selected");
                }
                optionStringBuilder.Append(optionBuilder.ToString());
            }
            selectBuilder.InnerHtml = optionStringBuilder.ToString();
            #endregion

            return (selectBuilder);
        }
        #endregion ==============================================================================================


        #region Span ============================================================================================
        public static MvcHtmlString SpanFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var valueGetter = expression.Compile();
            var value = valueGetter(helper.ViewData.Model);

            var span = new TagBuilder("span");
            span.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            if (value != null)
            {
                span.SetInnerText(value.ToString());
            }

            return MvcHtmlString.Create(span.ToString());
        }
        #endregion ==============================================================================================

        #region CheckBox =========================================================================================
        public static MvcHtmlString CheckBoxDisplayFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,
           bool? selectedValue = null, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;

            bool? _selectedValue = false;
            if (selectedValue != null && selectedValue.ToString() != string.Empty)
                _selectedValue = selectedValue;
            else
            {
                if (data.Model != null)
                    _selectedValue = Convert.ToBoolean(data.Model);
            }

            TagBuilder checkboxInput = new TagBuilder("input");
            checkboxInput.Attributes.Add("id", propertyName);
            checkboxInput.Attributes.Add("name", propertyName);
            checkboxInput.Attributes.Add("type", HtmlHelper.GetInputTypeString(InputType.CheckBox));
            checkboxInput.Attributes.Add("value", _selectedValue.ToString());

            TagBuilder hiddenInput = new TagBuilder("input");
            hiddenInput.Attributes.Add("name", propertyName);
            hiddenInput.Attributes.Add("type", HtmlHelper.GetInputTypeString(InputType.Hidden));
            hiddenInput.Attributes.Add("value", _selectedValue.ToString());


            if (data.IsRequired)
            {
                //input.Attributes.Add("readonly", "readonly");
                string Required = string.Format("Please complete the {0} field.", data.PropertyName);
                checkboxInput.Attributes.Add("data-val-required", Required);
            }

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                checkboxInput.MergeAttributes(attributes, true);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(checkboxInput.ToString(TagRenderMode.SelfClosing));
            sb.Append(hiddenInput.ToString(TagRenderMode.SelfClosing));
            return new MvcHtmlString(sb.ToString());
        }
        #endregion ===============================================================================================

        public static MvcHtmlString EmailFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
        {
            MvcHtmlString emailfor = html.TextBoxFor(expression, htmlAttributes);
            return new MvcHtmlString(emailfor.ToHtmlString().Replace("type=\"text\"", "type=\"email\""));
        }

        public static MvcHtmlString ValidationSummaryJQuery(this HtmlHelper htmlHelper, string message, IDictionary<string, object> htmlAttributes)
        {
            if (!htmlHelper.ViewData.ModelState.IsValid)
                return htmlHelper.ValidationSummary(message, htmlAttributes);


            StringBuilder sb = new StringBuilder(Environment.NewLine);

            var divBuilder = new TagBuilder("div");
            divBuilder.MergeAttributes<string, object>(htmlAttributes);
            divBuilder.AddCssClass(HtmlHelper.ValidationSummaryValidCssClassName); // intentionally add VALID css class

            if (!string.IsNullOrEmpty(message))
            {
                //--------------------------------------------------------------------------------
                // Build an EMPTY error summary message <span> tag
                //--------------------------------------------------------------------------------
                var spanBuilder = new TagBuilder("span");
                spanBuilder.SetInnerText(message);
                sb.Append(spanBuilder.ToString(TagRenderMode.Normal)).Append(Environment.NewLine);
            }

            divBuilder.InnerHtml = sb.ToString();
            return MvcHtmlString.Create(divBuilder.ToString(TagRenderMode.Normal));
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

        //public static IHtmlString RenderStyles(this HtmlHelper helper, params string[] additionalPaths)
        //{
        //    var page = helper.ViewDataContainer as WebPageExecutingBase;
        //    if (page != null && page.VirtualPath.StartsWith("~/"))
        //    {
        //        var virtualPath = "~/bundles" + page.VirtualPath.Substring(1);
        //        if (BundleTable.Bundles.GetBundleFor(virtualPath) == null)
        //        {
        //            var defaultPath = page.VirtualPath + ".css";
        //            BundleTable.Bundles.Add(new StyleBundle(virtualPath).Include(defaultPath).Include(additionalPaths));
        //        }
        //        return MvcHtmlString.Create(@"<link href=""" + HttpUtility.HtmlAttributeEncode(BundleTable.Bundles.ResolveBundleUrl(virtualPath)) + @""" rel=""stylesheet""/>");
        //    }
        //    return MvcHtmlString.Empty;
        //}

        //public static IHtmlString RenderScripts(this HtmlHelper helper, params string[] additionalPaths)
        //{
        //    var page = helper.ViewDataContainer as WebPageExecutingBase;
        //    if (page != null && page.VirtualPath.StartsWith("~/"))
        //    {
        //        var virtualPath = "~/bundles" + page.VirtualPath.Substring(1);
        //        if (BundleTable.Bundles.GetBundleFor(virtualPath) == null)
        //        {
        //            var defaultPath = page.VirtualPath + ".js";
        //            BundleTable.Bundles.Add(new ScriptBundle(virtualPath).Include(defaultPath).Include(additionalPaths));
        //        }
        //        return MvcHtmlString.Create(@"<script src=""" + HttpUtility.HtmlAttributeEncode(BundleTable.Bundles.ResolveBundleUrl(virtualPath)) + @"""></script>");
        //    }
        //    return MvcHtmlString.Empty;
        //}
    }
}