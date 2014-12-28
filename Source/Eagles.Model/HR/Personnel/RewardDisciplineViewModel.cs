using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Eagle.Model.HR
{
    #region Training Plcae ==============================================================================
    public class RewardDisciplineTypeModel
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }

    public class RewardDisciplineTypeModelList
    {
        public List<RewardDisciplineTypeModel> TypeList { get; set; }
        public RewardDisciplineTypeModelList(int LanguageId)
        {
            if (LanguageId == 124)
            {
                TypeList = new List<RewardDisciplineTypeModel>()
                {
                    new RewardDisciplineTypeModel(){ TypeId = 1, TypeName = "Reward" },
                    new RewardDisciplineTypeModel(){ TypeId = 2, TypeName = "Discipline" }
                };
            }
            else
            {
                TypeList = new List<RewardDisciplineTypeModel>()
                {
                    new RewardDisciplineTypeModel(){ TypeId = 1, TypeName = "Khen thưởng" },
                    new RewardDisciplineTypeModel(){ TypeId = 2, TypeName = "Kỷ luật" }
                };
            }
        }
    }
    #endregion Training Plcae =========================================================================
    public class RewardDisciplineViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RewardID")]
        public int RewardID { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]

        public int EmpID { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
        public string EmpName { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]

        public int Type { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSRewardID")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]

        public int LSRewardID { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSRewardName")]
        public string LSRewardName { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSRewardLevelID")]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]

        public int LSRewardLevelID { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSRewardLevelName")]
        public string LSRewardLevelName { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDate")]
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
         [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
         public System.DateTime JoinDate { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
         [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
         [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
         public System.DateTime EffectiveDate { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Reason"), StringLength(200)]
         //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
         public string Reason { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Decision")]
        public Nullable<bool> Decision { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DecisionNo"), StringLength(50)]
        public string DecisionNo { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SignedDate")]
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
         [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
         public Nullable<System.DateTime> SignedDate { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SignerEmpID")]
        public Nullable<int> SignerEmpID { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileId")]
        public Nullable<int> FileId { get; set; }

         public HttpPostedFileBase FileUpload { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note"), StringLength(200)]
        public string Note { get; set; }
      

        public virtual HR_tblEmp HR_tblEmp { get; set; }
        public virtual LS_tblReward LS_tblReward { get; set; }
        public virtual LS_tblRewardLevel LS_tblRewardLevel { get; set; }
    }
}
