using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model.TER;

namespace Eagle.Repository.Termination
{
    public class TerminationRespository
    {
        public EntityDataContext context { get; set; }

        public TerminationRespository(EntityDataContext context)
        {
            this.context = context;
        }

        //Type: 1:Ngay hien tai, 7:tuan hien tai, 30:thang hien tai, mac dinh la thang hien tai
        public static List<TerminationViewModel> GetTerminationList(int? Type, int LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = from p in context.TER_tblTermination
                            join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                            join com in context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into CompanyList
                            from com in CompanyList.DefaultIfEmpty()
                            join position in context.LS_tblPosition on emp.LSPositionID equals position.LSPositionID into postmp
                            from pos in postmp.DefaultIfEmpty()
                            join r in context.LS_tblTerminationReason on p.LSTerminationReasonID equals r.LSTerminationReasonID into reasonlist
                            from reason in reasonlist.DefaultIfEmpty()
                            join t in context.LS_tblTerminationType on p.LSTerminationTypeID equals t.LSTerminationTypeID into typelist
                            from type in typelist.DefaultIfEmpty()
                            where emp.Active == false
                            orderby p.LastWorkingDate
                            select new TerminationViewModel()
                            {
                                TerminationID = p.TerminationID,
                                EmpID = p.EmpID,
                                EmpCode = emp.EmpCode,
                                FullName = emp.LastName + " " + emp.FirstName,
                                Gender = emp.Gender,
                                Email = emp.Email,
                                Telephone = emp.Telephone,
                                Mobile = emp.Mobile,
                                FileId = emp.FileId,
                                LSCompanyID = emp.LSCompanyID,
                                Department = (LanguageId == 124) ? com.Name : com.VNName,
                                Position = (LanguageId == 124 && pos != null) ? pos.Name : pos.VNName,
                                LSTerminationReasonID = p.LSTerminationReasonID,
                                LSTerminationReasonName = (LanguageId == 124) ? reason.Name : reason.VNName,
                                LSTerminationTypeID = p.LSTerminationTypeID,
                                LSTerminationTypeName = (LanguageId == 124) ? type.Name : type.VNName,
                                Description = p.Description,
                                JoinedDate = emp.JoinDate,
                                InformedDate = p.InformedDate,
                                LastWorkingDate = p.LastWorkingDate,
                                EffectiveDate = p.EffectiveDate,
                                IsTerminationPaid = p.IsTerminationPaid,
                                ReturnDateForInsuranceCard = p.ReturnDateForInsuranceCard,
                                MonthsOfUnPaidInsuranceCard = p.MonthsOfUnPaidInsuranceCard,
                                DecisionNo = p.DecisionNo,
                                SignDate = p.SignDate,
                                EmpIDSigner = p.EmpIDSigner,                               
                                FileIds = p.FileIds
                            };

                List<TerminationViewModel> lst = new List<TerminationViewModel>();
                long result = query.Count();
                int CurrentDay = DateTime.Today.Day;

                switch (Type)
                {
                    case 1: //Ngay hien tai
                        lst = query.Where(p => p.LastWorkingDate.Day == CurrentDay).ToList();
                        break;
                    case 7: //Tuan hien tai
                        int FirstDayOfCurrentWeek = DateTimeUtils.FirstDayOfCurrentWeek();
                        int LastDayOfCurrentWeek = DateTimeUtils.LastDayOfCurrentWeek();
                        lst = query.Where(p => p.LastWorkingDate.Day >= FirstDayOfCurrentWeek && p.LastWorkingDate.Day >= LastDayOfCurrentWeek).ToList();
                        break;
                    case 30: //Thang hien tai
                        int FirstDayOfCurrentMonth = DateTimeUtils.FirstDayOfCurrentMonth();
                        int LastDayOfCurrentMonth = DateTimeUtils.LastDayOfCurrentMonth();
                        lst = query.Where(p => p.LastWorkingDate.Day >= FirstDayOfCurrentMonth && p.LastWorkingDate.Day <= LastDayOfCurrentMonth).ToList();
                        break;
                    default:
                        //Thang hien tai
                        lst = query.Where(p => p.LastWorkingDate.Day == CurrentDay).ToList();
                        break;
                }
                return lst;
            }
        }
        
