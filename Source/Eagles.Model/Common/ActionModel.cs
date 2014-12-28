using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model.Common
{
    #region @Html.DropDownListFor(model=>model.ActionId, Model.ActionsList) ======================
    public enum ActionType
    {
        Create = 1,
        Read = 2,
        Update = 3,
        Delete = 4
    }

    public class ActionModel
    {
        public ActionModel()
        {
            ActionsList = new List<SelectListItem>();
        }
        [Display(Name = "Actions")]
        public int ActionId { get; set; }
        public IEnumerable<SelectListItem> ActionsList { get; set; }
    }
    #endregion ===================================================================================

    #region @Html.EnumDropDownListFor(model => model.ActionId,Model.ActionTypeList) ==============
    public class ActionTypeModel
    {
        [Display(Name = "Actions")]
        public int ActionId { get; set; }
        public ActionType ActionTypeList { get; set; }
    }
    #endregion ==================================================================================
}
