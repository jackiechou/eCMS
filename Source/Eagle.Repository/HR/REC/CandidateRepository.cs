using AutoMapper;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;

namespace Eagle.Repository
{
    public class CandidateRepository
    {
        public EntityDataContext context { get; set; }
        public CandidateRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool Add(REC_tblCandidate model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                //Kiểm tra xem Quy trình này đã được thiết lập chưa
                if (CandidateCodeExists(model.CandidateCode))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.CandidateCodeExistsErrorMessage;
                    return false;
                }
                if (IDNoExists(model.IDNo))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;
                    return false;
                }
                model.Result = 4;
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public bool Update(REC_tblCandidate updateNodel, WorkingExpectationViewModel Expectation, out string errorMessage)
        {
            try
            {
                errorMessage = "";
                REC_tblWorkingExpectation ExpectationUpdate = new REC_tblWorkingExpectation();
                Mapper.CreateMap<WorkingExpectationViewModel, REC_tblWorkingExpectation>();
                Mapper.Map(Expectation, ExpectationUpdate);
                if (Expectation.ExpectationID == 0)
                {
                    context.Entry(ExpectationUpdate).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    context.Entry(ExpectationUpdate).State = System.Data.Entity.EntityState.Modified;
                }

                context.Entry(updateNodel).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool CandidateCodeExists(string CandidateCode, string ValidCandidateCode = null)
        {
            if (string.IsNullOrEmpty(ValidCandidateCode))
            {
                var OnlineProcess = context.REC_tblCandidate.SingleOrDefault(p => p.CandidateCode == CandidateCode);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context.REC_tblCandidate.SingleOrDefault(p => p.CandidateCode == CandidateCode && p.CandidateCode != ValidCandidateCode);
                return OnlineProcess != null;
            }
        }
        public bool IDNoExists(string IDNo, string ValidIDNo = null)
        {
            if (string.IsNullOrEmpty(ValidIDNo))
            {
                var OnlineProcess = context.REC_tblCandidate.SingleOrDefault(p => p.IDNo == IDNo);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context.REC_tblCandidate.SingleOrDefault(p => p.IDNo == IDNo && p.IDNo != ValidIDNo);
                return OnlineProcess != null;
            }
        }
        public List<CandidateResultViewModel> Search(CandidateSearchViewModel model)
        {
            var result = from candidate in context.REC_tblCandidate
                         join WorkingExpectation in context.REC_tblWorkingExpectation on candidate.CandidateID equals WorkingExpectation.CandidateID
                         into Listtmp
                         from List1 in Listtmp.DefaultIfEmpty()
                         where (model.CandidateCode == null || candidate.CandidateCode == model.CandidateCode) &&
                         (model.Result == null || candidate.Result == model.Result) &&
                         (model.FirstName == null || candidate.FirstName.Contains(model.FirstName)) &&
                         (model.LastName == null || candidate.LastName.Contains(model.LastName)) &&
                         (model.Gender == null || candidate.Gender == model.Gender) &&
                         (model.LSLocationID == null || List1.LSLocationID == model.LSLocationID) &&
                         (model.LSPositionID == null || List1.LSPositionID == model.LSPositionID) &&
                         (model.LSPositionID_Probation1 == null || List1.LSPositionID_Probation1 == model.LSPositionID_Probation1) &&
                         (model.LSPositionID_Probation2 == null || List1.LSPositionID_Probation2 == model.LSPositionID_Probation2)
                         select new CandidateResultViewModel()
                         {
                             ApplyDate = candidate.ApplyDate,
                             CandidateCode = candidate.CandidateCode,
                             CandidateID = candidate.CandidateID,
                             DOB = candidate.DOB,
                             FirstName = candidate.FirstName,
                             LastName = candidate.LastName,
                             Result = candidate.Result
                         };
            return result.ToList();
        }

        public List<CandidateResultViewModel> Search(CandidateSearchViewModel model, List<int> CandidateIDSelectedList)
        {
            var result = from candidate in context.REC_tblCandidate
                         join WorkingExpectation in context.REC_tblWorkingExpectation on candidate.CandidateID equals WorkingExpectation.CandidateID
                         into Listtmp
                         from List1 in Listtmp.DefaultIfEmpty()
                         where !CandidateIDSelectedList.Contains(candidate.CandidateID) &&
                         (model.CandidateCode == null || candidate.CandidateCode == model.CandidateCode) &&
                         (model.Result == null || candidate.Result == model.Result) &&
                         (model.FirstName == null || candidate.FirstName.Contains(model.FirstName)) &&
                         (model.LastName == null || candidate.LastName.Contains(model.LastName)) &&
                         (model.Gender == null || candidate.Gender == model.Gender) &&
                         (model.LSLocationID == null || List1.LSLocationID == model.LSLocationID) &&
                         (model.LSPositionID == null || List1.LSPositionID == model.LSPositionID) &&
                         (model.LSPositionID_Probation1 == null || List1.LSPositionID_Probation1 == model.LSPositionID_Probation1) &&
                         (model.LSPositionID_Probation2 == null || List1.LSPositionID_Probation2 == model.LSPositionID_Probation2)
                         select new CandidateResultViewModel()
                         {
                             ApplyDate = candidate.ApplyDate,
                             CandidateCode = candidate.CandidateCode,
                             CandidateID = candidate.CandidateID,
                             DOB = candidate.DOB,
                             FirstName = candidate.FirstName,
                             LastName = candidate.LastName,
                             Result = candidate.Result
                         };
            return result.ToList();
        }
               
        public List<CandidateResultViewModel> Search(CandidateSearch2ViewModel searchModel)
        {
            var result = from p in context.REC_tblCandidate
                         join w in context.REC_tblWorkingExpectation on p.CandidateID equals w.CandidateID into list1
                         from l1 in list1.DefaultIfEmpty()
                        where (searchModel.CandidateCodeSearch == null || p.CandidateCode.Contains(searchModel.CandidateCodeSearch)) &&
                            (searchModel.FullNameSearch == null || (p.LastName + " " + p.FirstName).Contains(searchModel.FullNameSearch)) &&
                            (searchModel.IDNoSearch == null || p.IDNo.Contains(searchModel.IDNoSearch)) &&
                            (searchModel.ResultSearch == null || p.Result == searchModel.ResultSearch) &&
                            (searchModel.GenderSearch == null || p.Gender == searchModel.GenderSearch) &&
                            (searchModel.LSLocationSearchID == null || l1.LSLocationID == searchModel.LSLocationSearchID) 
                    
                        select  new CandidateResultViewModel()
                        {
                            ApplyDate = p.ApplyDate,
                            CandidateCode = p.CandidateCode,
                            CandidateID = p.CandidateID,
                            DOB = p.DOB,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            IDNo = p.IDNo,
                            Gender = p.Gender,
                            Result = p.Result

                        };
            return result.ToList();
        }

        public List<CandidateResultViewModel> SearchForTransferCandidate(TransferCandidateViewModel searchModel)
        {
            var result = from p in context.REC_tblCandidate
                         join w in context.REC_tblWorkingExpectation on p.CandidateID equals w.CandidateID into list1
                         from l1 in list1.DefaultIfEmpty()
                         where (searchModel.CandidateCode == null || p.CandidateCode.Contains(searchModel.CandidateCode)) &&
                             (searchModel.FullName == null || (p.LastName + " " + p.FirstName).Contains(searchModel.FullName)) &&
                             (searchModel.Gender == null || p.Gender == searchModel.Gender) &&
                             p.Result == searchModel.Status

                         select new CandidateResultViewModel()
                         {
                             ApplyDate = p.ApplyDate,
                             CandidateCode = p.CandidateCode,
                             CandidateID = p.CandidateID,
                             DOB = p.DOB,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             IDNo = p.IDNo,
                             Gender = p.Gender,
                             Result = p.Result

                         };
            return result.ToList();
        }
        public REC_tblCandidate Find(int CandidateID)
        {
            return context.REC_tblCandidate.Find(CandidateID);
        }

        public CandidatetmpViewModel FindCandidatetmp(int CandidateID)
        {
            var result = context.REC_tblCandidate
                            .Where(p => p.CandidateID == CandidateID)
                            .Select(p => new CandidatetmpViewModel()
                            {
                                ApplyDatetmp = p.ApplyDate,
                                CandidatetmpCode = p.CandidateCode,
                                CandidatetmpID = p.CandidateID,
                                DOBtmp = p.DOB,
                                FirstNametmp = p.FirstName,
                                LastNametmp = p.LastName,
                                Gender = p.Gender,
                                IDNotmp = p.IDNo,
                                Result = p.Result,
                                FullNametmp =  p.LastName + " " + p.FirstName
                            });
            return result.FirstOrDefault();
        }

        public CandidateViewModel FindEdit(int? CandidateID,int LanguageId = 10001)
        {
            if (LanguageId == 124)
            {
                var result = from p in context.REC_tblCandidate
                             // lấy quốc gia
                             join bc in context.LS_tblCountry on p.BornLSCountryID equals bc.LSCountryID into bcList
                             from bc1 in bcList.DefaultIfEmpty()
                             join nc in context.LS_tblCountry on p.NativeCountryID equals nc.LSCountryID into ncList
                             from nc1 in ncList.DefaultIfEmpty()
                             join pc in context.LS_tblCountry on p.PLSCountryID equals pc.LSCountryID into pcList
                             from pc1 in pcList.DefaultIfEmpty()
                             join tc in context.LS_tblCountry on p.TLSCountryID equals tc.LSCountryID into tcList
                             from tc1 in tcList.DefaultIfEmpty()
                             //lấy tình thành
                             join bct2 in context.LS_tblProvince on p.BornLSProvinceID equals bct2.LSProvinceID into bcList2
                             from bc2 in bcList2.DefaultIfEmpty()
                             join nct2 in context.LS_tblProvince on p.NativeProvinceID equals nct2.LSProvinceID into ncList2
                             from nc2 in ncList2.DefaultIfEmpty()
                             join pct2 in context.LS_tblProvince on p.PLSProvinceID equals pct2.LSProvinceID into pcList2
                             from pc2 in pcList2.DefaultIfEmpty()
                             join tct2 in context.LS_tblProvince on p.TLSProvinceID equals tct2.LSProvinceID into tcList2
                             from tc2 in tcList2.DefaultIfEmpty()

                             join ipt in context.LS_tblProvince on p.IDIssuedPlace equals ipt.LSProvinceID into ipList
                             from ip in ipList.DefaultIfEmpty()

                             //lấy quận huyện
                             join bct3 in context.LS_tblDistrict on p.BornLSDistrictID equals bct3.LSDistrictID into bcList3
                             from bc3 in bcList3.DefaultIfEmpty()
                             join nct3 in context.LS_tblDistrict on p.NativeDistrictID equals nct3.LSDistrictID into ncList3
                             from nc3 in ncList3.DefaultIfEmpty()
                             join pct3 in context.LS_tblDistrict on p.PLSDistrictID equals pct3.LSDistrictID into pcList3
                             from pc3 in pcList3.DefaultIfEmpty()
                             join tct3 in context.LS_tblDistrict on p.TLSDistrictID equals tct3.LSDistrictID into tcList3
                             from tc3 in tcList3.DefaultIfEmpty()
                             // Quốc tịch LS_tblNationality
                             join na in context.LS_tblNationality on p.LSNationalityID equals na.LSNationalityID into NationalityList
                             from nalst in NationalityList.DefaultIfEmpty()
                             // Dân tộc LS_tblEthnic
                             join et in context.LS_tblEthnic on p.LSEthnicID equals et.LSEthnicID into LSEthnicList
                             from etlst in LSEthnicList.DefaultIfEmpty()
                             // Tôn giáo LS_tblReligion
                             join re in context.LS_tblReligion on p.LSReligionID equals re.LSReligionID into ReligionList
                             from relst in ReligionList.DefaultIfEmpty()

                             //Trình độ văn hóa - giáo dục LS_tblEducation
                             join edu in context.LS_tblEducation on p.LSEducationID equals edu.LSEducationID into eduList
                             from edulst in eduList.DefaultIfEmpty()
                             //Trình độ chuyên môn                            

                             join qua in context.LS_tblQualification on p.LSQualificationID equals qua.LSQualificationID into quaList
                             from qualst in quaList.DefaultIfEmpty()
                             //
                             join maj in context.LS_tblMajor on p.LSMajorID equals maj.LSMajorID into majList
                             from majlst in majList.DefaultIfEmpty()
                             where p.CandidateID == CandidateID
                             select new CandidateViewModel()
                             {
                                 CandidateID = p.CandidateID,
                                 CandidateCode = p.CandidateCode,
                                 FirstName = p.FirstName,
                                 LastName = p.LastName,
                                 DOB = p.DOB,
                                 ApplyDate = p.ApplyDate,
                                 Gender = p.Gender,

                                 BornLSCountryID = p.BornLSCountryID,
                                 BornLSCountryName = bc1.Name,
                                 BornLSProvinceID = p.BornLSProvinceID,
                                 BornLSProvinceName = bc2.Name,
                                 BornLSDistrictID = p.BornLSDistrictID,
                                 BornLSDistrictName = bc3.Name,

                                 NativeCountryID = p.NativeCountryID,
                                 NativeCountryName = nc1.Name,
                                 NativeProvinceID = p.NativeProvinceID,
                                 NativeProvinceName = nc2.Name,
                                 NativeDistrictID = p.NativeDistrictID,
                                 NativeDistrictName = nc3.Name,

                                 PLSCountryID = p.PLSCountryID,
                                 PLSCountryName = pc1.Name,
                                 PLSProvinceID = p.PLSProvinceID,
                                 PLSProvinceName = pc2.Name,
                                 PLSDistrictID = p.PLSDistrictID,
                                 PLSDistrictName = pc3.Name,
                                 PAddress = p.PAddress,

                                 TLSCountryID = p.TLSCountryID,
                                 TLSCountryName = tc1.Name,
                                 TLSProvinceID = p.TLSProvinceID,
                                 TLSProvinceName = tc2.Name,
                                 TLSDistrictID = p.TLSDistrictID,
                                 TLSDistrictName = tc3.Name,
                                 TAddress = p.TAddress,

                                 Email = p.Email,
                                 Telephone = p.Telephone,
                                 Mobile = p.Mobile,
                                 FileId = p.FileId,
                                 IDNo = p.IDNo,
                                 IDIssuedDate = p.IDIssuedDate,
                                 IDIssuedPlace = p.IDIssuedPlace,
                                 IDIssuedPlaceName = ip.Name,

                                 LSCompanyID = p.LSCompanyID,                       
                                 LSMaritalID = p.LSMaritalID,

                                 LSNationalityID = p.LSNationalityID,
                                 LSNationalityName = nalst.Name,
                                 LSEthnicID = p.LSEthnicID,
                                 LSEthnicName = etlst.Name,
                                 LSReligionID = p.LSReligionID,
                                 LSReligionName = relst.Name,
                                 LSEducationID = p.LSEducationID,
                                 LSEducationName = edulst.Name,
                                 LSQualificationID = p.LSQualificationID,
                                 LSQualificationName = qualst.Name,
                                 LSMajorID = p.LSMajorID,
                                 LSMajorName = majlst.Name,

                                 EmergencyContact = p.EmergencyContact,
                                 EmergencyAddess = p.EmergencyAddess,
                                 EmergencyPhone = p.EmergencyPhone,
                                 EmergencyMobile = p.EmergencyMobile,
                                 Result = p.Result,
                                 Favourite = p.Favourite,
                                 Skill = p.Skill,
                                 EmpId = p.EmpId,
                                 FileIds = p.FileIds

                             };
                return result.SingleOrDefault();

            }
            else
            {
                var result = from p in context.REC_tblCandidate
                             // lấy quốc gia
                             join bc in context.LS_tblCountry on p.BornLSCountryID equals bc.LSCountryID into bcList
                             from bc1 in bcList.DefaultIfEmpty()
                             join nc in context.LS_tblCountry on p.NativeCountryID equals nc.LSCountryID into ncList
                             from nc1 in ncList.DefaultIfEmpty()
                             join pc in context.LS_tblCountry on p.PLSCountryID equals pc.LSCountryID into pcList
                             from pc1 in pcList.DefaultIfEmpty()
                             join tc in context.LS_tblCountry on p.TLSCountryID equals tc.LSCountryID into tcList
                             from tc1 in tcList.DefaultIfEmpty()
                             //lấy tình thành
                             join bct2 in context.LS_tblProvince on p.BornLSProvinceID equals bct2.LSProvinceID into bcList2
                             from bc2 in bcList2.DefaultIfEmpty()
                             join nct2 in context.LS_tblProvince on p.NativeProvinceID equals nct2.LSProvinceID into ncList2
                             from nc2 in ncList2.DefaultIfEmpty()
                             join pct2 in context.LS_tblProvince on p.PLSProvinceID equals pct2.LSProvinceID into pcList2
                             from pc2 in pcList2.DefaultIfEmpty()
                             join tct2 in context.LS_tblProvince on p.TLSProvinceID equals tct2.LSProvinceID into tcList2
                             from tc2 in tcList2.DefaultIfEmpty()

                             join ipt in context.LS_tblProvince on p.IDIssuedPlace equals ipt.LSProvinceID into ipList
                             from ip in ipList.DefaultIfEmpty()

                             //lấy quận huyện
                             join bct3 in context.LS_tblDistrict on p.BornLSDistrictID equals bct3.LSDistrictID into bcList3
                             from bc3 in bcList3.DefaultIfEmpty()
                             join nct3 in context.LS_tblDistrict on p.NativeDistrictID equals nct3.LSDistrictID into ncList3
                             from nc3 in ncList3.DefaultIfEmpty()
                             join pct3 in context.LS_tblDistrict on p.PLSDistrictID equals pct3.LSDistrictID into pcList3
                             from pc3 in pcList3.DefaultIfEmpty()
                             join tct3 in context.LS_tblDistrict on p.TLSDistrictID equals tct3.LSDistrictID into tcList3
                             from tc3 in tcList3.DefaultIfEmpty()
                             // Quốc tịch LS_tblNationality
                             join na in context.LS_tblNationality on p.LSNationalityID equals na.LSNationalityID into NationalityList
                             from nalst in NationalityList.DefaultIfEmpty()
                             // Dân tộc LS_tblEthnic
                             join et in context.LS_tblEthnic on p.LSEthnicID equals et.LSEthnicID into LSEthnicList
                             from etlst in LSEthnicList.DefaultIfEmpty()
                             // Tôn giáo LS_tblReligion
                             join re in context.LS_tblReligion on p.LSReligionID equals re.LSReligionID into ReligionList
                             from relst in ReligionList.DefaultIfEmpty()

                             //Trình độ văn hóa - giáo dục LS_tblEducation
                             join edu in context.LS_tblEducation on p.LSEducationID equals edu.LSEducationID into eduList
                             from edulst in eduList.DefaultIfEmpty()
                             //Trình độ chuyên môn                            

                             join qua in context.LS_tblQualification on p.LSQualificationID equals qua.LSQualificationID into quaList
                             from qualst in quaList.DefaultIfEmpty()
                             //
                             join maj in context.LS_tblMajor on p.LSMajorID equals maj.LSMajorID into majList
                             from majlst in majList.DefaultIfEmpty()
                             where p.CandidateID == CandidateID
                             select new CandidateViewModel()
                             {
                                 CandidateID = p.CandidateID,
                                 CandidateCode = p.CandidateCode,
                                 FirstName = p.FirstName,
                                 LastName = p.LastName,
                                 DOB = p.DOB,
                                 ApplyDate = p.ApplyDate,
                                 Gender = p.Gender,

                                 BornLSCountryID = p.BornLSCountryID,
                                 BornLSCountryName = bc1.VNName,
                                 BornLSProvinceID = p.BornLSProvinceID,
                                 BornLSProvinceName = bc2.VNName,
                                 BornLSDistrictID = p.BornLSDistrictID,
                                 BornLSDistrictName = bc3.VNName,

                                 NativeCountryID = p.NativeCountryID,
                                 NativeCountryName = nc1.VNName,
                                 NativeProvinceID = p.NativeProvinceID,
                                 NativeProvinceName = nc2.VNName,
                                 NativeDistrictID = p.NativeDistrictID,
                                 NativeDistrictName = nc3.VNName,

                                 PLSCountryID = p.PLSCountryID,
                                 PLSCountryName = pc1.VNName,
                                 PLSProvinceID = p.PLSProvinceID,
                                 PLSProvinceName = pc2.VNName,
                                 PLSDistrictID = p.PLSDistrictID,
                                 PLSDistrictName = pc3.VNName,
                                 PAddress = p.PAddress,

                                 TLSCountryID = p.TLSCountryID,
                                 TLSCountryName = tc1.VNName,
                                 TLSProvinceID = p.TLSProvinceID,
                                 TLSProvinceName = tc2.VNName,
                                 TLSDistrictID = p.TLSDistrictID,
                                 TLSDistrictName = tc3.VNName,
                                 TAddress = p.TAddress,

                                 Email = p.Email,
                                 Telephone = p.Telephone,
                                 Mobile = p.Mobile,
                                 FileId = p.FileId,
                                 IDNo = p.IDNo,
                                 IDIssuedDate = p.IDIssuedDate,
                                 IDIssuedPlace = p.IDIssuedPlace,
                                 IDIssuedPlaceName = ip.VNName,

                                 LSCompanyID = p.LSCompanyID,
                                 LSMaritalID = p.LSMaritalID,

                                 LSNationalityID = p.LSNationalityID,
                                 LSNationalityName = nalst.VNName,
                                 LSEthnicID = p.LSEthnicID,
                                 LSEthnicName = etlst.VNName,
                                 LSReligionID = p.LSReligionID,
                                 LSReligionName = relst.VNName,
                                 LSEducationID = p.LSEducationID,
                                 LSEducationName = edulst.VNName,
                                 LSQualificationID = p.LSQualificationID,
                                 LSQualificationName = qualst.VNName,
                                 LSMajorID = p.LSMajorID,
                                 LSMajorName = majlst.VNName,

                                 EmergencyContact = p.EmergencyContact,
                                 EmergencyAddess = p.EmergencyAddess,
                                 EmergencyPhone = p.EmergencyPhone,
                                 EmergencyMobile = p.EmergencyMobile,
                                 Result = p.Result,
                                 Favourite = p.Favourite,
                                 Skill = p.Skill,
                                 EmpId = p.EmpId

                             };
                return result.SingleOrDefault();
 
            }
        }



        public bool Delete(int CandidateID,out string ErrorMessage)
        {
            try
            {
                var model = Find(CandidateID);
                if (model.Result != 4)
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.ErrorCandidateDelete;
                    return false;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
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

        public bool Delete(string[] chkDelete, out string ErrorMessage)
        {
            try
            {
                int CandidateId = 0;
                foreach (string str in chkDelete)
                {
                    if (int.TryParse(str,out CandidateId))
                    {
                        var modelDelete = Find(CandidateId);
                        if (modelDelete.Result != 4)
                        {
                            ErrorMessage = Eagle.Resource.LanguageResource.ErrorCandidateDelete;
                            return false;
                        }
                        else
                        {
                            context.Entry(modelDelete).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }
                }
          
                ErrorMessage = "";
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public bool UpdateFileIds(int Id, string FileIds, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                REC_tblCandidate entity = Find(Id);
                entity.FileIds = FileIds;

                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                int affectedRows = context.SaveChanges();
                if (affectedRows == 1)
                {
                    Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }


        public List<CandidateResultViewModel> GetByListID(List<int> CandidateIDListAdd)
        {
            var result = from p in context.REC_tblCandidate
                         where CandidateIDListAdd.Contains(p.CandidateID)
                         select new CandidateResultViewModel()
                         {
                             CandidateID = p.CandidateID,
                             CandidateCode = p.CandidateCode,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             DOB = p.DOB,
                             ApplyDate = p.ApplyDate,
                             Gender = p.Gender
                         };
            return result.ToList();

        }



        public bool Update(REC_tblCandidate candidate, out string errorMessage)
        {
            try
            {
                context.Entry(candidate).State = System.Data.Entity.EntityState.Modified;
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

        public TransferEditViewModel FindForTransfer(int CandidateID)
        {
            var result = from p in context.REC_tblCandidate
                         where p.CandidateID == CandidateID
                         select new TransferEditViewModel()
                         {
                             CandidateID = p.CandidateID,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             ApplyDate = p.ApplyDate
                         };
            return result.FirstOrDefault();
            
        }
    }
}
