using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Model
{
    public class OnlineProcessEmpViewModel : SYS_tblOnlineProcessEmp
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string strEmpName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NameProcessOnline")]
        public string OnlineProcessName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public string CompanyName { get; set; }
        public int RootLevel { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Approve")]
        public string Approve { get {
            string ret = "";
            if (ApproveLevel1 == true)
            {
                ret += "1";
            }
            if (ApproveLevel2 == true)
            {
                if (ret != "")
                {
                    ret += ",";
                }
                ret += "2";
            }
            if (ApproveLevel3 == true)
            {
                if (ret != "")
                {
                    ret += ",";
                }
                ret += "3";
            } 
            if (ApproveLevel4 == true)
            {
                if (ret != "")
                {
                    ret += ",";
                }
                ret += "4";
            } 
            if (ApproveLevel5 == true)
            {
                if (ret != "")
                {
                    ret += ",";
                }
                ret += "5";
            }

            ret += ".";
            return ret;
        } }
    }
}
