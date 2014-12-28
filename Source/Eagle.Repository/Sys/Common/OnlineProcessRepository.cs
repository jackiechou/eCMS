using Eagle.Core;
using Eagle.Repository.LS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Script.Serialization;
using Eagle.Model;

namespace Eagle.Repository
{
    public class OnlineProcessRepository
    {
        public EntityDataContext context { get; set; }
        public OnlineProcessRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<LS_tblOnlineProcessViewModel> GetAll(int LanguageId = 10001)
        {
            try
            {  
                var result = from p in context.LS_tblOnlineProcess
                             join function in context.SYS_tblFunctionList on p.FunctionID equals function.FunctionID into List1
                             from l1 in List1.DefaultIfEmpty()
                             select new LS_tblOnlineProcessViewModel()
                             {
                                 DMQuiTrinhID = p.DMQuiTrinhID,
                                 IsStart = p.IsStart,
                                 Description = p.Description,
                                 FunctionID = p.FunctionID,
                                 NameProcessOnline = p.NameProcessOnline,
                                 NoOfLevel = p.NoOfLevel,
                                 FunctionName = LanguageId == 124 ? l1.FunctionNameE : l1.FunctionNameV
                             };

                return result.ToList();
            }
            catch
            {
                return new List<LS_tblOnlineProcessViewModel>();
            }
        }
        public bool IsIDExists(int ID)
        {
            var query = context.LS_tblOnlineProcess.FirstOrDefault(p => p.DMQuiTrinhID.Equals(ID));
            return (query != null);
        }

        public LS_tblOnlineProcess GetDetails(int id)
        {
            return context.LS_tblOnlineProcess.Find(id);
        }
        public bool Add(LS_tblOnlineProcessViewModel model,out string ErrorMessage)
        {
            bool result = false;
            ErrorMessage = string.Empty;
            //Kiểm tra đã thiết lập function thì không cho thêm
            var checkResult = (from p in context.LS_tblOnlineProcess
                               where p.FunctionID == model.FunctionID
                               select new { p.DMQuiTrinhID }).FirstOrDefault();
            if (checkResult != null)
            {
                ErrorMessage = Eagle.Resource.LanguageResource.InvalidOnlineProcess_Function;
                return false;
            }
            bool isDuplicate = IsIDExists(model.DMQuiTrinhID);
            if (isDuplicate == false)
            {  
                // Define a transaction scope for the operations.                
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    try
                    {
                        
                        LS_tblOnlineProcess entity = new LS_tblOnlineProcess();
                        entity.NameProcessOnline = model.NameProcessOnline;
                        entity.FunctionID = model.FunctionID;
                        entity.NoOfLevel = model.NoOfLevel;
                        entity.IsStart = model.IsStart;
                        entity.Description = model.Description;
                        context.LS_tblOnlineProcess.Add(entity);  

                        int DMQuiTrinhID = entity.DMQuiTrinhID;
                        if (model.Bcc != null && model.Bcc != string.Empty)
                        {
                            List<string> Bcclist = model.Bcc.Split(',').ToList();

                            if (Bcclist.Count > 0)
                            {
                                foreach (var EmpID in Bcclist)
                                {
                                    LS_tblOnlineProcessMailBcc bcc_entity = new LS_tblOnlineProcessMailBcc();
                                    bcc_entity.DMQuiTrinhID = DMQuiTrinhID;
                                    bcc_entity.EmpID = Convert.ToInt32(EmpID);
                                    context.LS_tblOnlineProcessMailBcc.Add(bcc_entity);
                                }
                            }
                        }        
                        
                        if (model.Cc != null && model.Cc != string.Empty)
                        {
                            List<string> Cclist = model.Cc.Split(',').ToList();
                            if (Cclist.Count > 0)
                            {
                                foreach (var EmpID in Cclist)
                                {
                                    LS_tblOnlineProcessMailCc cc_entity = new LS_tblOnlineProcessMailCc();
                                    cc_entity.DMQuiTrinhID = DMQuiTrinhID;
                                    cc_entity.EmpID = Convert.ToInt32(EmpID);
                                    context.LS_tblOnlineProcessMailCc.Add(cc_entity);
                                }
                            }
                        }

                        context.SaveChanges();
                        transactionScope.Complete();
                        context.Dispose();
                        result = true;
                    }
                    catch (Exception ex)
                    {                      
                        ErrorMessage = ex.ToString();
                        result = false;                       
                    }
                }
            }            
            return result;
        }

       
        public LS_tblOnlineProcessViewModel Find(int id)
        {
            string strIdBccList = string.Empty, strIdCcList = string.Empty, strBccTextList = string.Empty, strCcTextList = string.Empty;
            string BccJson = LS_tblOnlineProcessMailBccRespository.GetEmpListById(id, out strIdBccList, out strBccTextList);
            string CcJson = LS_tblOnlineProcessMailCcRespository.GetEmpListById(id, out strIdCcList, out strCcTextList);

            var query = (from p in context.LS_tblOnlineProcess
                         where p.DMQuiTrinhID == id
                         select new LS_tblOnlineProcessViewModel()
                         {
                             DMQuiTrinhID = p.DMQuiTrinhID,
                             NameProcessOnline = p.NameProcessOnline,                           
                             FunctionID = p.FunctionID,
                             NoOfLevel = p.NoOfLevel,
                             IsStart = p.IsStart,
                             Description = p.Description,

                             Bcc = strIdBccList,
                             BccText = strBccTextList,
                             BccJson = BccJson,

                             Cc = strIdCcList,
                             CcText = strCcTextList,                            
                             CcJson = CcJson
                         }).FirstOrDefault();
            return query;
        }

