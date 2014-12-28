using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for OTByYearXtraReport
/// </summary>
public class OTByYearXtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    public DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRLabel xrLabelHeaderTitle;
    private XRLabel xrLabelOTJanuary;
    private XRLabel xrLabelHeaderNo;
    private XRLabel xrLabelHeaderOTTotalDayAndNight;
    private XRLabel xrLabelHeaderOTTotalNight;
    private XRLabel xrLabelHeaderOTTotalDay;
    private XRLabel xrLabelHeaderNH1;
    private XRLabel xrLabelHeaderNW1;
    private XRLabel xrLabelHeaderNND1;
    private XRLabel xrLabelHeaderH1;
    private XRLabel xrLabelHeaderW1;
    private XRLabel xrLabelHeaderND1;
    private XRLabel xrLabelHeaderPosition;
    private XRLabel xrLabelHeaderOTRestTotal;
    private XRLabel xrLabelHeaderFullName;
    private XRLabel xrLabelHeaderEmpCode;
    private XRLabel xrLabelDepartmentName;
    private XRTable xrTableDetails;
    private XRTableRow xrTableRowDetails;
    private XRTableCell xrTableCellNo;
    private XRTableCell xrTableCellEmpCode;
    private XRTableCell xrTableCellFullName;
    private XRTableCell xrTableCellPosition;
    private XRTableCell xrLabelND1;
    private XRTableCell xrTableCellOTWD;
    private XRTableCell xrTableCellOTHDD;
    private XRTableCell xrTableCellOTNDN;
    private XRTableCell xrTableCellOTWN;
    private XRTableCell xrTableCellOTHN;
    private XRTableCell xrTableCellOTTotalByMonth;
    private XRTableCell xrTableCellOTTotalsFromFirstMonthToSelectedMonth;
    private XRTableCell xrTableCellOTTotalByYear;
    private XRTableCell xrTableCellOTRestTotal;
    private XRPageInfo xrPageFooterLeftText;
    private XRPageInfo xrPageFooterRightText;
    private XRLabel xrLabelHeaderTotals;
    private XRLabel xrLabelOTFebuary;
    private XRLabel xrLabelHeaderNH2;
    private XRLabel xrLabelHeaderNW2;
    private XRLabel xrLabelHeaderNND2;
    private XRLabel xrLabelHeaderH2;
    private XRLabel xrLabelHeaderW2;
    private XRLabel xrLabelHeaderND2;
    private XRLabel xrLabelOTMarch;
    private XRLabel xrLabelHeaderNH3;
    private XRLabel xrLabelHeaderNW3;
    private XRLabel xrLabelHeaderNND3;
    private XRLabel xrLabelHeaderH3;
    private XRLabel xrLabelHeaderW3;
    private XRLabel xrLabelHeaderND3;
    private XRLabel xrLabelOTApril;
    private XRLabel xrLabelHeaderNH4;
    private XRLabel xrLabelHeaderNW4;
    private XRLabel xrLabelHeaderNND4;
    private XRLabel xrLabelHeaderH4;
    private XRLabel xrLabelHeaderW4;
    private XRLabel xrLabelHeaderND4;
    private XRLabel xrLabelHeaderND5;
    private XRLabel xrLabelHeaderNH5;
    private XRLabel xrLabelHeaderNW5;
    private XRLabel xrLabelHeaderNND5;
    private XRLabel xrLabelHeaderH5;
    private XRLabel xrLabelHeaderW5;
    private XRLabel xrLabelOTMay;
    private XRLabel xrLabelHeaderND6;
    private XRLabel xrLabelHeaderNH6;
    private XRLabel xrLabelHeaderNW6;
    private XRLabel xrLabelHeaderNND6;
    private XRLabel xrLabelHeaderH6;
    private XRLabel xrLabelHeaderW6;
    private XRLabel xrLabelOTJune;
    private XRLabel xrLabelHeaderND7;
    private XRLabel xrLabelHeaderNH7;
    private XRLabel xrLabelHeaderNW7;
    private XRLabel xrLabelHeaderNND7;
    private XRLabel xrLabelHeaderH7;
    private XRLabel xrLabelHeaderW7;
    private XRLabel xrLabelOTJuly;
    private XRLabel xrLabelHeaderND8;
    private XRLabel xrLabelHeaderNH8;
    private XRLabel xrLabelHeaderNW8;
    private XRLabel xrLabelHeaderNND8;
    private XRLabel xrLabelHeaderH8;
    private XRLabel xrLabelHeaderW8;
    private XRLabel xrLabelOTAugust;
    private XRLabel xrLabelHeaderND9;
    private XRLabel xrLabelHeaderNH9;
    private XRLabel xrLabelHeaderNW9;
    private XRLabel xrLabelHeaderNND9;
    private XRLabel xrLabelHeaderH9;
    private XRLabel xrLabelHeaderW9;
    private XRLabel xrLabelOTSeptember;
    private XRLabel xrLabelHeaderND10;
    private XRLabel xrLabelHeaderNH10;
    private XRLabel xrLabelHeaderNW10;
    private XRLabel xrLabelHeaderNND10;
    private XRLabel xrLabelHeaderH10;
    private XRLabel xrLabelHeaderW10;
    private XRLabel xrLabelHeaderND11;
    private XRLabel xrLabelHeaderNH11;
    private XRLabel xrLabelHeaderNW11;
    private XRLabel xrLabelHeaderNND11;
    private XRLabel xrLabelHeaderH11;
    private XRLabel xrLabelHeaderW11;
    private XRLabel xrLabelOTNovember;
    private XRLabel xrLabelHeaderND12;
    private XRLabel xrLabelHeaderNH12;
    private XRLabel xrLabelHeaderNW12;
    private XRLabel xrLabelHeaderNND12;
    private XRLabel xrLabelHeaderH12;
    private XRLabel xrLabelHeaderW12;
    private XRLabel xrLabelOTDecember;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrLabelH3;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell12;
    private XRTableCell xrLabelH4;
    private XRTableCell xrLabelNH4;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrLabelH5;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrLabelH6;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell25;
    private XRTableCell xrLabelH7;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrLabelH8;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell44;
    private XRTableCell xrLabelH9;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrLabelH10;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell66;
    private Eagle.WebApp.Areas.Admin.Reports.TS.OTByYear.YearlyOTDataSet yearlyOTDataSet1;
    private Eagle.WebApp.Areas.Admin.Reports.TS.OTByYear.YearlyOTDataSetTableAdapters.Timesheet_sprptOTByYear_MasterTableAdapter timesheet_sprptOTByYear_MasterTableAdapter;
    private Eagle.WebApp.Areas.Admin.Reports.TS.OTByYear.YearlyOTDataSetTableAdapters.Timesheet_sprptOTByYear_DetailsTableAdapter timesheet_sprptOTByYear_DetailsTableAdapter;
    private XRLabel xrLabelOTOctober;
    private XRLabel xrLabelHeaderHourTotalsPerYear;
    private XRTableCell xrTableCell5;
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
    private XRControlStyle xrControlStyle1;
    private XRControlStyle xrControlStyle2;
    private XRPictureBox xrPictureBoxLogo;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OTByYearXtraReport()
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
            this.xrLabelDepartmentName = new DevExpress.XtraReports.UI.XRLabel();
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
            this.xrLabelHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabelHeaderHourTotalsPerYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTDecember = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTNovember = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTOctober = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTSeptember = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTAugust = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTJuly = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTJune = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTMay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTApril = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTMarch = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTFebuary = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderTotals = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelOTJanuary = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTTotalDayAndNight = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTTotalNight = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTTotalDay = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNH1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNW1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderNND1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderH1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderW1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderND1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderPosition = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderOTRestTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderFullName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelHeaderEmpCode = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageFooterLeftText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageFooterRightText = new DevExpress.XtraReports.UI.XRPageInfo();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTableDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRowDetails = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelND1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTWD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTHDD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTNDN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTWN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTHN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTTotalByMonth = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTTotalByYear = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOTRestTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelH3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelH4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelNH4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelH5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelH6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelH7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelH8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelH9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelH10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.timesheet_sprptOTByYear_DetailsTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.OTByYear.YearlyOTDataSetTableAdapters.Timesheet_sprptOTByYear_DetailsTableAdapter();
            this.yearlyOTDataSet1 = new Eagle.WebApp.Areas.Admin.Reports.TS.OTByYear.YearlyOTDataSet();
            this.timesheet_sprptOTByYear_MasterTableAdapter = new Eagle.WebApp.Areas.Admin.Reports.TS.OTByYear.YearlyOTDataSetTableAdapters.Timesheet_sprptOTByYear_MasterTableAdapter();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearlyOTDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelDepartmentName});
            this.Detail.HeightF = 26.82928F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabelDepartmentName
            // 
            this.xrLabelDepartmentName.BackColor = System.Drawing.Color.SteelBlue;
            this.xrLabelDepartmentName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelDepartmentName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelDepartmentName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.DepartmentName")});
            this.xrLabelDepartmentName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelDepartmentName.ForeColor = System.Drawing.Color.White;
            this.xrLabelDepartmentName.LocationFloat = new DevExpress.Utils.PointFloat(1.913166F, 0F);
            this.xrLabelDepartmentName.Name = "xrLabelDepartmentName";
            this.xrLabelDepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelDepartmentName.SizeF = new System.Drawing.SizeF(4594.087F, 26.04167F);
            this.xrLabelDepartmentName.StylePriority.UseBackColor = false;
            this.xrLabelDepartmentName.StylePriority.UseBorderColor = false;
            this.xrLabelDepartmentName.StylePriority.UseBorders = false;
            this.xrLabelDepartmentName.StylePriority.UseFont = false;
            this.xrLabelDepartmentName.StylePriority.UseForeColor = false;
            this.xrLabelDepartmentName.StylePriority.UseTextAlignment = false;
            this.xrLabelDepartmentName.Text = "xrLabelDepartmentName";
            this.xrLabelDepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBoxLogo,
            this.xrTableCompany,
            this.xrLabelHeaderTitle});
            this.TopMargin.HeightF = 207.5344F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTableCompany
            // 
            this.xrTableCompany.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCompany.LocationFloat = new DevExpress.Utils.PointFloat(4113.09F, 0F);
            this.xrTableCompany.Name = "xrTableCompany";
            this.xrTableCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCompany.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5});
            this.xrTableCompany.SizeF = new System.Drawing.SizeF(472.9104F, 125F);
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
            this.xrTableCellLabelCompanyName.EvenStyleName = "xrControlStyle1";
            this.xrTableCellLabelCompanyName.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelCompanyName.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelCompanyName.Name = "xrTableCellLabelCompanyName";
            this.xrTableCellLabelCompanyName.OddStyleName = "xrControlStyle2";
            this.xrTableCellLabelCompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelCompanyName.StylePriority.UseBackColor = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseBorders = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseFont = false;
            this.xrTableCellLabelCompanyName.StylePriority.UseForeColor = false;
            this.xrTableCellLabelCompanyName.Text = "Headquarter :";
            this.xrTableCellLabelCompanyName.Weight = 0.40158846975429952D;
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
            this.xrTableCellCompanyName.EvenStyleName = "xrControlStyle1";
            this.xrTableCellCompanyName.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellCompanyName.Name = "xrTableCellCompanyName";
            this.xrTableCellCompanyName.OddStyleName = "xrControlStyle2";
            this.xrTableCellCompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellCompanyName.StylePriority.UseBackColor = false;
            this.xrTableCellCompanyName.StylePriority.UseBorderColor = false;
            this.xrTableCellCompanyName.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellCompanyName.StylePriority.UseBorders = false;
            this.xrTableCellCompanyName.StylePriority.UseBorderWidth = false;
            this.xrTableCellCompanyName.StylePriority.UseFont = false;
            this.xrTableCellCompanyName.Text = "Unit Corp";
            this.xrTableCellCompanyName.Weight = 1.5984115302457005D;
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
            this.xrTableCellLabelAddress.EvenStyleName = "xrControlStyle1";
            this.xrTableCellLabelAddress.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelAddress.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelAddress.Name = "xrTableCellLabelAddress";
            this.xrTableCellLabelAddress.OddStyleName = "xrControlStyle2";
            this.xrTableCellLabelAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelAddress.StylePriority.UseBackColor = false;
            this.xrTableCellLabelAddress.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelAddress.StylePriority.UseBorders = false;
            this.xrTableCellLabelAddress.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelAddress.StylePriority.UseFont = false;
            this.xrTableCellLabelAddress.StylePriority.UseForeColor = false;
            this.xrTableCellLabelAddress.Text = "Address :";
            this.xrTableCellLabelAddress.Weight = 0.40158846975429952D;
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
            this.xrTableCellAddress.EvenStyleName = "xrControlStyle1";
            this.xrTableCellAddress.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellAddress.Name = "xrTableCellAddress";
            this.xrTableCellAddress.OddStyleName = "xrControlStyle2";
            this.xrTableCellAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellAddress.StylePriority.UseBackColor = false;
            this.xrTableCellAddress.StylePriority.UseBorderColor = false;
            this.xrTableCellAddress.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellAddress.StylePriority.UseBorders = false;
            this.xrTableCellAddress.StylePriority.UseBorderWidth = false;
            this.xrTableCellAddress.StylePriority.UseFont = false;
            this.xrTableCellAddress.Text = "157 Nguyen Thi Thap, Tan Phu Ward, District 7, HCM";
            this.xrTableCellAddress.Weight = 1.5984115302457005D;
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
            this.xrTableCellLabelPhone.EvenStyleName = "xrControlStyle1";
            this.xrTableCellLabelPhone.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelPhone.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelPhone.Name = "xrTableCellLabelPhone";
            this.xrTableCellLabelPhone.OddStyleName = "xrControlStyle2";
            this.xrTableCellLabelPhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelPhone.StylePriority.UseBackColor = false;
            this.xrTableCellLabelPhone.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelPhone.StylePriority.UseBorders = false;
            this.xrTableCellLabelPhone.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelPhone.StylePriority.UseFont = false;
            this.xrTableCellLabelPhone.StylePriority.UseForeColor = false;
            this.xrTableCellLabelPhone.Text = "Phone :";
            this.xrTableCellLabelPhone.Weight = 0.40159227285262478D;
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
            this.xrTableCellPhone.EvenStyleName = "xrControlStyle1";
            this.xrTableCellPhone.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellPhone.Name = "xrTableCellPhone";
            this.xrTableCellPhone.OddStyleName = "xrControlStyle2";
            this.xrTableCellPhone.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellPhone.StylePriority.UseBackColor = false;
            this.xrTableCellPhone.StylePriority.UseBorderColor = false;
            this.xrTableCellPhone.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellPhone.StylePriority.UseBorders = false;
            this.xrTableCellPhone.StylePriority.UseBorderWidth = false;
            this.xrTableCellPhone.StylePriority.UseFont = false;
            this.xrTableCellPhone.Text = "(84-8) 37 402 388 - 37 402 908";
            this.xrTableCellPhone.Weight = 1.5984077271473753D;
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
            this.xrTableCellLabelFax.EvenStyleName = "xrControlStyle1";
            this.xrTableCellLabelFax.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelFax.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelFax.Name = "xrTableCellLabelFax";
            this.xrTableCellLabelFax.OddStyleName = "xrControlStyle2";
            this.xrTableCellLabelFax.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelFax.StylePriority.UseBackColor = false;
            this.xrTableCellLabelFax.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelFax.StylePriority.UseBorders = false;
            this.xrTableCellLabelFax.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelFax.StylePriority.UseFont = false;
            this.xrTableCellLabelFax.StylePriority.UseForeColor = false;
            this.xrTableCellLabelFax.Text = "Fax :";
            this.xrTableCellLabelFax.Weight = 0.40159227285262478D;
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
            this.xrTableCellFax.EvenStyleName = "xrControlStyle1";
            this.xrTableCellFax.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellFax.Name = "xrTableCellFax";
            this.xrTableCellFax.OddStyleName = "xrControlStyle2";
            this.xrTableCellFax.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellFax.StylePriority.UseBackColor = false;
            this.xrTableCellFax.StylePriority.UseBorderColor = false;
            this.xrTableCellFax.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellFax.StylePriority.UseBorders = false;
            this.xrTableCellFax.StylePriority.UseBorderWidth = false;
            this.xrTableCellFax.StylePriority.UseFont = false;
            this.xrTableCellFax.Text = "(84-8) 37 402 385";
            this.xrTableCellFax.Weight = 1.5984077271473753D;
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
            this.xrTableCellLabelEmail.EvenStyleName = "xrControlStyle1";
            this.xrTableCellLabelEmail.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelEmail.ForeColor = System.Drawing.Color.White;
            this.xrTableCellLabelEmail.Name = "xrTableCellLabelEmail";
            this.xrTableCellLabelEmail.OddStyleName = "xrControlStyle2";
            this.xrTableCellLabelEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellLabelEmail.StylePriority.UseBackColor = false;
            this.xrTableCellLabelEmail.StylePriority.UseBorderColor = false;
            this.xrTableCellLabelEmail.StylePriority.UseBorders = false;
            this.xrTableCellLabelEmail.StylePriority.UseBorderWidth = false;
            this.xrTableCellLabelEmail.StylePriority.UseFont = false;
            this.xrTableCellLabelEmail.StylePriority.UseForeColor = false;
            this.xrTableCellLabelEmail.Text = "Email :";
            this.xrTableCellLabelEmail.Weight = 0.40159227285262478D;
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
            this.xrTableCellEmail.EvenStyleName = "xrControlStyle1";
            this.xrTableCellEmail.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.xrTableCellEmail.Name = "xrTableCellEmail";
            this.xrTableCellEmail.OddStyleName = "xrControlStyle2";
            this.xrTableCellEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 2, 2, 100F);
            this.xrTableCellEmail.StylePriority.UseBackColor = false;
            this.xrTableCellEmail.StylePriority.UseBorderColor = false;
            this.xrTableCellEmail.StylePriority.UseBorderDashStyle = false;
            this.xrTableCellEmail.StylePriority.UseBorders = false;
            this.xrTableCellEmail.StylePriority.UseBorderWidth = false;
            this.xrTableCellEmail.StylePriority.UseFont = false;
            this.xrTableCellEmail.Text = "info@unit.com.vn";
            this.xrTableCellEmail.Weight = 1.5984077271473753D;
            // 
            // xrLabelHeaderTitle
            // 
            this.xrLabelHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabelHeaderTitle.ForeColor = System.Drawing.Color.Red;
            this.xrLabelHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(1.913198F, 146.625F);
            this.xrLabelHeaderTitle.Name = "xrLabelHeaderTitle";
            this.xrLabelHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTitle.SizeF = new System.Drawing.SizeF(4584.086F, 49.32294F);
            this.xrLabelHeaderTitle.StylePriority.UseFont = false;
            this.xrLabelHeaderTitle.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTitle.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTitle.Text = "Yearly OverTime Report";
            this.xrLabelHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 25F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelHeaderHourTotalsPerYear,
            this.xrLabelHeaderND12,
            this.xrLabelHeaderNH12,
            this.xrLabelHeaderNW12,
            this.xrLabelHeaderNND12,
            this.xrLabelHeaderH12,
            this.xrLabelHeaderW12,
            this.xrLabelOTDecember,
            this.xrLabelHeaderND11,
            this.xrLabelHeaderNH11,
            this.xrLabelHeaderNW11,
            this.xrLabelHeaderNND11,
            this.xrLabelHeaderH11,
            this.xrLabelHeaderW11,
            this.xrLabelOTNovember,
            this.xrLabelHeaderND10,
            this.xrLabelHeaderNH10,
            this.xrLabelHeaderNW10,
            this.xrLabelHeaderNND10,
            this.xrLabelHeaderH10,
            this.xrLabelHeaderW10,
            this.xrLabelOTOctober,
            this.xrLabelHeaderND9,
            this.xrLabelHeaderNH9,
            this.xrLabelHeaderNW9,
            this.xrLabelHeaderNND9,
            this.xrLabelHeaderH9,
            this.xrLabelHeaderW9,
            this.xrLabelOTSeptember,
            this.xrLabelHeaderND8,
            this.xrLabelHeaderNH8,
            this.xrLabelHeaderNW8,
            this.xrLabelHeaderNND8,
            this.xrLabelHeaderH8,
            this.xrLabelHeaderW8,
            this.xrLabelOTAugust,
            this.xrLabelHeaderND7,
            this.xrLabelHeaderNH7,
            this.xrLabelHeaderNW7,
            this.xrLabelHeaderNND7,
            this.xrLabelHeaderH7,
            this.xrLabelHeaderW7,
            this.xrLabelOTJuly,
            this.xrLabelHeaderND6,
            this.xrLabelHeaderNH6,
            this.xrLabelHeaderNW6,
            this.xrLabelHeaderNND6,
            this.xrLabelHeaderH6,
            this.xrLabelHeaderW6,
            this.xrLabelOTJune,
            this.xrLabelHeaderND5,
            this.xrLabelHeaderNH5,
            this.xrLabelHeaderNW5,
            this.xrLabelHeaderNND5,
            this.xrLabelHeaderH5,
            this.xrLabelHeaderW5,
            this.xrLabelOTMay,
            this.xrLabelOTApril,
            this.xrLabelHeaderNH4,
            this.xrLabelHeaderNW4,
            this.xrLabelHeaderNND4,
            this.xrLabelHeaderH4,
            this.xrLabelHeaderW4,
            this.xrLabelHeaderND4,
            this.xrLabelOTMarch,
            this.xrLabelHeaderNH3,
            this.xrLabelHeaderNW3,
            this.xrLabelHeaderNND3,
            this.xrLabelHeaderH3,
            this.xrLabelHeaderW3,
            this.xrLabelHeaderND3,
            this.xrLabelOTFebuary,
            this.xrLabelHeaderNH2,
            this.xrLabelHeaderNW2,
            this.xrLabelHeaderNND2,
            this.xrLabelHeaderH2,
            this.xrLabelHeaderW2,
            this.xrLabelHeaderND2,
            this.xrLabelHeaderTotals,
            this.xrLabelOTJanuary,
            this.xrLabelHeaderNo,
            this.xrLabelHeaderOTTotalDayAndNight,
            this.xrLabelHeaderOTTotalNight,
            this.xrLabelHeaderOTTotalDay,
            this.xrLabelHeaderNH1,
            this.xrLabelHeaderNW1,
            this.xrLabelHeaderNND1,
            this.xrLabelHeaderH1,
            this.xrLabelHeaderW1,
            this.xrLabelHeaderND1,
            this.xrLabelHeaderPosition,
            this.xrLabelHeaderOTRestTotal,
            this.xrLabelHeaderFullName,
            this.xrLabelHeaderEmpCode});
            this.PageHeader.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.PageHeader.HeightF = 88.26561F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseFont = false;
            // 
            // xrLabelHeaderHourTotalsPerYear
            // 
            this.xrLabelHeaderHourTotalsPerYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderHourTotalsPerYear.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderHourTotalsPerYear.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderHourTotalsPerYear.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderHourTotalsPerYear.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderHourTotalsPerYear.LocationFloat = new DevExpress.Utils.PointFloat(4396.546F, 0F);
            this.xrLabelHeaderHourTotalsPerYear.Name = "xrLabelHeaderHourTotalsPerYear";
            this.xrLabelHeaderHourTotalsPerYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderHourTotalsPerYear.SizeF = new System.Drawing.SizeF(100.0313F, 88.22772F);
            this.xrLabelHeaderHourTotalsPerYear.StylePriority.UseBackColor = false;
            this.xrLabelHeaderHourTotalsPerYear.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderHourTotalsPerYear.StylePriority.UseBorders = false;
            this.xrLabelHeaderHourTotalsPerYear.StylePriority.UseFont = false;
            this.xrLabelHeaderHourTotalsPerYear.StylePriority.UseForeColor = false;
            this.xrLabelHeaderHourTotalsPerYear.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderHourTotalsPerYear.Text = "Hour Totals Per Year";
            this.xrLabelHeaderHourTotalsPerYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND12
            // 
            this.xrLabelHeaderND12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND12.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND12.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND12.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND12.LocationFloat = new DevExpress.Utils.PointFloat(3876.758F, 34.87778F);
            this.xrLabelHeaderND12.Multiline = true;
            this.xrLabelHeaderND12.Name = "xrLabelHeaderND12";
            this.xrLabelHeaderND12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND12.SizeF = new System.Drawing.SizeF(43.97363F, 53.38751F);
            this.xrLabelHeaderND12.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND12.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND12.StylePriority.UseBorders = false;
            this.xrLabelHeaderND12.StylePriority.UseFont = false;
            this.xrLabelHeaderND12.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND12.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND12.Text = "Normal (day)";
            this.xrLabelHeaderND12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH12
            // 
            this.xrLabelHeaderNH12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH12.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH12.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH12.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH12.LocationFloat = new DevExpress.Utils.PointFloat(4134.944F, 34.84833F);
            this.xrLabelHeaderNH12.Multiline = true;
            this.xrLabelHeaderNH12.Name = "xrLabelHeaderNH12";
            this.xrLabelHeaderNH12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH12.SizeF = new System.Drawing.SizeF(54.90967F, 53.38715F);
            this.xrLabelHeaderNH12.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH12.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH12.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH12.StylePriority.UseFont = false;
            this.xrLabelHeaderNH12.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH12.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH12.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW12
            // 
            this.xrLabelHeaderNW12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW12.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW12.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW12.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW12.LocationFloat = new DevExpress.Utils.PointFloat(4076.215F, 34.87811F);
            this.xrLabelHeaderNW12.Multiline = true;
            this.xrLabelHeaderNW12.Name = "xrLabelHeaderNW12";
            this.xrLabelHeaderNW12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW12.SizeF = new System.Drawing.SizeF(58.01831F, 53.3875F);
            this.xrLabelHeaderNW12.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW12.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW12.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW12.StylePriority.UseFont = false;
            this.xrLabelHeaderNW12.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW12.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW12.Text = "Weekend (night)";
            this.xrLabelHeaderNW12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND12
            // 
            this.xrLabelHeaderNND12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND12.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND12.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND12.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND12.LocationFloat = new DevExpress.Utils.PointFloat(4032.292F, 34.87811F);
            this.xrLabelHeaderNND12.Multiline = true;
            this.xrLabelHeaderNND12.Name = "xrLabelHeaderNND12";
            this.xrLabelHeaderNND12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND12.SizeF = new System.Drawing.SizeF(43.92236F, 53.38717F);
            this.xrLabelHeaderNND12.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND12.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND12.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND12.StylePriority.UseFont = false;
            this.xrLabelHeaderNND12.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND12.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND12.Text = "Normal (night)";
            this.xrLabelHeaderNND12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH12
            // 
            this.xrLabelHeaderH12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH12.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH12.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH12.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH12.LocationFloat = new DevExpress.Utils.PointFloat(3981.547F, 34.8499F);
            this.xrLabelHeaderH12.Multiline = true;
            this.xrLabelHeaderH12.Name = "xrLabelHeaderH12";
            this.xrLabelHeaderH12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH12.SizeF = new System.Drawing.SizeF(50.74512F, 53.38714F);
            this.xrLabelHeaderH12.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH12.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH12.StylePriority.UseBorders = false;
            this.xrLabelHeaderH12.StylePriority.UseFont = false;
            this.xrLabelHeaderH12.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH12.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH12.Text = "Holiday (day)";
            this.xrLabelHeaderH12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW12
            // 
            this.xrLabelHeaderW12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW12.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW12.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW12.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW12.LocationFloat = new DevExpress.Utils.PointFloat(3921.731F, 34.8499F);
            this.xrLabelHeaderW12.Multiline = true;
            this.xrLabelHeaderW12.Name = "xrLabelHeaderW12";
            this.xrLabelHeaderW12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW12.SizeF = new System.Drawing.SizeF(59.81616F, 53.38714F);
            this.xrLabelHeaderW12.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW12.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW12.StylePriority.UseBorders = false;
            this.xrLabelHeaderW12.StylePriority.UseFont = false;
            this.xrLabelHeaderW12.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW12.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW12.Text = "Weekend(day)";
            this.xrLabelHeaderW12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTDecember
            // 
            this.xrLabelOTDecember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTDecember.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTDecember.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTDecember.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTDecember.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTDecember.LocationFloat = new DevExpress.Utils.PointFloat(3876.758F, 0F);
            this.xrLabelOTDecember.Name = "xrLabelOTDecember";
            this.xrLabelOTDecember.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTDecember.SizeF = new System.Drawing.SizeF(313.0957F, 34.84834F);
            this.xrLabelOTDecember.StylePriority.UseBackColor = false;
            this.xrLabelOTDecember.StylePriority.UseBorderColor = false;
            this.xrLabelOTDecember.StylePriority.UseBorders = false;
            this.xrLabelOTDecember.StylePriority.UseFont = false;
            this.xrLabelOTDecember.StylePriority.UseForeColor = false;
            this.xrLabelOTDecember.StylePriority.UseTextAlignment = false;
            this.xrLabelOTDecember.Text = "December";
            this.xrLabelOTDecember.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND11
            // 
            this.xrLabelHeaderND11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND11.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND11.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND11.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND11.LocationFloat = new DevExpress.Utils.PointFloat(3565.394F, 34.85765F);
            this.xrLabelHeaderND11.Multiline = true;
            this.xrLabelHeaderND11.Name = "xrLabelHeaderND11";
            this.xrLabelHeaderND11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND11.SizeF = new System.Drawing.SizeF(43.85059F, 53.38752F);
            this.xrLabelHeaderND11.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND11.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND11.StylePriority.UseBorders = false;
            this.xrLabelHeaderND11.StylePriority.UseFont = false;
            this.xrLabelHeaderND11.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND11.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND11.Text = "Normal (day)";
            this.xrLabelHeaderND11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH11
            // 
            this.xrLabelHeaderNH11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH11.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH11.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH11.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH11.LocationFloat = new DevExpress.Utils.PointFloat(3821.848F, 34.87197F);
            this.xrLabelHeaderNH11.Multiline = true;
            this.xrLabelHeaderNH11.Name = "xrLabelHeaderNH11";
            this.xrLabelHeaderNH11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH11.SizeF = new System.Drawing.SizeF(54.90967F, 53.38714F);
            this.xrLabelHeaderNH11.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH11.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH11.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH11.StylePriority.UseFont = false;
            this.xrLabelHeaderNH11.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH11.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH11.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW11
            // 
            this.xrLabelHeaderNW11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW11.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW11.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW11.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW11.LocationFloat = new DevExpress.Utils.PointFloat(3763.728F, 34.87159F);
            this.xrLabelHeaderNW11.Multiline = true;
            this.xrLabelHeaderNW11.Name = "xrLabelHeaderNW11";
            this.xrLabelHeaderNW11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW11.SizeF = new System.Drawing.SizeF(58.01807F, 53.38752F);
            this.xrLabelHeaderNW11.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW11.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW11.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW11.StylePriority.UseFont = false;
            this.xrLabelHeaderNW11.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW11.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW11.Text = "Weekend (night)";
            this.xrLabelHeaderNW11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND11
            // 
            this.xrLabelHeaderNND11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND11.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND11.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND11.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND11.LocationFloat = new DevExpress.Utils.PointFloat(3719.806F, 34.87159F);
            this.xrLabelHeaderNND11.Multiline = true;
            this.xrLabelHeaderNND11.Name = "xrLabelHeaderNND11";
            this.xrLabelHeaderNND11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND11.SizeF = new System.Drawing.SizeF(43.92212F, 53.38717F);
            this.xrLabelHeaderNND11.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND11.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND11.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND11.StylePriority.UseFont = false;
            this.xrLabelHeaderNND11.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND11.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND11.Text = "Normal (night)";
            this.xrLabelHeaderNND11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH11
            // 
            this.xrLabelHeaderH11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH11.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH11.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH11.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH11.LocationFloat = new DevExpress.Utils.PointFloat(3669.061F, 34.87159F);
            this.xrLabelHeaderH11.Multiline = true;
            this.xrLabelHeaderH11.Name = "xrLabelHeaderH11";
            this.xrLabelHeaderH11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH11.SizeF = new System.Drawing.SizeF(50.74512F, 53.38714F);
            this.xrLabelHeaderH11.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH11.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH11.StylePriority.UseBorders = false;
            this.xrLabelHeaderH11.StylePriority.UseFont = false;
            this.xrLabelHeaderH11.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH11.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH11.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW11
            // 
            this.xrLabelHeaderW11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW11.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW11.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW11.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW11.LocationFloat = new DevExpress.Utils.PointFloat(3609.245F, 34.87161F);
            this.xrLabelHeaderW11.Multiline = true;
            this.xrLabelHeaderW11.Name = "xrLabelHeaderW11";
            this.xrLabelHeaderW11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW11.SizeF = new System.Drawing.SizeF(59.81494F, 53.38713F);
            this.xrLabelHeaderW11.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW11.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW11.StylePriority.UseBorders = false;
            this.xrLabelHeaderW11.StylePriority.UseFont = false;
            this.xrLabelHeaderW11.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW11.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW11.Text = "Weekend(day)";
            this.xrLabelHeaderW11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTNovember
            // 
            this.xrLabelOTNovember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTNovember.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTNovember.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTNovember.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTNovember.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTNovember.LocationFloat = new DevExpress.Utils.PointFloat(3565.394F, 0.007760525F);
            this.xrLabelOTNovember.Name = "xrLabelOTNovember";
            this.xrLabelOTNovember.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTNovember.SizeF = new System.Drawing.SizeF(311.364F, 34.84834F);
            this.xrLabelOTNovember.StylePriority.UseBackColor = false;
            this.xrLabelOTNovember.StylePriority.UseBorderColor = false;
            this.xrLabelOTNovember.StylePriority.UseBorders = false;
            this.xrLabelOTNovember.StylePriority.UseFont = false;
            this.xrLabelOTNovember.StylePriority.UseForeColor = false;
            this.xrLabelOTNovember.StylePriority.UseTextAlignment = false;
            this.xrLabelOTNovember.Text = "November";
            this.xrLabelOTNovember.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND10
            // 
            this.xrLabelHeaderND10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND10.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND10.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND10.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND10.LocationFloat = new DevExpress.Utils.PointFloat(3253.106F, 34.85765F);
            this.xrLabelHeaderND10.Multiline = true;
            this.xrLabelHeaderND10.Name = "xrLabelHeaderND10";
            this.xrLabelHeaderND10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND10.SizeF = new System.Drawing.SizeF(43.97363F, 53.38752F);
            this.xrLabelHeaderND10.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND10.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND10.StylePriority.UseBorders = false;
            this.xrLabelHeaderND10.StylePriority.UseFont = false;
            this.xrLabelHeaderND10.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND10.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND10.Text = "Normal (day)";
            this.xrLabelHeaderND10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH10
            // 
            this.xrLabelHeaderNH10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH10.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH10.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH10.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH10.LocationFloat = new DevExpress.Utils.PointFloat(3509.683F, 34.87199F);
            this.xrLabelHeaderNH10.Multiline = true;
            this.xrLabelHeaderNH10.Name = "xrLabelHeaderNH10";
            this.xrLabelHeaderNH10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH10.SizeF = new System.Drawing.SizeF(55.71094F, 53.38715F);
            this.xrLabelHeaderNH10.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH10.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH10.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH10.StylePriority.UseFont = false;
            this.xrLabelHeaderNH10.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH10.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH10.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW10
            // 
            this.xrLabelHeaderNW10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW10.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW10.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW10.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW10.LocationFloat = new DevExpress.Utils.PointFloat(3451.563F, 34.87161F);
            this.xrLabelHeaderNW10.Multiline = true;
            this.xrLabelHeaderNW10.Name = "xrLabelHeaderNW10";
            this.xrLabelHeaderNW10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW10.SizeF = new System.Drawing.SizeF(58.01807F, 53.38752F);
            this.xrLabelHeaderNW10.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW10.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW10.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW10.StylePriority.UseFont = false;
            this.xrLabelHeaderNW10.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW10.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW10.Text = "Weekend (night)";
            this.xrLabelHeaderNW10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND10
            // 
            this.xrLabelHeaderNND10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND10.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND10.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND10.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND10.LocationFloat = new DevExpress.Utils.PointFloat(3407.641F, 34.87161F);
            this.xrLabelHeaderNND10.Multiline = true;
            this.xrLabelHeaderNND10.Name = "xrLabelHeaderNND10";
            this.xrLabelHeaderNND10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND10.SizeF = new System.Drawing.SizeF(43.92212F, 53.38717F);
            this.xrLabelHeaderNND10.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND10.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND10.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND10.StylePriority.UseFont = false;
            this.xrLabelHeaderNND10.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND10.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND10.Text = "Normal (night)";
            this.xrLabelHeaderNND10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH10
            // 
            this.xrLabelHeaderH10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH10.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH10.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH10.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH10.LocationFloat = new DevExpress.Utils.PointFloat(3356.896F, 34.87161F);
            this.xrLabelHeaderH10.Multiline = true;
            this.xrLabelHeaderH10.Name = "xrLabelHeaderH10";
            this.xrLabelHeaderH10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH10.SizeF = new System.Drawing.SizeF(50.74512F, 53.38713F);
            this.xrLabelHeaderH10.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH10.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH10.StylePriority.UseBorders = false;
            this.xrLabelHeaderH10.StylePriority.UseFont = false;
            this.xrLabelHeaderH10.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH10.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH10.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW10
            // 
            this.xrLabelHeaderW10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW10.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW10.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW10.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW10.LocationFloat = new DevExpress.Utils.PointFloat(3297.08F, 34.87161F);
            this.xrLabelHeaderW10.Multiline = true;
            this.xrLabelHeaderW10.Name = "xrLabelHeaderW10";
            this.xrLabelHeaderW10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW10.SizeF = new System.Drawing.SizeF(59.81641F, 53.38713F);
            this.xrLabelHeaderW10.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW10.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW10.StylePriority.UseBorders = false;
            this.xrLabelHeaderW10.StylePriority.UseFont = false;
            this.xrLabelHeaderW10.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW10.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW10.Text = "Weekend(day)";
            this.xrLabelHeaderW10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTOctober
            // 
            this.xrLabelOTOctober.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTOctober.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTOctober.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTOctober.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTOctober.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTOctober.LocationFloat = new DevExpress.Utils.PointFloat(3253.106F, 0.007760525F);
            this.xrLabelOTOctober.Name = "xrLabelOTOctober";
            this.xrLabelOTOctober.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTOctober.SizeF = new System.Drawing.SizeF(312.2881F, 34.84834F);
            this.xrLabelOTOctober.StylePriority.UseBackColor = false;
            this.xrLabelOTOctober.StylePriority.UseBorderColor = false;
            this.xrLabelOTOctober.StylePriority.UseBorders = false;
            this.xrLabelOTOctober.StylePriority.UseFont = false;
            this.xrLabelOTOctober.StylePriority.UseForeColor = false;
            this.xrLabelOTOctober.StylePriority.UseTextAlignment = false;
            this.xrLabelOTOctober.Text = "October";
            this.xrLabelOTOctober.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND9
            // 
            this.xrLabelHeaderND9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND9.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND9.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND9.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND9.LocationFloat = new DevExpress.Utils.PointFloat(2940.836F, 34.8499F);
            this.xrLabelHeaderND9.Multiline = true;
            this.xrLabelHeaderND9.Name = "xrLabelHeaderND9";
            this.xrLabelHeaderND9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND9.SizeF = new System.Drawing.SizeF(44.68799F, 53.4011F);
            this.xrLabelHeaderND9.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND9.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND9.StylePriority.UseBorders = false;
            this.xrLabelHeaderND9.StylePriority.UseFont = false;
            this.xrLabelHeaderND9.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND9.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND9.Text = "Normal (day)";
            this.xrLabelHeaderND9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH9
            // 
            this.xrLabelHeaderNH9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH9.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH9.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH9.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH9.LocationFloat = new DevExpress.Utils.PointFloat(3198.127F, 34.86423F);
            this.xrLabelHeaderNH9.Multiline = true;
            this.xrLabelHeaderNH9.Name = "xrLabelHeaderNH9";
            this.xrLabelHeaderNH9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH9.SizeF = new System.Drawing.SizeF(54.90942F, 53.38715F);
            this.xrLabelHeaderNH9.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH9.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH9.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH9.StylePriority.UseFont = false;
            this.xrLabelHeaderNH9.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH9.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH9.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW9
            // 
            this.xrLabelHeaderNW9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW9.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW9.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW9.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW9.LocationFloat = new DevExpress.Utils.PointFloat(3140.007F, 34.86387F);
            this.xrLabelHeaderNW9.Multiline = true;
            this.xrLabelHeaderNW9.Name = "xrLabelHeaderNW9";
            this.xrLabelHeaderNW9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW9.SizeF = new System.Drawing.SizeF(58.01782F, 53.38752F);
            this.xrLabelHeaderNW9.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW9.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW9.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW9.StylePriority.UseFont = false;
            this.xrLabelHeaderNW9.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW9.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW9.Text = "Weekend (night)";
            this.xrLabelHeaderNW9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND9
            // 
            this.xrLabelHeaderNND9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND9.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND9.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND9.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND9.LocationFloat = new DevExpress.Utils.PointFloat(3096.085F, 34.86387F);
            this.xrLabelHeaderNND9.Multiline = true;
            this.xrLabelHeaderNND9.Name = "xrLabelHeaderNND9";
            this.xrLabelHeaderNND9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND9.SizeF = new System.Drawing.SizeF(43.92236F, 53.38717F);
            this.xrLabelHeaderNND9.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND9.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND9.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND9.StylePriority.UseFont = false;
            this.xrLabelHeaderNND9.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND9.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND9.Text = "Normal (night)";
            this.xrLabelHeaderNND9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH9
            // 
            this.xrLabelHeaderH9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH9.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH9.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH9.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH9.LocationFloat = new DevExpress.Utils.PointFloat(3045.34F, 34.86387F);
            this.xrLabelHeaderH9.Multiline = true;
            this.xrLabelHeaderH9.Name = "xrLabelHeaderH9";
            this.xrLabelHeaderH9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH9.SizeF = new System.Drawing.SizeF(50.74487F, 53.38714F);
            this.xrLabelHeaderH9.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH9.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH9.StylePriority.UseBorders = false;
            this.xrLabelHeaderH9.StylePriority.UseFont = false;
            this.xrLabelHeaderH9.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH9.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH9.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW9
            // 
            this.xrLabelHeaderW9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW9.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW9.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW9.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW9.LocationFloat = new DevExpress.Utils.PointFloat(2985.524F, 34.86387F);
            this.xrLabelHeaderW9.Multiline = true;
            this.xrLabelHeaderW9.Name = "xrLabelHeaderW9";
            this.xrLabelHeaderW9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW9.SizeF = new System.Drawing.SizeF(59.81616F, 53.40073F);
            this.xrLabelHeaderW9.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW9.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW9.StylePriority.UseBorders = false;
            this.xrLabelHeaderW9.StylePriority.UseFont = false;
            this.xrLabelHeaderW9.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW9.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW9.Text = "Weekend(day)";
            this.xrLabelHeaderW9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTSeptember
            // 
            this.xrLabelOTSeptember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTSeptember.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTSeptember.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTSeptember.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTSeptember.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTSeptember.LocationFloat = new DevExpress.Utils.PointFloat(2940.836F, 0F);
            this.xrLabelOTSeptember.Name = "xrLabelOTSeptember";
            this.xrLabelOTSeptember.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTSeptember.SizeF = new System.Drawing.SizeF(312.2014F, 34.84834F);
            this.xrLabelOTSeptember.StylePriority.UseBackColor = false;
            this.xrLabelOTSeptember.StylePriority.UseBorderColor = false;
            this.xrLabelOTSeptember.StylePriority.UseBorders = false;
            this.xrLabelOTSeptember.StylePriority.UseFont = false;
            this.xrLabelOTSeptember.StylePriority.UseForeColor = false;
            this.xrLabelOTSeptember.StylePriority.UseTextAlignment = false;
            this.xrLabelOTSeptember.Text = "September";
            this.xrLabelOTSeptember.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND8
            // 
            this.xrLabelHeaderND8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND8.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND8.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND8.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND8.LocationFloat = new DevExpress.Utils.PointFloat(2629.349F, 34.85767F);
            this.xrLabelHeaderND8.Multiline = true;
            this.xrLabelHeaderND8.Name = "xrLabelHeaderND8";
            this.xrLabelHeaderND8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND8.SizeF = new System.Drawing.SizeF(43.97363F, 53.4011F);
            this.xrLabelHeaderND8.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND8.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND8.StylePriority.UseBorders = false;
            this.xrLabelHeaderND8.StylePriority.UseFont = false;
            this.xrLabelHeaderND8.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND8.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND8.Text = "Normal (day)";
            this.xrLabelHeaderND8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH8
            // 
            this.xrLabelHeaderNH8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH8.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH8.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH8.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH8.LocationFloat = new DevExpress.Utils.PointFloat(2885.926F, 34.86385F);
            this.xrLabelHeaderNH8.Multiline = true;
            this.xrLabelHeaderNH8.Name = "xrLabelHeaderNH8";
            this.xrLabelHeaderNH8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH8.SizeF = new System.Drawing.SizeF(54.90967F, 53.40074F);
            this.xrLabelHeaderNH8.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH8.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH8.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH8.StylePriority.UseFont = false;
            this.xrLabelHeaderNH8.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH8.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH8.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW8
            // 
            this.xrLabelHeaderNW8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW8.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW8.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW8.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW8.LocationFloat = new DevExpress.Utils.PointFloat(2827.806F, 34.86348F);
            this.xrLabelHeaderNW8.Multiline = true;
            this.xrLabelHeaderNW8.Name = "xrLabelHeaderNW8";
            this.xrLabelHeaderNW8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW8.SizeF = new System.Drawing.SizeF(58.01807F, 53.40112F);
            this.xrLabelHeaderNW8.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW8.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW8.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW8.StylePriority.UseFont = false;
            this.xrLabelHeaderNW8.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW8.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW8.Text = "Weekend (night)";
            this.xrLabelHeaderNW8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND8
            // 
            this.xrLabelHeaderNND8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND8.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND8.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND8.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND8.LocationFloat = new DevExpress.Utils.PointFloat(2783.884F, 34.86348F);
            this.xrLabelHeaderNND8.Multiline = true;
            this.xrLabelHeaderNND8.Name = "xrLabelHeaderNND8";
            this.xrLabelHeaderNND8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND8.SizeF = new System.Drawing.SizeF(43.92212F, 53.40076F);
            this.xrLabelHeaderNND8.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND8.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND8.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND8.StylePriority.UseFont = false;
            this.xrLabelHeaderNND8.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND8.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND8.Text = "Normal (night)";
            this.xrLabelHeaderNND8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH8
            // 
            this.xrLabelHeaderH8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH8.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH8.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH8.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH8.LocationFloat = new DevExpress.Utils.PointFloat(2733.139F, 34.86348F);
            this.xrLabelHeaderH8.Multiline = true;
            this.xrLabelHeaderH8.Name = "xrLabelHeaderH8";
            this.xrLabelHeaderH8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH8.SizeF = new System.Drawing.SizeF(50.7439F, 53.40073F);
            this.xrLabelHeaderH8.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH8.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH8.StylePriority.UseBorders = false;
            this.xrLabelHeaderH8.StylePriority.UseFont = false;
            this.xrLabelHeaderH8.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH8.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH8.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW8
            // 
            this.xrLabelHeaderW8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW8.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW8.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW8.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW8.LocationFloat = new DevExpress.Utils.PointFloat(2673.323F, 34.86385F);
            this.xrLabelHeaderW8.Multiline = true;
            this.xrLabelHeaderW8.Name = "xrLabelHeaderW8";
            this.xrLabelHeaderW8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW8.SizeF = new System.Drawing.SizeF(59.81592F, 53.40072F);
            this.xrLabelHeaderW8.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW8.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW8.StylePriority.UseBorders = false;
            this.xrLabelHeaderW8.StylePriority.UseFont = false;
            this.xrLabelHeaderW8.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW8.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW8.Text = "Weekend(day)";
            this.xrLabelHeaderW8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTAugust
            // 
            this.xrLabelOTAugust.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTAugust.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTAugust.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTAugust.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTAugust.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTAugust.LocationFloat = new DevExpress.Utils.PointFloat(2629.349F, 0.007760525F);
            this.xrLabelOTAugust.Name = "xrLabelOTAugust";
            this.xrLabelOTAugust.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTAugust.SizeF = new System.Drawing.SizeF(311.4863F, 34.84834F);
            this.xrLabelOTAugust.StylePriority.UseBackColor = false;
            this.xrLabelOTAugust.StylePriority.UseBorderColor = false;
            this.xrLabelOTAugust.StylePriority.UseBorders = false;
            this.xrLabelOTAugust.StylePriority.UseFont = false;
            this.xrLabelOTAugust.StylePriority.UseForeColor = false;
            this.xrLabelOTAugust.StylePriority.UseTextAlignment = false;
            this.xrLabelOTAugust.Text = "August";
            this.xrLabelOTAugust.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND7
            // 
            this.xrLabelHeaderND7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND7.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND7.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND7.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND7.LocationFloat = new DevExpress.Utils.PointFloat(2317.148F, 34.87811F);
            this.xrLabelHeaderND7.Multiline = true;
            this.xrLabelHeaderND7.Name = "xrLabelHeaderND7";
            this.xrLabelHeaderND7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND7.SizeF = new System.Drawing.SizeF(44.68799F, 53.37835F);
            this.xrLabelHeaderND7.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND7.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND7.StylePriority.UseBorders = false;
            this.xrLabelHeaderND7.StylePriority.UseFont = false;
            this.xrLabelHeaderND7.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND7.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND7.Text = "Normal (day)";
            this.xrLabelHeaderND7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH7
            // 
            this.xrLabelHeaderNH7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH7.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH7.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH7.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH7.LocationFloat = new DevExpress.Utils.PointFloat(2574.439F, 34.85609F);
            this.xrLabelHeaderNH7.Multiline = true;
            this.xrLabelHeaderNH7.Name = "xrLabelHeaderNH7";
            this.xrLabelHeaderNH7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH7.SizeF = new System.Drawing.SizeF(54.90967F, 53.40075F);
            this.xrLabelHeaderNH7.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH7.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH7.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH7.StylePriority.UseFont = false;
            this.xrLabelHeaderNH7.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH7.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH7.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW7
            // 
            this.xrLabelHeaderNW7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW7.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW7.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW7.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW7.LocationFloat = new DevExpress.Utils.PointFloat(2516.319F, 34.85572F);
            this.xrLabelHeaderNW7.Multiline = true;
            this.xrLabelHeaderNW7.Name = "xrLabelHeaderNW7";
            this.xrLabelHeaderNW7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW7.SizeF = new System.Drawing.SizeF(58.01807F, 53.40112F);
            this.xrLabelHeaderNW7.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW7.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW7.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW7.StylePriority.UseFont = false;
            this.xrLabelHeaderNW7.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW7.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW7.Text = "Weekend (night)";
            this.xrLabelHeaderNW7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND7
            // 
            this.xrLabelHeaderNND7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND7.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND7.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND7.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND7.LocationFloat = new DevExpress.Utils.PointFloat(2472.397F, 34.85572F);
            this.xrLabelHeaderNND7.Multiline = true;
            this.xrLabelHeaderNND7.Name = "xrLabelHeaderNND7";
            this.xrLabelHeaderNND7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND7.SizeF = new System.Drawing.SizeF(43.92212F, 53.40076F);
            this.xrLabelHeaderNND7.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND7.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND7.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND7.StylePriority.UseFont = false;
            this.xrLabelHeaderNND7.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND7.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND7.Text = "Normal (night)";
            this.xrLabelHeaderNND7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH7
            // 
            this.xrLabelHeaderH7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH7.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH7.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH7.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH7.LocationFloat = new DevExpress.Utils.PointFloat(2421.652F, 34.85572F);
            this.xrLabelHeaderH7.Multiline = true;
            this.xrLabelHeaderH7.Name = "xrLabelHeaderH7";
            this.xrLabelHeaderH7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH7.SizeF = new System.Drawing.SizeF(50.74512F, 53.40073F);
            this.xrLabelHeaderH7.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH7.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH7.StylePriority.UseBorders = false;
            this.xrLabelHeaderH7.StylePriority.UseFont = false;
            this.xrLabelHeaderH7.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH7.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH7.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW7
            // 
            this.xrLabelHeaderW7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW7.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW7.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW7.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW7.LocationFloat = new DevExpress.Utils.PointFloat(2361.836F, 34.85572F);
            this.xrLabelHeaderW7.Multiline = true;
            this.xrLabelHeaderW7.Name = "xrLabelHeaderW7";
            this.xrLabelHeaderW7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW7.SizeF = new System.Drawing.SizeF(59.81592F, 53.40073F);
            this.xrLabelHeaderW7.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW7.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW7.StylePriority.UseBorders = false;
            this.xrLabelHeaderW7.StylePriority.UseFont = false;
            this.xrLabelHeaderW7.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW7.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW7.Text = "Weekend(day)";
            this.xrLabelHeaderW7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTJuly
            // 
            this.xrLabelOTJuly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTJuly.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTJuly.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTJuly.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTJuly.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTJuly.LocationFloat = new DevExpress.Utils.PointFloat(2317.148F, 0.007760525F);
            this.xrLabelOTJuly.Name = "xrLabelOTJuly";
            this.xrLabelOTJuly.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTJuly.SizeF = new System.Drawing.SizeF(312.2002F, 34.84834F);
            this.xrLabelOTJuly.StylePriority.UseBackColor = false;
            this.xrLabelOTJuly.StylePriority.UseBorderColor = false;
            this.xrLabelOTJuly.StylePriority.UseBorders = false;
            this.xrLabelOTJuly.StylePriority.UseFont = false;
            this.xrLabelOTJuly.StylePriority.UseForeColor = false;
            this.xrLabelOTJuly.StylePriority.UseTextAlignment = false;
            this.xrLabelOTJuly.Text = "July";
            this.xrLabelOTJuly.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND6
            // 
            this.xrLabelHeaderND6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND6.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND6.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND6.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND6.LocationFloat = new DevExpress.Utils.PointFloat(2005.661F, 34.85765F);
            this.xrLabelHeaderND6.Multiline = true;
            this.xrLabelHeaderND6.Name = "xrLabelHeaderND6";
            this.xrLabelHeaderND6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND6.SizeF = new System.Drawing.SizeF(43.97375F, 53.39882F);
            this.xrLabelHeaderND6.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND6.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND6.StylePriority.UseBorders = false;
            this.xrLabelHeaderND6.StylePriority.UseFont = false;
            this.xrLabelHeaderND6.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND6.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND6.Text = "Normal (day)";
            this.xrLabelHeaderND6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH6
            // 
            this.xrLabelHeaderNH6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH6.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH6.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH6.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH6.LocationFloat = new DevExpress.Utils.PointFloat(2262.238F, 34.86385F);
            this.xrLabelHeaderNH6.Multiline = true;
            this.xrLabelHeaderNH6.Name = "xrLabelHeaderNH6";
            this.xrLabelHeaderNH6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH6.SizeF = new System.Drawing.SizeF(54.90991F, 53.39847F);
            this.xrLabelHeaderNH6.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH6.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH6.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH6.StylePriority.UseFont = false;
            this.xrLabelHeaderNH6.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH6.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH6.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW6
            // 
            this.xrLabelHeaderNW6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW6.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW6.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW6.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW6.LocationFloat = new DevExpress.Utils.PointFloat(2204.118F, 34.86347F);
            this.xrLabelHeaderNW6.Multiline = true;
            this.xrLabelHeaderNW6.Name = "xrLabelHeaderNW6";
            this.xrLabelHeaderNW6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW6.SizeF = new System.Drawing.SizeF(58.01807F, 53.39883F);
            this.xrLabelHeaderNW6.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW6.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW6.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW6.StylePriority.UseFont = false;
            this.xrLabelHeaderNW6.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW6.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW6.Text = "Weekend (night)";
            this.xrLabelHeaderNW6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND6
            // 
            this.xrLabelHeaderNND6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND6.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND6.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND6.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND6.LocationFloat = new DevExpress.Utils.PointFloat(2160.196F, 34.86347F);
            this.xrLabelHeaderNND6.Multiline = true;
            this.xrLabelHeaderNND6.Name = "xrLabelHeaderNND6";
            this.xrLabelHeaderNND6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND6.SizeF = new System.Drawing.SizeF(43.92236F, 53.39848F);
            this.xrLabelHeaderNND6.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND6.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND6.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND6.StylePriority.UseFont = false;
            this.xrLabelHeaderNND6.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND6.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND6.Text = "Normal (night)";
            this.xrLabelHeaderNND6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH6
            // 
            this.xrLabelHeaderH6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH6.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH6.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH6.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH6.LocationFloat = new DevExpress.Utils.PointFloat(2109.451F, 34.86347F);
            this.xrLabelHeaderH6.Multiline = true;
            this.xrLabelHeaderH6.Name = "xrLabelHeaderH6";
            this.xrLabelHeaderH6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH6.SizeF = new System.Drawing.SizeF(50.74512F, 53.39845F);
            this.xrLabelHeaderH6.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH6.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH6.StylePriority.UseBorders = false;
            this.xrLabelHeaderH6.StylePriority.UseFont = false;
            this.xrLabelHeaderH6.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH6.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH6.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW6
            // 
            this.xrLabelHeaderW6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW6.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW6.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW6.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW6.LocationFloat = new DevExpress.Utils.PointFloat(2049.635F, 34.86347F);
            this.xrLabelHeaderW6.Multiline = true;
            this.xrLabelHeaderW6.Name = "xrLabelHeaderW6";
            this.xrLabelHeaderW6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW6.SizeF = new System.Drawing.SizeF(59.81567F, 53.39845F);
            this.xrLabelHeaderW6.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW6.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW6.StylePriority.UseBorders = false;
            this.xrLabelHeaderW6.StylePriority.UseFont = false;
            this.xrLabelHeaderW6.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW6.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW6.Text = "Weekend(day)";
            this.xrLabelHeaderW6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTJune
            // 
            this.xrLabelOTJune.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTJune.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTJune.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTJune.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTJune.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTJune.LocationFloat = new DevExpress.Utils.PointFloat(2005.661F, 0.007754878F);
            this.xrLabelOTJune.Name = "xrLabelOTJune";
            this.xrLabelOTJune.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTJune.SizeF = new System.Drawing.SizeF(311.4871F, 34.84834F);
            this.xrLabelOTJune.StylePriority.UseBackColor = false;
            this.xrLabelOTJune.StylePriority.UseBorderColor = false;
            this.xrLabelOTJune.StylePriority.UseBorders = false;
            this.xrLabelOTJune.StylePriority.UseFont = false;
            this.xrLabelOTJune.StylePriority.UseForeColor = false;
            this.xrLabelOTJune.StylePriority.UseTextAlignment = false;
            this.xrLabelOTJune.Text = "June";
            this.xrLabelOTJune.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND5
            // 
            this.xrLabelHeaderND5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND5.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND5.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND5.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND5.LocationFloat = new DevExpress.Utils.PointFloat(1694.174F, 34.8499F);
            this.xrLabelHeaderND5.Multiline = true;
            this.xrLabelHeaderND5.Name = "xrLabelHeaderND5";
            this.xrLabelHeaderND5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND5.SizeF = new System.Drawing.SizeF(43.97375F, 53.40657F);
            this.xrLabelHeaderND5.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND5.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND5.StylePriority.UseBorders = false;
            this.xrLabelHeaderND5.StylePriority.UseFont = false;
            this.xrLabelHeaderND5.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND5.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND5.Text = "Normal (day)";
            this.xrLabelHeaderND5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH5
            // 
            this.xrLabelHeaderNH5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH5.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH5.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH5.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH5.LocationFloat = new DevExpress.Utils.PointFloat(1950.751F, 34.8561F);
            this.xrLabelHeaderNH5.Multiline = true;
            this.xrLabelHeaderNH5.Name = "xrLabelHeaderNH5";
            this.xrLabelHeaderNH5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH5.SizeF = new System.Drawing.SizeF(54.90967F, 53.40621F);
            this.xrLabelHeaderNH5.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH5.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH5.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH5.StylePriority.UseFont = false;
            this.xrLabelHeaderNH5.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH5.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH5.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW5
            // 
            this.xrLabelHeaderNW5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW5.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW5.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW5.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW5.LocationFloat = new DevExpress.Utils.PointFloat(1892.631F, 34.85572F);
            this.xrLabelHeaderNW5.Multiline = true;
            this.xrLabelHeaderNW5.Name = "xrLabelHeaderNW5";
            this.xrLabelHeaderNW5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW5.SizeF = new System.Drawing.SizeF(58.01807F, 53.40657F);
            this.xrLabelHeaderNW5.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW5.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW5.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW5.StylePriority.UseFont = false;
            this.xrLabelHeaderNW5.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW5.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW5.Text = "Weekend (night)";
            this.xrLabelHeaderNW5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND5
            // 
            this.xrLabelHeaderNND5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND5.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND5.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND5.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND5.LocationFloat = new DevExpress.Utils.PointFloat(1848.709F, 34.85572F);
            this.xrLabelHeaderNND5.Multiline = true;
            this.xrLabelHeaderNND5.Name = "xrLabelHeaderNND5";
            this.xrLabelHeaderNND5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND5.SizeF = new System.Drawing.SizeF(43.922F, 53.40622F);
            this.xrLabelHeaderNND5.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND5.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND5.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND5.StylePriority.UseFont = false;
            this.xrLabelHeaderNND5.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND5.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND5.Text = "Normal (night)";
            this.xrLabelHeaderNND5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH5
            // 
            this.xrLabelHeaderH5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH5.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH5.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH5.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH5.LocationFloat = new DevExpress.Utils.PointFloat(1797.964F, 34.85572F);
            this.xrLabelHeaderH5.Multiline = true;
            this.xrLabelHeaderH5.Name = "xrLabelHeaderH5";
            this.xrLabelHeaderH5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH5.SizeF = new System.Drawing.SizeF(50.74512F, 53.40619F);
            this.xrLabelHeaderH5.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH5.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH5.StylePriority.UseBorders = false;
            this.xrLabelHeaderH5.StylePriority.UseFont = false;
            this.xrLabelHeaderH5.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH5.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH5.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW5
            // 
            this.xrLabelHeaderW5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW5.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW5.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW5.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW5.LocationFloat = new DevExpress.Utils.PointFloat(1738.148F, 34.85572F);
            this.xrLabelHeaderW5.Multiline = true;
            this.xrLabelHeaderW5.Name = "xrLabelHeaderW5";
            this.xrLabelHeaderW5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW5.SizeF = new System.Drawing.SizeF(59.81592F, 53.40619F);
            this.xrLabelHeaderW5.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW5.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW5.StylePriority.UseBorders = false;
            this.xrLabelHeaderW5.StylePriority.UseFont = false;
            this.xrLabelHeaderW5.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW5.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW5.Text = "Weekend(day)";
            this.xrLabelHeaderW5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTMay
            // 
            this.xrLabelOTMay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTMay.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTMay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTMay.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTMay.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTMay.LocationFloat = new DevExpress.Utils.PointFloat(1694.174F, 0F);
            this.xrLabelOTMay.Name = "xrLabelOTMay";
            this.xrLabelOTMay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTMay.SizeF = new System.Drawing.SizeF(311.4871F, 34.84834F);
            this.xrLabelOTMay.StylePriority.UseBackColor = false;
            this.xrLabelOTMay.StylePriority.UseBorderColor = false;
            this.xrLabelOTMay.StylePriority.UseBorders = false;
            this.xrLabelOTMay.StylePriority.UseFont = false;
            this.xrLabelOTMay.StylePriority.UseForeColor = false;
            this.xrLabelOTMay.StylePriority.UseTextAlignment = false;
            this.xrLabelOTMay.Text = "May";
            this.xrLabelOTMay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTApril
            // 
            this.xrLabelOTApril.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTApril.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTApril.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTApril.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTApril.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTApril.LocationFloat = new DevExpress.Utils.PointFloat(1382.687F, 0F);
            this.xrLabelOTApril.Name = "xrLabelOTApril";
            this.xrLabelOTApril.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTApril.SizeF = new System.Drawing.SizeF(311.4871F, 34.84834F);
            this.xrLabelOTApril.StylePriority.UseBackColor = false;
            this.xrLabelOTApril.StylePriority.UseBorderColor = false;
            this.xrLabelOTApril.StylePriority.UseBorders = false;
            this.xrLabelOTApril.StylePriority.UseFont = false;
            this.xrLabelOTApril.StylePriority.UseForeColor = false;
            this.xrLabelOTApril.StylePriority.UseTextAlignment = false;
            this.xrLabelOTApril.Text = "April";
            this.xrLabelOTApril.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH4
            // 
            this.xrLabelHeaderNH4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH4.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH4.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH4.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH4.LocationFloat = new DevExpress.Utils.PointFloat(1639.264F, 34.8561F);
            this.xrLabelHeaderNH4.Multiline = true;
            this.xrLabelHeaderNH4.Name = "xrLabelHeaderNH4";
            this.xrLabelHeaderNH4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH4.SizeF = new System.Drawing.SizeF(54.90979F, 53.40621F);
            this.xrLabelHeaderNH4.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH4.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH4.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH4.StylePriority.UseFont = false;
            this.xrLabelHeaderNH4.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH4.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH4.Text = "Holidays\r\n(night)";
            this.xrLabelHeaderNH4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW4
            // 
            this.xrLabelHeaderNW4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW4.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW4.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW4.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW4.LocationFloat = new DevExpress.Utils.PointFloat(1581.144F, 34.85572F);
            this.xrLabelHeaderNW4.Multiline = true;
            this.xrLabelHeaderNW4.Name = "xrLabelHeaderNW4";
            this.xrLabelHeaderNW4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW4.SizeF = new System.Drawing.SizeF(58.01807F, 53.40658F);
            this.xrLabelHeaderNW4.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW4.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW4.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW4.StylePriority.UseFont = false;
            this.xrLabelHeaderNW4.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW4.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW4.Text = "Weekend (night)";
            this.xrLabelHeaderNW4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND4
            // 
            this.xrLabelHeaderNND4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND4.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND4.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND4.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND4.LocationFloat = new DevExpress.Utils.PointFloat(1537.222F, 34.85572F);
            this.xrLabelHeaderNND4.Multiline = true;
            this.xrLabelHeaderNND4.Name = "xrLabelHeaderNND4";
            this.xrLabelHeaderNND4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND4.SizeF = new System.Drawing.SizeF(43.922F, 53.40623F);
            this.xrLabelHeaderNND4.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND4.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND4.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND4.StylePriority.UseFont = false;
            this.xrLabelHeaderNND4.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND4.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND4.Text = "Normal (night)";
            this.xrLabelHeaderNND4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH4
            // 
            this.xrLabelHeaderH4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH4.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH4.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH4.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH4.LocationFloat = new DevExpress.Utils.PointFloat(1486.477F, 34.85572F);
            this.xrLabelHeaderH4.Multiline = true;
            this.xrLabelHeaderH4.Name = "xrLabelHeaderH4";
            this.xrLabelHeaderH4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH4.SizeF = new System.Drawing.SizeF(50.74512F, 53.4062F);
            this.xrLabelHeaderH4.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH4.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH4.StylePriority.UseBorders = false;
            this.xrLabelHeaderH4.StylePriority.UseFont = false;
            this.xrLabelHeaderH4.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH4.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH4.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW4
            // 
            this.xrLabelHeaderW4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW4.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW4.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW4.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW4.LocationFloat = new DevExpress.Utils.PointFloat(1426.661F, 34.85572F);
            this.xrLabelHeaderW4.Multiline = true;
            this.xrLabelHeaderW4.Name = "xrLabelHeaderW4";
            this.xrLabelHeaderW4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW4.SizeF = new System.Drawing.SizeF(59.81592F, 53.4062F);
            this.xrLabelHeaderW4.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW4.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW4.StylePriority.UseBorders = false;
            this.xrLabelHeaderW4.StylePriority.UseFont = false;
            this.xrLabelHeaderW4.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW4.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW4.Text = "Weekend(day)";
            this.xrLabelHeaderW4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND4
            // 
            this.xrLabelHeaderND4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND4.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND4.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND4.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND4.LocationFloat = new DevExpress.Utils.PointFloat(1382.687F, 34.84993F);
            this.xrLabelHeaderND4.Multiline = true;
            this.xrLabelHeaderND4.Name = "xrLabelHeaderND4";
            this.xrLabelHeaderND4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND4.SizeF = new System.Drawing.SizeF(43.97375F, 53.40657F);
            this.xrLabelHeaderND4.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND4.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND4.StylePriority.UseBorders = false;
            this.xrLabelHeaderND4.StylePriority.UseFont = false;
            this.xrLabelHeaderND4.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND4.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND4.Text = "Normal (day)";
            this.xrLabelHeaderND4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTMarch
            // 
            this.xrLabelOTMarch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTMarch.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTMarch.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTMarch.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTMarch.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTMarch.LocationFloat = new DevExpress.Utils.PointFloat(1071.2F, 0.001556397F);
            this.xrLabelOTMarch.Name = "xrLabelOTMarch";
            this.xrLabelOTMarch.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTMarch.SizeF = new System.Drawing.SizeF(311.4871F, 34.84834F);
            this.xrLabelOTMarch.StylePriority.UseBackColor = false;
            this.xrLabelOTMarch.StylePriority.UseBorderColor = false;
            this.xrLabelOTMarch.StylePriority.UseBorders = false;
            this.xrLabelOTMarch.StylePriority.UseFont = false;
            this.xrLabelOTMarch.StylePriority.UseForeColor = false;
            this.xrLabelOTMarch.StylePriority.UseTextAlignment = false;
            this.xrLabelOTMarch.Text = "March";
            this.xrLabelOTMarch.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH3
            // 
            this.xrLabelHeaderNH3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH3.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH3.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH3.LocationFloat = new DevExpress.Utils.PointFloat(1327.777F, 34.85765F);
            this.xrLabelHeaderNH3.Multiline = true;
            this.xrLabelHeaderNH3.Name = "xrLabelHeaderNH3";
            this.xrLabelHeaderNH3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH3.SizeF = new System.Drawing.SizeF(54.90979F, 53.40621F);
            this.xrLabelHeaderNH3.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH3.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH3.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH3.StylePriority.UseFont = false;
            this.xrLabelHeaderNH3.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH3.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH3.Text = "Holiday \r\n(night)";
            this.xrLabelHeaderNH3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW3
            // 
            this.xrLabelHeaderNW3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW3.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW3.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW3.LocationFloat = new DevExpress.Utils.PointFloat(1269.657F, 34.85727F);
            this.xrLabelHeaderNW3.Multiline = true;
            this.xrLabelHeaderNW3.Name = "xrLabelHeaderNW3";
            this.xrLabelHeaderNW3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW3.SizeF = new System.Drawing.SizeF(58.01807F, 53.40657F);
            this.xrLabelHeaderNW3.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW3.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW3.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW3.StylePriority.UseFont = false;
            this.xrLabelHeaderNW3.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW3.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW3.Text = "Weekend (night)";
            this.xrLabelHeaderNW3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND3
            // 
            this.xrLabelHeaderNND3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND3.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND3.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND3.LocationFloat = new DevExpress.Utils.PointFloat(1225.735F, 34.85727F);
            this.xrLabelHeaderNND3.Multiline = true;
            this.xrLabelHeaderNND3.Name = "xrLabelHeaderNND3";
            this.xrLabelHeaderNND3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND3.SizeF = new System.Drawing.SizeF(43.922F, 53.40622F);
            this.xrLabelHeaderNND3.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND3.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND3.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND3.StylePriority.UseFont = false;
            this.xrLabelHeaderNND3.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND3.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND3.Text = "Normal (night)";
            this.xrLabelHeaderNND3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH3
            // 
            this.xrLabelHeaderH3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH3.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH3.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH3.LocationFloat = new DevExpress.Utils.PointFloat(1174.99F, 34.85727F);
            this.xrLabelHeaderH3.Multiline = true;
            this.xrLabelHeaderH3.Name = "xrLabelHeaderH3";
            this.xrLabelHeaderH3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH3.SizeF = new System.Drawing.SizeF(50.74512F, 53.40619F);
            this.xrLabelHeaderH3.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH3.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH3.StylePriority.UseBorders = false;
            this.xrLabelHeaderH3.StylePriority.UseFont = false;
            this.xrLabelHeaderH3.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH3.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH3.Text = "Holiday \r\n(day)";
            this.xrLabelHeaderH3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW3
            // 
            this.xrLabelHeaderW3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW3.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW3.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW3.LocationFloat = new DevExpress.Utils.PointFloat(1115.174F, 34.85727F);
            this.xrLabelHeaderW3.Multiline = true;
            this.xrLabelHeaderW3.Name = "xrLabelHeaderW3";
            this.xrLabelHeaderW3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW3.SizeF = new System.Drawing.SizeF(59.81592F, 53.40619F);
            this.xrLabelHeaderW3.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW3.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW3.StylePriority.UseBorders = false;
            this.xrLabelHeaderW3.StylePriority.UseFont = false;
            this.xrLabelHeaderW3.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW3.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW3.Text = "Weekend(day)";
            this.xrLabelHeaderW3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND3
            // 
            this.xrLabelHeaderND3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND3.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND3.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND3.LocationFloat = new DevExpress.Utils.PointFloat(1071.2F, 34.85146F);
            this.xrLabelHeaderND3.Multiline = true;
            this.xrLabelHeaderND3.Name = "xrLabelHeaderND3";
            this.xrLabelHeaderND3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND3.SizeF = new System.Drawing.SizeF(43.97375F, 53.40657F);
            this.xrLabelHeaderND3.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND3.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND3.StylePriority.UseBorders = false;
            this.xrLabelHeaderND3.StylePriority.UseFont = false;
            this.xrLabelHeaderND3.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND3.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND3.Text = "Normal (day)";
            this.xrLabelHeaderND3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTFebuary
            // 
            this.xrLabelOTFebuary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTFebuary.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTFebuary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTFebuary.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTFebuary.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTFebuary.LocationFloat = new DevExpress.Utils.PointFloat(759.713F, 0.001556397F);
            this.xrLabelOTFebuary.Name = "xrLabelOTFebuary";
            this.xrLabelOTFebuary.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTFebuary.SizeF = new System.Drawing.SizeF(311.487F, 34.84989F);
            this.xrLabelOTFebuary.StylePriority.UseBackColor = false;
            this.xrLabelOTFebuary.StylePriority.UseBorderColor = false;
            this.xrLabelOTFebuary.StylePriority.UseBorders = false;
            this.xrLabelOTFebuary.StylePriority.UseFont = false;
            this.xrLabelOTFebuary.StylePriority.UseForeColor = false;
            this.xrLabelOTFebuary.StylePriority.UseTextAlignment = false;
            this.xrLabelOTFebuary.Text = "Febuary";
            this.xrLabelOTFebuary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH2
            // 
            this.xrLabelHeaderNH2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH2.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH2.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH2.LocationFloat = new DevExpress.Utils.PointFloat(1016.29F, 34.85146F);
            this.xrLabelHeaderNH2.Multiline = true;
            this.xrLabelHeaderNH2.Name = "xrLabelHeaderNH2";
            this.xrLabelHeaderNH2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH2.SizeF = new System.Drawing.SizeF(54.90979F, 53.40659F);
            this.xrLabelHeaderNH2.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH2.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH2.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH2.StylePriority.UseFont = false;
            this.xrLabelHeaderNH2.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH2.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH2.Text = "Holiday (night)";
            this.xrLabelHeaderNH2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW2
            // 
            this.xrLabelHeaderNW2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW2.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW2.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW2.LocationFloat = new DevExpress.Utils.PointFloat(958.2723F, 34.85651F);
            this.xrLabelHeaderNW2.Multiline = true;
            this.xrLabelHeaderNW2.Name = "xrLabelHeaderNW2";
            this.xrLabelHeaderNW2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW2.SizeF = new System.Drawing.SizeF(58.01801F, 53.40696F);
            this.xrLabelHeaderNW2.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW2.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW2.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW2.StylePriority.UseFont = false;
            this.xrLabelHeaderNW2.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW2.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW2.Text = "Weekend (night)";
            this.xrLabelHeaderNW2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND2
            // 
            this.xrLabelHeaderNND2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND2.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND2.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND2.LocationFloat = new DevExpress.Utils.PointFloat(914.3502F, 34.85177F);
            this.xrLabelHeaderNND2.Multiline = true;
            this.xrLabelHeaderNND2.Name = "xrLabelHeaderNND2";
            this.xrLabelHeaderNND2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND2.SizeF = new System.Drawing.SizeF(43.922F, 53.4066F);
            this.xrLabelHeaderNND2.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND2.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND2.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND2.StylePriority.UseFont = false;
            this.xrLabelHeaderNND2.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND2.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND2.Text = "Normal (night)";
            this.xrLabelHeaderNND2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH2
            // 
            this.xrLabelHeaderH2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH2.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH2.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH2.LocationFloat = new DevExpress.Utils.PointFloat(863.6051F, 34.85686F);
            this.xrLabelHeaderH2.Multiline = true;
            this.xrLabelHeaderH2.Name = "xrLabelHeaderH2";
            this.xrLabelHeaderH2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH2.SizeF = new System.Drawing.SizeF(50.74512F, 53.40657F);
            this.xrLabelHeaderH2.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH2.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH2.StylePriority.UseBorders = false;
            this.xrLabelHeaderH2.StylePriority.UseFont = false;
            this.xrLabelHeaderH2.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH2.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH2.Text = "Holiday (day)";
            this.xrLabelHeaderH2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW2
            // 
            this.xrLabelHeaderW2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW2.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW2.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW2.LocationFloat = new DevExpress.Utils.PointFloat(803.7892F, 34.85339F);
            this.xrLabelHeaderW2.Multiline = true;
            this.xrLabelHeaderW2.Name = "xrLabelHeaderW2";
            this.xrLabelHeaderW2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW2.SizeF = new System.Drawing.SizeF(59.81592F, 53.40657F);
            this.xrLabelHeaderW2.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW2.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW2.StylePriority.UseBorders = false;
            this.xrLabelHeaderW2.StylePriority.UseFont = false;
            this.xrLabelHeaderW2.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW2.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW2.Text = "Weekend(day)";
            this.xrLabelHeaderW2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND2
            // 
            this.xrLabelHeaderND2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND2.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND2.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND2.LocationFloat = new DevExpress.Utils.PointFloat(759.7128F, 34.85651F);
            this.xrLabelHeaderND2.Multiline = true;
            this.xrLabelHeaderND2.Name = "xrLabelHeaderND2";
            this.xrLabelHeaderND2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND2.SizeF = new System.Drawing.SizeF(43.97369F, 53.40694F);
            this.xrLabelHeaderND2.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND2.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND2.StylePriority.UseBorders = false;
            this.xrLabelHeaderND2.StylePriority.UseFont = false;
            this.xrLabelHeaderND2.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND2.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND2.Text = "Normal (day)";
            this.xrLabelHeaderND2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderTotals
            // 
            this.xrLabelHeaderTotals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderTotals.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderTotals.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabelHeaderTotals.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderTotals.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderTotals.LocationFloat = new DevExpress.Utils.PointFloat(4190.853F, 0.007756551F);
            this.xrLabelHeaderTotals.Name = "xrLabelHeaderTotals";
            this.xrLabelHeaderTotals.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderTotals.SizeF = new System.Drawing.SizeF(205.6934F, 34.84058F);
            this.xrLabelHeaderTotals.StylePriority.UseBackColor = false;
            this.xrLabelHeaderTotals.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderTotals.StylePriority.UseBorders = false;
            this.xrLabelHeaderTotals.StylePriority.UseFont = false;
            this.xrLabelHeaderTotals.StylePriority.UseForeColor = false;
            this.xrLabelHeaderTotals.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderTotals.Text = "Totals of OT";
            this.xrLabelHeaderTotals.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelOTJanuary
            // 
            this.xrLabelOTJanuary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelOTJanuary.BorderColor = System.Drawing.Color.White;
            this.xrLabelOTJanuary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabelOTJanuary.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelOTJanuary.ForeColor = System.Drawing.Color.White;
            this.xrLabelOTJanuary.LocationFloat = new DevExpress.Utils.PointFloat(448.2257F, 0F);
            this.xrLabelOTJanuary.Name = "xrLabelOTJanuary";
            this.xrLabelOTJanuary.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelOTJanuary.SizeF = new System.Drawing.SizeF(311.4871F, 34.85221F);
            this.xrLabelOTJanuary.StylePriority.UseBackColor = false;
            this.xrLabelOTJanuary.StylePriority.UseBorderColor = false;
            this.xrLabelOTJanuary.StylePriority.UseBorders = false;
            this.xrLabelOTJanuary.StylePriority.UseFont = false;
            this.xrLabelOTJanuary.StylePriority.UseForeColor = false;
            this.xrLabelOTJanuary.StylePriority.UseTextAlignment = false;
            this.xrLabelOTJanuary.Text = "January";
            this.xrLabelOTJanuary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNo
            // 
            this.xrLabelHeaderNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNo.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNo.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNo.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNo.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNo.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
            this.xrLabelHeaderNo.Name = "xrLabelHeaderNo";
            this.xrLabelHeaderNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNo.SizeF = new System.Drawing.SizeF(50.38144F, 88.26343F);
            this.xrLabelHeaderNo.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNo.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNo.StylePriority.UseBorders = false;
            this.xrLabelHeaderNo.StylePriority.UseFont = false;
            this.xrLabelHeaderNo.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNo.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNo.Text = "No.";
            this.xrLabelHeaderNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTTotalDayAndNight
            // 
            this.xrLabelHeaderOTTotalDayAndNight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTTotalDayAndNight.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalDayAndNight.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTTotalDayAndNight.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTTotalDayAndNight.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalDayAndNight.LocationFloat = new DevExpress.Utils.PointFloat(4311.125F, 34.86507F);
            this.xrLabelHeaderOTTotalDayAndNight.Name = "xrLabelHeaderOTTotalDayAndNight";
            this.xrLabelHeaderOTTotalDayAndNight.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTTotalDayAndNight.SizeF = new System.Drawing.SizeF(85.42139F, 53.37044F);
            this.xrLabelHeaderOTTotalDayAndNight.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTTotalDayAndNight.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTTotalDayAndNight.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTTotalDayAndNight.StylePriority.UseFont = false;
            this.xrLabelHeaderOTTotalDayAndNight.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTTotalDayAndNight.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTTotalDayAndNight.Text = "Day & Night";
            this.xrLabelHeaderOTTotalDayAndNight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTTotalNight
            // 
            this.xrLabelHeaderOTTotalNight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTTotalNight.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalNight.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTTotalNight.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTTotalNight.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalNight.LocationFloat = new DevExpress.Utils.PointFloat(4254.052F, 34.86507F);
            this.xrLabelHeaderOTTotalNight.Name = "xrLabelHeaderOTTotalNight";
            this.xrLabelHeaderOTTotalNight.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTTotalNight.SizeF = new System.Drawing.SizeF(57.07275F, 53.37041F);
            this.xrLabelHeaderOTTotalNight.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTTotalNight.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTTotalNight.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTTotalNight.StylePriority.UseFont = false;
            this.xrLabelHeaderOTTotalNight.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTTotalNight.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTTotalNight.Text = "Night";
            this.xrLabelHeaderOTTotalNight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTTotalDay
            // 
            this.xrLabelHeaderOTTotalDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTTotalDay.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalDay.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTTotalDay.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTTotalDay.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTTotalDay.LocationFloat = new DevExpress.Utils.PointFloat(4189.854F, 34.86507F);
            this.xrLabelHeaderOTTotalDay.Name = "xrLabelHeaderOTTotalDay";
            this.xrLabelHeaderOTTotalDay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTTotalDay.SizeF = new System.Drawing.SizeF(64.19775F, 53.37041F);
            this.xrLabelHeaderOTTotalDay.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTTotalDay.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTTotalDay.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTTotalDay.StylePriority.UseFont = false;
            this.xrLabelHeaderOTTotalDay.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTTotalDay.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTTotalDay.Text = "Day";
            this.xrLabelHeaderOTTotalDay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNH1
            // 
            this.xrLabelHeaderNH1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNH1.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNH1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNH1.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNH1.LocationFloat = new DevExpress.Utils.PointFloat(704.8032F, 34.85184F);
            this.xrLabelHeaderNH1.Multiline = true;
            this.xrLabelHeaderNH1.Name = "xrLabelHeaderNH1";
            this.xrLabelHeaderNH1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNH1.SizeF = new System.Drawing.SizeF(54.90979F, 53.41151F);
            this.xrLabelHeaderNH1.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNH1.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNH1.StylePriority.UseBorders = false;
            this.xrLabelHeaderNH1.StylePriority.UseFont = false;
            this.xrLabelHeaderNH1.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNH1.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNH1.Text = "Holiday\r\n (night)";
            this.xrLabelHeaderNH1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNW1
            // 
            this.xrLabelHeaderNW1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNW1.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNW1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNW1.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNW1.LocationFloat = new DevExpress.Utils.PointFloat(646.7851F, 34.85689F);
            this.xrLabelHeaderNW1.Multiline = true;
            this.xrLabelHeaderNW1.Name = "xrLabelHeaderNW1";
            this.xrLabelHeaderNW1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNW1.SizeF = new System.Drawing.SizeF(58.01807F, 53.40646F);
            this.xrLabelHeaderNW1.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNW1.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNW1.StylePriority.UseBorders = false;
            this.xrLabelHeaderNW1.StylePriority.UseFont = false;
            this.xrLabelHeaderNW1.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNW1.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNW1.Text = "Weekend (night)";
            this.xrLabelHeaderNW1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderNND1
            // 
            this.xrLabelHeaderNND1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderNND1.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderNND1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderNND1.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderNND1.LocationFloat = new DevExpress.Utils.PointFloat(602.863F, 34.85219F);
            this.xrLabelHeaderNND1.Multiline = true;
            this.xrLabelHeaderNND1.Name = "xrLabelHeaderNND1";
            this.xrLabelHeaderNND1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderNND1.SizeF = new System.Drawing.SizeF(43.922F, 53.41126F);
            this.xrLabelHeaderNND1.StylePriority.UseBackColor = false;
            this.xrLabelHeaderNND1.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderNND1.StylePriority.UseBorders = false;
            this.xrLabelHeaderNND1.StylePriority.UseFont = false;
            this.xrLabelHeaderNND1.StylePriority.UseForeColor = false;
            this.xrLabelHeaderNND1.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderNND1.Text = "Normal (night)";
            this.xrLabelHeaderNND1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderH1
            // 
            this.xrLabelHeaderH1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderH1.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderH1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderH1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderH1.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderH1.LocationFloat = new DevExpress.Utils.PointFloat(552.118F, 34.85724F);
            this.xrLabelHeaderH1.Multiline = true;
            this.xrLabelHeaderH1.Name = "xrLabelHeaderH1";
            this.xrLabelHeaderH1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderH1.SizeF = new System.Drawing.SizeF(50.74512F, 53.40612F);
            this.xrLabelHeaderH1.StylePriority.UseBackColor = false;
            this.xrLabelHeaderH1.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderH1.StylePriority.UseBorders = false;
            this.xrLabelHeaderH1.StylePriority.UseFont = false;
            this.xrLabelHeaderH1.StylePriority.UseForeColor = false;
            this.xrLabelHeaderH1.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderH1.Text = "Holiday (day)";
            this.xrLabelHeaderH1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderW1
            // 
            this.xrLabelHeaderW1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderW1.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderW1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderW1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderW1.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderW1.LocationFloat = new DevExpress.Utils.PointFloat(492.3021F, 34.85378F);
            this.xrLabelHeaderW1.Multiline = true;
            this.xrLabelHeaderW1.Name = "xrLabelHeaderW1";
            this.xrLabelHeaderW1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderW1.SizeF = new System.Drawing.SizeF(59.81598F, 53.40962F);
            this.xrLabelHeaderW1.StylePriority.UseBackColor = false;
            this.xrLabelHeaderW1.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderW1.StylePriority.UseBorders = false;
            this.xrLabelHeaderW1.StylePriority.UseFont = false;
            this.xrLabelHeaderW1.StylePriority.UseForeColor = false;
            this.xrLabelHeaderW1.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderW1.Text = "Weekend (day)";
            this.xrLabelHeaderW1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderND1
            // 
            this.xrLabelHeaderND1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderND1.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderND1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderND1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderND1.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderND1.LocationFloat = new DevExpress.Utils.PointFloat(448.2257F, 34.85689F);
            this.xrLabelHeaderND1.Multiline = true;
            this.xrLabelHeaderND1.Name = "xrLabelHeaderND1";
            this.xrLabelHeaderND1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderND1.SizeF = new System.Drawing.SizeF(43.97375F, 53.40651F);
            this.xrLabelHeaderND1.StylePriority.UseBackColor = false;
            this.xrLabelHeaderND1.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderND1.StylePriority.UseBorders = false;
            this.xrLabelHeaderND1.StylePriority.UseFont = false;
            this.xrLabelHeaderND1.StylePriority.UseForeColor = false;
            this.xrLabelHeaderND1.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderND1.Text = "Normal (day)";
            this.xrLabelHeaderND1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderPosition
            // 
            this.xrLabelHeaderPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderPosition.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderPosition.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderPosition.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderPosition.LocationFloat = new DevExpress.Utils.PointFloat(298.8873F, 0.001557668F);
            this.xrLabelHeaderPosition.Name = "xrLabelHeaderPosition";
            this.xrLabelHeaderPosition.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderPosition.SizeF = new System.Drawing.SizeF(149.3385F, 88.26186F);
            this.xrLabelHeaderPosition.StylePriority.UseBackColor = false;
            this.xrLabelHeaderPosition.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderPosition.StylePriority.UseBorders = false;
            this.xrLabelHeaderPosition.StylePriority.UseFont = false;
            this.xrLabelHeaderPosition.StylePriority.UseForeColor = false;
            this.xrLabelHeaderPosition.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderPosition.Text = "Position";
            this.xrLabelHeaderPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderOTRestTotal
            // 
            this.xrLabelHeaderOTRestTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderOTRestTotal.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTRestTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderOTRestTotal.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderOTRestTotal.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderOTRestTotal.LocationFloat = new DevExpress.Utils.PointFloat(4496.578F, 0F);
            this.xrLabelHeaderOTRestTotal.Name = "xrLabelHeaderOTRestTotal";
            this.xrLabelHeaderOTRestTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderOTRestTotal.SizeF = new System.Drawing.SizeF(99.42236F, 88.22772F);
            this.xrLabelHeaderOTRestTotal.StylePriority.UseBackColor = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseBorders = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseFont = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseForeColor = false;
            this.xrLabelHeaderOTRestTotal.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderOTRestTotal.Text = "Rest of OT Totals";
            this.xrLabelHeaderOTRestTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderFullName
            // 
            this.xrLabelHeaderFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderFullName.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderFullName.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderFullName.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderFullName.LocationFloat = new DevExpress.Utils.PointFloat(151.3746F, 0.0007311503F);
            this.xrLabelHeaderFullName.Name = "xrLabelHeaderFullName";
            this.xrLabelHeaderFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderFullName.SizeF = new System.Drawing.SizeF(147.5127F, 88.26268F);
            this.xrLabelHeaderFullName.StylePriority.UseBackColor = false;
            this.xrLabelHeaderFullName.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderFullName.StylePriority.UseBorders = false;
            this.xrLabelHeaderFullName.StylePriority.UseFont = false;
            this.xrLabelHeaderFullName.StylePriority.UseForeColor = false;
            this.xrLabelHeaderFullName.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderFullName.Text = "Full Name";
            this.xrLabelHeaderFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabelHeaderEmpCode
            // 
            this.xrLabelHeaderEmpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.xrLabelHeaderEmpCode.BorderColor = System.Drawing.Color.White;
            this.xrLabelHeaderEmpCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelHeaderEmpCode.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabelHeaderEmpCode.ForeColor = System.Drawing.Color.White;
            this.xrLabelHeaderEmpCode.LocationFloat = new DevExpress.Utils.PointFloat(50.38145F, 0.001207987F);
            this.xrLabelHeaderEmpCode.Name = "xrLabelHeaderEmpCode";
            this.xrLabelHeaderEmpCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHeaderEmpCode.SizeF = new System.Drawing.SizeF(100.9932F, 88.26221F);
            this.xrLabelHeaderEmpCode.StylePriority.UseBackColor = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseBorderColor = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseBorders = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseFont = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseForeColor = false;
            this.xrLabelHeaderEmpCode.StylePriority.UseTextAlignment = false;
            this.xrLabelHeaderEmpCode.Text = "Code";
            this.xrLabelHeaderEmpCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageFooterLeftText,
            this.xrPageFooterRightText});
            this.PageFooter.HeightF = 50.16022F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageFooterLeftText
            // 
            this.xrPageFooterLeftText.LocationFloat = new DevExpress.Utils.PointFloat(0F, 17.16022F);
            this.xrPageFooterLeftText.Name = "xrPageFooterLeftText";
            this.xrPageFooterLeftText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageFooterLeftText.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageFooterLeftText.SizeF = new System.Drawing.SizeF(328.8873F, 23F);
            this.xrPageFooterLeftText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterLeftText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageFooterRightText
            // 
            this.xrPageFooterRightText.Format = "Page {0} of {1}";
            this.xrPageFooterRightText.LocationFloat = new DevExpress.Utils.PointFloat(4236.125F, 17.16022F);
            this.xrPageFooterRightText.Name = "xrPageFooterRightText";
            this.xrPageFooterRightText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageFooterRightText.SizeF = new System.Drawing.SizeF(313F, 23F);
            this.xrPageFooterRightText.StylePriority.UseTextAlignment = false;
            this.xrPageFooterRightText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataAdapter = this.timesheet_sprptOTByYear_DetailsTableAdapter;
            this.DetailReport.DataMember = "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
    "Year_Details";
            this.DetailReport.DataSource = this.yearlyOTDataSet1;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableDetails});
            this.Detail1.HeightF = 26.04167F;
            this.Detail1.Name = "Detail1";
            // 
            // xrTableDetails
            // 
            this.xrTableDetails.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableDetails.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableDetails.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableDetails.Name = "xrTableDetails";
            this.xrTableDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRowDetails});
            this.xrTableDetails.SizeF = new System.Drawing.SizeF(4596F, 26.04167F);
            this.xrTableDetails.StylePriority.UseBorderColor = false;
            this.xrTableDetails.StylePriority.UseBorders = false;
            this.xrTableDetails.StylePriority.UsePadding = false;
            // 
            // xrTableRowDetails
            // 
            this.xrTableRowDetails.BorderColor = System.Drawing.Color.Gray;
            this.xrTableRowDetails.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRowDetails.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellNo,
            this.xrTableCellEmpCode,
            this.xrTableCellFullName,
            this.xrTableCellPosition,
            this.xrLabelND1,
            this.xrTableCellOTWD,
            this.xrTableCellOTHDD,
            this.xrTableCellOTNDN,
            this.xrTableCellOTWN,
            this.xrTableCellOTHN,
            this.xrTableCellOTTotalByMonth,
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth,
            this.xrTableCellOTTotalByYear,
            this.xrTableCellOTRestTotal,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrLabelH3,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell13,
            this.xrTableCell12,
            this.xrLabelH4,
            this.xrLabelNH4,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrLabelH5,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrLabelH6,
            this.xrTableCell28,
            this.xrTableCell27,
            this.xrTableCell26,
            this.xrTableCell29,
            this.xrTableCell25,
            this.xrLabelH7,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell31,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrLabelH8,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell9,
            this.xrTableCell44,
            this.xrLabelH9,
            this.xrTableCell42,
            this.xrTableCell45,
            this.xrTableCell46,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrLabelH10,
            this.xrTableCell51,
            this.xrTableCell49,
            this.xrTableCell59,
            this.xrTableCell60,
            this.xrTableCell52,
            this.xrTableCell61,
            this.xrTableCell40,
            this.xrTableCell62,
            this.xrTableCell58,
            this.xrTableCell53,
            this.xrTableCell57,
            this.xrTableCell50,
            this.xrTableCell64,
            this.xrTableCell63,
            this.xrTableCell65,
            this.xrTableCell56,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell5,
            this.xrTableCell66});
            this.xrTableRowDetails.Name = "xrTableRowDetails";
            this.xrTableRowDetails.StylePriority.UseBorderColor = false;
            this.xrTableRowDetails.StylePriority.UseBorders = false;
            this.xrTableRowDetails.Weight = 1D;
            // 
            // xrTableCellNo
            // 
            this.xrTableCellNo.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Seq")});
            this.xrTableCellNo.Name = "xrTableCellNo";
            this.xrTableCellNo.StylePriority.UseBorderColor = false;
            this.xrTableCellNo.StylePriority.UseBorders = false;
            this.xrTableCellNo.StylePriority.UseTextAlignment = false;
            this.xrTableCellNo.Text = "xrTableCellNo";
            this.xrTableCellNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellNo.Weight = 0.097305349470250838D;
            // 
            // xrTableCellEmpCode
            // 
            this.xrTableCellEmpCode.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellEmpCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellEmpCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.EmpCode")});
            this.xrTableCellEmpCode.Name = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.StylePriority.UseBorderColor = false;
            this.xrTableCellEmpCode.StylePriority.UseBorders = false;
            this.xrTableCellEmpCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellEmpCode.Text = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellEmpCode.Weight = 0.19505546943083579D;
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFullName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.FullName")});
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.StylePriority.UseBorderColor = false;
            this.xrTableCellFullName.StylePriority.UseBorders = false;
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.Text = "xrTableCellFullName";
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellFullName.Weight = 0.284901999524607D;
            // 
            // xrTableCellPosition
            // 
            this.xrTableCellPosition.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPosition.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.PositionName")});
            this.xrTableCellPosition.Name = "xrTableCellPosition";
            this.xrTableCellPosition.StylePriority.UseBorderColor = false;
            this.xrTableCellPosition.StylePriority.UseBorders = false;
            this.xrTableCellPosition.StylePriority.UseTextAlignment = false;
            this.xrTableCellPosition.Text = "xrTableCellPosition";
            this.xrTableCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellPosition.Weight = 0.28842830912800094D;
            // 
            // xrLabelND1
            // 
            this.xrLabelND1.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelND1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabelND1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jan_TotalHoursOfDayOTInNormalDays")});
            this.xrLabelND1.Name = "xrLabelND1";
            this.xrLabelND1.StylePriority.UseBorderColor = false;
            this.xrLabelND1.StylePriority.UseBorders = false;
            this.xrLabelND1.StylePriority.UseTextAlignment = false;
            this.xrLabelND1.Text = "xrLabelND1";
            this.xrLabelND1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelND1.Weight = 0.084929545164029729D;
            // 
            // xrTableCellOTWD
            // 
            this.xrTableCellOTWD.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTWD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTWD.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jan_TotalHoursOfDayOTInWeekend")});
            this.xrTableCellOTWD.Name = "xrTableCellOTWD";
            this.xrTableCellOTWD.StylePriority.UseBorderColor = false;
            this.xrTableCellOTWD.StylePriority.UseBorders = false;
            this.xrTableCellOTWD.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTWD.Text = "xrTableCellOTWD";
            this.xrTableCellOTWD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTWD.Weight = 0.11572507411935182D;
            // 
            // xrTableCellOTHDD
            // 
            this.xrTableCellOTHDD.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTHDD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTHDD.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jan_TotalHoursOfDayOTInHolidays")});
            this.xrTableCellOTHDD.Name = "xrTableCellOTHDD";
            this.xrTableCellOTHDD.StylePriority.UseBorderColor = false;
            this.xrTableCellOTHDD.StylePriority.UseBorders = false;
            this.xrTableCellOTHDD.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTHDD.Text = "xrTableCellOTHDD";
            this.xrTableCellOTHDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTHDD.Weight = 0.098007995355048783D;
            // 
            // xrTableCellOTNDN
            // 
            this.xrTableCellOTNDN.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTNDN.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTNDN.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Details.Jan_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCellOTNDN.Name = "xrTableCellOTNDN";
            this.xrTableCellOTNDN.StylePriority.UseBorderColor = false;
            this.xrTableCellOTNDN.StylePriority.UseBorders = false;
            this.xrTableCellOTNDN.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTNDN.Text = "xrTableCellOTNDN";
            this.xrTableCellOTNDN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTNDN.Weight = 0.084829513184755567D;
            // 
            // xrTableCellOTWN
            // 
            this.xrTableCellOTWN.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTWN.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTWN.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Feb_TotalHoursOfNightOTInWeekend")});
            this.xrTableCellOTWN.Name = "xrTableCellOTWN";
            this.xrTableCellOTWN.StylePriority.UseBorderColor = false;
            this.xrTableCellOTWN.StylePriority.UseBorders = false;
            this.xrTableCellOTWN.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTWN.Text = "xrTableCellOTWN";
            this.xrTableCellOTWN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTWN.Weight = 0.11205466671876846D;
            // 
            // xrTableCellOTHN
            // 
            this.xrTableCellOTHN.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTHN.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTHN.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jan_TotalHoursOfNightOTInHolidays")});
            this.xrTableCellOTHN.Name = "xrTableCellOTHN";
            this.xrTableCellOTHN.StylePriority.UseBorderColor = false;
            this.xrTableCellOTHN.StylePriority.UseBorders = false;
            this.xrTableCellOTHN.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTHN.Text = "xrTableCellOTHN";
            this.xrTableCellOTHN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTHN.Weight = 0.10605118694148286D;
            // 
            // xrTableCellOTTotalByMonth
            // 
            this.xrTableCellOTTotalByMonth.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTTotalByMonth.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTTotalByMonth.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Feb_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCellOTTotalByMonth.Name = "xrTableCellOTTotalByMonth";
            this.xrTableCellOTTotalByMonth.StylePriority.UseBorderColor = false;
            this.xrTableCellOTTotalByMonth.StylePriority.UseBorders = false;
            this.xrTableCellOTTotalByMonth.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTTotalByMonth.Text = "xrTableCellOTTotalByMonth";
            this.xrTableCellOTTotalByMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTTotalByMonth.Weight = 0.084929614232946316D;
            // 
            // xrTableCellOTTotalsFromFirstMonthToSelectedMonth
            // 
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Feb_TotalHoursOfDayOTInWeekend")});
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.Name = "xrTableCellOTTotalsFromFirstMonthToSelectedMonth";
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.StylePriority.UseBorderColor = false;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.StylePriority.UseBorders = false;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.Text = "xrTableCellOTTotalsFromFirstMonthToSelectedMonth";
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTTotalsFromFirstMonthToSelectedMonth.Weight = 0.11572490197267804D;
            // 
            // xrTableCellOTTotalByYear
            // 
            this.xrTableCellOTTotalByYear.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTTotalByYear.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOTTotalByYear.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Details.Feb_TotalHoursOfDayOTInHolidays")});
            this.xrTableCellOTTotalByYear.Name = "xrTableCellOTTotalByYear";
            this.xrTableCellOTTotalByYear.StylePriority.UseBorderColor = false;
            this.xrTableCellOTTotalByYear.StylePriority.UseBorders = false;
            this.xrTableCellOTTotalByYear.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTTotalByYear.Text = "xrTableCellOTTotalByYear";
            this.xrTableCellOTTotalByYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTTotalByYear.Weight = 0.09800812469976572D;
            // 
            // xrTableCellOTRestTotal
            // 
            this.xrTableCellOTRestTotal.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCellOTRestTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Feb_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCellOTRestTotal.Name = "xrTableCellOTRestTotal";
            this.xrTableCellOTRestTotal.StylePriority.UseBorderColor = false;
            this.xrTableCellOTRestTotal.StylePriority.UseTextAlignment = false;
            this.xrTableCellOTRestTotal.Text = "xrTableCellOTRestTotal";
            this.xrTableCellOTRestTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOTRestTotal.Weight = 0.08482963716440578D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Feb_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.1120544386507859D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Feb_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.10605115492236705D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Mar_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.084930188864318745D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Mar_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 0.11552684846066447D;
            // 
            // xrLabelH3
            // 
            this.xrLabelH3.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelH3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Mar_TotalHoursOfDayOTInHolidays")});
            this.xrLabelH3.Name = "xrLabelH3";
            this.xrLabelH3.StylePriority.UseBorderColor = false;
            this.xrLabelH3.StylePriority.UseTextAlignment = false;
            this.xrLabelH3.Text = "xrLabelH3";
            this.xrLabelH3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelH3.Weight = 0.098007514484930919D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Mar_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 0.084829525128092748D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Mar_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorderColor = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.11205452998470025D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Mar_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorderColor = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.10624886626555971D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Apr_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorderColor = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "xrTableCell13";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.084929968679356732D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Apr_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 0.11552710304045638D;
            // 
            // xrLabelH4
            // 
            this.xrLabelH4.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelH4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Apr_TotalHoursOfDayOTInHolidays")});
            this.xrLabelH4.Name = "xrLabelH4";
            this.xrLabelH4.StylePriority.UseBorderColor = false;
            this.xrLabelH4.StylePriority.UseTextAlignment = false;
            this.xrLabelH4.Text = "xrLabelH4";
            this.xrLabelH4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelH4.Weight = 0.098008002771332037D;
            // 
            // xrLabelNH4
            // 
            this.xrLabelNH4.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelNH4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Apr_TotalHoursOfNightOTInNormalDays")});
            this.xrLabelNH4.Name = "xrLabelNH4";
            this.xrLabelNH4.StylePriority.UseBorderColor = false;
            this.xrLabelNH4.StylePriority.UseTextAlignment = false;
            this.xrLabelNH4.Text = "xrLabelNH4";
            this.xrLabelNH4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelNH4.Weight = 0.084829297744667076D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Apr_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorderColor = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "xrTableCell15";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 0.11205500570127008D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Apr_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorderColor = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "xrTableCell16";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell16.Weight = 0.10624886075190798D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.May_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorderColor = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = "xrTableCell17";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell17.Weight = 0.084929490720136946D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.May_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorderColor = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "xrTableCell18";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell18.Weight = 0.11552732456016934D;
            // 
            // xrLabelH5
            // 
            this.xrLabelH5.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelH5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.May_TotalHoursOfDayOTInHolidays")});
            this.xrLabelH5.Name = "xrLabelH5";
            this.xrLabelH5.StylePriority.UseBorderColor = false;
            this.xrLabelH5.StylePriority.UseTextAlignment = false;
            this.xrLabelH5.Text = "xrLabelH5";
            this.xrLabelH5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelH5.Weight = 0.098007516771370234D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.May_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBorderColor = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.Text = "xrTableCell20";
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell20.Weight = 0.0848299977979386D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.May_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBorderColor = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "xrTableCell21";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell21.Weight = 0.11205476631962318D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.May_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorderColor = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.Text = "xrTableCell22";
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell22.Weight = 0.10624838741611489D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jun_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBorderColor = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "xrTableCell23";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell23.Weight = 0.084929489815553483D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jun_TotalHoursOfDayOTInHolidays")});
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseBorderColor = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.Text = "xrTableCell24";
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell24.Weight = 0.11552732411505683D;
            // 
            // xrLabelH6
            // 
            this.xrLabelH6.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelH6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jun_TotalHoursOfDayOTInHolidays")});
            this.xrLabelH6.Name = "xrLabelH6";
            this.xrLabelH6.StylePriority.UseBorderColor = false;
            this.xrLabelH6.StylePriority.UseTextAlignment = false;
            this.xrLabelH6.Text = "xrLabelH6";
            this.xrLabelH6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelH6.Weight = 0.098007755980715636D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jun_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseBorderColor = false;
            this.xrTableCell28.StylePriority.UseTextAlignment = false;
            this.xrTableCell28.Text = "xrTableCell28";
            this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell28.Weight = 0.084830001358838641D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jun_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseBorderColor = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.Text = "xrTableCell27";
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell27.Weight = 0.11205453785041179D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Jun_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseBorderColor = false;
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.Text = "xrTableCell26";
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell26.Weight = 0.10624863050224662D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.July_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseBorderColor = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = "xrTableCell29";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell29.Weight = 0.086308241007683417D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.July_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseBorderColor = false;
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "xrTableCell25";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell25.Weight = 0.11552733880376953D;
            // 
            // xrLabelH7
            // 
            this.xrLabelH7.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelH7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.July_TotalHoursOfDayOTInHolidays")});
            this.xrLabelH7.Name = "xrLabelH7";
            this.xrLabelH7.StylePriority.UseBorderColor = false;
            this.xrLabelH7.StylePriority.UseTextAlignment = false;
            this.xrLabelH7.Text = "xrLabelH7";
            this.xrLabelH7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelH7.Weight = 0.098007667569473239D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.July_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseBorderColor = false;
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.Text = "xrTableCell32";
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell32.Weight = 0.084829056467702363D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.July_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseBorderColor = false;
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            this.xrTableCell33.Text = "xrTableCell33";
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell33.Weight = 0.11205594691663864D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.July_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseBorderColor = false;
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "xrTableCell31";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell31.Weight = 0.10624719751139325D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Aug_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBorderColor = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "xrTableCell34";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell34.Weight = 0.084929955798101586D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Aug_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorderColor = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.Text = "xrTableCell35";
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell35.Weight = 0.11552637828258619D;
            // 
            // xrLabelH8
            // 
            this.xrLabelH8.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelH8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Aug_TotalHoursOfDayOTInHolidays")});
            this.xrLabelH8.Name = "xrLabelH8";
            this.xrLabelH8.StylePriority.UseBorderColor = false;
            this.xrLabelH8.StylePriority.UseTextAlignment = false;
            this.xrLabelH8.Text = "xrLabelH8";
            this.xrLabelH8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelH8.Weight = 0.098008222449144131D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Aug_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBorderColor = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.Text = "xrTableCell37";
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell37.Weight = 0.0848299969918559D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Aug_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseBorderColor = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "xrTableCell38";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell38.Weight = 0.11205594473314727D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Aug_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseBorderColor = false;
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            this.xrTableCell39.Text = "xrTableCell39";
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell39.Weight = 0.1062476797518512D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Sep_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorderColor = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.086308233648113908D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Sep_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseBorderColor = false;
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.Text = "xrTableCell44";
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell44.Weight = 0.11552779561929055D;
            // 
            // xrLabelH9
            // 
            this.xrLabelH9.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelH9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Sep_TotalHoursOfDayOTInHolidays")});
            this.xrLabelH9.Name = "xrLabelH9";
            this.xrLabelH9.StylePriority.UseBorderColor = false;
            this.xrLabelH9.StylePriority.UseTextAlignment = false;
            this.xrLabelH9.Text = "xrLabelH9";
            this.xrLabelH9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelH9.Weight = 0.098007280762253679D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Sep_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseBorderColor = false;
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.Text = "xrTableCell42";
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell42.Weight = 0.08482999766700286D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Sep_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseBorderColor = false;
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.Text = "xrTableCell45";
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell45.Weight = 0.1120550020174684D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Sep_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseBorderColor = false;
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.Text = "xrTableCell46";
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell46.Weight = 0.1062476800934417D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Oct_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseBorderColor = false;
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.Text = "xrTableCell47";
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell47.Weight = 0.085063874887648078D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Oct_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseBorderColor = false;
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.Text = "xrTableCell48";
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell48.Weight = 0.11552685257206369D;
            // 
            // xrLabelH10
            // 
            this.xrLabelH10.BorderColor = System.Drawing.SystemColors.Control;
            this.xrLabelH10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Oct_TotalHoursOfDayOTInHolidays")});
            this.xrLabelH10.Name = "xrLabelH10";
            this.xrLabelH10.StylePriority.UseBorderColor = false;
            this.xrLabelH10.StylePriority.UseTextAlignment = false;
            this.xrLabelH10.Text = "xrLabelH10";
            this.xrLabelH10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabelH10.Weight = 0.098008223815506029D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Oct_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseBorderColor = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.Text = "xrTableCell51";
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell51.Weight = 0.084829526140376671D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Oct_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseBorderColor = false;
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.Text = "xrTableCell49";
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell49.Weight = 0.11205453785844574D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Oct_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.StylePriority.UseBorderColor = false;
            this.xrTableCell59.StylePriority.UseTextAlignment = false;
            this.xrTableCell59.Text = "xrTableCell59";
            this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell59.Weight = 0.10779517522354083D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Nov_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.StylePriority.UseBorderColor = false;
            this.xrTableCell60.StylePriority.UseTextAlignment = false;
            this.xrTableCell60.Text = "xrTableCell60";
            this.xrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell60.Weight = 0.084691844061384622D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Nov_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseBorderColor = false;
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.Text = "xrTableCell52";
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell52.Weight = 0.11552732409266438D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Oct_TotalHoursOfDayOTInHolidays")});
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.StylePriority.UseBorderColor = false;
            this.xrTableCell61.StylePriority.UseTextAlignment = false;
            this.xrTableCell61.Text = "xrTableCell61";
            this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell61.Weight = 0.098008223823540047D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Nov_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseBorderColor = false;
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            this.xrTableCell40.Text = "xrTableCell40";
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell40.Weight = 0.0848290546217845D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Nov_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.StylePriority.UseBorderColor = false;
            this.xrTableCell62.StylePriority.UseTextAlignment = false;
            this.xrTableCell62.Text = "xrTableCell62";
            this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell62.Weight = 0.11205594507072075D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Nov_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.StylePriority.UseBorderColor = false;
            this.xrTableCell58.StylePriority.UseTextAlignment = false;
            this.xrTableCell58.Text = "xrTableCell58";
            this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell58.Weight = 0.10624720855476452D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Dec_TotalHoursOfDayOTInNormalDays")});
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.StylePriority.UseBorderColor = false;
            this.xrTableCell53.StylePriority.UseTextAlignment = false;
            this.xrTableCell53.Text = "xrTableCell53";
            this.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell53.Weight = 0.084929489785127016D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Dec_TotalHoursOfDayOTInWeekend")});
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.StylePriority.UseBorderColor = false;
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            this.xrTableCell57.Text = "xrTableCell57";
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell57.Weight = 0.11745918340730052D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Dec_TotalHoursOfDayOTInHolidays")});
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseBorderColor = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.Text = "xrTableCell50";
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell50.Weight = 0.098007755984732617D;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Dec_TotalHoursOfNightOTInNormalDays")});
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.StylePriority.UseBorderColor = false;
            this.xrTableCell64.StylePriority.UseTextAlignment = false;
            this.xrTableCell64.Text = "xrTableCell64";
            this.xrTableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell64.Weight = 0.084828586782977083D;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Dec_TotalHoursOfNightOTInWeekend")});
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.StylePriority.UseBorderColor = false;
            this.xrTableCell63.StylePriority.UseTextAlignment = false;
            this.xrTableCell63.Text = "xrTableCell63";
            this.xrTableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell63.Weight = 0.11205500570528716D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.Dec_TotalHoursOfNightOTInHolidays")});
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.StylePriority.UseBorderColor = false;
            this.xrTableCell65.StylePriority.UseTextAlignment = false;
            this.xrTableCell65.Text = "xrTableCell65";
            this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell65.Weight = 0.10742508576281712D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.TotalHoursOfDayOT")});
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseBorderColor = false;
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            this.xrTableCell56.Text = "xrTableCell56";
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell56.Weight = 0.12398981244469862D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.TotalHoursOfNightOT")});
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseBorderColor = false;
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.Text = "xrTableCell54";
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell54.Weight = 0.12375534582983014D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.TotalHoursOfDayAndNightOT")});
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.StylePriority.UseBorderColor = false;
            this.xrTableCell55.StylePriority.UseTextAlignment = false;
            this.xrTableCell55.Text = "xrTableCell55";
            this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell55.Weight = 0.15145530630744408D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.TotalHoursOfDefaultYearOT")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.1931966953597743D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.BorderColor = System.Drawing.SystemColors.Control;
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Timesheet_sprptOTByYear_Master.Timesheet_sprptOTByYear_Master_Timesheet_sprptOTBy" +
                    "Year_Details.OTRestTotal")});
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseBorderColor = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.Text = "xrTableCell66";
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell66.Weight = 0.19202203362219103D;
            // 
            // timesheet_sprptOTByYear_DetailsTableAdapter
            // 
            this.timesheet_sprptOTByYear_DetailsTableAdapter.ClearBeforeFill = true;
            // 
            // yearlyOTDataSet1
            // 
            this.yearlyOTDataSet1.DataSetName = "YearlyOTDataSet";
            this.yearlyOTDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // timesheet_sprptOTByYear_MasterTableAdapter
            // 
            this.timesheet_sprptOTByYear_MasterTableAdapter.ClearBeforeFill = true;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.Name = "xrControlStyle2";
            this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.ImageUrl = "/Content/Admin/images/logo_report.png";
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(600.6667F, 125F);
            this.xrPictureBoxLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // OTByYearXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.DetailReport});
            this.DataAdapter = this.timesheet_sprptOTByYear_MasterTableAdapter;
            this.DataMember = "Timesheet_sprptOTByYear_Master";
            this.DataSource = this.yearlyOTDataSet1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 4, 208, 25);
            this.PageHeight = 4596;
            this.PageWidth = 4700;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2});
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTableCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearlyOTDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
