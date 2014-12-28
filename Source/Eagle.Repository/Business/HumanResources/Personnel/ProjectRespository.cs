using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model.HR;

namespace Eagle.Repository.HR
{
    public class ProjectRespository
    {
        public EntityDataContext context { get; set; }

        public ProjectRespository(EntityDataContext context)
        {
            this.context = context;
        }

        public string GenerateProjectCode(int num)
        {
            System.Nullable<Int32> Max_ID = (from u in context.HR_tblProject select u.ProjectID).DefaultIfEmpty(0).Max() + 1;
            string _MaxID =Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
            return _MaxID;
        }

        public string GenerateProjectCode(int num, string sid)
        {
            System.Nullable<Int32> Max_ID = (from u in context.HR_tblProject select u.ProjectID).DefaultIfEmpty(0).Max() + 1;
            string number = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
            return string.Format("{0}-{1}", number, sid.Substring(0, 5).ToUpper());
        }


        public bool CheckExistCode(string Code)
        {
            var query = context.HR_tblProject.FirstOrDefault(p => p.ProjectCode.Equals(Code));
            return (query != null);
        }

        public List<ProjectViewModel> GetList(int? EmpID, int? ModuleId, string UserName, bool isAdmin)
        {
            List<ProjectViewModel> lst = new List<ProjectViewModel>();
            if (EmpID != null && EmpID != 0)
            {
                var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
               lst = (from p in context.HR_tblProject                            
                    where p.EmpID == EmpID && (isAdmin == true || jointable.Contains(p.EmpID))     
                        select new ProjectViewModel()
                        {
                            ProjectID = p.ProjectID,
                            EmpID = p.EmpID,
                            ProjectCode = p.ProjectCode,
                            ProjectName = p.ProjectName,
                            FromMonth = p.FromMonth,
                            ToMonth = p.ToMonth,
                            Position = p.Position,
                            MainWork = p.MainWork,
                            Note = p.Note,
                            Creater = p.Creater,
                            CreatedTime = p.CreatedTime
                        }).OrderByDescending(p => p.ProjectID).ToList();    
            }
            return lst;
        }

        public ProjectViewModel Details(int id)
        {
            try
            {
                var query = (from p in context.HR_tblProject                            
                             where p.ProjectID == id
                             select new ProjectViewModel()
                             {
                                 ProjectID = p.ProjectID,
                                 EmpID = p.EmpID,
                                 ProjectCode = p.ProjectCode,
                                 ProjectName = p.ProjectName,
                                 FromMonth = p.FromMonth,
                                 ToMonth = p.ToMonth,
                                 Position = p.Position,
                                 MainWork = p.MainWork,
                                 Note = p.Note,
                                 Creater = p.Creater,
                                 CreatedTime = p.CreatedTime
                             }).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ProjectViewModel();
            }
        }

        public bool IsIDExisted(int ID)
        {
            var query = context.HR_tblProject.FirstOrDefault(p => p.ProjectID.Equals(ID));
            return (query != null);
        }

        public bool IsDataExisted(string ProjectCode, string ProjectName, int EmpID)
        {
            var query = context.HR_tblProject.FirstOrDefault(c => c.ProjectCode.ToUpper().Equals(ProjectCode.ToUpper())
                && c.ProjectName.ToUpper().Equals(ProjectName) && c.EmpID == EmpID
                );
            return (query != null);
        }

        public HR_tblProject Find(int? id)
        {
            return context.HR_tblProject.Find(id);
        }

        public bool Add(ProjectViewModel add_model, int? UserId, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.ProjectCode, add_model.ProjectName, add_model.EmpID);
                if (isDuplicate == false)
                {
                    bool isProjectCodeExist = CheckExistCode(add_model.ProjectCode);
                    if (isProjectCodeExist)
                        add_model.ProjectCode = GenerateProjectCode(10);

                    HR_tblProject model = new HR_tblProject();
                    model.EmpID = add_model.EmpID;
                    model.ProjectCode = add_model.ProjectCode;
                    model.ProjectName = add_model.ProjectName;
                    model.Position = add_model.Position;
                    model.MainWork = add_model.MainWork;         

                    if (add_model.FromMonth != null && add_model.FromMonth.ToString() != string.Empty)
                        model.FromMonth = Convert.ToDateTime(add_model.FromMonth.ToString());

                    if (add_model.ToMonth != null && add_model.ToMonth.ToString() != string.Empty)
                        model.ToMonth = Convert.ToDateTime(add_model.ToMonth.ToString());

                    model.Note = add_model.Note;
                    model.Creater = UserId;
                    model.CreatedTime = DateTime.Now;
                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        add_model.ProjectID = model.ProjectID;
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

        public bool Update(ProjectViewModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {                
                HR_tblProject modelUpdate = Find(model.ProjectID);
                if (modelUpdate.ProjectID > 0)
                {
                    modelUpdate.EmpID = model.EmpID;
                    modelUpdate.ProjectCode = model.ProjectCode;
                    modelUpdate.ProjectName = model.ProjectName;
                    modelUpdate.Position = model.Position;
                    modelUpdate.MainWork = model.MainWork;

                    if (model.FromMonth != null && model.FromMonth.ToString() != string.Empty)
                        modelUpdate.FromMonth = Convert.ToDateTime(model.FromMonth.ToString());

                    if (model.ToMonth != null && model.ToMonth.ToString() != string.Empty)
                        modelUpdate.ToMonth = Convert.ToDateTime(model.ToMonth.ToString());

                    modelUpdate.Note = model.Note;
                    context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
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

        public bool Delete(int? id, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                HR_tblProject model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    result = true;
                    message = Eagle.Resource.LanguageResource.DeleteSuccess;
                }
                else
                {
                    message = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                message = Eagle.Resource.LanguageResource.DeleteFailure;
            }

            return result;
        }
    }
}