        public bool Update(LS_tblOnlineProcessViewModel model, out string ErrorMessage)
        {
            bool result = false;
            ErrorMessage = string.Empty;
            try
            {
                LS_tblOnlineProcess entity = GetDetails(model.DMQuiTrinhID);
                if (entity.DMQuiTrinhID > 0)
                {
                    entity.NameProcessOnline = model.NameProcessOnline;
                    entity.FunctionID = model.FunctionID;
                    entity.NoOfLevel = model.NoOfLevel;
                    entity.IsStart = model.IsStart;
                    entity.Description = model.Description;

                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        try
                        {
                            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                            int affectedRow = context.SaveChanges();
                            if (affectedRow > 0)
                            {
                                int DMQuiTrinhID = model.DMQuiTrinhID;

                                //BCC - DELETE ALL DATA RELATED TO ID==============================================================
                                List<LS_tblOnlineProcessMailBcc> bcc_lst = context.LS_tblOnlineProcessMailBcc.Where(p => p.DMQuiTrinhID.Equals(DMQuiTrinhID)).ToList();
                                if (bcc_lst.Count > 0)
                                {
                                    foreach (var bcc_entity in bcc_lst)
                                    {
                                        context.LS_tblOnlineProcessMailBcc.Remove(bcc_entity);
                                    }
                                }
                                //BCC - INSERT DATA =====================================================================================
                                if (model.Bcc != null && model.Bcc != string.Empty)
                                {
                                    List<string> Bcclist = model.Bcc.Split(',').ToList();
                                    if (Bcclist.Count > 0)
                                    {
                                        
                                        foreach (var EmpID in Bcclist)
                                        {
                                            LS_tblOnlineProcessMailBcc bcc_entity = new LS_tblOnlineProcessMailBcc();
                                            bcc_entity.DMQuiTrinhID = DMQuiTrinhID;
                                            bcc_entity.EmpID = Convert.ToInt32(EmpID);
                                            context.LS_tblOnlineProcessMailBcc.Add(bcc_entity);
                                        }
                                       
                                    }
                                }
                                //=================================================================================================
                            

                                //=================================================================================================
                                //CC - DELETE ALL DATA RELATED TO ID==============================================================
                                List<LS_tblOnlineProcessMailCc> cc_lst = context.LS_tblOnlineProcessMailCc.Where(p => p.DMQuiTrinhID.Equals(DMQuiTrinhID)).ToList();
                                if (cc_lst.Count > 0)
                                {
                                    foreach (var cc_entity in cc_lst)
                                    {
                                        context.LS_tblOnlineProcessMailCc.Remove(cc_entity);
                                    }
                                }
                                //CC - INSERT DATA =====================================================================================
                                if (model.Cc != null && model.Cc != string.Empty)
                                {
                                    List<string> Cclist = model.Cc.Split(',').ToList();
                                    if (Cclist.Count > 0)
                                    {
                                        foreach (var EmpID in Cclist)
                                        {
                                            LS_tblOnlineProcessMailCc cc_entity = new LS_tblOnlineProcessMailCc();
                                            cc_entity.DMQuiTrinhID = DMQuiTrinhID;
                                            cc_entity.EmpID = Convert.ToInt32(EmpID);
                                            context.LS_tblOnlineProcessMailCc.Add(cc_entity);
                                        }
                                    }
                                }
                                //=================================================================================================
                               
                            }

                            context.SaveChanges();
                            transactionScope.Complete();
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = ex.ToString();
                            result = false;
                        }
                    }                    
                }
                
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
            }
            return result;

        }

        public bool Delete(int id, out string ErrorMessage)
        {
            try
            {
                var modelDelete = GetDetails(id);
                context.Entry(modelDelete).State = System.Data.Entity.EntityState.Deleted;
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
    }
}
