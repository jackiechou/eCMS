using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using Eagle.Repository.SYS.Roles;
using Eagle.Repository.SYS.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
    public class ValidationController : BaseController
    {

        #region Candidate

        public JsonResult ValidationCandidateQualificationNo(string QualificationNo, int CandidateID, string InitialQualificationNo)
        {
            CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
            if (InitialQualificationNo != null)
            {
                if ((InitialQualificationNo != QualificationNo) && _repository.isQualificationNoExists(CandidateID, QualificationNo))
                {
                    return base.Json(Eagle.Resource.LanguageResource.InValidQualificationNo, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (_repository.isQualificationNoExists(CandidateID, QualificationNo))
                {
                    return base.Json(Eagle.Resource.LanguageResource.InValidQualificationNo, JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidationCandidatePriority(int PriorityAlowNull, int CandidateID, int? InitialPriorityAlowNull)
        {
            CandidateQualificationRepository _repository = new CandidateQualificationRepository(db);
            if (InitialPriorityAlowNull != null)
            {
                if ((InitialPriorityAlowNull != PriorityAlowNull) && _repository.isPriorityExists(CandidateID, PriorityAlowNull))
                {
                    return base.Json(Eagle.Resource.LanguageResource.InValidPriority, JsonRequestBehavior.AllowGet);
                }
                if (PriorityAlowNull <= 0)
                {
                    return base.Json(string.Format(Eagle.Resource.LanguageResource.MinErrorMessage,Eagle.Resource.LanguageResource.Priority,"0"), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (_repository.isPriorityExists(CandidateID, PriorityAlowNull))
                {
                    return base.Json(Eagle.Resource.LanguageResource.InValidPriority, JsonRequestBehavior.AllowGet);
                }
                if (PriorityAlowNull <= 0)
                {
                    return base.Json(string.Format(Eagle.Resource.LanguageResource.MinErrorMessage, Eagle.Resource.LanguageResource.Priority, "0"), JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
   
        #region LSCompany
        public JsonResult CheckValidCompany(int LSCompanyID)
        {
            List<int?> allDefine = db.LS_tblCompanyDefine.Select(p => p.Parent).ToList();
            var lastDefine = db.LS_tblCompanyDefine.Where(p => !allDefine.Contains(p.LSCompanyDefineID)).FirstOrDefault();

            if (lastDefine != null)
            {
                var modelCheck = db.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID && p.LSCompanyDefineID == lastDefine.LSCompanyDefineID).Select(p => p.LSCompanyDefineID).FirstOrDefault();
                if (modelCheck != null)
                {
                    return base.Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string errorMessage = string.Format(Eagle.Resource.LanguageResource.PleaseSelect0, Eagle.Resource.LanguageResource.LastCompanyDefine);
                    return base.Json(errorMessage, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return base.Json("Lỗi dữ liệu cài đặt tại table 'LS_tblCompanyDefine'", JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult ValidationLSCompanyCode(string LSCompanyCode, string InitialLSCompanyCode)
        {
            CompanyRepository repository = new CompanyRepository(db);
            if ((LSCompanyCode != InitialLSCompanyCode) && repository.LSCompanyCodeExists(LSCompanyCode))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + LSCompanyCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + LSCompanyCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region For Recruiment Plan

        public JsonResult ValidationPlanQuantity(int PlanQuantityAlowNull, int QuotaRecruitment, int YearAlowNull, int LSCompanyID, int LSPositionIDAlowNull, int PlanID)
        {
            if (PlanQuantityAlowNull <= 0)
            {
                string errorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputLessThan, Eagle.Resource.LanguageResource.PlanQuantity,"0");
                return base.Json(errorMessage, JsonRequestBehavior.AllowGet);
            }

            if (PlanQuantityAlowNull > QuotaRecruitment)
            {
                string errorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputGreaterThan, Eagle.Resource.LanguageResource.PlanQuantity, QuotaRecruitment.ToString());
                return base.Json(errorMessage, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int TotalPlan = 0;
                List<int> StatusUnapprove = new List<int>() { 3, 5, 7, 9, 11 };
                // lấy tổng các PlanQuantity với điều kiện
                //không phải là plan hiện tại (dành cho chức năng Cập nhật)
                //cùng năm,Cty,phòng ban, tổ nhóm, chức vụ và không ở trạng thái unapprove
                var TotalPlantmp = db.REC_tblPlan
                                     .Where(p => p.Year == YearAlowNull && 
                                         p.LSCompanyID == LSCompanyID &&                                    
                                         p.LSPositionID == LSPositionIDAlowNull &&
                                         p.PlanID != PlanID && 
                                         !StatusUnapprove.Contains(p.StatusPlan))
                                     .Select(p => p.PlanQuantity)
                                     .ToList();
                if (TotalPlantmp != null && TotalPlantmp.Count > 0)
                {
                    TotalPlan = TotalPlantmp.Sum();
                }
                int MaxPlan = QuotaRecruitment - TotalPlan;

                if (PlanQuantityAlowNull > MaxPlan)
                {
                    string errorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputOver,
                        Eagle.Resource.LanguageResource.PlanQuantity,
                        MaxPlan,TotalPlan,QuotaRecruitment);
                    return base.Json(errorMessage, JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region For Demand
        public JsonResult ValidationDemandCode(string DemandCode, string InitialDemandCode)
        {
            DemandRepository repository = new DemandRepository(db);
            if ((DemandCode != InitialDemandCode) && repository.CheckExist(DemandCode))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + DemandCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + DemandCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region For Candidate
        public JsonResult ValidationNoIDCandidate(string IDNo, string InitialIDNo)
        {
            CandidateRepository repository = new CandidateRepository(db);
            if ((IDNo != InitialIDNo) && repository.IDNoExists(IDNo))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + IDNo + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + IDNo + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ValidationCandidateCode(string CandidateCode, string InitialCandidateCode)
        {
            CandidateRepository repository = new CandidateRepository(db);
            if ((CandidateCode != InitialCandidateCode) && repository.CandidateCodeExists(CandidateCode))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + CandidateCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + CandidateCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region For account

        public JsonResult ValidationUserName(string UserName, string InitialUserName)
        {
            UserRepository repository = new UserRepository(db);
            if ((UserName != InitialUserName) && repository.CheckExist(UserName))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + UserName + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + UserName + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Recruitment Project
        public JsonResult ValidationProjectCode(string ProjectCode, string InitialProjectCode)
        {
            RecruitmentProjectRepository repository = new RecruitmentProjectRepository(db);
            if ((ProjectCode != InitialProjectCode) && repository.CheckExist(ProjectCode))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + ProjectCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + ProjectCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region For BirthDate Valiate
        public JsonResult ValidateBirthDate(string SelectedDate)
        {
            string result = "true", SpecifiedFormat = "dd/MM/yyyy";
            if (SelectedDate != string.Empty)
            {
                //if (LanguageId == 124)
                //    SpecifiedFormat = "MM/dd/yyyy";
                //else
                //    SpecifiedFormat = "dd/MM/yyyy";

                DateTime SELECTED_DATE = new DateTime();
                if (DateTime.TryParseExact(SelectedDate, SpecifiedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out SELECTED_DATE))
                {
                    DateTime RangeDateTime = DateTime.Now.AddYears(-System.Math.Abs(18));
                    DateTime MinDate = new DateTime(1900, 1, 1);
                    if (SELECTED_DATE.CompareTo(MinDate) > 0)
                    {
                        if (SELECTED_DATE.CompareTo(RangeDateTime) > 0)
                        {
                            if (LanguageId == 124)
                                result = SelectedDate + " is invalid!";
                            else
                                result = SelectedDate + " không hợp lệ!";
                        }
                    }
                    else
                    {
                        if (LanguageId == 124)
                            result = SelectedDate + " is greater than 1900!";
                        else
                            result = SelectedDate + " phải lớn hơn 1900!";
                    }
                }
                else
                {
                    if (LanguageId == 124)
                        result = SelectedDate + " is not date type!";
                    else
                        result = SelectedDate + " không phải kiểu ngày!";
                }
            }
            else
            {
                if (LanguageId == 124)
                    result = "Please pick a date!";
                else
                    result = "Vui lòng chọn ngày!";
            }
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidateBirthDate(string SelectedDate, int AllowedYear)
        {
            string result = "true", SpecifiedFormat = "dd/MM/yyyy";
            if (SelectedDate != string.Empty)
            {
                //if (LanguageId == 124)
                //    SpecifiedFormat = "MM/dd/yyyy";
                //else
                //    SpecifiedFormat = "dd/MM/yyyy";

                DateTime SELECTED_DATE = new DateTime();
                if (DateTime.TryParseExact(SelectedDate, SpecifiedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out SELECTED_DATE))
                {
                    int _AllowedYear = (AllowedYear > 0) ? AllowedYear : 18;
                    DateTime RangeDateTime = DateTime.Now.AddYears(-System.Math.Abs(_AllowedYear));
                    DateTime MinDate = new DateTime(1900, 1, 1);
                    if (SELECTED_DATE.CompareTo(MinDate) > 0)
                    {
                        if (SELECTED_DATE.CompareTo(RangeDateTime) > 0)
                        {
                            if (LanguageId == 124)
                                result = SelectedDate + " is invalid!";
                            else
                                result = SelectedDate + " không hợp lệ!";
                        }
                    }else
                    {
                        if (LanguageId == 124)
                            result = SelectedDate + " is greater than 1900!";
                        else
                            result = SelectedDate + " phải lớn hơn 1900!";
                    }
                }                   
                else
                {
                    if (LanguageId == 124)
                        result = SelectedDate + " is not date type!";
                    else
                        result = SelectedDate + " không phải kiểu ngày!";
                }
            }
            else
            {
                if (LanguageId == 124)
                    result = "Please pick a date!";
                else
                    result = "Vui lòng chọn ngày!";
            }           
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Group
        public JsonResult ValidationGroupCode(string GroupCode, string InitialGroupCode)
        {
            RoleRepository repository = new RoleRepository(db);
            if (repository.CheckExist(GroupCode) && (GroupCode.ToUpper() != InitialGroupCode.ToUpper()))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + GroupCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + GroupCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For LeaveDayType Code

        public JsonResult ValidationLSLeaveDayTypeCode(string LSLeaveDayTypeCode, string strInitCode)
        {
            LeaveDayTypeRepository repository = new LeaveDayTypeRepository(db);
            if (repository.CheckExist(LSLeaveDayTypeCode) && (LSLeaveDayTypeCode != strInitCode))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + LSLeaveDayTypeCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + LSLeaveDayTypeCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        
        public JsonResult ValidationNotEqualZero(int? num)
        {
            string result = "true";
            if (num == 0)
            {
                if (LanguageId == 124)
                    result = String.Format(CultureInfo.InvariantCulture, "{0} is not suitable.", num);
                else
                    result = String.Format(CultureInfo.InvariantCulture, "{0} - Vui lòng chọn item khác"); 
            }
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidationOTLimitIsGreaterThan0(int OTLimit)
        {
            if (OTLimit < 0)
            {
                string result = String.Format(Eagle.Resource.LanguageResource.IsGreaterOrEqualThan0, Eagle.Resource.LanguageResource.OTLimit);
                return base.Json(result, JsonRequestBehavior.AllowGet);
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidationStandardAnnualLeaveIsGreaterThan0(int StandardAnnualLeave)
        {
            if (StandardAnnualLeave < 0)
            {
                string result = String.Format(Eagle.Resource.LanguageResource.IsGreaterOrEqualThan0, Eagle.Resource.LanguageResource.StandardAnnualLeave);
                return base.Json(result, JsonRequestBehavior.AllowGet);
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ValidationCompare2Date(DateTime? StartDate, DateTime? EndDate)
        {
            string result = "true";
            if(StartDate.HasValue && EndDate.HasValue)
            {
                if (DateTime.Compare(StartDate.Value, EndDate.Value) > 0)
                {
                    if (LanguageId == 124)
                        result = String.Format(CultureInfo.InvariantCulture, "{0} is less than {1}.", StartDate, EndDate);
                    else
                        result = String.Format(CultureInfo.InvariantCulture, "Vui lòng chọn ngày trước {0} nhỏ hơn ngày sau {1}", StartDate, EndDate);
                }
            }
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }

        #region For Training Code
        public JsonResult ValidationTrainingCode(string Code, string InitialTrainingCode)
        {
            TrainingCodeRepository repository = new TrainingCodeRepository(db);
            string errorMessage;
            if (repository.CheckExist(Code, out errorMessage) && (Code != InitialTrainingCode))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + Code + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + Code + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Training Category
        public JsonResult ValidationTrainingCategory(string Code, string InitialTrainingCategory)
        {
            TrainingCategoryRepository repository = new TrainingCategoryRepository(db);
            string errorMessage;
            if (repository.CheckExist(Code, out errorMessage) && (Code != InitialTrainingCategory))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + Code + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + Code + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Training Provider
        public JsonResult ValidationTrainingProvider(string Code, string InitialTrainingProvider)
        {
            TrainingProviderRepository repository = new TrainingProviderRepository(db);
            string errorMessage;
            if (repository.CheckExist(Code, out errorMessage) && (Code != InitialTrainingProvider))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + Code + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + Code + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Training Expense
        public JsonResult ValidationTrainingExpense(string Code, string InitialTrainingExpense)
        {
            TrainingExpenseRepository repository = new TrainingExpenseRepository(db);
            string errorMessage;
            if (repository.CheckExist(Code, out errorMessage) && (Code != InitialTrainingExpense))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + Code + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + Code + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Training Form
        public JsonResult ValidationTrainingForm(string Code, string InitialTrainingForm)
        {
            TrainingFormRepository repository = new TrainingFormRepository(db);
            string errorMessage;
            if (repository.CheckExist(Code, out errorMessage) && (Code != InitialTrainingForm))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + Code + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + Code + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Training Type
        public JsonResult ValidationTrainingType(string Code, string InitialTrainingType)
        {
            TrainingTypeRepository repository = new TrainingTypeRepository(db);
            string errorMessage;
            if (repository.CheckExist(Code, out errorMessage) && (Code != InitialTrainingType))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + Code + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + Code + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Training Location
        public JsonResult ValidationTrainingLocation(string Code, string InitialTrainingLocation)
        {
            TrainingLocationRepository repository = new TrainingLocationRepository(db);
            string errorMessage;
            if (repository.CheckExist(Code, out errorMessage) && (Code != InitialTrainingLocation))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + Code + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + Code + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Training Course
        public JsonResult ValidationTrainingCourse(string LSTrainingCourseCode, string InitialTrainingCourse)
        {
            TrainingCourseRepository repository = new TrainingCourseRepository(db);
            string errorMessage;
            if (repository.CheckExist(LSTrainingCourseCode, out errorMessage) && (LSTrainingCourseCode != InitialTrainingCourse))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + LSTrainingCourseCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + LSTrainingCourseCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For Payroll SalaryAdjust
        public JsonResult ValidationSalaryAdjust(string LSSalaryAdjustCode, string InitialSalaryAdjust)
        {
            SalaryAjustRepository repository = new SalaryAjustRepository(db);
            string errorMessage;
            if (repository.CheckExist(LSSalaryAdjustCode, out errorMessage) && (LSSalaryAdjustCode != InitialSalaryAdjust))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + LSSalaryAdjustCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + LSSalaryAdjustCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public JsonResult ValidationPriority(int Priority, int EmpID, int? InitialPriority)
        {
            QualificationRespository _repository = new QualificationRespository(db);
            if (InitialPriority != null)
            {
                if ((InitialPriority != Priority) && _repository.IsPriorityExists(EmpID, Priority))
                    return base.Json(Eagle.Resource.LanguageResource.InValidPriority, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (_repository.IsPriorityExists(EmpID, Priority))
                    return base.Json(Eagle.Resource.LanguageResource.InValidPriority, JsonRequestBehavior.AllowGet);
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }

        #region For AnswerType
        public JsonResult ValidationAnswerType(string AnswerTypeCode, string InitialAnswerType)
        {
            var repository = new AnswerTypeRepository(db);
            string errorMessage;
            if (repository.IsExisted(AnswerTypeCode, out errorMessage) && (AnswerTypeCode != InitialAnswerType))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + AnswerTypeCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + AnswerTypeCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For TrainingAnswerType
        public JsonResult ValidationTrainingAnswerType(string LSTrainingAnswerTypeCode, string InitialTrainingAnswerType)
        {
            var repository = new TrainingAnswerTypeRepository(db); 
            string errorMessage;
            if (repository.IsExisted(LSTrainingAnswerTypeCode, out errorMessage) && (LSTrainingAnswerTypeCode != InitialTrainingAnswerType))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + LSTrainingAnswerTypeCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + LSTrainingAnswerTypeCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For TrainingApprisalPart
        public JsonResult ValidationTrainingApprisalPart(string LSTrainingApprisalPartCode, string InitialTrainingApprisalPart)
        {
            var repository = new TrainingApprisalPartRepository(db);
            string errorMessage;
            if (repository.IsExisted(LSTrainingApprisalPartCode, out errorMessage) && (LSTrainingApprisalPartCode != InitialTrainingApprisalPart))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + LSTrainingApprisalPartCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + LSTrainingApprisalPartCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For TrainingApprisalItem
        public JsonResult ValidationTrainingApprisalItem(string LSTrainingApprisalItemCode, string InitialTrainingApprisalItem)
        {
            var repository = new TrainingApprisalItemRepository(db);
            string errorMessage;
            if (repository.IsExisted(LSTrainingApprisalItemCode, out errorMessage) && (LSTrainingApprisalItemCode != InitialTrainingApprisalItem))
            {
                if (LanguageId == 124)
                {
                    return base.Json("\"" + LSTrainingApprisalItemCode + "\" is exists!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return base.Json("\"" + LSTrainingApprisalItemCode + "\" đ\x00e3 tồn tại!", JsonRequestBehavior.AllowGet);
                }
            }

            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
