using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Model;
using Eagle.Core;
using Eagle.Model.HR;
using System.Data.Entity.Validation;
using System.Transactions;
namespace Eagle.Repository
{
    public class CyclicRepository
    {
         public EntityDataContext context { get; set; }
         public CyclicRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(int from, int to)
        {
            var code = context.Timesheet_tblCyclic.FirstOrDefault(p => p.FromCyclic == from && p.ToCyclic == to);
            return (code != null);
        }
       /// <summary>
       /// Kiem tra duplicate code  khi edit cho truong hop cho chinh sua Code
       /// </summary>
       /// <param name="strValidate"></param>
       /// <returns></returns>
        public bool CheckExistEdit(int from,int to, int CyclicID)
        {
            var code = context.Timesheet_tblCyclic
                        .FirstOrDefault(p => p.FromCyclic.Equals(from) && p.ToCyclic.Equals(to) && p.CyclicID != CyclicID);
            return (code != null);
        }
        public IEnumerable<CyclicCreateViewModel> List()
        {

            try
            {
                var tmp = from p in context.Timesheet_tblCyclic
                          select new CyclicCreateViewModel()
                          {
                              CyclicID = p.CyclicID,
                              FromCyclic = p.FromCyclic,
                              ToCyclic = p.ToCyclic,
                              Note = p.Note
                          };

                List<CyclicCreateViewModel> ret = new List<CyclicCreateViewModel>();
                foreach (var item in tmp)
                {
                    CyclicCreateViewModel s = new CyclicCreateViewModel()
                    {
                        CyclicID = item.CyclicID,
                        FromCyclic = item.FromCyclic,
                        ToCyclic = item.ToCyclic,
                        Note = item.Note
                    };
                    ret.Add(s);
                }
                return ret;
            }
            catch
            {
                return new List<CyclicCreateViewModel>();
            }
        }
        public Timesheet_tblCyclic Find(int id)
        {
            return context.Timesheet_tblCyclic.Find(id);
        }
        public string  Add(Timesheet_tblCyclic model)
        {
            try
            {
                if (CheckExist(model.FromCyclic, model.ToCyclic))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistData;
                    return errorMessage;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return "success";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public string Update(CyclicCreateViewModel model)
        {

            try
            {
                if (CheckExistEdit(model.FromCyclic, model.ToCyclic,model.CyclicID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                Timesheet_tblCyclic modelUpdate = Find(model.CyclicID);
                modelUpdate.FromCyclic = model.FromCyclic;
                modelUpdate.ToCyclic = model.ToCyclic;
                modelUpdate.Note = model.Note;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return strErr;
            }
        }
        public CyclicCreateViewModel FindEdit(int id)
        {
            try
            {
                CyclicCreateViewModel ret = context.Timesheet_tblCyclic
                                        .Where(p => p.CyclicID == id)
                                        .Select(p => new CyclicCreateViewModel()
                                        {
                                            CyclicID = p.CyclicID,
                                            FromCyclic = p.FromCyclic,
                                            ToCyclic = p.ToCyclic,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new CyclicCreateViewModel();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                Timesheet_tblCyclic modelUpdate = Find(id);
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
        // load len danh sach nhan vien chua co trong chu ky cham cong nao
        public List<EmployeeViewModel> ListBox( bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var tmp = from u in context.HR_tblEmp
                          orderby u.FirstName
                          where !(from u1 in context.Timesheet_tblCyclicEmp select u1.EmpID).Contains(u.EmpID)
                          && jointable.Contains(u.EmpID) // join voi bang phan quyen du lieu  
                          select new EmployeeViewModel()
                          {
                              EmpID = u.EmpID,
                              FullName = u.EmpCode + "--" + u.LastName + " " + u.FirstName
                          };

                List<EmployeeViewModel> ret = new List<EmployeeViewModel>();
                foreach (var item in tmp)
                {
                    EmployeeViewModel p = new EmployeeViewModel()
                    {
                        EmpID = item.EmpID,
                        FullName = item.FullName
                    };
                    ret.Add(p);
                }
                return ret;
            }
            catch
            {
                return new List<EmployeeViewModel>();
            }
        }
        // load len du lieu nhan vien co trong chu ky cham cong duoc chon
        public List<EmployeeViewModel> ListBoxInCyclic(int CyclicID, bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var tmp = from u in context.HR_tblEmp
                          orderby u.FirstName
                          where (from u1 in context.Timesheet_tblCyclicEmp
                                  where u1.CyclicID == CyclicID
                                  select u1.EmpID).Contains(u.EmpID)
                          && jointable.Contains(u.EmpID) // join voi bang phan quyen du lieu  
                          select new EmployeeViewModel()
                          {
                              EmpID = u.EmpID,
                              FullName = u.EmpCode + "--" + u.LastName + " " + u.FirstName
                          };

                List<EmployeeViewModel> ret = new List<EmployeeViewModel>();
                foreach (var item in tmp)
                {
                    EmployeeViewModel p = new EmployeeViewModel()
                    {
                        EmpID = item.EmpID,
                        FullName = item.FullName
                    };
                    ret.Add(p);
                }
                return ret;
            }
            catch
            {
                return new List<EmployeeViewModel>();
            }
        }
        public List<Timesheet_tblCyclicEmp> FindToDeleteDetail(int id, List<int> lstBoxAssigned)
        {
            return context.Timesheet_tblCyclicEmp
                            .Where(c => c.CyclicID == id && lstBoxAssigned.Contains(c.EmpID))
                            .ToList();
        }
        public bool AddDetail(int CyclicID, IEnumerable<string> lstBox)
        {
            using (EntityDataContext contextTrans = new EntityDataContext())
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    try
                    {
                        // thuc hien them lai du lieu
                        foreach (var item in lstBox)
                        {
                            int itemID = Int32.Parse(item);
                            var temp = context.Timesheet_tblCyclicEmp.Where(p => p.CyclicID == CyclicID && p.EmpID == itemID).FirstOrDefault();
                            if (temp == null )
                            {
                                Timesheet_tblCyclicEmp addModel = new Timesheet_tblCyclicEmp()
                                {
                                    EmpID = itemID,
                                    CyclicID = CyclicID,
                                };
                                context.Entry(addModel).State = System.Data.Entity.EntityState.Added;
                            }
                        }
                        context.SaveChanges();
                        // ket thuc transaction thanh cong
                        tranScope.Complete();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }
        public bool DeleteDetail(int CyclicID, List<int> lstBox)
        {
            try
            {
                List<Timesheet_tblCyclicEmp> modelUpdate = FindToDeleteDetail(CyclicID, lstBox);
                foreach (var item in modelUpdate)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
        public List<EmployeeViewModel> SearchNotIn(string SearchTerm,  bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var tmp = from u in context.HR_tblEmp
                          where u.EmpCode.Contains(SearchTerm) || (u.LastName + " " + u.FirstName).Contains(SearchTerm)
                          orderby u.FirstName
                          where !(from u1 in context.Timesheet_tblCyclicEmp select u1.EmpID).Contains(u.EmpID)
                          && jointable.Contains(u.EmpID) // join voi bang phan quyen du lieu  
                          select new EmployeeViewModel()
                          {
                              EmpID = u.EmpID,
                              FullName = u.EmpCode + "--" + u.LastName + " " + u.FirstName
                          };

                List<EmployeeViewModel> ret = new List<EmployeeViewModel>();
                foreach (var item in tmp)
                {
                    EmployeeViewModel p = new EmployeeViewModel()
                    {
                        EmpID = item.EmpID,
                        FullName = item.FullName
                    };
                    ret.Add(p);
                }
                return ret;
            }
            catch
            {
                return new List<EmployeeViewModel>();
            }
        }
        public List<EmployeeViewModel> SearchInCyclic(string SearchTerm,int CyclicID ,bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var tmp = from u in context.HR_tblEmp
                          where u.EmpCode.Contains(SearchTerm) || (u.LastName + " " + u.FirstName).Contains(SearchTerm)
                          orderby u.FirstName
                          where (from u1 in context.Timesheet_tblCyclicEmp 
                                 where u1.CyclicID == CyclicID
                                 select u1.EmpID
                                 ).Contains(u.EmpID)
                          && jointable.Contains(u.EmpID) // join voi bang phan quyen du lieu  
                          select new EmployeeViewModel()
                          {
                              EmpID = u.EmpID,
                              FullName = u.EmpCode + "--" + u.LastName + " " + u.FirstName
                          };

                List<EmployeeViewModel> ret = new List<EmployeeViewModel>();
                foreach (var item in tmp)
                {
                    EmployeeViewModel p = new EmployeeViewModel()
                    {
                        EmpID = item.EmpID,
                        FullName = item.FullName
                    };
                    ret.Add(p);
                }
                return ret;
            }
            catch
            {
                return new List<EmployeeViewModel>();
            }
        }
    }
}
