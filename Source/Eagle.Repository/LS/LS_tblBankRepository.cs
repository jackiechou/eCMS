using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
using Eagle.Model.LS;
namespace Eagle.Repository
{
    public class LS_tblBankRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblBankRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public static string GenerateCode(int num)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.LS_tblBank select u.LSBankID).DefaultIfEmpty(0).Max() + 1;
                return Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
            }
        }

        public static string GenerateCode(int num, string sid)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.LS_tblBank select u.LSBankID).DefaultIfEmpty(0).Max() + 1;
                string number = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return string.Format("{0}-{1}", number, sid.Substring(0, 5).ToUpper());
            }
        }

        public short GenerateRank()
        {
            int? Rank = (from u in context.LS_tblBank select u.Rank).DefaultIfEmpty((short)0).Max() + 1;
            return (short)Rank;
        }

        public List<LS_tblBankViewModel> GetList(int? LanguageId)
        {
            List<LS_tblBankViewModel> lst = new List<LS_tblBankViewModel>();
            if (LanguageId == 124)
            {
                lst = (from p in context.LS_tblBank
                       join c in context.LS_tblProvince on p.LSProvinceID equals c.LSProvinceID
                       select new LS_tblBankViewModel()
                       {
                           LSBankID = p.LSBankID,
                           LSBankCode = p.LSBankCode,
                           Name = p.Name,
                           VNName = p.VNName,
                           BankName = p.Name,
                           Rank = p.Rank,
                           Used = p.Used,
                           Note = p.Note,
                           ProvinceName = c.Name
                       }).OrderBy(p => p.Rank).ToList();
            }
            else
            {
                lst = (from p in context.LS_tblBank
                       join c in context.LS_tblProvince on p.LSProvinceID equals c.LSProvinceID
                       select new LS_tblBankViewModel()
                       {
                           LSBankID = p.LSBankID,
                           LSBankCode = p.LSBankCode,
                           Name = p.Name,
                           VNName = p.VNName,
                           BankName = p.VNName,
                           Rank = p.Rank,
                           Used = p.Used,
                           Note = p.Note,
                           ProvinceName = c.VNName
                       }).OrderBy(p => p.Rank).ToList();
            }
            return lst;
        }

        public IEnumerable<LS_tblBankViewModel> List()
        {
            try
            {
                return (from p in context.LS_tblBank
                       join c in context.LS_tblProvince on p.LSProvinceID equals c.LSProvinceID
                          select new LS_tblBankViewModel()
                                {
                                    LSBankID = p.LSBankID,
                                    LSBankCode = p.LSBankCode,
                                    Name = p.Name,
                                    VNName = p.VNName,
                                    Rank = p.Rank,
                                    Used = p.Used,
                                    Note = p.Note,
                                    strProvinceName = c.Name
                                }).OrderBy(p => p.Rank);
            }
            catch
            {
                return new List<LS_tblBankViewModel>();
            }
        }

        public List<BankEntity> GetListForDropDownList(int? LanguageId)
        {
            try
            {
                return (context.LS_tblBank.Select(p => new BankEntity()
                {
                    Id = p.LSBankID,
                    Name = (LanguageId ==10001)?p.Name:p.VNName
                }).OrderBy(p => p.Name)).ToList();
            }
            catch
            {
                return new List<BankEntity>();
            }
        }

        public List<LS_tblBankViewModel> GetList()
        {
            try
            {
                return (from p in context.LS_tblBank
                       join c in context.LS_tblProvince on p.LSProvinceID equals c.LSProvinceID
                       select new LS_tblBankViewModel()
                       {
                           LSBankID = p.LSBankID,
                           LSBankCode = p.LSBankCode,
                           Name = p.Name,
                           VNName = p.VNName,
                           Rank = p.Rank,
                           Used = p.Used,
                           Note = p.Note,
                           strProvinceName = c.Name
                       }).OrderBy(p => p.Rank).ToList();
            }
            catch
            {
                return new List<LS_tblBankViewModel>();
            }
        }

        public LS_tblBankViewModel GetDetails(int? LSBankID)
        {
            LS_tblBankViewModel entity = new LS_tblBankViewModel();
            if (LSBankID != null && LSBankID > 0)
            {
                entity = (from p in context.LS_tblBank
                                join c in context.LS_tblProvince on p.LSProvinceID equals c.LSProvinceID
                                where p.LSBankID == LSBankID
                                select new LS_tblBankViewModel
                                {
                                    LSBankID = p.LSBankID,
                                    LSBankCode = p.LSBankCode,
                                    LSProvinceID = p.LSProvinceID,
                                    strProvinceName = c.Name,
                                    Name = p.Name,
                                    VNName = p.VNName,
                                    Rank = p.Rank,
                                    Used = p.Used,
                                    Note = p.Note
                                }).FirstOrDefault();
            }
            return entity;
        }

        public LS_tblBankCreateModel Details(int? LSBankID, int LanguageId)
        {
            LS_tblBankCreateModel entity = new LS_tblBankCreateModel();
            if (LSBankID != null && LSBankID > 0)
            {
                if (LanguageId == 124)
                {
                    entity = (from p in context.LS_tblBank
                              join c in context.LS_tblProvince on p.LSProvinceID equals c.LSProvinceID
                              where p.LSBankID == LSBankID
                              select new LS_tblBankCreateModel()
                              {
                                  LSBankID = p.LSBankID,
                                  LSBankCode = p.LSBankCode,
                                  ProvinceID = p.LSProvinceID,
                                  ProvinceName = c.Name,
                                  Name = p.Name,
                                  VNName = p.VNName,
                                  Rank = p.Rank,
                                  Used = p.Used,
                                  Note = p.Note
                              }).FirstOrDefault();
                }
                else
                {
                    entity = (from p in context.LS_tblBank
                              join c in context.LS_tblProvince on p.LSProvinceID equals c.LSProvinceID
                              select new LS_tblBankCreateModel()
                              {
                                  LSBankID = p.LSBankID,
                                  LSBankCode = p.LSBankCode,
                                  ProvinceID = p.LSProvinceID,
                                  ProvinceName = c.VNName,
                                  Name = p.Name,
                                  VNName = p.VNName,
                                  Rank = p.Rank,
                                  Used = p.Used,
                                  Note = p.Note
                              }).FirstOrDefault();   
                }
            }
            return entity;
        } 

        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblBank.FirstOrDefault(p => p.LSBankCode.Equals(strCode));
            return (code != null);
        }

        public bool IsNameExisted(string Name)
        {
            var result = context.LS_tblBank.FirstOrDefault(p => p.Name.Equals(Name));
            return (result != null);
        }

        public bool IsVNNameExisted(string VNName)
        {
            var result = context.LS_tblBank.FirstOrDefault(p => p.VNName.Equals(VNName));
            return (result != null);
        }

       
        public static LS_tblBank Find(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.LS_tblBank.Find(id);
            }
        }

        public bool Insert(LS_tblBankCreateModel model, out string message)
        {
            message = string.Empty;
            bool result = false;
            bool isExisted = CheckExistCode(model.LSBankCode);
            if (!isExisted)
            {
                try
                {
                    model.Rank = GenerateRank();
                    LS_tblBank entity = new LS_tblBank()
                    {
                        LSBankCode = model.LSBankCode,
                        LSProvinceID = model.ProvinceID,
                        Name = model.Name,
                        VNName = model.VNName,
                        Rank = model.Rank,
                        Used = model.Used,
                        Note = model.Note
                    };

                    context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    int i = context.SaveChanges();
                    if (i > 0)
                    {
                        result = true;
                        message = entity.LSBankID.ToString();
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
            else
            {
                message = Eagle.Resource.LanguageResource.ExistCode;
            }
            return result;
        }
        
        public bool Update(LS_tblBankCreateModel model, out string message)
        {
             message = string.Empty;
            bool result = false;  
            try
            {
                LS_tblBank entity = Find(model.LSBankID);
                if (entity != null)
                {
                    entity.LSProvinceID = model.ProvinceID;
                    entity.Name = model.Name;
                    entity.VNName = model.VNName;
                   // entity.Rank = model.Rank;
                    entity.Used = model.Used;
                    entity.Note = model.Note;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i > 0)
                    {
                        result = true;
                        message = entity.LSBankID.ToString();
                    }
                }
                else
                    message = Eagle.Resource.LanguageResource.NullReferenceException;
            }
            catch{ message = "Error";}
            return result;
        }

        public static bool UpdateListOrder(int id, short? listOrder, out string messsage)
        {
            bool result = false;
            messsage = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    LS_tblBank entity = Find(id);
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

        public bool Add(LS_tblBank model, out string message)
        {
            message = string.Empty;
            bool result = false;
            bool isExisted = CheckExistCode(model.LSBankCode);
            if (!isExisted)
            {
                try
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    int i = context.SaveChanges();
                    if (i > 0)
                        result = true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
            else
            {
                message = Eagle.Resource.LanguageResource.ExistCode;
            }
            return result;
        }

        public bool Edit(LS_tblBankViewModel model, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {                
                LS_tblBank entity = Find(model.LSBankID);
                if (entity != null)
                {
                    entity.LSProvinceID = model.LSProvinceID;
                    entity.Name = model.Name;
                    entity.VNName = model.VNName;
                    entity.Rank = model.Rank;
                    entity.Used = model.Used;
                    entity.Note = model.Note;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        message = string.Empty;
                    }
                }
                else
                {
                    result = false;
                    message = Eagle.Resource.LanguageResource.NullReferenceException;
                }
            }
            catch (Exception ex) { message = ex.ToString(); result = false; }
            return result;
        }
                
        public LS_tblBankViewModel FindEditModel(int id)
        {
            LS_tblBankViewModel ret = context.LS_tblBank
                                        .Where(p => p.LSBankID == id)                                        
                                        .Select(p => new LS_tblBankViewModel() {
                                            LSBankID = p.LSBankID,
                                            LSBankCode = p.LSBankCode,
                                            LSProvinceID = p.LSProvinceID,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note,
                                            strProvinceName =  p.LS_tblProvince.Name
                                        }).FirstOrDefault();
            return ret;
        }

        #region Bind DropdownList Bank 
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblBank
                        .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                        .OrderBy(c => c.Rank)
                        .Select(c => new AutoCompleteModel
                        {
                            id = c.LSBankID,
                            name = (LanguageId == 124) ? c.Name : c.VNName,
                            text = c.Note
                        }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion
    }
}
