﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for QualificationDetailXtraReport
/// </summary>
public class QualificationDetailXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabelHeaderTitle;
    private PageHeaderBand PageHeader;
    private ReportHeaderBand ReportHeader;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBoxLogo;
    private XRPageInfo xrPageFooterRightText;
    private XRPageInfo xrPageFooterLeftText;
    private XRTable xrTableCompany;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCellLabelCompanyName;
    private XRTableCell xrTableCellCompanyName;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCellLabelAddress;
    private XRTableCell xrTableCellAddress;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCellLabelPhone;
    private XRTableCell xrTableCellPhone;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCellLabelFax;
    private XRTableCell xrTableCellFax;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCellLabelEmail;
    private XRTableCell xrTableCellEmail;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCellMasterQualificationName;
    public DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCellEmpCode;
    private XRTableCell xrTableCellFullName;
    private XRTableCell xrTableCellDOB;
    private XRTableCell xrTableCellSeq;
    private XRTable xrTable3;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCellHeaderSeq;
    private XRTableCell xrTableCellHeaderEmpCode;
    private XRTableCell xrTableCellHeaderFullName;
    private XRTableCell xrTableCellHeaderDOB;
    private XRTableCell xrTableCellHeaderGender;
    private XRTableCell xrTableCellHeaderPosition;
    private XRTableCell xrTableCellGender;
    private XRTableCell xrTableCellPosition;
    private XRTableCell xrTableCellHeaderMarital;
    private XRTableCell xrTableCellMarital;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public QualificationDetailXtraReport()
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
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellMasterQualificationName = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabelHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellHeaderSeq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderEmpCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderMarital = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderDOB = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeaderPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTableCompany = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelCompanyName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCompanyName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelPhone = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPhone = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelFax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelEmail = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmail = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageFooterLeftText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageFooterRightText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSeq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMarital = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDOB = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
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
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(1.73591F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(974.2641F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellMasterQualificationName});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCellMasterQualificationName
            // 
            this.xrTableCellMasterQualificationName.BackColor = System.Drawing.Color.SteelBlue;
            this.xrTableCellMasterQualificationName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellMasterQualificationName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMasterQualificationName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCellMasterQualificationName.ForeColor = System.Drawing.Color.White;
            this.xrTableCellMasterQualificationName.Name = "xrTableCellMasterQualificationName";
            this.xrTableCellMasterQualificationName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellMasterQualificationName.StylePriority.UseBackColor = false;
            this.xrTableCellMasterQualificationName.StylePriority.UseBorderColor = false;
            this.xrTableCellMasterQualificationName.StylePriority.UseBorders = false;
            this.xrTableCellMasterQualificationName.StylePriority.UseFont = false;
            this.xrTableCellMasterQualificationName.StylePriority.UseForeColor = false;
            this.xrTableCellMasterQualificationName.StylePriority.UsePadding = false;
            this.xrTableCellMasterQualificationName.StylePriority.UseTextAlignment = false;
            this.xrTableCellMasterQualificationName.Text = "QualificationName";
            this.xrTableCellMasterQualificationName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellMasterQualificationName.Weight = 1D;
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
            this.xrLabelHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(0.7500966F, 149.7084F);
            this.xrLabelHeaderTitle.Name = "xrLabelHeaderTitle";
            this.xrLabelHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTitle.SizeF = new System.Drawing.SizeF(975.2499F, 50.87498F);
            this.xrLabelHeaderTitle.StylePriority.UseFont = false;
            this.xrLabelHeaderTitle.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTitle.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTitle.Text = "EMPLOYEE DETAILS STATISTIC BY QUALIFICATION";
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
            this.xrTable3});
            this.PageHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.PageHeader.ForeColor = System.Drawing.Color.White;
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.PageHeader.StylePriority.UseFont = false;
            this.PageHeader.StylePriority.UseForeColor = false;
            this.PageHeader.StylePriority.UsePadding = false;
            this.PageHeader.StylePriority.UseTextAlignment = false;
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable3.SizeF = new System.Drawing.SizeF(975.2499F, 25F);
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellHeaderSeq,
            this.xrTableCellHeaderEmpCode,
            this.xrTableCellHeaderFullName,
            this.xrTableCellHeaderGender,
            this.xrTableCellHeaderMarital,
            this.xrTableCellHeaderDOB,
            this.xrTableCellHeaderPosition});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCellHeaderSeq
            // 
            this.xrTableCellHeaderSeq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderSeq.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderSeq.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderSeq.Name = "xrTableCellHeaderSeq";
            this.xrTableCellHeaderSeq.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderSeq.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderSeq.StylePriority.UseBorders = false;
            this.xrTableCellHeaderSeq.Text = "Seq";
            this.xrTableCellHeaderSeq.Weight = 0.76083297729492183D;
            // 
            // xrTableCellHeaderEmpCode
            // 
            this.xrTableCellHeaderEmpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderEmpCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderEmpCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderEmpCode.Name = "xrTableCellHeaderEmpCode";
            this.xrTableCellHeaderEmpCode.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderEmpCode.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderEmpCode.StylePriority.UseBorders = false;
            this.xrTableCellHeaderEmpCode.Text = "EmpCode";
            this.xrTableCellHeaderEmpCode.Weight = 1.2322222900390627D;
            // 
            // xrTableCellHeaderFullName
            // 
            this.xrTableCellHeaderFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderFullName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderFullName.Name = "xrTableCellHeaderFullName";
            this.xrTableCellHeaderFullName.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderFullName.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderFullName.StylePriority.UseBorders = false;
            this.xrTableCellHeaderFullName.Text = "FullName";
            this.xrTableCellHeaderFullName.Weight = 3.0095812988281239D;
            // 
            // xrTableCellHeaderGender
            // 
            this.xrTableCellHeaderGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderGender.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderGender.Name = "xrTableCellHeaderGender";
            this.xrTableCellHeaderGender.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderGender.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderGender.StylePriority.UseBorders = false;
            this.xrTableCellHeaderGender.Text = "Gender";
            this.xrTableCellHeaderGender.Weight = 0.96873277664184565D;
            // 
            // xrTableCellHeaderMarital
            // 
            this.xrTableCellHeaderMarital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderMarital.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderMarital.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderMarital.Name = "xrTableCellHeaderMarital";
            this.xrTableCellHeaderMarital.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderMarital.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderMarital.StylePriority.UseBorders = false;
            this.xrTableCellHeaderMarital.Text = "Marital";
            this.xrTableCellHeaderMarital.Weight = 0.96873277664184565D;
            // 
            // xrTableCellHeaderDOB
            // 
            this.xrTableCellHeaderDOB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderDOB.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderDOB.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeaderDOB.Name = "xrTableCellHeaderDOB";
            this.xrTableCellHeaderDOB.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderDOB.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderDOB.StylePriority.UseBorders = false;
            this.xrTableCellHeaderDOB.Text = "DOB";
            this.xrTableCellHeaderDOB.Weight = 0.9374655532836913D;
            // 
            // xrTableCellHeaderPosition
            // 
            this.xrTableCellHeaderPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrTableCellHeaderPosition.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellHeaderPosition.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCellHeaderPosition.Name = "xrTableCellHeaderPosition";
            this.xrTableCellHeaderPosition.StylePriority.UseBackColor = false;
            this.xrTableCellHeaderPosition.StylePriority.UseBorderColor = false;
            this.xrTableCellHeaderPosition.StylePriority.UseBorders = false;
            this.xrTableCellHeaderPosition.Text = "Position";
            this.xrTableCellHeaderPosition.Weight = 1.8749311065673826D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableCompany,
            this.xrPictureBoxLogo,
            this.xrLabelHeaderTitle});
            this.ReportHeader.HeightF = 210.5834F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTableCompany
            // 
            this.xrTableCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCompany.LocationFloat = new DevExpress.Utils.PointFloat(597.137F, 0F);
            this.xrTableCompany.Name = "xrTableCompany";
            this.xrTableCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCompany.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow7});
            this.xrTableCompany.SizeF = new System.Drawing.SizeF(378.8213F, 125F);
            this.xrTableCompany.StylePriority.UseBorders = false;
            this.xrTableCompany.StylePriority.UsePadding = false;
            this.xrTableCompany.StylePriority.UseTextAlignment = false;
            this.xrTableCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelCompanyName,
            this.xrTableCellCompanyName});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
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
            this.xrTableCellLabelCompanyName.Weight = 0.47634262326167737D;
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
            this.xrTableCellCompanyName.Weight = 1.5236573767383226D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelAddress,
            this.xrTableCellAddress});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
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
            this.xrTableCellLabelAddress.Weight = 0.47634262326167737D;
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
            this.xrTableCellAddress.Weight = 1.5236573767383226D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelPhone,
            this.xrTableCellPhone});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
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
            this.xrTableCellLabelPhone.Weight = 0.47634671836955544D;
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
            this.xrTableCellPhone.Weight = 1.5236532816304447D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelFax,
            this.xrTableCellFax});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
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
            this.xrTableCellLabelFax.Weight = 0.47634671836955544D;
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
            this.xrTableCellFax.Weight = 1.5236532816304447D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelEmail,
            this.xrTableCellEmail});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
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
            this.xrTableCellLabelEmail.Weight = 0.47634671836955544D;
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
            this.xrTableCellEmail.Weight = 1.5236532816304447D;
            // 
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0.3475189F, 0F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(451.0417F, 125F);
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
            this.xrPageFooterLeftText.SizeF = new System.Drawing.SizeF(301.7358F, 23F);
            this.xrPageFooterLeftText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterLeftText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageFooterRightText
            // 
            this.xrPageFooterRightText.Format = "Page {0} of {1}";
            this.xrPageFooterRightText.LocationFloat = new DevExpress.Utils.PointFloat(733.5141F, 0F);
            this.xrPageFooterRightText.Name = "xrPageFooterRightText";
            this.xrPageFooterRightText.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrPageFooterRightText.SizeF = new System.Drawing.SizeF(241.736F, 23F);
            this.xrPageFooterRightText.StylePriority.UsePadding = false;
            this.xrPageFooterRightText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterRightText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            this.xrTable1});
            this.Detail1.HeightF = 25F;
            this.Detail1.Name = "Detail1";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(975.2499F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSeq,
            this.xrTableCellEmpCode,
            this.xrTableCellFullName,
            this.xrTableCellGender,
            this.xrTableCellMarital,
            this.xrTableCellDOB,
            this.xrTableCellPosition});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCellSeq
            // 
            this.xrTableCellSeq.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellSeq.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSeq.ForeColor = System.Drawing.Color.Black;
            this.xrTableCellSeq.Name = "xrTableCellSeq";
            this.xrTableCellSeq.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellSeq.StylePriority.UseBorderColor = false;
            this.xrTableCellSeq.StylePriority.UseBorders = false;
            this.xrTableCellSeq.StylePriority.UseForeColor = false;
            this.xrTableCellSeq.StylePriority.UsePadding = false;
            this.xrTableCellSeq.StylePriority.UseTextAlignment = false;
            this.xrTableCellSeq.Text = "Seq";
            this.xrTableCellSeq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSeq.Weight = 0.23404253554809359D;
            // 
            // xrTableCellEmpCode
            // 
            this.xrTableCellEmpCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellEmpCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellEmpCode.ForeColor = System.Drawing.Color.Black;
            this.xrTableCellEmpCode.Name = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellEmpCode.StylePriority.UseBorderColor = false;
            this.xrTableCellEmpCode.StylePriority.UseBorders = false;
            this.xrTableCellEmpCode.StylePriority.UseForeColor = false;
            this.xrTableCellEmpCode.StylePriority.UsePadding = false;
            this.xrTableCellEmpCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellEmpCode.Text = "EmpCode";
            this.xrTableCellEmpCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellEmpCode.Weight = 0.37904802699178036D;
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFullName.ForeColor = System.Drawing.Color.Black;
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellFullName.StylePriority.UseBorderColor = false;
            this.xrTableCellFullName.StylePriority.UseBorders = false;
            this.xrTableCellFullName.StylePriority.UseForeColor = false;
            this.xrTableCellFullName.StylePriority.UsePadding = false;
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.Text = "FullName";
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellFullName.Weight = 0.92578775728175611D;
            // 
            // xrTableCellGender
            // 
            this.xrTableCellGender.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellGender.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGender.Name = "xrTableCellGender";
            this.xrTableCellGender.StylePriority.UseBorderColor = false;
            this.xrTableCellGender.StylePriority.UseBorders = false;
            this.xrTableCellGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellGender.Text = "Gender";
            this.xrTableCellGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGender.Weight = 0.29799526163103979D;
            // 
            // xrTableCellMarital
            // 
            this.xrTableCellMarital.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellMarital.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMarital.Name = "xrTableCellMarital";
            this.xrTableCellMarital.StylePriority.UseBorderColor = false;
            this.xrTableCellMarital.StylePriority.UseBorders = false;
            this.xrTableCellMarital.StylePriority.UseTextAlignment = false;
            this.xrTableCellMarital.Text = "Marital";
            this.xrTableCellMarital.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMarital.Weight = 0.29799526163103979D;
            // 
            // xrTableCellDOB
            // 
            this.xrTableCellDOB.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellDOB.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDOB.ForeColor = System.Drawing.Color.Black;
            this.xrTableCellDOB.Name = "xrTableCellDOB";
            this.xrTableCellDOB.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellDOB.StylePriority.UseBorderColor = false;
            this.xrTableCellDOB.StylePriority.UseBorders = false;
            this.xrTableCellDOB.StylePriority.UseForeColor = false;
            this.xrTableCellDOB.StylePriority.UsePadding = false;
            this.xrTableCellDOB.StylePriority.UseTextAlignment = false;
            this.xrTableCellDOB.Text = "DOB";
            this.xrTableCellDOB.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDOB.Weight = 0.28837705230543004D;
            // 
            // xrTableCellPosition
            // 
            this.xrTableCellPosition.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPosition.Name = "xrTableCellPosition";
            this.xrTableCellPosition.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellPosition.StylePriority.UseBorderColor = false;
            this.xrTableCellPosition.StylePriority.UseBorders = false;
            this.xrTableCellPosition.StylePriority.UsePadding = false;
            this.xrTableCellPosition.StylePriority.UseTextAlignment = false;
            this.xrTableCellPosition.Text = "Position";
            this.xrTableCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellPosition.Weight = 0.57675410461086007D;
            // 
            // QualificationDetailXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.ReportHeader,
            this.PageFooter,
            this.DetailReport});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 93, 0, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
