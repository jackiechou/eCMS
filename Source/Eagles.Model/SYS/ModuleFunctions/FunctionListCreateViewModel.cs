using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Eagle.Model
{
    public class FunctionListCreateViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FunctionModel_FunctionID")]
        public string FunctionID;

        public Nullable<int> Parent { get; set; }
        public string FunctionNameV { get; set; }
        public string FunctionNameE { get; set; }
        public string ModuleID { get; set; }
        public string Url { get; set; }
        public string Ascx { get; set; }
        public Nullable<byte> Rank { get; set; }
        public Nullable<bool> Display { get; set; }
        public string Left_Horizontal { get; set; }
        public string cssClass { get; set; }
        public string TitleV { get; set; }
        public string TitleE { get; set; }
        public Nullable<bool> HaveSetup { get; set; }
        public Nullable<int> FunctionDefault { get; set; }
        public Nullable<byte> MenuLevel { get; set; }
        public Nullable<int> LevelParent { get; set; }
        public string LookId { get; set; }
        public string AscxParent { get; set; }
        public string Tooltips { get; set; }
        public string strTypeTable { get; set; }

        public bool FView { get; set; }
        public bool FEdit { get; set; }
        public bool FDelete { get; set; }
        public bool FExport { get; set; }

        
    }
}
