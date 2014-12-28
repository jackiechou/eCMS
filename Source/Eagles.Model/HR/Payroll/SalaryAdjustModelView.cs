using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SalaryAdjustModelView : LS_tblSalaryAdjust
    {
        /// <summary>
        /// 
        /// </summary>
        public string InitialSalaryAdjust { get { return this.LSSalaryAdjustCode; } }

        /// <summary>
        /// 
        /// </summary>
        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AddSalaryInfo
        {
            get
            {
                return this.AddSalary == true ? "X" : String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TypeAddInfo
        {
            get
            {
                Dictionary<int, string> result = this.GetListOfTypeAdd();
                if (result.ContainsKey(this.TypeAdd))
                {
                    return result[this.TypeAdd];
                }
                return String.Empty;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PITInfo
        {
            get
            {
                return this.PIT == true ? "X" : String.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeInfo
        {
            get
            {
                Dictionary<int, string> result = this.GetListOfType();
                if (result.ContainsKey(this.Type))
                {
                    return result[this.Type];
                }
                return String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MaxNoPITInfo
        {
            get
            {
                if (this.MaxNonPIT.HasValue)
                {
                    return this.MaxNonPIT.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetListOfTypeAdd()
        {
            Dictionary<int, string> result = new Dictionary<int,string>();
            result.Add(1, Eagle.Resource.LanguageResource.PR_TYPEADD_ITEM_1);
            result.Add(2, Eagle.Resource.LanguageResource.PR_TYPEADD_ITEM_2);
            result.Add(3, Eagle.Resource.LanguageResource.PR_TYPEADD_ITEM_3);
            result.Add(4, Eagle.Resource.LanguageResource.PR_TYPEADD_ITEM_4);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetListOfType()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            result.Add(1, Eagle.Resource.LanguageResource.PR_TYPE_ITEM_1);
            result.Add(2, Eagle.Resource.LanguageResource.PR_TYPE_ITEM_2);
            return result;
        }        
    }
}
