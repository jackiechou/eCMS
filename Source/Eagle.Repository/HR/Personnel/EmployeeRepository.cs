using Eagle.Common.Data;
using Eagle.Common.Utilities;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Model.HR;
using Eagle.Model.Report.HR.Department;
using Eagle.Model.Report.HR.Position;
using Eagle.Model.Report.HR.Qualification;
using Eagle.Common.Settings;

namespace Eagle.Repository.HR
{
    public class EmployeeRepository
    {
        public EntityDataContext context { get; set; }

        public EmployeeRepository(EntityDataContext context)
        {
            this.context = context;
        }

        //Type: 1:Ngay hien tai, 30:thang hien tai, mac dinh la thang hien tai
        public static List<EmployeeViewModel> LoadEmpListByBirthday(int? SelectedMonth, int? Type, int LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                if (Type == 1)
                    SelectedMonth = DateTime.Today.Month;                

                var query = from e in context.HR_tblEmp
                            join com in context.LS_tblCompany on e.LSCompanyID equals com.LSCompanyID into CompanyList
                            from com in CompanyList.DefaultIfEmpty()

                            join location in context.LS_tblLocation on e.LSLocationID equals location.LSLocationID into locationtmp
                            from locate in locationtmp.DefaultIfEmpty()

                            join position in context.LS_tblPosition on e.LSPositionID equals position.LSPositionID into postmp
                            from pos in postmp.DefaultIfEmpty()
                            where (e.Active == true || e.Active != null) && e.DOB != null
                                && e.DOB.Value.Month == ((SelectedMonth != null && SelectedMonth > 0) ? SelectedMonth : DateTime.Today.Month)
                            orderby e.DOB
                            select new EmployeeViewModel
                            {
                                EmpID = e.EmpID,
                                EmpCode = e.EmpCode,
                                FirstName = e.FirstName,
                                LastName = e.LastName,
                                FullName = e.LastName + " " + e.FirstName,
                                Gender = e.Gender,
                                BloodType = e.BloodType,
                                Email = e.Email,
                                Telephone = e.Telephone,
                                Mobile = e.Mobile,
                                LSMaritalID = e.LSMaritalID,
                                LSNationalityID = e.LSNationalityID,
                                LSEthnicID = e.LSEthnicID,
                                LSReligionID = e.LSReligionID,
                                LSEducationID = e.LSEducationID,
                                LSMajorID = e.LSMajorID,
                                LineManagerID = e.LineManagerID,
                                LSLocationID = e.LSLocationID,
                                LSPositionID = e.LSPositionID,
                                DOB = e.DOB,
                                Department = (LanguageId == 124) ? com.Name : com.VNName,
                                Position = (LanguageId == 124 && pos != null) ? pos.Name : pos.VNName,
                                JoinDate = e.JoinDate,
                                FileId = e.FileId,
                                Active = e.Active
                            };

                List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
                long result = query.Count();
                int CurrentDay = DateTime.Today.Day;
                int FirstDayOfCurrentMonth = DateTimeUtils.FirstDayOfCurrentMonth();
                int LastDayOfCurrentMonth = DateTimeUtils.LastDayOfCurrentMonth();

                switch (Type)
                {
                    case 1:
                        lst = query.Where(e => e.DOB.Value.Day == CurrentDay).ToList();
                        break;                 
                    case 30:
                        lst = query.Where(e => e.DOB.Value.Day >= FirstDayOfCurrentMonth && e.DOB.Value.Day <= LastDayOfCurrentMonth).ToList();
                        break;
                    default:
                        lst = query.Where(e => e.DOB.Value.Day >= FirstDayOfCurrentMonth && e.DOB.Value.Day <= LastDayOfCurrentMonth).ToList();
                        break;
                }
                return lst;
            }
        }

        //Type: 1:Ngay hien tai theo thang, 7:tuan hien tai theo thang, 30:thang hien tai, mac dinh la thang hien tai
        public static List<EmployeeViewModel> GetEmpListByBirthday(int? SelectedMonth, int? Type, int LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {                
                var query = from e in context.HR_tblEmp
                            join com in context.LS_tblCompany on e.LSCompanyID equals com.LSCompanyID into CompanyList
                            from com in CompanyList.DefaultIfEmpty()

                            join location in context.LS_tblLocation on e.LSLocationID equals location.LSLocationID into locationtmp
                            from locate in locationtmp.DefaultIfEmpty()

                            join position in context.LS_tblPosition on e.LSPositionID equals position.LSPositionID into postmp
                            from pos in postmp.DefaultIfEmpty()
                            where (e.Active == true || e.Active != null) && e.DOB != null 
                                && e.DOB.Value.Month == ((SelectedMonth != null && SelectedMonth > 0) ? SelectedMonth : DateTime.Today.Month)
                            orderby e.DOB
                            select new EmployeeViewModel
                            {
                                EmpID = e.EmpID,
                                EmpCode = e.EmpCode,
                                FirstName = e.FirstName,
                                LastName = e.LastName,
                                FullName = e.LastName + " " + e.FirstName,
                                Gender = e.Gender,
                                BloodType = e.BloodType,
                                Email = e.Email,
                                Telephone = e.Telephone,
                                Mobile = e.Mobile,
                                LSMaritalID = e.LSMaritalID,
                                LSNationalityID = e.LSNationalityID,
                                LSEthnicID = e.LSEthnicID,
                                LSReligionID = e.LSReligionID,
                                LSEducationID = e.LSEducationID,
                                LSMajorID = e.LSMajorID,
                                LineManagerID = e.LineManagerID,
                                LSLocationID = e.LSLocationID,
                                LSPositionID = e.LSPositionID,
                                DOB = e.DOB,
                                Department = (LanguageId == 124) ? com.Name : com.VNName,
                                Position = (LanguageId == 124 && pos != null) ? pos.Name : pos.VNName,
                                JoinDate = e.JoinDate,
                                FileId= e.FileId,
                                Active = e.Active
                            };

                List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
                long result = query.Count();
                int CurrentDay = DateTime.Today.Day;
                int FirstDayOfCurrentMonth = DateTimeUtils.FirstDayOfCurrentMonth();
                int LastDayOfCurrentMonth = DateTimeUtils.LastDayOfCurrentMonth();

                switch (Type)
                {
                    case 1:
                        lst = query.Where(e => e.DOB.Value.Day == CurrentDay).ToList();
                        break;
                    case 7:
                        int FirstDayOfCurrentWeek = DateTimeUtils.FirstDayOfCurrentWeek();
                        int LastDayOfCurrentWeek = DateTimeUtils.LastDayOfCurrentWeek();
                        lst = query.Where(e => e.DOB.Value.Day >= FirstDayOfCurrentWeek && e.DOB.Value.Day >= LastDayOfCurrentWeek).ToList();
                        break;
                    case 30:           
                        lst = query.Where(e => e.DOB.Value.Day >= FirstDayOfCurrentMonth && e.DOB.Value.Day <= LastDayOfCurrentMonth).ToList();
                        break;
                    default:
                        lst = query.Where(e => e.DOB.Value.Day >= FirstDayOfCurrentMonth && e.DOB.Value.Day <= LastDayOfCurrentMonth).ToList();
                        break;
                }
                return lst;
            }
        }