        public IQueryable<TerminationViewModel> GetList(int? ModuleId, string UserName, bool isAdmin)
        {           
            var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();

            var lst = (from p in context.TER_tblTermination
                        join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                        join com in context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into CompanyList
                        from com in CompanyList.DefaultIfEmpty()
                        join r in context.LS_tblTerminationReason on p.LSTerminationReasonID equals r.LSTerminationReasonID into reasonlist
                        from reason in reasonlist.DefaultIfEmpty()
                        join t in context.LS_tblTerminationType on p.LSTerminationTypeID equals t.LSTerminationTypeID into typelist
                        from type in typelist.DefaultIfEmpty()
                        where emp.Active == false  && (jointable.Contains(p.EmpID))
                        select new TerminationViewModel()
                        {
                            TerminationID = p.TerminationID,
                            EmpID = p.EmpID,
                            EmpCode = emp.EmpCode,
                            FullName = emp.LastName + " " + emp.FirstName,
                            LSCompanyID = emp.LSCompanyID,
                            LSTerminationReasonID = p.LSTerminationReasonID,
                            LSTerminationReasonName = reason.Name,
                            LSTerminationTypeID = p.LSTerminationTypeID,
                            LSTerminationTypeName = type.Name,
                            Description = p.Description,
                            InformedDate = p.InformedDate,
                            LastWorkingDate = p.LastWorkingDate,
                            EffectiveDate = p.EffectiveDate,
                            IsTerminationPaid = p.IsTerminationPaid,
                            ReturnDateForInsuranceCard = p.ReturnDateForInsuranceCard,
                            MonthsOfUnPaidInsuranceCard = p.MonthsOfUnPaidInsuranceCard,
                            DecisionNo = p.DecisionNo,
                            SignDate = p.SignDate,
                            EmpIDSigner = p.EmpIDSigner,
                            FileIds = p.FileIds
                        }).OrderByDescending(c => c.TerminationID);               
            return lst;        
        }

        public List<TerminationViewModel> Search(int? LSCompanyID, string FullName, string EmpCode, string InformedDate, string EffectiveDate, int? ModuleId, string UserName, bool isAdmin)
        {
            var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            var dateformat = new System.Globalization.DateTimeFormatInfo();
            dateformat.ShortDatePattern = "MM/dd/yyyy";
            culture.DateTimeFormat = dateformat;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            List<TerminationViewModel> list = GetList(ModuleId, UserName, isAdmin).ToList();
            int count = list.Count();
           

            if (LSCompanyID != null && LSCompanyID > 0)
            {
                List<int> company_lst = LS_tblCompanyRepository.GetTreeIdListByNodeId(LSCompanyID);
                list = list.Where(p => company_lst.Contains(p.LSCompanyID)).ToList();
            }
            if (!string.IsNullOrEmpty(FullName))
            {
                string UnSigned_FullName = StringUtils.ConvertToUnSign(FullName);
                list = list.Where(x => x.FullName.ToLower().Contains(FullName.ToLower()) || x.FullName.ToLower().Contains(UnSigned_FullName.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(EmpCode))
            {
                list = list.Where(x => x.EmpCode.ToLower().Contains(EmpCode.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(InformedDate) && string.IsNullOrEmpty(EffectiveDate))
            {
                DateTime? _InformedDate = Convert.ToDateTime(InformedDate);
                list = list.Where(x => x.InformedDate == _InformedDate).ToList();
            }
            if (!string.IsNullOrEmpty(EffectiveDate) && string.IsNullOrEmpty(InformedDate))
            {
                DateTime? _EffectiveDate = Convert.ToDateTime(EffectiveDate);
                list = list.Where(x => x.EffectiveDate == _EffectiveDate).ToList();
            }
            if (!string.IsNullOrEmpty(EffectiveDate) && !string.IsNullOrEmpty(InformedDate))
            {
                DateTime? _informedDate = Convert.ToDateTime(InformedDate);
                DateTime? _effectiveDate = Convert.ToDateTime(EffectiveDate);
                list = list.Where(x => x.InformedDate == _informedDate && x.EffectiveDate == _effectiveDate).ToList();
            }  
            return list;
        } 

        public static TerminationViewModel Details(int? ID)
        {
            TerminationViewModel entity = new TerminationViewModel();
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    entity = (from p in context.TER_tblTermination
                              where p.TerminationID == ID
                              select new TerminationViewModel()
                              {
                                  TerminationID = p.TerminationID,
                                  EmpID = p.EmpID,
                                  LSTerminationReasonID = p.LSTerminationReasonID,
                                  LSTerminationTypeID = p.LSTerminationTypeID,
                                  Description = p.Description,
                                  InformedDate = p.InformedDate,
                                  LastWorkingDate = p.LastWorkingDate,
                                  EffectiveDate = p.EffectiveDate,
                                  IsTerminationPaid = p.IsTerminationPaid,
                                  ReturnDateForInsuranceCard = p.ReturnDateForInsuranceCard,
                                  MonthsOfUnPaidInsuranceCard = p.MonthsOfUnPaidInsuranceCard,
                                  DecisionNo = p.DecisionNo,
                                  SignDate = p.SignDate,
                                  EmpIDSigner = p.EmpIDSigner,
                                  FileIds = p.FileIds
                              }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return entity;
            }
        }

        public static TerminationViewModel GetDetailByEmpID(int? EmpID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                TerminationViewModel entity = new TerminationViewModel();
                entity = (from p in context.TER_tblTermination
                          where p.EmpID == EmpID
                          select new TerminationViewModel()
                          {
                              TerminationID = p.TerminationID,
                              EmpID = p.EmpID,
                              LSTerminationReasonID = p.LSTerminationReasonID,
                              LSTerminationTypeID = p.LSTerminationTypeID,
                              Description = p.Description,
                              InformedDate = p.InformedDate,
                              LastWorkingDate = p.LastWorkingDate,
                              EffectiveDate = p.EffectiveDate,
                              IsTerminationPaid = p.IsTerminationPaid,
                              ReturnDateForInsuranceCard = p.ReturnDateForInsuranceCard,
                              MonthsOfUnPaidInsuranceCard = p.MonthsOfUnPaidInsuranceCard,
                              DecisionNo = p.DecisionNo,
                              SignDate = p.SignDate,
                              EmpIDSigner = p.EmpIDSigner,
                              FileIds = p.FileIds
                          }).FirstOrDefault();
                return entity;
            }
        }
        
        public static bool IsIDExists(int ID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.TER_tblTermination.FirstOrDefault(c => c.TerminationID.Equals(ID));
                return (query != null);
            }
        }

        public static bool IsEmpIDExists(int EmpID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.TER_tblTermination.FirstOrDefault(c => c.EmpID.Equals(EmpID));
                return (query != null);
            }
        }

        public static TER_tblTermination Find(int? ID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.TER_tblTermination.Find(ID);
            }
        }

