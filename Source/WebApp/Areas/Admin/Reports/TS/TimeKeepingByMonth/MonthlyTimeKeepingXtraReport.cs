using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for MonthlyTimeKeepingXtraRepor
/// </summary>
public class MonthlyTimeKeepingXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPictureBox xrPictureBoxLogo;
    private XRLabel xrLabelHeaderTitle;
    private XRLabel xrLblMonth;
    private XRLabel xrLblYear;
    private PageHeaderBand PageHeader;
    private XRTable xrTableHeader;
    private XRTableRow xrTableRowHeader;
    private XRTableCell xrTableCellHeaderNo;
    private XRTableCell xrTableCellHeaderEmpCode;
    private XRTableCell xrTableCellHeaderFullName;
    private XRTableCell xrTableCellHeaderPosition;
   
    private XRTableCell xrTableCellHeader1st;
    private XRTableCell xrTableCellHeader2nd;
    private XRTableCell xrTableCellHeader3rd;
    private XRTableCell xrTableCellHeader4th;
    private XRTableCell xrTableCellHeader5th;
    private XRTableCell xrTableCellHeader6th;
    private XRTableCell xrTableCellHeader7th;
    private XRTableCell xrTableCellHeader8th;
    private XRTableCell xrTableCellHeader9th;
    private XRTableCell xrTableCellHeader10th;
    private XRTableCell xrTableCellHeader11th;
    private XRTableCell xrTableCellHeader12th;
    private XRTableCell xrTableCellHeader13th;
    private XRTableCell xrTableCellHeader14th;
    private XRTableCell xrTableCellHeader15th;
    private XRTableCell xrTableCell1Header16th;
    private XRTableCell xrTableCellHeader17th;
    private XRTableCell xrTableCellHeader18th;    
    private XRTableCell xrTableCellHeader19th;
    private XRTableCell xrTableCellHeader20th;
    private XRTableCell xrTableCellHeader21st;
    private XRTableCell xrTableCellHeader22nd;
    private XRTableCell xrTableCellHeader23rd;
    private XRTableCell xrTableCellHeader24th;
    private XRTableCell xrTableCellHeader25th;
    private XRTableCell xrTableCellHeader26th;
    private XRTableCell xrTableCellHeader27th;
    private XRTableCell xrTableCellHeader28th;
    private XRTableCell xrTableCellHeader29th;
    private XRTableCell xrTableCellHeader30th;
    private XRTableCell xrTableCellHeader31st;
    private XRTable xrTableDetails;
    private XRTableRow xrTableRowDetails;
    private XRTableCell xrTableCellNo;
    private XRTableCell xrTableCellEmpCode;
    private XRTableCell xrTableCellFullName;
    private XRTableCell xrTableCellPosition;
    private XRTableCell xrTableCell1st;
    private XRTableCell xrTableCell2nd;
    private XRTableCell xrTableCell3rd;
    private XRTableCell xrTableCell4rd;
    private XRTableCell xrTableCell5th;
    private XRTableCell xrTableCell6th;
    private XRTableCell xrTableCell7th;
    private XRTableCell xrTableCell8th;
    private XRTableCell xrTableCell9th;
    private XRTableCell xrTableCell10th;
    private XRTableCell xrTableCell11th;
    private XRTableCell xrTableCell12th;
    private XRTableCell xrTableCell13th;
    private XRTableCell xrTableCell14th;
    private XRTableCell xrTableCell15th;
    private XRTableCell xrTableCell16th;
    private XRTableCell xrTableCell17th;
    private XRTableCell xrTableCell18th;
    private XRTableCell xrTableCell19th;
    private XRTableCell xrTableCell20th;
    private XRTableCell xrTableCell21st;
    private XRTableCell xrTableCell22nd;
    private XRTableCell xrTableCell23rd;
    private XRTableCell xrTableCell24th;
    private XRTableCell xrTableCell25th;
    private XRTableCell xrTableCell26th;
    private XRTableCell xrTableCell27th;
    private XRTableCell xrTableCell28th;
    private XRTableCell xrTableCell29th;
    private XRTableCell xrTableCell30th;
    private XRTableCell xrTableCell31st;
    private XRPageInfo xrPageFooterLeftText;
    private XRPageInfo xrPageFooterRightText;
    private Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeepingByMonth.MonthlyTimeKeepingDataSet monthlyTimeKeepingDataSet1;
    private Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeepingByMonth.MonthlyTimeKeepingDataSetTableAdapters.Timesheet_sprptTimeKeepingByMonth_MasterTableAdapter timesheet_sprptTimeKeepingByMonth_MasterTableAdapter;
    public DetailReportBand DetailReport;
    private DetailBand Detail1;
    private Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeepingByMonth.MonthlyTimeKeepingDataSetTableAdapters.Timesheet_sprptTimeKeepingByMonth_DetailsTableAdapter timesheet_sprptTimeKeepingByMonth_DetailsTableAdapter;
    private XRTable xrTableMaster;
    private XRTableRow xrTableRowMaster;
    private XRTableCell xrTableCellMasterName;
    private XRTable xrTableCompany;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCellLabelCompanyName;
    private XRTableCell xrTableCellCompanyName;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCellLabelAddress;
    private XRTableCell xrTableCellAddress;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCellLabelPhone;
    private XRTableCell xrTableCellPhone;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCellLabelFax;
    private XRTableCell xrTableCellFax;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCellLabelEmail;
    private XRTableCell xrTableCellEmail;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public MonthlyTimeKeepingXtraReport()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTableMaster = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRowMaster = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellMasterName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRowDetails = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1st = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2nd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3rd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4rd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21st = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22nd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23rd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31st = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabelHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblYear = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageFooterLeftText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageFooterRightText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTableHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRowHeader = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellHeaderNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderEmpCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader1st = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader2nd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader3rd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader4th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader5th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader6th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader7th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader8th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader9th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader10th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader11th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader12th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader13th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader14th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader15th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1Header16th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader17th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader18th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader19th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader20th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader21st = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader22nd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader23rd = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader24th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader25th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader26th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader27th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader28th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader29th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader30th = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeader31st = new DevExpress.XtraReports.UI.XRTableCell();
            this.monthlyTimeKeepingDataSet1 = new Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeepingByMonth.MonthlyTimeKeepingDataSet();
            this.timesheet_sprptTimeKeepingByMonth_MasterTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeepingByMonth.MonthlyTimeKeepingDataSetTableAdapters.Timesheet_sprptTimeKeepingByMonth_MasterTableAdapter();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.timesheet_sprptTimeKeepingByMonth_DetailsTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeepingByMonth.MonthlyTimeKeepingDataSetTableAdapters.Timesheet_sprptTimeKeepingByMonth_DetailsTableAdapter();
            this.xrTableCompany = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelCompanyName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCompanyName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelPhone = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPhone = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelFax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelEmail = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmail = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyTimeKeepingDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableMaster});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTableMaster
            // 
            this.xrTableMaster.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableMaster.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableMaster.Name = "xrTableMaster";
            this.xrTableMaster.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRowMaster});
            this.xrTableMaster.SizeF = new System.Drawing.SizeF(1454F, 25F);
            this.xrTableMaster.StylePriority.UseBorderColor = false;
            // 
            // xrTableRowMaster
            // 
            this.xrTableRowMaster.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellMasterName});
            this.xrTableRowMaster.Name = "xrTableRowMaster";
            this.xrTableRowMaster.Weight = 1D;
            // 
            // xrTableCellMasterName
            // 
            this.xrTableCellMasterName.BackColor = System.Drawing.Color.SteelBlue;
            this.xrTableCellMasterName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.DepartmentName")});
            this.xrTableCellMasterName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellMasterName.ForeColor = System.Drawing.Color.White;
            this.xrTableCellMasterName.Name = "xrTableCellMasterName";
            this.xrTableCellMasterName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrTableCellMasterName.StylePriority.UseBackColor = false;
            this.xrTableCellMasterName.StylePriority.UseFont = false;
            this.xrTableCellMasterName.StylePriority.UseForeColor = false;
            this.xrTableCellMasterName.StylePriority.UsePadding = false;
            this.xrTableCellMasterName.StylePriority.UseTextAlignment = false;
            this.xrTableCellMasterName.Text = "xrTableCellMasterName";
            this.xrTableCellMasterName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellMasterName.Weight = 3D;
            // 
            // xrTableDetails
            // 
            this.xrTableDetails.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableDetails.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableDetails.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
            this.xrTableDetails.Name = "xrTableDetails";
            this.xrTableDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRowDetails});
            this.xrTableDetails.SizeF = new System.Drawing.SizeF(1454F, 25F);
            this.xrTableDetails.StylePriority.UseBorderColor = false;
            this.xrTableDetails.StylePriority.UseBorders = false;
            this.xrTableDetails.StylePriority.UsePadding = false;
            this.xrTableDetails.StylePriority.UseTextAlignment = false;
            this.xrTableDetails.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRowDetails
            // 
            this.xrTableRowDetails.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellNo,
            this.xrTableCellEmpCode,
            this.xrTableCellFullName,
            this.xrTableCellPosition,
            this.xrTableCell1st,
            this.xrTableCell2nd,
            this.xrTableCell3rd,
            this.xrTableCell4rd,
            this.xrTableCell5th,
            this.xrTableCell6th,
            this.xrTableCell7th,
            this.xrTableCell8th,
            this.xrTableCell9th,
            this.xrTableCell10th,
            this.xrTableCell11th,
            this.xrTableCell12th,
            this.xrTableCell13th,
            this.xrTableCell14th,
            this.xrTableCell15th,
            this.xrTableCell16th,
            this.xrTableCell17th,
            this.xrTableCell18th,
            this.xrTableCell19th,
            this.xrTableCell20th,
            this.xrTableCell21st,
            this.xrTableCell22nd,
            this.xrTableCell23rd,
            this.xrTableCell24th,
            this.xrTableCell25th,
            this.xrTableCell26th,
            this.xrTableCell27th,
            this.xrTableCell28th,
            this.xrTableCell29th,
            this.xrTableCell30th,
            this.xrTableCell31st});
            this.xrTableRowDetails.Name = "xrTableRowDetails";
            this.xrTableRowDetails.Weight = 1D;
            // 
            // xrTableCellNo
            // 
            this.xrTableCellNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.Seq")});
            this.xrTableCellNo.Name = "xrTableCellNo";
            this.xrTableCellNo.StylePriority.UseTextAlignment = false;
            this.xrTableCellNo.Text = "xrTableCellNo";
            this.xrTableCellNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellNo.Weight = 0.10376547455295737D;
            // 
            // xrTableCellEmpCode
            // 
            this.xrTableCellEmpCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.EmpCode")});
            this.xrTableCellEmpCode.Name = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellEmpCode.Text = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellEmpCode.Weight = 0.20907855656976704D;
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.FullName")});
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.Text = "xrTableCellFullName";
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellFullName.Weight = 0.38410931876989496D;
            // 
            // xrTableCellPosition
            // 
            this.xrTableCellPosition.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.PositionName")});
            this.xrTableCellPosition.Name = "xrTableCellPosition";
            this.xrTableCellPosition.StylePriority.UseTextAlignment = false;
            this.xrTableCellPosition.Text = "xrTableCellPosition";
            this.xrTableCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellPosition.Weight = 0.28008044100009555D;
            // 
            // xrTableCell1st
            // 
            this.xrTableCell1st.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.1")});
            this.xrTableCell1st.Name = "xrTableCell1st";
            this.xrTableCell1st.Text = "xrTableCell1st";
            this.xrTableCell1st.Weight = 0.061898213141230209D;
            // 
            // xrTableCell2nd
            // 
            this.xrTableCell2nd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.2")});
            this.xrTableCell2nd.Name = "xrTableCell2nd";
            this.xrTableCell2nd.Text = "xrTableCell2nd";
            this.xrTableCell2nd.Weight = 0.061898210517641924D;
            // 
            // xrTableCell3rd
            // 
            this.xrTableCell3rd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.3")});
            this.xrTableCell3rd.Name = "xrTableCell3rd";
            this.xrTableCell3rd.Text = "xrTableCell3rd";
            this.xrTableCell3rd.Weight = 0.0618982105176419D;
            // 
            // xrTableCell4rd
            // 
            this.xrTableCell4rd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.4")});
            this.xrTableCell4rd.Name = "xrTableCell4rd";
            this.xrTableCell4rd.Text = "xrTableCell4rd";
            this.xrTableCell4rd.Weight = 0.0618982124853331D;
            // 
            // xrTableCell5th
            // 
            this.xrTableCell5th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.5")});
            this.xrTableCell5th.Name = "xrTableCell5th";
            this.xrTableCell5th.Text = "xrTableCell5th";
            this.xrTableCell5th.Weight = 0.061898212485333073D;
            // 
            // xrTableCell6th
            // 
            this.xrTableCell6th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.6")});
            this.xrTableCell6th.Name = "xrTableCell6th";
            this.xrTableCell6th.Text = "xrTableCell6th";
            this.xrTableCell6th.Weight = 0.0618982105176419D;
            // 
            // xrTableCell7th
            // 
            this.xrTableCell7th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.7")});
            this.xrTableCell7th.Name = "xrTableCell7th";
            this.xrTableCell7th.Text = "xrTableCell7th";
            this.xrTableCell7th.Weight = 0.061898210517641918D;
            // 
            // xrTableCell8th
            // 
            this.xrTableCell8th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.8")});
            this.xrTableCell8th.Name = "xrTableCell8th";
            this.xrTableCell8th.Text = "xrTableCell8th";
            this.xrTableCell8th.Weight = 0.061898210517641931D;
            // 
            // xrTableCell9th
            // 
            this.xrTableCell9th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.9")});
            this.xrTableCell9th.Name = "xrTableCell9th";
            this.xrTableCell9th.Text = "xrTableCell9th";
            this.xrTableCell9th.Weight = 0.061898210517641959D;
            // 
            // xrTableCell10th
            // 
            this.xrTableCell10th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.10")});
            this.xrTableCell10th.Name = "xrTableCell10th";
            this.xrTableCell10th.Text = "xrTableCell10th";
            this.xrTableCell10th.Weight = 0.061898210517641904D;
            // 
            // xrTableCell11th
            // 
            this.xrTableCell11th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.11")});
            this.xrTableCell11th.Name = "xrTableCell11th";
            this.xrTableCell11th.Text = "xrTableCell11th";
            this.xrTableCell11th.Weight = 0.061898210517641911D;
            // 
            // xrTableCell12th
            // 
            this.xrTableCell12th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.12")});
            this.xrTableCell12th.Name = "xrTableCell12th";
            this.xrTableCell12th.Text = "xrTableCell12th";
            this.xrTableCell12th.Weight = 0.061898210517642D;
            // 
            // xrTableCell13th
            // 
            this.xrTableCell13th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.13")});
            this.xrTableCell13th.Name = "xrTableCell13th";
            this.xrTableCell13th.Text = "xrTableCell13th";
            this.xrTableCell13th.Weight = 0.061898210517641904D;
            // 
            // xrTableCell14th
            // 
            this.xrTableCell14th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.14")});
            this.xrTableCell14th.Name = "xrTableCell14th";
            this.xrTableCell14th.Text = "xrTableCell14th";
            this.xrTableCell14th.Weight = 0.061898209205847904D;
            // 
            // xrTableCell15th
            // 
            this.xrTableCell15th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.15")});
            this.xrTableCell15th.Name = "xrTableCell15th";
            this.xrTableCell15th.Text = "xrTableCell15th";
            this.xrTableCell15th.Weight = 0.061898210517641848D;
            // 
            // xrTableCell16th
            // 
            this.xrTableCell16th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.16")});
            this.xrTableCell16th.Name = "xrTableCell16th";
            this.xrTableCell16th.Text = "xrTableCell16th";
            this.xrTableCell16th.Weight = 0.061898210517641938D;
            // 
            // xrTableCell17th
            // 
            this.xrTableCell17th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.17")});
            this.xrTableCell17th.Name = "xrTableCell17th";
            this.xrTableCell17th.Text = "xrTableCell17th";
            this.xrTableCell17th.Weight = 0.061898210517641883D;
            // 
            // xrTableCell18th
            // 
            this.xrTableCell18th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.18")});
            this.xrTableCell18th.Name = "xrTableCell18th";
            this.xrTableCell18th.Text = "xrTableCell18th";
            this.xrTableCell18th.Weight = 0.061898213141230195D;
            // 
            // xrTableCell19th
            // 
            this.xrTableCell19th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.18")});
            this.xrTableCell19th.Name = "xrTableCell19th";
            this.xrTableCell19th.Text = "xrTableCell19th";
            this.xrTableCell19th.Weight = 0.061898210517641924D;
            // 
            // xrTableCell20th
            // 
            this.xrTableCell20th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.20")});
            this.xrTableCell20th.Name = "xrTableCell20th";
            this.xrTableCell20th.Text = "xrTableCell20th";
            this.xrTableCell20th.Weight = 0.061898213141230021D;
            // 
            // xrTableCell21st
            // 
            this.xrTableCell21st.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.21")});
            this.xrTableCell21st.Name = "xrTableCell21st";
            this.xrTableCell21st.Text = "xrTableCell21st";
            this.xrTableCell21st.Weight = 0.061898212485333115D;
            // 
            // xrTableCell22nd
            // 
            this.xrTableCell22nd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.22")});
            this.xrTableCell22nd.Name = "xrTableCell22nd";
            this.xrTableCell22nd.Text = "xrTableCell22nd";
            this.xrTableCell22nd.Weight = 0.061898211419500387D;
            // 
            // xrTableCell23rd
            // 
            this.xrTableCell23rd.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.23")});
            this.xrTableCell23rd.Name = "xrTableCell23rd";
            this.xrTableCell23rd.Text = "xrTableCell23rd";
            this.xrTableCell23rd.Weight = 0.06189821141950038D;
            // 
            // xrTableCell24th
            // 
            this.xrTableCell24th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.24")});
            this.xrTableCell24th.Name = "xrTableCell24th";
            this.xrTableCell24th.Text = "xrTableCell24th";
            this.xrTableCell24th.Weight = 0.0618982114195004D;
            // 
            // xrTableCell25th
            // 
            this.xrTableCell25th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.25")});
            this.xrTableCell25th.Name = "xrTableCell25th";
            this.xrTableCell25th.Text = "xrTableCell25th";
            this.xrTableCell25th.Weight = 0.061898211419500394D;
            // 
            // xrTableCell26th
            // 
            this.xrTableCell26th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.26")});
            this.xrTableCell26th.Name = "xrTableCell26th";
            this.xrTableCell26th.Text = "xrTableCell26th";
            this.xrTableCell26th.Weight = 0.061898211419500387D;
            // 
            // xrTableCell27th
            // 
            this.xrTableCell27th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.27")});
            this.xrTableCell27th.Name = "xrTableCell27th";
            this.xrTableCell27th.Text = "xrTableCell27th";
            this.xrTableCell27th.Weight = 0.06189821141950036D;
            // 
            // xrTableCell28th
            // 
            this.xrTableCell28th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.28")});
            this.xrTableCell28th.Name = "xrTableCell28th";
            this.xrTableCell28th.Text = "xrTableCell28th";
            this.xrTableCell28th.Weight = 0.061898211419500394D;
            // 
            // xrTableCell29th
            // 
            this.xrTableCell29th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.29")});
            this.xrTableCell29th.Name = "xrTableCell29th";
            this.xrTableCell29th.Text = "xrTableCell29th";
            this.xrTableCell29th.Weight = 0.061898215559850547D;
            // 
            // xrTableCell30th
            // 
            this.xrTableCell30th.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.30")});
            this.xrTableCell30th.Name = "xrTableCell30th";
            this.xrTableCell30th.Text = "xrTableCell30th";
            this.xrTableCell30th.Weight = 0.0618982136946433D;
            // 
            // xrTableCell31st
            // 
            this.xrTableCell31st.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
                    "_Timesheet_sprptTimeKeepingByMonth_Details.31")});
            this.xrTableCell31st.Name = "xrTableCell31st";
            this.xrTableCell31st.Text = "xrTableCell31st";
            this.xrTableCell31st.Weight = 0.061898339626877644D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableCompany,
            this.xrPictureBoxLogo,
            this.xrLabelHeaderTitle,
            this.xrLblMonth,
            this.xrLblYear});
            this.TopMargin.HeightF = 243.9927F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.ImageUrl = "/Content/Admin/images/logo_report.png";
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(309F, 87.73196F);
            this.xrPictureBoxLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLabelHeaderTitle
            // 
            this.xrLabelHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabelHeaderTitle.ForeColor = System.Drawing.Color.Red;
            this.xrLabelHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 148.8781F);
            this.xrLabelHeaderTitle.Name = "xrLabelHeaderTitle";
            this.xrLabelHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTitle.SizeF = new System.Drawing.SizeF(1454F, 49.32295F);
            this.xrLabelHeaderTitle.StylePriority.UseFont = false;
            this.xrLabelHeaderTitle.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTitle.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTitle.Text = "Montly Time-Keeping Report";
            this.xrLabelHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLblMonth
            // 
            this.xrLblMonth.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLblMonth.LocationFloat = new DevExpress.Utils.PointFloat(627.6919F, 201.2011F);
            this.xrLblMonth.Name = "xrLblMonth";
            this.xrLblMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblMonth.SizeF = new System.Drawing.SizeF(124.4738F, 23F);
            this.xrLblMonth.StylePriority.UseFont = false;
            this.xrLblMonth.StylePriority.UseTextAlignment = false;
            this.xrLblMonth.Text = "Month :";
            this.xrLblMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblYear
            // 
            this.xrLblYear.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLblYear.LocationFloat = new DevExpress.Utils.PointFloat(763.5298F, 201.2011F);
            this.xrLblYear.Name = "xrLblYear";
            this.xrLblYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblYear.SizeF = new System.Drawing.SizeF(82.14087F, 23F);
            this.xrLblYear.StylePriority.UseFont = false;
            this.xrLblYear.StylePriority.UseTextAlignment = false;
            this.xrLblYear.Text = "Year :";
            this.xrLblYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageFooterLeftText,
            this.xrPageFooterRightText});
            this.BottomMargin.HeightF = 23F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageFooterLeftText
            // 
            this.xrPageFooterLeftText.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPageFooterLeftText.Name = "xrPageFooterLeftText";
            this.xrPageFooterLeftText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageFooterLeftText.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageFooterLeftText.SizeF = new System.Drawing.SizeF(328.8873F, 23F);
            this.xrPageFooterLeftText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterLeftText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageFooterRightText
            // 
            this.xrPageFooterRightText.Format = "Page {0} of {1}";
            this.xrPageFooterRightText.LocationFloat = new DevExpress.Utils.PointFloat(1141F, 0F);
            this.xrPageFooterRightText.Name = "xrPageFooterRightText";
            this.xrPageFooterRightText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageFooterRightText.SizeF = new System.Drawing.SizeF(313F, 23F);
            this.xrPageFooterRightText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterRightText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableHeader});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTableHeader
            // 
            this.xrTableHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableHeader.Name = "xrTableHeader";
            this.xrTableHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRowHeader});
            this.xrTableHeader.SizeF = new System.Drawing.SizeF(1454F, 25F);
            this.xrTableHeader.StylePriority.UseBorders = false;
            this.xrTableHeader.StylePriority.UsePadding = false;
            this.xrTableHeader.StylePriority.UseTextAlignment = false;
            this.xrTableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRowHeader
            // 
            this.xrTableRowHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableRowHeader.BorderColor = System.Drawing.Color.White;
            this.xrTableRowHeader.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellHeaderNo,
            this.xrTableCellHeaderEmpCode,
            this.xrTableCellHeaderFullName,
            this.xrTableCellHeaderPosition,
            this.xrTableCellHeader1st,
            this.xrTableCellHeader2nd,
            this.xrTableCellHeader3rd,
            this.xrTableCellHeader4th,
            this.xrTableCellHeader5th,
            this.xrTableCellHeader6th,
            this.xrTableCellHeader7th,
            this.xrTableCellHeader8th,
            this.xrTableCellHeader9th,
            this.xrTableCellHeader10th,
            this.xrTableCellHeader11th,
            this.xrTableCellHeader12th,
            this.xrTableCellHeader13th,
            this.xrTableCellHeader14th,
            this.xrTableCellHeader15th,
            this.xrTableCell1Header16th,
            this.xrTableCellHeader17th,
            this.xrTableCellHeader18th,
            this.xrTableCellHeader19th,
            this.xrTableCellHeader20th,
            this.xrTableCellHeader21st,
            this.xrTableCellHeader22nd,
            this.xrTableCellHeader23rd,
            this.xrTableCellHeader24th,
            this.xrTableCellHeader25th,
            this.xrTableCellHeader26th,
            this.xrTableCellHeader27th,
            this.xrTableCellHeader28th,
            this.xrTableCellHeader29th,
            this.xrTableCellHeader30th,
            this.xrTableCellHeader31st});
            this.xrTableRowHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableRowHeader.ForeColor = System.Drawing.Color.White;
            this.xrTableRowHeader.Name = "xrTableRowHeader";
            this.xrTableRowHeader.StylePriority.UseBackColor = false;
            this.xrTableRowHeader.StylePriority.UseBorderColor = false;
            this.xrTableRowHeader.StylePriority.UseFont = false;
            this.xrTableRowHeader.StylePriority.UseForeColor = false;
            this.xrTableRowHeader.Weight = 1D;
            // 
            // xrTableCellHeaderNo
            // 
            this.xrTableCellHeaderNo.Name = "xrTableCellHeaderNo";
            this.xrTableCellHeaderNo.Text = "No";
            this.xrTableCellHeaderNo.Weight = 0.10376547455295737D;
            // 
            // xrTableCellHeaderEmpCode
            // 
            this.xrTableCellHeaderEmpCode.Name = "xrTableCellHeaderEmpCode";
            this.xrTableCellHeaderEmpCode.Text = "EmpCode";
            this.xrTableCellHeaderEmpCode.Weight = 0.20907855656976704D;
            // 
            // xrTableCellHeaderFullName
            // 
            this.xrTableCellHeaderFullName.Name = "xrTableCellHeaderFullName";
            this.xrTableCellHeaderFullName.Text = "FullName";
            this.xrTableCellHeaderFullName.Weight = 0.38410931876989496D;
            // 
            // xrTableCellHeaderPosition
            // 
            this.xrTableCellHeaderPosition.Name = "xrTableCellHeaderPosition";
            this.xrTableCellHeaderPosition.Text = "Position";
            this.xrTableCellHeaderPosition.Weight = 0.28008044100009555D;
            // 
            // xrTableCellHeader1st
            // 
            this.xrTableCellHeader1st.Name = "xrTableCellHeader1st";
            this.xrTableCellHeader1st.Text = "1";
            this.xrTableCellHeader1st.Weight = 0.061898213141230209D;
            // 
            // xrTableCellHeader2nd
            // 
            this.xrTableCellHeader2nd.Name = "xrTableCellHeader2nd";
            this.xrTableCellHeader2nd.Text = "2";
            this.xrTableCellHeader2nd.Weight = 0.061898210517641924D;
            // 
            // xrTableCellHeader3rd
            // 
            this.xrTableCellHeader3rd.Name = "xrTableCellHeader3rd";
            this.xrTableCellHeader3rd.Text = "3";
            this.xrTableCellHeader3rd.Weight = 0.0618982105176419D;
            // 
            // xrTableCellHeader4th
            // 
            this.xrTableCellHeader4th.Name = "xrTableCellHeader4th";
            this.xrTableCellHeader4th.Text = "4";
            this.xrTableCellHeader4th.Weight = 0.0618982124853331D;
            // 
            // xrTableCellHeader5th
            // 
            this.xrTableCellHeader5th.Name = "xrTableCellHeader5th";
            this.xrTableCellHeader5th.Text = "5";
            this.xrTableCellHeader5th.Weight = 0.061898212485333073D;
            // 
            // xrTableCellHeader6th
            // 
            this.xrTableCellHeader6th.Name = "xrTableCellHeader6th";
            this.xrTableCellHeader6th.Text = "6";
            this.xrTableCellHeader6th.Weight = 0.0618982105176419D;
            // 
            // xrTableCellHeader7th
            // 
            this.xrTableCellHeader7th.Name = "xrTableCellHeader7th";
            this.xrTableCellHeader7th.Text = "7";
            this.xrTableCellHeader7th.Weight = 0.061898210517641918D;
            // 
            // xrTableCellHeader8th
            // 
            this.xrTableCellHeader8th.Name = "xrTableCellHeader8th";
            this.xrTableCellHeader8th.Text = "8";
            this.xrTableCellHeader8th.Weight = 0.061898210517641931D;
            // 
            // xrTableCellHeader9th
            // 
            this.xrTableCellHeader9th.Name = "xrTableCellHeader9th";
            this.xrTableCellHeader9th.Text = "9";
            this.xrTableCellHeader9th.Weight = 0.061898210517641959D;
            // 
            // xrTableCellHeader10th
            // 
            this.xrTableCellHeader10th.Name = "xrTableCellHeader10th";
            this.xrTableCellHeader10th.Text = "10";
            this.xrTableCellHeader10th.Weight = 0.061898210517641904D;
            // 
            // xrTableCellHeader11th
            // 
            this.xrTableCellHeader11th.Name = "xrTableCellHeader11th";
            this.xrTableCellHeader11th.Text = "11";
            this.xrTableCellHeader11th.Weight = 0.061898210517641911D;
            // 
            // xrTableCellHeader12th
            // 
            this.xrTableCellHeader12th.Name = "xrTableCellHeader12th";
            this.xrTableCellHeader12th.Text = "12";
            this.xrTableCellHeader12th.Weight = 0.061898210517642D;
            // 
            // xrTableCellHeader13th
            // 
            this.xrTableCellHeader13th.Name = "xrTableCellHeader13th";
            this.xrTableCellHeader13th.Text = "13";
            this.xrTableCellHeader13th.Weight = 0.061898210517641904D;
            // 
            // xrTableCellHeader14th
            // 
            this.xrTableCellHeader14th.Name = "xrTableCellHeader14th";
            this.xrTableCellHeader14th.Text = "14";
            this.xrTableCellHeader14th.Weight = 0.061898209205847904D;
            // 
            // xrTableCellHeader15th
            // 
            this.xrTableCellHeader15th.Name = "xrTableCellHeader15th";
            this.xrTableCellHeader15th.Text = "15";
            this.xrTableCellHeader15th.Weight = 0.061898210517641848D;
            // 
            // xrTableCell1Header16th
            // 
            this.xrTableCell1Header16th.Name = "xrTableCell1Header16th";
            this.xrTableCell1Header16th.Text = "16";
            this.xrTableCell1Header16th.Weight = 0.061898210517641938D;
            // 
            // xrTableCellHeader17th
            // 
            this.xrTableCellHeader17th.Name = "xrTableCellHeader17th";
            this.xrTableCellHeader17th.Text = "17";
            this.xrTableCellHeader17th.Weight = 0.061898210517641883D;
            // 
            // xrTableCellHeader18th
            // 
            this.xrTableCellHeader18th.Name = "xrTableCellHeader18th";
            this.xrTableCellHeader18th.Text = "18";
            this.xrTableCellHeader18th.Weight = 0.061898213141230195D;
            // 
            // xrTableCellHeader19th
            // 
            this.xrTableCellHeader19th.Name = "xrTableCellHeader19th";
            this.xrTableCellHeader19th.Text = "19";
            this.xrTableCellHeader19th.Weight = 0.061898210517641924D;
            // 
            // xrTableCellHeader20th
            // 
            this.xrTableCellHeader20th.Name = "xrTableCellHeader20th";
            this.xrTableCellHeader20th.Text = "20";
            this.xrTableCellHeader20th.Weight = 0.061898213141230021D;
            // 
            // xrTableCellHeader21st
            // 
            this.xrTableCellHeader21st.Name = "xrTableCellHeader21st";
            this.xrTableCellHeader21st.Text = "21";
            this.xrTableCellHeader21st.Weight = 0.061898212485333115D;
            // 
            // xrTableCellHeader22nd
            // 
            this.xrTableCellHeader22nd.Name = "xrTableCellHeader22nd";
            this.xrTableCellHeader22nd.Text = "22";
            this.xrTableCellHeader22nd.Weight = 0.061898211419500387D;
            // 
            // xrTableCellHeader23rd
            // 
            this.xrTableCellHeader23rd.Name = "xrTableCellHeader23rd";
            this.xrTableCellHeader23rd.Text = "23";
            this.xrTableCellHeader23rd.Weight = 0.06189821141950038D;
            // 
            // xrTableCellHeader24th
            // 
            this.xrTableCellHeader24th.Name = "xrTableCellHeader24th";
            this.xrTableCellHeader24th.Text = "24";
            this.xrTableCellHeader24th.Weight = 0.0618982114195004D;
            // 
            // xrTableCellHeader25th
            // 
            this.xrTableCellHeader25th.Name = "xrTableCellHeader25th";
            this.xrTableCellHeader25th.Text = "25";
            this.xrTableCellHeader25th.Weight = 0.061898211419500394D;
            // 
            // xrTableCellHeader26th
            // 
            this.xrTableCellHeader26th.Name = "xrTableCellHeader26th";
            this.xrTableCellHeader26th.Text = "26";
            this.xrTableCellHeader26th.Weight = 0.061898211419500387D;
            // 
            // xrTableCellHeader27th
            // 
            this.xrTableCellHeader27th.Name = "xrTableCellHeader27th";
            this.xrTableCellHeader27th.Text = "27";
            this.xrTableCellHeader27th.Weight = 0.06189821141950036D;
            // 
            // xrTableCellHeader28th
            // 
            this.xrTableCellHeader28th.Name = "xrTableCellHeader28th";
            this.xrTableCellHeader28th.Text = "28";
            this.xrTableCellHeader28th.Weight = 0.061898211419500394D;
            // 
            // xrTableCellHeader29th
            // 
            this.xrTableCellHeader29th.Name = "xrTableCellHeader29th";
            this.xrTableCellHeader29th.Text = "29";
            this.xrTableCellHeader29th.Weight = 0.061898215559850547D;
            // 
            // xrTableCellHeader30th
            // 
            this.xrTableCellHeader30th.Name = "xrTableCellHeader30th";
            this.xrTableCellHeader30th.Text = "30";
            this.xrTableCellHeader30th.Weight = 0.0618982136946433D;
            // 
            // xrTableCellHeader31st
            // 
            this.xrTableCellHeader31st.Name = "xrTableCellHeader31st";
            this.xrTableCellHeader31st.Text = "31";
            this.xrTableCellHeader31st.Weight = 0.061898339626877644D;
            // 
            // monthlyTimeKeepingDataSet1
            // 
            this.monthlyTimeKeepingDataSet1.DataSetName = "MonthlyTimeKeepingDataSet";
            this.monthlyTimeKeepingDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // timesheet_sprptTimeKeepingByMonth_MasterTableAdapter
            // 
            this.timesheet_sprptTimeKeepingByMonth_MasterTableAdapter.ClearBeforeFill = true;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataAdapter = this.timesheet_sprptTimeKeepingByMonth_DetailsTableAdapter;
            this.DetailReport.DataMember = "Timesheet_sprptTimeKeepingByMonth_Master.Timesheet_sprptTimeKeepingByMonth_Master" +
    "_Timesheet_sprptTimeKeepingByMonth_Details";
            this.DetailReport.DataSource = this.monthlyTimeKeepingDataSet1;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableDetails});
            this.Detail1.HeightF = 25F;
            this.Detail1.Name = "Detail1";
            // 
            // timesheet_sprptTimeKeepingByMonth_DetailsTableAdapter
            // 
            this.timesheet_sprptTimeKeepingByMonth_DetailsTableAdapter.ClearBeforeFill = true;
            // 
            // xrTableCompany
            // 
            this.xrTableCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCompany.LocationFloat = new DevExpress.Utils.PointFloat(971.0896F, 0F);
            this.xrTableCompany.Name = "xrTableCompany";
            this.xrTableCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCompany.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5});
            this.xrTableCompany.SizeF = new System.Drawing.SizeF(472.9104F, 125F);
            this.xrTableCompany.StylePriority.UseBorders = false;
            this.xrTableCompany.StylePriority.UsePadding = false;
            this.xrTableCompany.StylePriority.UseTextAlignment = false;
            this.xrTableCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelCompanyName,
            this.xrTableCellCompanyName});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCellLabelCompanyName
            // 
            this.xrTableCellLabelCompanyName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.xrTableCellLabelCompanyName.BorderColor = System.Drawing.Color.White;
            this.xrTableCellLabelCompanyName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLabelCompanyName.BorderWidth = 3;
            this.xrTableCellLabelCompanyName.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelCompanyName.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelCompanyName.Name = "xrTableCellLabelCompanyName";
            this.xrTableCellLabelCompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelCompanyName.StylePriority.UseBackColor = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseBorders = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseFont = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseForeColor = false;
            this.xrTableCellLabelCompanyName.Text = "Headquarter :";
            this.xrTableCellLabelCompanyName.Weight = 0.40158846975429952D;
            // 
            // xrTableCellCompanyName
            // 
            this.xrTableCellCompanyName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.xrTableCellCompanyName.BorderColor = System.Drawing.Color.White;
            this.xrTableCellCompanyName.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTableCellCompanyName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCompanyName.BorderWidth = 3;
            this.xrTableCellCompanyName.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellCompanyName.Name = "xrTableCellCompanyName";
            this.xrTableCellCompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellCompanyName.StylePriority.UseBackColor = false;
            this.xrTableCellCompanyName.StylePriority.UseBorderColor = false;
            this.xrTableCellCompanyName.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellCompanyName.StylePriority.UseBorders = false;
            this.xrTableCellCompanyName.StylePriority.UseBorderWidth = false;
            this.xrTableCellCompanyName.StylePriority.UseFont = false;
            this.xrTableCellCompanyName.Text = "Unit Corp";
            this.xrTableCellCompanyName.Weight = 1.5984115302457005D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelAddress,
            this.xrTableCellAddress});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCellLabelAddress
            // 
            this.xrTableCellLabelAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.xrTableCellLabelAddress.BorderColor = System.Drawing.Color.White;
            this.xrTableCellLabelAddress.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLabelAddress.BorderWidth = 3;
            this.xrTableCellLabelAddress.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelAddress.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelAddress.Name = "xrTableCellLabelAddress";
            this.xrTableCellLabelAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelAddress.StylePriority.UseBackColor = false;
            this.xrTableCellLabelAddress.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelAddress.StylePriority.UseBorders = false;
            this.xrTableCellLabelAddress.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelAddress.StylePriority.UseFont = false;
            this.xrTableCellLabelAddress.StylePriority.UseForeColor = false;
            this.xrTableCellLabelAddress.Text = "Address :";
            this.xrTableCellLabelAddress.Weight = 0.40158846975429952D;
            // 
            // xrTableCellAddress
            // 
            this.xrTableCellAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.xrTableCellAddress.BorderColor = System.Drawing.Color.White;
            this.xrTableCellAddress.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTableCellAddress.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellAddress.BorderWidth = 3;
            this.xrTableCellAddress.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellAddress.Name = "xrTableCellAddress";
            this.xrTableCellAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellAddress.StylePriority.UseBackColor = false;
            this.xrTableCellAddress.StylePriority.UseBorderColor = false;
            this.xrTableCellAddress.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellAddress.StylePriority.UseBorders = false;
            this.xrTableCellAddress.StylePriority.UseBorderWidth = false;
            this.xrTableCellAddress.StylePriority.UseFont = false;
            this.xrTableCellAddress.Text = "157 Nguyen Thi Thap, Tan Phu Ward, District 7, HCM";
            this.xrTableCellAddress.Weight = 1.5984115302457005D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelPhone,
            this.xrTableCellPhone});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCellLabelPhone
            // 
            this.xrTableCellLabelPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.xrTableCellLabelPhone.BorderColor = System.Drawing.Color.White;
            this.xrTableCellLabelPhone.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLabelPhone.BorderWidth = 3;
            this.xrTableCellLabelPhone.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelPhone.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelPhone.Name = "xrTableCellLabelPhone";
            this.xrTableCellLabelPhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelPhone.StylePriority.UseBackColor = false;
            this.xrTableCellLabelPhone.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelPhone.StylePriority.UseBorders = false;
            this.xrTableCellLabelPhone.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelPhone.StylePriority.UseFont = false;
            this.xrTableCellLabelPhone.StylePriority.UseForeColor = false;
            this.xrTableCellLabelPhone.Text = "Phone :";
            this.xrTableCellLabelPhone.Weight = 0.40159227285262478D;
            // 
            // xrTableCellPhone
            // 
            this.xrTableCellPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.xrTableCellPhone.BorderColor = System.Drawing.Color.White;
            this.xrTableCellPhone.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTableCellPhone.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPhone.BorderWidth = 3;
            this.xrTableCellPhone.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellPhone.Name = "xrTableCellPhone";
            this.xrTableCellPhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellPhone.StylePriority.UseBackColor = false;
            this.xrTableCellPhone.StylePriority.UseBorderColor = false;
            this.xrTableCellPhone.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellPhone.StylePriority.UseBorders = false;
            this.xrTableCellPhone.StylePriority.UseBorderWidth = false;
            this.xrTableCellPhone.StylePriority.UseFont = false;
            this.xrTableCellPhone.Text = "(84-8) 37 402 388 - 37 402 908";
            this.xrTableCellPhone.Weight = 1.5984077271473753D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelFax,
            this.xrTableCellFax});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCellLabelFax
            // 
            this.xrTableCellLabelFax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.xrTableCellLabelFax.BorderColor = System.Drawing.Color.White;
            this.xrTableCellLabelFax.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLabelFax.BorderWidth = 3;
            this.xrTableCellLabelFax.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelFax.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelFax.Name = "xrTableCellLabelFax";
            this.xrTableCellLabelFax.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelFax.StylePriority.UseBackColor = false;
            this.xrTableCellLabelFax.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelFax.StylePriority.UseBorders = false;
            this.xrTableCellLabelFax.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelFax.StylePriority.UseFont = false;
            this.xrTableCellLabelFax.StylePriority.UseForeColor = false;
            this.xrTableCellLabelFax.Text = "Fax :";
            this.xrTableCellLabelFax.Weight = 0.40159227285262478D;
            // 
            // xrTableCellFax
            // 
            this.xrTableCellFax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.xrTableCellFax.BorderColor = System.Drawing.Color.White;
            this.xrTableCellFax.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTableCellFax.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFax.BorderWidth = 3;
            this.xrTableCellFax.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellFax.Name = "xrTableCellFax";
            this.xrTableCellFax.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellFax.StylePriority.UseBackColor = false;
            this.xrTableCellFax.StylePriority.UseBorderColor = false;
            this.xrTableCellFax.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellFax.StylePriority.UseBorders = false;
            this.xrTableCellFax.StylePriority.UseBorderWidth = false;
            this.xrTableCellFax.StylePriority.UseFont = false;
            this.xrTableCellFax.Text = "(84-8) 37 402 385";
            this.xrTableCellFax.Weight = 1.5984077271473753D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelEmail,
            this.xrTableCellEmail});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCellLabelEmail
            // 
            this.xrTableCellLabelEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.xrTableCellLabelEmail.BorderColor = System.Drawing.Color.White;
            this.xrTableCellLabelEmail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLabelEmail.BorderWidth = 3;
            this.xrTableCellLabelEmail.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelEmail.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelEmail.Name = "xrTableCellLabelEmail";
            this.xrTableCellLabelEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelEmail.StylePriority.UseBackColor = false;
            this.xrTableCellLabelEmail.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelEmail.StylePriority.UseBorders = false;
            this.xrTableCellLabelEmail.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelEmail.StylePriority.UseFont = false;
            this.xrTableCellLabelEmail.StylePriority.UseForeColor = false;
            this.xrTableCellLabelEmail.Text = "Email :";
            this.xrTableCellLabelEmail.Weight = 0.40159227285262478D;
            // 
            // xrTableCellEmail
            // 
            this.xrTableCellEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.xrTableCellEmail.BorderColor = System.Drawing.Color.White;
            this.xrTableCellEmail.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTableCellEmail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellEmail.BorderWidth = 3;
            this.xrTableCellEmail.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellEmail.Name = "xrTableCellEmail";
            this.xrTableCellEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellEmail.StylePriority.UseBackColor = false;
            this.xrTableCellEmail.StylePriority.UseBorderColor = false;
            this.xrTableCellEmail.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellEmail.StylePriority.UseBorders = false;
            this.xrTableCellEmail.StylePriority.UseBorderWidth = false;
            this.xrTableCellEmail.StylePriority.UseFont = false;
            this.xrTableCellEmail.Text = "info@unit.com.vn";
            this.xrTableCellEmail.Weight = 1.5984077271473753D;
            // 
            // MonthlyTimeKeepingXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.DetailReport});
            this.DataAdapter = this.timesheet_sprptTimeKeepingByMonth_MasterTableAdapter;
            this.DataMember = "Timesheet_sprptTimeKeepingByMonth_Master";
            this.DataSource = this.monthlyTimeKeepingDataSet1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 244, 23);
            this.PageHeight = 1169;
            this.PageWidth = 1654;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTableMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyTimeKeepingDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
