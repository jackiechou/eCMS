using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;

using Eagle.Model.LS;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class HR_MasterDataController : BaseController
    {
        //
        // GET: /Admin/HR_MasterData/
        // list danh sach cac danh muc 
        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }

            MasterDataRepository _repository = new MasterDataRepository(db);
            List<MasterDataViewModel> sources = _repository.ListMaster("HR", LanguageId);
            return View(sources);
        }

        #region Danh muc Location ##############################################################
        // tra ve list danh muc location
        public ActionResult LS_tblLocation()
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblLocationRepository _repository = new LS_tblLocationRepository(db);
            IList<LS_tblLocationViewModel> sources = _repository.ListLocation().ToList();
            return PartialView("_LS_tblLocation", sources);
        }
        public ActionResult _CreateLS_tblLocation()
        {
            LS_tblLocationViewModel acc = new LS_tblLocationViewModel();
            acc.Used = true;
            return PartialView("_LS_tblLocationCreate", acc);
        }
        public ActionResult _EditLS_tblLocation(int id)
        {
            LS_tblLocationRepository _repository = new LS_tblLocationRepository(db);
            LS_tblLocationViewModel acc = _repository.FindEditModel(id);

            return PartialView("_LS_tblLocationCreate", acc);
        }
        #endregion
        #region Danh muc Position
        // tra ve list danh muc location
        public ActionResult LS_tblPosition()
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblPositionRepository _repository = new LS_tblPositionRepository(db);
            IList<LS_tblPositionViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblPosition", sources);
        }
        public ActionResult _CreateLS_tblPosition()
        {
            LS_tblPositionViewModel model = new LS_tblPositionViewModel();
            model.Used = true;

            return PartialView("_LS_tblPositionCreate", model);
        }
        public ActionResult _EditLS_tblPosition(int id)
        {
            LS_tblPositionRepository _repository = new LS_tblPositionRepository(db);
            LS_tblPositionViewModel model =_repository.FindEditModel(id);

            return PartialView("_LS_tblPositionCreate", model);
        }
        #endregion
        #region Danh muc Country - quoc gia
        // tra ve list danh muc 
        public ActionResult LS_tblCountry()
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblCountryRepository _repository = new LS_tblCountryRepository(db);
            IList<LS_tblCountryViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblCountry", sources);
        }
        public ActionResult _CreateLS_tblCountry()
        {
            LS_tblCountryViewModel model = new LS_tblCountryViewModel(); 
            model.Used = true;
            return PartialView("_LS_tblCountryCreate", model);
        }
        public ActionResult _EditLS_tblCountry(int id)
        {
            LS_tblCountryRepository _repository = new LS_tblCountryRepository(db);
            LS_tblCountryViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblCountryCreate", model);
        }
        #endregion
        #region  Danh muc Province - Tinh thanh
        // tra ve list danh muc
        public ActionResult LS_tblProvince()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblProvinceRepository _repository = new LS_tblProvinceRepository(db);
            IList<LS_tblProvinceViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblProvince", sources);
        }
        public ActionResult _CreateLS_tblProvince()
        {
            LS_tblProvinceViewModel model = new LS_tblProvinceViewModel();
            model.Used = true;
            return PartialView("_LS_tblProvinceCreate", model);
        }
        public ActionResult _EditLS_tblProvince(int id)
        {
            LS_tblProvinceRepository _repository = new LS_tblProvinceRepository(db);
            LS_tblProvinceViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblProvinceCreate", model);
        }
        #endregion
        #region Danh muc district - quan huyen
        // tra ve list danh muc
        public ActionResult LS_tblDistrict()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblDistrictRepository _repository = new LS_tblDistrictRepository(db);
            IList<LS_tblDistrictViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblDistrict", sources);
        }
        public ActionResult _CreateLS_tblDistrict()
        {
            LS_tblDistrictViewModel model = new LS_tblDistrictViewModel();
            model.Used = true;
            return PartialView("_LS_tblDistrictCreate", model);
        }
        public ActionResult _EditLS_tblDistrict(int id)
        {
            LS_tblDistrictRepository _repository = new LS_tblDistrictRepository(db);
            LS_tblDistrictViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblDistrictCreate", model);
        }
       
        #endregion
        #region  Danh muc School - trường
        // tra ve list danh muc
        public ActionResult LS_tblSchool()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblSchoolRepository _repository = new LS_tblSchoolRepository(db);
            IList<LS_tblSchoolViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblSchool", sources);
        }
        public ActionResult _CreateLS_tblSchool()
        {
            LS_tblSchoolViewModel model = new LS_tblSchoolViewModel();
            model.Used = true;
            return PartialView("_LS_tblSchoolCreate", model);
        }
        public ActionResult _EditLS_tblSchool(int id)
        {
            LS_tblSchoolRepository _repository = new LS_tblSchoolRepository(db);
            LS_tblSchoolViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblSchoolCreate", model);
        }
       

        #endregion
        #region  Danh muc Marital - Tinh trang hon nhan
        // tra ve list danh muc
        public ActionResult LS_tblMarital()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblMaritalRepository _repository = new LS_tblMaritalRepository(db);
            IList<LS_tblMaritalViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblMarital", sources);
        }

        public ActionResult _CreateLS_tblMarital()
        {
            LS_tblMaritalViewModel model = new LS_tblMaritalViewModel();
            model.Used = true;
            return PartialView("_LS_tblMaritalCreate", model);
        }
        public ActionResult _EditLS_tblMarital(int id)
        {
            LS_tblMaritalRepository _repository = new LS_tblMaritalRepository(db);
            LS_tblMaritalViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblMaritalCreate", model);
        }
        #endregion
        #region  Danh muc Faculty - Khoa
        // tra ve list danh muc
        public ActionResult LS_tblFaculty()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblFacultyRepository _repository = new LS_tblFacultyRepository(db);
            IList<LS_tblFacultyViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblFaculty", sources);
        }

        public ActionResult _CreateLS_tblFaculty()
        {
            LS_tblFacultyViewModel model = new LS_tblFacultyViewModel();
            model.Used = true;
            return PartialView("_LS_tblFacultyCreate", model);
        }
        public ActionResult _EditLS_tblFaculty(int id)
        {
            LS_tblFacultyRepository _repository = new LS_tblFacultyRepository(db);
            LS_tblFacultyViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblFacultyCreate", model);
        }
        #endregion
        #region  Danh muc Major - chuyen nganh
        // tra ve list danh muc
        public ActionResult LS_tblMajor()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblMajorRepository _repository = new LS_tblMajorRepository(db);
            IList<LS_tblMajorViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblMajor", sources);
        }

        public ActionResult _CreateLS_tblMajor()
        {
            LS_tblMajorViewModel model = new LS_tblMajorViewModel();
            model.Used = true;
            return PartialView("_LS_tblMajorCreate", model);
        }
        public ActionResult _EditLS_tblMajor(int id)
        {
            LS_tblMajorRepository _repository = new LS_tblMajorRepository(db);
            LS_tblMajorViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblMajorCreate", model);
        }
        #endregion
        #region  Danh muc Nationality - quoc tich
        // tra ve list danh muc
        public ActionResult LS_tblNationality()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblNationalityRepository _repository = new LS_tblNationalityRepository(db);
            IList<LS_tblNationalityViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblNationality", sources);
        }

        public ActionResult _CreateLS_tblNationality()
        {
            LS_tblNationalityViewModel model = new LS_tblNationalityViewModel();
            model.Used = true;
            return PartialView("_LS_tblNationalityCreate", model);
        }
        public ActionResult _EditLS_tblNationality(int id)
        {
            LS_tblNationalityRepository _repository = new LS_tblNationalityRepository(db);
            LS_tblNationalityViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblNationalityCreate", model);
        }
        #endregion
        #region  Danh muc Ethnic - dan toc
        // tra ve list danh muc
        public ActionResult LS_tblEthnic()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblEthnicRepository _repository = new LS_tblEthnicRepository(db);
            IList<LS_tblEthnicViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblEthnic", sources);
        }

        public ActionResult _CreateLS_tblEthnic()
        {
            LS_tblEthnicViewModel model = new LS_tblEthnicViewModel();
            model.Used = true;
            return PartialView("_LS_tblEthnicCreate", model);
        }
        public ActionResult _EditLS_tblEthnic(int id)
        {
            LS_tblEthnicRepository _repository = new LS_tblEthnicRepository(db);
            LS_tblEthnicViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblEthnicCreate", model);
        }
        #endregion
        #region  Danh muc Religion - ton giao
        // tra ve list danh muc
        public ActionResult LS_tblReligion()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblReligionRepository _repository = new LS_tblReligionRepository(db);
            IList<LS_tblReligionViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblReligion", sources);
        }

        public ActionResult _CreateLS_tblReligion()
        {
            LS_tblReligionViewModel model = new LS_tblReligionViewModel();
            model.Used = true;
            return PartialView("_LS_tblReligionCreate", model);
        }
        public ActionResult _EditLS_tblReligion(int id)
        {
            LS_tblReligionRepository _repository = new LS_tblReligionRepository(db);
            LS_tblReligionViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblReligionCreate", model);
        }
        #endregion
        #region  Danh muc Education - Trinh do hoc van
        // tra ve list danh muc
        public ActionResult LS_tblEducation()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblEducationRepository _repository = new LS_tblEducationRepository(db);
            IList<LS_tblEducationViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblEducation", sources);
        }

        public ActionResult _CreateLS_tblEducation()
        {
            LS_tblEducationViewModel model = new LS_tblEducationViewModel();
            model.Used = true;
            return PartialView("_LS_tblEducationCreate", model);
        }
        public ActionResult _EditLS_tblEducation(int id)
        {
            LS_tblEducationRepository _repository = new LS_tblEducationRepository(db);
            LS_tblEducationViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblEducationCreate", model);
        }
        #endregion
        #region  Danh muc Qualification - bang cap
        // tra ve list danh muc
        public ActionResult LS_tblQualification()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblQualificationRepository _repository = new LS_tblQualificationRepository(db);
            IList<LS_tblQualificationViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblQualification", sources);
        }

        public ActionResult _CreateLS_tblQualification()
        {
            LS_tblQualificationViewModel model = new LS_tblQualificationViewModel();
            model.Used = true;
            return PartialView("_LS_tblQualificationCreate", model);
        }
        public ActionResult _EditLS_tblQualification(int id)
        {
            LS_tblQualificationRepository _repository = new LS_tblQualificationRepository(db);
            LS_tblQualificationViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblQualificationCreate", model);
        }
        #endregion
        #region Danh muc Bank - Ngan hang
        // tra ve list danh muc Level1
        public ActionResult LS_tblBank()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            IList<LS_tblBankViewModel> sources = _repository.List().ToList();
            return PartialView("_LS_tblBank", sources);
        }
        public ActionResult _CreateLS_tblBank()
        {
            LS_tblBankViewModel model = new LS_tblBankViewModel();
            model.Used = true;
            return PartialView("_LS_tblBankCreate", model);
        }
        public ActionResult _EditLS_tblBank(int id)
        {
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            LS_tblBankViewModel model = _repository.FindEditModel(id);

            return PartialView("_LS_tblBankCreate", model);
        }
        #endregion
    }
}
