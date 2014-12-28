using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for QuarterlyCAFXtraReport
/// </summary>
public class QuarterlyCAFXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private XRPictureBox xrPictureBoxLogo;
    private XRLabel xrLabelHeaderTitle;
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
    private XRTable xrTable2;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCellSeq;
    private XRTableCell xrTableCellDepartment;
    private XRTableCell xrTableCellTGBSFirstQuarter;
    private XRTableCell xrTableCellTGBSSecondQuarter;
    private XRTableCell xrTableCellTGBSThirdQuarter;
    private XRLabel xrLabelHeaderSeq;
    private XRLabel xrLabelHeaderDepartment;
    private XRLabel xrLabelHeaderFirstQuarterTGBS;
    private XRLabel xrLabelHeaderSecondQuarterTGBS;
    private XRLabel xrLabelHeaderFourthQuarterTGBS;
    private XRLabel xrLabelHeaderTotalGROSSBasicSalary;
    private XRLabel xrLabelHeaderSecondQuarterTIFPBI;
    private XRLabel xrLabelHeaderFirstQuarterTIFPBI;
    private XRLabel xrLabelHeaderTotalInsuranceFeePaidByIndividual;
    private XRLabel xrLabelHeaderFirstQuarterTIFPBC;
    private XRLabel xrLabelHeaderFourthQuarterTIFPBI;
    private XRLabel xrLabelHeaderThirdQuarterTIFPBI;
    private XRLabel xrLabelHeaderTotalInsuranceFeePaidByCompany;
    private XRLabel xrLabelHeaderFourthQuarterTIFPBC;
    private XRLabel xrLabelHeaderThirdQuarterTIFPBC;
    private XRLabel xrLabelHeaderSecondQuarterTIFPBC;
    private XRLabel xrLabelHeaderYear;
    private PageFooterBand PageFooter;
    private XRTable xrTable3;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCellTotalsByQuarter;
    private XRTableCell xrTableCellTotalBasicSalaryGROSSFirstQuarter;

    private XRTableCell xrTableCellTotalBasicSalaryGROSSSeconQuarter;
    private XRPageInfo xrPageFooterRightText;
    private XRPageInfo xrPageFooterLeftText;
    private XRTable xrTable1;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCellTotalsByType;
    private XRTableCell xrTableCellTotalGROSSBasicSalary;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByIndividual;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByCompany;
    private XRTableCell xrTableCellTotalBasicSalaryGROSSFourthQuarter;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter;
    private XRTable xrTable4;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCellInfoTotalsByYear;
    private XRTableCell xrTableCellTotalsByYear;
    private XRTableCell xrTableCellTGBSFourthQuarter;
    private XRTableCell xrTableCellTIFPBIFirstQuarter;
    private XRTableCell xrTableCellTIFPBISecondQuarter;
    private XRTableCell xrTableCellTIFPBIThirdQuarter;
    private XRTableCell xrTableCellTIFPBIFourthQuarter;
    private XRTableCell xrTableCellTIFPBCFirstQuarter;
    private XRTableCell xrTableCellTIFPBCSecondQuarter;
    private XRTableCell xrTableCellTIFPBCThirdQuarter;
    private XRTableCell xrTableCellTIFPBCFourthQuarter;
    private XRLabel xrLabelHeaderThirdQuarterTGBS;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter;
    private XRTableCell xrTableCellTotalBasicSalaryGROSSThirdQuarter;
    private ReportFooterBand ReportFooter;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public QuarterlyCAFXtraReport()
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
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSeq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTGBSFirstQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTGBSSecondQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTGBSThirdQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTGBSFourthQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTIFPBIFirstQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTIFPBISecondQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTIFPBIThirdQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTIFPBIFourthQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTIFPBCFirstQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTIFPBCSecondQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTIFPBCThirdQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTIFPBCFourthQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabelHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
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
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabelHeaderThirdQuarterTGBS = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderFourthQuarterTIFPBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderThirdQuarterTIFPBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderSecondQuarterTIFPBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderFirstQuarterTIFPBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderFourthQuarterTIFPBI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderThirdQuarterTIFPBI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderSecondQuarterTIFPBI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderFirstQuarterTIFPBI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderFourthQuarterTGBS = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderTotalGROSSBasicSalary = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderSecondQuarterTGBS = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderFirstQuarterTGBS = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderDepartment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderSeq = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellInfoTotalsByYear = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalsByYear = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellTotalsByType = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalGROSSBasicSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByIndividual = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByCompany = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPageFooterRightText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageFooterLeftText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellTotalsByQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.214224F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1453.786F, 25F);
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSeq,
            this.xrTableCellDepartment,
            this.xrTableCellTGBSFirstQuarter,
            this.xrTableCellTGBSSecondQuarter,
            this.xrTableCellTGBSThirdQuarter,
            this.xrTableCellTGBSFourthQuarter,
            this.xrTableCellTIFPBIFirstQuarter,
            this.xrTableCellTIFPBISecondQuarter,
            this.xrTableCellTIFPBIThirdQuarter,
            this.xrTableCellTIFPBIFourthQuarter,
            this.xrTableCellTIFPBCFirstQuarter,
            this.xrTableCellTIFPBCSecondQuarter,
            this.xrTableCellTIFPBCThirdQuarter,
            this.xrTableCellTIFPBCFourthQuarter});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCellSeq
            // 
            this.xrTableCellSeq.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellSeq.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSeq.Name = "xrTableCellSeq";
            this.xrTableCellSeq.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellSeq.StylePriority.UseBorderColor = false;
            this.xrTableCellSeq.StylePriority.UseBorders = false;
            this.xrTableCellSeq.StylePriority.UsePadding = false;
            this.xrTableCellSeq.StylePriority.UseTextAlignment = false;
            this.xrTableCellSeq.Text = "Seq";
            this.xrTableCellSeq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSeq.Weight = 0.1620055732674564D;
            // 
            // xrTableCellDepartment
            // 
            this.xrTableCellDepartment.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellDepartment.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDepartment.Name = "xrTableCellDepartment";
            this.xrTableCellDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellDepartment.StylePriority.UseBorderColor = false;
            this.xrTableCellDepartment.StylePriority.UseBorders = false;
            this.xrTableCellDepartment.StylePriority.UsePadding = false;
            this.xrTableCellDepartment.StylePriority.UseTextAlignment = false;
            this.xrTableCellDepartment.Text = "Department";
            this.xrTableCellDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellDepartment.Weight = 0.63933482479251114D;
            // 
            // xrTableCellTGBSFirstQuarter
            // 
            this.xrTableCellTGBSFirstQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTGBSFirstQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTGBSFirstQuarter.Name = "xrTableCellTGBSFirstQuarter";
            this.xrTableCellTGBSFirstQuarter.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTGBSFirstQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTGBSFirstQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTGBSFirstQuarter.StylePriority.UsePadding = false;
            this.xrTableCellTGBSFirstQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTGBSFirstQuarter.Text = "FirstQuarterTGBS";
            this.xrTableCellTGBSFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTGBSFirstQuarter.Weight = 0.32001839671964477D;
            // 
            // xrTableCellTGBSSecondQuarter
            // 
            this.xrTableCellTGBSSecondQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTGBSSecondQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTGBSSecondQuarter.Name = "xrTableCellTGBSSecondQuarter";
            this.xrTableCellTGBSSecondQuarter.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTGBSSecondQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTGBSSecondQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTGBSSecondQuarter.StylePriority.UsePadding = false;
            this.xrTableCellTGBSSecondQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTGBSSecondQuarter.Text = "SecondQuarterTGBS";
            this.xrTableCellTGBSSecondQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTGBSSecondQuarter.Weight = 0.31882425907305645D;
            // 
            // xrTableCellTGBSThirdQuarter
            // 
            this.xrTableCellTGBSThirdQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTGBSThirdQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTGBSThirdQuarter.Name = "xrTableCellTGBSThirdQuarter";
            this.xrTableCellTGBSThirdQuarter.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTGBSThirdQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTGBSThirdQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTGBSThirdQuarter.StylePriority.UsePadding = false;
            this.xrTableCellTGBSThirdQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTGBSThirdQuarter.Text = "ThirdQuarterTGBS";
            this.xrTableCellTGBSThirdQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTGBSThirdQuarter.Weight = 0.32009358029129908D;
            // 
            // xrTableCellTGBSFourthQuarter
            // 
            this.xrTableCellTGBSFourthQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTGBSFourthQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTGBSFourthQuarter.Name = "xrTableCellTGBSFourthQuarter";
            this.xrTableCellTGBSFourthQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTGBSFourthQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTGBSFourthQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTGBSFourthQuarter.Text = "FourthQuarterTGBS";
            this.xrTableCellTGBSFourthQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTGBSFourthQuarter.Weight = 0.31158516590658414D;
            // 
            // xrTableCellTIFPBIFirstQuarter
            // 
            this.xrTableCellTIFPBIFirstQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTIFPBIFirstQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTIFPBIFirstQuarter.Name = "xrTableCellTIFPBIFirstQuarter";
            this.xrTableCellTIFPBIFirstQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTIFPBIFirstQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTIFPBIFirstQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTIFPBIFirstQuarter.Text = "FirstQuarterTIFPBI";
            this.xrTableCellTIFPBIFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTIFPBIFirstQuarter.Weight = 0.3317441534589215D;
            // 
            // xrTableCellTIFPBISecondQuarter
            // 
            this.xrTableCellTIFPBISecondQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTIFPBISecondQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTIFPBISecondQuarter.Name = "xrTableCellTIFPBISecondQuarter";
            this.xrTableCellTIFPBISecondQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTIFPBISecondQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTIFPBISecondQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTIFPBISecondQuarter.Text = "SecondQuarterTIFPBI";
            this.xrTableCellTIFPBISecondQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTIFPBISecondQuarter.Weight = 0.31855205516122431D;
            // 
            // xrTableCellTIFPBIThirdQuarter
            // 
            this.xrTableCellTIFPBIThirdQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTIFPBIThirdQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTIFPBIThirdQuarter.Name = "xrTableCellTIFPBIThirdQuarter";
            this.xrTableCellTIFPBIThirdQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTIFPBIThirdQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTIFPBIThirdQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTIFPBIThirdQuarter.Text = "ThirdQuarterTIFPBI";
            this.xrTableCellTIFPBIThirdQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTIFPBIThirdQuarter.Weight = 0.321743984387722D;
            // 
            // xrTableCellTIFPBIFourthQuarter
            // 
            this.xrTableCellTIFPBIFourthQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTIFPBIFourthQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTIFPBIFourthQuarter.Name = "xrTableCellTIFPBIFourthQuarter";
            this.xrTableCellTIFPBIFourthQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTIFPBIFourthQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTIFPBIFourthQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTIFPBIFourthQuarter.Text = "FourthQuarterTIFPBI";
            this.xrTableCellTIFPBIFourthQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTIFPBIFourthQuarter.Weight = 0.31250381279656381D;
            // 
            // xrTableCellTIFPBCFirstQuarter
            // 
            this.xrTableCellTIFPBCFirstQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTIFPBCFirstQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTIFPBCFirstQuarter.Name = "xrTableCellTIFPBCFirstQuarter";
            this.xrTableCellTIFPBCFirstQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTIFPBCFirstQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTIFPBCFirstQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTIFPBCFirstQuarter.Text = "FirstQuarterTIFPBC";
            this.xrTableCellTIFPBCFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTIFPBCFirstQuarter.Weight = 0.31707805131754824D;
            // 
            // xrTableCellTIFPBCSecondQuarter
            // 
            this.xrTableCellTIFPBCSecondQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTIFPBCSecondQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTIFPBCSecondQuarter.Name = "xrTableCellTIFPBCSecondQuarter";
            this.xrTableCellTIFPBCSecondQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTIFPBCSecondQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTIFPBCSecondQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTIFPBCSecondQuarter.Text = "SecondQuarterTIFPBC";
            this.xrTableCellTIFPBCSecondQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTIFPBCSecondQuarter.Weight = 0.32138917244440268D;
            // 
            // xrTableCellTIFPBCThirdQuarter
            // 
            this.xrTableCellTIFPBCThirdQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTIFPBCThirdQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTIFPBCThirdQuarter.Name = "xrTableCellTIFPBCThirdQuarter";
            this.xrTableCellTIFPBCThirdQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTIFPBCThirdQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTIFPBCThirdQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTIFPBCThirdQuarter.Text = "ThirdQuarterTIFPBC";
            this.xrTableCellTIFPBCThirdQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTIFPBCThirdQuarter.Weight = 0.31739066947211658D;
            // 
            // xrTableCellTIFPBCFourthQuarter
            // 
            this.xrTableCellTIFPBCFourthQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTIFPBCFourthQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTIFPBCFourthQuarter.Name = "xrTableCellTIFPBCFourthQuarter";
            this.xrTableCellTIFPBCFourthQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTIFPBCFourthQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTIFPBCFourthQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTIFPBCFourthQuarter.Text = "FourthQuarterTIFPBC";
            this.xrTableCellTIFPBCFourthQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTIFPBCFourthQuarter.Weight = 0.33548729900839791D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBoxLogo,
            this.xrLabelHeaderTitle,
            this.xrTableCompany});
            this.TopMargin.HeightF = 199.4921F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(450.694F, 125F);
            // 
            // xrLabelHeaderTitle
            // 
            this.xrLabelHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderTitle.ForeColor = System.Drawing.Color.Red;
            this.xrLabelHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 148.9583F);
            this.xrLabelHeaderTitle.Name = "xrLabelHeaderTitle";
            this.xrLabelHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTitle.SizeF = new System.Drawing.SizeF(1454F, 50.53377F);
            this.xrLabelHeaderTitle.StylePriority.UseFont = false;
            this.xrLabelHeaderTitle.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTitle.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTitle.Text = "COMPREHENSIVE ANNUAL FINANCIAL REPORT BY QUARTER";
            this.xrLabelHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCompany
            // 
            this.xrTableCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCompany.LocationFloat = new DevExpress.Utils.PointFloat(1076.958F, 0F);
            this.xrTableCompany.Name = "xrTableCompany";
            this.xrTableCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCompany.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5});
            this.xrTableCompany.SizeF = new System.Drawing.SizeF(373.0422F, 125F);
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
            this.xrTableCellLabelCompanyName.Weight = 0.613333751927913D;
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
            this.xrTableCellCompanyName.Weight = 1.3866662480720868D;
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
            this.xrTableCellLabelAddress.Weight = 0.613333751927913D;
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
            this.xrTableCellAddress.Weight = 1.3866662480720868D;
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
            this.xrTableCellLabelPhone.Weight = 0.6133382913930201D;
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
            this.xrTableCellPhone.Weight = 1.3866617086069801D;
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
            this.xrTableCellLabelFax.Weight = 0.6133382913930201D;
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
            this.xrTableCellFax.Weight = 1.3866617086069801D;
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
            this.xrTableCellLabelEmail.Weight = 0.6133382913930201D;
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
            this.xrTableCellEmail.Weight = 1.3866617086069801D;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelHeaderThirdQuarterTGBS,
            this.xrLabelHeaderYear,
            this.xrLabelHeaderFourthQuarterTIFPBC,
            this.xrLabelHeaderThirdQuarterTIFPBC,
            this.xrLabelHeaderSecondQuarterTIFPBC,
            this.xrLabelHeaderFirstQuarterTIFPBC,
            this.xrLabelHeaderFourthQuarterTIFPBI,
            this.xrLabelHeaderThirdQuarterTIFPBI,
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany,
            this.xrLabelHeaderSecondQuarterTIFPBI,
            this.xrLabelHeaderFirstQuarterTIFPBI,
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual,
            this.xrLabelHeaderFourthQuarterTGBS,
            this.xrLabelHeaderTotalGROSSBasicSalary,
            this.xrLabelHeaderSecondQuarterTGBS,
            this.xrLabelHeaderFirstQuarterTGBS,
            this.xrLabelHeaderDepartment,
            this.xrLabelHeaderSeq});
            this.PageHeader.HeightF = 100.0001F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabelHeaderThirdQuarterTGBS
            // 
            this.xrLabelHeaderThirdQuarterTGBS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderThirdQuarterTGBS.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderThirdQuarterTGBS.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderThirdQuarterTGBS.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderThirdQuarterTGBS.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderThirdQuarterTGBS.LocationFloat = new DevExpress.Utils.PointFloat(450.5035F, 46F);
            this.xrLabelHeaderThirdQuarterTGBS.Name = "xrLabelHeaderThirdQuarterTGBS";
            this.xrLabelHeaderThirdQuarterTGBS.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderThirdQuarterTGBS.SizeF = new System.Drawing.SizeF(100.3137F, 54.00005F);
            this.xrLabelHeaderThirdQuarterTGBS.StylePriority.UseBackColor = false;
            this.xrLabelHeaderThirdQuarterTGBS.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderThirdQuarterTGBS.StylePriority.UseBorders = false;
            this.xrLabelHeaderThirdQuarterTGBS.StylePriority.UseFont = false;
            this.xrLabelHeaderThirdQuarterTGBS.StylePriority.UseForeColor = false;
            this.xrLabelHeaderThirdQuarterTGBS.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderThirdQuarterTGBS.Text = "3";
            this.xrLabelHeaderThirdQuarterTGBS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderYear
            // 
            this.xrLabelHeaderYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderYear.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderYear.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderYear.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderYear.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderYear.LocationFloat = new DevExpress.Utils.PointFloat(250.8682F, 1.644266E-05F);
            this.xrLabelHeaderYear.Name = "xrLabelHeaderYear";
            this.xrLabelHeaderYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderYear.SizeF = new System.Drawing.SizeF(1203.132F, 23F);
            this.xrLabelHeaderYear.StylePriority.UseBackColor = false;
            this.xrLabelHeaderYear.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderYear.StylePriority.UseBorders = false;
            this.xrLabelHeaderYear.StylePriority.UseFont = false;
            this.xrLabelHeaderYear.StylePriority.UseForeColor = false;
            this.xrLabelHeaderYear.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderYear.Text = "Year";
            this.xrLabelHeaderYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderFourthQuarterTIFPBC
            // 
            this.xrLabelHeaderFourthQuarterTIFPBC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderFourthQuarterTIFPBC.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderFourthQuarterTIFPBC.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderFourthQuarterTIFPBC.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderFourthQuarterTIFPBC.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderFourthQuarterTIFPBC.LocationFloat = new DevExpress.Utils.PointFloat(1349.062F, 45.99996F);
            this.xrLabelHeaderFourthQuarterTIFPBC.Name = "xrLabelHeaderFourthQuarterTIFPBC";
            this.xrLabelHeaderFourthQuarterTIFPBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderFourthQuarterTIFPBC.SizeF = new System.Drawing.SizeF(104.938F, 54.00005F);
            this.xrLabelHeaderFourthQuarterTIFPBC.StylePriority.UseBackColor = false;
            this.xrLabelHeaderFourthQuarterTIFPBC.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderFourthQuarterTIFPBC.StylePriority.UseBorders = false;
            this.xrLabelHeaderFourthQuarterTIFPBC.StylePriority.UseFont = false;
            this.xrLabelHeaderFourthQuarterTIFPBC.StylePriority.UseForeColor = false;
            this.xrLabelHeaderFourthQuarterTIFPBC.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderFourthQuarterTIFPBC.Text = "4";
            this.xrLabelHeaderFourthQuarterTIFPBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderThirdQuarterTIFPBC
            // 
            this.xrLabelHeaderThirdQuarterTIFPBC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderThirdQuarterTIFPBC.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderThirdQuarterTIFPBC.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderThirdQuarterTIFPBC.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderThirdQuarterTIFPBC.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderThirdQuarterTIFPBC.LocationFloat = new DevExpress.Utils.PointFloat(1249.784F, 45.99994F);
            this.xrLabelHeaderThirdQuarterTIFPBC.Name = "xrLabelHeaderThirdQuarterTIFPBC";
            this.xrLabelHeaderThirdQuarterTIFPBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderThirdQuarterTIFPBC.SizeF = new System.Drawing.SizeF(99.27795F, 54.00003F);
            this.xrLabelHeaderThirdQuarterTIFPBC.StylePriority.UseBackColor = false;
            this.xrLabelHeaderThirdQuarterTIFPBC.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderThirdQuarterTIFPBC.StylePriority.UseBorders = false;
            this.xrLabelHeaderThirdQuarterTIFPBC.StylePriority.UseFont = false;
            this.xrLabelHeaderThirdQuarterTIFPBC.StylePriority.UseForeColor = false;
            this.xrLabelHeaderThirdQuarterTIFPBC.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderThirdQuarterTIFPBC.Text = "3";
            this.xrLabelHeaderThirdQuarterTIFPBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderSecondQuarterTIFPBC
            // 
            this.xrLabelHeaderSecondQuarterTIFPBC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderSecondQuarterTIFPBC.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderSecondQuarterTIFPBC.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderSecondQuarterTIFPBC.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderSecondQuarterTIFPBC.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderSecondQuarterTIFPBC.LocationFloat = new DevExpress.Utils.PointFloat(1149.256F, 45.99994F);
            this.xrLabelHeaderSecondQuarterTIFPBC.Name = "xrLabelHeaderSecondQuarterTIFPBC";
            this.xrLabelHeaderSecondQuarterTIFPBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderSecondQuarterTIFPBC.SizeF = new System.Drawing.SizeF(100.5283F, 54.00004F);
            this.xrLabelHeaderSecondQuarterTIFPBC.StylePriority.UseBackColor = false;
            this.xrLabelHeaderSecondQuarterTIFPBC.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderSecondQuarterTIFPBC.StylePriority.UseBorders = false;
            this.xrLabelHeaderSecondQuarterTIFPBC.StylePriority.UseFont = false;
            this.xrLabelHeaderSecondQuarterTIFPBC.StylePriority.UseForeColor = false;
            this.xrLabelHeaderSecondQuarterTIFPBC.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderSecondQuarterTIFPBC.Text = "2";
            this.xrLabelHeaderSecondQuarterTIFPBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderFirstQuarterTIFPBC
            // 
            this.xrLabelHeaderFirstQuarterTIFPBC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderFirstQuarterTIFPBC.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderFirstQuarterTIFPBC.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderFirstQuarterTIFPBC.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderFirstQuarterTIFPBC.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderFirstQuarterTIFPBC.LocationFloat = new DevExpress.Utils.PointFloat(1050.076F, 45.99994F);
            this.xrLabelHeaderFirstQuarterTIFPBC.Name = "xrLabelHeaderFirstQuarterTIFPBC";
            this.xrLabelHeaderFirstQuarterTIFPBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderFirstQuarterTIFPBC.SizeF = new System.Drawing.SizeF(99.18005F, 54.00003F);
            this.xrLabelHeaderFirstQuarterTIFPBC.StylePriority.UseBackColor = false;
            this.xrLabelHeaderFirstQuarterTIFPBC.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderFirstQuarterTIFPBC.StylePriority.UseBorders = false;
            this.xrLabelHeaderFirstQuarterTIFPBC.StylePriority.UseFont = false;
            this.xrLabelHeaderFirstQuarterTIFPBC.StylePriority.UseForeColor = false;
            this.xrLabelHeaderFirstQuarterTIFPBC.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderFirstQuarterTIFPBC.Text = "1";
            this.xrLabelHeaderFirstQuarterTIFPBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderFourthQuarterTIFPBI
            // 
            this.xrLabelHeaderFourthQuarterTIFPBI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderFourthQuarterTIFPBI.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderFourthQuarterTIFPBI.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderFourthQuarterTIFPBI.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderFourthQuarterTIFPBI.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderFourthQuarterTIFPBI.LocationFloat = new DevExpress.Utils.PointFloat(952.3274F, 46.00007F);
            this.xrLabelHeaderFourthQuarterTIFPBI.Name = "xrLabelHeaderFourthQuarterTIFPBI";
            this.xrLabelHeaderFourthQuarterTIFPBI.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderFourthQuarterTIFPBI.SizeF = new System.Drawing.SizeF(97.74854F, 54.00003F);
            this.xrLabelHeaderFourthQuarterTIFPBI.StylePriority.UseBackColor = false;
            this.xrLabelHeaderFourthQuarterTIFPBI.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderFourthQuarterTIFPBI.StylePriority.UseBorders = false;
            this.xrLabelHeaderFourthQuarterTIFPBI.StylePriority.UseFont = false;
            this.xrLabelHeaderFourthQuarterTIFPBI.StylePriority.UseForeColor = false;
            this.xrLabelHeaderFourthQuarterTIFPBI.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderFourthQuarterTIFPBI.Text = "4";
            this.xrLabelHeaderFourthQuarterTIFPBI.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderThirdQuarterTIFPBI
            // 
            this.xrLabelHeaderThirdQuarterTIFPBI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderThirdQuarterTIFPBI.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderThirdQuarterTIFPBI.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderThirdQuarterTIFPBI.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderThirdQuarterTIFPBI.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderThirdQuarterTIFPBI.LocationFloat = new DevExpress.Utils.PointFloat(851.6455F, 46.00003F);
            this.xrLabelHeaderThirdQuarterTIFPBI.Name = "xrLabelHeaderThirdQuarterTIFPBI";
            this.xrLabelHeaderThirdQuarterTIFPBI.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderThirdQuarterTIFPBI.SizeF = new System.Drawing.SizeF(100.6813F, 54.00003F);
            this.xrLabelHeaderThirdQuarterTIFPBI.StylePriority.UseBackColor = false;
            this.xrLabelHeaderThirdQuarterTIFPBI.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderThirdQuarterTIFPBI.StylePriority.UseBorders = false;
            this.xrLabelHeaderThirdQuarterTIFPBI.StylePriority.UseFont = false;
            this.xrLabelHeaderThirdQuarterTIFPBI.StylePriority.UseForeColor = false;
            this.xrLabelHeaderThirdQuarterTIFPBI.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderThirdQuarterTIFPBI.Text = "3";
            this.xrLabelHeaderThirdQuarterTIFPBI.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderTotalInsuranceFeePaidByCompany
            // 
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.LocationFloat = new DevExpress.Utils.PointFloat(1050.076F, 22.99996F);
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.Name = "xrLabelHeaderTotalInsuranceFeePaidByCompany";
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.SizeF = new System.Drawing.SizeF(403.9242F, 23F);
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.StylePriority.UseBackColor = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.StylePriority.UseBorders = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.StylePriority.UseFont = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.Text = "Total Insurance Fee Paid By Company";
            this.xrLabelHeaderTotalInsuranceFeePaidByCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderSecondQuarterTIFPBI
            // 
            this.xrLabelHeaderSecondQuarterTIFPBI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderSecondQuarterTIFPBI.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderSecondQuarterTIFPBI.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderSecondQuarterTIFPBI.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderSecondQuarterTIFPBI.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderSecondQuarterTIFPBI.LocationFloat = new DevExpress.Utils.PointFloat(752.0461F, 45.99994F);
            this.xrLabelHeaderSecondQuarterTIFPBI.Name = "xrLabelHeaderSecondQuarterTIFPBI";
            this.xrLabelHeaderSecondQuarterTIFPBI.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderSecondQuarterTIFPBI.SizeF = new System.Drawing.SizeF(99.59937F, 54.00003F);
            this.xrLabelHeaderSecondQuarterTIFPBI.StylePriority.UseBackColor = false;
            this.xrLabelHeaderSecondQuarterTIFPBI.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderSecondQuarterTIFPBI.StylePriority.UseBorders = false;
            this.xrLabelHeaderSecondQuarterTIFPBI.StylePriority.UseFont = false;
            this.xrLabelHeaderSecondQuarterTIFPBI.StylePriority.UseForeColor = false;
            this.xrLabelHeaderSecondQuarterTIFPBI.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderSecondQuarterTIFPBI.Text = "2";
            this.xrLabelHeaderSecondQuarterTIFPBI.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderFirstQuarterTIFPBI
            // 
            this.xrLabelHeaderFirstQuarterTIFPBI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderFirstQuarterTIFPBI.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderFirstQuarterTIFPBI.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderFirstQuarterTIFPBI.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderFirstQuarterTIFPBI.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderFirstQuarterTIFPBI.LocationFloat = new DevExpress.Utils.PointFloat(648.7373F, 45.99994F);
            this.xrLabelHeaderFirstQuarterTIFPBI.Name = "xrLabelHeaderFirstQuarterTIFPBI";
            this.xrLabelHeaderFirstQuarterTIFPBI.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderFirstQuarterTIFPBI.SizeF = new System.Drawing.SizeF(103.3089F, 54.00006F);
            this.xrLabelHeaderFirstQuarterTIFPBI.StylePriority.UseBackColor = false;
            this.xrLabelHeaderFirstQuarterTIFPBI.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderFirstQuarterTIFPBI.StylePriority.UseBorders = false;
            this.xrLabelHeaderFirstQuarterTIFPBI.StylePriority.UseFont = false;
            this.xrLabelHeaderFirstQuarterTIFPBI.StylePriority.UseForeColor = false;
            this.xrLabelHeaderFirstQuarterTIFPBI.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderFirstQuarterTIFPBI.Text = "1";
            this.xrLabelHeaderFirstQuarterTIFPBI.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderTotalInsuranceFeePaidByIndividual
            // 
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.LocationFloat = new DevExpress.Utils.PointFloat(648.7373F, 22.99996F);
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.Name = "xrLabelHeaderTotalInsuranceFeePaidByIndividual";
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.SizeF = new System.Drawing.SizeF(401.3386F, 22.99999F);
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.StylePriority.UseBackColor = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.StylePriority.UseBorders = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.StylePriority.UseFont = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.Text = "Total Insurance Fee Paid By Individual";
            this.xrLabelHeaderTotalInsuranceFeePaidByIndividual.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderFourthQuarterTGBS
            // 
            this.xrLabelHeaderFourthQuarterTGBS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderFourthQuarterTGBS.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderFourthQuarterTGBS.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderFourthQuarterTGBS.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderFourthQuarterTGBS.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderFourthQuarterTGBS.LocationFloat = new DevExpress.Utils.PointFloat(550.8171F, 46.00005F);
            this.xrLabelHeaderFourthQuarterTGBS.Name = "xrLabelHeaderFourthQuarterTGBS";
            this.xrLabelHeaderFourthQuarterTGBS.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderFourthQuarterTGBS.SizeF = new System.Drawing.SizeF(97.46179F, 54.00004F);
            this.xrLabelHeaderFourthQuarterTGBS.StylePriority.UseBackColor = false;
            this.xrLabelHeaderFourthQuarterTGBS.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderFourthQuarterTGBS.StylePriority.UseBorders = false;
            this.xrLabelHeaderFourthQuarterTGBS.StylePriority.UseFont = false;
            this.xrLabelHeaderFourthQuarterTGBS.StylePriority.UseForeColor = false;
            this.xrLabelHeaderFourthQuarterTGBS.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderFourthQuarterTGBS.Text = "4";
            this.xrLabelHeaderFourthQuarterTGBS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderTotalGROSSBasicSalary
            // 
            this.xrLabelHeaderTotalGROSSBasicSalary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderTotalGROSSBasicSalary.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderTotalGROSSBasicSalary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderTotalGROSSBasicSalary.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderTotalGROSSBasicSalary.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderTotalGROSSBasicSalary.LocationFloat = new DevExpress.Utils.PointFloat(250.8682F, 23.00002F);
            this.xrLabelHeaderTotalGROSSBasicSalary.Name = "xrLabelHeaderTotalGROSSBasicSalary";
            this.xrLabelHeaderTotalGROSSBasicSalary.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTotalGROSSBasicSalary.SizeF = new System.Drawing.SizeF(397.4107F, 23F);
            this.xrLabelHeaderTotalGROSSBasicSalary.StylePriority.UseBackColor = false;
            this.xrLabelHeaderTotalGROSSBasicSalary.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderTotalGROSSBasicSalary.StylePriority.UseBorders = false;
            this.xrLabelHeaderTotalGROSSBasicSalary.StylePriority.UseFont = false;
            this.xrLabelHeaderTotalGROSSBasicSalary.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTotalGROSSBasicSalary.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTotalGROSSBasicSalary.Text = "Total GROSS Basic Salary";
            this.xrLabelHeaderTotalGROSSBasicSalary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderSecondQuarterTGBS
            // 
            this.xrLabelHeaderSecondQuarterTGBS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderSecondQuarterTGBS.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderSecondQuarterTGBS.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderSecondQuarterTGBS.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderSecondQuarterTGBS.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderSecondQuarterTGBS.LocationFloat = new DevExpress.Utils.PointFloat(350.6157F, 46F);
            this.xrLabelHeaderSecondQuarterTGBS.Name = "xrLabelHeaderSecondQuarterTGBS";
            this.xrLabelHeaderSecondQuarterTGBS.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderSecondQuarterTGBS.SizeF = new System.Drawing.SizeF(99.88776F, 54.00003F);
            this.xrLabelHeaderSecondQuarterTGBS.StylePriority.UseBackColor = false;
            this.xrLabelHeaderSecondQuarterTGBS.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderSecondQuarterTGBS.StylePriority.UseBorders = false;
            this.xrLabelHeaderSecondQuarterTGBS.StylePriority.UseFont = false;
            this.xrLabelHeaderSecondQuarterTGBS.StylePriority.UseForeColor = false;
            this.xrLabelHeaderSecondQuarterTGBS.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderSecondQuarterTGBS.Text = "2";
            this.xrLabelHeaderSecondQuarterTGBS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderFirstQuarterTGBS
            // 
            this.xrLabelHeaderFirstQuarterTGBS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderFirstQuarterTGBS.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderFirstQuarterTGBS.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderFirstQuarterTGBS.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderFirstQuarterTGBS.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderFirstQuarterTGBS.LocationFloat = new DevExpress.Utils.PointFloat(250.8682F, 46F);
            this.xrLabelHeaderFirstQuarterTGBS.Name = "xrLabelHeaderFirstQuarterTGBS";
            this.xrLabelHeaderFirstQuarterTGBS.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderFirstQuarterTGBS.SizeF = new System.Drawing.SizeF(99.7475F, 54.00003F);
            this.xrLabelHeaderFirstQuarterTGBS.StylePriority.UseBackColor = false;
            this.xrLabelHeaderFirstQuarterTGBS.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderFirstQuarterTGBS.StylePriority.UseBorders = false;
            this.xrLabelHeaderFirstQuarterTGBS.StylePriority.UseFont = false;
            this.xrLabelHeaderFirstQuarterTGBS.StylePriority.UseForeColor = false;
            this.xrLabelHeaderFirstQuarterTGBS.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderFirstQuarterTGBS.Text = "1";
            this.xrLabelHeaderFirstQuarterTGBS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderDepartment
            // 
            this.xrLabelHeaderDepartment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderDepartment.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderDepartment.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderDepartment.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderDepartment.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderDepartment.LocationFloat = new DevExpress.Utils.PointFloat(50.88851F, 0F);
            this.xrLabelHeaderDepartment.Name = "xrLabelHeaderDepartment";
            this.xrLabelHeaderDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderDepartment.SizeF = new System.Drawing.SizeF(199.9798F, 100F);
            this.xrLabelHeaderDepartment.StylePriority.UseBackColor = false;
            this.xrLabelHeaderDepartment.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderDepartment.StylePriority.UseBorders = false;
            this.xrLabelHeaderDepartment.StylePriority.UseFont = false;
            this.xrLabelHeaderDepartment.StylePriority.UseForeColor = false;
            this.xrLabelHeaderDepartment.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderDepartment.Text = "Department";
            this.xrLabelHeaderDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderSeq
            // 
            this.xrLabelHeaderSeq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderSeq.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelHeaderSeq.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderSeq.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderSeq.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderSeq.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabelHeaderSeq.Name = "xrLabelHeaderSeq";
            this.xrLabelHeaderSeq.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderSeq.SizeF = new System.Drawing.SizeF(50.88851F, 100F);
            this.xrLabelHeaderSeq.StylePriority.UseBackColor = false;
            this.xrLabelHeaderSeq.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderSeq.StylePriority.UseBorders = false;
            this.xrLabelHeaderSeq.StylePriority.UseFont = false;
            this.xrLabelHeaderSeq.StylePriority.UseForeColor = false;
            this.xrLabelHeaderSeq.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderSeq.Text = "Seq";
            this.xrLabelHeaderSeq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageFooterRightText,
            this.xrPageFooterLeftText});
            this.PageFooter.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.PageFooter.HeightF = 33.00001F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.StylePriority.UseFont = false;
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.2142857F, 50F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1453.786F, 25F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellInfoTotalsByYear,
            this.xrTableCellTotalsByYear});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCellInfoTotalsByYear
            // 
            this.xrTableCellInfoTotalsByYear.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellInfoTotalsByYear.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellInfoTotalsByYear.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellInfoTotalsByYear.Name = "xrTableCellInfoTotalsByYear";
            this.xrTableCellInfoTotalsByYear.StylePriority.UseBorderColor = false;
            this.xrTableCellInfoTotalsByYear.StylePriority.UseBorders = false;
            this.xrTableCellInfoTotalsByYear.StylePriority.UseFont = false;
            this.xrTableCellInfoTotalsByYear.StylePriority.UseTextAlignment = false;
            this.xrTableCellInfoTotalsByYear.Text = "Totals By Year";
            this.xrTableCellInfoTotalsByYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellInfoTotalsByYear.Weight = 0.40266968074762621D;
            // 
            // xrTableCellTotalsByYear
            // 
            this.xrTableCellTotalsByYear.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalsByYear.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalsByYear.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalsByYear.Name = "xrTableCellTotalsByYear";
            this.xrTableCellTotalsByYear.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalsByYear.StylePriority.UseBorders = false;
            this.xrTableCellTotalsByYear.StylePriority.UseFont = false;
            this.xrTableCellTotalsByYear.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalsByYear.Text = "Totals";
            this.xrTableCellTotalsByYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalsByYear.Weight = 1.9308080399831962D;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.214227F, 25F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1453.786F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellTotalsByType,
            this.xrTableCellTotalGROSSBasicSalary,
            this.xrTableCellTotalInsuranceFeePaidByIndividual,
            this.xrTableCellTotalInsuranceFeePaidByCompany});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.StylePriority.UseBorders = false;
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCellTotalsByType
            // 
            this.xrTableCellTotalsByType.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalsByType.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalsByType.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalsByType.Name = "xrTableCellTotalsByType";
            this.xrTableCellTotalsByType.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalsByType.StylePriority.UseBorders = false;
            this.xrTableCellTotalsByType.StylePriority.UseFont = false;
            this.xrTableCellTotalsByType.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalsByType.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalsByType.Weight = 0.3510626132290891D;
            // 
            // xrTableCellTotalGROSSBasicSalary
            // 
            this.xrTableCellTotalGROSSBasicSalary.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalGROSSBasicSalary.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalGROSSBasicSalary.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalGROSSBasicSalary.Name = "xrTableCellTotalGROSSBasicSalary";
            this.xrTableCellTotalGROSSBasicSalary.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalGROSSBasicSalary.StylePriority.UseBorders = false;
            this.xrTableCellTotalGROSSBasicSalary.StylePriority.UseFont = false;
            this.xrTableCellTotalGROSSBasicSalary.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalGROSSBasicSalary.Text = "TotalGROSSBasicSalary";
            this.xrTableCellTotalGROSSBasicSalary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalGROSSBasicSalary.Weight = 0.55724984647179943D;
            // 
            // xrTableCellTotalInsuranceFeePaidByIndividual
            // 
            this.xrTableCellTotalInsuranceFeePaidByIndividual.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Name = "xrTableCellTotalInsuranceFeePaidByIndividual";
            this.xrTableCellTotalInsuranceFeePaidByIndividual.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Text = "TotalInsuranceFeePaidByIndividual";
            this.xrTableCellTotalInsuranceFeePaidByIndividual.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Weight = 0.56210942385792084D;
            // 
            // xrTableCellTotalInsuranceFeePaidByCompany
            // 
            this.xrTableCellTotalInsuranceFeePaidByCompany.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByCompany.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByCompany.Name = "xrTableCellTotalInsuranceFeePaidByCompany";
            this.xrTableCellTotalInsuranceFeePaidByCompany.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByCompany.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByCompany.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByCompany.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByCompany.Text = "TotalInsuranceFeePaidByCompany";
            this.xrTableCellTotalInsuranceFeePaidByCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByCompany.Weight = 0.565730643599114D;
            // 
            // xrPageFooterRightText
            // 
            this.xrPageFooterRightText.Format = "Page {0} of {1}";
            this.xrPageFooterRightText.LocationFloat = new DevExpress.Utils.PointFloat(1170.346F, 10.00001F);
            this.xrPageFooterRightText.Name = "xrPageFooterRightText";
            this.xrPageFooterRightText.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrPageFooterRightText.SizeF = new System.Drawing.SizeF(273.6536F, 23F);
            this.xrPageFooterRightText.StylePriority.UsePadding = false;
            this.xrPageFooterRightText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterRightText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrPageFooterLeftText
            // 
            this.xrPageFooterLeftText.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrPageFooterLeftText.Name = "xrPageFooterLeftText";
            this.xrPageFooterLeftText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageFooterLeftText.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageFooterLeftText.SizeF = new System.Drawing.SizeF(239.583F, 23F);
            this.xrPageFooterLeftText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterLeftText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1454F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellTotalsByQuarter,
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter,
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter,
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter,
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter,
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter,
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter,
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter,
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter,
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter,
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter,
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter,
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCellTotalsByQuarter
            // 
            this.xrTableCellTotalsByQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalsByQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalsByQuarter.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalsByQuarter.Name = "xrTableCellTotalsByQuarter";
            this.xrTableCellTotalsByQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalsByQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalsByQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalsByQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalsByQuarter.Text = "Totals By Quarter";
            this.xrTableCellTotalsByQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalsByQuarter.Weight = 0.35838954351923569D;
            // 
            // xrTableCellTotalBasicSalaryGROSSFirstQuarter
            // 
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.Name = "xrTableCellTotalBasicSalaryGROSSFirstQuarter";
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.Text = "xrTableCellTotalBasicSalaryGROSSFirstQuarter";
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalBasicSalaryGROSSFirstQuarter.Weight = 0.14300199227042088D;
            // 
            // xrTableCellTotalBasicSalaryGROSSSeconQuarter
            // 
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.Name = "xrTableCellTotalBasicSalaryGROSSSeconQuarter";
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.Text = "TotalBasicSalaryGROSSSeconQuarter";
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalBasicSalaryGROSSSeconQuarter.Weight = 0.14246831817046074D;
            // 
            // xrTableCellTotalBasicSalaryGROSSThirdQuarter
            // 
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.Name = "xrTableCellTotalBasicSalaryGROSSThirdQuarter";
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.Text = "TotalBasicSalaryGROSSThirdQuarter";
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalBasicSalaryGROSSThirdQuarter.Weight = 0.14303564764249133D;
            // 
            // xrTableCellTotalBasicSalaryGROSSFourthQuarter
            // 
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.Name = "xrTableCellTotalBasicSalaryGROSSFourthQuarter";
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.Text = "TotalBasicSalaryGROSSFourthQuarter";
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalBasicSalaryGROSSFourthQuarter.Weight = 0.13988831463262505D;
            // 
            // xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter
            // 
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.Name = "xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter";
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.Text = "TotalInsuranceFeePaidByIndividualFirstQuarter";
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.Weight = 0.14758672851860716D;
            // 
            // xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter
            // 
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.Name = "xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter";
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.Text = "TotalInsuranceFeePaidByIndividualSecondQuarter";
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.Weight = 0.14234677135241458D;
            // 
            // xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter
            // 
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.Name = "xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter";
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.Text = "TotalInsuranceFeePaidByIndividualThirdQuarter";
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.Weight = 0.14377335766867294D;
            // 
            // xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter
            // 
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.Name = "xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter";
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.Text = "TotalInsuranceFeePaidByIndividualFourQuarter";
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.Weight = 0.13964408894495373D;
            this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter_BeforePrint);
            // 
            // xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter
            // 
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.Name = "xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter";
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.Text = "TotalInsuranceFeePaidByCompanyFirstQuarter";
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.Weight = 0.14168810127331794D;
            // 
            // xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter
            // 
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.Name = "xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter";
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.Text = "TotalInsuranceFeePaidByCompanySecondQuarter";
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.Weight = 0.143614607131393D;
            // 
            // xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter
            // 
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.Name = "xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter";
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.Text = "xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter";
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.Weight = 0.14182778655602757D;
            // 
            // xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter
            // 
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.Name = "xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter";
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.Text = "xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter";
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.Weight = 0.14991401577242289D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrTable1,
            this.xrTable4});
            this.ReportFooter.HeightF = 75F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // QuarterlyCAFXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 199, 0);
            this.PageHeight = 1169;
            this.PageWidth = 1654;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }
}
