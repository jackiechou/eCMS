using Eagle.Common.Data;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Report.TS
{
    public class MonthlyOTReportRespository
    {
        public EntityDataContext Context { get; set; }

        public MonthlyOTReportRespository(EntityDataContext context)
        {
            this.Context = context;
        }

        public DataTable GetMonthlyMasterList(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName, string UserName, int? ModuleID, int? FunctionID, int LanguageId)
        {
            //string sql = "select LSCompanyID,LSCompanyDefineID,Parent,Lineage,LSCompanyCode,Name,VNName, Address,Phone,Fax, Rank,Used, TaxCode, FileId from dbo.LS_tblCompany";
            //SQLHandler sqlHandler = new SQLHandler();
            //DataTable dt = sqlHandler.ExecuteSQL(sql);
            //return dt;

            string sp = "[dbo].[Timesheet_sprptOTByMonth_Master]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@Month", Month));
            ParamCollInput.Add(new KeyValuePair<string, object>("@Year", Year));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@EmpCode", EmpCode));
            ParamCollInput.Add(new KeyValuePair<string, object>("@FullName", FullName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserGroupID", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@FunctionID", FunctionID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataTable dt = new DataTable();
            dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            return dt;         
        }

        public DataTable GetMonthlyDetailList(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName, string UserName, int? ModuleID, int? FunctionID, int LanguageId)            
        {

            string sp = "[dbo].[Timesheet_sprptOTByMonth_Details]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@Month", Month));
            ParamCollInput.Add(new KeyValuePair<string, object>("@Year", Year));           
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@EmpCode", EmpCode));
            ParamCollInput.Add(new KeyValuePair<string, object>("@FullName", FullName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserGroupID", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@FunctionID", FunctionID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataTable dt = new DataTable();
            dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            return dt;         
        }
        
        //public List<Timesheet_sprptOTRecord_Master_Result> GetListOfOTCompany(Timesheet_sprptOTRecord_Result model, out string errorMessage)
        //{
        //    try
        //    {
        //        var listOfFound = this.Context.Timesheet_sprptOTRecord_Master(model.Month, model.Year, model.LSCompanyID).ToList();
        //        if (listOfFound != null)
        //        {
        //            var i = 1;
        //            foreach (var item in listOfFound)
        //            {
        //                item.Seq = (i++).ToString();
        //            }
        //        }
        //        errorMessage = String.Empty;

        //        return listOfFound;
        //    }
        //    catch (Exception exp)
        //    {
        //        errorMessage = exp.Message;
        //        return null;
        //    }
        //}
    }
}
