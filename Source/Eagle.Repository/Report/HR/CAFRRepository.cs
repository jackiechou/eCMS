using Eagle.Common.Data;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Report.HR
{
    public class CAFRRepository
    {
        public EntityDataContext Context { get; set; }

        public CAFRRepository(EntityDataContext context)
        {
            this.Context = context;
        }

        public DataSet GetData_Master(int? LSCompanyID, int LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_CreateCAFR_Master]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataSet ds = new DataSet("ParentChildrenDS");
            ds = sqlHandler.ExecuteAsDataSet(sp, ParamCollInput);
            ds.Tables[0].TableName = "HR_sprptEmployee_CreateCAFR_Master";
            ds.Tables[1].TableName = "HR_sprptEmployee_CreateCAFR_Summary";           
            return ds;
        }

        public DataSet GetData_ByQuarter(int? LSCompanyID, int LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_CreateCAFRByQuarter]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataSet ds = new DataSet("ParentChildrenDS");
            ds = sqlHandler.ExecuteAsDataSet(sp, ParamCollInput);
            ds.Tables[0].TableName = "HR_sprptEmployee_CreateCAFRByQuarter_MasterContent";
            ds.Tables[1].TableName = "HR_sprptEmployee_CreateCAFRByQuarter_TotalsByQuarter";
            ds.Tables[2].TableName = "HR_sprptEmployee_CreateCAFRByQuarter_SummaryContent";
            ds.Tables[3].TableName = "HR_sprptEmployee_CreateCAFRByQuarter_TotalsByYear";
            //DataRelation dataRelation = new DataRelation(String.Format("{0}_{1}", ds.Tables[0].TableName, ds.Tables[1].TableName), ds.Tables[0].Columns["LSCompanyID"], ds.Tables[1].Columns["LSCompanyID"], false);
            //ds.Relations.Add(dataRelation);
            return ds;
        }

        //public DataSet GetData_Details(int? LSCompanyID, int LanguageId)
        //{
        //    string sp = "[dbo].[HR_sprptEmployee_CreateCAFR_Details]";
        //    SQLHandler sqlHandler = new SQLHandler();
        //    List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
        //    DataSet ds = new DataSet("ParentChildrenDS");
        //    ds = sqlHandler.ExecuteAsDataSet(sp, ParamCollInput);
        //    ds.Tables[0].TableName = "HR_sprptEmployee_CreateCAFR_MasterContent";
        //    ds.Tables[1].TableName = "HR_sprptEmployee_CreateCAFR_DetailContent";
        //    ds.Tables[2].TableName = "HR_sprptEmployee_CreateCAFR_SummaryContent";
        //    DataRelation dataRelation = new DataRelation(String.Format("{0}_{1}", ds.Tables[0].TableName, ds.Tables[1].TableName), ds.Tables[0].Columns["LSCompanyID"], ds.Tables[1].Columns["LSCompanyID"], false);
        //    ds.Relations.Add(dataRelation);
        //    return ds;
        //}

        //public DataTable GetYearlyDetailList(int? Year, int? LSCompanyID, string EmpCode, string FullName, string UserName, int? ModuleID, int? FunctionID, int LanguageId)            
        //{

        //    string sp = "[dbo].[Timesheet_sprptOTByYear_Details]";
        //    SQLHandler sqlHandler = new SQLHandler();
        //    List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@Year", Year));           
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@EmpCode", EmpCode));
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@FullName", FullName));
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@UserGroupID", UserName));
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@FunctionID", FunctionID));
        //    ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
        //    DataTable dt = new DataTable();
        //    dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
        //    return dt;         
        //}

    }
}
