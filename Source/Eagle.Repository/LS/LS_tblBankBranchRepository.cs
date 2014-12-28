using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;
using Eagle.Model.LS;

namespace Eagle.Repository
{
    public class LS_tblBankBranchRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblBankBranchRepository(EntityDataContext context)
        {
            this.context = context;
        }
        
        public List<LS_tblBankBranchEditModel> GetList(int? BankID, int LanguageId)
        {
            IQueryable<LS_tblBankBranchEditModel> query =null;
            if(LanguageId == 124){
                query = (from p in context.LS_tblBankBranch
                                   join b in context.LS_tblBank on p.LSBankID equals b.LSBankID
                                   select new LS_tblBankBranchEditModel()
                                   {
                                       BankID = p.LSBankID,
                                       BankName = b.Name,
                                       BankBranchID = p.LSBankBranchID,
                                       LSBankBranchCode = p.LSBankBranchCode,
                                       Name = p.Name,
                                       VNName = p.VNName,
                                       BankBranchName = p.Name,
                                       Rank = p.Rank,
                                       Used = p.Used,
                                       Note = p.Note
                                   }).OrderBy(p => p.Rank);
                if (BankID != null && BankID > 0)
                    query = query.Where(p => p.BankID == BankID);             
            }else
            {
                query = (from p in context.LS_tblBankBranch
                             join b in context.LS_tblBank on p.LSBankID equals b.LSBankID
                             select new LS_tblBankBranchEditModel()
                             {
                                 BankID = p.LSBankID,
                                 BankName = b.VNName,
                                 BankBranchID = p.LSBankBranchID,
                                 LSBankBranchCode = p.LSBankBranchCode,
                                 Name = p.Name,
                                 VNName = p.VNName,
                                 BankBranchName = p.VNName,
                                 Rank = p.Rank,
                                 Used = p.Used,
                                 Note = p.Note
                             }).OrderBy(p => p.Rank);
                if (BankID != null && BankID > 0)
                    query = query.Where(p => p.BankID == BankID);  
            }
            List<LS_tblBankBranchEditModel> lst = new List<LS_tblBankBranchEditModel>();
            lst = query.OrderBy(p => p.Rank).ToList();
            return lst;
        }

        public LS_tblBankBranchEditModel Details(int? LSBankBranchID, int LanguageId)
        {
            LS_tblBankBranchEditModel entity = new LS_tblBankBranchEditModel();
            if (LSBankBranchID != null && LSBankBranchID > 0)
            {
                if (LanguageId == 124)
                {
                    entity = (from p in context.LS_tblBankBranch
                              join c in context.LS_tblBank on p.LSBankID equals c.LSBankID
                              where p.LSBankBranchID == LSBankBranchID
                              select new LS_tblBankBranchEditModel()
                              {
                                  BankID = p.LSBankID,
                                  BankName = c.Name,
                                  BankBranchID = p.LSBankBranchID,
                                  LSBankBranchCode = p.LSBankBranchCode,
                                  Name = p.Name,
                                  VNName = p.VNName,
                                  BankBranchName = p.Name,
                                  Rank = p.Rank,
                                  Used = p.Used,
                                  Note = p.Note
                              }).FirstOrDefault();
                }
                else
                {
                    entity = (from p in context.LS_tblBankBranch
                              join c in context.LS_tblBank on p.LSBankID equals c.LSBankID
                              where p.LSBankBranchID == LSBankBranchID
                              select new LS_tblBankBranchEditModel()
                              {
                                  BankID = p.LSBankID,
                                  BankName = c.VNName,
                                  BankBranchID = p.LSBankBranchID,
                                  LSBankBranchCode = p.LSBankBranchCode,
                                  Name = p.Name,
                                  VNName = p.VNName,
                                  BankBranchName = p.VNName,
                                  Rank = p.Rank,
                                  Used = p.Used,
                                  Note = p.Note
                              }).FirstOrDefault();
                }
            }
            return entity;
        }

