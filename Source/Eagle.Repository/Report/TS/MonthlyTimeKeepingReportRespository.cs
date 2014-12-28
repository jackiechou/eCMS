using Eagle.Common.Data;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Eagle.Repository.Report.TS
{
    public class MonthlyTimeKeepingReportRespository
    {
         public EntityDataContext Context { get; set; }

         public MonthlyTimeKeepingReportRespository(EntityDataContext context)
        {
            this.Context = context;
        }

         public DataTable GetMonthlyMasterList(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName, string UserName, int? ModuleID, int LanguageId)
        {
            //string sql = "select LSCompanyID,LSCompanyDefineID,Parent,Lineage,LSCompanyCode,Name,VNName, Address,Phone,Fax, Rank,Used, TaxCode, FileId from dbo.LS_tblCompany";
            //SQLHandler sqlHandler = new SQLHandler();
            //DataTable dt = sqlHandler.ExecuteSQL(sql);
            //return dt;

            string sp = "[dbo].[Timesheet_sprptTimeKeepingByMonth_Master]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@Month", Month));
            ParamCollInput.Add(new KeyValuePair<string, object>("@Year", Year));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@EmpCode", EmpCode));
            ParamCollInput.Add(new KeyValuePair<string, object>("@FullName", FullName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserGroupID", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataTable dt = new DataTable();
            dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            return dt;         
        }

        public DataTable GetMonthlyDetailList(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName, string UserName, int? ModuleID, int LanguageId)            
        {

            string sp = "[dbo].[Timesheet_sprptTimeKeepingByMonth_Details]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@Month", Month));
            ParamCollInput.Add(new KeyValuePair<string, object>("@Year", Year));           
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@EmpCode", EmpCode));
            ParamCollInput.Add(new KeyValuePair<string, object>("@FullName", FullName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserGroupID", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataTable dt = new DataTable();
            dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            return dt;         
        }
    }
}
