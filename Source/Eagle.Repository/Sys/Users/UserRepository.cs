using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
using Eagle.Model.HR;
using Eagle.Common.Security.Cryptography;
using Eagle.Common.Utilities;
using Eagle.Repository.Mail;
using System.Collections;
using Eagle.Common.Settings;
using Eagle.Model.SYS.User;
using System.Web;
using Eagle.Repository.HR;

namespace Eagle.Repository.SYS.Users
{
    public class UserRepository
    {
        public EntityDataContext context { get; set; }
        public UserRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public static UserViewModel GetDetails(string UserName, string Password)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                string HashedPassword = MD5Crypto.GetMd5Hash(Password);
                UserViewModel entity = (from u in context.Users
                                        join r in context.UserRoles on u.UserId equals r.UserId into rolelist
                                        from rl in rolelist.DefaultIfEmpty()
                                        join ue in context.UserEmployees on u.UserId equals ue.UserId into useremp
                                        from uel in useremp.DefaultIfEmpty()
                                        join e in context.HR_tblEmp on uel.EmpId equals e.EmpID into temp
                                        from emp in temp.DefaultIfEmpty()
                                        where u.UserName == UserName && u.Password == HashedPassword && u.IsApproved == true && u.IsLockedOut == false
                                        && (u.EndDate == null || (u.EndDate !=null && u.EndDate > DateTime.Now))
                                        select new UserViewModel()
                                           {
                                               ApplicationId = u.ApplicationId,
                                               EmpId = emp.EmpID,
                                               UserId = u.UserId,
                                               RoleId = rl.RoleId,
                                               UserCode = u.UserCode,
                                               UserName = u.UserName,
                                               LoweredUserName = u.LoweredUserName,
                                               FirstName = u.FirstName,
                                               LastName = u.LastName,
                                               FullName = u.FirstName + " " + u.LastName,
                                               DisplayName = u.DisplayName,
                                               Password = u.Password,
                                               PasswordSalt = u.PasswordSalt,
                                               PasswordFormat = u.PasswordFormat,

                                               Email = u.Email,
                                               PasswordQuestion = u.PasswordQuestion,
                                               PasswordAnswer = u.PasswordAnswer,
                                               Phone = u.Phone,
                                               Mobile = u.Mobile,
                                               Address = u.Address,

                                               StartDate = u.StartDate,
                                               EndDate = u.EndDate,

                                               IsAnonymous = u.IsAnonymous,
                                               IsDeleted = u.IsDeleted,
                                               IsSuperUser = u.IsSuperUser,
                                               IsApproved = u.IsApproved,
                                               ScopeTypeId = (u.IsSuperUser != null && u.IsSuperUser == true)? 1: 2,

                                               IsLockedOut = u.IsLockedOut,
                                               UpdatePassword = u.UpdatePassword,
                                               LastIPAddress = u.LastIPAddress,
                                               LastActivityDate = u.LastActivityDate
                                           }).FirstOrDefault();
                return entity;
            }
        }
        public static AccountViewModel GetDetailsByUserNameAndPassword(string UserName, string Password)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                Password = Eagle.Common.Utilities.StringUtils.GetMd5Sum(Password);
                AccountViewModel entity = (from u in context.SYS_tblUserAccount
                                           join c in context.HR_tblEmp on u.EmpID equals c.EmpID into temp
                                           from item in temp.DefaultIfEmpty()
                                           join t in context.TER_tblTermination on item.EmpID equals t.EmpID into ter
                                           from termination in ter.DefaultIfEmpty()
                                           where u.UserName == UserName && u.Password == Password &&
                                           ((item.Active == true || (item.Active == false && termination.LastWorkingDate > DateTime.Now) ||
                                               u.UserName == "admin"
                                           ))
                                           select new AccountViewModel()
                                             {
                                                 EmpId = u.EmpID,
                                                 FirstName = item.FirstName,
                                                 LastName = item.LastName,
                                                 FullName = item.LastName + " " + item.FirstName,
                                                 UserName = u.UserName,
                                                 FromDate = u.FromDate,
                                                 ToDate = u.ToDate,
                                                 IsLDAP = u.IsLDAP,
                                                 FAdm = u.FAdm,
                                                 FLock = u.FLock,
                                                 UserID = u.UserID,
                                                 LockDate = u.LockDate
                                             }).FirstOrDefault();
                return entity;
            }
        }

        public bool CheckExist(string strValidate)
        {
            var code = context.SYS_tblUserAccount.FirstOrDefault(p => p.UserName.Equals(strValidate));
            return (code != null);
        }
    
        public static bool CheckLogin(string UserName, string Password, string LanguageCode)
        {
            bool result = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    UserViewModel userInfo = GetDetails(UserName, Password);
                    if (userInfo != null)
                    {
                        //Set up Language Enviroment
                        System.Globalization.CultureInfo myCIclone = new System.Globalization.CultureInfo(LanguageCode);
                        myCIclone.DateTimeFormat = myCIclone.DateTimeFormat;
                        HttpContext.Current.Session[SettingKeys.CurrentLanguage] = myCIclone;
                        HttpContext.Current.Session[SettingKeys.LanguageCode] = LanguageCode;
                        HttpContext.Current.Session[SettingKeys.LanguageId] = (LanguageCode == "vi-VN") ? 124 : 41;

                        //Save user info into session
                        HttpContext.Current.Session[SettingKeys.UserInfo] = userInfo;
                        HttpContext.Current.Session[SettingKeys.UserId] = userInfo.UserId;
                        HttpContext.Current.Session[SettingKeys.UserName] = userInfo.UserName;
                        HttpContext.Current.Session[SettingKeys.IsSuperUser] = userInfo.IsSuperUser;
                        HttpContext.Current.Session[SettingKeys.RoleId] = userInfo.RoleId;
                        HttpContext.Current.Session[SettingKeys.ScopeTypeId] = userInfo.ScopeTypeId;
                        HttpContext.Current.Session[SettingKeys.ApplicationId] = userInfo.ApplicationId;

                        HttpContext.Current.Session[SettingKeys.DisplayName] = userInfo.DisplayName;
                        HttpContext.Current.Session[SettingKeys.DisplayName] = userInfo.DisplayName;

                        //Save employee info into session
                        EmployeeViewModel empInfo = EmployeeRepository.GetDetailsByEmpIdAndLanguageCode(userInfo.EmpId, LanguageCode);
                        if (empInfo != null)
                        {
                            HttpContext.Current.Session[SettingKeys.EmpInfo] = empInfo;
                            HttpContext.Current.Session[SettingKeys.EmpId] = empInfo.EmpID;
                            HttpContext.Current.Session[SettingKeys.EmpCode] = empInfo.EmpCode;
                            userInfo.EmployeeInfo = empInfo;
                        }
                        else
                        {
                            empInfo = new EmployeeViewModel() { FullName = userInfo.UserName };
                            HttpContext.Current.Session[SettingKeys.EmpInfo] = empInfo;
                            HttpContext.Current.Session[SettingKeys.EmpId] = empInfo.EmpID;
                            HttpContext.Current.Session[SettingKeys.EmpCode] = empInfo.EmpCode;
                        }

                        HttpContext.Current.Session[SettingKeys.MenuList] = null;


                        //Load Menu List By LanguageCode
                        //List<SYS_tblFunctionPermissionViewModel> moduleList = context.sp_clsCommon("ModuleList", LanguageCode, "", 0, 0, 1, userInfo.UserName).Select(p => new SYS_tblFunctionPermissionViewModel() { Url = p.Url, FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameE, Rank = p.Rank, Parent = p.Parent, Tooltips = p.Tooltips, FView = (p.FView != 0 & p.FView != null), FEdit = (p.FEdit != 0 && p.FEdit != null), FDelete = (p.FDelete != 0 && p.FDelete != null), FExport = (p.FExport != 0 && p.FExport != null) }).ToList();
                        //moduleList.Add(new SYS_tblFunctionPermissionViewModel() { Url = "/Admin/ChangePassword" });
                        //HttpContext.Current.Session[SettingKeys.MenuList] = moduleList;
                        //Get Application Settings
                        //Session["PortalId"] = 1;
                        //PortalInfo portal_entity = PortalRespository.GetPortal(Convert.ToInt32(Session["PortalId"]));
                        //Session["PortalInfo"] = portal_entity;

                        result = true;
                    }
                }
                catch (Exception ex){ ex.ToString();}
            }
            return result;
        }

        public void SwitchLanguage(string LanguageCode)
        {
            System.Globalization.CultureInfo myCIclone = new System.Globalization.CultureInfo(LanguageCode);
            myCIclone.DateTimeFormat = myCIclone.DateTimeFormat;
            HttpContext.Current.Session["CurrentLanguage"] = myCIclone;
            HttpContext.Current.Session["LanguageCode"] = LanguageCode;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    UserViewModel acc = (UserViewModel)HttpContext.Current.Session[Eagle.Common.Settings.SettingKeys.UserInfo];

                    //Thay doi MENU theo LanguageCode
                    List<SYS_tblFunctionPermissionViewModel> moduleList = context.sp_clsCommon("ModuleList", LanguageCode, "", 0, 0, 1, acc.UserName).Select(p => new SYS_tblFunctionPermissionViewModel() { Url = p.Url, FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameE, Rank = p.Rank, Parent = p.Parent, Tooltips = p.Tooltips, FView = (p.FView != 0 && p.FView != null), FEdit = (p.FEdit != 0 && p.FEdit != null), FDelete = (p.FDelete != 0 && p.FDelete != null), FExport = (p.FExport != 0 && p.FExport != null) }).ToList();
                    moduleList.Add(new SYS_tblFunctionPermissionViewModel() { Url = "/Admin/ChangePassword" });
                    HttpContext.Current.Session[SettingKeys.MenuList] = moduleList;

                    //Thay doi thong tin tai khoan User Account theo LanguageCode
                    EmployeeViewModel model = new EmployeeViewModel();
                    EmployeeViewModel Emp = (EmployeeViewModel)HttpContext.Current.Session[SettingKeys.EmpInfo];
                    if (Emp != null)
                        HttpContext.Current.Session[SettingKeys.EmpInfo] = Emp;
                    else
                    {
                        EmployeeRepository _repository = new EmployeeRepository(context);
                        var modelEmpId = _repository.GetEmployeeProfile(acc.EmpId, LanguageCode);
                        if (modelEmpId != null)
                        {
                            modelEmpId = new EmployeeViewModel() { FullName = acc.UserName };
                        }
                        HttpContext.Current.Session[SettingKeys.EmpInfo] = modelEmpId;
                    }                  
                   
                }
              
            }
            catch (Exception ex)
            {
                ex.ToString();
            }       
        }
        
        public string GetPasswordSalt(string Email)
        {
            string PasswordSalt = string.Empty;
            if (!string.IsNullOrEmpty(Email))
                PasswordSalt = (from u in context.Users where u.Email == Email select u.PasswordSalt).FirstOrDefault();
            return PasswordSalt;
        }
        public string GetPasswordSalt(string Email, string PasswordQuestion, string PasswordAnswer)
        {
           string PasswordSalt = string.Empty;
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(PasswordQuestion) && !string.IsNullOrEmpty(PasswordAnswer))
                PasswordSalt = (from u in context.Users where u.Email == Email && u.PasswordQuestion == PasswordQuestion && u.PasswordAnswer == PasswordAnswer select u.PasswordSalt).FirstOrDefault();         
            return PasswordSalt;
        }
        public bool ChangePassword(string UserName, string OldPassword, string NewPassword, string RetypeNewPassword, out string Message)
        {
            Message = "";
            bool result = false;
            if (NewPassword != RetypeNewPassword)
                Message = Eagle.Resource.LanguageResource.PasswordMismatch;
            else
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    try
                    {
                        var entity = context.Users.Where(p => p.PasswordSalt == OldPassword && p.UserName == UserName).FirstOrDefault();
                        if (entity != null)
                        {

                            entity.Password = MD5Crypto.GetMd5Hash(NewPassword);
                            entity.PasswordSalt = NewPassword;
                            entity.LastPasswordChangedDate = DateTime.Now;
                            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                            int affectedRow = context.SaveChanges();
                            if (affectedRow > 0)
                            {
                                Message = Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                                result = true;
                            }

                        }
                        else
                        {
                            Message = Eagle.Resource.LanguageResource.NoDataFound;
                        }
                    }
                    catch (Exception ex)
                    {
                        Message = ex.ToString();
                    }
                }
            }
            return result;

        }
        public bool ResetPassword(string Email, out string NewPassword)
        {
            bool result = false;
            NewPassword = string.Empty;
            if (!string.IsNullOrEmpty(Email))
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    User entity = (from u in context.Users where u.Email == Email select u).FirstOrDefault();
                    if (entity != null)
                    {
                        string FullName = entity.FirstName+ " " + entity.LastName;
                        NewPassword = RandomPassword.Generate(8);
                        entity.PasswordSalt = NewPassword;
                        entity.Password = MD5Crypto.GetMd5Hash(NewPassword);
                        entity.LastActivityDate = DateTime.Now;
                        entity.LastIPAddress = NetworkUtils.GetIP4Address();
                        context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            Hashtable TemplateVariables = new Hashtable();
                            TemplateVariables.Add("FullName", FullName);
                            TemplateVariables.Add("NewPassword", NewPassword);
                            int Mail_Template_Id = 33;
                            if (MailTemplateRespository.SendGMailByTemplate(TemplateVariables, Mail_Template_Id, Email, null, null))
                                result = true;
                        }
                            
                    }
                }               
            }
            return result;
        }
        public AccountCreateModel FindEditModel(int id)
        {
            try
            {

                var tmp = from u in context.SYS_tblUserAccount.AsEnumerable()
                          join c in context.HR_tblEmp.AsEnumerable() on u.EmpID equals c.EmpID into temp
                          from item in temp.DefaultIfEmpty(new HR_tblEmp { EmpID = 0 })
                          where u.UserID == id
                          select  new AccountViewModel()
                          {
                              EmpId = u.EmpID,
                              FirstName = item.FirstName,
                              LastName = item.LastName,
                              UserName = u.UserName,
                              FromDate = u.FromDate,
                              ToDate = u.ToDate,
                              IsLDAP = u.IsLDAP,
                              FAdm = u.FAdm,
                              FLock = u.FLock,
                              UserID = u.UserID,
                              LockDate = u.LockDate,
                          };
                var model = tmp.FirstOrDefault();
                if (model != null)
                {
                    AccountCreateModel p = new AccountCreateModel()
                    {
                        EmpID = model.EmpId,
                        strEmpName = model.LastName + " " + model.FirstName,
                        UserName = model.UserName,
                        FromDate = model.FromDate,
                        ToDate = model.ToDate,
                        IsLDAP = model.IsLDAP == 1,
                        FAdm = model.FAdm == 1,
                        FLock = model.FLock == 1,
                        UserID = model.UserID,
                        LockDate = model.LockDate,
                    };
                    return p;
                }
                return null;

             }
            catch//(Exception ex)
            {
                return null;
            }
        }
        public bool AddAccount(SYS_tblUserAccount model,out List<string> errorMessage)
        {
            errorMessage = new List<string>();

            if (model.ToDate != null && model.FromDate != null && model.ToDate.Value.CompareTo(model.FromDate.Value) <= 0)
            {

                errorMessage.Add(string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid,
                                            Eagle.Resource.LanguageResource.ToDate,
                                            Eagle.Resource.LanguageResource.FromDate));
                return false;
            }
            if (model.LockDate != null && model.FromDate != null && model.LockDate.Value.CompareTo(model.FromDate.Value) <= 0)
            {
                errorMessage.Add(string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid,
                        Eagle.Resource.LanguageResource.LockDate,
                        Eagle.Resource.LanguageResource.FromDate));
                return false;
            }
            if (CheckExist(model.UserName))
            {
                errorMessage.Add(Eagle.Resource.LanguageResource.ExistUser);
                return false;
            }
            if (CheckEmployeeExists(model.EmpID))
            {
                errorMessage.Add(Eagle.Resource.LanguageResource.EmployeeExists);
                return false;
            }

            model.Password = Eagle.Common.Utilities.StringUtils.GetMd5Sum(model.Password);
            model.Editer = model.Editer;
            model.CreateDate = DateTime.Now; 

            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return true;
        }

        private bool CheckEmployeeExists(int? EmpID)
        {
            var code = context.SYS_tblUserAccount.FirstOrDefault(p => p.EmpID == EmpID);
            return (code != null);
        }
        public SYS_tblUserAccount Find(int id)
        {
            return context.SYS_tblUserAccount.Find(id);
        }
        public bool Update(AccountCreateModel model,out string ErrorMessage)
        {
            try
            {
                if (model.ToDate != null && model.FromDate != null && model.ToDate.Value.CompareTo(model.FromDate.Value) <= 0)
                {
                    
                    ErrorMessage = string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid,
                                                Eagle.Resource.LanguageResource.ToDate,
                                                Eagle.Resource.LanguageResource.FromDate);
                    return false;
                }
                if (model.LockDate != null && model.FromDate != null && model.LockDate.Value.CompareTo(model.FromDate.Value) <= 0)
                {
                    ErrorMessage = string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid,
                            Eagle.Resource.LanguageResource.LockDate,
                            Eagle.Resource.LanguageResource.FromDate);
                    return false;
                }
                SYS_tblUserAccount modelUpdate = Find(model.UserID);
                modelUpdate.UserName = model.UserName;
                modelUpdate.FromDate = model.FromDate;
                modelUpdate.ToDate =model.ToDate;
                modelUpdate.LockDate = model.LockDate;
                modelUpdate.FLock = model.FLock == true ? 1 : 0;
                modelUpdate.FAdm = model.FAdm == true ? 1 : 0;
                modelUpdate.IsLDAP = model.IsLDAP == true ? 1 : 0;
                modelUpdate.Editer = model.Editer;
                modelUpdate.EditDate = DateTime.Now; 

                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            
        }
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                SYS_tblUserAccount modelUpdate = Find(id);
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

        }

        public IEnumerable<AccountViewModel> Search(string userGroupID, int? moduleID, bool isAdmin)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                var tmp = from u in context.SYS_tblUserAccount.AsEnumerable()
                          join c in context.HR_tblEmp.AsEnumerable() on u.EmpID equals c.EmpID into temp
                          from item in temp.DefaultIfEmpty(new HR_tblEmp { EmpID = 0 })
                          where  (isAdmin == true || jointable.Contains(u.EmpID))
                          select new AccountViewModel()
                          {
                              EmpId = u.EmpID,
                              FirstName = item.FirstName,
                              LastName = item.LastName,
                              JoinDate = item.JoinDate,
                              UserName = u.UserName,
                              FromDate = u.FromDate,
                              ToDate = u.ToDate,
                              IsLDAP = u.IsLDAP,
                              FAdm = u.FAdm,
                              FLock = u.FLock,
                              UserID = u.UserID,
                              LockDate = u.LockDate,
                              strIsLDAP = u.IsLDAP == 1 ? "X" : "",
                              strFAdm = u.FAdm == 1 ? "X" : "",
                              strFLock = u.FLock == 1 ? "X" : ""
                          };

                List<AccountViewModel> ret = new List<AccountViewModel>();
                foreach (var item in tmp)
                {
                    AccountViewModel p = new AccountViewModel()
                    {
                        EmpId = item.EmpId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.UserName,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        JoinDate = item.JoinDate,
                        IsLDAP = item.IsLDAP,
                        FAdm = item.FAdm,
                        FLock = item.FLock,
                        UserID = item.UserID,
                        LockDate = item.LockDate,
                        strIsLDAP = item.strIsLDAP,
                        strFromDate = item.FromDate == null ? "" : ((DateTime)item.FromDate).ToString("dd/MM/yyyy"),
                        strToDate = item.ToDate == null ? "" : ((DateTime)item.ToDate).ToString("dd/MM/yyyy"),
                        strLockDate = item.LockDate == null ? "" : ((DateTime)item.LockDate).ToString("dd/MM/yyyy"),
                        strFAdm = item.strFAdm,
                        strFLock = item.strFLock
                    };
                    ret.Add(p);
                }
                return ret;

            }
            catch// (Exception ex)
            {
                return new List<AccountViewModel>();
            }
        }

        public List<EmployeeViewModel> FindEmployee(string EmpCode, string FullName, int? LSCompanyID, bool? Active,string userGroupID,int? moduleID, bool isAdmin, int LanguageId = 10001)
        {
            var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
            var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
            var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

            var Employeelst = from empl in context.HR_tblEmp

                              join com in context.LS_tblCompany on empl.LSCompanyID equals com.LSCompanyID

                              join pos in context.LS_tblPosition on empl.LSPositionID equals pos.LSPositionID into postmp
                              from pos in postmp.DefaultIfEmpty()

                              where  (isAdmin == true || jointable.Contains(empl.EmpID)) &&
                              (EmpCode == null || empl.EmpCode.Contains(EmpCode)) &&
                              (FullName == null || (empl.LastName + " " + empl.FirstName).Contains(FullName)) &&// search theo format tiếng anh
                              (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(empl.LSCompanyID)) &&
                              (Active == null || empl.Active == Active)
                              select new Eagle.Model.HR.EmployeeViewModel
                              {
                                  EmpID = empl.EmpID,
                                  EmpCode = empl.EmpCode,
                                  FirstName = empl.FirstName,
                                  LastName = empl.LastName,
                                  CompanyName = (LanguageId == 124) ? com.Name : com.VNName,
                                  Position = pos != null ? ((LanguageId == 124) ? pos.Name : pos.VNName) : null,
                                  FullName = empl.LastName + " " + empl.FirstName,
                                  JoinDate = empl.JoinDate
                              };
            return Employeelst.ToList();
        }


        private List<AutoCompleteViewModel> GetListCompany(string searchTerm)
        {
            //var tmp = context.LS_tblCompany
            //               .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
            //               .Select(c => new
            //               {
            //                   id = c.LSCompanyID,
            //                   name = c.Name,
            //                   text = c.VNName
            //               });
            var tmp = from emp in context.HR_tblEmp
                         join account in context.SYS_tblUserAccount on emp.EmpID equals account.EmpID
                         where (emp.LastName + " " + emp.FirstName).Contains(searchTerm)
                         select new
                         {
                             id = account.UserName,
                             name = account.UserName +" | "+ emp.LastName + " " + emp.FirstName,
                             text = emp.LastName + " " + emp.FirstName
                         };
                        
            List<AutoCompleteViewModel> ret = new List<AutoCompleteViewModel>();
            foreach (var item in tmp.ToList())
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = item.id,
                    name = item.name,
                    text = item.text
                };
                ret.Add(p);
            }
            return ret;
        }

        // dùng cho bind dropdownlist
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            return GetListCompany(searchTerm).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }

    }
}
