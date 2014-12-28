using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for TrainingPlanReport
/// </summary>
public class TrainingPlanExpenseXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLblTrainingRequestEvaluation;
    private XRLabel xrLblTrainingCourse;
    private XRLabel xrLblTrainingCode;
    private Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlanExpense.TrainingPlanExpenseDS trainingPlanExpenseDS1;
    private Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlanExpense.TrainingPlanExpenseDSTableAdapters.TR_sprptTrainingPlanExpenseTableAdapter tR_sprptTrainingPlanExpenseTableAdapter;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCellSEQ;
    private XRTableCell xrTableCellTrainingCode;
    private XRTableCell xrTableCellTrainingCourse;
    private XRTableCell xrTableCellNumStaff;
    private XRTableCell xrTableCellExpenseOfPlan;
    private XRTableCell xrTableCellExpenseOfRequest;
    private XRTableCell xrTableCellDurationInfo;
    private PageHeaderBand PageHeader;
    private XRPictureBox xrPictureBox1;
    private XRRichText xrRichText1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public TrainingPlanExpenseXtraReport()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingPlanExpenseXtraReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLblTrainingRequestEvaluation = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrLblTrainingCourse = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblTrainingCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSEQ = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingCourse = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNumStaff = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDurationInfo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellExpenseOfPlan = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellExpenseOfRequest = new DevExpress.XtraReports.UI.XRTableCell();
            this.trainingPlanExpenseDS1 = new Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlanExpense.TrainingPlanExpenseDS();
            this.tR_sprptTrainingPlanExpenseTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlanExpense.TrainingPlanExpenseDSTableAdapters.TR_sprptTrainingPlanExpenseTableAdapter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingPlanExpenseDS1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTable1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(999.9999F, 25F);
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorderDashStyle = false;
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanExpense.Seq")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.1531249788284289D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanExpense.TrainingCode")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 0.50937454280850469D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanExpense.TrainingCourse")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.45787530260469389D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanExpense.NumOfStaff")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.22385441429775477D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanExpense.DurationInfo")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 0.885519982026366D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanExpense.ExpenseOfTrainingPlanInfo")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.35937544918063047D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanExpense.ExpenseOfTrainingRequestInfo")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 0.41087533025362122D;
            // 
            // xrLblTrainingRequestEvaluation
            // 
            this.xrLblTrainingRequestEvaluation.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblTrainingRequestEvaluation.LocationFloat = new DevExpress.Utils.PointFloat(250.1599F, 96.875F);
            this.xrLblTrainingRequestEvaluation.Name = "xrLblTrainingRequestEvaluation";
            this.xrLblTrainingRequestEvaluation.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblTrainingRequestEvaluation.SizeF = new System.Drawing.SizeF(468.7501F, 34.45834F);
            this.xrLblTrainingRequestEvaluation.StylePriority.UseFont = false;
            this.xrLblTrainingRequestEvaluation.StylePriority.UseTextAlignment = false;
            this.xrLblTrainingRequestEvaluation.Text = "Training Request Expense";
            this.xrLblTrainingRequestEvaluation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrLblTrainingCourse,
            this.xrLblTrainingCode,
            this.xrLblTrainingRequestEvaluation});
            this.ReportHeader.HeightF = 175.4166F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "/Content/Admin/images/logo_report.png";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(309F, 77.08334F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrRichText1
            // 
            this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(653.4584F, 10.00001F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(346.5416F, 79.99999F);
            this.xrRichText1.StylePriority.UseFont = false;
            // 
            // xrLblTrainingCourse
            // 
            this.xrLblTrainingCourse.LocationFloat = new DevExpress.Utils.PointFloat(598.2014F, 142.4166F);
            this.xrLblTrainingCourse.Name = "xrLblTrainingCourse";
            this.xrLblTrainingCourse.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblTrainingCourse.SizeF = new System.Drawing.SizeF(264.8402F, 23F);
            this.xrLblTrainingCourse.StylePriority.UseTextAlignment = false;
            this.xrLblTrainingCourse.Text = "Training Course :";
            this.xrLblTrainingCourse.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblTrainingCode
            // 
            this.xrLblTrainingCode.LocationFloat = new DevExpress.Utils.PointFloat(279.3266F, 142.4166F);
            this.xrLblTrainingCode.Name = "xrLblTrainingCode";
            this.xrLblTrainingCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblTrainingCode.SizeF = new System.Drawing.SizeF(306.6318F, 23F);
            this.xrLblTrainingCode.StylePriority.UseTextAlignment = false;
            this.xrLblTrainingCode.Text = "Training Code : ";
            this.xrLblTrainingCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTable2.BorderColor = System.Drawing.Color.White;
            this.xrTable2.ForeColor = System.Drawing.Color.White;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(999.9999F, 25F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseForeColor = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSEQ,
            this.xrTableCellTrainingCode,
            this.xrTableCellTrainingCourse,
            this.xrTableCellNumStaff,
            this.xrTableCellDurationInfo,
            this.xrTableCellExpenseOfPlan,
            this.xrTableCellExpenseOfRequest});
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
            this.xrTableCellSEQ.Weight = 0.51041659170122688D;
            // 
            // xrTableCellTrainingCode
            // 
            this.xrTableCellTrainingCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTrainingCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTrainingCode.Name = "xrTableCellTrainingCode";
            this.xrTableCellTrainingCode.StylePriority.UseBorders = false;
            this.xrTableCellTrainingCode.StylePriority.UseFont = false;
            this.xrTableCellTrainingCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingCode.Text = "Training Code";
            this.xrTableCellTrainingCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingCode.Weight = 1.6979150009156188D;
            // 
            // xrTableCellTrainingCourse
            // 
            this.xrTableCellTrainingCourse.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTrainingCourse.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTrainingCourse.Name = "xrTableCellTrainingCourse";
            this.xrTableCellTrainingCourse.StylePriority.UseBorders = false;
            this.xrTableCellTrainingCourse.StylePriority.UseFont = false;
            this.xrTableCellTrainingCourse.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingCourse.Text = "TrainingCourse";
            this.xrTableCellTrainingCourse.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingCourse.Weight = 1.5262507068462849D;
            // 
            // xrTableCellNumStaff
            // 
            this.xrTableCellNumStaff.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNumStaff.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellNumStaff.Name = "xrTableCellNumStaff";
            this.xrTableCellNumStaff.StylePriority.UseBorders = false;
            this.xrTableCellNumStaff.StylePriority.UseFont = false;
            this.xrTableCellNumStaff.StylePriority.UseTextAlignment = false;
            this.xrTableCellNumStaff.Text = "Num. Staff";
            this.xrTableCellNumStaff.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNumStaff.Weight = 0.7461816841048976D;
            // 
            // xrTableCellDurationInfo
            // 
            this.xrTableCellDurationInfo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDurationInfo.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellDurationInfo.Name = "xrTableCellDurationInfo";
            this.xrTableCellDurationInfo.StylePriority.UseBorders = false;
            this.xrTableCellDurationInfo.StylePriority.UseFont = false;
            this.xrTableCellDurationInfo.StylePriority.UseTextAlignment = false;
            this.xrTableCellDurationInfo.Text = "Duration Info";
            this.xrTableCellDurationInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDurationInfo.Weight = 2.9517323722461857D;
            // 
            // xrTableCellExpenseOfPlan
            // 
            this.xrTableCellExpenseOfPlan.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellExpenseOfPlan.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellExpenseOfPlan.Name = "xrTableCellExpenseOfPlan";
            this.xrTableCellExpenseOfPlan.StylePriority.UseBorders = false;
            this.xrTableCellExpenseOfPlan.StylePriority.UseFont = false;
            this.xrTableCellExpenseOfPlan.StylePriority.UseTextAlignment = false;
            this.xrTableCellExpenseOfPlan.Text = "Expense of Plan";
            this.xrTableCellExpenseOfPlan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellExpenseOfPlan.Weight = 1.197919601895757D;
            // 
            // xrTableCellExpenseOfRequest
            // 
            this.xrTableCellExpenseOfRequest.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellExpenseOfRequest.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellExpenseOfRequest.Name = "xrTableCellExpenseOfRequest";
            this.xrTableCellExpenseOfRequest.StylePriority.UseBorders = false;
            this.xrTableCellExpenseOfRequest.StylePriority.UseFont = false;
            this.xrTableCellExpenseOfRequest.StylePriority.UseTextAlignment = false;
            this.xrTableCellExpenseOfRequest.Text = "Expense of Request";
            this.xrTableCellExpenseOfRequest.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellExpenseOfRequest.Weight = 1.3695822750035211D;
            // 
            // trainingPlanExpenseDS1
            // 
            this.trainingPlanExpenseDS1.DataSetName = "TrainingPlanExpenseDS";
            this.trainingPlanExpenseDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tR_sprptTrainingPlanExpenseTableAdapter
            // 
            this.tR_sprptTrainingPlanExpenseTableAdapter.ClearBeforeFill = true;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // TrainingPlanExpenseXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader});
            this.DataAdapter = this.tR_sprptTrainingPlanExpenseTableAdapter;
            this.DataMember = "TR_sprptTrainingPlanExpense";
            this.DataSource = this.trainingPlanExpenseDS1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(52, 48, 76, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingPlanExpenseDS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
