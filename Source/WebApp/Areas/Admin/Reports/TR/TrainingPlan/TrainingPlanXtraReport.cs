using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for TrainingPlanReport
/// </summary>
public class TrainingPlanXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLbllTrainingPlan;
    private XRLabel xrLblYear;
    private XRLabel xrLbllMonth;
    private Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlan.TrainingPlanDS trainingPlanDS1;
    private Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlan.TrainingPlanDSTableAdapters.TR_sprptTrainingPlanTableAdapter tR_sprptTrainingPlanTableAdapter;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCellSEQ;
    private XRTableCell xrTableCellTrainingCode;
    private XRTableCell xrTableCellTrainingCourse;
    private XRTableCell xrTableCellTrainingCategory;
    private XRTableCell xrTableCellTrainingLocation;
    private XRTableCell xrTableCellDays;
    private XRTableCell xrTableCellHours;
    private XRTableCell xrTableCellNumStaff;
    private XRTableCell xrTableCellCost;
    private XRRichText xrRichText1;
    private PageHeaderBand PageHeader;
    private XRPictureBox xrPictureBox1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public TrainingPlanXtraReport()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingPlanXtraReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLbllTrainingPlan = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrLblYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLbllMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSEQ = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingCourse = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingCategory = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingLocation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDays = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHours = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNumStaff = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCost = new DevExpress.XtraReports.UI.XRTableCell();
            this.trainingPlanDS1 = new Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlan.TrainingPlanDS();
            this.tR_sprptTrainingPlanTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlan.TrainingPlanDSTableAdapters.TR_sprptTrainingPlanTableAdapter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingPlanDS1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 25.08335F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(990F, 25F);
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell6,
            this.xrTableCell5,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.Seq")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 0.5104165649414063D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.TrainingCode")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 1.6979164123535155D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.TrainingCourse")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 1.526250457763672D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.TrainingCategory")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 1.5625015258789061D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.TrainingLocation")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell5.Weight = 1.5416638183593752D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.PlanDays")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell7.Weight = 0.59374938964843749D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.PlanHours")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell8.Weight = 0.85416809082031264D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.NumOfStaff")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell9.Weight = 0.84833312988281251D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlan.CostInfo")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 0.76500061035156253D;
            // 
            // xrLbllTrainingPlan
            // 
            this.xrLbllTrainingPlan.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLbllTrainingPlan.LocationFloat = new DevExpress.Utils.PointFloat(373.4583F, 96.06978F);
            this.xrLbllTrainingPlan.Name = "xrLbllTrainingPlan";
            this.xrLbllTrainingPlan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbllTrainingPlan.SizeF = new System.Drawing.SizeF(251.0417F, 34.45834F);
            this.xrLbllTrainingPlan.StylePriority.UseFont = false;
            this.xrLbllTrainingPlan.StylePriority.UseTextAlignment = false;
            this.xrLbllTrainingPlan.Text = "Training Plan";
            this.xrLbllTrainingPlan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrPictureBox1,
            this.xrRichText1,
            this.xrLblYear,
            this.xrLbllMonth,
            this.xrLbllTrainingPlan});
            this.ReportHeader.HeightF = 174.9865F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "/Content/Admin/images/logo_report.png";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(2.384186E-05F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(309F, 77.08334F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrRichText1
            // 
            this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(643.4584F, 10F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(346.5416F, 79.99999F);
            this.xrRichText1.StylePriority.UseFont = false;
            // 
            // xrLblYear
            // 
            this.xrLblYear.LocationFloat = new DevExpress.Utils.PointFloat(529.7084F, 141.9865F);
            this.xrLblYear.Name = "xrLblYear";
            this.xrLblYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblYear.SizeF = new System.Drawing.SizeF(85.79865F, 23F);
            this.xrLblYear.StylePriority.UseTextAlignment = false;
            this.xrLblYear.Text = "Year :";
            this.xrLblYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLbllMonth
            // 
            this.xrLbllMonth.LocationFloat = new DevExpress.Utils.PointFloat(427.2432F, 141.9865F);
            this.xrLbllMonth.Name = "xrLbllMonth";
            this.xrLbllMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbllMonth.SizeF = new System.Drawing.SizeF(80.59021F, 23F);
            this.xrLbllMonth.StylePriority.UseTextAlignment = false;
            this.xrLbllMonth.Text = "Month : ";
            this.xrLbllMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTable2.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTable2.ForeColor = System.Drawing.Color.White;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(2.384186E-05F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(990F, 25F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseForeColor = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSEQ,
            this.xrTableCellTrainingCode,
            this.xrTableCellTrainingCourse,
            this.xrTableCellTrainingCategory,
            this.xrTableCellTrainingLocation,
            this.xrTableCellDays,
            this.xrTableCellHours,
            this.xrTableCellNumStaff,
            this.xrTableCellCost});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCellSEQ
            // 
            this.xrTableCellSEQ.Name = "xrTableCellSEQ";
            this.xrTableCellSEQ.StylePriority.UseTextAlignment = false;
            this.xrTableCellSEQ.Text = "Seq.";
            this.xrTableCellSEQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSEQ.Weight = 0.1531249788284289D;
            // 
            // xrTableCellTrainingCode
            // 
            this.xrTableCellTrainingCode.Name = "xrTableCellTrainingCode";
            this.xrTableCellTrainingCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingCode.Text = "Training Code";
            this.xrTableCellTrainingCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingCode.Weight = 0.50937486324309467D;
            // 
            // xrTableCellTrainingCourse
            // 
            this.xrTableCellTrainingCourse.Name = "xrTableCellTrainingCourse";
            this.xrTableCellTrainingCourse.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingCourse.Text = "Training Course";
            this.xrTableCellTrainingCourse.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingCourse.Weight = 0.45787498217010392D;
            // 
            // xrTableCellTrainingCategory
            // 
            this.xrTableCellTrainingCategory.Name = "xrTableCellTrainingCategory";
            this.xrTableCellTrainingCategory.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingCategory.Text = "Training Category";
            this.xrTableCellTrainingCategory.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingCategory.Weight = 0.46875066947941096D;
            // 
            // xrTableCellTrainingLocation
            // 
            this.xrTableCellTrainingLocation.Name = "xrTableCellTrainingLocation";
            this.xrTableCellTrainingLocation.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingLocation.Text = "Training Location";
            this.xrTableCellTrainingLocation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingLocation.Weight = 0.46249902305594714D;
            // 
            // xrTableCellDays
            // 
            this.xrTableCellDays.Name = "xrTableCellDays";
            this.xrTableCellDays.StylePriority.UseTextAlignment = false;
            this.xrTableCellDays.Text = "Days";
            this.xrTableCellDays.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDays.Weight = 0.17812479534150275D;
            // 
            // xrTableCellHours
            // 
            this.xrTableCellHours.Name = "xrTableCellHours";
            this.xrTableCellHours.StylePriority.UseTextAlignment = false;
            this.xrTableCellHours.Text = "Hours";
            this.xrTableCellHours.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHours.Weight = 0.25625066547398062D;
            // 
            // xrTableCellNumStaff
            // 
            this.xrTableCellNumStaff.Name = "xrTableCellNumStaff";
            this.xrTableCellNumStaff.StylePriority.UseTextAlignment = false;
            this.xrTableCellNumStaff.Text = "Num. Staff";
            this.xrTableCellNumStaff.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNumStaff.Weight = 0.25450015204240584D;
            // 
            // xrTableCellCost
            // 
            this.xrTableCellCost.Name = "xrTableCellCost";
            this.xrTableCellCost.StylePriority.UseTextAlignment = false;
            this.xrTableCellCost.Text = "Cost";
            this.xrTableCellCost.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCost.Weight = 0.22950023291399771D;
            // 
            // trainingPlanDS1
            // 
            this.trainingPlanDS1.DataSetName = "TrainingPlanDS";
            this.trainingPlanDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tR_sprptTrainingPlanTableAdapter
            // 
            this.tR_sprptTrainingPlanTableAdapter.ClearBeforeFill = true;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // TrainingPlanXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader});
            this.DataAdapter = this.tR_sprptTrainingPlanTableAdapter;
            this.DataMember = "TR_sprptTrainingPlan";
            this.DataSource = this.trainingPlanDS1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(52, 58, 76, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingPlanDS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
