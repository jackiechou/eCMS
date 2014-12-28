using Eagle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Repository.Extensions
{
    public class DropdownListUtils
    { 
        #region Convert
        public string ConvertDictionaryToDropDownListOption(IEnumerable<SelectListItem> lst, string id = "Dropdownlist", string selected = "")
        {
            string strResult = "<select id='" + id + "'>";
            strResult += "<option value=''>Please Select</option>";
            foreach (var item in lst)
            {
                if (item.Value == selected)
                {
                    strResult += "<option value='" + item.Value + "' selected='selected'>" + item.Text + "</option>";
                }
                else
                {
                    strResult += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                }
            }
            strResult += "</select>";
            return strResult;
        }
        public string ConvertDictionaryToDropDownListOptionNoSelect(IEnumerable<SelectListItem> lst, string id = "Dropdownlist", string selected = "")
        {
            string strResult = "<select id='" + id + "'>";
            foreach (var item in lst)
            {
                if (item.Value == selected)
                {
                    strResult += "<option value='" + item.Value + "' selected='selected'>" + item.Text + "</option>";
                }
                else
                {
                    strResult += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                }
            }
            strResult += "</select>";
            return strResult;
        }
        public string ConvertDictionaryToDropDownListOptionSelectAll(IEnumerable<SelectListItem> lst, string id = "Dropdownlist", string selected = "")
        {
            string strResult = "<select id='" + id + "'>";
            strResult += "<option value='0'> (Tất cả) </option>";
            foreach (var item in lst)
            {
                if (item.Value == selected)
                {
                    strResult += "<option value='" + item.Value + "' selected='selected'>" + item.Text + "</option>";
                }
                else
                {
                    strResult += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                }
            }
            strResult += "</select>";
            return strResult;
        }

        public string ConvertDictionaryToDropdownListItemCustomOption(List<SelectListItemCustom> selectListItems, string id = "Dropdownlist", string selected = "")
        {
            var selectListHtml = "";

            foreach (var item in selectListItems)
            {
                //var attributes = item.itemsHtmlAttributes.ForEach(p => string.Format("{0}='{1}'", p.Key, p.Value));

                string strAttr = item.itemsHtmlAttributes.Aggregate("", (a, b) => a + b.Key + "='" + b.Value + "' ");
                selectListHtml += string.Format(
                    "<option value='{0}' {1} {2}>{3}</option>",
                    item.Value,
                    item.Selected ? "selected" : string.Empty,
                    //string.Join(" ", attributes),
                    strAttr,
                    item.Text);
            }

            var html = string.Format("<select id='{0}' name='{0}'>{1}</select>", id, selectListHtml);

            return html;
        }

        #endregion
    }
}
