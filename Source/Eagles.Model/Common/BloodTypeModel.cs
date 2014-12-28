using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.Common
{
    public class BloodTypeModel
    {
        public string BloodTypeId { get; set; }
        public string BloodTypeName { get; set; }
    }

    public class BloodTypeListModel
    {
        public List<BloodTypeModel> BloodTypeList { get; set; }
        public BloodTypeListModel()
        {
            BloodTypeList = new List<BloodTypeModel>()
                {
                    new BloodTypeModel(){ BloodTypeId = "A", BloodTypeName = Resource.LanguageResource.GroupA },
                    new BloodTypeModel(){ BloodTypeId = "B", BloodTypeName = Resource.LanguageResource.GroupB },
                    new BloodTypeModel(){ BloodTypeId = "AB", BloodTypeName = Resource.LanguageResource.GroupAB },
                    new BloodTypeModel(){ BloodTypeId = "O", BloodTypeName = Resource.LanguageResource.GroupO }
                };
        }
    }
}
