using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class WorkingExpectationRepository
    {
        public EntityDataContext context { get; set; }
        public WorkingExpectationRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<WorkingExpectationViewModel> GetAll()
        {
            List<WorkingExpectationViewModel> lst = context.REC_tblWorkingExpectation
                                                     .Select(p => new WorkingExpectationViewModel() { 
                                                         CandidateID =  p.CandidateID,
                                                         CurrentSalary = p.CurrentSalary,
                                                         ExpectationID = p.ExpectationID,
                                                         ExpectationSalary = p.ExpectationSalary,
                                                         LS_tblCurrency = p.LS_tblCurrency,
                                                         LS_tblLocation = p.LS_tblLocation,
                                                         LS_tblPosition = p.LS_tblPosition,
                                                         LS_tblPosition1 = p.LS_tblPosition1
                                                     }).ToList();
            return lst;
        }

        public WorkingExpectationViewModel FindEdit(int? CandidateId,int LanguageId = 10001)
        {
            if (LanguageId == 124)
            {
                var result =    from p in context.REC_tblWorkingExpectation
                                join Position in context.LS_tblPosition on p.LSPositionID equals Position.LSPositionID into table1
                                from p1 in table1.DefaultIfEmpty()
                                
                                join Position1 in context.LS_tblPosition on p.LSPositionID_Probation1 equals Position1.LSPositionID into table2
                                from p2 in table2.DefaultIfEmpty()

                                join Position2 in context.LS_tblPosition on p.LSPositionID_Probation2 equals Position2.LSPositionID into table3
                                from p3 in table3.DefaultIfEmpty()

                                join Currency in context.LS_tblCurrency on p.LSCurrencyID equals Currency.LSCurrencyID into table4
                                from c1 in table4.DefaultIfEmpty()

                                where p.CandidateID == CandidateId
                                select new WorkingExpectationViewModel()
                                {
                                    CandidateID = p.CandidateID,
                                    ExpectationID = p.ExpectationID,
                                    LSPositionID = p.LSPositionID,
                                    LSPositionID_Probation1 = p.LSPositionID_Probation1,
                                    LSPositionID_Probation2 = p.LSPositionID_Probation2,
                                    ExpectationSalary = p.ExpectationSalary,
                                    MinSalary = p.MinSalary,
                                    CurrentSalary = p.CurrentSalary,
                                    LSCurrencyID = p.LSCurrencyID,
                                    LSLocationID = p.LSLocationID,
                                    WorkType = p.WorkType,
                                    OtherExpectation = p.OtherExpectation,
                                    Note = p.Note,

                                    LSPositionName = p1.Name,
                                    LSPositionID_Probation1Name = p2.Name,
                                    LSPositionID_Probation2Name = p3.Name,
                                    LSCurrencyName = c1.Name
                                };
                var ret = result.FirstOrDefault();
                if (ret == null)
                {
                    return new WorkingExpectationViewModel();
                }
                else
                {
                    return ret;
                }
            }
            else
            {
                var result = from p in context.REC_tblWorkingExpectation
                             join Position in context.LS_tblPosition on p.LSPositionID equals Position.LSPositionID into table1
                             from p1 in table1.DefaultIfEmpty()

                             join Position1 in context.LS_tblPosition on p.LSPositionID_Probation1 equals Position1.LSPositionID into table2
                             from p2 in table2.DefaultIfEmpty()

                             join Position2 in context.LS_tblPosition on p.LSPositionID_Probation2 equals Position2.LSPositionID into table3
                             from p3 in table3.DefaultIfEmpty()

                             join Currency in context.LS_tblCurrency on p.LSCurrencyID equals Currency.LSCurrencyID into table4
                             from c1 in table4.DefaultIfEmpty()

                             where p.CandidateID == CandidateId
                              select new WorkingExpectationViewModel()
                              {
                                  CandidateID = p.CandidateID,
                                  ExpectationID = p.ExpectationID,
                                  LSPositionID = p.LSPositionID,
                                  LSPositionID_Probation1 = p.LSPositionID_Probation1,
                                  LSPositionID_Probation2 = p.LSPositionID_Probation2,
                                  ExpectationSalary = p.ExpectationSalary,
                                  MinSalary = p.MinSalary,
                                  CurrentSalary = p.CurrentSalary,
                                  LSCurrencyID = p.LSCurrencyID,
                                  LSLocationID = p.LSLocationID,
                                  WorkType = p.WorkType,
                                  OtherExpectation = p.OtherExpectation,
                                  Note = p.Note,

                                  LSPositionName = p1.VNName,
                                  LSPositionID_Probation1Name = p2.VNName,
                                  LSPositionID_Probation2Name = p3.VNName,
                                  LSCurrencyName = c1.VNName
                              };
                var ret = result.FirstOrDefault();
                if (ret == null)
                {
                    return new WorkingExpectationViewModel();
                }
                else
                {
                    return ret;
                }
            }
           
        }

        public bool Add(REC_tblWorkingExpectation addModel,out string errorMessage)
        {
            try
            {
                if (addModel.ExpectationID == 0)
                {
                    context.Entry(addModel).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    context.Entry(addModel).State = System.Data.Entity.EntityState.Modified;
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
    }
}
