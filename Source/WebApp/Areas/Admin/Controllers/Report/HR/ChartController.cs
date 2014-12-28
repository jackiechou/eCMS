using Eagle.Common.Entities;
using Eagle.Repository.Report.HR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.Report.HR.Department;

namespace Eagle.WebApp.Areas.Admin.Controllers.Report.HR
{
    public class ChartController : BaseController
    {
        #region CONTRACT CHART ============================================================================================================================================
        public JsonResult Contract_GetPieChartDataByPercent(int? LSCompanyID)
        {
            DataSet DS = HRReportRespository.GetData_Contract(LSCompanyID, LanguageId);
            List<ChartInfo> lst = new List<ChartInfo>();
            if (DS.Tables.Count > 0)
            {
                DataTable dt = DS.Tables[0];
                int rowCounts = dt.Rows.Count;
                string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                for (int i = 0; i < rowCounts; i++)
                {
                    string strName = dt.Rows[i]["ContractTypeName"].ToString();
                    string TotalPercentByLSContractTypeID = dt.Rows[i]["TotalPercentByLSContractTypeID"].ToString();
                    lst.Add(new ChartInfo() { label = strName, data = TotalPercentByLSContractTypeID, color = colorArray[i] });
                }
            }
            return base.Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Contract_GetPieChartData(int? LSCompanyID)
        {
            DataSet DS = HRReportRespository.GetData_Contract(LSCompanyID, LanguageId);
            List<ChartInfo> lst = new List<ChartInfo>();
            if (DS.Tables.Count > 0)
            {
                DataTable dt = DS.Tables[0];
                int rowCounts = dt.Rows.Count;
                string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                for (int i = 0; i < rowCounts; i++)
                {
                    string strName = dt.Rows[i]["ContractTypeName"].ToString();
                    string TotalPercentByLSContractTypeID = dt.Rows[i]["TotalContractsByLSContractTypeID"].ToString();
                    lst.Add(new ChartInfo() { label = strName, data = TotalPercentByLSContractTypeID, color = colorArray[i] });
                }
            }
            return base.Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Contract_GetPieChartDataAllMonths()
        {
            List<LineChartInfo> lst = new List<LineChartInfo>();

            DataTable dtYears = HRReportRespository.GetMonthlyDigitDataWithPivot();
            if (dtYears.Rows.Count > 0)
            {

                Dictionary<int, string> dict = new Dictionary<int, string>();
                List<int> yearList = new List<int>();

                for (int i = 0; i < dtYears.Rows.Count; i++)
                {
                    yearList.Add(Convert.ToInt32(dtYears.Rows[i]["Year"].ToString()));
                }

                string data = string.Empty, lines = string.Empty, points = string.Empty, symbol = string.Empty, color = string.Empty, year = string.Empty;
                for (int x = 0; x < yearList.Count; x++)
                {
                    year = yearList[x].ToString();
                    DataTable table = HRReportRespository.GetMonthlyDigitDataWithUnPivotByYear(yearList[x]);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append("[");
                        sb2.Append(string.Format("{0},{1}", table.Rows[i]["Col"].ToString(), table.Rows[i]["Value"].ToString()));
                        sb2.Append("]");

                        if (i != 0 && i < table.Rows.Count)
                            sb.Append(",");

                        sb.Append(sb2);
                    }
                    sb.Insert(0, "[");
                    sb.Append("]");
                    data = sb.ToString();

                    //if (item != yearList.First() || item != yearList.Last())
                    //    sb.Append(",");    
                    int rowCounts = yearList.Count;
                    string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                    color = colorArray[x].ToString();

                    string[] shapeArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomShapeStrings(rowCounts);
                    symbol = shapeArray[x].ToString();

                    //StringBuilder sbPoints = new StringBuilder();
                    //sbPoints.Append(string.Format("{0}:{1}", "show", "true"));
                    //sbPoints.Append(",");
                    //sbPoints.Append(string.Format("{0}:'{1}'", "symbol", symbol));                   
                    //sbPoints.Append(",");
                    //sbPoints.Append(string.Format("{0}:'{1}'", "fillColor", color));                  
                    //sbPoints.Insert(0, "{");
                    //sbPoints.Append("}");
                    //points = sbPoints.ToString();

                    //StringBuilder sbLines = new StringBuilder();
                    //sbLines.Append(string.Format("{0}:{1}", "show", "true"));
                    //sbLines.Insert(0, "{");
                    //sbLines.Append("}");
                    //lines = sbLines.ToString();

                    ChartPoint chartPoint = new ChartPoint();
                    chartPoint.show = true;
                    chartPoint.symbol = symbol;
                    chartPoint.fillColor = color;

                    ChartLine chartLine = new ChartLine();
                    chartLine.show = true;

                    lst.Add(new LineChartInfo() { label = year, data = sb.ToString(), lines = chartLine, points = chartPoint, color = color });
                }
            }
            JsonResult jsonResult = base.Json(lst, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }


        //public JsonResult Contract_GetPieChartDataAllMonths()
        //{
        //List<LS_tblContractTypeViewModel> lst_type = LS_tblContractTypeRespository.GetList();
        //Dictionary<string, string> dict = new Dictionary<string, string>();
        //foreach (var item in lst_type)
        //{
        //    dict.Add(item.Name, )
        //}

        //    int LSContractTypeID = 1;
        //    DataSet DS = ContractRespository.GetListByLSContractTypeID(LSContractTypeID);
        //    List<ChartInfo> lst = new List<ChartInfo>();
        //    if (DS.Tables.Count > 0)
        //    {
        //        DataTable dt = DS.Tables[0];
        //        int rowCounts = dt.Rows.Count;
        //        string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
        //        for (int i = 0; i < rowCounts; i++)
        //        {
        //            string strName = dt.Rows[i]["Name"].ToString();
        //            double TotalPercentByLSContractTypeID = Convert.ToDouble(dt.Rows[i]["TotalContractsByLSContractTypeID"].ToString());
        //            lst.Add(new ChartInfo() { label = strName, data = TotalPercentByLSContractTypeID, color = colorArray[i] });
        //        }
        //    }
        //    return base.Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //data: [[1, 12], [2, 4], [3, 5], [4, 25], [5, 16]]
        #endregion CHART ==========================================================================================================================================

        #region CHART BY DEPARTMENT ============================================================================================================================================

        //public JsonResult Department_GetChartDataByDepartmentMonthlyWithPivot()
        //{
        //    List<LineChartInfo> lst = new List<LineChartInfo>();

        //    DataTable dtYears = HRReportRespository.GetListByDepartmentMonthlyWithPivot();
        //    if (dtYears.Rows.Count > 0)
        //    {

        //        Dictionary<int, string> dict = new Dictionary<int, string>();
        //        List<int> yearList = new List<int>();

        //        for (int i = 0; i < dtYears.Rows.Count; i++)
        //        {
        //            yearList.Add(Convert.ToInt32(dtYears.Rows[i]["Year"].ToString()));
        //        }

        //        string data = string.Empty, lines = string.Empty, points = string.Empty, symbol = string.Empty, color = string.Empty, year = string.Empty;
        //        for (int x = 0; x < yearList.Count; x++)
        //        {
        //            year = yearList[x].ToString();
        //            DataTable table = HRReportRespository.GetMonthlyDigitDataWithUnPivotByYear(yearList[x]);
        //            StringBuilder sb = new StringBuilder();
        //            for (int i = 0; i < table.Rows.Count; i++)
        //            {
        //                StringBuilder sb2 = new StringBuilder();
        //                sb2.Append("[");
        //                sb2.Append(string.Format("{0},{1}", table.Rows[i]["Col"].ToString(), table.Rows[i]["Value"].ToString()));
        //                sb2.Append("]");

        //                if (i != 0 && i < table.Rows.Count)
        //                    sb.Append(",");

        //                sb.Append(sb2);
        //            }
        //            sb.Insert(0, "[");
        //            sb.Append("]");
        //            data = sb.ToString();

        //            //if (item != yearList.First() || item != yearList.Last())
        //            //    sb.Append(",");    
        //            int rowCounts = yearList.Count;
        //            string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
        //            color = colorArray[x].ToString();

        //            string[] shapeArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomShapeStrings(rowCounts);
        //            symbol = shapeArray[x].ToString();

        //            //StringBuilder sbPoints = new StringBuilder();
        //            //sbPoints.Append(string.Format("{0}:{1}", "show", "true"));
        //            //sbPoints.Append(",");
        //            //sbPoints.Append(string.Format("{0}:'{1}'", "symbol", symbol));                   
        //            //sbPoints.Append(",");
        //            //sbPoints.Append(string.Format("{0}:'{1}'", "fillColor", color));                  
        //            //sbPoints.Insert(0, "{");
        //            //sbPoints.Append("}");
        //            //points = sbPoints.ToString();

        //            //StringBuilder sbLines = new StringBuilder();
        //            //sbLines.Append(string.Format("{0}:{1}", "show", "true"));
        //            //sbLines.Insert(0, "{");
        //            //sbLines.Append("}");
        //            //lines = sbLines.ToString();

        //            ChartPoint chartPoint = new ChartPoint();
        //            chartPoint.show = true;
        //            chartPoint.symbol = symbol;
        //            chartPoint.fillColor = color;

        //            ChartLine chartLine = new ChartLine();
        //            chartLine.show = true;

        //            lst.Add(new LineChartInfo() { label = year, data = sb.ToString(), lines = chartLine, points = chartPoint, color = color });
        //        }
        //    }
        //    JsonResult jsonResult = base.Json(lst, JsonRequestBehavior.AllowGet);
        //    return jsonResult;
        //}

        public JsonResult Department_GetChartData(int? LSCompanyID)
        {
            List<DepartmentReportModel> department_lst = HRReportRespository.GetEmpListByDepartment(LSCompanyID, LanguageId);
            List<ChartInfo> lst = new List<ChartInfo>();
            int rowCounts = department_lst.Count;
            if (rowCounts > 0)
            {
                string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                foreach (var item in department_lst)
                {
                    string Name = item.DepartmentName;
                    string Qty = item.Qty.ToString();
                    var position = department_lst.IndexOf(item);
                    lst.Add(new ChartInfo() { label = Name, data = Qty, color = colorArray[position] });
                }
            }
            return base.Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Department_GetChartDataByPercent(int? LSCompanyID)
        {
            List<DepartmentReportModel> department_lst = HRReportRespository.GetEmpListByDepartment(LSCompanyID, LanguageId);
            List<ChartInfo> lst = new List<ChartInfo>();
            int rowCounts = department_lst.Count;
            if (rowCounts > 0)
            {
                string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                foreach (var item in department_lst)
                {
                    string Name = item.DepartmentName;
                    string Qty = item.Qty.ToString();
                    string Percentage = item.Percentage.ToString();
                    var position = department_lst.IndexOf(item);
                    lst.Add(new ChartInfo() { label = Name, data = Percentage, color = colorArray[position] });
                }
            }
            return base.Json(lst, JsonRequestBehavior.AllowGet);
        }
        #endregion CHART BY POSTIION ==========================================================================================================================================

        #region CHART BY POSTIION ============================================================================================================================================
        public JsonResult Position_GetChartData(int? LSCompanyID)
        {
            DataTable DT = HRReportRespository.GetListByQualification_Master(LSCompanyID, LanguageId);
            List<ChartInfo> lst = new List<ChartInfo>();
            int rowCounts = DT.Rows.Count;
            if (rowCounts > 0)
            {
                string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                for (int i = 0; i < rowCounts; i++)
                {
                    string Name = DT.Rows[i]["PositionName"].ToString();
                    string Qty = DT.Rows[i]["Qty"].ToString();
                    lst.Add(new ChartInfo() { label = Name, data = Qty, color = colorArray[i] });
                }
            }
            return base.Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Position_GetChartDataByPercent(int? LSCompanyID)
        {
            DataTable DT = HRReportRespository.GetListByPosition_Master(LSCompanyID, LanguageId);
            List<ChartInfo> lst = new List<ChartInfo>();
            int rowCounts = DT.Rows.Count;
            if (rowCounts > 0)
            {
                string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                for (int i = 0; i < rowCounts; i++)
                {
                    string Name = DT.Rows[i]["PositionName"].ToString();
                    string Percentage = DT.Rows[i]["Percentage"].ToString();
                    lst.Add(new ChartInfo() { label = Name, data = Percentage, color = colorArray[i] });
                }
            }
            return base.Json(lst, JsonRequestBehavior.AllowGet);
        }
        #endregion CHART BY POSTIION ==========================================================================================================================================

        #region QUALIFICATION CHART ============================================================================================================================================
        public JsonResult Qualification_GetChartData(int? LSCompanyID)
        {
            DataSet DS = HRReportRespository.GetData_Qualification(LSCompanyID, LanguageId);
            List<ChartInfo> lst = new List<ChartInfo>();
            int rowCounts = DS.Tables[0].Rows.Count;
            if (rowCounts > 0)
            {
                string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                for (int i = 0; i < rowCounts; i++)
                {
                    string Name = DS.Tables[0].Rows[i]["QualificationName"].ToString();
                    string Qty = DS.Tables[0].Rows[i]["Qty"].ToString();
                    lst.Add(new ChartInfo() { label = Name, data = Qty, color = colorArray[i] });
                }
            }
            return base.Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Qualification_GetPercentChartData(int? LSCompanyID)
        {
            DataSet DS = HRReportRespository.GetData_Qualification(LSCompanyID, LanguageId);
            List<ChartInfo> lst = new List<ChartInfo>();
            int rowCounts = DS.Tables[0].Rows.Count;
            if (rowCounts > 0)
            {
                string[] colorArray = Eagle.Common.Utilities.ImageUtils.GetUniqueRandomHexStrings(rowCounts);
                for (int i = 0; i < rowCounts; i++)
                {
                    string Name = DS.Tables[0].Rows[i]["QualificationName"].ToString();
                    string Percentage = DS.Tables[0].Rows[i]["Percentage"].ToString();
                    lst.Add(new ChartInfo() { label = Name, data = Percentage, color = colorArray[i] });
                }
            }
            return base.Json(lst, JsonRequestBehavior.AllowGet);
        }
        #endregion CHART ==========================================================================================================================================
    }
}