        //Type: 1:Ngay hien tai, 7:tuan hien tai, 30:thang hien tai, mac dinh la ngay hien tai
        public static List<EmployeeViewModel> GetEmpBirthdayList(int? Type, int LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = from e in context.HR_tblEmp
                                    join com in context.LS_tblCompany on e.LSCompanyID equals com.LSCompanyID into CompanyList
                                    from com in CompanyList.DefaultIfEmpty()

                                    join location in context.LS_tblLocation on e.LSLocationID equals location.LSLocationID into locationtmp
                                    from locate in locationtmp.DefaultIfEmpty()

                                    join position in context.LS_tblPosition on e.LSPositionID equals position.LSPositionID into postmp
                                    from pos in postmp.DefaultIfEmpty()
                                    where (e.Active == true || e.Active != null) && e.DOB != null && e.DOB.Value.Month == DateTime.Today.Month
                                    orderby e.DOB
                                    select new EmployeeViewModel
                                    {
                                        EmpID = e.EmpID,
                                        EmpCode = e.EmpCode,
                                        FirstName = e.FirstName,
                                        LastName = e.LastName,
                                        FullName = e.LastName + " " + e.FirstName,
                                        Gender = e.Gender,
                                        BloodType = e.BloodType,
                                        Email = e.Email,
                                        Telephone = e.Telephone,
                                        Mobile = e.Mobile,                                  
                                        LSMaritalID = e.LSMaritalID,
                                        LSNationalityID = e.LSNationalityID,
                                        LSEthnicID = e.LSEthnicID,
                                        LSReligionID = e.LSReligionID,
                                        LSEducationID = e.LSEducationID,
                                        LSMajorID = e.LSMajorID,
                                        LineManagerID = e.LineManagerID,
                                        LSLocationID = e.LSLocationID,
                                        LSPositionID = e.LSPositionID,
                                        DOB = e.DOB,
                                        Department = (LanguageId == 124) ? com.Name : com.VNName,
                                        Position = (LanguageId == 124 && pos != null)? pos.Name : pos.VNName,
                                        JoinDate = e.JoinDate,
                                        Active = e.Active
                                    };

                List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
                long result = query.Count();
                int CurrentDay = DateTime.Today.Day;

                switch (Type)
                {
                    case 1:                        
                        lst = query.Where(e => e.DOB.Value.Day == CurrentDay).ToList();
                        break;
                    case 7:
                        int FirstDayOfCurrentWeek = DateTimeUtils.FirstDayOfCurrentWeek();
                        int LastDayOfCurrentWeek = DateTimeUtils.LastDayOfCurrentWeek();
                        lst = query.Where(e => e.DOB.Value.Day >= FirstDayOfCurrentWeek && e.DOB.Value.Day >= LastDayOfCurrentWeek).ToList();
                        break;
                    case 30:
                        int FirstDayOfCurrentMonth = DateTimeUtils.FirstDayOfCurrentMonth();
                        int LastDayOfCurrentMonth = DateTimeUtils.LastDayOfCurrentMonth();

                        lst = query.Where(e => e.DOB.Value.Day >= FirstDayOfCurrentMonth && e.DOB.Value.Day <= LastDayOfCurrentMonth).ToList();
                        break;
                    default:
                        lst = query.Where(e => e.DOB.Value.Day == CurrentDay).ToList();
                        break;
                }
                return lst;
            }            
        }

        public static List<EmployeeViewModel> GetProbationaryList(int? Type, int LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                DateTime FirstDateOfPredefinedPreviousMonth = DateTimeUtils.FirstDateOfPredefinedPreviousMonth(2);
                DateTime LastDateOfPredefinedPreviousMonth = DateTimeUtils.LastDateOfPredefinedPreviousMonth(2);

                var query = from e in context.HR_tblEmp
                            join com in context.LS_tblCompany on e.LSCompanyID equals com.LSCompanyID into CompanyList
                            from com in CompanyList.DefaultIfEmpty()

                            join location in context.LS_tblLocation on e.LSLocationID equals location.LSLocationID into locationtmp
                            from locate in locationtmp.DefaultIfEmpty()

                            join position in context.LS_tblPosition on e.LSPositionID equals position.LSPositionID into postmp
                            from pos in postmp.DefaultIfEmpty()
                            where (e.Active == true || e.Active != null) && e.JoinDate.Value >= FirstDateOfPredefinedPreviousMonth && e.JoinDate.Value <= LastDateOfPredefinedPreviousMonth
                            orderby e.JoinDate
                            select new EmployeeViewModel
                            {
                                EmpID = e.EmpID,
                                EmpCode = e.EmpCode,
                                FirstName = e.FirstName,
                                LastName = e.LastName,
                                FullName = e.LastName + " " + e.FirstName,
                                Gender = e.Gender,
                                BloodType = e.BloodType,
                                Email = e.Email,
                                Telephone = e.Telephone,
                                Mobile = e.Mobile,
                                LSMaritalID = e.LSMaritalID,
                                LSNationalityID = e.LSNationalityID,
                                LSEthnicID = e.LSEthnicID,
                                LSReligionID = e.LSReligionID,
                                LSEducationID = e.LSEducationID,
                                LSMajorID = e.LSMajorID,
                                LineManagerID = e.LineManagerID,
                                LSLocationID = e.LSLocationID,
                                LSPositionID = e.LSPositionID,
                                DOB = e.DOB,                               
                                Department = (LanguageId == 124) ? com.Name : com.VNName,
                                Position = (LanguageId == 124 && pos != null) ? pos.Name : pos.VNName,
                                JoinDate = e.JoinDate,
                                Active = e.Active
                            };

                List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
                long result = query.Count();
                int CurrentDay = DateTime.Today.Day;

                switch (Type)
                {
                    case 1:
                        lst = query.Where(e => e.JoinDate.Value.Day == CurrentDay).ToList();
                        break;
                    case 7:
                        int FirstDayOfCurrentWeek = DateTimeUtils.FirstDayOfCurrentWeek();
                        int LastDayOfCurrentWeek = DateTimeUtils.LastDayOfCurrentWeek();
                        lst = query.Where(e => e.JoinDate.Value.Day >= FirstDayOfCurrentWeek && e.JoinDate.Value.Day >= LastDayOfCurrentWeek).ToList();
                        break;
                    case 60:
                        lst = query.ToList();
                        break;
                    default:
                        lst = query.ToList();
                        break;
                }
                return lst;
            } 
        }

        public List<EmployeeViewModel> FindEmployee(string EmpCode, string FullName, int? LSCompanyID, bool? Active, string userGroupID, int? moduleID, bool isAdmin, int LanguageId = 10001)
        {
            var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

            string Lineage = "";
            if (LSCompanyID != null && LSCompanyID != 0)
                Lineage = context.LS_tblCompany.Find(LSCompanyID).Lineage;

            List<EmployeeViewModel> list = new List<EmployeeViewModel>();
            list = (from empl in context.HR_tblEmp
                              join com in context.LS_tblCompany on empl.LSCompanyID equals com.LSCompanyID into CompanyList
                              from com in CompanyList.DefaultIfEmpty()

                              join location in context.LS_tblLocation on empl.LSLocationID equals location.LSLocationID into locationtmp
                              from locate in locationtmp.DefaultIfEmpty()

                              join position in context.LS_tblPosition on empl.LSPositionID equals position.LSPositionID into postmp
                              from pos in postmp.DefaultIfEmpty()
                              where (jointable.Contains(empl.EmpID)) &&                                
                              (LSCompanyID == null || LSCompanyID == 0 || com.Lineage.Contains(Lineage)) &&
                              (empl.Active == Active || Active == null)
                              select new EmployeeViewModel
                              {
                                  EmpID = empl.EmpID,
                                  EmpCode = empl.EmpCode,
                                  FirstName = empl.FirstName,
                                  LastName = empl.LastName,
                                  FullName = empl.LastName + " " + empl.FirstName,
                                  Gender = empl.Gender,
                                  BloodType = empl.BloodType,
                                  Email = empl.Email,
                                  Telephone = empl.Telephone,
                                  Mobile = empl.Mobile,
                                  PAddress = empl.PAddress,
                                  TAddress = empl.TAddress,
                                  LSMaritalID = empl.LSMaritalID,
                                  LSNationalityID = empl.LSNationalityID,
                                  LSEthnicID = empl.LSEthnicID,
                                  LSReligionID = empl.LSReligionID,
                                  LSEducationID = empl.LSEducationID,                               
                                  LSMajorID = empl.LSMajorID,
                                  LineManagerID = empl.LineManagerID,
                                  LSLocationID = empl.LSLocationID,
                                  LSPositionID = empl.LSPositionID,
                                  DOB = empl.DOB,
                                  JoinDate = empl.JoinDate,
                                  Active = empl.Active,
                                  CompanyName = (LanguageId ==10001)?com.Name:com.VNName,
                                  LSLocationName = (LanguageId == 124) ? locate.Name : locate.VNName,
                                  Position = (LanguageId == 124 && pos != null) ? pos.Name : pos.VNName
                              }).ToList();

            if (!string.IsNullOrEmpty(EmpCode))
                list = list.Where(x => x.EmpCode.ToLower().Contains(EmpCode.ToLower())).ToList();

            if (!string.IsNullOrEmpty(FullName))
            {
                string UnSigned_FullName = StringUtils.ConvertToUnSign(FullName);
                list = list.Where(x => x.FullName.ToLower().Contains(FullName.ToLower()) || x.FullName.ToLower().Contains(UnSigned_FullName.ToLower())).ToList();
            }
            return list;
        }

        public static EmployeeViewModel GetBriefDetails(int? EmpID, int? LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                EmployeeViewModel entity = new EmployeeViewModel();
                entity = (from empl in context.HR_tblEmp
                              join c in context.LS_tblCompany on empl.LSCompanyID equals c.LSCompanyID into companylist
                              from com in companylist.DefaultIfEmpty()

                              join location in context.LS_tblLocation on empl.LSLocationID equals location.LSLocationID into locationtmp
                              from location in locationtmp.DefaultIfEmpty()

                              join pos in context.LS_tblPosition on empl.LSPositionID equals pos.LSPositionID into postmp
                              from pos in postmp.DefaultIfEmpty()
                              where empl.EmpID == EmpID
                              select new Eagle.Model.HR.EmployeeViewModel
                              {
                                  EmpID = empl.EmpID,
                                  EmpCode = empl.EmpCode,
                                  FirstName = empl.FirstName,
                                  LastName = empl.LastName,
                                  FullName = empl.LastName + " " + empl.FirstName,
                                  Gender = empl.Gender,
                                  BloodType = empl.BloodType,
                                  Email = empl.Email,
                                  Telephone = empl.Telephone,
                                  Mobile = empl.Mobile,
                                  PAddress = empl.PAddress,
                                  TAddress = empl.TAddress,
                                  LineManagerID = empl.LineManagerID,
                                  LSLocationID = empl.LSLocationID,
                                  LSPositionID = empl.LSPositionID,
                                  DOB = empl.DOB,
                                  JoinDate = empl.JoinDate,
                                  Active = empl.Active,
                                  CompanyName = (LanguageId == 124) ? com.Name : com.VNName,
                                  LSLocationName = (LanguageId == 124) ? location.Name : location.VNName,
                                  Position = (LanguageId == 124 && pos != null) ? pos.Name : pos.VNName
                              }).FirstOrDefault();
                return entity;
            }    
        }

        public Eagle.Model.HR.EmployeeViewModel GetBrief(int? EmpID, int? LanguageId)
        {
            var entity = (from empl in context.HR_tblEmp
                          join com in context.LS_tblCompany on empl.LSCompanyID equals com.LSCompanyID

                          join location in context.LS_tblLocation on empl.LSLocationID equals location.LSLocationID into locationtmp
                          from location in locationtmp.DefaultIfEmpty()

                          join pos in context.LS_tblPosition on empl.LSPositionID equals pos.LSPositionID into postmp
                          from pos in postmp.DefaultIfEmpty()
                          where empl.EmpID == EmpID
                          select new Eagle.Model.HR.EmployeeViewModel
                          {
                              EmpID = empl.EmpID,
                              EmpCode = empl.EmpCode,
                              FirstName = empl.FirstName,
                              LastName = empl.LastName,
                              FullName = empl.LastName + " " + empl.FirstName,
                              Gender = empl.Gender,
                              BloodType = empl.BloodType,
                              Email = empl.Email,
                              Telephone = empl.Telephone,
                              Mobile = empl.Mobile,
                              PAddress = empl.PAddress,
                              TAddress = empl.TAddress,
                              LSMaritalID = empl.LSMaritalID,
                              LSNationalityID = empl.LSNationalityID,
                              LSEthnicID = empl.LSEthnicID,
                              LSReligionID = empl.LSReligionID,
                              LSEducationID = empl.LSEducationID,
                              LSMajorID = empl.LSMajorID,
                              LineManagerID = empl.LineManagerID,
                              
                              LSLocationID = empl.LSLocationID,
                              LSPositionID = empl.LSPositionID,
                              DOB = empl.DOB,
                              JoinDate = empl.JoinDate,
                              Active = empl.Active,
                              CompanyName = (LanguageId == 124) ? com.Name : com.VNName,
                              LSLocationName = (LanguageId == 124) ? location.Name : location.VNName,
                              Position = pos != null ? pos.Name : null
                          }).FirstOrDefault();
            entity.LineManagerName = GetManagerName(entity.LineManagerID);
            return entity;
        } 

        public List<EmployeeEntity> GetListForDropDownList()
        {
            try
            {
                var result = from p in context.HR_tblEmp
                             where p.Active == true
                             select new EmployeeEntity()
                            {
                                Id = p.EmpID,
                                Name = p.LastName + " - " + p.FirstName  
                            };
                return result.ToList();
            }
            catch
            {
                return new List<EmployeeEntity>();
            }
        }

        public static List<SelectItem> GetEmailsForDropDownList()
        {
            try
            {
                List<SelectItem> lst= new List<SelectItem>();
                using (EntityDataContext context = new EntityDataContext())
                {
                    lst = (context.HR_tblEmp.Select(p => new SelectItem()
                    {
                        id = p.Email,
                        text = p.LastName + " - " + p.FirstName  
                    })).ToList();
                    return lst;
                }                
            }
            catch
            {
                return new List<SelectItem>();
            }
        }

        public List<EmployeeViewModel> GetList(int? ModuleId, string UserName, bool isAdmin, int? LanguageId)
        {
            List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
            var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
            var _lst = from emp in context.HR_tblEmp
                       join t in context.TER_tblTermination on emp.EmpID equals t.EmpID into terminationlist
                       from termination in terminationlist.DefaultIfEmpty()
                       join f in context.ApplicationFiles on emp.FileId equals f.FileId into filelist
                       from file in filelist.DefaultIfEmpty()
                       where (jointable.Contains(emp.EmpID))
                       select emp;
            lst = _lst.Select(p => new EmployeeViewModel()
            {
                EmpID = p.EmpID,
                EmpCode = p.EmpCode,
                LastName = p.LastName,
                FirstName = p.FirstName,
                //FullName = ((p.Gender > 0) ? "Mr. " : "Ms. ") + p.LastName + " "+ p.FirstName,
                FullName = p.LastName + " " + p.FirstName,
                Gender = p.Gender,
                BloodType = p.BloodType,
                Email = p.Email,
                Telephone = p.Telephone,
                Mobile = p.Mobile,
                FileId = p.FileId,
                FileUrl = "",
                IDNo = p.IDNo,

                IDIssuedDate = p.IDIssuedDate,
                IDIssuedPlace = p.IDIssuedPlace,
                TaxNo = p.TaxNo,

                PAddress = p.PAddress,
                PLSCountryID = p.PLSCountryID,
                PLSProvinceID = p.PLSProvinceID,
                PLSDistrictID = p.PLSDistrictID,
                TLSCountryID = p.TLSCountryID,
                TLSProvinceID = p.TLSProvinceID,
                TLSDistrictID = p.TLSDistrictID,
                TAddress = p.TAddress,
                LSCompanyID = p.LSCompanyID,
                DOB = p.DOB,
                YOB = p.YOB,
                StartDate = p.StartDate,
                JoinDate = p.JoinDate,
                BornLSCountryID = p.BornLSCountryID,
                BornLSProvinceID = p.BornLSProvinceID,
                BornLSDistrictID = p.BornLSDistrictID,
                NativeCountryID = p.NativeCountryID,
                NativeProvinceID = p.NativeProvinceID,
                NativeDistrictID = p.NativeDistrictID,
                LSMaritalID = p.LSMaritalID,
                LSNationalityID = p.LSNationalityID,
                LSEthnicID = p.LSEthnicID,
                LSReligionID = p.LSReligionID,
                LSEducationID = p.LSEducationID,
                LSMajorID = p.LSMajorID,
                LineManagerID = p.LineManagerID,
                LSLocationID = p.LSLocationID,
                LSPositionID = p.LSPositionID,
                AccountNumber = p.AccountNumber,
                AccountName = p.AccountName,
                LSBankID = p.LSBankID,
                LSBankBranchID = p.LSBankBranchID,
                SelfDeduction = p.SelfDeduction,
                DependDeduction = p.DependDeduction,
                NoOfDependent = p.NoOfDependent,
                SIBook = p.SIBook,
                HIBook = p.HIBook,
                SI = p.SI,
                HI = p.HI,
                UI = p.UI,
                UserMachineID = p.UserMachineID,
                PayByBank = p.PayByBank,
                EmergencyContact = p.EmergencyContact,
                EmergencyAddess = p.EmergencyAddess,
                EmergencyPhone = p.EmergencyPhone,
                EmergencyMobile = p.EmergencyMobile,
                Active = p.Active
            }).OrderByDescending(p => p.EmpID).ToList();

            return lst;
        }

        public List<EmployeeViewModel> GetActiveList(int? ModuleId, string UserName, bool isAdmin, int? LanguageId)
        {
            List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
            var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
            var _lst = from emp in context.HR_tblEmp
                       join t in context.TER_tblTermination on emp.EmpID equals t.EmpID into terminationlist
                       from termination in terminationlist.DefaultIfEmpty()
                       join f in context.ApplicationFiles on emp.FileId equals f.FileId into filelist
                       from file in filelist.DefaultIfEmpty()
                       where (jointable.Contains(emp.EmpID))
                       && (emp.Active == true || termination.LastWorkingDate >= DateTime.Now )
                       select emp;
            lst = _lst.Select(p => new EmployeeViewModel()
            {
                EmpID = p.EmpID,
                EmpCode = p.EmpCode,
                LastName = p.LastName,
                FirstName = p.FirstName,
                //FullName = ((p.Gender > 0) ? "Mr. " : "Ms. ") + p.LastName + " "+ p.FirstName,
                FullName = p.LastName + " " + p.FirstName,
                Gender = p.Gender,
                BloodType = p.BloodType,
                Email = p.Email,
                Telephone = p.Telephone,
                Mobile = p.Mobile,
                FileId = p.FileId,
                FileUrl = "",
                IDNo = p.IDNo,

                IDIssuedDate = p.IDIssuedDate,
                IDIssuedPlace = p.IDIssuedPlace,
                TaxNo = p.TaxNo,

                PAddress = p.PAddress,
                PLSCountryID = p.PLSCountryID,
                PLSProvinceID = p.PLSProvinceID,
                PLSDistrictID = p.PLSDistrictID,
                TLSCountryID = p.TLSCountryID,
                TLSProvinceID = p.TLSProvinceID,
                TLSDistrictID = p.TLSDistrictID,
                TAddress = p.TAddress,
                LSCompanyID = p.LSCompanyID,             
                DOB = p.DOB,
                YOB = p.YOB,
                StartDate = p.StartDate,
                JoinDate = p.JoinDate,
                BornLSCountryID = p.BornLSCountryID,
                BornLSProvinceID = p.BornLSProvinceID,
                BornLSDistrictID = p.BornLSDistrictID,
                NativeCountryID = p.NativeCountryID,
                NativeProvinceID = p.NativeProvinceID,
                NativeDistrictID = p.NativeDistrictID,
                LSMaritalID = p.LSMaritalID,
                LSNationalityID = p.LSNationalityID,
                LSEthnicID = p.LSEthnicID,
                LSReligionID = p.LSReligionID,
                LSEducationID = p.LSEducationID,
                LSMajorID = p.LSMajorID,
                LineManagerID = p.LineManagerID,
                LSLocationID = p.LSLocationID,
                LSPositionID = p.LSPositionID,
                AccountNumber = p.AccountNumber,
                AccountName = p.AccountName,
                LSBankID = p.LSBankID,
                LSBankBranchID = p.LSBankBranchID,
                SelfDeduction = p.SelfDeduction,
                DependDeduction = p.DependDeduction,
                NoOfDependent = p.NoOfDependent,
                SIBook = p.SIBook,
                HIBook = p.HIBook,
                SI = p.SI,
                HI = p.HI,
                UI = p.UI,
                UserMachineID = p.UserMachineID,
                PayByBank = p.PayByBank,
                EmergencyContact = p.EmergencyContact,
                EmergencyAddess = p.EmergencyAddess,
                EmergencyPhone = p.EmergencyPhone,
                EmergencyMobile = p.EmergencyMobile,
                Active = p.Active
            }).OrderByDescending(p=>p.EmpID).ToList();

            return lst;
        }

        public List<EmployeeViewModel> GetListByCompanyID(int? CompanyID)
        {
            List<EmployeeViewModel> lst = new List<EmployeeViewModel>();            
            lst = context.HR_tblEmp.Where(p => p.LSCompanyID == CompanyID).Select(p => new EmployeeViewModel()
            {
                EmpID = p.EmpID,
                EmpCode = p.EmpCode,

                LastName = p.LastName,
                FirstName = p.FirstName,
                Gender = p.Gender,
                BloodType = p.BloodType,
                Email = p.Email,
                Telephone = p.Telephone,
                Mobile = p.Mobile,
                FileId = p.FileId,
                IDNo = p.IDNo,

                IDIssuedDate = p.IDIssuedDate,
                IDIssuedPlace = p.IDIssuedPlace,
                TaxNo = p.TaxNo,

                PAddress = p.PAddress,
                PLSCountryID = p.PLSCountryID,
                PLSProvinceID = p.PLSProvinceID,
                PLSDistrictID = p.PLSDistrictID,
                TLSCountryID = p.TLSCountryID,
                TLSProvinceID = p.TLSProvinceID,
                TLSDistrictID = p.TLSDistrictID,
                TAddress = p.TAddress,
                LSCompanyID = p.LSCompanyID,               
                DOB = p.DOB,
                YOB = p.YOB,
                StartDate = p.StartDate,
                JoinDate = p.JoinDate,
                BornLSCountryID = p.BornLSCountryID,
                BornLSProvinceID = p.BornLSProvinceID,
                BornLSDistrictID = p.BornLSDistrictID,
                NativeCountryID = p.NativeCountryID,
                NativeProvinceID = p.NativeProvinceID,
                NativeDistrictID = p.NativeDistrictID,
                LSMaritalID = p.LSMaritalID,
                LSNationalityID = p.LSNationalityID,
                LSEthnicID = p.LSEthnicID,
                LSReligionID = p.LSReligionID,
                LSEducationID = p.LSEducationID,
                LSMajorID = p.LSMajorID,
                LineManagerID = p.LineManagerID,
                LSLocationID = p.LSLocationID,
                LSPositionID = p.LSPositionID,
                AccountNumber = p.AccountNumber,
                AccountName = p.AccountName,
                LSBankID = p.LSBankID,
                LSBankBranchID = p.LSBankBranchID,
                SelfDeduction = p.SelfDeduction,
                DependDeduction = p.DependDeduction,
                NoOfDependent = p.NoOfDependent,
                SIBook = p.SIBook,
                HIBook = p.HIBook,
                SI = p.SI,
                HI = p.HI,
                UI = p.UI,
                UserMachineID = p.UserMachineID,
                PayByBank = p.PayByBank,
                EmergencyContact = p.EmergencyContact,
                EmergencyAddess = p.EmergencyAddess,
                EmergencyPhone = p.EmergencyPhone,
                EmergencyMobile = p.EmergencyMobile
            }).ToList();
            return lst;
        }

        public List<EmployeeViewModel> Search(int? LSCompanyID, int? LSLocationID, 
            int? LSPositionID, string Code, string SearchText, bool? Active, int? ModuleId, string UserName, bool isAdmin, int LanguageId)
        {
            List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
            lst = GetList(ModuleId, UserName, isAdmin, LanguageId);
            if (Active != null)
                lst = lst.Where(p => p.Active == Active).ToList();
            if (LSCompanyID != null && LSCompanyID > 0)
            {
                List<int> company_lst = LS_tblCompanyRepository.GetTreeIdListByNodeId(LSCompanyID);
                lst = lst.Where(p => company_lst.Contains(p.LSCompanyID)).ToList();
            }
            if (LSLocationID != null && LSLocationID > 0)
                lst = lst.Where(p => p.LSLocationID == LSLocationID).ToList();
            if (LSPositionID != null && LSPositionID > 0)
                lst = lst.Where(p => p.LSPositionID == LSPositionID).ToList();
            if (Code != null && Code != string.Empty)
                lst = lst.Where(p => p.EmpCode.ToLower().Contains(Code.ToLower())).ToList();
            if (SearchText != null && SearchText != string.Empty)
            {
                string UnSigned_SearchText = StringUtils.ConvertToUnSign(SearchText);
                lst = lst.Where(p => p.FullName.ToLower().Contains(SearchText.ToLower()) || p.FullName.ToLower().Contains(UnSigned_SearchText.ToLower())).ToList();
            }
            
            return lst;
        }

        public static string GenerateEmployeeCode(int num)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.HR_tblEmp select u.EmpID).DefaultIfEmpty(0).Max() + 1;
                return Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
            }
        }

        public static string GenerateEmployeeCode(int num, string sid)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.HR_tblEmp select u.EmpID).DefaultIfEmpty(0).Max() + 1;
                string number = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return string.Format("{0}-{1}", number, sid.Substring(0, 5).ToUpper());
            }
        }

        public static string GenerateEmployeeCodeWithMillisecond(int num)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.HR_tblEmp select u.EmpID).DefaultIfEmpty(0).Max() + 1;
                string number = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return Eagle.Common.Utilities.StringUtils.GenerateCodeWithMillisecond(Max_ID.ToString(), num);
            }
        }

        public static bool CheckExistCode(string strCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.HR_tblEmp.FirstOrDefault(p => p.EmpCode.Equals(strCode));
                return (query != null);
            }
        }

        public static bool CheckDataExist(string EmpCode, string FirstName, string LastName, int LSCompanyID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.HR_tblEmp.FirstOrDefault(p => p.EmpCode.ToUpper().Equals(EmpCode.ToUpper())
                     && p.FirstName.ToUpper().Equals(FirstName.ToUpper())
                    && p.LastName.ToUpper().Equals(LastName.ToUpper()) 
                    && p.LSCompanyID.Equals(LSCompanyID));
                return (query != null);
            }
        }
        
        public bool Add(EmployeeEditModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = CheckDataExist(add_model.EmpCode, add_model.FirstName, add_model.LastName, add_model.LSCompanyID);
                if (isDuplicate == false)
                {
                    bool isCodeExist = CheckExistCode(add_model.EmpCode);
                    if (isCodeExist)
                        add_model.EmpCode = GenerateEmployeeCode(10);

                    HR_tblEmp model = new HR_tblEmp();
                    model.EmpCode = add_model.EmpCode;
                    model.FirstName = add_model.FirstName;
                    model.LastName = add_model.LastName;
                    model.Gender = Convert.ToInt32(add_model.Gender);
                    model.BloodType = add_model.BloodType;
                    model.Email = add_model.Email;
                    model.Telephone = add_model.Telephone;
                    model.Mobile = add_model.Mobile;
                    model.FileId = add_model.FileId;
                    model.IDNo = add_model.IDNo;

                    if(add_model.IDIssuedDate!=null && add_model.IDIssuedDate.ToString()!=string.Empty)
                        model.IDIssuedDate = Convert.ToDateTime(add_model.IDIssuedDate.ToString());

                    model.IDIssuedPlace = (add_model.IDIssuedPlace != null && add_model.IDIssuedPlace > 0) ? Convert.ToInt32(add_model.IDIssuedPlace) : (int?)null;
                    model.TaxNo = add_model.TaxNo;

                    int? PLSCountryID = (add_model.PLSCountryID != null && add_model.PLSCountryID > 0) ? Convert.ToInt32(add_model.PLSCountryID) : (int?)null;
                    int? PLSProvinceID = (add_model.PLSProvinceID != null && add_model.PLSProvinceID > 0) ? Convert.ToInt32(add_model.PLSProvinceID) : (int?)null;
                    int? PLSDistrictID = (add_model.PLSDistrictID != null && add_model.PLSDistrictID > 0) ? Convert.ToInt32(add_model.PLSDistrictID) : (int?)null;

                    model.PAddress = add_model.PAddress;
                    model.PLSCountryID = PLSCountryID;
                    model.PLSProvinceID = PLSProvinceID;
                    model.PLSDistrictID = PLSDistrictID;


                    int? TLSCountryID = (add_model.TLSCountryID != null && add_model.TLSCountryID != 0) ? Convert.ToInt32(add_model.TLSCountryID) : (int?)null;
                    int? TLSProvinceID = (add_model.TLSProvinceID != null && add_model.TLSProvinceID != 0) ? Convert.ToInt32(add_model.TLSProvinceID) : (int?)null;
                    int? TLSDistrictID = (add_model.TLSDistrictID != null && add_model.TLSDistrictID != 0) ? Convert.ToInt32(add_model.TLSDistrictID) : (int?)null;

                    model.TAddress = add_model.TAddress;
                    model.TLSCountryID = TLSCountryID;
                    model.TLSProvinceID = TLSProvinceID;
                    model.TLSDistrictID = TLSDistrictID;

                    int? BornLSCountryID = (add_model.BornLSCountryID != null && add_model.BornLSCountryID != 0) ? Convert.ToInt32(add_model.BornLSCountryID) : (int?)null;
                    int? BornLSProvinceID = (add_model.BornLSProvinceID != null && add_model.BornLSProvinceID != 0) ? Convert.ToInt32(add_model.BornLSProvinceID) : (int?)null;
                    int? BornLSDistrictID = (add_model.BornLSDistrictID != null && add_model.BornLSDistrictID != 0) ? Convert.ToInt32(add_model.BornLSDistrictID) : (int?)null;


                    model.BornLSCountryID = BornLSCountryID;
                    model.BornLSProvinceID = BornLSProvinceID;
                    model.BornLSDistrictID = BornLSDistrictID;

                    int? NativeCountryID = (add_model.NativeCountryID != null && add_model.NativeCountryID != 0) ? Convert.ToInt32(add_model.NativeCountryID) : (int?)null;
                    int? NativeProvinceID = (add_model.NativeProvinceID != null && add_model.NativeProvinceID != 0) ? Convert.ToInt32(add_model.NativeProvinceID) : (int?)null;
                    int? NativeDistrictID = (add_model.NativeDistrictID != null && add_model.NativeDistrictID != 0) ? Convert.ToInt32(add_model.NativeDistrictID) : (int?)null;


                    model.NativeCountryID = NativeCountryID;
                    model.NativeProvinceID = NativeProvinceID;
                    model.NativeDistrictID = NativeDistrictID;


                    model.LSCompanyID = Convert.ToInt32(add_model.LSCompanyID);
                   

                    if (add_model.YOB != null && add_model.YOB.ToString() != string.Empty)
                        model.YOB = Convert.ToInt32(add_model.YOB);

                    if (add_model.DOB != null && add_model.DOB.ToString() != string.Empty)
                        model.DOB = Convert.ToDateTime(add_model.DOB.ToString());                    

                    if (add_model.StartDate != null && add_model.StartDate.ToString() != string.Empty)
                        model.StartDate =Convert.ToDateTime(add_model.StartDate.ToString());

                    if (add_model.JoinDate != null && add_model.JoinDate.ToString() != string.Empty)
                        model.JoinDate = Convert.ToDateTime(add_model.JoinDate.ToString());

                    if (model.LSMaritalID != null && model.LSMaritalID.ToString() != string.Empty)
                        model.LSMaritalID = Convert.ToInt32(add_model.LSMaritalID);

                    if (model.LSNationalityID != null && model.LSNationalityID.ToString() != string.Empty)
                        model.LSNationalityID = Convert.ToInt32(add_model.LSNationalityID);

                    if (model.LSEthnicID != null && model.LSEthnicID.ToString() != string.Empty)
                        model.LSEthnicID = Convert.ToInt32(add_model.LSEthnicID);

                    if (model.LSReligionID != null && model.LSReligionID.ToString() != string.Empty)
                        model.LSReligionID = Convert.ToInt32(add_model.LSReligionID);

                    if (model.LSEducationID != null && model.LSEducationID.ToString() != string.Empty)
                        model.LSEducationID = Convert.ToInt32(add_model.LSEducationID);

                    if (model.LSMajorID != null && model.LSMajorID.ToString() != string.Empty)
                        model.LSMajorID = Convert.ToInt32(add_model.LSMajorID);

                    if (model.LineManagerID != null && model.LineManagerID.ToString() != string.Empty)
                        model.LineManagerID = Convert.ToInt32(add_model.LineManagerID);

                    if (model.LSLocationID != null && model.LSLocationID.ToString() != string.Empty)
                        model.LSLocationID = Convert.ToInt32(add_model.LSLocationID);

                    if (model.LSPositionID != null && model.LSPositionID.ToString() != string.Empty)
                        model.LSPositionID = Convert.ToInt32(add_model.LSPositionID);                   

                    if (model.LSBankID != null && model.LSBankID.ToString() != string.Empty)
                        model.LSBankID = Convert.ToInt32(add_model.LSBankID);

                    if (model.LSBankBranchID != null && model.LSBankBranchID.ToString() != string.Empty)
                        model.LSBankBranchID = Convert.ToInt32(add_model.LSBankBranchID);

                    model.AccountNumber = add_model.AccountNumber;
                    model.AccountName = add_model.AccountName;
                    model.SelfDeduction = Convert.ToBoolean(add_model.SelfDeduction);
                    model.DependDeduction = Convert.ToBoolean(add_model.DependDeduction);
                    model.NoOfDependent = Convert.ToInt32(add_model.NoOfDependent);

                    model.SIBook = add_model.SIBook;
                    model.HIBook = add_model.HIBook;
                    model.PayByBank = Convert.ToBoolean(add_model.PayByBank);
                    model.EmergencyContact = add_model.EmergencyContact;

                    model.EmergencyAddess = add_model.EmergencyAddess;
                    model.EmergencyPhone = add_model.EmergencyPhone;
                    model.EmergencyMobile = add_model.EmergencyMobile;
                    model.Active = true;
                    model.CreatedOnDate = DateTime.Now;


                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow > 0)
                    {
                        model.EmpID = add_model.EmpID;
                        Message = Eagle.Resource.LanguageResource.CreateSuccess;
                        result = true;
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
            return result;
        }

        public static int Insert(string EmpCode, string FirstName, string LastName, string Gender, string Email, string Telephone,
            string Mobile, string IDNo, string IDIssuedDate, string PAddress, string TAddress, string LSCompanyID, string DOB,
            string StartDate, string JoinDate, out string Message)
        {
            Message = string.Empty;
            int EmpID = 0;
            try
            {
                bool isDuplicate = CheckExistCode(EmpCode);
                if (isDuplicate == false)
                {
                    if (string.IsNullOrEmpty(EmpCode))
                        EmpCode = GenerateEmployeeCode(10);

                    using (EntityDataContext context = new EntityDataContext())
                    {
                        HR_tblEmp model = new HR_tblEmp();                       
                        model.EmpCode = EmpCode;
                        model.FirstName = FirstName;
                        model.LastName = LastName;
                        model.Gender = Convert.ToInt32(Gender);
                        model.Email = Email;
                        model.Telephone =Telephone;
                        model.Mobile = Mobile;
                        model.IDNo = IDNo;

                        if (!string.IsNullOrEmpty(IDIssuedDate))
                            model.IDIssuedDate = Convert.ToDateTime(IDIssuedDate);                                            

                        if (!string.IsNullOrEmpty(DOB))
                            model.DOB = Convert.ToDateTime(DOB);

                        if (!string.IsNullOrEmpty(StartDate))
                            model.StartDate = Convert.ToDateTime(StartDate);

                        if (!string.IsNullOrEmpty(JoinDate))
                            model.JoinDate = Convert.ToDateTime(JoinDate);

                        model.PAddress = PAddress;
                        model.TAddress = TAddress;
                        model.LSCompanyID = Convert.ToInt32(LSCompanyID);
                        model.Active = true;
                        model.CreatedOnDate = DateTime.Now;


                        int affectedRow = 0;                    
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow > 0)
                        {
                            EmpID = model.EmpID;
                            Message = Eagle.Resource.LanguageResource.CreateSuccess;
                        }
                    }
                }
                else
                {
                    Message = Eagle.Resource.LanguageResource.DuplicateError;
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return EmpID;
        }

        public EmployeeViewModel Details(int? EmpID)
        {
            EmployeeViewModel entity = new EmployeeViewModel();
            if (EmpID != null && EmpID > 0)
            {

                entity = (from emp in context.HR_tblEmp
                          join position in context.LS_tblPosition on emp.LSPositionID equals position.LSPositionID into list1
                          from l1 in list1.DefaultIfEmpty()
                          join company in context.LS_tblCompany on emp.LSCompanyID equals company.LSCompanyID into list4
                          from l4 in list4.DefaultIfEmpty()
                          join location in context.LS_tblLocation on emp.LSLocationID equals location.LSLocationID into list5
                          from location in list5.DefaultIfEmpty()
                          where emp.EmpID == EmpID
                          select new EmployeeViewModel()
                          {
                              Position = l1.Name,
                              CompanyName = l4.Name,
                              LSLocationName = location.Name,

                              EmpID = emp.EmpID,
                              EmpCode = emp.EmpCode,
                              FirstName = emp.FirstName,
                              LastName = emp.LastName,
                              FullName = emp.LastName + " " + emp.FirstName,
                              Gender = emp.Gender,
                              BloodType = emp.BloodType,
                              Saturation = (emp.Gender == 0) ? "Mr." : "Ms.",
                              Email = emp.Email,
                              Telephone = emp.Telephone,
                              Mobile = emp.Mobile,
                              FileId = emp.FileId,

                              IDNo = emp.IDNo,
                              IDIssuedDate = emp.IDIssuedDate,
                              IDIssuedPlace = emp.IDIssuedPlace,
                              TaxNo = emp.TaxNo,

                              PAddress = emp.PAddress,
                              PLSCountryID = emp.PLSCountryID,
                              PLSProvinceID = emp.PLSProvinceID,
                              PLSDistrictID = emp.PLSDistrictID,

                              TAddress = emp.TAddress,
                              TLSCountryID = emp.TLSCountryID,
                              TLSProvinceID = emp.TLSProvinceID,
                              TLSDistrictID = emp.TLSDistrictID,

                              LSCompanyID = emp.LSCompanyID,

                              DOB = emp.DOB,
                              YOB = emp.YOB,
                              StartDate = emp.StartDate,
                              JoinDate = emp.JoinDate,
                              CreatedOnDate = emp.CreatedOnDate,
                              LastModifiedOnDate = emp.LastModifiedOnDate,

                              BornLSCountryID = emp.BornLSCountryID,
                              BornLSProvinceID = emp.BornLSProvinceID,
                              BornLSDistrictID = emp.BornLSDistrictID,

                              NativeCountryID = emp.NativeCountryID,
                              NativeProvinceID = emp.NativeProvinceID,
                              NativeDistrictID = emp.NativeDistrictID,

                              LSMaritalID = emp.LSMaritalID,
                              LSNationalityID = emp.LSNationalityID,
                              LSEthnicID = emp.LSEthnicID,
                              LSReligionID = emp.LSReligionID,
                              LSEducationID = emp.LSEducationID,
                              LSMajorID = emp.LSMajorID,
                              LineManagerID = emp.LineManagerID,
                              LSLocationID = emp.LSLocationID,
                              LSPositionID = emp.LSPositionID,

                              AccountNumber = emp.AccountNumber,
                              AccountName = emp.AccountName,
                              LSBankID = emp.LSBankID,
                              LSBankBranchID = emp.LSBankBranchID,
                              SelfDeduction = emp.SelfDeduction,
                              DependDeduction = emp.DependDeduction,
                              NoOfDependent = emp.NoOfDependent,

                              SIBook = emp.SIBook,
                              HIBook = emp.HIBook,
                              SI = emp.SI,
                              HI = emp.HI,
                              UI = emp.UI,
                              UserMachineID = emp.UserMachineID,
                              PayByBank = emp.PayByBank,

                              EmergencyContact = emp.EmergencyContact,
                              EmergencyAddess = emp.EmergencyAddess,
                              EmergencyPhone = emp.EmergencyPhone,
                              EmergencyMobile = emp.EmergencyMobile,
                              Active = emp.Active
                          }).FirstOrDefault();
            }
            return entity;
        }
           
        public static HR_tblEmp Find(int? id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.HR_tblEmp.Find(id);
            }
        }

        public static string GetManagerName(int? LineMagagerID)
        {
            string LineManagerName = string.Empty;
            using (EntityDataContext context = new EntityDataContext())
            {                
                if (LineMagagerID != null && LineMagagerID > 0)
                {
                    var entity = (from p in context.HR_tblEmp
                                 let name = p.LastName + " " + p.FirstName
                                 where p.EmpID == LineMagagerID
                                 select new
                                 {
                                     FullName = p.LastName + " " + p.FirstName
                                 }).FirstOrDefault();
                    if (entity !=null)
                        LineManagerName = entity.FullName;
                }
            }
            return LineManagerName;           
        }

        public static EmployeeViewModel GetDetailsByEmpIdAndLanguageCode(int? EmpId, string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                EmployeeViewModel entity = new EmployeeViewModel();
                if (EmpId != null)
                {
                    entity = (from emp in context.HR_tblEmp
                                    join position in context.LS_tblPosition on emp.LSPositionID equals position.LSPositionID into list1
                                    from l1 in list1.DefaultIfEmpty()
                                    join company in context.LS_tblCompany on emp.LSCompanyID equals company.LSCompanyID into list4
                                    from l4 in list4.DefaultIfEmpty()
                                    join location in context.LS_tblLocation on emp.LSLocationID equals location.LSLocationID into list5
                                    from location in list5.DefaultIfEmpty()
                                    where emp.EmpID == EmpId
                                    select new EmployeeViewModel()
                                    {
                                        Position = (LanguageCode == "en-US") ? l1.Name : l1.VNName,
                                        CompanyName = (LanguageCode == "en-US") ? l4.Name : l4.VNName,
                                        LSLocationName = (LanguageCode == "en-US") ? location.Name : location.VNName,

                                        EmpID = emp.EmpID,
                                        EmpCode = emp.EmpCode,
                                        FirstName = emp.FirstName,
                                        LastName = emp.LastName,
                                        FullName = emp.LastName + " " + emp.FirstName,
                                        Gender = emp.Gender,
                                        BloodType = emp.BloodType,
                                        Saturation = (emp.Gender == 0) ? "Mr." : "Ms.",
                                        Email = emp.Email,
                                        Telephone = emp.Telephone,
                                        Mobile = emp.Mobile,
                                        FileId = emp.FileId,

                                        IDNo = emp.IDNo,
                                        IDIssuedDate = emp.IDIssuedDate,
                                        IDIssuedPlace = emp.IDIssuedPlace,
                                        TaxNo = emp.TaxNo,

                                        PAddress = emp.PAddress,
                                        PLSCountryID = emp.PLSCountryID,
                                        PLSProvinceID = emp.PLSProvinceID,
                                        PLSDistrictID = emp.PLSDistrictID,

                                        TAddress = emp.TAddress,
                                        TLSCountryID = emp.TLSCountryID,
                                        TLSProvinceID = emp.TLSProvinceID,
                                        TLSDistrictID = emp.TLSDistrictID,

                                        LSCompanyID = emp.LSCompanyID,

                                        DOB = emp.DOB,
                                        YOB = emp.YOB,
                                        StartDate = emp.StartDate,
                                        JoinDate = emp.JoinDate,
                                        CreatedOnDate = emp.CreatedOnDate,
                                        LastModifiedOnDate = emp.LastModifiedOnDate,

                                        BornLSCountryID = emp.BornLSCountryID,
                                        BornLSProvinceID = emp.BornLSProvinceID,
                                        BornLSDistrictID = emp.BornLSDistrictID,

                                        NativeCountryID = emp.NativeCountryID,
                                        NativeProvinceID = emp.NativeProvinceID,
                                        NativeDistrictID = emp.NativeDistrictID,

                                        LSMaritalID = emp.LSMaritalID,
                                        LSNationalityID = emp.LSNationalityID,
                                        LSEthnicID = emp.LSEthnicID,
                                        LSReligionID = emp.LSReligionID,
                                        LSEducationID = emp.LSEducationID,
                                        LSMajorID = emp.LSMajorID,
                                        LineManagerID = emp.LineManagerID,
                                        LSLocationID = emp.LSLocationID,
                                        LSPositionID = emp.LSPositionID,

                                        AccountNumber = emp.AccountNumber,
                                        AccountName = emp.AccountName,
                                        LSBankID = emp.LSBankID,
                                        LSBankBranchID = emp.LSBankBranchID,
                                        SelfDeduction = emp.SelfDeduction,
                                        DependDeduction = emp.DependDeduction,
                                        NoOfDependent = emp.NoOfDependent,

                                        SIBook = emp.SIBook,
                                        HIBook = emp.HIBook,
                                        SI = emp.SI,
                                        HI = emp.HI,
                                        UI = emp.UI,
                                        UserMachineID = emp.UserMachineID,
                                        PayByBank = emp.PayByBank,

                                        EmergencyContact = emp.EmergencyContact,
                                        EmergencyAddess = emp.EmergencyAddess,
                                        EmergencyPhone = emp.EmergencyPhone,
                                        EmergencyMobile = emp.EmergencyMobile,
                                        Active = emp.Active
                                    }).FirstOrDefault();
                }
                return entity;
            }          
        }
        
        public static EmployeeViewModel GetDetails(int? EmpID, int? LanguageId)
        {            
            using (EntityDataContext context = new EntityDataContext())
            {
                EmployeeViewModel entity = new EmployeeViewModel(); 
                if (EmpID != null && EmpID > 0)
                {
                    entity = (from emp in context.HR_tblEmp
                              join position in context.LS_tblPosition on emp.LSPositionID equals position.LSPositionID into list1
                              from l1 in list1.DefaultIfEmpty()
                              join company in context.LS_tblCompany on emp.LSCompanyID equals company.LSCompanyID into list4
                              from l4 in list4.DefaultIfEmpty()
                              join location in context.LS_tblLocation on emp.LSLocationID equals location.LSLocationID into list5
                              from location in list5.DefaultIfEmpty()
                              where emp.EmpID == EmpID
                              select new EmployeeViewModel()
                              {
                                  Position = (LanguageId == 124) ? l1.Name : l1.VNName,
                                  CompanyName = (LanguageId == 124) ? l4.Name : l4.VNName,
                                  LSLocationName = (LanguageId == 124) ? location.Name : location.VNName,

                                  EmpID = emp.EmpID,
                                  EmpCode = emp.EmpCode,
                                  FirstName = emp.FirstName,
                                  LastName = emp.LastName,
                                  FullName = emp.LastName + " " + emp.FirstName,
                                  Gender = emp.Gender,
                                  BloodType = emp.BloodType,
                                  Saturation = (emp.Gender == 0) ? "Mr." : "Ms.",
                                  Email = emp.Email,
                                  Telephone = emp.Telephone,
                                  Mobile = emp.Mobile,
                                  FileId = emp.FileId,

                                  IDNo = emp.IDNo,
                                  IDIssuedDate = emp.IDIssuedDate,
                                  IDIssuedPlace = emp.IDIssuedPlace,
                                  TaxNo = emp.TaxNo,

                                  PAddress = emp.PAddress,
                                  PLSCountryID = emp.PLSCountryID,
                                  PLSProvinceID = emp.PLSProvinceID,
                                  PLSDistrictID = emp.PLSDistrictID,

                                  TAddress = emp.TAddress,
                                  TLSCountryID = emp.TLSCountryID,
                                  TLSProvinceID = emp.TLSProvinceID,
                                  TLSDistrictID = emp.TLSDistrictID,

                                  LSCompanyID = emp.LSCompanyID,

                                  DOB = emp.DOB,
                                  YOB = emp.YOB,
                                  StartDate = emp.StartDate,
                                  JoinDate = emp.JoinDate,
                                  CreatedOnDate = emp.CreatedOnDate,
                                  LastModifiedOnDate = emp.LastModifiedOnDate,

                                  BornLSCountryID = emp.BornLSCountryID,
                                  BornLSProvinceID = emp.BornLSProvinceID,
                                  BornLSDistrictID = emp.BornLSDistrictID,

                                  NativeCountryID = emp.NativeCountryID,
                                  NativeProvinceID = emp.NativeProvinceID,
                                  NativeDistrictID = emp.NativeDistrictID,

                                  LSMaritalID = emp.LSMaritalID,
                                  LSNationalityID = emp.LSNationalityID,
                                  LSEthnicID = emp.LSEthnicID,
                                  LSReligionID = emp.LSReligionID,
                                  LSEducationID = emp.LSEducationID,
                                  LSMajorID = emp.LSMajorID,
                                  LineManagerID = emp.LineManagerID,
                                  LSLocationID = emp.LSLocationID,
                                  LSPositionID = emp.LSPositionID,

                                  AccountNumber = emp.AccountNumber,
                                  AccountName = emp.AccountName,
                                  LSBankID = emp.LSBankID,
                                  LSBankBranchID = emp.LSBankBranchID,
                                  SelfDeduction = emp.SelfDeduction,
                                  DependDeduction = emp.DependDeduction,
                                  NoOfDependent = emp.NoOfDependent,

                                  SIBook = emp.SIBook,
                                  HIBook = emp.HIBook,
                                  SI = emp.SI,
                                  HI = emp.HI,
                                  UI = emp.UI,
                                  UserMachineID = emp.UserMachineID,
                                  PayByBank = emp.PayByBank,

                                  EmergencyContact = emp.EmergencyContact,
                                  EmergencyAddess = emp.EmergencyAddess,
                                  EmergencyPhone = emp.EmergencyPhone,
                                  EmergencyMobile = emp.EmergencyMobile,
                                  Active = emp.Active
                              }).FirstOrDefault();
                }
                return entity;
            }            
        }

        public EmployeeEditModel GetDetailsById(int? EmpID, int? LanguageId)
        {
            EmployeeEditModel entity = new EmployeeEditModel();
            if (EmpID != null && EmpID > 0)
            {
                entity = (from emp in context.HR_tblEmp
                              join position in context.LS_tblPosition on emp.LSPositionID equals position.LSPositionID into position_list
                              from position_entity in position_list.DefaultIfEmpty()

                              join company in context.LS_tblCompany on emp.LSCompanyID equals company.LSCompanyID into company_list
                              from company_entity in company_list.DefaultIfEmpty()

                              join location in context.LS_tblLocation on emp.LSLocationID equals location.LSLocationID into locationlist
                              from location_entity in locationlist.DefaultIfEmpty()

                              join bank in context.LS_tblBank on emp.LSBankID equals bank.LSBankID into banklist
                              from bank_entity in banklist.DefaultIfEmpty()

                              join bankBranch in context.LS_tblBankBranch on emp.LSBankBranchID equals bankBranch.LSBankBranchID into bankBranchlist
                              from bankBranch_entity in bankBranchlist.DefaultIfEmpty()

                              join marital in context.LS_tblMarital on emp.LSMaritalID equals marital.LSMaritalID into maritallist
                              from marital_entity in maritallist.DefaultIfEmpty()

                              join nationality in context.LS_tblNationality on emp.LSNationalityID equals nationality.LSNationalityID into nationalitylist
                              from nationality_entity in nationalitylist.DefaultIfEmpty()

                              join ethnic in context.LS_tblEthnic on emp.LSEthnicID equals ethnic.LSEthnicID into ethniclist
                              from ethnic_entity in ethniclist.DefaultIfEmpty()

                              join religion in context.LS_tblReligion on emp.LSReligionID equals religion.LSReligionID into religionlist
                              from religion_entity in religionlist.DefaultIfEmpty()

                              join education in context.LS_tblEducation on emp.LSEducationID equals education.LSEducationID into educationlist
                              from education_entity in educationlist.DefaultIfEmpty()

                              join major in context.LS_tblMajor on emp.LSMajorID equals major.LSMajorID into majorlist
                              from major_entity in majorlist.DefaultIfEmpty()

                              join idissuedplace in context.LS_tblProvince on emp.IDIssuedPlace equals idissuedplace.LSProvinceID into idissuedplacelist
                              from idissuedplace_entity in idissuedplacelist.DefaultIfEmpty()

                              where emp.EmpID == EmpID
                              select new EmployeeEditModel()
                              {
                                  EmpID = emp.EmpID,
                                  EmpCode = emp.EmpCode,
                                  FirstName = emp.FirstName,
                                  LastName = emp.LastName,
                                  FullName = emp.LastName + " " + emp.FirstName,
                                  Gender = emp.Gender,
                                  BloodType = emp.BloodType,
                                  Email = emp.Email,
                                  Telephone = emp.Telephone,
                                  Mobile = emp.Mobile,
                                  FileId = emp.FileId,

                                  IDNo = emp.IDNo,
                                  IDIssuedDate = emp.IDIssuedDate,
                                  IDIssuedPlace = emp.IDIssuedPlace,
                                  IDIssuedPlaceName = (LanguageId == 124) ? idissuedplace_entity.Name : idissuedplace_entity.VNName,
                                  TaxNo = emp.TaxNo,

                                  PAddress = emp.PAddress,
                                  PLSCountryID = emp.PLSCountryID,
                                  PLSProvinceID = emp.PLSProvinceID,
                                  PLSDistrictID = emp.PLSDistrictID,

                                  TAddress = emp.TAddress,
                                  TLSCountryID = emp.TLSCountryID,
                                  TLSProvinceID = emp.TLSProvinceID,
                                  TLSDistrictID = emp.TLSDistrictID,

                                  LSCompanyID = emp.LSCompanyID,
                                  LSCompanyName = (LanguageId == 124)? company_entity.Name : company_entity.VNName,

                                  DOB = emp.DOB,
                                  YOB = emp.YOB,
                                  StartDate = emp.StartDate,
                                  JoinDate = emp.JoinDate,
                                  CreatedOnDate = emp.CreatedOnDate,
                                  LastModifiedOnDate = emp.LastModifiedOnDate,

                                  BornLSCountryID = emp.BornLSCountryID,                                  
                                  BornLSProvinceID = emp.BornLSProvinceID,
                                  BornLSDistrictID = emp.BornLSDistrictID,
                                  NativeCountryID = emp.NativeCountryID,
                                  NativeProvinceID = emp.NativeProvinceID,
                                  NativeDistrictID = emp.NativeDistrictID,

                                  LSMaritalID = emp.LSMaritalID,
                                  LSMaritalName = (LanguageId == 124)?marital_entity.Name: marital_entity.VNName,

                                  LSNationalityID = emp.LSNationalityID,
                                  LSNationalityName = (LanguageId == 124) ? nationality_entity.Name : nationality_entity.VNName,

                                  LSEthnicID = emp.LSEthnicID,
                                  LSEthnicName = (LanguageId == 124) ? ethnic_entity.Name : ethnic_entity.VNName,

                                  LSReligionID = emp.LSReligionID,
                                  LSReligionName = (LanguageId == 124) ? religion_entity.Name : religion_entity.VNName,

                                  LSEducationID = emp.LSEducationID,
                                  LSEducationName = (LanguageId == 124) ? education_entity.Name : education_entity.VNName,

                                  LSMajorID = emp.LSMajorID,
                                  LSMajorName = (LanguageId == 124) ? major_entity.Name : major_entity.VNName,

                                  LineManagerID = emp.LineManagerID,

                                  LSLocationID = emp.LSLocationID,
                                  LSLocationName = (LanguageId == 124) ? location_entity.Name : location_entity.VNName,

                                  LSPositionID = emp.LSPositionID,
                                  LSPositionName = (LanguageId == 124) ? position_entity.Name : position_entity.VNName,

                                  AccountNumber = emp.AccountNumber,
                                  AccountName = emp.AccountName,

                                  LSBankID = emp.LSBankID,
                                  LSBankName = (LanguageId == 124) ? bank_entity.Name : bank_entity.VNName,

                                  LSBankBranchID = emp.LSBankBranchID,
                                  LSBankBranchName = (LanguageId == 124) ? bankBranch_entity.Name : bankBranch_entity.VNName,

                                  SelfDeduction = emp.SelfDeduction,
                                  DependDeduction = emp.DependDeduction,
                                  NoOfDependent = emp.NoOfDependent,

                                  SIBook = emp.SIBook,
                                  HIBook = emp.HIBook,
                                  SI = emp.SI,
                                  HI = emp.HI,
                                  UI = emp.UI,
                                  UserMachineID = emp.UserMachineID,
                                  PayByBank = emp.PayByBank,

                                  EmergencyContact = emp.EmergencyContact,
                                  EmergencyAddess = emp.EmergencyAddess,
                                  EmergencyPhone = emp.EmergencyPhone,
                                  EmergencyMobile = emp.EmergencyMobile,
                                  Active = emp.Active
                              }).FirstOrDefault();

                entity.LineManagerName = GetManagerName(entity.LineManagerID);
                entity.BornLSCountryName = GetCountryName(entity.BornLSCountryID, LanguageId);
                entity.BornLSProvinceName = GetProviceName(entity.BornLSProvinceID, LanguageId);
                entity.BornLSDistrictName = GetDistrictName(entity.BornLSDistrictID, LanguageId);
                entity.NativeCountryName = GetCountryName(entity.NativeCountryID, LanguageId);
                entity.NativeProvinceName = GetProviceName(entity.NativeProvinceID, LanguageId);
                entity.NativeDistrictName = GetDistrictName(entity.NativeDistrictID, LanguageId);

                entity.PLSCountryName = GetCountryName(entity.PLSCountryID, LanguageId);
                entity.PLSProvinceName= GetProviceName(entity.PLSProvinceID, LanguageId);
                entity.PLSDistrictName = GetDistrictName(entity.PLSDistrictID, LanguageId);
                entity.TLSCountryName=  GetCountryName(entity.TLSCountryID, LanguageId);
                entity.TLSProvinceName = GetProviceName(entity.TLSProvinceID, LanguageId);
                entity.TLSDistrictName = GetDistrictName(entity.TLSDistrictID, LanguageId);

            }
            return entity;
        }

        public static string GetCountryName(int? LSCountryID, int? LanguageId)
        {
            string CountryName = string.Empty;
            using (EntityDataContext context = new EntityDataContext())
            {
                if (LSCountryID != null && LSCountryID > 0)
                {
                    var entity = (from p in context.LS_tblCountry
                                  where p.LSCountryID == LSCountryID
                                  select new
                                  {
                                      CountryName = (LanguageId == 124) ? p.Name : p.VNName
                                  }).FirstOrDefault();
                    if (entity != null)
                        CountryName = entity.CountryName;
                }
            }
            return CountryName;
        }

        public static string GetProviceName(int? LSProvinceID, int? LanguageId)
        {
            string ProviceName = string.Empty;
            using (EntityDataContext context = new EntityDataContext())
            {
                if (LSProvinceID != null && LSProvinceID > 0)
                {
                    var entity = (from p in context.LS_tblProvince
                                  where p.LSProvinceID == LSProvinceID
                                  select new
                                  {
                                      ProviceName = (LanguageId == 124) ? p.Name : p.VNName
                                  }).FirstOrDefault();
                    if (entity != null)
                        ProviceName = entity.ProviceName;
                }
            }
            return ProviceName;
        }

        public static string GetDistrictName(int? LSDistrictID, int? LanguageId)
        {
            string DistrictName = string.Empty;
            using (EntityDataContext context = new EntityDataContext())
            {
                if (LSDistrictID != null && LSDistrictID > 0)
                {
                    var entity = (from p in context.LS_tblDistrict
                                  where p.LSDistrictID == LSDistrictID
                                  select new
                                  {
                                      DistrictName = (LanguageId == 124) ? p.Name : p.VNName
                                  }).FirstOrDefault();
                    if (entity != null)
                        DistrictName = entity.DistrictName;
                }
            }
            return DistrictName;
        }

        public static EmployeeViewModel GetDetailInfo(int? EmpID, int? LanguageId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                EmployeeViewModel entity = new EmployeeViewModel();
                if (EmpID != null && EmpID > 0)
                {
                    entity = (from emp in context.HR_tblEmp
                              join position in context.LS_tblPosition on emp.LSPositionID equals position.LSPositionID into position_list
                              from position_entity in position_list.DefaultIfEmpty()

                              join company in context.LS_tblCompany on emp.LSCompanyID equals company.LSCompanyID into company_list
                              from company_entity in company_list.DefaultIfEmpty()

                              join location in context.LS_tblLocation on emp.LSLocationID equals location.LSLocationID into locationlist
                              from location_entity in locationlist.DefaultIfEmpty()

                              join bank in context.LS_tblBank on emp.LSBankID equals bank.LSBankID into banklist
                              from bank_entity in banklist.DefaultIfEmpty()

                              join bankBranch in context.LS_tblBankBranch on emp.LSBankBranchID equals bankBranch.LSBankBranchID into bankBranchlist
                              from bankBranch_entity in bankBranchlist.DefaultIfEmpty()

                              join marital in context.LS_tblMarital on emp.LSMaritalID equals marital.LSMaritalID into maritallist
                              from marital_entity in maritallist.DefaultIfEmpty()

                              join nationality in context.LS_tblNationality on emp.LSNationalityID equals nationality.LSNationalityID into nationalitylist
                              from nationality_entity in nationalitylist.DefaultIfEmpty()

                              join ethnic in context.LS_tblEthnic on emp.LSEthnicID equals ethnic.LSEthnicID into ethniclist
                              from ethnic_entity in ethniclist.DefaultIfEmpty()

                              join religion in context.LS_tblReligion on emp.LSReligionID equals religion.LSReligionID into religionlist
                              from religion_entity in religionlist.DefaultIfEmpty()

                              join education in context.LS_tblEducation on emp.LSEducationID equals education.LSEducationID into educationlist
                              from education_entity in educationlist.DefaultIfEmpty()

                              join major in context.LS_tblMajor on emp.LSMajorID equals major.LSMajorID into majorlist
                              from major_entity in majorlist.DefaultIfEmpty()

                              join idissuedplace in context.LS_tblProvince on emp.IDIssuedPlace equals idissuedplace.LSProvinceID into idissuedplacelist
                              from idissuedplace_entity in idissuedplacelist.DefaultIfEmpty()

                              where emp.EmpID == EmpID
                              select new EmployeeViewModel()
                              {
                                  EmpID = emp.EmpID,
                                  EmpCode = emp.EmpCode,
                                  FirstName = emp.FirstName,
                                  LastName = emp.LastName,
                                  FullName = emp.LastName + " " + emp.FirstName,
                                  Gender = emp.Gender,
                                  Email = emp.Email,
                                  Telephone = emp.Telephone,
                                  Mobile = emp.Mobile,
                                  FileId = emp.FileId,

                                  IDNo = emp.IDNo,
                                  IDIssuedDate = emp.IDIssuedDate,
                                  IDIssuedPlace = emp.IDIssuedPlace,
                                  IDIssuedPlaceName = (LanguageId == 124) ? idissuedplace_entity.Name : idissuedplace_entity.VNName,
                                  TaxNo = emp.TaxNo,

                                  PAddress = emp.PAddress,
                                  PLSCountryID = emp.PLSCountryID,
                                  PLSProvinceID = emp.PLSProvinceID,
                                  PLSDistrictID = emp.PLSDistrictID,

                                  TAddress = emp.TAddress,
                                  TLSCountryID = emp.TLSCountryID,
                                  TLSProvinceID = emp.TLSProvinceID,
                                  TLSDistrictID = emp.TLSDistrictID,

                                  LSCompanyID = emp.LSCompanyID,
                                  LSCompanyName = (LanguageId==10001)? company_entity.Name:company_entity.VNName,


                                  DOB = emp.DOB,
                                  YOB = emp.YOB,
                                  StartDate = emp.StartDate,
                                  JoinDate = emp.JoinDate,
                                  CreatedOnDate = emp.CreatedOnDate,
                                  LastModifiedOnDate = emp.LastModifiedOnDate,

                                  BornLSCountryID = emp.BornLSCountryID,
                                  BornLSProvinceID = emp.BornLSProvinceID,
                                  BornLSDistrictID = emp.BornLSDistrictID,

                                  NativeCountryID = emp.NativeCountryID,
                                  NativeProvinceID = emp.NativeProvinceID,
                                  NativeDistrictID = emp.NativeDistrictID,

                                  LSMaritalID = emp.LSMaritalID,
                                  LSMaritalName = (LanguageId==10001)?marital_entity.Name:marital_entity.VNName,

                                  LSNationalityID = emp.LSNationalityID,
                                  LSNationalityName =  (LanguageId==10001)?nationality_entity.Name:nationality_entity.VNName,

                                  LSEthnicID = emp.LSEthnicID,
                                  LSEthnicName =  (LanguageId==10001)?ethnic_entity.Name:ethnic_entity.VNName,

                                  LSReligionID = emp.LSReligionID,
                                  LSReligionName =  (LanguageId==10001)?religion_entity.Name:religion_entity.VNName,

                                  LSEducationID = emp.LSEducationID,
                                  LSEducationName =  (LanguageId==10001)?education_entity.Name:education_entity.VNName,

                                  LSMajorID = emp.LSMajorID,
                                  LSMajorName =  (LanguageId==10001)?major_entity.Name:major_entity.VNName,

                                  LineManagerID = emp.LineManagerID,

                                  LSLocationID = emp.LSLocationID,
                                  LSLocationName =  (LanguageId==10001)?location_entity.Name:location_entity.VNName,

                                  LSPositionID = emp.LSPositionID,
                                  LSPositionName =  (LanguageId==10001)?position_entity.Name:position_entity.VNName,

                                  AccountNumber = emp.AccountNumber,
                                  AccountName =  emp.AccountName,

                                  LSBankID = emp.LSBankID,
                                  LSBankName =  (LanguageId==10001)?bank_entity.Name:bank_entity.VNName,

                                  LSBankBranchID = emp.LSBankBranchID,
                                  LSBankBranchName =  (LanguageId==10001)?bankBranch_entity.Name:bankBranch_entity.VNName,

                                  SelfDeduction = emp.SelfDeduction,
                                  DependDeduction = emp.DependDeduction,
                                  NoOfDependent = emp.NoOfDependent,

                                  SIBook = emp.SIBook,
                                  HIBook = emp.HIBook,
                                  SI = emp.SI,
                                  HI = emp.HI,
                                  UI = emp.UI,
                                  UserMachineID = emp.UserMachineID,
                                  PayByBank = emp.PayByBank,

                                  EmergencyContact = emp.EmergencyContact,
                                  EmergencyAddess = emp.EmergencyAddess,
                                  EmergencyPhone = emp.EmergencyPhone,
                                  EmergencyMobile = emp.EmergencyMobile,
                                  Active = emp.Active
                              }).FirstOrDefault();
                    entity.LineManagerName = GetManagerName(entity.LineManagerID);
                }
                return entity;
            }            
        }

        public bool IsIDExisted(int ID)
        {
            var query = context.HR_tblEmp.FirstOrDefault(c => c.EmpID.Equals(ID));
            return (query != null);
        }
        
        public static bool UpdateFileId(int Id, int FileId)
        {  
            bool result = false;
            try
            {
                List<string> AddedFileIdList = new List<string>();
                List<string> InitialFileIdList = new List<string>();
                using (EntityDataContext context = new EntityDataContext())
                {
                    HR_tblEmp entity = Find(Id);
                    entity.FileId = FileId;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                        result = true;
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }

        public bool UpdateEmpBasics(EmployeeEditModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                HR_tblEmp modelUpdate = Find(model.EmpID);
                if (modelUpdate != null)
                {
                    if (model.LSCompanyID > 0)
                        modelUpdate.LSCompanyID = model.LSCompanyID;

                    if (model.FirstName != null && model.FirstName.ToString() != string.Empty)
                        modelUpdate.FirstName = model.FirstName;

                    if (model.LastName != null && model.LastName.ToString() != string.Empty)
                        modelUpdate.LastName = model.LastName;

                    if (model.Gender > 0)
                        modelUpdate.Gender = model.Gender;

                    if (model.Email != null && model.Email.ToString() != string.Empty)
                        modelUpdate.Email = model.Email;

                    if (model.Telephone != null && model.Telephone.ToString() != string.Empty)
                        modelUpdate.Telephone = model.Telephone;

                    if (model.Mobile != null && model.Mobile.ToString() != string.Empty)
                        modelUpdate.Mobile = model.Mobile;

                    if (model.FileId != null && model.FileId > 0)
                        modelUpdate.FileId = model.FileId;

                    if (model.TaxNo != null && model.TaxNo.ToString() != string.Empty)
                        modelUpdate.TaxNo = model.TaxNo;

                    if (model.IDNo != null && model.IDNo.ToString() != string.Empty)
                        modelUpdate.IDNo = model.IDNo;

                    if (model.IDIssuedPlace != null && model.IDIssuedPlace.ToString() != string.Empty)
                        modelUpdate.IDIssuedPlace = (model.IDIssuedPlace != null && model.IDIssuedPlace > 0) ? Convert.ToInt32(model.IDIssuedPlace) : (int?)null;

                    if (model.IDIssuedDate != null && model.IDIssuedDate.ToString() != string.Empty)
                        modelUpdate.IDIssuedDate = Convert.ToDateTime(model.IDIssuedDate);

                    if (model.DOB != null && model.DOB.ToString() != string.Empty)
                        modelUpdate.DOB = Convert.ToDateTime(model.DOB);

                    if (model.YOB != null && model.YOB.ToString() != string.Empty)
                        modelUpdate.YOB = model.YOB;

                    if (model.StartDate != null && model.StartDate.ToString() != string.Empty)
                        modelUpdate.StartDate = Convert.ToDateTime(model.StartDate);

                    if (model.JoinDate != null && model.JoinDate.ToString() != string.Empty)
                        modelUpdate.JoinDate = Convert.ToDateTime(model.JoinDate);


                    if (model.PAddress != null && model.PAddress.ToString() != string.Empty)
                        modelUpdate.PAddress = model.PAddress;

                    if (model.PLSCountryID != null && model.PLSCountryID > 0)
                        modelUpdate.PLSCountryID = model.PLSCountryID;

                    if (model.PLSProvinceID != null && model.PLSProvinceID > 0)
                        modelUpdate.PLSProvinceID = model.PLSProvinceID;

                    if (model.PLSDistrictID != null && model.PLSDistrictID > 0)
                        modelUpdate.PLSDistrictID = model.PLSDistrictID;

                    //int? PLSCountryID = (model.PLSCountryID != null && model.PLSCountryID > 0) ? Convert.ToInt32(model.PLSCountryID) : (int?)null;

                    //Temporary Address -------------------------------------------------------
                    if (model.TAddress != null && model.TAddress.ToString() != string.Empty)
                        modelUpdate.TAddress = model.TAddress;

                    if (model.TLSCountryID != null && model.TLSCountryID > 0)
                        modelUpdate.TLSCountryID = model.TLSCountryID;

                    if (model.TLSProvinceID != null && model.TLSProvinceID > 0)
                        modelUpdate.TLSProvinceID = model.TLSProvinceID;

                    if (model.TLSDistrictID != null && model.TLSDistrictID > 0)
                        modelUpdate.TLSDistrictID = model.TLSDistrictID;


                    //Born -----------------------------------------------------------------
                    if (model.BornLSCountryID != null && model.BornLSCountryID > 0)
                        modelUpdate.BornLSCountryID = model.BornLSCountryID;

                    if (model.BornLSProvinceID != null && model.BornLSProvinceID > 0)
                        modelUpdate.BornLSProvinceID = model.BornLSProvinceID;

                    if (model.BornLSDistrictID != null && model.BornLSDistrictID > 0)
                        modelUpdate.BornLSDistrictID = model.BornLSDistrictID;


                    //Native ----------------------------------------------------------------
                    if (model.NativeCountryID != null && model.NativeCountryID > 0)
                        modelUpdate.NativeCountryID = model.NativeCountryID;

                    if (model.NativeProvinceID != null && model.NativeProvinceID > 0)
                        modelUpdate.NativeProvinceID = model.NativeProvinceID;

                    if (model.NativeDistrictID != null && model.NativeDistrictID > 0)
                        modelUpdate.NativeDistrictID = model.NativeDistrictID;



                    if (model.LSMaritalID != null && model.LSMaritalID.ToString() != string.Empty)
                        modelUpdate.LSMaritalID = model.LSMaritalID;

                    if (model.LSNationalityID != null && model.LSNationalityID > 0)
                        modelUpdate.LSNationalityID = model.LSNationalityID;

                    if (model.LSEthnicID != null && model.LSEthnicID > 0)
                        modelUpdate.LSEthnicID = model.LSEthnicID;

                    if (model.LSReligionID != null && model.LSReligionID > 0)
                        modelUpdate.LSReligionID = model.LSReligionID;

                    if (model.LineManagerID != null && model.LineManagerID > 0)
                        modelUpdate.LineManagerID = model.LineManagerID;

                    if (model.LSLocationID != null && model.LSLocationID > 0)
                        modelUpdate.LSLocationID = model.LSLocationID;

                    if (model.LSPositionID != null && model.LSPositionID > 0)
                        modelUpdate.LSPositionID = model.LSPositionID;

                    if (model.EmergencyContact != null && model.EmergencyContact.ToString() != string.Empty)
                        modelUpdate.EmergencyContact = model.EmergencyContact;

                    if (model.EmergencyAddess != null && model.EmergencyAddess.ToString() != string.Empty)
                        modelUpdate.EmergencyAddess = model.EmergencyAddess;

                    if (model.EmergencyPhone != null && model.EmergencyPhone.ToString() != string.Empty)
                        modelUpdate.EmergencyPhone = model.EmergencyPhone;

                    if (model.EmergencyMobile != null && model.EmergencyMobile.ToString() != string.Empty)
                        modelUpdate.EmergencyMobile = model.EmergencyMobile;

                    if (model.LastModifiedOnDate != null && model.LastModifiedOnDate.ToString() != string.Empty)
                        modelUpdate.LastModifiedOnDate = DateTime.Now;

                    modelUpdate.BloodType = model.BloodType;
                    context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
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
            return result;
        }

        public bool UpdateEmpQualification(EmployeeEditModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                HR_tblEmp modelUpdate = Find(model.EmpID);
                if (modelUpdate != null)
                {                          
                    if (model.LSEducationID != null && model.LSEducationID > 0)
                        modelUpdate.LSEducationID = model.LSEducationID;

                    if (model.LSMajorID != null && model.LSMajorID > 0)
                        modelUpdate.LSMajorID = model.LSMajorID;


                    if (model.LastModifiedOnDate != null && model.LastModifiedOnDate.ToString() != string.Empty)
                        modelUpdate.LastModifiedOnDate = DateTime.Now;

                    context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
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
            return result;
        }

        public bool UpdateAccountInfo(EmployeeEditModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                HR_tblEmp modelUpdate = Find(model.EmpID);
                if (modelUpdate != null)
                {
                    if (model.AccountNumber != null && model.AccountNumber.ToString() != string.Empty)
                        modelUpdate.AccountNumber = model.AccountNumber;

                    if (model.AccountName != null && model.AccountName.ToString() != string.Empty)
                        modelUpdate.AccountName = model.AccountName;

                    if (model.LSBankID != null && model.LSBankID > 0)
                        modelUpdate.LSBankID = model.LSBankID;

                    if (model.LSBankBranchID != null && model.LSBankBranchID > 0)
                        modelUpdate.LSBankBranchID = model.LSBankBranchID;

                    if (model.SelfDeduction != null && model.SelfDeduction.ToString() != string.Empty)
                        modelUpdate.SelfDeduction = model.SelfDeduction;

                    if (model.DependDeduction != null && model.DependDeduction.ToString() != string.Empty)
                        modelUpdate.DependDeduction = model.DependDeduction;

                    if (model.NoOfDependent != null && model.NoOfDependent > 0)
                        modelUpdate.NoOfDependent = model.NoOfDependent;

                    if (model.SIBook != null && model.SIBook.ToString() != string.Empty)
                        modelUpdate.SIBook = model.SIBook;

                    if (model.HIBook != null && model.HIBook.ToString() != string.Empty)
                        modelUpdate.HIBook = model.HIBook;

                    if (model.PayByBank != null && model.PayByBank.ToString() != string.Empty)
                        modelUpdate.PayByBank = model.PayByBank;

                    if (model.TaxNo != null && model.TaxNo.ToString() != string.Empty)
                        modelUpdate.TaxNo = model.TaxNo;

                    if (model.LastModifiedOnDate != null && model.LastModifiedOnDate.ToString() != string.Empty)
                        modelUpdate.LastModifiedOnDate = DateTime.Now;
                    

                    context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
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
            return result;
        }

        public bool Update(EmployeeEditModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {               
                HR_tblEmp modelUpdate = Find(model.EmpID); 
                if(modelUpdate != null)
                {
                    if (model.LSCompanyID > 0)
                        modelUpdate.LSCompanyID = model.LSCompanyID;

                    if (model.FirstName != null && model.FirstName.ToString() != string.Empty)
                        modelUpdate.FirstName = model.FirstName;

                    if (model.LastName != null && model.LastName.ToString() != string.Empty)
                        modelUpdate.LastName = model.LastName;

                    if (model.Gender > 0)
                        modelUpdate.Gender = model.Gender;

                    if (model.Email != null && model.Email.ToString() != string.Empty)
                        modelUpdate.Email = model.Email;

                    if (model.Telephone != null && model.Telephone.ToString() != string.Empty)
                        modelUpdate.Telephone = model.Telephone;

                    if (model.Mobile != null && model.Mobile.ToString() != string.Empty)
                        modelUpdate.Mobile = model.Mobile;

                    if (model.FileId != null && model.FileId > 0)
                        modelUpdate.FileId = model.FileId;

                    if (model.TaxNo != null && model.TaxNo.ToString() != string.Empty)
                        modelUpdate.TaxNo = model.TaxNo;

                    if (model.IDNo != null && model.IDNo.ToString() != string.Empty)
                        modelUpdate.IDNo = model.IDNo;

                    if (model.IDIssuedPlace != null && model.IDIssuedPlace.ToString() != string.Empty)
                        modelUpdate.IDIssuedPlace = (model.IDIssuedPlace != null && model.IDIssuedPlace > 0) ? Convert.ToInt32(model.IDIssuedPlace) : (int?)null;

                    if (model.IDIssuedDate != null && model.IDIssuedDate.ToString() != string.Empty)
                        modelUpdate.IDIssuedDate = Convert.ToDateTime(model.IDIssuedDate);

                    if (model.DOB != null && model.DOB.ToString() != string.Empty)
                        modelUpdate.DOB = Convert.ToDateTime(model.DOB);

                    if (model.YOB != null && model.YOB.ToString() != string.Empty)
                        modelUpdate.YOB = model.YOB;

                    if (model.StartDate != null && model.StartDate.ToString() != string.Empty)
                        modelUpdate.StartDate = Convert.ToDateTime(model.StartDate);

                    if (model.JoinDate != null && model.JoinDate.ToString() != string.Empty)
                        modelUpdate.JoinDate = Convert.ToDateTime(model.JoinDate);

                    if (model.LastModifiedOnDate != null && model.LastModifiedOnDate.ToString() != string.Empty)
                        modelUpdate.LastModifiedOnDate = DateTime.Now;

                    if (model.EmergencyContact != null && model.EmergencyContact.ToString() != string.Empty)
                        modelUpdate.EmergencyContact = model.EmergencyContact;

                    if (model.EmergencyAddess != null && model.EmergencyAddess.ToString() != string.Empty)
                        modelUpdate.EmergencyAddess = model.EmergencyAddess;

                    if (model.EmergencyPhone != null && model.EmergencyPhone.ToString() != string.Empty)
                        modelUpdate.EmergencyPhone = model.EmergencyPhone;

                    if (model.EmergencyMobile != null && model.EmergencyMobile.ToString() != string.Empty)
                        modelUpdate.EmergencyMobile = model.EmergencyMobile;

                    if (model.PAddress != null && model.PAddress.ToString() != string.Empty)
                        modelUpdate.PAddress = model.PAddress;

                    if (model.PLSCountryID != null && model.PLSCountryID > 0)
                        modelUpdate.PLSCountryID = model.PLSCountryID;

                    if (model.PLSProvinceID != null && model.PLSProvinceID > 0)
                        modelUpdate.PLSProvinceID = model.PLSProvinceID;

                    if (model.PLSDistrictID != null && model.PLSDistrictID > 0)
                        modelUpdate.PLSDistrictID = model.PLSDistrictID;

                    //int? PLSCountryID = (model.PLSCountryID != null && model.PLSCountryID > 0) ? Convert.ToInt32(model.PLSCountryID) : (int?)null;

                    //Temporary Address -------------------------------------------------------
                    if (model.TAddress != null && model.TAddress.ToString() != string.Empty)
                        modelUpdate.TAddress = model.TAddress;

                    if (model.TLSCountryID != null && model.TLSCountryID > 0)
                        modelUpdate.TLSCountryID = model.TLSCountryID;

                    if (model.TLSProvinceID != null && model.TLSProvinceID > 0)
                        modelUpdate.TLSProvinceID = model.TLSProvinceID;

                    if (model.TLSDistrictID != null && model.TLSDistrictID > 0)
                        modelUpdate.TLSDistrictID = model.TLSDistrictID;


                    //Born -----------------------------------------------------------------
                    if (model.BornLSCountryID != null && model.BornLSCountryID > 0)
                        modelUpdate.BornLSCountryID = model.BornLSCountryID;

                    if (model.BornLSProvinceID != null && model.BornLSProvinceID > 0)
                        modelUpdate.BornLSProvinceID = model.BornLSProvinceID;

                    if (model.BornLSDistrictID != null && model.BornLSDistrictID > 0)
                        modelUpdate.BornLSDistrictID = model.BornLSDistrictID;


                    //Native ----------------------------------------------------------------
                    if (model.NativeCountryID != null && model.NativeCountryID > 0)
                        modelUpdate.NativeCountryID = model.NativeCountryID;

                    if (model.NativeProvinceID != null && model.NativeProvinceID > 0)
                        modelUpdate.NativeProvinceID = model.NativeProvinceID;

                    if (model.NativeDistrictID != null && model.NativeDistrictID > 0)
                        modelUpdate.NativeDistrictID = model.NativeDistrictID;



                    if (model.LSMaritalID != null && model.LSMaritalID.ToString() != string.Empty)
                        modelUpdate.LSMaritalID = model.LSMaritalID;

                    if (model.LSNationalityID != null && model.LSNationalityID > 0)
                        modelUpdate.LSNationalityID = model.LSNationalityID;

                    if (model.LSEthnicID != null && model.LSEthnicID > 0)
                        modelUpdate.LSEthnicID = model.LSEthnicID;

                    if (model.LSReligionID != null && model.LSReligionID > 0)
                        modelUpdate.LSReligionID = model.LSReligionID;

                    if (model.LSEducationID != null && model.LSEducationID > 0)
                        modelUpdate.LSEducationID = model.LSEducationID;                  

                    if (model.LSMajorID != null && model.LSMajorID > 0)
                        modelUpdate.LSMajorID = model.LSMajorID;

                    if (model.LineManagerID != null && model.LineManagerID > 0)
                        modelUpdate.LineManagerID = model.LineManagerID;

                    if (model.LSLocationID != null && model.LSLocationID > 0)
                        modelUpdate.LSLocationID = model.LSLocationID;

                    if (model.LSPositionID != null && model.LSPositionID > 0)
                        modelUpdate.LSPositionID = model.LSPositionID;

                    if (model.AccountNumber != null && model.AccountNumber.ToString() != string.Empty)
                        modelUpdate.AccountNumber = model.AccountNumber;

                    if (model.AccountName != null && model.AccountName.ToString() != string.Empty)
                        modelUpdate.AccountName = model.AccountName;

                    if (model.LSBankID != null && model.LSBankID > 0)
                        modelUpdate.LSBankID = model.LSBankID;

                    if (model.LSBankBranchID != null && model.LSBankBranchID > 0)
                        modelUpdate.LSBankBranchID = model.LSBankBranchID;

                    if (model.SelfDeduction != null && model.SelfDeduction.ToString() != string.Empty)
                        modelUpdate.SelfDeduction = model.SelfDeduction;

                    if (model.DependDeduction != null && model.DependDeduction.ToString() != string.Empty)
                        modelUpdate.DependDeduction = model.DependDeduction;

                    if (model.NoOfDependent != null && model.NoOfDependent > 0)
                        modelUpdate.NoOfDependent = model.NoOfDependent;

                    if (model.SIBook != null && model.SIBook.ToString() != string.Empty)
                        modelUpdate.SIBook = model.SIBook;

                    if (model.HIBook != null && model.HIBook.ToString() != string.Empty)
                        modelUpdate.HIBook = model.HIBook;

                    if (model.PayByBank != null && model.PayByBank.ToString() != string.Empty)
                        modelUpdate.PayByBank = model.PayByBank;

                    modelUpdate.BloodType = model.BloodType;
                    //modelUpdate.Active = model.Active;

                    context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }else
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
            return result;
        }

        public static bool UpdateStatus(int id, out string Message)
        {
            Message = string.Empty;
            bool result = false;

            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    HR_tblEmp entity = Find(id);
                    bool? status = entity.Active;
                    if (status != null && status == true)
                        entity.Active = false;
                    else
                        entity.Active = true;
                    entity.LastModifiedOnDate = DateTime.Now;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public bool DeactivateStatus(int id)
        {
            bool result = false;
          
            try
            {
                HR_tblEmp entity = Find(id);
                entity.Active = false;
                entity.LastModifiedOnDate = DateTime.Now;
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                int i = context.SaveChanges();
                if (i == 1)               
                    result = true; 
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
            }
            return result;
        }

        public bool ActivateStatus(int id)
        {
            bool result = false;

            try
            {
                HR_tblEmp entity = Find(id);
                entity.Active = true;
                entity.LastModifiedOnDate = DateTime.Now;
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                int i = context.SaveChanges();
                if (i == 1)
                    result = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
            }
            return result;
        }

        public EmployeeViewModel GetEmployeeProfile(int? EmpID, string LanguageCode)
        {
            var entity = (from emp in context.HR_tblEmp
                          join position in context.LS_tblPosition on emp.LSPositionID equals position.LSPositionID into list1
                          from l1 in list1.DefaultIfEmpty()
                          join company in context.LS_tblCompany on emp.LSCompanyID equals company.LSCompanyID into list4
                          from l4 in list4.DefaultIfEmpty()
                          join location in context.LS_tblLocation on emp.LSLocationID equals location.LSLocationID into list5
                          from location in list5.DefaultIfEmpty()
                          where emp.EmpID == EmpID
                          select new EmployeeViewModel()
                          {
                              Position = (LanguageCode == "en-US") ? l1.Name : l1.VNName,
                              CompanyName = (LanguageCode == "en-US") ? l4.Name : l4.VNName,
                              LSLocationName = (LanguageCode == "en-US") ? location.Name : location.VNName,

                              EmpID = emp.EmpID,
                              EmpCode = emp.EmpCode,  
                              FirstName = emp.FirstName,
                              LastName = emp.LastName,
                              FullName = emp.LastName + " " + emp.FirstName,
                              FileId = emp.FileId,
                              DOB = emp.DOB,
                              YOB = emp.YOB,
                              StartDate = emp.StartDate,
                              JoinDate = emp.JoinDate
                          }).FirstOrDefault();
            return entity;
        }

        public EmployeeViewModel GetEmployee(int? EmpID, int LanguageId)
        {
                var entity = (from emp in context.HR_tblEmp
                              join position in context.LS_tblPosition on emp.LSPositionID equals position.LSPositionID into list1
                              from l1 in list1.DefaultIfEmpty()
                              join company in context.LS_tblCompany on emp.LSCompanyID equals company.LSCompanyID into list4
                              from l4 in list4.DefaultIfEmpty()
                              join location in context.LS_tblLocation on emp.LSLocationID equals location.LSLocationID into list5
                              from location in list5.DefaultIfEmpty()
                              where emp.EmpID == EmpID
                              select new EmployeeViewModel()
                              {
                                  Position = (LanguageId == 124) ? l1.Name : l1.VNName,
                                  CompanyName = (LanguageId == 124) ? l4.Name : l4.VNName,
                                  LSLocationName = (LanguageId == 124) ? location.Name : location.VNName,

                                  EmpID = emp.EmpID,
                                  EmpCode = emp.EmpCode,
                                  FirstName = emp.FirstName,
                                  LastName = emp.LastName,
                                  FullName = emp.LastName + " " + emp.FirstName,
                                  FileId = emp.FileId,
                                  DOB = emp.DOB,
                                  YOB = emp.YOB,
                                  StartDate = emp.StartDate,
                                  JoinDate = emp.JoinDate
                              }).FirstOrDefault();
                return entity;
        }

        public List<EmployeeSearchViewModel> GetByListID(List<int> ListIDAdd, int LanguageId)
        {
            var result = from p in context.HR_tblEmp
                         join company in context.LS_tblCompany on p.LSCompanyID equals company.LSCompanyID
                         where ListIDAdd.Contains(p.EmpID)
                         select new EmployeeSearchViewModel()
                         {
                             EmpID = p.EmpID,
                             EmpCode = p.EmpCode,
                             FullName = p.LastName + " " + p.FirstName,
                             LSCompanyName = (LanguageId == 124) ? company.Name : company.VNName
                         };
            return result.ToList();

        }
      
        #region Bind DropdownList Employee - FullName - EmpID =========================================================================================
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = (context.HR_tblEmp
                           .Where(c => c.FirstName.ToLower().Contains(searchTerm.ToLower()) || c.LastName.ToLower().Contains(searchTerm.ToLower())
                               || c.EmpCode.Contains(searchTerm) || searchTerm == null || searchTerm == "")
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.EmpID,
                               name = c.LastName + " " + c.FirstName,
                               text = c.EmpCode + "-" + c.LastName + " " + c.FirstName + " <" + c.Email + ">"
                           })).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ================================================================================================================================

        #region Bind DropdownList Employee Based On - Code  ======================================================================================= 
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteViewModel> ListDropdownOnSingleCode(string searchTerm, int pageSize, int pageNume, int? LSCompanyID, int LanguageId)
        {
            List<AutoCompleteViewModel> lst = new List<AutoCompleteViewModel>();
            string Lineage = "";
            if (LSCompanyID != null && LSCompanyID != 0)
                Lineage = context.LS_tblCompany.Find(LSCompanyID).Lineage;

            lst = (from empl in context.HR_tblEmp
                   join com in context.LS_tblCompany on empl.LSCompanyID equals com.LSCompanyID into companyList
                   from comlst in companyList.DefaultIfEmpty()
                   where (LSCompanyID == null || LSCompanyID == 0 || comlst.Lineage.Contains(Lineage))
                   && (searchTerm == null || searchTerm == "" || empl.EmpCode.Contains(searchTerm))
                   select new AutoCompleteViewModel
                   {
                       id = empl.EmpCode,
                       name = empl.EmpCode,
                       text = empl.EmpCode + " - " + empl.LastName + " " + empl.FirstName,
                   }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize).ToList();
            return lst;
        }
        #endregion ===============================================================================================
        
        #region Bind DropdownList Employee Based On - Code - EmpId ===============================================
        private List<AutoCompleteModel> GetDropdownListOnCode(string searchTerm, int? LSCompanyID, int LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            string Lineage = "";
            if (LSCompanyID != null && LSCompanyID != 0)
                Lineage = context.LS_tblCompany.Find(LSCompanyID).Lineage;

            lst = (from empl in context.HR_tblEmp
                   join com in context.LS_tblCompany on empl.LSCompanyID equals com.LSCompanyID into companyList
                   from comlst in companyList.DefaultIfEmpty()
                   where (LSCompanyID == null || LSCompanyID == 0 || comlst.Lineage.Contains(Lineage))
                   && (searchTerm == null || searchTerm == "" || empl.EmpCode.Contains(searchTerm))
                   select new AutoCompleteModel
                   {
                       id = empl.EmpID,
                       name = empl.EmpCode,
                       text = empl.EmpCode + " - " + empl.LastName + " " + empl.FirstName
                   }).ToList();
            return lst;
        }
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdownOnCode(string searchTerm, int pageSize, int pageNume, int? LSCompanyID, int LanguageId)
        {
            return GetDropdownListOnCode(searchTerm, LSCompanyID, LanguageId).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize).ToList();
        }
        #endregion ===============================================================================================

        #region Bind DropdownList Employee Based On - FullName - EmpID ===============================================
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdownOnFullName(string searchTerm, int pageSize, int pageNum, int? LSCompanyID, int LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            string Lineage = "";
            if (LSCompanyID != null && LSCompanyID != 0)
                Lineage = context.LS_tblCompany.Find(LSCompanyID).Lineage;

            lst = (from emp in context.HR_tblEmp
                   join co in context.LS_tblCompany on emp.LSCompanyID equals co.LSCompanyID into companyList
                   from com in companyList.DefaultIfEmpty()
                   where (LSCompanyID == null || LSCompanyID == 0 || com.Lineage.Contains(Lineage))
                   && (searchTerm == null || searchTerm == "" || emp.EmpCode.ToUpper().Contains(searchTerm.ToUpper()))
                   select new AutoCompleteModel
                   {
                       id = emp.EmpID,
                       name = emp.LastName + " " + emp.FirstName,
                       text = emp.LastName + " " + emp.FirstName + " - " + ((LanguageId == 124) ? com.Name : com.VNName)
                   }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize).ToList();
            return lst;
        }
        #endregion ===============================================================================================

        #region Bind DropdownList Employee Based On - FullName  ===============================================
        private List<AutoCompleteViewModel> GetDropdownListOnSingleFullName(string searchTerm, int? LSCompanyID, int LanguageId)
        {
            List<AutoCompleteViewModel> lst = new List<AutoCompleteViewModel>();
            string Lineage = "";
            if (LSCompanyID != null && LSCompanyID != 0)
                Lineage = context.LS_tblCompany.Find(LSCompanyID).Lineage;

            lst = (from emp in context.HR_tblEmp
                   join co in context.LS_tblCompany on emp.LSCompanyID equals co.LSCompanyID into companyList
                   from com in companyList.DefaultIfEmpty()
                   where (LSCompanyID == null || LSCompanyID == 0 || com.Lineage.Contains(Lineage))
                   && (searchTerm == null || searchTerm == "" || emp.EmpCode.ToUpper().Contains(searchTerm.ToUpper()))
                   select new AutoCompleteViewModel
                   {
                       id = emp.LastName + " " + emp.FirstName,
                       name = emp.LastName + " " + emp.FirstName,
                       text = emp.LastName + " " + emp.FirstName + " - " + ((LanguageId == 124) ? com.Name : com.VNName)
                   }).ToList();
            return lst;
        }
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteViewModel> ListDropdownOnSingleFullName(string searchTerm, int pageSize, int pageNum, int? LSCompanyID, int LanguageId)
        {
            return GetDropdownListOnSingleFullName(searchTerm, LSCompanyID, LanguageId).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize).ToList();
        }
        #endregion ===============================================================================================

        #region Bind DropdownList Employee Chưa nghỉ việc và không chuẩn bị nghỉ việc      
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdown2(string searchTerm, int pageSize, int pageNume)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
             lst = (context.HR_tblEmp
                           .Where(c => (searchTerm == null || searchTerm == "" || (c.LastName + " " + c.FirstName + " " + c.EmpCode).ToLower().Contains(searchTerm.ToLower()))
                               && c.Active == true)
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.EmpID,
                               name = c.LastName + " " + c.FirstName + " | " + c.Email,
                               text = c.LastName + " " + c.FirstName + " | " + c.Email 
                           })).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
             return lst;
        }
        #endregion ===============================================================================================================================
        
        #region Bind DropdownList Employee for Sending Mail ======================================================================================     
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdownForSedingMail(string searchTerm, int pageSize, int pageNume)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.HR_tblEmp
                           .Where(c => c.FirstName.ToLower().Contains(searchTerm.ToLower()) || c.LastName.ToLower().Contains(searchTerm.ToLower())
                               || c.EmpCode.Contains(searchTerm) || searchTerm == null || searchTerm == "")
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.EmpID,
                               name = c.LastName + " " + c.FirstName,
                               text = c.LastName + " " + c.FirstName + " " + c.Email
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ===============================================================================================

        #region Bind DropdownList Employee Based On - FullName - Department =======================================================================      
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdownOnFullNameDepartment(string searchTerm, int pageSize, int pageNum, int LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = (from emp in context.HR_tblEmp
                   join c in context.LS_tblCompany on emp.LSCompanyID equals c.LSCompanyID into colst
                   from com in colst.DefaultIfEmpty()
                   where (emp.LastName +" "+ emp.FirstName).Contains(searchTerm)
                   select new AutoCompleteModel
                   {
                       id = emp.EmpID,
                       name = emp.LastName + " " + emp.FirstName + " - " + ((LanguageId == 124) ? com.Name : com.VNName),
                       text = emp.LastName + " " + emp.FirstName + " - " + ((LanguageId == 124) ? com.Name : com.VNName)
                   }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize).ToList();
            return lst;
        }
        #endregion ==================================================================================================================================
    
    }
}
