using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for TrainingPlanReport
/// </summary>
public class WorkingSchedule31XtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLblWorkingSchedule;
    private XRLabel xrLblYear;
    private XRLabel xrLblMonth;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell13;
    private XRLabel xrLblCompany;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell68;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell78;
    private XRTableCell xrTableCell79;
    private XRTableCell xrTableCell80;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell83;
    private XRTableCell xrTableCell84;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTableCell86;
    private XRTableCell xrTableCell87;
    private XRTableCell xrTableCell88;
    private XRTableCell xrTableCell89;
    private XRTableCell xrTableCell90;
    private XRTableCell xrTableCell91;
    private XRTableCell xrTableCell92;
    private XRTableCell xrTableCell93;
    private XRTableCell xrTableCell94;
    private XRTableCell xrTableCell95;
    private XRTableCell xrTableCell96;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell98;
    private XRTableCell xrTableCell99;
    private XRTableCell xrTableCell2;
    private XRTableRow xrTableRow1;
    private XRTable xrTable1;
    public DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell4;
    private Eagle.WebApp.Areas.Admin.Reports.TS.WorkingSchedule.WorkingScheduleDS workingScheduleDS1;
    private Eagle.WebApp.Areas.Admin.Reports.TS.WorkingSchedule.WorkingScheduleDSTableAdapters.Timesheet_sprptShiftOfWorkingScheduleTableAdapter timesheet_sprptShiftOfWorkingScheduleTableAdapter;
    private PageHeaderBand PageHeader;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public WorkingSchedule31XtraReport()
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
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLblWorkingSchedule = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLblCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLblYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.workingScheduleDS1 = new Eagle.WebApp.Areas.Admin.Reports.TS.WorkingSchedule.WorkingScheduleDS();
            this.timesheet_sprptShiftOfWorkingScheduleTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.WorkingSchedule.WorkingScheduleDSTableAdapters.Timesheet_sprptShiftOfWorkingScheduleTableAdapter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.workingScheduleDS1)).BeginInit();
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
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.CompanyName")});
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
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.7150669F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(999.2849F, 25F);
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
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell71,
            this.xrTableCell72,
            this.xrTableCell73,
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76,
            this.xrTableCell77,
            this.xrTableCell78,
            this.xrTableCell79,
            this.xrTableCell80,
            this.xrTableCell81,
            this.xrTableCell82,
            this.xrTableCell83,
            this.xrTableCell84,
            this.xrTableCell85,
            this.xrTableCell86,
            this.xrTableCell87,
            this.xrTableCell88,
            this.xrTableCell89,
            this.xrTableCell90,
            this.xrTableCell91,
            this.xrTableCell92,
            this.xrTableCell93,
            this.xrTableCell94,
            this.xrTableCell95,
            this.xrTableCell96,
            this.xrTableCell97,
            this.xrTableCell98,
            this.xrTableCell99,
            this.xrTableCell5});
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
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.Seq")});
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
            this.xrTableCell24.Weight = 0.29999990506919239D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.FullName")});
            this.xrTableCell66.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseBorders = false;
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.Text = "xrTableCell66";
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell66.Weight = 1.2644266240990016D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.StartDateInfo")});
            this.xrTableCell69.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.StylePriority.UseBorders = false;
            this.xrTableCell69.StylePriority.UseFont = false;
            this.xrTableCell69.StylePriority.UseTextAlignment = false;
            this.xrTableCell69.Text = "xrTableCell69";
            this.xrTableCell69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell69.Weight = 0.69646244229511434D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C01")});
            this.xrTableCell70.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.StylePriority.UseBorders = false;
            this.xrTableCell70.StylePriority.UseFont = false;
            this.xrTableCell70.StylePriority.UseTextAlignment = false;
            this.xrTableCell70.Text = "xrTableCell70";
            this.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell70.Weight = 0.2499990407053693D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C02")});
            this.xrTableCell71.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.StylePriority.UseBorders = false;
            this.xrTableCell71.StylePriority.UseFont = false;
            this.xrTableCell71.StylePriority.UseTextAlignment = false;
            this.xrTableCell71.Text = "xrTableCell71";
            this.xrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell71.Weight = 0.25000102882929104D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule.C03")});
            this.xrTableCell72.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.StylePriority.UseBorders = false;
            this.xrTableCell72.StylePriority.UseFont = false;
            this.xrTableCell72.StylePriority.UseTextAlignment = false;
            this.xrTableCell72.Text = "xrTableCell72";
            this.xrTableCell72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell72.Weight = 0.24999996071431735D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C04")});
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.StylePriority.UseTextAlignment = false;
            this.xrTableCell73.Text = "xrTableCell73";
            this.xrTableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell73.Weight = 0.25000057334322578D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C05")});
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.Text = "xrTableCell74";
            this.xrTableCell74.Weight = 0.24999996512684003D;
            // 
            // xrTableCell75
            // 
            this.xrTableCell75.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C06")});
            this.xrTableCell75.Name = "xrTableCell75";
            this.xrTableCell75.Text = "xrTableCell75";
            this.xrTableCell75.Weight = 0.24999965995110104D;
            // 
            // xrTableCell76
            // 
            this.xrTableCell76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C07")});
            this.xrTableCell76.Name = "xrTableCell76";
            this.xrTableCell76.Text = "xrTableCell76";
            this.xrTableCell76.Weight = 0.24999996512684D;
            // 
            // xrTableCell77
            // 
            this.xrTableCell77.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C08")});
            this.xrTableCell77.Name = "xrTableCell77";
            this.xrTableCell77.Text = "xrTableCell77";
            this.xrTableCell77.Weight = 0.25000027137012465D;
            // 
            // xrTableCell78
            // 
            this.xrTableCell78.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C09")});
            this.xrTableCell78.Name = "xrTableCell78";
            this.xrTableCell78.Text = "xrTableCell78";
            this.xrTableCell78.Weight = 0.24999966101864724D;
            // 
            // xrTableCell79
            // 
            this.xrTableCell79.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C10")});
            this.xrTableCell79.Name = "xrTableCell79";
            this.xrTableCell79.StylePriority.UseTextAlignment = false;
            this.xrTableCell79.Text = "xrTableCell79";
            this.xrTableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell79.Weight = 0.24999996619438569D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C11")});
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.Text = "xrTableCell80";
            this.xrTableCell80.Weight = 0.24999935902775367D;
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C12")});
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.Text = "xrTableCell81";
            this.xrTableCell81.Weight = 0.24999966314484268D;
            // 
            // xrTableCell82
            // 
            this.xrTableCell82.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C13")});
            this.xrTableCell82.Name = "xrTableCell82";
            this.xrTableCell82.Text = "xrTableCell82";
            this.xrTableCell82.Weight = 0.25000118849421027D;
            // 
            // xrTableCell83
            // 
            this.xrTableCell83.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C14")});
            this.xrTableCell83.Name = "xrTableCell83";
            this.xrTableCell83.Text = "xrTableCell83";
            this.xrTableCell83.Weight = 0.25000058264644293D;
            // 
            // xrTableCell84
            // 
            this.xrTableCell84.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C15")});
            this.xrTableCell84.Name = "xrTableCell84";
            this.xrTableCell84.Text = "xrTableCell84";
            this.xrTableCell84.Weight = 0.24999935240674504D;
            // 
            // xrTableCell85
            // 
            this.xrTableCell85.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell85.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C16")});
            this.xrTableCell85.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell85.Name = "xrTableCell85";
            this.xrTableCell85.StylePriority.UseBorders = false;
            this.xrTableCell85.StylePriority.UseFont = false;
            this.xrTableCell85.StylePriority.UseTextAlignment = false;
            this.xrTableCell85.Text = "xrTableCell85";
            this.xrTableCell85.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell85.Weight = 0.24999996275822231D;
            // 
            // xrTableCell86
            // 
            this.xrTableCell86.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C17")});
            this.xrTableCell86.Name = "xrTableCell86";
            this.xrTableCell86.Text = "xrTableCell86";
            this.xrTableCell86.Weight = 0.24999997229496551D;
            // 
            // xrTableCell87
            // 
            this.xrTableCell87.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C18")});
            this.xrTableCell87.Name = "xrTableCell87";
            this.xrTableCell87.Text = "xrTableCell87";
            this.xrTableCell87.Weight = 0.25000056860599107D;
            // 
            // xrTableCell88
            // 
            this.xrTableCell88.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C19")});
            this.xrTableCell88.Name = "xrTableCell88";
            this.xrTableCell88.Text = "xrTableCell88";
            this.xrTableCell88.Weight = 0.24999911902123251D;
            // 
            // xrTableCell89
            // 
            this.xrTableCell89.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C20")});
            this.xrTableCell89.Name = "xrTableCell89";
            this.xrTableCell89.Text = "xrTableCell89";
            this.xrTableCell89.Weight = 0.24999995825451346D;
            // 
            // xrTableCell90
            // 
            this.xrTableCell90.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C21")});
            this.xrTableCell90.Name = "xrTableCell90";
            this.xrTableCell90.Text = "xrTableCell90";
            this.xrTableCell90.Weight = 0.24999995825451379D;
            // 
            // xrTableCell91
            // 
            this.xrTableCell91.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C22")});
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.Text = "xrTableCell91";
            this.xrTableCell91.Weight = 0.2499999582545136D;
            // 
            // xrTableCell92
            // 
            this.xrTableCell92.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C23")});
            this.xrTableCell92.Name = "xrTableCell92";
            this.xrTableCell92.Text = "xrTableCell92";
            this.xrTableCell92.Weight = 0.24999995825451357D;
            // 
            // xrTableCell93
            // 
            this.xrTableCell93.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C24")});
            this.xrTableCell93.Name = "xrTableCell93";
            this.xrTableCell93.Text = "xrTableCell93";
            this.xrTableCell93.Weight = 0.24999995825451377D;
            // 
            // xrTableCell94
            // 
            this.xrTableCell94.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C25")});
            this.xrTableCell94.Name = "xrTableCell94";
            this.xrTableCell94.Text = "xrTableCell94";
            this.xrTableCell94.Weight = 0.24999996966599705D;
            // 
            // xrTableCell95
            // 
            this.xrTableCell95.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C26")});
            this.xrTableCell95.Name = "xrTableCell95";
            this.xrTableCell95.Text = "xrTableCell95";
            this.xrTableCell95.Weight = 0.23000058402953069D;
            // 
            // xrTableCell96
            // 
            this.xrTableCell96.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C27")});
            this.xrTableCell96.Name = "xrTableCell96";
            this.xrTableCell96.StylePriority.UseTextAlignment = false;
            this.xrTableCell96.Text = "xrTableCell96";
            this.xrTableCell96.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell96.Weight = 0.24999996832058122D;
            // 
            // xrTableCell97
            // 
            this.xrTableCell97.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C28")});
            this.xrTableCell97.Name = "xrTableCell97";
            this.xrTableCell97.Text = "xrTableCell97";
            this.xrTableCell97.Weight = 0.24999996832058141D;
            // 
            // xrTableCell98
            // 
            this.xrTableCell98.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C29")});
            this.xrTableCell98.Name = "xrTableCell98";
            this.xrTableCell98.Text = "xrTableCell98";
            this.xrTableCell98.Weight = 0.24999996937923086D;
            // 
            // xrTableCell99
            // 
            this.xrTableCell99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C30")});
            this.xrTableCell99.Name = "xrTableCell99";
            this.xrTableCell99.Text = "xrTableCell99";
            this.xrTableCell99.Weight = 0.24999997436714525D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
                    "e_Master_Timesheet_sprptShiftOfWorkingSchedule.C31")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 0.25195870678881782D;
            // 
            // xrLblWorkingSchedule
            // 
            this.xrLblWorkingSchedule.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblWorkingSchedule.LocationFloat = new DevExpress.Utils.PointFloat(235.5766F, 0F);
            this.xrLblWorkingSchedule.Name = "xrLblWorkingSchedule";
            this.xrLblWorkingSchedule.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblWorkingSchedule.SizeF = new System.Drawing.SizeF(468.7501F, 34.45834F);
            this.xrLblWorkingSchedule.StylePriority.UseFont = false;
            this.xrLblWorkingSchedule.StylePriority.UseTextAlignment = false;
            this.xrLblWorkingSchedule.Text = "Working Schedule Report";
            this.xrLblWorkingSchedule.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 75.99999F;
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
            this.xrLblCompany,
            this.xrLblYear,
            this.xrLblMonth,
            this.xrLblWorkingSchedule});
            this.ReportHeader.HeightF = 109.5832F;
            this.ReportHeader.Name = "ReportHeader";
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
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(1.715104F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(999.2849F, 25F);
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
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell20,
            this.xrTableCell25,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell10,
            this.xrTableCell41,
            this.xrTableCell40,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell30,
            this.xrTableCell33,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell68,
            this.xrTableCell23,
            this.xrTableCell19,
            this.xrTableCell29,
            this.xrTableCell28,
            this.xrTableCell26,
            this.xrTableCell4});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Seq";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.29999995723337369D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Employee Name";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 1.2644266026194815D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Start date";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 0.69646224782665223D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "01";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 0.2499989261912477D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "02";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.25000120049085806D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "03";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell14.Weight = 0.24999997978779956D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "04";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell21.Weight = 0.24999998206523066D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "05";
            this.xrTableCell22.Weight = 0.24999998420032224D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "06";
            this.xrTableCell15.Weight = 0.24999998420032193D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "07";
            this.xrTableCell16.Weight = 0.24999998420032221D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "08";
            this.xrTableCell17.Weight = 0.24999998526786821D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Text = "09";
            this.xrTableCell18.Weight = 0.24999998526786821D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.Text = "10";
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell20.Weight = 0.24999998526786796D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Text = "11";
            this.xrTableCell25.Weight = 0.24999998845271326D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Text = "12";
            this.xrTableCell36.Weight = 0.24999998739406368D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Text = "13";
            this.xrTableCell37.Weight = 0.24999998686473793D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Text = "14";
            this.xrTableCell38.Weight = 0.24999999136844786D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Text = "15";
            this.xrTableCell39.Weight = 0.2499999818317046D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "16";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.24999998183170463D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Text = "17";
            this.xrTableCell41.Weight = 0.24999999136844775D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.Text = "18";
            this.xrTableCell40.Weight = 0.24999997732799595D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.Text = "19";
            this.xrTableCell34.Weight = 0.24999997732799606D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Text = "20";
            this.xrTableCell35.Weight = 0.24999997732799578D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.Text = "21";
            this.xrTableCell30.Weight = 0.24999997732799609D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Text = "22";
            this.xrTableCell33.Weight = 0.24999997732799584D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.Text = "23";
            this.xrTableCell31.Weight = 0.24999997732799584D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.Text = "24";
            this.xrTableCell32.Weight = 0.24999997732799603D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.Text = "25";
            this.xrTableCell68.Weight = 0.24999999057001227D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Text = "26";
            this.xrTableCell23.Weight = 0.22999999166317908D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.Text = "27";
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell19.Weight = 0.24999998739406343D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Text = "28";
            this.xrTableCell29.Weight = 0.24999998739406354D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Text = "29";
            this.xrTableCell28.Weight = 0.2499999884527131D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Text = "30";
            this.xrTableCell26.Weight = 0.24999993363444509D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "31";
            this.xrTableCell4.Weight = 0.25195866544517542D;
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
            this.DetailReport.DataMember = "Timesheet_sprptShiftOfWorkingSchedule_Master.Timesheet_sprptShiftOfWorkingSchedul" +
    "e_Master_Timesheet_sprptShiftOfWorkingSchedule";
            this.DetailReport.DataSource = this.workingScheduleDS1;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail1.HeightF = 25F;
            this.Detail1.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail1.Name = "Detail1";
            // 
            // workingScheduleDS1
            // 
            this.workingScheduleDS1.DataSetName = "WorkingScheduleDS";
            this.workingScheduleDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // timesheet_sprptShiftOfWorkingScheduleTableAdapter
            // 
            this.timesheet_sprptShiftOfWorkingScheduleTableAdapter.ClearBeforeFill = true;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // WorkingSchedule31XtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.DetailReport,
            this.PageHeader});
            this.DataAdapter = this.timesheet_sprptShiftOfWorkingScheduleTableAdapter;
            this.DataMember = "Timesheet_sprptShiftOfWorkingSchedule";
            this.DataSource = this.workingScheduleDS1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(51, 48, 76, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.workingScheduleDS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
