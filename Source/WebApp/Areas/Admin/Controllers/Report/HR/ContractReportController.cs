using Eagle.Common.Data;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Web.Mvc;
using DevExpress.XtraPivotGrid;
using Eagle.Core;
using Eagle.Repository.Common;
using Eagle.Repository.HR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Eagle.Model.Report.HR.Contract;


namespace Eagle.WebApp.Areas.Admin.Controllers.Report.HR
{
    public class ContractReportController : BaseController
    {        
        //
        // GET: /Admin/ContractReport/
        public static IEnumerable GetCategories()
        {
            return from category in DataProvider.DataSources.LS_tblContractType
                select new
                {
                    CategoryID = category.LSContractTypeID,
                    CategoryName = category.Name,
                    Description = category.Note
                };           
        }

        public static LS_tblContractType GetCategoryByID(int categoryID)
        {
            return (from category in DataProvider.DataSources.LS_tblContractType where category.LSContractTypeID == categoryID select category).SingleOrDefault<LS_tblContractType>();
        }

        public static string GetCategoryNameById(int id)
        {
            LS_tblContractType category = GetCategoryByID(id);
            return category != null ? category.Name : null;
        }

        public static IEnumerable GetCategoriesNames()
        {
            return from category in DataProvider.DataSources.LS_tblContractType select category.Name;
        }

        public static IEnumerable GetContracts()
        {
            return from c in DataProvider.DataSources.HR_tblContract select c;
        }

        public static IEnumerable GetContracts(string categoryName)
        {
            return from c in DataProvider.DataSources.HR_tblContract
                   join t in DataProvider.DataSources.LS_tblContractType on c.LSContractTypeID equals t.LSContractTypeID
                   where t.Name == categoryName
                   select c;
        }

        public static IEnumerable GetContracts(int? categoryID)
        {
            return from product in DataProvider.DataSources.HR_tblContract
                   where product.LSContractTypeID == categoryID
                   select product;
        }

        //public List<ContractDetailReportModel> GetContractReports(int? LSCompanyID)
        //{
        //    using (EntityDataContext context = new EntityDataContext())
        //    {
        //        System.Data.Objects.ObjectResult<HR_sprptContract> objectResult = context.HR_sprptContract_GetDetailList(LSCompanyID, LanguageId);
        //        return objectResult.Select(c => new ContractDetailReportModel()
        //        {
        //            LSContractTypeID = c.LSContractTypeID,
        //            ContractTypeName = c.ContractTypeName,
        //            FullName = c.FullName,
        //            EffectiveDate = c.EffectiveDate,
        //            ExpiredDate = c.ExpiredDate
        //        }).ToList();
        //    }

        //}

        //public List<ContractDetailReportModel> GetContractReports(int? LSCompanyID)
        //{
        //    using (EntityDataContext context = new EntityDataContext())
        //    {
        //        System.Data.Objects.ObjectResult<HR_sprptContract> objectResult = context.HR_sprptContract_GetDetailList(LSCompanyID, LanguageId);
        //        return objectResult.Select(c => new ContractDetailReportModel()
        //        {
        //            LSContractTypeID = c.LSContractTypeID,
        //            ContractTypeName = c.ContractTypeName,
        //            FullName = c.FullName,
        //            EffectiveDate = c.EffectiveDate,
        //            ExpiredDate = c.ExpiredDate
        //        }).ToList();
        //    }

        //}

        #region PIVOT GRID ===============================================
        //public ActionResult LoadPivotGrid(int? LSCompanyID)
        //{
        //    List<ContractDetailReportModel> lst = GetContractReports(LSCompanyID);
        //    return PartialView("../Report/HR/Contract/_ContractDetailPivotGrid", lst);
        //}

        //public class PivotGridReportsOptions
        //{
        //    public CustomerReportKind DemoKind { get; set; }
        //    public int FilterYearIndex { get; set; }
        //    public int FilterQuarterIndex { get; set; }

