using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for TrainingPlanReport
/// </summary>
public class TrainingPlanEvaluationXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLblTrainingRequestEvaluation;
    private XRLabel xrLblTrainingCourse;
    private XRLabel xrLblTrainingCode;
    private XRLabel xrLblFullName;
    private Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlanEvaluation.TrainingPlanEvaluationDS trainingPlanEvaluationDS1;
    private Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlanEvaluation.TrainingPlanEvaluationDSTableAdapters.TR_sprptTrainingPlanEvaluationTableAdapter tR_sprptTrainingPlanEvaluationTableAdapter;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCellSEQ;
    private XRTableCell xrTableCellTrainingEmployee;
    private XRTableCell xrTableCellTrainingCode;
    private XRTableCell xrTableCellTrainingCourse;
    private XRTableCell xrTableCellDurationFrom;
    private XRTableCell xrTableCellDurationTo;
    private XRTableCell xrTableCellEvalRequired;
    private XRTableCell xrTableCellEmpEvaluation;
    private PageHeaderBand PageHeader;
    private XRPictureBox xrPictureBox1;
    private XRRichText xrRichText1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public TrainingPlanEvaluationXtraReport()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingPlanEvaluationXtraReport));
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
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLblTrainingRequestEvaluation = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLblFullName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblTrainingCourse = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblTrainingCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSEQ = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingEmployee = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrainingCourse = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDurationFrom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDurationTo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEvalRequired = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpEvaluation = new DevExpress.XtraReports.UI.XRTableCell();
            this.trainingPlanEvaluationDS1 = new Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlanEvaluation.TrainingPlanEvaluationDS();
            this.tR_sprptTrainingPlanEvaluationTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TR.TrainingPlanEvaluation.TrainingPlanEvaluationDSTableAdapters.TR_sprptTrainingPlanEvaluationTableAdapter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingPlanEvaluationDS1)).BeginInit();
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
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1000F, 25F);
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
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanEvaluation.Seq")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 0.5104165649414063D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanEvaluation.FullName")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 1.6979164123535155D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanEvaluation.TrainingCode")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 1.526250457763672D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanEvaluation.TrainingCourse")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 1.5625015258789061D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanEvaluation.FromDate")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 1.0208319091796876D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanEvaluation.ToDate")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.Weight = 1.114581298828125D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanEvaluation.EvaluationRequiredInfo")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.Weight = 1.1979180908203126D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TR_sprptTrainingPlanEvaluation.EmployeeEvaluation")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 1.3695837402343751D;
            // 
            // xrLblTrainingRequestEvaluation
            // 
            this.xrLblTrainingRequestEvaluation.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblTrainingRequestEvaluation.LocationFloat = new DevExpress.Utils.PointFloat(284.9167F, 102.5F);
            this.xrLblTrainingRequestEvaluation.Name = "xrLblTrainingRequestEvaluation";
            this.xrLblTrainingRequestEvaluation.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblTrainingRequestEvaluation.SizeF = new System.Drawing.SizeF(468.7501F, 34.45834F);
            this.xrLblTrainingRequestEvaluation.StylePriority.UseFont = false;
            this.xrLblTrainingRequestEvaluation.StylePriority.UseTextAlignment = false;
            this.xrLblTrainingRequestEvaluation.Text = "Training Request Evaluation";
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
            this.xrRichText1,
            this.xrPictureBox1,
            this.xrLblFullName,
            this.xrLblTrainingCourse,
            this.xrLblTrainingCode,
            this.xrLblTrainingRequestEvaluation});
            this.ReportHeader.HeightF = 182.2917F;
            this.ReportHeader.Name = "ReportHeader";
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
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "/Content/Admin/images/logo_report.png";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(309F, 77.08334F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLblFullName
            // 
            this.xrLblFullName.LocationFloat = new DevExpress.Utils.PointFloat(51.04165F, 149.2917F);
            this.xrLblFullName.Name = "xrLblFullName";
            this.xrLblFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblFullName.SizeF = new System.Drawing.SizeF(271.2151F, 23F);
            this.xrLblFullName.StylePriority.UseTextAlignment = false;
            this.xrLblFullName.Text = "Training of Emloyee :";
            this.xrLblFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblTrainingCourse
            // 
            this.xrLblTrainingCourse.LocationFloat = new DevExpress.Utils.PointFloat(735.1598F, 149.2917F);
            this.xrLblTrainingCourse.Name = "xrLblTrainingCourse";
            this.xrLblTrainingCourse.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblTrainingCourse.SizeF = new System.Drawing.SizeF(264.8402F, 23F);
            this.xrLblTrainingCourse.StylePriority.UseTextAlignment = false;
            this.xrLblTrainingCourse.Text = "Training Course :";
            this.xrLblTrainingCourse.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblTrainingCode
            // 
            this.xrLblTrainingCode.LocationFloat = new DevExpress.Utils.PointFloat(373.4583F, 149.2917F);
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
            this.xrTable2.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTable2.ForeColor = System.Drawing.Color.White;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(999.9999F, 25F);
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
            this.xrTableCellTrainingEmployee,
            this.xrTableCellTrainingCode,
            this.xrTableCellTrainingCourse,
            this.xrTableCellDurationFrom,
            this.xrTableCellDurationTo,
            this.xrTableCellEvalRequired,
            this.xrTableCellEmpEvaluation});
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
            // xrTableCellTrainingEmployee
            // 
            this.xrTableCellTrainingEmployee.Name = "xrTableCellTrainingEmployee";
            this.xrTableCellTrainingEmployee.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingEmployee.Text = "Training Employee";
            this.xrTableCellTrainingEmployee.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingEmployee.Weight = 0.50937486324309467D;
            // 
            // xrTableCellTrainingCode
            // 
            this.xrTableCellTrainingCode.Name = "xrTableCellTrainingCode";
            this.xrTableCellTrainingCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingCode.Text = "Training Code";
            this.xrTableCellTrainingCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingCode.Weight = 0.45787498217010392D;
            // 
            // xrTableCellTrainingCourse
            // 
            this.xrTableCellTrainingCourse.Name = "xrTableCellTrainingCourse";
            this.xrTableCellTrainingCourse.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrainingCourse.Text = "Training Course";
            this.xrTableCellTrainingCourse.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrainingCourse.Weight = 0.46875066947941096D;
            // 
            // xrTableCellDurationFrom
            // 
            this.xrTableCellDurationFrom.Name = "xrTableCellDurationFrom";
            this.xrTableCellDurationFrom.StylePriority.UseTextAlignment = false;
            this.xrTableCellDurationFrom.Text = "Duration From";
            this.xrTableCellDurationFrom.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDurationFrom.Weight = 0.306249408340418D;
            // 
            // xrTableCellDurationTo
            // 
            this.xrTableCellDurationTo.Name = "xrTableCellDurationTo";
            this.xrTableCellDurationTo.StylePriority.UseTextAlignment = false;
            this.xrTableCellDurationTo.Text = "DurationTo";
            this.xrTableCellDurationTo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDurationTo.Weight = 0.33437441005703189D;
            // 
            // xrTableCellEvalRequired
            // 
            this.xrTableCellEvalRequired.Name = "xrTableCellEvalRequired";
            this.xrTableCellEvalRequired.StylePriority.UseTextAlignment = false;
            this.xrTableCellEvalRequired.Text = "Eval. Required";
            this.xrTableCellEvalRequired.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellEvalRequired.Weight = 0.35937563228611036D;
            // 
            // xrTableCellEmpEvaluation
            // 
            this.xrTableCellEmpEvaluation.Name = "xrTableCellEmpEvaluation";
            this.xrTableCellEmpEvaluation.StylePriority.UseTextAlignment = false;
            this.xrTableCellEmpEvaluation.Text = "Emp. Evaluation";
            this.xrTableCellEmpEvaluation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellEmpEvaluation.Weight = 0.41087505559540133D;
            // 
            // trainingPlanEvaluationDS1
            // 
            this.trainingPlanEvaluationDS1.DataSetName = "TrainingPlanEvaluationDS";
            this.trainingPlanEvaluationDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tR_sprptTrainingPlanEvaluationTableAdapter
            // 
            this.tR_sprptTrainingPlanEvaluationTableAdapter.ClearBeforeFill = true;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // TrainingPlanEvaluationXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader});
            this.DataAdapter = this.tR_sprptTrainingPlanEvaluationTableAdapter;
            this.DataMember = "TR_sprptTrainingPlanEvaluation";
            this.DataSource = this.trainingPlanEvaluationDS1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(52, 48, 76, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingPlanEvaluationDS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
