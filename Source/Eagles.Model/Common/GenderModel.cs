using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eagle.Model
{
    public class GenderModel
    {
        //<%= Html.DropDownListFor(x => x.GenderId, Model.GenderName) %>
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }

    public class GenderListModel
    {
        public List<GenderModel> GenderList { get; set; }
        public GenderListModel(int LanguageId)
        {
            GenderList = new List<GenderModel>()
                {
                    new GenderModel(){ GenderId = 0, GenderName = Resource.LanguageResource.Male },
                    new GenderModel(){ GenderId = 1, GenderName = Resource.LanguageResource.Female },
                    new GenderModel(){ GenderId = -1, GenderName = Resource.LanguageResource.NonSpecified }
                };
        }
    }
}
