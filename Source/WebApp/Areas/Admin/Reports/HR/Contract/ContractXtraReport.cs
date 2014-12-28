using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for ContractXtraReport
/// </summary>
public class ContractXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabelHeaderTitle;
    private ReportHeaderBand ReportHeader;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageFooterRightText;
    private XRPageInfo xrPageFooterLeftText;
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
    private XRPictureBox xrPictureBoxLogo;
    private XRTable xrTable1;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCellHeaderSeq;
    private XRTableCell xrTableCellHeaderContractTypeName;
    private XRTableCell xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender;
    private XRTableCell xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender;
    private XRTableCell xrTableCellHeaderTotalContractsByLSContractTypeID;
    private XRTableCell xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender;
    private XRTableCell xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender;
    private XRTableCell xrTableCellHeaderTotalPercentByLSContractTypeID;
    private XRTable xrTable2;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCellSeq;
    private XRTableCell xrTableCellContractTypeName;
    private XRTableCell xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender;
    private XRTableCell xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender;
    private XRTableCell xrTableCellTotalContractsByLSContractTypeID;
    private XRTableCell xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender;
    private XRTableCell xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender;
    private XRTableCell xrTableCellTotalPercentByLSContractTypeID;
    private XRTableCell xrTableCellLSContractTypeCode;
    private XRTableCell xrTableCellHeaderLSContractTypeCode;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public ContractXtraReport()
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
            this.xrTableCellContractTypeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalContractsByLSContractTypeID = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalPercentByLSContractTypeID = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabelHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
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
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellHeaderSeq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderContractTypeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalContractsByLSContractTypeID = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalPercentByLSContractTypeID = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageFooterLeftText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageFooterRightText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrTableCellHeaderLSContractTypeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellLSContractTypeCode = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
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
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.001207987F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable2.SizeF = new System.Drawing.SizeF(973.9992F, 25F);
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSeq,
            this.xrTableCellLSContractTypeCode,
            this.xrTableCellContractTypeName,
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender,
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender,
            this.xrTableCellTotalContractsByLSContractTypeID,
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender,
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender,
            this.xrTableCellTotalPercentByLSContractTypeID});
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
            this.xrTableCellSeq.Weight = 0.27790940520787838D;
            // 
            // xrTableCellContractTypeName
            // 
            this.xrTableCellContractTypeName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellContractTypeName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellContractTypeName.Name = "xrTableCellContractTypeName";
            this.xrTableCellContractTypeName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellContractTypeName.StylePriority.UseBorderColor = false;
            this.xrTableCellContractTypeName.StylePriority.UseBorders = false;
            this.xrTableCellContractTypeName.StylePriority.UsePadding = false;
            this.xrTableCellContractTypeName.StylePriority.UseTextAlignment = false;
            this.xrTableCellContractTypeName.Text = "ContractTypeName";
            this.xrTableCellContractTypeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellContractTypeName.Weight = 1.1683763253729127D;
            // 
            // xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender
            // 
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.Name = "xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorders = false;
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UsePadding = false;
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.Text = "TotalMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.Weight = 0.3025652620276244D;
            // 
            // xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender
            // 
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Name = "xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorders = false;
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UsePadding = false;
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Text = "TotalFeMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Weight = 0.30256512228501492D;
            // 
            // xrTableCellTotalContractsByLSContractTypeID
            // 
            this.xrTableCellTotalContractsByLSContractTypeID.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalContractsByLSContractTypeID.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalContractsByLSContractTypeID.Name = "xrTableCellTotalContractsByLSContractTypeID";
            this.xrTableCellTotalContractsByLSContractTypeID.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalContractsByLSContractTypeID.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalContractsByLSContractTypeID.StylePriority.UseBorders = false;
            this.xrTableCellTotalContractsByLSContractTypeID.StylePriority.UsePadding = false;
            this.xrTableCellTotalContractsByLSContractTypeID.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalContractsByLSContractTypeID.Text = "TotalContractsByLSContractTypeID";
            this.xrTableCellTotalContractsByLSContractTypeID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalContractsByLSContractTypeID.Weight = 0.43822777762303355D;
            // 
            // xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender
            // 
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Name = "xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorders = false;
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UsePadding = false;
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Text = "TotalPercentMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Weight = 0.34048337375923737D;
            // 
            // xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender
            // 
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Name = "xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorders = false;
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UsePadding = false;
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Text = "TotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Weight = 0.46254495140332857D;
            // 
            // xrTableCellTotalPercentByLSContractTypeID
            // 
            this.xrTableCellTotalPercentByLSContractTypeID.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalPercentByLSContractTypeID.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalPercentByLSContractTypeID.Name = "xrTableCellTotalPercentByLSContractTypeID";
            this.xrTableCellTotalPercentByLSContractTypeID.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalPercentByLSContractTypeID.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalPercentByLSContractTypeID.StylePriority.UseBorders = false;
            this.xrTableCellTotalPercentByLSContractTypeID.StylePriority.UsePadding = false;
            this.xrTableCellTotalPercentByLSContractTypeID.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalPercentByLSContractTypeID.Text = "TotalPercentByLSContractTypeID";
            this.xrTableCellTotalPercentByLSContractTypeID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalPercentByLSContractTypeID.Weight = 0.57413562044064426D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabelHeaderTitle
            // 
            this.xrLabelHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderTitle.ForeColor = System.Drawing.Color.Red;
            this.xrLabelHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(0.0004291534F, 147.3378F);
            this.xrLabelHeaderTitle.Name = "xrLabelHeaderTitle";
            this.xrLabelHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTitle.SizeF = new System.Drawing.SizeF(974F, 50.53378F);
            this.xrLabelHeaderTitle.StylePriority.UseFont = false;
            this.xrLabelHeaderTitle.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTitle.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTitle.Text = "EMPLOYEE STATISTIC BY CONTRACT";
            this.xrLabelHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBoxLogo,
            this.xrTableCompany,
            this.xrLabelHeaderTitle});
            this.ReportHeader.HeightF = 211.6442F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(450.694F, 125F);
            // 
            // xrTableCompany
            // 
            this.xrTableCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCompany.LocationFloat = new DevExpress.Utils.PointFloat(600.9581F, 0F);
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
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable1.SizeF = new System.Drawing.SizeF(973.9996F, 25F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellHeaderSeq,
            this.xrTableCellHeaderLSContractTypeCode,
            this.xrTableCellHeaderContractTypeName,
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender,
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender,
            this.xrTableCellHeaderTotalContractsByLSContractTypeID,
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender,
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender,
            this.xrTableCellHeaderTotalPercentByLSContractTypeID});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCellHeaderSeq
            // 
            this.xrTableCellHeaderSeq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderSeq.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderSeq.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderSeq.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderSeq.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderSeq.Name = "xrTableCellHeaderSeq";
            this.xrTableCellHeaderSeq.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderSeq.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderSeq.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderSeq.StylePriority.UseBorders = false;
            this.xrTableCellHeaderSeq.StylePriority.UseFont = false;
            this.xrTableCellHeaderSeq.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderSeq.StylePriority.UsePadding = false;
            this.xrTableCellHeaderSeq.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderSeq.Text = "Seq";
            this.xrTableCellHeaderSeq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderSeq.Weight = 0.18506124808552243D;
            // 
            // xrTableCellHeaderContractTypeName
            // 
            this.xrTableCellHeaderContractTypeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderContractTypeName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderContractTypeName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderContractTypeName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderContractTypeName.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderContractTypeName.Name = "xrTableCellHeaderContractTypeName";
            this.xrTableCellHeaderContractTypeName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderContractTypeName.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderContractTypeName.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderContractTypeName.StylePriority.UseBorders = false;
            this.xrTableCellHeaderContractTypeName.StylePriority.UseFont = false;
            this.xrTableCellHeaderContractTypeName.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderContractTypeName.StylePriority.UsePadding = false;
            this.xrTableCellHeaderContractTypeName.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderContractTypeName.Text = "ContractTypeName";
            this.xrTableCellHeaderContractTypeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellHeaderContractTypeName.Weight = 0.77801954531107231D;
            // 
            // xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender
            // 
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.Name = "xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.Text = "Male";
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.Weight = 0.20147819662214755D;
            // 
            // xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender
            // 
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Name = "xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Text = "Female";
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Weight = 0.20147819662214755D;
            // 
            // xrTableCellHeaderTotalContractsByLSContractTypeID
            // 
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.Name = "xrTableCellHeaderTotalContractsByLSContractTypeID";
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.Text = "Totals";
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalContractsByLSContractTypeID.Weight = 0.29181573497044655D;
            // 
            // xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender
            // 
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Name = "xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Text = "Male";
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Weight = 0.22672829204445241D;
            // 
            // xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender
            // 
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Name = "xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender";
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Text = "Female";
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Weight = 0.30800852882085733D;
            // 
            // xrTableCellHeaderTotalPercentByLSContractTypeID
            // 
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.Name = "xrTableCellHeaderTotalPercentByLSContractTypeID";
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.Text = "Total Percent";
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalPercentByLSContractTypeID.Weight = 0.38231662389712906D;
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
            this.xrPageFooterLeftText.LocationFloat = new DevExpress.Utils.PointFloat(0.0003814697F, 0F);
            this.xrPageFooterLeftText.Name = "xrPageFooterLeftText";
            this.xrPageFooterLeftText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageFooterLeftText.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageFooterLeftText.SizeF = new System.Drawing.SizeF(239.583F, 23F);
            this.xrPageFooterLeftText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterLeftText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageFooterRightText
            // 
            this.xrPageFooterRightText.Format = "Page {0} of {1}";
            this.xrPageFooterRightText.LocationFloat = new DevExpress.Utils.PointFloat(700.3468F, 0F);
            this.xrPageFooterRightText.Name = "xrPageFooterRightText";
            this.xrPageFooterRightText.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrPageFooterRightText.SizeF = new System.Drawing.SizeF(273.6536F, 23F);
            this.xrPageFooterRightText.StylePriority.UsePadding = false;
            this.xrPageFooterRightText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterRightText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrTableCellHeaderLSContractTypeCode
            // 
            this.xrTableCellHeaderLSContractTypeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderLSContractTypeCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderLSContractTypeCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderLSContractTypeCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderLSContractTypeCode.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderLSContractTypeCode.Name = "xrTableCellHeaderLSContractTypeCode";
            this.xrTableCellHeaderLSContractTypeCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderLSContractTypeCode.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderLSContractTypeCode.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderLSContractTypeCode.StylePriority.UseBorders = false;
            this.xrTableCellHeaderLSContractTypeCode.StylePriority.UseFont = false;
            this.xrTableCellHeaderLSContractTypeCode.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderLSContractTypeCode.StylePriority.UsePadding = false;
            this.xrTableCellHeaderLSContractTypeCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderLSContractTypeCode.Text = "Contract Type Code";
            this.xrTableCellHeaderLSContractTypeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderLSContractTypeCode.Weight = 0.425093633626225D;
            // 
            // xrTableCellLSContractTypeCode
            // 
            this.xrTableCellLSContractTypeCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellLSContractTypeCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLSContractTypeCode.Name = "xrTableCellLSContractTypeCode";
            this.xrTableCellLSContractTypeCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellLSContractTypeCode.StylePriority.UseBorderColor = false;
            this.xrTableCellLSContractTypeCode.StylePriority.UseBorders = false;
            this.xrTableCellLSContractTypeCode.StylePriority.UsePadding = false;
            this.xrTableCellLSContractTypeCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellLSContractTypeCode.Text = "LSContractTypeCode";
            this.xrTableCellLSContractTypeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellLSContractTypeCode.Weight = 0.63837084652664222D;
            // 
            // ContractXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.PageFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 95, 0, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
