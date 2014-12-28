using Eagle.Common.Data;
using Eagle.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Eagle.Model.Report.HR.Contract;
using Eagle.Model.Report.HR.Department;
using Eagle.Model.Report.HR.Position;
using Eagle.Model.Report.HR.Qualification;

namespace Eagle.Repository.Report.HR
{
    public class HRReportRespository
    {
        #region Department ======================================================================================================      
        public static DataTable GetListByDepartment_Master(int? LSCompanyID, int? LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_GetListByDepartment_Master]";
            SQLHandler sqlHandler = new SQLHandler();
             List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
             ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataTable dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            dt.TableName = "HR_sprptEmployee_GetListByDepartment_Master";
            if (dt.Rows.Count == 0)
            {
                var row = dt.NewRow();
                row["LSCompanyID"] = -1;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable GetListByDepartment_Details(int? LSCompanyID, int? LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_GetListByDepartment_Details]";
            SQLHandler sqlHandler = new SQLHandler();
             List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
             ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataTable dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            dt.TableName = "HR_sprptEmployee_GetListByDepartment_Details";
            if (dt.Rows.Count == 0)
            {
                var row = dt.NewRow();
                row["LSCompanyID"] = -1;
                row["Department"] = string.Empty;
                row["EmpCode"] = string.Empty;
                row["FullName"] = string.Empty;
                row["DOB"] = string.Empty;
                row["Email"] = string.Empty;
                row["Gender"] = string.Empty;
                row["Marital"] = string.Empty;
                row["Position"] = string.Empty;              
                row["StartDate"] = string.Empty;
                row["JoinDate"] = string.Empty;
                row["Active"] = false;
                dt.Rows.Add(row);
            }
            return dt;            
        }

        public static DataSet GetListByDepartment(int? LSCompanyID, int? LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_GetListByDepartment]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataSet DS = sqlHandler.ExecuteAsDataSet(sp, ParamCollInput);
            DS.Tables[0].TableName = "HR_sprptEmployee_GetListByDepartment";
            if (DS.Tables[0].Rows.Count == 0)
            {
                var row = DS.Tables[0].NewRow();
                row["LSCompanyID"] = -1;
                row["Department"] = string.Empty;
                row["Qty"] = string.Empty;
                row["Percentage"] = string.Empty;
                DS.Tables[0].Rows.Add(row);
            }
            return DS;
        }

