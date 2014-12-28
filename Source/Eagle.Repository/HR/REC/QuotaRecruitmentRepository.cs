using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class QuotaRecruitmentRepository
    {
        public EntityDataContext context { get; set; }
        public QuotaRecruitmentRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public REC_tblQuota Find(int Id)
        {
            return context.REC_tblQuota.Find(Id);
        }
        public bool DeleteByYear(int tmpYear, out string errorMessage)
        {
            try
            {
                var DeleteList = context.REC_tblQuota.Where(p => p.Year == tmpYear).ToList();
                foreach (var item in DeleteList)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
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
        public bool Update(List<REC_tblQuota> ListForEdit,int year, out string ErrorMessage)
        {
            try
            {

                List<int> lstInt = ListForEdit.Select(p => p.QuotaID).ToList();
                //Không tồn tại trong list này thì xóa
                List<REC_tblQuota> listDelete = context.REC_tblQuota.Where(p => p.Year == year && !lstInt.Contains(p.QuotaID)).ToList();
                foreach (var modelDelete in listDelete)
                {
                    context.Entry(modelDelete).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach (var model in ListForEdit)
                {
                    

                    //Chưa tồn tại thì thêm
                    //Đã tồn tại thì cập nhật

                    //=> chưa tồn tại => thêm
                    if (model.QuotaID == 0)
                    {
                        // kiểm tra phải lớn hơn 0
                        if (model.QuotaQuantity <= 0)
                        {
                            ErrorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputLessThan,
                                                       Eagle.Resource.LanguageResource.QuotaQuantity,"0");
                            return false;
                        }

                        //kiểm tra hợp lệ
                        if (model.QuotaQuantity < model.RecruitedQuantity)
                        {
                            ErrorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputLessThan,
                                                          Eagle.Resource.LanguageResource.QuotaQuantity,
                                                          Eagle.Resource.LanguageResource.RecruitedQuantity);
                            return false;
                        }
                       
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        var modelUpdate = Find(model.QuotaID);

                        // kiểm tra phải lớn hơn 0
                        if (model.QuotaQuantity <= 0)
                        {
                            ErrorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputLessThan,
                                                       Eagle.Resource.LanguageResource.QuotaQuantity, "0");
                            return false;
                        }

                        if (model.QuotaQuantity < modelUpdate.RecruitedQuantity)
                        {
                            ErrorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputLessThan,
                                                          Eagle.Resource.LanguageResource.QuotaQuantity,
                                                          Eagle.Resource.LanguageResource.RecruitedQuantity);
                            return false;
                        }
                        else
                        {
                            modelUpdate.QuotaQuantity = model.QuotaQuantity;
                        }

                        context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                    }
                }
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

        public List<QuotaRecruitmentViewModel> GetQuotaRecruitmentListByYear(int year, int LanguageId)
        {
            var result = from q in context.REC_tblQuota
                         join lsc in context.LS_tblCompany on q.LSCompanyID equals lsc.LSCompanyID
                         join lsp in context.LS_tblPosition on q.LSPositionID equals lsp.LSPositionID 
                         where q.Year == year
                         select new QuotaRecruitmentViewModel() { 
                            QuotaID = q.QuotaID,
                            Year = q.Year,
                            LSCompanyID = q.LSCompanyID,
                            LSCompanyName = LanguageId == 124 ? lsc.Name : lsc.VNName,
                            LSPositionID = q.LSPositionID,
                            LSPositionName = LanguageId == 124 ? lsp.Name : lsp.VNName,
                            QuotaQuantity = q.QuotaQuantity,
                            RecruitedQuantity = q.RecruitedQuantity,
                            Note = q.Note
                         };
            return result.ToList();
        }

        public int GetQuotaRecruitmentListByYear(int Year, int LSCompanyID, int LSPosition)
        {
            var result = context.REC_tblQuota.Where(p => p.Year == Year &&
                                                         p.LSCompanyID == LSCompanyID &&
                                                         p.LSPositionID == LSPosition)
                                             .Select(p => p.QuotaQuantity)
                                             .FirstOrDefault();
            return result;
            
        }

    }
}
