using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class CandidateQualificationRepository
    {
        
        public EntityDataContext context { get; set; }
        public CandidateQualificationRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<CandidateQualificationViewModel> GetBy(int? CandidateID,int LanguageId = 10001)
        {
            var result = from q in context.REC_tblQualification
                         where (CandidateID != 0 && q.CandidateID == CandidateID)
                         orderby q.Priority
                         select new CandidateQualificationViewModel()
                         {
                             QualificationID = q.QualificationID,
                             CandidateID = q.CandidateID,
                             QualificationNo = q.QualificationNo,
                             LSQualificationID = q.LSQualificationID,
                             LSQualificationIDAlowNull = q.LSQualificationID,
                             LSQualificationIDAlowNullName = LanguageId == 124 ? q.LS_tblQualification.Name : q.LS_tblQualification.VNName,
                             QualificationDate = q.QualificationDate,
                             Priority = q.Priority
                         };
            return result.ToList();
        }

        public CandidateQualificationViewModel FindEdit(int QualificationID, int LanguageId = 10001)
        {
            if (LanguageId == 124)
            {
                var result = from q in context.REC_tblQualification
                             join sc in context.LS_tblSchool on q.LSSchoolID equals sc.LSSchoolID into scList
                             from psc in scList.DefaultIfEmpty()

                             join fa in context.LS_tblFaculty on q.LSFacultyID equals fa.LSFacultyID into faList
                             from pfa in faList.DefaultIfEmpty()

                             join ma in context.LS_tblMajor on q.LSMajorID equals ma.LSMajorID into maList
                             from pma in maList.DefaultIfEmpty()

                             join co in context.LS_tblCountry on q.LSCountryID equals co.LSCountryID into coList
                             from pco in coList.DefaultIfEmpty()


                             where  q.QualificationID == QualificationID
                             select new CandidateQualificationViewModel()
                             {
                                 QualificationID = q.QualificationID,
                                 CandidateID = q.CandidateID,
                                 QualificationNo = q.QualificationNo,
                                 LSQualificationID = q.LSQualificationID,
                                 LSQualificationIDAlowNull = q.LSQualificationID,
                                 LSQualificationIDAlowNullName = q.LS_tblQualification.Name,
                                 QualificationDate = q.QualificationDate,
                                 QualificationDateAlowNull = q.QualificationDate,
                                 FromMonth = q.FromMonth,
                                 ToMonth = q.ToMonth,
                                 LSSchoolID = q.LSSchoolID,
                                 LSSchoolName = psc.Name,//zz
                                 OtherSchool = q.OtherSchool,
                                 LSFacultyID = q.LSFacultyID,
                                 LSFacultyName = pfa.Name,//zz
                                 OtherFaculty = q.OtherFaculty,
                                 LSMajorID = q.LSMajorID,
                                 LSMajorName = pma.Name,//zz
                                 LSTrainingTypeID = q.LSTrainingTypeID,
                                 PayByCompany = q.PayByCompany,
                                 TrainingPlace = q.TrainingPlace,
                                 LSCountryID = q.LSCountryID,
                                 LSCountryName = pco.Name,//zz
                                 AttachFile = q.AttachFile,
                                 Priority = q.Priority,
                                 PriorityAlowNull = q.Priority,
                                 Note = q.Note
                             };
                return result.FirstOrDefault();
            }
            else
            {
                var result = from q in context.REC_tblQualification
                             join sc in context.LS_tblSchool on q.LSSchoolID equals sc.LSSchoolID into scList
                             from psc in scList.DefaultIfEmpty()

                             join fa in context.LS_tblFaculty on q.LSFacultyID equals fa.LSFacultyID into faList
                             from pfa in faList.DefaultIfEmpty()

                             join ma in context.LS_tblMajor on q.LSMajorID equals ma.LSMajorID into maList
                             from pma in maList.DefaultIfEmpty()

                             join co in context.LS_tblCountry on q.LSCountryID equals co.LSCountryID into coList
                             from pco in coList.DefaultIfEmpty()


                             where q.QualificationID == QualificationID
                             select new CandidateQualificationViewModel()
                             {
                                 QualificationID = q.QualificationID,
                                 CandidateID = q.CandidateID,
                                 QualificationNo = q.QualificationNo,
                                 LSQualificationID = q.LSQualificationID,
                                 LSQualificationIDAlowNull = q.LSQualificationID,
                                 LSQualificationIDAlowNullName = q.LS_tblQualification.VNName,
                                 QualificationDate = q.QualificationDate,
                                 QualificationDateAlowNull = q.QualificationDate,
                                 FromMonth = q.FromMonth,
                                 ToMonth = q.ToMonth,
                                 LSSchoolID = q.LSSchoolID,
                                 LSSchoolName = psc.VNName,//zz
                                 OtherSchool = q.OtherSchool,
                                 LSFacultyID = q.LSFacultyID,
                                 LSFacultyName = pfa.VNName,//zz
                                 OtherFaculty = q.OtherFaculty,
                                 LSMajorID = q.LSMajorID,
                                 LSMajorName = pma.VNName,//zz
                                 LSTrainingTypeID = q.LSTrainingTypeID,
                                 PayByCompany = q.PayByCompany,
                                 TrainingPlace = q.TrainingPlace,
                                 LSCountryID = q.LSCountryID,
                                 LSCountryName = pco.VNName,//zz
                                 AttachFile = q.AttachFile,
                                 Priority = q.Priority,
                                 PriorityAlowNull = q.Priority,
                                 Note = q.Note
                             };
                return result.FirstOrDefault();
            }
        }

        public bool Add(REC_tblQualification modelAdd, out string errorMessage)
        {
            try
            {                
                if (isPriorityExists(modelAdd.CandidateID,modelAdd.Priority))
                {
                    errorMessage = Eagle.Resource.LanguageResource.PriorityisExists;
                    return false;
                }
                if (isQualificationNoExists(modelAdd.CandidateID,modelAdd.QualificationNo))
                {
                    errorMessage = Eagle.Resource.LanguageResource.InValidQualificationNo;
                    return false;
                }
                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;
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

        public bool Update(REC_tblQualification modelAdd, out string errorMessage)
        {
            try
            {
                var tmp = context.REC_tblQualification.Where(p => p.QualificationID == modelAdd.QualificationID).Select(p => new { p.Priority, p.QualificationNo }).FirstOrDefault();
                if (tmp != null)
                {
                    if (tmp.Priority != modelAdd.Priority && isPriorityExists(modelAdd.CandidateID, modelAdd.Priority))
                    {
                        errorMessage = Eagle.Resource.LanguageResource.PriorityisExists;
                        return false;
                    }
                    if (tmp.QualificationNo != modelAdd.QualificationNo && isQualificationNoExists(modelAdd.CandidateID, modelAdd.QualificationNo))
                    {
                        errorMessage = Eagle.Resource.LanguageResource.InValidQualificationNo;
                        return false;
                    }
                }
                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Modified;
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

        public bool Delete(int id,out string ErrorMessage)
        {
            try
            {
                var modelDelete = context.REC_tblQualification.Find(id);
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

        public bool isPriorityExists(int CandidateID, int Priority)
        {
            var result = context.REC_tblQualification.Where(p => p.CandidateID == CandidateID && p.Priority == Priority).FirstOrDefault();
            return result != null;
        }

        public bool isQualificationNoExists(int CandidateID, string QualificationNo)
        {
            var result = context.REC_tblQualification.Where(p => p.CandidateID == CandidateID && p.QualificationNo == QualificationNo).FirstOrDefault();
            return result != null;
        }
    }
}
