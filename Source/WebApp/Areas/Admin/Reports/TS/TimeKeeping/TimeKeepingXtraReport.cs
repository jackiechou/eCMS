using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for TrainingPlanReport
/// </summary>
public class TimeKeepingXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLblTimeKeepingReport;
    private XRLabel xrLblToDate;
    private XRLabel xrLblFromDate;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    public XRTableCell xrTableCellSEQ;
    public XRTableCell xrTableCellEmployeeName;
    public XRTableCell xrTableCellDate;
    public XRTableCell xrTableCellShiftName;
    public XRTableCell xrTableCellTimeIn;
    public XRTableCell xrTableCellTimeOut;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell99;
    private XRTableCell xrTableCell2;
    private XRTableRow xrTableRow1;
    private XRTable xrTable1;
    public DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTableCell xrTableCell5;
    public XRTableCell xrTableCellRealTimeOut;
    private XRTableCell xrTableCell1;
    public XRTableCell xrTableCellRealTimeIn;
    private PageHeaderBand PageHeader;
    private XRPictureBox xrPictureBox1;
    private XRRichText xrRichText1;
    private XRTableCell xrTableCellPositionName;
    private XRTableCell xrTableCell3;
    private Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeeping.TimeKeepingDSTableAdapters.timesheet_sprptTimeKeepingReport_detailTableAdapter timesheet_sprptTimeKeepingReport_detailTableAdapter;
    private Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeeping.TimeKeepingDS timeKeepingDS1;
    private Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeeping.TimeKeepingDSTableAdapters.timesheet_sprptTimeKeepingReport_masterTableAdapter timesheet_sprptTimeKeepingReport_masterTableAdapter;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public TimeKeepingXtraReport()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeKeepingXtraReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLblTimeKeepingReport = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLblToDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblFromDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSEQ = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmployeeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPositionName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellShiftName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTimeIn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTimeOut = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellRealTimeIn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellRealTimeOut = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.timesheet_sprptTimeKeepingReport_detailTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeeping.TimeKeepingDSTableAdapters.timesheet_sprptTimeKeepingReport_detailTableAdapter();
            this.timeKeepingDS1 = new Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeeping.TimeKeepingDS();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.timesheet_sprptTimeKeepingReport_masterTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.TimeKeeping.TimeKeepingDSTableAdapters.timesheet_sprptTimeKeepingReport_masterTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeKeepingDS1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 25.47351F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1000F, 25.47351F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.BackColor = System.Drawing.Color.SteelBlue;
            this.xrTableRow1.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow1.ForeColor = System.Drawing.Color.White;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseBackColor = false;
            this.xrTableRow1.StylePriority.UseBorderColor = false;
            this.xrTableRow1.StylePriority.UseBorders = false;
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UseForeColor = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.CompanyName")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBackColor = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 3D;
            // 
            // xrTable3
            // 
            this.xrTable3.BackColor = System.Drawing.Color.Transparent;
            this.xrTable3.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable3.ForeColor = System.Drawing.Color.Black;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1001F, 25F);
            this.xrTable3.StylePriority.UseBackColor = false;
            this.xrTable3.StylePriority.UseBorderColor = false;
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseForeColor = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24,
            this.xrTableCell66,
            this.xrTableCell70,
            this.xrTableCell73,
            this.xrTableCell74,
            this.xrTableCell99,
            this.xrTableCell5,
            this.xrTableCell3,
            this.xrTableCell1});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.seq")});
            this.xrTableCell24.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell24.ForeColor = System.Drawing.Color.Black;
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseBorderColor = false;
            this.xrTableCell24.StylePriority.UseBorders = false;
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.StylePriority.UseForeColor = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.Text = "xrTableCell24";
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell24.Weight = 0.35000007928420718D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.EmployeeName")});
            this.xrTableCell66.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseBorders = false;
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.Text = "xrTableCell66";
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell66.Weight = 2.4897614156192276D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.PositionName")});
            this.xrTableCell70.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.StylePriority.UseBorders = false;
            this.xrTableCell70.StylePriority.UseFont = false;
            this.xrTableCell70.StylePriority.UseTextAlignment = false;
            this.xrTableCell70.Text = "xrTableCell70";
            this.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell70.Weight = 1.4602384631229541D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.DateInfo")});
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.StylePriority.UseTextAlignment = false;
            this.xrTableCell73.Text = "xrTableCell73";
            this.xrTableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell73.Weight = 0.9499998744558813D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.ShiftName")});
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.StylePriority.UseTextAlignment = false;
            this.xrTableCell74.Text = "xrTableCell74";
            this.xrTableCell74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell74.Weight = 0.94999914245862582D;
            // 
            // xrTableCell99
            // 
            this.xrTableCell99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.TimeInInfo")});
            this.xrTableCell99.Name = "xrTableCell99";
            this.xrTableCell99.Text = "xrTableCell99";
            this.xrTableCell99.Weight = 0.94999929198908184D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.TimeOutInfo")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 0.95000081020867522D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.TimeInLateInfo")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 0.94999988855419248D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
                    "imesheet_sprptTimeKeepingReport_detail.TimeOutEarlyInfo")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 0.95999897178935067D;
            // 
            // xrLblTimeKeepingReport
            // 
            this.xrLblTimeKeepingReport.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblTimeKeepingReport.LocationFloat = new DevExpress.Utils.PointFloat(208.7639F, 107.7498F);
            this.xrLblTimeKeepingReport.Name = "xrLblTimeKeepingReport";
            this.xrLblTimeKeepingReport.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblTimeKeepingReport.SizeF = new System.Drawing.SizeF(601.2362F, 34.45835F);
            this.xrLblTimeKeepingReport.StylePriority.UseFont = false;
            this.xrLblTimeKeepingReport.StylePriority.UseTextAlignment = false;
            this.xrLblTimeKeepingReport.Text = "Statistic of late early employee report";
            this.xrLblTimeKeepingReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 76F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1,
            this.xrPictureBox1,
            this.xrLblToDate,
            this.xrLblFromDate,
            this.xrLblTimeKeepingReport});
            this.ReportHeader.HeightF = 189.7915F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrRichText1
            // 
            this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(644.4584F, 10.00001F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(346.5416F, 79.99999F);
            this.xrRichText1.StylePriority.UseFont = false;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "/Content/Admin/images/logo_report.png";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(309F, 77.08334F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLblToDate
            // 
            this.xrLblToDate.LocationFloat = new DevExpress.Utils.PointFloat(506.4901F, 156.7915F);
            this.xrLblToDate.Name = "xrLblToDate";
            this.xrLblToDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblToDate.SizeF = new System.Drawing.SizeF(131.6181F, 23F);
            this.xrLblToDate.StylePriority.UseTextAlignment = false;
            this.xrLblToDate.Text = "To date : 00/00/0000";
            this.xrLblToDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblFromDate
            // 
            this.xrLblFromDate.LocationFloat = new DevExpress.Utils.PointFloat(330.9959F, 156.7915F);
            this.xrLblFromDate.Name = "xrLblFromDate";
            this.xrLblFromDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblFromDate.SizeF = new System.Drawing.SizeF(139.9651F, 23F);
            this.xrLblFromDate.StylePriority.UseTextAlignment = false;
            this.xrLblFromDate.Text = "From date : 00/00/0000";
            this.xrLblFromDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTable2.BorderColor = System.Drawing.Color.White;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable2.ForeColor = System.Drawing.Color.White;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1000F, 25F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseForeColor = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSEQ,
            this.xrTableCellEmployeeName,
            this.xrTableCellPositionName,
            this.xrTableCellDate,
            this.xrTableCellShiftName,
            this.xrTableCellTimeIn,
            this.xrTableCellTimeOut,
            this.xrTableCellRealTimeIn,
            this.xrTableCellRealTimeOut});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCellSEQ
            // 
            this.xrTableCellSEQ.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSEQ.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellSEQ.Name = "xrTableCellSEQ";
            this.xrTableCellSEQ.StylePriority.UseBorders = false;
            this.xrTableCellSEQ.StylePriority.UseFont = false;
            this.xrTableCellSEQ.StylePriority.UseTextAlignment = false;
            this.xrTableCellSEQ.Text = "Seq.";
            this.xrTableCellSEQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSEQ.Weight = 0.31451916595118912D;
            // 
            // xrTableCellEmployeeName
            // 
            this.xrTableCellEmployeeName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellEmployeeName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellEmployeeName.Name = "xrTableCellEmployeeName";
            this.xrTableCellEmployeeName.StylePriority.UseBorders = false;
            this.xrTableCellEmployeeName.StylePriority.UseFont = false;
            this.xrTableCellEmployeeName.StylePriority.UseTextAlignment = false;
            this.xrTableCellEmployeeName.Text = "Employee Name";
            this.xrTableCellEmployeeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellEmployeeName.Weight = 2.23736528246922D;
            // 
            // xrTableCellPositionName
            // 
            this.xrTableCellPositionName.Name = "xrTableCellPositionName";
            this.xrTableCellPositionName.Text = "Position";
            this.xrTableCellPositionName.Weight = 1.312208366767641D;
            // 
            // xrTableCellDate
            // 
            this.xrTableCellDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDate.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellDate.Name = "xrTableCellDate";
            this.xrTableCellDate.StylePriority.UseBorders = false;
            this.xrTableCellDate.StylePriority.UseFont = false;
            this.xrTableCellDate.StylePriority.UseTextAlignment = false;
            this.xrTableCellDate.Text = "Date";
            this.xrTableCellDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDate.Weight = 0.85369488587211706D;
            // 
            // xrTableCellShiftName
            // 
            this.xrTableCellShiftName.Name = "xrTableCellShiftName";
            this.xrTableCellShiftName.Text = "Shift";
            this.xrTableCellShiftName.Weight = 0.85369493124979035D;
            // 
            // xrTableCellTimeIn
            // 
            this.xrTableCellTimeIn.Name = "xrTableCellTimeIn";
            this.xrTableCellTimeIn.Text = "TimeIn";
            this.xrTableCellTimeIn.Weight = 0.85369485343709472D;
            // 
            // xrTableCellTimeOut
            // 
            this.xrTableCellTimeOut.Name = "xrTableCellTimeOut";
            this.xrTableCellTimeOut.Text = "TimeOut";
            this.xrTableCellTimeOut.Weight = 0.85369488280853889D;
            // 
            // xrTableCellRealTimeIn
            // 
            this.xrTableCellRealTimeIn.Name = "xrTableCellRealTimeIn";
            this.xrTableCellRealTimeIn.Text = "Late Time";
            this.xrTableCellRealTimeIn.Weight = 0.85369488280853911D;
            // 
            // xrTableCellRealTimeOut
            // 
            this.xrTableCellRealTimeOut.Name = "xrTableCellRealTimeOut";
            this.xrTableCellRealTimeOut.Text = "Early Time";
            this.xrTableCellRealTimeOut.Weight = 0.85369491091136018D;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataAdapter = this.timesheet_sprptTimeKeepingReport_detailTableAdapter;
            this.DetailReport.DataMember = "timesheet_sprptTimeKeepingReport_master.timesheet_sprptTimeKeepingReport_master_t" +
    "imesheet_sprptTimeKeepingReport_detail";
            this.DetailReport.DataSource = this.timeKeepingDS1;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail1.HeightF = 25.07519F;
            this.Detail1.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail1.Name = "Detail1";
            // 
            // timesheet_sprptTimeKeepingReport_detailTableAdapter
            // 
            this.timesheet_sprptTimeKeepingReport_detailTableAdapter.ClearBeforeFill = true;
            // 
            // timeKeepingDS1
            // 
            this.timeKeepingDS1.DataSetName = "TimeKeepingDS";
            this.timeKeepingDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // timesheet_sprptTimeKeepingReport_masterTableAdapter
            // 
            this.timesheet_sprptTimeKeepingReport_masterTableAdapter.ClearBeforeFill = true;
            // 
            // TimeKeepingXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.DetailReport,
            this.PageHeader});
            this.DataAdapter = this.timesheet_sprptTimeKeepingReport_masterTableAdapter;
            this.DataMember = "timesheet_sprptTimeKeepingReport_master";
            this.DataSource = this.timeKeepingDS1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(51, 48, 76, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeKeepingDS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