        public static List<DepartmentReportModel> GetEmpListByDepartment(int? LSCompanyID, int? LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<DepartmentReportModel> lst = new List<DepartmentReportModel>();
                //System.Data.Objects.ObjectResult<HR_sprptEmployee_GetListByDepartment_Result> objectResult = context.HR_sprptEmployee_GetListByDepartment(LSCompanyID,LanguageId);
                //return objectResult.Select(p => new DepartmentReportModel()
                //{                    
                //    LSCompanyID = p.LSCompanyID,
                //    DepartmentName = p.Department,
                //    Qty = p.Qty,
                //    Percentage = p.Percentage
                //}).ToList();
                return lst;
            }
        }

        //public static DataTable GetListByDepartmentMonthlyWithPivot()
        //{
        //    string sp = "[dbo].[HR_sprptEmployee_GetListByDepartmentMonthlyWithPivot]";
        //    SQLHandler sqlHandler = new SQLHandler();
        //    DataTable dt = sqlHandler.ExecuteAsDataTable(sp);
        //    return dt;
        //}
        #endregion =============================================================================================================

        #region Qualification =================================================================================================
        //public static List<QualificationReportModel> GetListByLSQualificationId(int? LSCompanyID, int? LanguageId)
        //{
        //    using (EntityDataContext context = new EntityDataContext())
        //    {
        //        System.Data.Objects.ObjectResult<HR_sprptQualification_GetListByCategory_Result> objectResult = context.HR_sprptQualification_GetListByCategory(LSCompanyID, LanguageId);
        //        return objectResult.Select(p => new QualificationReportModel()
        //        {
        //            LSQualificationID = p.LSQualificationID,
        //            LSQualificationCode = p.LSQualificationCode,
        //            QualificationName = p.QualificationName,
        //            Qty = p.Qty,
        //            Percentage = p.Percentage
        //        }).ToList();
        //    }
        //}

        public static DataSet GetData_Qualification(int? LSCompanyID, int? LanguageId)
        {
            string sp = "[dbo].[HR_sprptQualification_GetListByCategory]";
            SQLHandler sqlHandler = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
                return sqlHandler.ExecuteAsDataSet(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static DataTable GetListByQualification_Master(int? LSCompanyID, int? LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_GetListByQualification_Master]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataTable dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            dt.TableName = "HR_sprptEmployee_GetListByQualification_Master";
            if (dt.Rows.Count == 0)
            {
                var row = dt.NewRow();
                row["LSQualificationID"] = -1;
                row["LSQualificationCode"] = string.Empty;
                row["QualificationName"] = string.Empty;
                row["Qty"] = string.Empty;
                row["Percentage"] = string.Empty;
                dt.Rows.Add(row);
            }
            return dt;
        }


        public static DataTable GetListByQualification_Details(int? LSCompanyID, int LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_GetListByQualification_Details]";
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            SQLHandler sqlHandler = new SQLHandler();
            DataTable dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            dt.TableName = "HR_sprptEmployee_GetListByQualification_Details";
            if (dt.Rows.Count == 0)
            {
                var row = dt.NewRow();
                row["LSQualificationID"] = -1;
                row["LSQualificationCode"] = string.Empty;
                row["QualificationName"] = string.Empty;
                row["EmpCode"] = string.Empty;
                row["QualificationName"] = string.Empty;
                row["FullName"] = string.Empty;
                row["DOB"] = string.Empty;
                row["Gender"] = string.Empty;
                row["Marital"] = string.Empty;
                row["Position"] = string.Empty;
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion Qualification ==============================================================================================

        #region POSITION ======================================================================================================
        public static DataTable GetListByPosition_Master(int? LSCompanyID, int? LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_GetListByPosition_Master]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataTable dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            dt.TableName = "HR_sprptEmployee_GetListByPosition_Master";
            if (dt.Rows.Count == 0)
            {
                var row = dt.NewRow();
                row["LSPositionID"] = -1;
                row["LSPositionCode"] = string.Empty;
                row["PositionName"] = string.Empty;
                row["Qty"] = string.Empty;
                row["Percentage"] = string.Empty;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable GetListByPosition_Detail(int? LSCompanyID, int? LanguageId)
        {
            string sp = "[dbo].[HR_sprptEmployee_GetListByPosition_Details]";
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            SQLHandler sqlHandler = new SQLHandler();           
            DataTable dt = sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            dt.TableName = "HR_sprptEmployee_GetListByPosition_Details";
            if (dt.Rows.Count == 0)
            {
                var row = dt.NewRow();
                row["LSPositionID"] = -1;
                row["LSPositionCode"] = string.Empty;
                row["PositionName"] = string.Empty;
                row["EmpID"] = string.Empty;
                row["EmpCode"] = string.Empty;
                row["FullName"] = string.Empty;
                row["DOB"] = string.Empty;
                row["Gender"] = string.Empty;
                row["Marital"] = string.Empty;
                row["Qualification"] = string.Empty;
                dt.Rows.Add(row);
            }
            return dt;
        }

        //public static List<PositionReportModel> GetListByPositionId(int? LSCompanyID, int? LanguageId)
        //{
        //    using (EntityDataContext context = new EntityDataContext())
        //    {
        //        System.Data.Objects.ObjectResult<HR_sprptEmployee_GetListByPosition_Result> objectResult = context.HR_sprptEmployee_GetListByPosition(LSCompanyID, LanguageId);
        //        return objectResult.Select(p => new PositionReportModel()
        //        {
        //            LSPositionID = p.LSPositionID,
        //            LSPositionCode = p.LSPositionCode,
        //            PositionName = p.PositionName,
        //            Qty = p.Qty,
        //            Percentage = p.Percentage
        //        }).ToList();
        //    }
        //}       
        #endregion POSITION ==================================================================================================

        #region Contract =====================================================================================================
        //public static List<ContractReportModel> GetListByContractTypeID(int? LSCompanyID, int? LanguageId)
        //{
        //    using (EntityDataContext context = new EntityDataContext())
        //    {
        //        System.Data.Objects.ObjectResult<HR_sprptEmployee_GetListByContractType_Result> objectResult = context.HR_sprptEmployee_GetListByContractType(LSCompanyID, LanguageId);
        //        return objectResult.Select(p => new ContractReportModel()
        //        {
        //            LSContractTypeID = p.LSContractTypeID,
        //            ContractTypeName = p.ContractTypeName,
        //            TotalMaleContractsByLSContractTypeIDAndMaleGender = p.TotalMaleContractsByLSContractTypeIDAndMaleGender,
        //            TotalFeMaleContractsByLSContractTypeIDAndMaleGender = p.TotalFeMaleContractsByLSContractTypeIDAndMaleGender,
        //            TotalContractsByLSContractTypeID = p.TotalContractsByLSContractTypeID,
        //            TotalPercentMaleContractsByLSContractTypeIDAndMaleGender = p.TotalPercentMaleContractsByLSContractTypeIDAndMaleGender,
        //            TotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender = p.TotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender,
        //            TotalPercentByLSContractTypeID = p.TotalPercentByLSContractTypeID
        //        }).ToList();
        //    }

        //}

        public static DataTable GetMonthlyDigitDataWithUnPivotByYear(int Year)
        {
            string sp = "[dbo].[HR_sprptContract_GetMonthlyDigitDataWithUnPivotByYear]";
            SQLHandler sqlHandler = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@Year", Year));
                return sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet GetData_Contract(int? LSCompanyID, int? LanguageId)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));
            DataSet ds = sqlHandler.ExecuteAsDataSet("[dbo].[HR_sprptEmployee_GetListByContractType]", ParamCollInput);
            return ds;

            //SQLHandler sqlHandler = new SQLHandler();
            //Hashtable hashtable = new Hashtable();
            //hashtable.Add("LanguageId", LanguageId);
            //DataSet DS = sqlHandler.ExecuteAsDataSet("[dbo].[HR_sprptEmployee_GetListByContractType]", hashtable);
            //return ds;
           
        }

        public static DataTable GetListByContractType_Master(int? LSCompanyID, int? LanguageId)
        {
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));

            SQLHandler sqlHandler = new SQLHandler();
            DataTable dt = sqlHandler.ExecuteAsDataTable("[dbo].[HR_sprptEmployee_GetListByContractType_Master]", ParamCollInput);
            dt.TableName = "HR_sprptEmployee_GetListByContractType_Master";
            if (dt.Rows.Count == 0)
            {
                var row = dt.NewRow();
                row["LSContractTypeID"] = -1;
                row["LSContractTypeCode"] = string.Empty;
                row["ContractTypeName"] = string.Empty;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable GetListByContractType_Details(int? LSCompanyID, int? LanguageId)
        {
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@LSCompanyID", LSCompanyID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@LanguageId", LanguageId));

            SQLHandler sqlHandler = new SQLHandler();
            DataTable dt = sqlHandler.ExecuteAsDataTable("[dbo].[HR_sprptEmployee_GetListByContractType_Details]", ParamCollInput);
            dt.TableName = "HR_sprptEmployee_GetListByContractType_Details";
            if (dt.Rows.Count == 0)
            {
                var row = dt.NewRow();
                row["LSContractTypeID"] = -1;
                row["LSContractTypeCode"] = string.Empty;
                row["ContractTypeName"] = string.Empty;
                row["FullName"] = string.Empty;
                row["Used"] = false;
                row["EffectiveDate"] = string.Empty;
                 row["ExpiredDate"] = string.Empty;
                dt.Rows.Add(row);
            }
            return dt;
        }
       
        public static DataTable GetMonthlyDataWithPivot()
        {
            SQLHandler sqlHandler = new SQLHandler();
            DataTable dt = sqlHandler.ExecuteAsDataTable("[dbo].[HR_sprptContract_GetMonthlyDataWithPivot]");
            return dt;
        }

        public static DataTable GetMonthlyDigitDataWithPivot()
        {
            SQLHandler sqlHandler = new SQLHandler();
            DataTable dt = sqlHandler.ExecuteAsDataTable("[dbo].[HR_sprptContract_GetMonthlyDigitDataWithPivot]");
            return dt;
        }

        public static DataTable GetMonthlyDigitDataWithPivotByYear(int Year)
        {
            string sp = "[dbo].[HR_sprptContract_GetMonthlyDigitDataWithPivotByYear]";
            SQLHandler sqlHandler = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@Year", Year));
                return sqlHandler.ExecuteAsDataTable(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Contract ================================================================================================
    }
}
