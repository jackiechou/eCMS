using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for TrainingPlanReport
/// </summary>
public class OTNightRecordXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLblOTNightRecordReport;
    private XRLabel xrLblYear;
    private XRLabel xrLblMonth;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    public XRTableCell xrTableCellSeq;
    public XRTableCell xrTableCellFullName;
    public XRTableCell xrTableCellOTDate;
    private XRLabel xrLblCompany;
    public XRTableCell xrTableCellTimeInAM;
    public XRTableCell xrTableCellTimeOutAM;
    public XRTableCell xrTableCellTimeInPM;
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
    public XRTableCell xrTableCellNightOT;
    private XRTableCell xrTableCell1;
    public XRTableCell xrTableCellTimeOutPM;
    private Eagle.WebApp.Areas.Admin.Reports.TS.OTNightRecord.OTNightRecordTableAdapters.Timesheet_sprptOTRecord_DetailTableAdapter timesheet_sprptOTRecord_DetailTableAdapter;
    private Eagle.WebApp.Areas.Admin.Reports.TS.OTNightRecord.OTNightRecord otNightRecord1;
    private Eagle.WebApp.Areas.Admin.Reports.TS.OTNightRecord.OTNightRecordTableAdapters.Timesheet_sprptOTRecord_MasterTableAdapter timesheet_sprptOTRecord_MasterTableAdapter;
    private XRLabel xrLblOTNight;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OTNightRecordXtraReport()
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
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLblOTNightRecordReport = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLblOTNight = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSeq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTimeInAM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTimeOutAM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTimeInPM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTimeOutPM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNightOT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLblYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.timesheet_sprptOTRecord_DetailTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.OTNightRecord.OTNightRecordTableAdapters.Timesheet_sprptOTRecord_DetailTableAdapter();
            this.otNightRecord1 = new Eagle.WebApp.Areas.Admin.Reports.TS.OTNightRecord.OTNightRecord();
            this.timesheet_sprptOTRecord_MasterTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.OTNightRecord.OTNightRecordTableAdapters.Timesheet_sprptOTRecord_MasterTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otNightRecord1)).BeginInit();
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
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.CompanyName")});
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
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(35.71506F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(964.2849F, 25F);
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
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
                    "cord_Detail.Seq")});
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
            this.xrTableCell24.Weight = 0.34999995803801043D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
                    "cord_Detail.FullName")});
            this.xrTableCell66.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseBorders = false;
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.Text = "xrTableCell66";
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell66.Weight = 2.6028084586042906D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
                    "cord_Detail.OTDateInfo")});
            this.xrTableCell70.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.StylePriority.UseBorders = false;
            this.xrTableCell70.StylePriority.UseFont = false;
            this.xrTableCell70.StylePriority.UseTextAlignment = false;
            this.xrTableCell70.Text = "xrTableCell70";
            this.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell70.Weight = 1.1475859139265623D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
                    "cord_Detail.TimeInAMInfo")});
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.StylePriority.UseTextAlignment = false;
            this.xrTableCell73.Text = "xrTableCell73";
            this.xrTableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell73.Weight = 1.0995076074174788D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
                    "cord_Detail.TimeOutAMInfo")});
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.Text = "xrTableCell74";
            this.xrTableCell74.Weight = 1.1261112575036116D;
            // 
            // xrTableCell99
            // 
            this.xrTableCell99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
                    "cord_Detail.TimeInPMInfo")});
            this.xrTableCell99.Name = "xrTableCell99";
            this.xrTableCell99.Text = "xrTableCell99";
            this.xrTableCell99.Weight = 1.1128087200817904D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
                    "cord_Detail.TimeOutPMInfo")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 1.1128106197710632D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
                    "cord_Detail.NightOTInfo")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 1.0912153923411738D;
            // 
            // xrLblOTNightRecordReport
            // 
            this.xrLblOTNightRecordReport.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblOTNightRecordReport.LocationFloat = new DevExpress.Utils.PointFloat(235.5766F, 0F);
            this.xrLblOTNightRecordReport.Name = "xrLblOTNightRecordReport";
            this.xrLblOTNightRecordReport.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblOTNightRecordReport.SizeF = new System.Drawing.SizeF(468.7501F, 34.45834F);
            this.xrLblOTNightRecordReport.StylePriority.UseFont = false;
            this.xrLblOTNightRecordReport.StylePriority.UseTextAlignment = false;
            this.xrLblOTNightRecordReport.Text = "OT Night Record Report";
            this.xrLblOTNightRecordReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrLblOTNight,
            this.xrLblCompany,
            this.xrTable2,
            this.xrLblYear,
            this.xrLblMonth,
            this.xrLblOTNightRecordReport});
            this.ReportHeader.HeightF = 134.5832F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLblOTNight
            // 
            this.xrLblOTNight.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblOTNight.LocationFloat = new DevExpress.Utils.PointFloat(737.4933F, 80.29169F);
            this.xrLblOTNight.Name = "xrLblOTNight";
            this.xrLblOTNight.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblOTNight.SizeF = new System.Drawing.SizeF(253.5067F, 23F);
            this.xrLblOTNight.StylePriority.UseFont = false;
            this.xrLblOTNight.StylePriority.UseTextAlignment = false;
            this.xrLblOTNight.Text = "(Datetime of OT Night :)";
            this.xrLblOTNight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLblCompany
            // 
            this.xrLblCompany.LocationFloat = new DevExpress.Utils.PointFloat(368.5281F, 80.29169F);
            this.xrLblCompany.Name = "xrLblCompany";
            this.xrLblCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblCompany.SizeF = new System.Drawing.SizeF(253.5067F, 23F);
            this.xrLblCompany.StylePriority.UseTextAlignment = false;
            this.xrLblCompany.Text = "Company :";
            this.xrLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 109.5832F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1001F, 25F);
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
            this.xrTableCellSeq,
            this.xrTableCellFullName,
            this.xrTableCellOTDate,
            this.xrTableCellTimeInAM,
            this.xrTableCellTimeOutAM,
            this.xrTableCellTimeInPM,
            this.xrTableCellTimeOutPM,
            this.xrTableCellNightOT});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCellSeq
            // 
            this.xrTableCellSeq.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSeq.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellSeq.Name = "xrTableCellSeq";
            this.xrTableCellSeq.StylePriority.UseBorders = false;
            this.xrTableCellSeq.StylePriority.UseFont = false;
            this.xrTableCellSeq.StylePriority.UseTextAlignment = false;
            this.xrTableCellSeq.Text = "Seq.";
            this.xrTableCellSeq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSeq.Weight = 0.34999992804509278D;
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFullName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.StylePriority.UseBorders = false;
            this.xrTableCellFullName.StylePriority.UseFont = false;
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.Text = "Employee Name";
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellFullName.Weight = 2.6244167335352819D;
            // 
            // xrTableCellOTDate
            // 
            this.xrTableCellOTDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTDate.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellOTDate.Name = "xrTableCellOTDate";
            this.xrTableCellOTDate.StylePriority.UseBorders = false;
            this.xrTableCellOTDate.StylePriority.UseFont = false;
            this.xrTableCellOTDate.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTDate.Text = "OT Date";
            this.xrTableCellOTDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTDate.Weight = 1.0312506932838799D;
            // 
            // xrTableCellTimeInAM
            // 
            this.xrTableCellTimeInAM.Name = "xrTableCellTimeInAM";
            this.xrTableCellTimeInAM.Text = "TimeIn AM";
            this.xrTableCellTimeInAM.Weight = 0.99999976968976223D;
            // 
            // xrTableCellTimeOutAM
            // 
            this.xrTableCellTimeOutAM.Name = "xrTableCellTimeOutAM";
            this.xrTableCellTimeOutAM.Text = "TimeOut AM";
            this.xrTableCellTimeOutAM.Weight = 0.999999760436806D;
            // 
            // xrTableCellTimeInPM
            // 
            this.xrTableCellTimeInPM.Name = "xrTableCellTimeInPM";
            this.xrTableCellTimeInPM.Text = "TimeIn PM";
            this.xrTableCellTimeInPM.Weight = 0.99999978980825022D;
            // 
            // xrTableCellTimeOutPM
            // 
            this.xrTableCellTimeOutPM.Name = "xrTableCellTimeOutPM";
            this.xrTableCellTimeOutPM.Text = "TimeOut PM";
            this.xrTableCellTimeOutPM.Weight = 0.99999978980825044D;
            // 
            // xrTableCellNightOT
            // 
            this.xrTableCellNightOT.Name = "xrTableCellNightOT";
            this.xrTableCellNightOT.Text = "Night OT";
            this.xrTableCellNightOT.Weight = 0.98958195983044217D;
            // 
            // xrLblYear
            // 
            this.xrLblYear.LocationFloat = new DevExpress.Utils.PointFloat(475.2401F, 51.12502F);
            this.xrLblYear.Name = "xrLblYear";
            this.xrLblYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblYear.SizeF = new System.Drawing.SizeF(80.46521F, 23F);
            this.xrLblYear.StylePriority.UseTextAlignment = false;
            this.xrLblYear.Text = "Year :";
            this.xrLblYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblMonth
            // 
            this.xrLblMonth.LocationFloat = new DevExpress.Utils.PointFloat(368.5281F, 51.12502F);
            this.xrLblMonth.Name = "xrLblMonth";
            this.xrLblMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblMonth.SizeF = new System.Drawing.SizeF(79.54843F, 23F);
            this.xrLblMonth.StylePriority.UseTextAlignment = false;
            this.xrLblMonth.Text = "Month :";
            this.xrLblMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataAdapter = this.timesheet_sprptOTRecord_DetailTableAdapter;
            this.DetailReport.DataMember = "Timesheet_sprptOTRecord_Master.Timesheet_sprptOTRecord_Master_Timesheet_sprptOTRe" +
    "cord_Detail";
            this.DetailReport.DataSource = this.otNightRecord1;
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
            // timesheet_sprptOTRecord_DetailTableAdapter
            // 
            this.timesheet_sprptOTRecord_DetailTableAdapter.ClearBeforeFill = true;
            // 
            // otNightRecord1
            // 
            this.otNightRecord1.DataSetName = "OTNightRecord";
            this.otNightRecord1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // timesheet_sprptOTRecord_MasterTableAdapter
            // 
            this.timesheet_sprptOTRecord_MasterTableAdapter.ClearBeforeFill = true;
            // 
            // OTNightRecordXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.DetailReport});
            this.DataAdapter = this.timesheet_sprptOTRecord_MasterTableAdapter;
            this.DataMember = "Timesheet_sprptOTRecord_Master";
            this.DataSource = this.otNightRecord1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(51, 48, 76, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otNightRecord1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
