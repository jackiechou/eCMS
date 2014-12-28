using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for OTByMonth
/// </summary>
public class OTByMonthXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private XRLabel xrLabelHeaderPosition;
    private XRLabel xrLabelHeaderFullName;
    private XRLabel xrLabelHeaderEmpCode;
    private XRLabel xrLabelHeaderNo;
    private XRLabel xrLabelHeaderTitle;
    private XRLabel xrLblMonth;
    private XRLabel xrLblYear;
    private XRLabel xrLabelDepartmentName;
    private BottomMarginBand BottomMargin;
    private TopMarginBand TopMargin;
    private DetailBand Detail;
    private XRLabel xrLabelMonth;
    private XRLabel xrLabelHeaderOTNDD;
    private XRLabel xrLabelHeaderOTWD;
    private XRLabel xrLabelHeaderOTHDD;
    private XRLabel xrLabelHeaderOTWN;
    private XRLabel xrLabelHeaderOTNDN;
    private XRLabel xrLabelHeaderOTHN;
    private XRLabel xrLabelHeaderOTTotalByMonth;
    private XRLabel xrLabelHeaderOTTotalsFromFirstMonthToPresent;
    private XRLabel xrLabelHeaderOTTotalByYear;
    private XRLabel xrLabelHeaderOTRestTotal;
    private PageHeaderBand PageHeader;
    private ReportHeaderBand ReportHeader;
    private XRPictureBox xrPictureBoxLogo;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageFooterLeftText;
    private XRPageInfo xrPageFooterRightText;
    private XRTable xrTableReport;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCellNo;
    private XRTableCell xrTableCellEmpCode;
    private XRTableCell xrTableCellFullName;
    private XRTableCell xrTableCellPosition;
    private XRTableCell xrTableCellOTNDD;
    private XRTableCell xrTableCellOTWD;
    private XRTableCell xrTableCellOTHDD;
    private XRTableCell xrTableCellOTNDN;
    private XRTableCell xrTableCellOTWN;
    private XRTableCell xrTableCellOTHN;
    private XRTableCell xrTableCellOTTotalByMonth;
    private XRTableCell xrTableCellOTTotalsFromFirstMonthToSelectedMonth;
    private XRTableCell xrTableCellOTTotalByYear;
    private XRTableCell xrTableCellOTRestTotal;
    private Eagle.WebApp.Areas.Admin.Reports.TS.OTByMonth.MonthlyOTDataSet monthlyOTDataSet1;
    
    public DetailReportBand DetailReport;
    private DetailBand Detail1;
    private Eagle.WebApp.Areas.Admin.Reports.TS.OTByMonth.MonthlyOTDataSetTableAdapters.Timesheet_sprptOTByMonth_DetailsTableAdapter timesheet_sprptOTByMonth_DetailsTableAdapter;
    private XRTable xrTableCompany;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCellLabelCompanyName;
    private XRTableCell xrTableCellCompanyName;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCellLabelAddress;
    private XRTableCell xrTableCellAddress;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCellLabelPhone;
    private XRTableCell xrTableCellPhone;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCellLabelFax;
    private XRTableCell xrTableCellFax;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCellLabelEmail;
    private XRTableCell xrTableCellEmail;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OTByMonthXtraReport()
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
            this.xrLabelDepartmentName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderEmpCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderFullName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderPosition = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTableReport = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTNDD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTWD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTHDD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTNDN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTWN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTHN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTTotalByMonth = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTTotalByYear = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTRestTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTNDD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTWD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTHDD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTNDN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTWN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTHN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTTotalByMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTTotalByYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTRestTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTableCompany = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelCompanyName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCompanyName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelPhone = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPhone = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelFax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelEmail = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmail = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageFooterLeftText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageFooterRightText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.monthlyOTDataSet1 = new Eagle.WebApp.Areas.Admin.Reports.TS.OTByMonth.MonthlyOTDataSet();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.timesheet_sprptOTByMonth_DetailsTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.OTByMonth.MonthlyOTDataSetTableAdapters.Timesheet_sprptOTByMonth_DetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyOTDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // xrLabelDepartmentName
            // 
            this.xrLabelDepartmentName.BackColor = System.Drawing.Color.SteelBlue;
            this.xrLabelDepartmentName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelDepartmentName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelDepartmentName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.DepartmentName")});
            this.xrLabelDepartmentName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelDepartmentName.ForeColor = System.Drawing.Color.White;
            this.xrLabelDepartmentName.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 0F);
            this.xrLabelDepartmentName.Name = "xrLabelDepartmentName";
            this.xrLabelDepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelDepartmentName.SizeF = new System.Drawing.SizeF(1451.216F, 26.04167F);
            this.xrLabelDepartmentName.StylePriority.UseBackColor = false;
            this.xrLabelDepartmentName.StylePriority.UseBorderColor = false;
            this.xrLabelDepartmentName.StylePriority.UseBorders = false;
            this.xrLabelDepartmentName.StylePriority.UseFont = false;
            this.xrLabelDepartmentName.StylePriority.UseForeColor = false;
            this.xrLabelDepartmentName.StylePriority.UseTextAlignment = false;
            this.xrLabelDepartmentName.Text = "xrLabelDepartmentName";
            this.xrLabelDepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblYear
            // 
            this.xrLblYear.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLblYear.LocationFloat = new DevExpress.Utils.PointFloat(766.7422F, 205.3677F);
            this.xrLblYear.Name = "xrLblYear";
            this.xrLblYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblYear.SizeF = new System.Drawing.SizeF(82.14087F, 23F);
            this.xrLblYear.StylePriority.UseFont = false;
            this.xrLblYear.StylePriority.UseTextAlignment = false;
            this.xrLblYear.Text = "Year :";
            this.xrLblYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblMonth
            // 
            this.xrLblMonth.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLblMonth.LocationFloat = new DevExpress.Utils.PointFloat(630.9043F, 205.3677F);
            this.xrLblMonth.Name = "xrLblMonth";
            this.xrLblMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblMonth.SizeF = new System.Drawing.SizeF(124.4738F, 23F);
            this.xrLblMonth.StylePriority.UseFont = false;
            this.xrLblMonth.StylePriority.UseTextAlignment = false;
            this.xrLblMonth.Text = "Month :";
            this.xrLblMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabelHeaderTitle
            // 
            this.xrLabelHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabelHeaderTitle.ForeColor = System.Drawing.Color.Red;
            this.xrLabelHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 152.3153F);
            this.xrLabelHeaderTitle.Name = "xrLabelHeaderTitle";
            this.xrLabelHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTitle.SizeF = new System.Drawing.SizeF(1451.216F, 49.32292F);
            this.xrLabelHeaderTitle.StylePriority.UseFont = false;
            this.xrLabelHeaderTitle.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTitle.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTitle.Text = "Montly OverTime Report";
            this.xrLabelHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNo
            // 
            this.xrLabelHeaderNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNo.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNo.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNo.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNo.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabelHeaderNo.Name = "xrLabelHeaderNo";
            this.xrLabelHeaderNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNo.SizeF = new System.Drawing.SizeF(50.38144F, 92.13864F);
            this.xrLabelHeaderNo.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNo.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNo.StylePriority.UseBorders = false;
            this.xrLabelHeaderNo.StylePriority.UseFont = false;
            this.xrLabelHeaderNo.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNo.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNo.Text = "No.";
            this.xrLabelHeaderNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderEmpCode
            // 
            this.xrLabelHeaderEmpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderEmpCode.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderEmpCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderEmpCode.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderEmpCode.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderEmpCode.LocationFloat = new DevExpress.Utils.PointFloat(50.38144F, 0.001209298F);
            this.xrLabelHeaderEmpCode.Name = "xrLabelHeaderEmpCode";
            this.xrLabelHeaderEmpCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderEmpCode.SizeF = new System.Drawing.SizeF(100.9932F, 92.13866F);
            this.xrLabelHeaderEmpCode.StylePriority.UseBackColor = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseBorders = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseFont = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseForeColor = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderEmpCode.Text = "Code";
            this.xrLabelHeaderEmpCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderFullName
            // 
            this.xrLabelHeaderFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderFullName.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderFullName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderFullName.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderFullName.LocationFloat = new DevExpress.Utils.PointFloat(151.3746F, 0.0007311503F);
            this.xrLabelHeaderFullName.Name = "xrLabelHeaderFullName";
            this.xrLabelHeaderFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderFullName.SizeF = new System.Drawing.SizeF(177.5127F, 92.13864F);
            this.xrLabelHeaderFullName.StylePriority.UseBackColor = false;
            this.xrLabelHeaderFullName.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderFullName.StylePriority.UseBorders = false;
            this.xrLabelHeaderFullName.StylePriority.UseFont = false;
            this.xrLabelHeaderFullName.StylePriority.UseForeColor = false;
            this.xrLabelHeaderFullName.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderFullName.Text = "Full Name";
            this.xrLabelHeaderFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderPosition
            // 
            this.xrLabelHeaderPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderPosition.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderPosition.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderPosition.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderPosition.LocationFloat = new DevExpress.Utils.PointFloat(328.8882F, 0.001207987F);
            this.xrLabelHeaderPosition.Name = "xrLabelHeaderPosition";
            this.xrLabelHeaderPosition.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderPosition.SizeF = new System.Drawing.SizeF(138.9033F, 92.13866F);
            this.xrLabelHeaderPosition.StylePriority.UseBackColor = false;
            this.xrLabelHeaderPosition.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderPosition.StylePriority.UseBorders = false;
            this.xrLabelHeaderPosition.StylePriority.UseFont = false;
            this.xrLabelHeaderPosition.StylePriority.UseForeColor = false;
            this.xrLabelHeaderPosition.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderPosition.Text = "Position";
            this.xrLabelHeaderPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.BottomMargin.HeightF = 2F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseFont = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.TopMargin.HeightF = 26F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseFont = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelDepartmentName});
            this.Detail.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.Detail.HeightF = 26.04167F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTableReport
            // 
            this.xrTableReport.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableReport.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableReport.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableReport.Name = "xrTableReport";
            this.xrTableReport.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableReport.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTableReport.SizeF = new System.Drawing.SizeF(1452.258F, 26.04167F);
            this.xrTableReport.StylePriority.UseBorderColor = false;
            this.xrTableReport.StylePriority.UseBorders = false;
            this.xrTableReport.StylePriority.UsePadding = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.BorderColor = System.Drawing.Color.Gray;
            this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellNo,
            this.xrTableCellEmpCode,
            this.xrTableCellFullName,
            this.xrTableCellPosition,
            this.xrTableCellOTNDD,
            this.xrTableCellOTWD,
            this.xrTableCellOTHDD,
            this.xrTableCellOTNDN,
            this.xrTableCellOTWN,
            this.xrTableCellOTHN,
            this.xrTableCellOTTotalByMonth,
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth,
            this.xrTableCellOTTotalByYear,
            this.xrTableCellOTRestTotal});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseBorderColor = false;
            this.xrTableRow1.StylePriority.UseBorders = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCellNo
            // 
            this.xrTableCellNo.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.Seq")});
            this.xrTableCellNo.Name = "xrTableCellNo";
            this.xrTableCellNo.StylePriority.UseBorderColor = false;
            this.xrTableCellNo.StylePriority.UseBorders = false;
            this.xrTableCellNo.StylePriority.UseTextAlignment = false;
            this.xrTableCellNo.Text = "xrTableCellNo";
            this.xrTableCellNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellNo.Weight = 0.097305349470250838D;
            // 
            // xrTableCellEmpCode
            // 
            this.xrTableCellEmpCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellEmpCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellEmpCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.EmpCode")});
            this.xrTableCellEmpCode.Name = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.StylePriority.UseBorderColor = false;
            this.xrTableCellEmpCode.StylePriority.UseBorders = false;
            this.xrTableCellEmpCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellEmpCode.Text = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellEmpCode.Weight = 0.19505546943083579D;
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFullName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.FullName")});
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.StylePriority.UseBorderColor = false;
            this.xrTableCellFullName.StylePriority.UseBorders = false;
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.Text = "xrTableCellFullName";
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellFullName.Weight = 0.3428433092313698D;
            // 
            // xrTableCellPosition
            // 
            this.xrTableCellPosition.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPosition.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.PositionName")});
            this.xrTableCellPosition.Name = "xrTableCellPosition";
            this.xrTableCellPosition.StylePriority.UseBorderColor = false;
            this.xrTableCellPosition.StylePriority.UseBorders = false;
            this.xrTableCellPosition.StylePriority.UseTextAlignment = false;
            this.xrTableCellPosition.Text = "xrTableCellPosition";
            this.xrTableCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellPosition.Weight = 0.26827602735579775D;
            // 
            // xrTableCellOTNDD
            // 
            this.xrTableCellOTNDD.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTNDD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTNDD.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.TotalHoursOfDayOTInNormalDays", "{0:#,##0.0}")});
            this.xrTableCellOTNDD.Name = "xrTableCellOTNDD";
            this.xrTableCellOTNDD.StylePriority.UseBorderColor = false;
            this.xrTableCellOTNDD.StylePriority.UseBorders = false;
            this.xrTableCellOTNDD.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTNDD.Text = "xrTableCellOTNDD";
            this.xrTableCellOTNDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTNDD.Weight = 0.19308662812858668D;
            // 
            // xrTableCellOTWD
            // 
            this.xrTableCellOTWD.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTWD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTWD.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.TotalHoursOfDayOTInWeekend", "{0:#,##0.0}")});
            this.xrTableCellOTWD.Name = "xrTableCellOTWD";
            this.xrTableCellOTWD.StylePriority.UseBorderColor = false;
            this.xrTableCellOTWD.StylePriority.UseBorders = false;
            this.xrTableCellOTWD.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTWD.Text = "xrTableCellOTWD";
            this.xrTableCellOTWD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTWD.Weight = 0.18059247635080772D;
            // 
            // xrTableCellOTHDD
            // 
            this.xrTableCellOTHDD.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTHDD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTHDD.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.TotalHoursOfDayOTInHolidays")});
            this.xrTableCellOTHDD.Name = "xrTableCellOTHDD";
            this.xrTableCellOTHDD.StylePriority.UseBorderColor = false;
            this.xrTableCellOTHDD.StylePriority.UseBorders = false;
            this.xrTableCellOTHDD.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTHDD.Text = "xrTableCellOTHDD";
            this.xrTableCellOTHDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTHDD.Weight = 0.15340978053485802D;
            // 
            // xrTableCellOTNDN
            // 
            this.xrTableCellOTNDN.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTNDN.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTNDN.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.TotalHoursOfNightOTInNormalDays", "{0:#,##0.0}")});
            this.xrTableCellOTNDN.Name = "xrTableCellOTNDN";
            this.xrTableCellOTNDN.StylePriority.UseBorderColor = false;
            this.xrTableCellOTNDN.StylePriority.UseBorders = false;
            this.xrTableCellOTNDN.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTNDN.Text = "xrTableCellOTNDN";
            this.xrTableCellOTNDN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTNDN.Weight = 0.18912439523030028D;
            // 
            // xrTableCellOTWN
            // 
            this.xrTableCellOTWN.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTWN.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTWN.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.TotalHoursOfNightOTInWeekend", "{0:#,##0.0}")});
            this.xrTableCellOTWN.Name = "xrTableCellOTWN";
            this.xrTableCellOTWN.StylePriority.UseBorderColor = false;
            this.xrTableCellOTWN.StylePriority.UseBorders = false;
            this.xrTableCellOTWN.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTWN.Text = "xrTableCellOTWN";
            this.xrTableCellOTWN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTWN.Weight = 0.16015627667204518D;
            // 
            // xrTableCellOTHN
            // 
            this.xrTableCellOTHN.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTHN.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTHN.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.TotalHoursOfNightOTInHolidays", "{0:#,##0.0}")});
            this.xrTableCellOTHN.Name = "xrTableCellOTHN";
            this.xrTableCellOTHN.StylePriority.UseBorderColor = false;
            this.xrTableCellOTHN.StylePriority.UseBorders = false;
            this.xrTableCellOTHN.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTHN.Text = "xrTableCellOTHN";
            this.xrTableCellOTHN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTHN.Weight = 0.15851547912184172D;
            // 
            // xrTableCellOTTotalByMonth
            // 
            this.xrTableCellOTTotalByMonth.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTTotalByMonth.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTTotalByMonth.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.TotalHoursOfMonthOT", "{0:#,##0.0}")});
            this.xrTableCellOTTotalByMonth.Name = "xrTableCellOTTotalByMonth";
            this.xrTableCellOTTotalByMonth.StylePriority.UseBorderColor = false;
            this.xrTableCellOTTotalByMonth.StylePriority.UseBorders = false;
            this.xrTableCellOTTotalByMonth.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTTotalByMonth.Text = "xrTableCellOTTotalByMonth";
            this.xrTableCellOTTotalByMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTTotalByMonth.Weight = 0.23508072590830861D;
            // 
            // xrTableCellOTTotalsFromFirstMonthToSelectedMonth
            // 
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.OTTotalsFromFirstMonthToSelectedMonth", "{0:#,##0.0}")});
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.Name = "xrTableCellOTTotalsFromFirstMonthToSelectedMonth";
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.StylePriority.UseBorderColor = false;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.StylePriority.UseBorders = false;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.Text = "xrTableCellOTTotalsFromFirstMonthToSelectedMonth";
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.Weight = 0.24624795160249377D;
            // 
            // xrTableCellOTTotalByYear
            // 
            this.xrTableCellOTTotalByYear.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTTotalByYear.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTTotalByYear.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.TotalHoursOfYear", "{0:#,##0.0}")});
            this.xrTableCellOTTotalByYear.Name = "xrTableCellOTTotalByYear";
            this.xrTableCellOTTotalByYear.StylePriority.UseBorderColor = false;
            this.xrTableCellOTTotalByYear.StylePriority.UseBorders = false;
            this.xrTableCellOTTotalByYear.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTTotalByYear.Text = "xrTableCellOTTotalByYear";
            this.xrTableCellOTTotalByYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTTotalByYear.Weight = 0.1931363817802835D;
            // 
            // xrTableCellOTRestTotal
            // 
            this.xrTableCellOTRestTotal.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTRestTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
                    "ByMonth_Details.OTRestTotal", "{0:#,##0.0}")});
            this.xrTableCellOTRestTotal.Name = "xrTableCellOTRestTotal";
            this.xrTableCellOTRestTotal.StylePriority.UseBorderColor = false;
            this.xrTableCellOTRestTotal.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTRestTotal.Text = "xrTableCellOTRestTotal";
            this.xrTableCellOTRestTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTRestTotal.Weight = 0.19202183716123744D;
            // 
            // xrLabelMonth
            // 
            this.xrLabelMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelMonth.BorderColor = System.Drawing.Color.White;
            this.xrLabelMonth.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelMonth.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelMonth.ForeColor = System.Drawing.Color.White;
            this.xrLabelMonth.LocationFloat = new DevExpress.Utils.PointFloat(468.2915F, 0F);
            this.xrLabelMonth.Name = "xrLabelMonth";
            this.xrLabelMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelMonth.SizeF = new System.Drawing.SizeF(535.3284F, 32.92407F);
            this.xrLabelMonth.StylePriority.UseBackColor = false;
            this.xrLabelMonth.StylePriority.UseBorderColor = false;
            this.xrLabelMonth.StylePriority.UseBorders = false;
            this.xrLabelMonth.StylePriority.UseFont = false;
            this.xrLabelMonth.StylePriority.UseForeColor = false;
            this.xrLabelMonth.StylePriority.UseTextAlignment = false;
            this.xrLabelMonth.Text = "Month";
            this.xrLabelMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTNDD
            // 
            this.xrLabelHeaderOTNDD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTNDD.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTNDD.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTNDD.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTNDD.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTNDD.LocationFloat = new DevExpress.Utils.PointFloat(468.2915F, 32.92379F);
            this.xrLabelHeaderOTNDD.Multiline = true;
            this.xrLabelHeaderOTNDD.Name = "xrLabelHeaderOTNDD";
            this.xrLabelHeaderOTNDD.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTNDD.SizeF = new System.Drawing.SizeF(99.47375F, 59.21457F);
            this.xrLabelHeaderOTNDD.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTNDD.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTNDD.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTNDD.StylePriority.UseFont = false;
            this.xrLabelHeaderOTNDD.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTNDD.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTNDD.Text = "OT Normal days (day)";
            this.xrLabelHeaderOTNDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTWD
            // 
            this.xrLabelHeaderOTWD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTWD.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTWD.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTWD.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTWD.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTWD.LocationFloat = new DevExpress.Utils.PointFloat(567.7653F, 32.92402F);
            this.xrLabelHeaderOTWD.Multiline = true;
            this.xrLabelHeaderOTWD.Name = "xrLabelHeaderOTWD";
            this.xrLabelHeaderOTWD.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTWD.SizeF = new System.Drawing.SizeF(93.5047F, 59.21454F);
            this.xrLabelHeaderOTWD.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTWD.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTWD.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTWD.StylePriority.UseFont = false;
            this.xrLabelHeaderOTWD.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTWD.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTWD.Text = "OT Weekend(day)";
            this.xrLabelHeaderOTWD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTHDD
            // 
            this.xrLabelHeaderOTHDD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTHDD.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTHDD.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTHDD.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTHDD.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTHDD.LocationFloat = new DevExpress.Utils.PointFloat(661.2698F, 32.92405F);
            this.xrLabelHeaderOTHDD.Multiline = true;
            this.xrLabelHeaderOTHDD.Name = "xrLabelHeaderOTHDD";
            this.xrLabelHeaderOTHDD.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTHDD.SizeF = new System.Drawing.SizeF(79.43073F, 59.21457F);
            this.xrLabelHeaderOTHDD.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTHDD.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTHDD.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTHDD.StylePriority.UseFont = false;
            this.xrLabelHeaderOTHDD.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTHDD.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTHDD.Text = "OT Holidays\r\n(day)";
            this.xrLabelHeaderOTHDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTNDN
            // 
            this.xrLabelHeaderOTNDN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTNDN.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTNDN.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTNDN.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTNDN.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTNDN.LocationFloat = new DevExpress.Utils.PointFloat(740.7007F, 32.92405F);
            this.xrLabelHeaderOTNDN.Multiline = true;
            this.xrLabelHeaderOTNDN.Name = "xrLabelHeaderOTNDN";
            this.xrLabelHeaderOTNDN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTNDN.SizeF = new System.Drawing.SizeF(97.922F, 59.21451F);
            this.xrLabelHeaderOTNDN.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTNDN.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTNDN.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTNDN.StylePriority.UseFont = false;
            this.xrLabelHeaderOTNDN.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTNDN.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTNDN.Text = "OT Normal days (night)";
            this.xrLabelHeaderOTNDN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTWN
            // 
            this.xrLabelHeaderOTWN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTWN.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTWN.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTWN.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTWN.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTWN.LocationFloat = new DevExpress.Utils.PointFloat(838.623F, 32.9237F);
            this.xrLabelHeaderOTWN.Multiline = true;
            this.xrLabelHeaderOTWN.Name = "xrLabelHeaderOTWN";
            this.xrLabelHeaderOTWN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTWN.SizeF = new System.Drawing.SizeF(82.92322F, 59.21453F);
            this.xrLabelHeaderOTWN.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTWN.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTWN.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTWN.StylePriority.UseFont = false;
            this.xrLabelHeaderOTWN.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTWN.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTWN.Text = "OT Weekend (night)";
            this.xrLabelHeaderOTWN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTHN
            // 
            this.xrLabelHeaderOTHN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTHN.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTHN.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTHN.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTHN.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTHN.LocationFloat = new DevExpress.Utils.PointFloat(921.5461F, 32.9237F);
            this.xrLabelHeaderOTHN.Multiline = true;
            this.xrLabelHeaderOTHN.Name = "xrLabelHeaderOTHN";
            this.xrLabelHeaderOTHN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTHN.SizeF = new System.Drawing.SizeF(82.0741F, 59.21453F);
            this.xrLabelHeaderOTHN.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTHN.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTHN.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTHN.StylePriority.UseFont = false;
            this.xrLabelHeaderOTHN.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTHN.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTHN.Text = "OT Holidays\r\n(night)";
            this.xrLabelHeaderOTHN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTTotalByMonth
            // 
            this.xrLabelHeaderOTTotalByMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTTotalByMonth.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalByMonth.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTTotalByMonth.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTTotalByMonth.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalByMonth.LocationFloat = new DevExpress.Utils.PointFloat(1003.62F, 0F);
            this.xrLabelHeaderOTTotalByMonth.Name = "xrLabelHeaderOTTotalByMonth";
            this.xrLabelHeaderOTTotalByMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTTotalByMonth.SizeF = new System.Drawing.SizeF(121.717F, 92.13866F);
            this.xrLabelHeaderOTTotalByMonth.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTTotalByMonth.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTTotalByMonth.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTTotalByMonth.StylePriority.UseFont = false;
            this.xrLabelHeaderOTTotalByMonth.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTTotalByMonth.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTTotalByMonth.Text = "Totals (Montly OT)";
            this.xrLabelHeaderOTTotalByMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTTotalsFromFirstMonthToPresent
            // 
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.LocationFloat = new DevExpress.Utils.PointFloat(1125.337F, 0F);
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.Name = "xrLabelHeaderOTTotalsFromFirstMonthToPresent";
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.SizeF = new System.Drawing.SizeF(127.4989F, 92.13866F);
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.StylePriority.UseFont = false;
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.Text = "Totals (From 1st Month To Selected Month)";
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTTotalByYear
            // 
            this.xrLabelHeaderOTTotalByYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTTotalByYear.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalByYear.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTTotalByYear.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTTotalByYear.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalByYear.LocationFloat = new DevExpress.Utils.PointFloat(1252.836F, 0.001335144F);
            this.xrLabelHeaderOTTotalByYear.Name = "xrLabelHeaderOTTotalByYear";
            this.xrLabelHeaderOTTotalByYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTTotalByYear.SizeF = new System.Drawing.SizeF(99.99963F, 92.13853F);
            this.xrLabelHeaderOTTotalByYear.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTTotalByYear.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTTotalByYear.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTTotalByYear.StylePriority.UseFont = false;
            this.xrLabelHeaderOTTotalByYear.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTTotalByYear.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTTotalByYear.Text = "Totals (OT By Year)";
            this.xrLabelHeaderOTTotalByYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTRestTotal
            // 
            this.xrLabelHeaderOTRestTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTRestTotal.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTRestTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTRestTotal.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTRestTotal.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTRestTotal.LocationFloat = new DevExpress.Utils.PointFloat(1352.836F, 0.0009218852F);
            this.xrLabelHeaderOTRestTotal.Name = "xrLabelHeaderOTRestTotal";
            this.xrLabelHeaderOTRestTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTRestTotal.SizeF = new System.Drawing.SizeF(99.42236F, 92.13831F);
            this.xrLabelHeaderOTRestTotal.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseFont = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTRestTotal.Text = "Rest Of OT Totals";
            this.xrLabelHeaderOTRestTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelHeaderEmpCode,
            this.xrLabelHeaderFullName,
            this.xrLabelHeaderOTRestTotal,
            this.xrLabelHeaderPosition,
            this.xrLabelHeaderOTNDD,
            this.xrLabelHeaderOTWD,
            this.xrLabelHeaderOTHDD,
            this.xrLabelHeaderOTNDN,
            this.xrLabelHeaderOTWN,
            this.xrLabelHeaderOTHN,
            this.xrLabelHeaderOTTotalByMonth,
            this.xrLabelHeaderOTTotalsFromFirstMonthToPresent,
            this.xrLabelHeaderOTTotalByYear,
            this.xrLabelHeaderNo,
            this.xrLabelMonth});
            this.PageHeader.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.PageHeader.HeightF = 93.13986F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseFont = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableCompany,
            this.xrPictureBoxLogo,
            this.xrLabelHeaderTitle,
            this.xrLblYear,
            this.xrLblMonth});
            this.ReportHeader.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.ReportHeader.HeightF = 237.576F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.StylePriority.UseFont = false;
            // 
            // xrTableCompany
            // 
            this.xrTableCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCompany.LocationFloat = new DevExpress.Utils.PointFloat(970.0895F, 0F);
            this.xrTableCompany.Name = "xrTableCompany";
            this.xrTableCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCompany.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6});
            this.xrTableCompany.SizeF = new System.Drawing.SizeF(472.9104F, 125F);
            this.xrTableCompany.StylePriority.UseBorders = false;
            this.xrTableCompany.StylePriority.UsePadding = false;
            this.xrTableCompany.StylePriority.UseTextAlignment = false;
            this.xrTableCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelCompanyName,
            this.xrTableCellCompanyName});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
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
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelAddress,
            this.xrTableCellAddress});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
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
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelPhone,
            this.xrTableCellPhone});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
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
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelFax,
            this.xrTableCellFax});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
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
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelEmail,
            this.xrTableCellEmail});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
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
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.ImageUrl = "/Content/Admin/images/logo_report.png";
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 0F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(600.6667F, 125F);
            this.xrPictureBoxLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageFooterLeftText,
            this.xrPageFooterRightText});
            this.PageFooter.HeightF = 23F;
            this.PageFooter.Name = "PageFooter";
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
            this.xrPageFooterRightText.LocationFloat = new DevExpress.Utils.PointFloat(1139.258F, 0F);
            this.xrPageFooterRightText.Name = "xrPageFooterRightText";
            this.xrPageFooterRightText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageFooterRightText.SizeF = new System.Drawing.SizeF(313F, 23F);
            this.xrPageFooterRightText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterRightText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // monthlyOTDataSet1
            // 
            this.monthlyOTDataSet1.DataSetName = "MonthlyOTDataSet";
            this.monthlyOTDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataAdapter = this.timesheet_sprptOTByMonth_DetailsTableAdapter;
            this.DetailReport.DataMember = "Timesheet_sprptOTByMonth_Master.Timesheet_sprptOTByMonth_Master_Timesheet_sprptOT" +
    "ByMonth_Details";
            this.DetailReport.DataSource = this.monthlyOTDataSet1;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableReport});
            this.Detail1.HeightF = 26.04167F;
            this.Detail1.Name = "Detail1";
            // 
            // timesheet_sprptOTByMonth_DetailsTableAdapter
            // 
            this.timesheet_sprptOTByMonth_DetailsTableAdapter.ClearBeforeFill = true;
            // 
            // OTByMonthXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageFooter,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.ReportHeader,
            this.DetailReport});
            this.DataSource = this.monthlyOTDataSet1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 101, 26, 2);
            this.PageHeight = 1169;
            this.PageWidth = 1654;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTableReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyOTDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