        public static bool Add(TerminationViewModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    bool isDuplicate = IsEmpIDExists(model.EmpID);
                    if (isDuplicate == false)
                    {
                        TER_tblTermination entity = new TER_tblTermination();
                        entity.EmpID = model.EmpID;
                        entity.LSTerminationReasonID = model.LSTerminationReasonID;
                        entity.LSTerminationTypeID = model.LSTerminationTypeID;
                        entity.Description = model.Description;
                        entity.InformedDate = model.InformedDate;
                        entity.LastWorkingDate = model.LastWorkingDate;
                        entity.EffectiveDate = model.EffectiveDate;
                        entity.IsTerminationPaid = model.IsTerminationPaid;
                        entity.ReturnDateForInsuranceCard = model.ReturnDateForInsuranceCard;
                        entity.MonthsOfUnPaidInsuranceCard = model.MonthsOfUnPaidInsuranceCard;
                        entity.DecisionNo = model.DecisionNo;
                        entity.SignDate = model.SignDate;
                        entity.EmpIDSigner = model.EmpID;
                        entity.FileIds = model.FileIds;

                        int affectedRow = 0;
                        context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            model.TerminationID = entity.TerminationID;

                            EmployeeRepository emp_respository = new EmployeeRepository(context);
                            bool flag = emp_respository.DeactivateStatus(entity.EmpID);
                            if (flag == true)
                            {
                                Message = Eagle.Resource.LanguageResource.CreateSuccess;
                                result = true;
                            }
                        }
                    }
                    else
                    {
                        result = false;
                        Message = Eagle.Resource.LanguageResource.DuplicateError;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                    Message = Eagle.Resource.LanguageResource.SystemError;
                }
            }
            return result;
        }

        public static bool Update(TerminationViewModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    bool IDExist = IsIDExists(model.TerminationID);
                    if (IDExist == true)
                    {
                        TER_tblTermination entity = Find(model.TerminationID);
                        entity.EmpID = model.EmpID;
                        entity.LSTerminationReasonID = model.LSTerminationReasonID;
                        entity.LSTerminationTypeID = model.LSTerminationTypeID;
                        entity.Description = model.Description;
                        entity.InformedDate = model.InformedDate;
                        entity.LastWorkingDate = model.LastWorkingDate;
                        entity.EffectiveDate = model.EffectiveDate;
                        entity.IsTerminationPaid = model.IsTerminationPaid;
                        entity.ReturnDateForInsuranceCard = model.ReturnDateForInsuranceCard;
                        entity.MonthsOfUnPaidInsuranceCard = model.MonthsOfUnPaidInsuranceCard;
                        entity.DecisionNo = model.DecisionNo;
                        entity.SignDate = model.SignDate;
                        entity.EmpIDSigner = model.EmpID;
                        entity.FileIds = model.FileIds;

                        context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int affectedRows = context.SaveChanges();
                        if (affectedRows == 1)
                        {
                            Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                            result = true;
                        }
                    }
                    else
                    {
                        result = Add(model, out Message);
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                    Message = Eagle.Resource.LanguageResource.SystemError;
                }
            }
            return result;
        }

        public static bool UpdateFileIds(int Id, string FileIds, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    TER_tblTermination entity = Find(Id);
                    entity.FileIds = FileIds;

                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                    Message = Eagle.Resource.LanguageResource.SystemError;
                }
            }
            return result;
        }

        public static bool Delete(int id, out string message)
        {
            bool result = false;
            message = Eagle.Resource.LanguageResource.UpdateFailed;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    TER_tblTermination entity = Find(id);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        EmployeeRepository emp_respository = new EmployeeRepository(context);
                        bool flag = emp_respository.ActivateStatus(entity.EmpID);
                        if (flag == true)
                        {
                            result = true;
                            message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        }
                    }
                    else
                        result = false;
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                result = false;
            }
            return result;
        }
    }
}
