using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.HR;

namespace Eagle.Repository.HR
{
    public class RelationshipRespository
    {
         public EntityDataContext context { get; set; }

         public RelationshipRespository(EntityDataContext context)
        {
            this.context = context;
        }

         public List<RelationshipViewModel> GetList(int? EmpID, int? ModuleId, string UserName, bool isAdmin, int? LanguageId)
        {
             List<RelationshipViewModel> lst = new List<RelationshipViewModel>();
             if (EmpID != null && EmpID != 0)
             {
                 var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
                 lst = (from c in context.HR_tblRelationship
                        join lr in context.LS_tblRelationship on c.LSRelationshipID equals lr.LSRelationshipID into lsrelation
                        from lr in lsrelation.DefaultIfEmpty()
                        join e in context.HR_tblEmp on c.EmpID equals e.EmpID into employee
                        from e in employee.DefaultIfEmpty()
                        where c.EmpID == EmpID && (isAdmin == true || jointable.Contains(c.EmpID))    
                        select new RelationshipViewModel()
                        {
                            RelationshipID = c.RelationshipID,
                            LSRelationshipID = c.LSRelationshipID,
                            LSRelationshipName = (LanguageId==10001)?lr.Name:lr.VNName,
                            EmpID = c.EmpID,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            EmpName = e.FirstName + " " + e.LastName,
                            Gender = c.Gender,
                            DOB = c.DOB,
                            YOB = c.YOB,
                            IDNo = c.IDNo,
                            Address = c.Address,
                            Telephone = c.Telephone,
                            IsDependant = c.IsDependant,
                            FromDatePIT = c.FromDatePIT,
                            ToDatePIT = c.ToDatePIT,
                            WokPlace = c.WokPlace,
                            StillAlive = c.StillAlive,
                            Before75 = c.Before75,
                            After75 = c.After75,
                            TaxNo = c.TaxNo,
                            Note = c.Note,
                            Creater = c.Creater,
                            CreatedDate = c.CreatedDate
                        }).OrderByDescending(c => c.RelationshipID).ToList();
             }           
            return lst;
        }

        public RelationshipViewModel Details(int id)
        {
            try
            {
                var query = (from c in context.HR_tblRelationship
                             join lr in context.LS_tblRelationship on c.LSRelationshipID equals lr.LSRelationshipID into lsrelation
                             from lr in lsrelation.DefaultIfEmpty()
                             join e in context.HR_tblEmp on c.EmpID equals e.EmpID into employee
                             from e in employee.DefaultIfEmpty()
                             where c.RelationshipID == id
                             select new RelationshipViewModel()
                             {
                                 RelationshipID = c.RelationshipID,
                                 LSRelationshipID = c.LSRelationshipID,
                                 LSRelationshipName = lr.Name,
                                 EmpID = c.EmpID,
                                 FirstName = c.FirstName,
                                 LastName = c.LastName,
                                 EmpName = e.FirstName + " " + e.LastName,
                                 Gender = c.Gender,
                                 DOB = c.DOB,
                                 YOB = c.YOB,                                 
                                 IDNo = c.IDNo,
                                 Address = c.Address,
                                 Telephone = c.Telephone,
                                 IsDependant = c.IsDependant,
                                 FromDatePIT = c.FromDatePIT,
                                 ToDatePIT = c.ToDatePIT,
                                 WokPlace = c.WokPlace,
                                 StillAlive = c.StillAlive,
                                 Before75 = c.Before75,
                                 After75 = c.After75,
                                 TaxNo = c.TaxNo,
                                 Note = c.Note,
                                 Creater = c.Creater,
                                 CreatedDate = c.CreatedDate
                             }).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new RelationshipViewModel();
            }
        }

        public bool IsDataExisted(string FirstName, string LastName, int LSRelationshipID, int EmpID)
        {
            var query = context.HR_tblRelationship.FirstOrDefault(c => c.FirstName.ToUpper().Equals(FirstName.ToUpper())
                && c.LastName.ToUpper().Equals(LastName.ToUpper())
                && c.LSRelationshipID == LSRelationshipID && c.EmpID == EmpID
                );
            return (query != null);
        }

        public bool IsIDExisted(int ID)
        {
            var query = context.HR_tblRelationship.FirstOrDefault(c => c.RelationshipID.Equals(ID));
            return (query != null);
        }

        public bool IsEmpIDExisted(int EmpID)
        {
            var query = context.HR_tblRelationship.FirstOrDefault(c => c.EmpID.Equals(EmpID));
            return (query != null);
        }

        public HR_tblRelationship Find(int? ID)
        {
            return context.HR_tblRelationship.Find(ID);
        }

        public bool Add(RelationshipViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.FirstName,add_model.LastName,add_model.LSRelationshipID, add_model.EmpID);
                if (isDuplicate == false)
                {
                    HR_tblRelationship model = new HR_tblRelationship();
                    model.EmpID = add_model.EmpID;
                    model.LSRelationshipID = add_model.LSRelationshipID;
                    model.LastName = add_model.LastName;
                    model.FirstName = add_model.FirstName;
                    model.Gender = add_model.Gender;
                    model.DOB = add_model.DOB;
                    model.YOB = add_model.YOB;
                    model.IDNo = add_model.IDNo;
                    model.Address = add_model.Address;
                    model.Telephone = add_model.Telephone;
                    model.IsDependant = add_model.IsDependant;
                    model.FromDatePIT = add_model.FromDatePIT;
                    model.ToDatePIT = add_model.ToDatePIT;
                    model.WokPlace = add_model.WokPlace;
                    model.StillAlive = add_model.StillAlive;
                    model.Before75 = add_model.Before75;
                    model.After75 = add_model.After75;
                    model.TaxNo = add_model.TaxNo;
                    model.Note = add_model.Note;
                    model.Creater = add_model.Creater;
                    model.CreatedDate = DateTime.Now;
                   
                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        add_model.RelationshipID = model.RelationshipID;
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
        
        public bool Update(RelationshipViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {

                bool EmpIDExist = IsEmpIDExisted(edit_model.EmpID);
                if(EmpIDExist == true)
                {
                    HR_tblRelationship model = Find(edit_model.RelationshipID);
                    model.EmpID = edit_model.EmpID;
                    model.LSRelationshipID = edit_model.LSRelationshipID;
                    model.LastName = edit_model.LastName;
                    model.FirstName = edit_model.FirstName;
                    model.Gender = edit_model.Gender;
                    model.DOB = edit_model.DOB;
                    model.YOB = edit_model.YOB;
                    model.IDNo = edit_model.IDNo;
                    model.Address = edit_model.Address;
                    model.Telephone = edit_model.Telephone;
                    model.IsDependant = edit_model.IsDependant;
                    model.FromDatePIT = edit_model.FromDatePIT;
                    model.ToDatePIT = edit_model.ToDatePIT;
                    model.WokPlace = edit_model.WokPlace;
                    model.StillAlive = edit_model.StillAlive;
                    model.Before75 = edit_model.Before75;
                    model.After75 = edit_model.After75;
                    model.TaxNo = edit_model.TaxNo;
                    model.Note = edit_model.Note;

                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }else
                {
                    edit_model.RelationshipID = 0;
                    result = Add(edit_model, out Message);
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

        public bool Delete(int? id, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            try
            {
                HR_tblRelationship model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    Message = Eagle.Resource.LanguageResource.DeleteSuccess;
                    result = true;
                }else
                {
                     Message = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Message = Eagle.Resource.LanguageResource.DeleteFailure;
                result = false;
            }

            return result;
        }
    }
}
