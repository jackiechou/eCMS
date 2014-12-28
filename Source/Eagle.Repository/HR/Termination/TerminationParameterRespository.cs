using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model.TER;

namespace Eagle.Repository.Termination
{
    public class TerminationParameterRespository
    {
        public static List<TerminationParameterViewModel> GetList()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = (from p in context.TER_tblParameters
                           select new TerminationParameterViewModel()
                           {
                               ID = p.ID,
                               IsPaidForAnnualLeave = p.IsPaidForAnnualLeave,
                               IsPaidForMandatoryTrainingFee = p.IsPaidForMandatoryTrainingFee,
                               IsManipulatedLeaveDayRemains = p.IsManipulatedLeaveDayRemains,
                               IsManipulatedForPaidLeave = p.IsManipulatedForPaidLeave,
                               IsDeletedAfterTerminationSettlement = p.IsDeletedAfterTerminationSettlement,
                               IsAutomatedDecisionNo = p.IsAutomatedDecisionNo,
                               AutoNumberLenght = p.AutoNumberLenght,
                               Prefix = p.Prefix,
                               Suffix = p.Suffix
                           }).OrderByDescending(c => c.ID).ToList();
                return lst;
            }
        }

        public static TerminationParameterViewModel Details(int ID)
        {
            TerminationParameterViewModel entity = new TerminationParameterViewModel();
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    entity = (from p in context.TER_tblParameters
                              where p.ID == ID
                              select new TerminationParameterViewModel()
                              {
                                  ID = p.ID,
                                  IsPaidForAnnualLeave = p.IsPaidForAnnualLeave,
                                  IsPaidForMandatoryTrainingFee = p.IsPaidForMandatoryTrainingFee,
                                  IsManipulatedLeaveDayRemains = p.IsManipulatedLeaveDayRemains,
                                  IsManipulatedForPaidLeave = p.IsManipulatedForPaidLeave,
                                  IsDeletedAfterTerminationSettlement = p.IsDeletedAfterTerminationSettlement,
                                  IsAutomatedDecisionNo = p.IsAutomatedDecisionNo,
                                  AutoNumberLenght = p.AutoNumberLenght,
                                  Prefix = p.Prefix,
                                  Suffix = p.Suffix
                              }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return entity;
            }
        }

        public static bool IsIDExists(int ID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.TER_tblParameters.FirstOrDefault(c => c.ID.Equals(ID));
                return (query != null);
            }
        }

        public static TER_tblParameters Find(int? ID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.TER_tblParameters.Find(ID);
            }
        }

        public static bool Add(TerminationParameterViewModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    bool isDuplicate = IsIDExists(model.ID);
                    if (isDuplicate == false)
                    {
                        TER_tblParameters entity = new TER_tblParameters();
                        entity.IsPaidForAnnualLeave = model.IsPaidForAnnualLeave;
                        entity.IsPaidForMandatoryTrainingFee = model.IsPaidForMandatoryTrainingFee;
                        entity.IsManipulatedLeaveDayRemains = model.IsManipulatedLeaveDayRemains;
                        entity.IsManipulatedForPaidLeave = model.IsManipulatedForPaidLeave;
                        entity.IsDeletedAfterTerminationSettlement = model.IsDeletedAfterTerminationSettlement;
                        entity.IsAutomatedDecisionNo = model.IsAutomatedDecisionNo;
                        entity.AutoNumberLenght = model.AutoNumberLenght;
                        entity.Prefix = model.Prefix;
                        entity.Suffix = model.Suffix;

                        int affectedRow = 0;
                        context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            model.ID = entity.ID;
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
            }
            return result;
        }

        public static bool Update(TerminationParameterViewModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    bool IDExist = IsIDExists(model.ID);
                    if (IDExist == true)
                    {
                        TER_tblParameters entity = Find(model.ID);
                        entity.IsPaidForAnnualLeave = model.IsPaidForAnnualLeave;
                        entity.IsPaidForMandatoryTrainingFee = model.IsPaidForMandatoryTrainingFee;
                        entity.IsManipulatedLeaveDayRemains = model.IsManipulatedLeaveDayRemains;
                        entity.IsManipulatedForPaidLeave = model.IsManipulatedForPaidLeave;
                        entity.IsDeletedAfterTerminationSettlement = model.IsDeletedAfterTerminationSettlement;
                        entity.IsAutomatedDecisionNo = model.IsAutomatedDecisionNo;
                        entity.AutoNumberLenght = model.AutoNumberLenght;
                        entity.Prefix = model.Prefix;
                        entity.Suffix = model.Suffix;

                        context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int affectedRows = context.SaveChanges();
                        if (affectedRows == 1)
                        {
                            Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                            result = true;
                        }
                    }
                    else
                    {
                        result = Add(model, out Message);
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                    Message = Eagle.Resource.LanguageResource.SystemError;
                }
            }
            return result;
        }

        public static bool Delete(int? id)
        {
            bool result = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    TER_tblParameters entity = Find(id);
                    if (entity != null)
                    {
                        context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                }

                return result;
            }
        }
    }
}
