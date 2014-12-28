using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for CAFDetailXtraReport
/// </summary>
public class CAFDetailXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
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
    private XRLabel xrLabelHeaderTitle;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCellHeaderSeq;
    private XRTableCell xrTableCellHeaderLSCompanyCode;
    private XRTableCell xrTableCellHeaderDepartment;
    private XRTableCell xrTableCellHeaderTotalGROSSBasicSalaryByDept;
    private XRTableCell xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual;
    private XRTableCell xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany;
    private XRTable xrTable4;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCellMasterLSCompanyCode;
    private XRTableCell xrTableCellMasterDepartmentName;
    public DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCellSeq;
    private XRTableCell xrTableCellLSCompanyCode;
    private XRTableCell xrTableCellDepartment;
    private XRTableCell xrTableCellTotalGROSSBasicSalaryByDept;
    private XRTableCell xrTableCellTotalInsuranceFeeByDeptPaidByIndividual;
    private XRTableCell xrTableCellTotalInsuranceFeeByDeptPaidByCompany;
    private PageFooterBand PageFooter;
    private XRTable xrTable3;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCellTotalSummary;
    private XRTableCell xrTableCellTotalBasicSalaryGROSS;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByIndividual;
    private XRTableCell xrTableCellTotalInsuranceFeePaidByCompany;
    private XRPageInfo xrPageFooterLeftText;
    private XRPageInfo xrPageFooterRightText;
    private ReportFooterBand ReportFooter;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public CAFDetailXtraReport()
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
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellMasterLSCompanyCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMasterDepartmentName = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
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
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabelHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellHeaderSeq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderLSCompanyCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSeq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellLSCompanyCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalGROSSBasicSalaryByDept = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellTotalSummary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalBasicSalaryGROSS = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByIndividual = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsuranceFeePaidByCompany = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPageFooterLeftText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageFooterRightText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.000222524F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable4.SizeF = new System.Drawing.SizeF(968.9999F, 25F);
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellMasterLSCompanyCode,
            this.xrTableCellMasterDepartmentName});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCellMasterLSCompanyCode
            // 
            this.xrTableCellMasterLSCompanyCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.xrTableCellMasterLSCompanyCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellMasterLSCompanyCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMasterLSCompanyCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellMasterLSCompanyCode.Name = "xrTableCellMasterLSCompanyCode";
            this.xrTableCellMasterLSCompanyCode.StylePriority.UseBackColor = false;
            this.xrTableCellMasterLSCompanyCode.StylePriority.UseBorderColor = false;
            this.xrTableCellMasterLSCompanyCode.StylePriority.UseBorders = false;
            this.xrTableCellMasterLSCompanyCode.StylePriority.UseFont = false;
            this.xrTableCellMasterLSCompanyCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellMasterLSCompanyCode.Text = "xrTableCellMasterLSCompanyCode";
            this.xrTableCellMasterLSCompanyCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMasterLSCompanyCode.Weight = 0.088845341933601552D;
            // 
            // xrTableCellMasterDepartmentName
            // 
            this.xrTableCellMasterDepartmentName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.xrTableCellMasterDepartmentName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellMasterDepartmentName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMasterDepartmentName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellMasterDepartmentName.ForeColor = System.Drawing.Color.Black;
            this.xrTableCellMasterDepartmentName.Name = "xrTableCellMasterDepartmentName";
            this.xrTableCellMasterDepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellMasterDepartmentName.StylePriority.UseBackColor = false;
            this.xrTableCellMasterDepartmentName.StylePriority.UseBorderColor = false;
            this.xrTableCellMasterDepartmentName.StylePriority.UseBorders = false;
            this.xrTableCellMasterDepartmentName.StylePriority.UseFont = false;
            this.xrTableCellMasterDepartmentName.StylePriority.UseForeColor = false;
            this.xrTableCellMasterDepartmentName.StylePriority.UsePadding = false;
            this.xrTableCellMasterDepartmentName.StylePriority.UseTextAlignment = false;
            this.xrTableCellMasterDepartmentName.Text = "Department Name";
            this.xrTableCellMasterDepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellMasterDepartmentName.Weight = 0.9111546580663985D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableCompany,
            this.xrPictureBoxLogo,
            this.xrLabelHeaderTitle});
            this.TopMargin.HeightF = 218.75F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTableCompany
            // 
            this.xrTableCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCompany.LocationFloat = new DevExpress.Utils.PointFloat(595.9579F, 0F);
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
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 9.62896F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(450.694F, 125F);
            // 
            // xrLabelHeaderTitle
            // 
            this.xrLabelHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderTitle.ForeColor = System.Drawing.Color.Red;
            this.xrLabelHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 158.5872F);
            this.xrLabelHeaderTitle.Name = "xrLabelHeaderTitle";
            this.xrLabelHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTitle.SizeF = new System.Drawing.SizeF(969.0001F, 50.53378F);
            this.xrLabelHeaderTitle.StylePriority.UseFont = false;
            this.xrLabelHeaderTitle.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTitle.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTitle.Text = "COMPREHENSIVE ANNUAL FINANCIAL REPORT";
            this.xrLabelHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrTable1});
            this.PageHeader.HeightF = 50F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.000222524F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable1.SizeF = new System.Drawing.SizeF(968.9999F, 50F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellHeaderSeq,
            this.xrTableCellHeaderLSCompanyCode,
            this.xrTableCellHeaderDepartment,
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept,
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual,
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany});
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
            // xrTableCellHeaderLSCompanyCode
            // 
            this.xrTableCellHeaderLSCompanyCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderLSCompanyCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderLSCompanyCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderLSCompanyCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderLSCompanyCode.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderLSCompanyCode.Name = "xrTableCellHeaderLSCompanyCode";
            this.xrTableCellHeaderLSCompanyCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderLSCompanyCode.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderLSCompanyCode.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderLSCompanyCode.StylePriority.UseBorders = false;
            this.xrTableCellHeaderLSCompanyCode.StylePriority.UseFont = false;
            this.xrTableCellHeaderLSCompanyCode.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderLSCompanyCode.StylePriority.UsePadding = false;
            this.xrTableCellHeaderLSCompanyCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderLSCompanyCode.Text = "Company Code";
            this.xrTableCellHeaderLSCompanyCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderLSCompanyCode.Weight = 0.2482008121477679D;
            // 
            // xrTableCellHeaderDepartment
            // 
            this.xrTableCellHeaderDepartment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderDepartment.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderDepartment.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderDepartment.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderDepartment.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderDepartment.Name = "xrTableCellHeaderDepartment";
            this.xrTableCellHeaderDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderDepartment.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderDepartment.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderDepartment.StylePriority.UseBorders = false;
            this.xrTableCellHeaderDepartment.StylePriority.UseFont = false;
            this.xrTableCellHeaderDepartment.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderDepartment.StylePriority.UsePadding = false;
            this.xrTableCellHeaderDepartment.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderDepartment.Text = "Department";
            this.xrTableCellHeaderDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellHeaderDepartment.Weight = 0.64143135671622642D;
            // 
            // xrTableCellHeaderTotalGROSSBasicSalaryByDept
            // 
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.Name = "xrTableCellHeaderTotalGROSSBasicSalaryByDept";
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.Text = "TotalGROSSBasicSalaryByDept";
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalGROSSBasicSalaryByDept.Weight = 0.45228143174752838D;
            // 
            // xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual
            // 
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.Name = "xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual";
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.Text = "TotalInsuranceFeeByDeptPaidByIndividual";
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.Weight = 0.26415597157006976D;
            // 
            // xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany
            // 
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.ForeColor = System.Drawing.Color.White;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.Name = "xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany";
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseBorders = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseFont = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseForeColor = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UsePadding = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.Text = "TotalInsuranceFeeByDeptPaidByCompany";
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.Weight = 0.29181573497044655D;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail1.HeightF = 25F;
            this.Detail1.Name = "Detail1";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable2.SizeF = new System.Drawing.SizeF(969F, 25F);
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSeq,
            this.xrTableCellLSCompanyCode,
            this.xrTableCellDepartment,
            this.xrTableCellTotalGROSSBasicSalaryByDept,
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual,
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCellSeq
            // 
            this.xrTableCellSeq.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellSeq.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSeq.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Master.HR_sprptEmployee_CreateCAFR_Master_HR_sprptEmp" +
                    "loyee_CreateCAFR_Details.Seq")});
            this.xrTableCellSeq.Name = "xrTableCellSeq";
            this.xrTableCellSeq.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellSeq.StylePriority.UseBorderColor = false;
            this.xrTableCellSeq.StylePriority.UseBorders = false;
            this.xrTableCellSeq.StylePriority.UsePadding = false;
            this.xrTableCellSeq.StylePriority.UseTextAlignment = false;
            this.xrTableCellSeq.Text = "xrTableCellSeq";
            this.xrTableCellSeq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSeq.Weight = 0.27790940520787838D;
            // 
            // xrTableCellLSCompanyCode
            // 
            this.xrTableCellLSCompanyCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellLSCompanyCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLSCompanyCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Master.HR_sprptEmployee_CreateCAFR_Master_HR_sprptEmp" +
                    "loyee_CreateCAFR_DetaiLSCompanyCode")});
            this.xrTableCellLSCompanyCode.Name = "xrTableCellLSCompanyCode";
            this.xrTableCellLSCompanyCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellLSCompanyCode.StylePriority.UseBorderColor = false;
            this.xrTableCellLSCompanyCode.StylePriority.UseBorders = false;
            this.xrTableCellLSCompanyCode.StylePriority.UsePadding = false;
            this.xrTableCellLSCompanyCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellLSCompanyCode.Text = "xrTableCellLSCompanyCode";
            this.xrTableCellLSCompanyCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellLSCompanyCode.Weight = 0.37272632817906454D;
            // 
            // xrTableCellDepartment
            // 
            this.xrTableCellDepartment.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellDepartment.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDepartment.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Master.HR_sprptEmployee_CreateCAFR_Master_HR_sprptEmp" +
                    "loyee_CreateCAFR_Details.Department")});
            this.xrTableCellDepartment.Name = "xrTableCellDepartment";
            this.xrTableCellDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellDepartment.StylePriority.UseBorderColor = false;
            this.xrTableCellDepartment.StylePriority.UseBorders = false;
            this.xrTableCellDepartment.StylePriority.UsePadding = false;
            this.xrTableCellDepartment.StylePriority.UseTextAlignment = false;
            this.xrTableCellDepartment.Text = "xrTableCellDepartment";
            this.xrTableCellDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellDepartment.Weight = 0.96325855578303321D;
            // 
            // xrTableCellTotalGROSSBasicSalaryByDept
            // 
            this.xrTableCellTotalGROSSBasicSalaryByDept.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalGROSSBasicSalaryByDept.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalGROSSBasicSalaryByDept.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Master.HR_sprptEmployee_CreateCAFR_Master_HR_sprptEmp" +
                    "loyee_CreateCAFR_Details.TotalGROSSBasicSalaryByDept", "{0:#,##0.0}")});
            this.xrTableCellTotalGROSSBasicSalaryByDept.Name = "xrTableCellTotalGROSSBasicSalaryByDept";
            this.xrTableCellTotalGROSSBasicSalaryByDept.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalGROSSBasicSalaryByDept.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalGROSSBasicSalaryByDept.StylePriority.UseBorders = false;
            this.xrTableCellTotalGROSSBasicSalaryByDept.StylePriority.UsePadding = false;
            this.xrTableCellTotalGROSSBasicSalaryByDept.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalGROSSBasicSalaryByDept.Text = "xrTableCellTotalGROSSBasicSalaryByDept";
            this.xrTableCellTotalGROSSBasicSalaryByDept.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalGROSSBasicSalaryByDept.Weight = 0.67920282388504138D;
            // 
            // xrTableCellTotalInsuranceFeeByDeptPaidByIndividual
            // 
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Master.HR_sprptEmployee_CreateCAFR_Master_HR_sprptEmp" +
                    "loyee_CreateCAFR_Details.TotalInsuranceFeeByDeptPaidByCompany", "{0:#,##0.0}")});
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.Name = "xrTableCellTotalInsuranceFeeByDeptPaidByIndividual";
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UsePadding = false;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.Text = "xrTableCellTotalInsuranceFeeByDeptPaidByIndividual";
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.Weight = 0.39668984836505516D;
            // 
            // xrTableCellTotalInsuranceFeeByDeptPaidByCompany
            // 
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Master.HR_sprptEmployee_CreateCAFR_Master_HR_sprptEmp" +
                    "loyee_CreateCAFR_Details.TotalInsuranceFeeByDeptPaidByIndividual", "{0:#,##0.0}")});
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.Name = "xrTableCellTotalInsuranceFeeByDeptPaidByCompany";
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UsePadding = false;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.Text = "xrTableCellTotalInsuranceFeeByDeptPaidByCompany";
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeeByDeptPaidByCompany.Weight = 0.43822777762303355D;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageFooterLeftText,
            this.xrPageFooterRightText});
            this.PageFooter.HeightF = 32.99997F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable3.SizeF = new System.Drawing.SizeF(968.9999F, 25F);
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellTotalSummary,
            this.xrTableCellTotalBasicSalaryGROSS,
            this.xrTableCellTotalInsuranceFeePaidByIndividual,
            this.xrTableCellTotalInsuranceFeePaidByCompany});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.StylePriority.UseBorders = false;
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCellTotalSummary
            // 
            this.xrTableCellTotalSummary.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalSummary.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalSummary.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalSummary.Name = "xrTableCellTotalSummary";
            this.xrTableCellTotalSummary.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalSummary.StylePriority.UseBorders = false;
            this.xrTableCellTotalSummary.StylePriority.UseFont = false;
            this.xrTableCellTotalSummary.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalSummary.Text = "Totals";
            this.xrTableCellTotalSummary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalSummary.Weight = 0.80247836495046909D;
            // 
            // xrTableCellTotalBasicSalaryGROSS
            // 
            this.xrTableCellTotalBasicSalaryGROSS.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalBasicSalaryGROSS.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalBasicSalaryGROSS.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Summary.TotalBasicSalaryGROSS", "{0:#,##0.0}")});
            this.xrTableCellTotalBasicSalaryGROSS.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalBasicSalaryGROSS.Name = "xrTableCellTotalBasicSalaryGROSS";
            this.xrTableCellTotalBasicSalaryGROSS.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalBasicSalaryGROSS.StylePriority.UseBorders = false;
            this.xrTableCellTotalBasicSalaryGROSS.StylePriority.UseFont = false;
            this.xrTableCellTotalBasicSalaryGROSS.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalBasicSalaryGROSS.Text = "xrTableCellTotalBasicSalaryGROSS";
            this.xrTableCellTotalBasicSalaryGROSS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalBasicSalaryGROSS.Weight = 0.33772050052115016D;
            // 
            // xrTableCellTotalInsuranceFeePaidByIndividual
            // 
            this.xrTableCellTotalInsuranceFeePaidByIndividual.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByIndividual.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Summary.TotalInsuranceFeePaidByCompany", "{0:#,##0.0}")});
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Name = "xrTableCellTotalInsuranceFeePaidByIndividual";
            this.xrTableCellTotalInsuranceFeePaidByIndividual.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Text = "xrTableCellTotalInsuranceFeePaidByIndividual";
            this.xrTableCellTotalInsuranceFeePaidByIndividual.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByIndividual.Weight = 0.19724644120683205D;
            // 
            // xrTableCellTotalInsuranceFeePaidByCompany
            // 
            this.xrTableCellTotalInsuranceFeePaidByCompany.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellTotalInsuranceFeePaidByCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsuranceFeePaidByCompany.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HR_sprptEmployee_CreateCAFR_Summary.TotalInsuranceFeePaidByIndividual", "{0:#,##0.0}")});
            this.xrTableCellTotalInsuranceFeePaidByCompany.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalInsuranceFeePaidByCompany.Name = "xrTableCellTotalInsuranceFeePaidByCompany";
            this.xrTableCellTotalInsuranceFeePaidByCompany.StylePriority.UseBorderColor = false;
            this.xrTableCellTotalInsuranceFeePaidByCompany.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsuranceFeePaidByCompany.StylePriority.UseFont = false;
            this.xrTableCellTotalInsuranceFeePaidByCompany.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalInsuranceFeePaidByCompany.Text = "xrTableCellTotalInsuranceFeePaidByCompany";
            this.xrTableCellTotalInsuranceFeePaidByCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalInsuranceFeePaidByCompany.Weight = 0.21790025988498959D;
            // 
            // xrPageFooterLeftText
            // 
            this.xrPageFooterLeftText.LocationFloat = new DevExpress.Utils.PointFloat(0F, 9.999974F);
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
            this.xrPageFooterRightText.LocationFloat = new DevExpress.Utils.PointFloat(695.3463F, 9.999974F);
            this.xrPageFooterRightText.Name = "xrPageFooterRightText";
            this.xrPageFooterRightText.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrPageFooterRightText.SizeF = new System.Drawing.SizeF(273.6536F, 23F);
            this.xrPageFooterRightText.StylePriority.UsePadding = false;
            this.xrPageFooterRightText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterRightText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.ReportFooter.HeightF = 25F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // CAFDetailXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.DetailReport,
            this.PageFooter,
            this.ReportFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 219, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
