using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.LS;

namespace Eagle.Repository.LS
{
    public class LS_tblTerminationReasonRespository
    {
         public EntityDataContext context { get; set; }
        public LS_tblTerminationReasonRespository(EntityDataContext context)
        {
            this.context = context;
        }

        public static SelectList PopulateDataToDropDownList(bool? isSelectOption, int? selected_value, int? LanguageId)
        {
            List<TerminationReasonEntity> list = new List<TerminationReasonEntity>();

            using (EntityDataContext context = new EntityDataContext())
            {
                if (LanguageId == 124)
                {
                    list = (from t in context.LS_tblTerminationReason
                            where t.Used == true
                            select new TerminationReasonEntity()
                            {
                                Id = t.LSTerminationReasonID,
                                Name = t.Name,
                            }).ToList();
                }
                else
                {
                    list = (from t in context.LS_tblTerminationReason
                            where t.Used == true
                            select new TerminationReasonEntity()
                            {
                                Id = t.LSTerminationReasonID,
                                Name = t.VNName,
                            }).ToList();
                }
            }
            if (list.Count == 0)
                list.Insert(0, new TerminationReasonEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });

            if (isSelectOption != null && isSelectOption == true)
                list.Insert(0, new TerminationReasonEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            SelectList selectList = new SelectList(list, "Id", "Name", selected_value);
            return selectList;
        }
        public static int GenerateRank()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                int Rank_ID = (from u in context.LS_tblTerminationReason select u.Rank).DefaultIfEmpty(0).Max() + 1;
                return Rank_ID;
            }            
        }

        public static string GenerateCode(int num)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.LS_tblTerminationReason select u.LSTerminationReasonID).DefaultIfEmpty(0).Max() + 1;
                return Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
            }
        }

        public static string GenerateCode(int num, string sid)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.LS_tblTerminationReason select u.LSTerminationReasonID).DefaultIfEmpty(0).Max() + 1;
                string number = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return string.Format("{0}-{1}", number, sid.Substring(0, 5).ToUpper());

            }
        }

        public static bool IsIdExisted(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var result = context.LS_tblTerminationReason.FirstOrDefault(p => p.LSTerminationReasonID.Equals(id));
                return (result != null);
            }
        }

        public static bool IsCodeExisted(string Code)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var code = context.LS_tblTerminationReason.FirstOrDefault(p => p.Code.Equals(Code));
                return (code != null);
            }
        }
       /// <summary>
       /// Kiem tra duplicate code  khi edit cho truong hop cho chinh sua Code
       /// </summary>
       /// <param name="strValidate"></param>
       /// <returns></returns>
        public static bool IsCodeExistedWithTypeID(string Code, int LSTerminationReasonID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var code = context.LS_tblTerminationReason
                            .FirstOrDefault(p => p.Code.Equals(Code) && p.LSTerminationReasonID != LSTerminationReasonID);
                return (code != null);
            }
        }
        public IEnumerable<LS_tblTerminationReasonViewModel> List()
        {
            List<LS_tblTerminationReasonViewModel> lst = new List<LS_tblTerminationReasonViewModel>();           
            lst = (from p in context.LS_tblTerminationReason
                        select new LS_tblTerminationReasonViewModel()
                        {
                            LSTerminationReasonID = p.LSTerminationReasonID,
                            Code = p.Code,
                            Name = p.Name,
                            VNName = p.VNName,
                            Rank = p.Rank,
                            Note = p.Note,
                            Used = p.Used
                        }).OrderBy(p=>p.Rank).ToList();           
            return lst;
        }
        public static LS_tblTerminationReason Find(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.LS_tblTerminationReason.Find(id);
            }
        }

        public LS_tblTerminationReasonViewModel Details(int id)
        {
            try
            {
                LS_tblTerminationReasonViewModel ret = context.LS_tblTerminationReason
                                        .Where(p => p.LSTerminationReasonID == id)
                                        .Select(p => new LS_tblTerminationReasonViewModel()
                                        {
                                            LSTerminationReasonID = p.LSTerminationReasonID,
                                            Code = p.Code,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Note = p.Note,
                                            Used = p.Used
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new LS_tblTerminationReasonViewModel();
            }

        }
        public static bool Add(LS_tblTerminationReasonViewModel model, out string messsage)
        {
            bool result = false;
            messsage = string.Empty;
            try
            {
                if (IsCodeExisted(model.Code))
                    messsage = Eagle.Resource.LanguageResource.ExistCode;

                using (EntityDataContext context = new EntityDataContext())
                {
                    LS_tblTerminationReason entity = new LS_tblTerminationReason();
                    entity.Code = model.Code;
                    entity.Name = model.Name;
                    entity.VNName = model.VNName;
                    entity.Rank = LS_tblTerminationReasonRespository.GenerateRank();
                    entity.Note = model.Note;
                    entity.Used = true;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    int i = context.SaveChanges();
                    if (i > 0)
                    {
                        result = true;
                        messsage = Eagle.Resource.LanguageResource.CreateSuccess;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                result = false;
                foreach (var eve in e.EntityValidationErrors)
                {
                    messsage += "Entity of type \"{0}\" in state \"{1}\" has the following validation errors: " +  eve.Entry.Entity.GetType().Name +" "+ eve.Entry.State;
                    foreach (var ve in eve.ValidationErrors)
                    {
                        messsage += "- Property: \"{0}\", Error: \"{1}\"" + ve.PropertyName + " " + ve.ErrorMessage;
                    }
                }                
            }
            return result;
        }
                
        public static bool Update(LS_tblTerminationReasonViewModel model, out string messsage)
        {
            bool result = false;
            messsage = string.Empty;
            try
            {
                if (IsCodeExistedWithTypeID(model.Code, model.LSTerminationReasonID))               
                    messsage = Eagle.Resource.LanguageResource.ExistCode;

                using (EntityDataContext context = new EntityDataContext())
                {
                    LS_tblTerminationReason entity = Find(model.LSTerminationReasonID);
                    entity.Code = model.Code;
                    entity.Name = model.Name;
                    entity.VNName = model.VNName;
                    entity.Note = model.Note;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        messsage = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                }  
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                result = false;
            }
            return result;
        }

        public static bool UpdateListOrder(int id, int listOrder, out string messsage)
        {
            bool result = false;
            messsage = string.Empty;
            try
            {          
                using (EntityDataContext context = new EntityDataContext())
                {
                    LS_tblTerminationReason entity = Find(id);
                    entity.Rank = listOrder;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        messsage = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                result = false;
            }
            return result;
        }

        public static bool UpdateStatus(int id, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    LS_tblTerminationReason entity = Find(id);
                    bool flag = (entity.Used == true) ? false : true;
                    entity.Used = flag;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        message = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                    else
                    {
                        result = false;
                        message = Eagle.Resource.LanguageResource.UpdateFailed;
                    }                        
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                result = false;
            }
            return result;
        }


        public static bool Delete(int id, out string message)
        {
            bool result = false;
            try
            {                
                using (EntityDataContext context = new EntityDataContext())
                {
                    LS_tblTerminationReason entity = Find(id);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        message = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                    else
                    {
                        result = false;
                        message = Eagle.Resource.LanguageResource.UpdateFailed;
                    }             
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                result = false;
            }
            return result;
        }

        


        #region Bind DropdownList =====================================================================================     
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblTerminationReason
                            .Where(c => c.Name.Contains(searchTerm))
                            .Select(c => new AutoCompleteModel
                            {
                                id = c.LSTerminationReasonID,
                                name = (LanguageId == 124) ? c.Name : c.VNName,
                                text = ""
                            }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion====================================================================================================
    }
}
