using Eagle.Common.Data;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Report.TS
{
    public class YearlyOTReportRepository
    {
        public EntityDataContext Context { get; set; }

        public YearlyOTReportRepository(EntityDataContext context)
        {
            this.Context = context;
        }

        public DataTable GetYearlyMasterList(int? Year, int? LSCompanyID, string EmpCode, string FullName, string UserName, int? ModuleID, int? FunctionID, int LanguageId)
        {
            string sp = "[dbo].[Timesheet_sprptOTByYear_Master]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
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

        public DataTable GetYearlyDetailList(int? Year, int? LSCompanyID, string EmpCode, string FullName, string UserName, int? ModuleID, int? FunctionID, int LanguageId)            
        {

            string sp = "[dbo].[Timesheet_sprptOTByYear_Details]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
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

    }
}