        //    public static PivotGridReportsOptions Default
        //    {
        //        get
        //        {
        //            return new PivotGridReportsOptions()
        //            {
        //                DemoKind = CustomerReportKind.Filtered,
        //                FilterYearIndex = -1,
        //                FilterQuarterIndex = -1
        //            };
        //        }
        //    }
        //}
        //private void pivotGridControl1_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        //{
        //    if (e.ValueType == PivotGridValueType.GrandTotal)
        //    {
        //        if (e.IsColumn)
        //        {
        //            e.DisplayText = "Total";
        //        }
        //        else
        //        {
        //            e.DisplayText = "Total";
        //        }
        //    }
        //}
        public class Contract_PivotGridHelper
        {
            static HttpSessionState Session
            {
                get { return System.Web.HttpContext.Current.Session; }
            }
            public static PivotGridSettings CreateGeneralSettings()
            {
                var settings = new PivotGridSettings();
                settings.Name = "ContractReport";
                settings.Theme = "Aqua";
                // settings.CallbackRouteValues = new { Controller = "ContractReport", Action = "LoadPivotGrid" };
                //settings.OptionsView.ShowFilterHeaders = false;
                //settings.CustomizationFieldsLeft = 600;
                //settings.CustomizationFieldsTop = 400;
                //settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

               
                settings.OptionsView.GroupFieldsInCustomizationWindow = false;
                settings.OptionsView.ShowHorizontalScrollBar = true;               
                settings.OptionsView.DataHeadersDisplayMode = DevExpress.Web.ASPxPivotGrid.PivotDataHeadersDisplayMode.Popup;
                settings.OptionsView.DataHeadersPopupMinCount = 4;
                settings.Width = new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage);
                settings.Height = new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage);
                settings.OptionsView.HideAllTotals();


                settings.OptionsView.ShowColumnGrandTotalHeader = false;
                settings.OptionsView.ShowColumnGrandTotals = false;
                settings.OptionsView.ShowRowGrandTotals = false;
                settings.OptionsView.ShowTotalsForSingleValues = false;

                settings.OptionsPager.Visible = false;
                //settings.OptionsPager.Position = PagerPosition.TopAndBottom;
                //settings.OptionsPager.FirstPageButton.Visible = true;
                //settings.OptionsPager.LastPageButton.Visible = true;
                //settings.OptionsPager.PageSizeItemSettings.Visible = true;
                //settings.OptionsPager.PageSizeItemSettings.Items = new string[] { "10", "20", "50" };
              

                settings.Groups.Add("Contract Types - Contracts");
                settings.Groups.Add("Sum");

                //settings.Groups.Add("Year - Quarter");
                //settings.Groups.Add("Sum, Min, Average");
              

                settings.Fields.Add(field =>
                {
                    field.Area = PivotArea.RowArea;
                    field.AreaIndex = 0;
                    field.Caption = "Contract Type";
                    field.FieldName = "ContractTypeName";
                    //field.Width = 200;                    
                    field.GroupIndex = 0;
                    field.InnerGroupIndex = 0;
                    field.CustomTotals.Add(PivotSummaryType.Count);
                });

                settings.Fields.Add(field =>
                {
                    field.Area = PivotArea.RowArea;
                    field.FieldName = "FullName";
                    field.Caption = "FullName";
                    //field.Width = 200;
                    field.GroupIndex = 0;
                    field.InnerGroupIndex = 1;
                    //field.CustomTotals.Add(PivotSummaryType.Count);
                    //field.CustomTotals.Add(PivotSummaryType.Average);
                    //field.CustomTotals.Add(PivotSummaryType.Min);
                    //field.CustomTotals.Add(PivotSummaryType.Max);
                });

                settings.Fields.Add(field =>
                {
                    field.Area = PivotArea.RowArea;
                    field.FieldName = "EffectiveDate";
                    field.Caption = "EffectiveDate";
                    field.CellFormat.FormatType = FormatType.DateTime;
                    field.ValueFormat.FormatString = "dd/MM/yyyy";
                    field.InnerGroupIndex = 1;
                });

                settings.Fields.Add(field =>
                {
                    field.Area = PivotArea.RowArea;
                    field.FieldName = "ExpiredDate";
                    field.Caption = "ExpiredDate";
                    field.CellFormat.FormatType = FormatType.DateTime;
                    field.ValueFormat.FormatString = "dd/MM/yyyy";
                    field.InnerGroupIndex = 1;
                });                           


                settings.CustomFieldValueCells = (sender, e) =>
                {
                    for (int i = e.GetCellCount(true) - 1; i >= 0; i--)
                    {
                        var cell = e.GetCell(true, i);
                        if (cell != null)
                            e.Remove(cell);
                    }                    
                };

                // Saves layout after change
                settings.GridLayout = (sender, e) =>
                {
                    MVCxPivotGrid PivotGrid = sender as MVCxPivotGrid;
                    Session["Layout"] = PivotGrid.SaveLayoutToString(PivotGridWebOptionsLayout.DefaultLayout);
                };

                // Loads layout after change
                settings.PreRender = (sender, e) =>
                {
                    MVCxPivotGrid PivotGrid = sender as MVCxPivotGrid;
                    string layout = Session["Layout"] as string;
                    if (!string.IsNullOrEmpty(layout))
                    {
                        PivotGrid.LoadLayoutFromString(layout, DevExpress.Web.ASPxPivotGrid.PivotGridWebOptionsLayout.DefaultLayout);
                    }
                };
                
                return settings;
            }
        }
        #endregion =======================================================
      
    }


}