        public static string GenerateCode(int num)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.LS_tblBankBranch select u.LSBankBranchID).DefaultIfEmpty(0).Max() + 1;
                return Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
            }
        }

        public static string GenerateCode(int num, string sid)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.LS_tblBankBranch select u.LSBankBranchID).DefaultIfEmpty(0).Max() + 1;
                string number = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return string.Format("{0}-{1}", number, sid.Substring(0, 5).ToUpper());
            }
        }

        public short GenerateRank()
        {
            int? Rank = (from u in context.LS_tblBank select u.Rank).DefaultIfEmpty((short)0).Max() + 1;
            return (short)Rank;
        }

        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblBankBranch.FirstOrDefault(p => p.LSBankBranchCode.Equals(strCode));
            return (code != null);
        }

        public bool IsNameExisted(string Name)
        {
            var result = context.LS_tblBankBranch.FirstOrDefault(p => p.Name.Equals(Name));
            return (result != null);
        }

        public bool IsVNNameExisted(string VNName)
        {
            var result = context.LS_tblBankBranch.FirstOrDefault(p => p.VNName.Equals(VNName));
            return (result != null);
        }

        public bool Add(LS_tblBankBranch model)
        {
            if (CheckExistCode(model.LSBankBranchCode))
            {
                string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
            catch (Exception ex)
            { string strerr = ex.Message; }
            return true;
        }

        public static LS_tblBankBranch Find(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.LS_tblBankBranch.Find(id);
            }
        }

        public bool Insert(LS_tblBankBranchEditModel model, out string message)
        {
            message = string.Empty;
            bool result = false;
            bool isExisted = CheckExistCode(model.LSBankBranchCode);
            if (!isExisted)
            {
                try
                {
                    model.Rank = GenerateRank();
                    LS_tblBankBranch entity = new LS_tblBankBranch()
                    {
                        LSBankBranchCode = model.LSBankBranchCode,
                        LSBankID = model.BankID,
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
                        message = entity.LSBankBranchID.ToString();
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

        public bool Update(LS_tblBankBranchEditModel model, out string message)
        {
            message = string.Empty;
            bool result = false;
            try
            {
                LS_tblBankBranch entity = Find(model.BankBranchID);
                entity.LSBankID = model.BankID;
                entity.Name = model.Name;
                entity.VNName = model.VNName;
                // entity.Rank = model.Rank;
                entity.Used = model.Used;
                entity.Note = model.Note;
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                int i = context.SaveChanges();
                if (i == 1)
                {
                    result = true;
                    message = entity.LSBankBranchID.ToString();
                }
            }
            catch
            {
                message = Eagle.Resource.LanguageResource.ExistCode;
            }
            return result;
        }

        public bool Update(LS_tblBankBranch modelUpdate)
        {
            try
            {
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static bool UpdateListOrder(int id, short? listOrder, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    LS_tblBankBranch entity = Find(id);
                    entity.Rank = listOrder;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        message = entity.LSBankBranchID.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = false;
            }
            return result;
        }

        public LS_tblBankBranchViewModel FindEditModel(int id)
        {
            LS_tblBankBranchViewModel ret = context.LS_tblBankBranch
                                        .Where(p => p.LSBankBranchID == id)
                                        .Select(p => new LS_tblBankBranchViewModel()
                                        {
                                            LSBankBranchID = p.LSBankBranchID,
                                            LSBankBranchCode = p.LSBankBranchCode,
                                            LSBankID = p.LSBankID,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }
        
        #region Bind DropdownList Bank    
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int? LSBankID, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> autoCompleteList = new List<AutoCompleteModel>();
            var lst = context.LS_tblBankBranch.Where(c => c.LSBankID == LSBankID).OrderBy(p => p.Rank);
            autoCompleteList = lst.Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                            .Select(c => new AutoCompleteModel
                            {
                                id = c.LSBankBranchID,
                                name = (LanguageId == 124) ? c.Name : c.VNName,
                                text = c.Note
                            }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return autoCompleteList;
        }
        #endregion
    }
}
