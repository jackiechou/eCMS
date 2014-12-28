using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Model;
using Eagle.Core;

namespace Eagle.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class EvaluationTemplateReposiory
    {
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public EvaluationTemplateReposiory(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<EvaluationTemplateViewModel> GetListOfEvaluationTemplate(out string errorMessage)
        {
            try
            {
                var found = (from temp in this.Context.TR_tblEvaluationTemplate
                            select new EvaluationTemplateViewModel() 
                            {
                                EvaluationTemplateID = temp.EvaluationTemplateID,
                                EvaluationTemplateCode = temp.EvaluationTemplateCode,
                                EvaluationTemplateName = temp.EvaluationTemplateName,
                                Note = temp.Note

                            }).ToList();
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<EvaluationTemplateViewModel> GetListOfEvaluationTemplate(EvaluationTemplateViewModel model, out string errorMessage)
        {
            try
            {
                var found = (from temp in this.Context.TR_tblEvaluationTemplate
                             where (temp.EvaluationTemplateCode.ToUpper() == model.EvaluationTemplateCode.ToUpper() || String.IsNullOrEmpty(model.EvaluationTemplateCode))
                             && (temp.EvaluationTemplateName.ToUpper().Contains(model.EvaluationTemplateName.ToUpper()) || String.IsNullOrEmpty(model.EvaluationTemplateName))
                             select new EvaluationTemplateViewModel()
                             {
                                 EvaluationTemplateID = temp.EvaluationTemplateID,
                                 EvaluationTemplateCode = temp.EvaluationTemplateCode,
                                 EvaluationTemplateName = temp.EvaluationTemplateName,
                                 Note = temp.Note

                             }).ToList();
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<EvaluationTemplateDetailViewModel> GetListOfEvaluationTemplateDetail(out string errorMessage)
        {
            try
            {
                var found = (from part in this.Context.LS_tblTrainingApprisalPart
                             //join detail in this.Context.TR_tblEvaluationTemplateDetail on part.LSTrainingApprisalPartID equals detail.LSTrainingApprisalPartID into DetailPart
                             //from detail in DetailPart.DefaultIfEmpty()
                             select new EvaluationTemplateDetailViewModel()
                             {
                                 EvaluationTemplateID = 0,
                                 //LSTrainingApprisalPartID = part.LSTrainingApprisalPartID(detail == null ? part.LSTrainingApprisalPartID : detail.LSTrainingApprisalPartID),
                                 LSTrainingApprisalPartID = part.LSTrainingApprisalPartID,
                                 LSTrainingApprisalPartName = part.Name,
                                 Checked = true
                             }).ToList();

                foreach (var detail in found)
                {
                    var listOfTrainingApprisalItem = (from item in this.Context.LS_tblTrainingApprisalItem
                                                      //join templateItem in this.Context.TR_tblTemplateItem on item.LSTrainingApprisalItemID equals templateItem.LSTrainingApprisalItemID into ItemTemplate
                                                      //from templateItem in ItemTemplate.DefaultIfEmpty()
                                                      where item.LSTrainingApprisalPartID == detail.LSTrainingApprisalPartID
                                                      select new TrainingApprisalItemViewModel()
                                                      {
                                                          LSTrainingApprisalItemID = item.LSTrainingApprisalItemID,
                                                          LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                                                          Name = item.Name,
                                                          Note = item.Note,
                                                          Checked = true,
                                                          Item_EvaluationTemplateID = detail.EvaluationTemplateID,
                                                          Item_TemplateItemID = 0
                                                          
                                                      }).ToList();
                    detail.ListOfTrainingApprisalItem = listOfTrainingApprisalItem;
                }
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public EvaluationTemplateViewModel FindForEdit(int id, out string errorMessage)
        {
            try
            {
                var found = (from template in this.Context.TR_tblEvaluationTemplate
                            where template.EvaluationTemplateID == id
                            select new EvaluationTemplateViewModel()
                            {
                                EvaluationTemplateID = template.EvaluationTemplateID,
                                EvaluationTemplateCode = template.EvaluationTemplateCode,
                                EvaluationTemplateName = template.EvaluationTemplateName,
                                Note = template.Note

                            }).FirstOrDefault();

                var listOfTemplateDetail = (from part in this.Context.LS_tblTrainingApprisalPart
                                           join detail in this.Context.TR_tblEvaluationTemplateDetail.Where(p => p.EvaluationTemplateID == found.EvaluationTemplateID) on part.LSTrainingApprisalPartID equals detail.LSTrainingApprisalPartID into PartDetail
                                           from detail in PartDetail.DefaultIfEmpty()                                                  
                                            select new EvaluationTemplateDetailViewModel()
                                            {
                                                EvaluationTemplateID = (detail == null ? 0 : detail.EvaluationTemplateID),
                                                LSTrainingApprisalPartID = part.LSTrainingApprisalPartID,
                                                LSTrainingApprisalPartName = part.Name,
                                                Checked = (detail != null ? true : false),
                                                Note = part.Note

                                            }).ToList();
                found.ListOfTemplateDetail = listOfTemplateDetail;


                foreach (var item in found.ListOfTemplateDetail)
                {
                    item.ListOfTrainingApprisalItem.Clear();

                    var listOfApprisalItem = (from apprisalItem in this.Context.LS_tblTrainingApprisalItem.Where(p => p.LSTrainingApprisalPartID == item.LSTrainingApprisalPartID)
                                              join template in this.Context.TR_tblTemplateItem.Where(p => p.EvaluationTemplateID == found.EvaluationTemplateID) on apprisalItem.LSTrainingApprisalItemID equals template.LSTrainingApprisalItemID into ItemTemplate
                                              from template in ItemTemplate.DefaultIfEmpty()                                              
                                              select new TrainingApprisalItemViewModel
                                              {                                                  
                                                  LSTrainingApprisalItemID = (template == null ? apprisalItem.LSTrainingApprisalItemID : apprisalItem.LSTrainingApprisalItemID),                                                  
                                                  LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,                                                  
                                                  LSTrainingApprisalItemCode = apprisalItem.LSTrainingApprisalItemCode,
                                                  Name = apprisalItem.Name,
                                                  Note = apprisalItem.Note,
                                                  Checked = (template != null ? true : false),
                                                  //Checked =
                                                  //(
                                                  //   template.EvaluationTemplateID == item.EvaluationTemplateID &&
                                                  //   template.LSTrainingApprisalPartID == item.LSTrainingApprisalPartID &&
                                                  //   template.EvaluationTemplateID == id ? true : false
                                                  //),
                                                  Item_EvaluationTemplateID = item.EvaluationTemplateID == null ? 0 : item.EvaluationTemplateID,
                                                  Item_TemplateItemID = template.TemplateItemID == null ? 0 : template.TemplateItemID
                                              }).ToList();


                    item.ListOfTrainingApprisalItem = listOfApprisalItem;
                }
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="templateModel"></param>
        /// <param name="listOfTemplateItem"></param>
        /// <param name="template"></param>
        /// <param name="templateItem"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool MappingForAdding(EvaluationTemplateViewModel templateModel, List<TemplateItemViewModel> listOfTemplateItemModel, 
            ref TR_tblEvaluationTemplate Template, ref List<TR_tblTemplateItem> ListOfTemplateItem, out string errorMessage)
        {
            try
            {
                // Master : EvaluationTemplate
                AutoMapper.Mapper.CreateMap<EvaluationTemplateViewModel, TR_tblEvaluationTemplate>();
                Template = AutoMapper.Mapper.Map<TR_tblEvaluationTemplate>(templateModel);

                // Detail : EvaluationTemplate
                Template.TR_tblEvaluationTemplateDetail.Clear();
                foreach (var item in templateModel.ListOfTemplateDetail)
                {
                    Template.TR_tblEvaluationTemplateDetail.Add(new TR_tblEvaluationTemplateDetail()
                        {
                            EvaluationTemplateID = item.EvaluationTemplateID,
                            LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                            Note = item.Note
                        });
                }

                // TemplateItem
                ListOfTemplateItem.Clear();
                foreach (var item in listOfTemplateItemModel)
                {
                    ListOfTemplateItem.Add(new TR_tblTemplateItem()
                    {
                        TemplateItemID = Template.EvaluationTemplateID,
                        EvaluationTemplateID = item.EvaluationTemplateID,
                        LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                        LSTrainingApprisalItemID = item.LSTrainingApprisalItemID,
                        Note = item.Note
                    });
                }                                
                errorMessage = String.Empty;

                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="templateModel"></param>
        /// <param name="listOfTemplateItem"></param>
        /// <param name="template"></param>
        /// <param name="templateItem"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool MappingForEditing(EvaluationTemplateViewModel templateModel, List<TemplateItemViewModel> listOfTemplateItemModel,
            ref TR_tblEvaluationTemplate Template, ref List<TR_tblTemplateItem> ListOfTemplateItem, out string errorMessage)
        {
            try
            {
                // Master : EvaluationTemplate
                AutoMapper.Mapper.CreateMap<EvaluationTemplateViewModel, TR_tblEvaluationTemplate>();
                Template = AutoMapper.Mapper.Map<TR_tblEvaluationTemplate>(templateModel);

                // Detail : EvaluationTemplate
                Template.TR_tblEvaluationTemplateDetail.Clear();
                foreach (var item in templateModel.ListOfTemplateDetail)
                {
                    Template.TR_tblEvaluationTemplateDetail.Add(new TR_tblEvaluationTemplateDetail()
                    {
                        EvaluationTemplateID = item.EvaluationTemplateID,
                        LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                        Note = item.Note
                    });
                }

                // TemplateItem
                ListOfTemplateItem.Clear();
                foreach (var item in listOfTemplateItemModel)
                {
                    ListOfTemplateItem.Add(new TR_tblTemplateItem()
                    {
                        TemplateItemID = item.TemplateItemID,
                        EvaluationTemplateID = Template.EvaluationTemplateID,
                        LSTrainingApprisalPartID = item.LSTrainingApprisalPartID,
                        LSTrainingApprisalItemID = item.LSTrainingApprisalItemID,
                        Note = item.Note
                    });
                }
                errorMessage = String.Empty;

                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="listOfTemplateItem"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(EvaluationTemplateViewModel templateModel, List<TemplateItemViewModel> listOfTemplateItemModel, out string errorMessage)
        {
            try
            {
                var template = new TR_tblEvaluationTemplate();
                var listOfTemplateItem = new List<TR_tblTemplateItem>();
                var resultOfMapping = this.MappingForAdding(templateModel, listOfTemplateItemModel, ref template, ref listOfTemplateItem, out errorMessage);
                if (!resultOfMapping)
                {
                    return false;
                }
                this.Context.Entry(template).State = System.Data.Entity.EntityState.Added;

                foreach (var item in listOfTemplateItem)
                {
                    item.EvaluationTemplateID = template.EvaluationTemplateID;
                    this.Context.Entry(item).State = System.Data.Entity.EntityState.Added;
                }
                this.Context.SaveChanges();
                errorMessage = String.Empty;

                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateModel"></param>
        /// <param name="listOfTemplateItemModel"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Update(EvaluationTemplateViewModel templateModel, List<TemplateItemViewModel> listOfTemplateItemModel, out string errorMessage)
        {
            try
            {
                var template = new TR_tblEvaluationTemplate();
                var listOfTemplateItem = new List<TR_tblTemplateItem>();
                var resultOfMapping = this.MappingForEditing(templateModel, listOfTemplateItemModel, ref template, ref listOfTemplateItem, out errorMessage);
                if (!resultOfMapping)
                {
                    return false;
                }
                var found = this.Context.TR_tblEvaluationTemplate.Where(p => p.EvaluationTemplateID == template.EvaluationTemplateID).FirstOrDefault();
                if (found != null)
                {
                    found.EvaluationTemplateCode = template.EvaluationTemplateCode;
                    found.EvaluationTemplateName = template.EvaluationTemplateName;
                    found.TR_tblEvaluationTemplateDetail.Clear();
                    found.TR_tblEvaluationTemplateDetail = template.TR_tblEvaluationTemplateDetail;                    
                }

                var listOfItem = this.Context.TR_tblTemplateItem.Where(p => p.EvaluationTemplateID == found.EvaluationTemplateID).ToList();
                foreach (var item in listOfItem)
                {
                    var checking = listOfTemplateItem.Where(p => p.LSTrainingApprisalPartID == item.LSTrainingApprisalPartID && p.LSTrainingApprisalItemID == item.LSTrainingApprisalItemID).FirstOrDefault();
                    if (checking == null)
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                    else
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                // Updating of TemplateItem
                foreach (var item in listOfTemplateItem)
                {
                    if (item.TemplateItemID == 0)
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }                    
                }

                // Updating of EvaluationTemplate
                this.Context.Entry(found).State = System.Data.Entity.EntityState.Modified;

                this.Context.SaveChanges();
                errorMessage = String.Empty;

                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var found = this.Context.TR_tblEvaluationTemplate.Where(p => p.EvaluationTemplateID == id).FirstOrDefault();
                if (found == null)
                {
                    errorMessage = Eagle.Resource.LanguageResource.TrainingAnwserTypeNotExistDataForUpdateOrDelete;
                    return false;
                }
                var listOfFound = this.Context.TR_tblTemplateItem.Where(p => p.EvaluationTemplateID == id).ToList();
                if (listOfFound != null && listOfFound.Count == 0)
                {
                    errorMessage = Eagle.Resource.LanguageResource.TrainingAnwserTypeNotExistDataForUpdateOrDelete;
                    return false;
                }
                foreach (var item in listOfFound)
                {
                    if (item.EvaluationTemplateID == id)
                    {
                        this.Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                var listOfTemplateDetail = this.Context.TR_tblEvaluationTemplateDetail.Where(p => p.EvaluationTemplateID == id).ToList();
                foreach (var detail in listOfTemplateDetail)
                {
                    if (detail.EvaluationTemplateID == id)
                    {
                        this.Context.Entry(detail).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                this.Context.Entry(found).State = System.Data.Entity.EntityState.Deleted;
                this.Context.SaveChanges();
                errorMessage = String.Empty;

                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }
    }

    
}
