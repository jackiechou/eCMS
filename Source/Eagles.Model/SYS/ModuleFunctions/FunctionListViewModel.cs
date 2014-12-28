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
    public class FunctionEntity
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int Depth { get; set; }

        public string DisplayName { get; set; }

        public IList<FunctionEntity> FunctionEntities { set; get; }

        //public IEnumerable<SelectListItem> FunctionItems
        //{
        //    get { return new SelectList(FunctionEntities, "Id", "Name"); }
        //}

        //public IEnumerable<SelectListItem> FunctionItems
        //{
        //    get
        //    {
        //        var listItems = FunctionEntities.Select(x => new SelectListItem
        //        {
        //            Value = x.Id.ToString(),
        //            Text = x.Name
        //        });
        //        return listItems;
        //        //return DefaultItem.Concat(listItems);
        //    }
        //}
       
        //public IEnumerable<SelectListItem> DefaultItem
        //{
        //    get
        //    {
        //        return Enumerable.Repeat(new SelectListItem
        //        {
        //            Value = "-1",
        //            Text = "Select an item"
        //        }, count: 1);
        //    }
        //}
        public FunctionEntity()
        {
            FunctionEntities = new List<FunctionEntity>();
        }
    }

    public class FunctionListViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FunctionModel_FunctionID")]
        public int FunctionID { get; set; }
        
        public Nullable<int> Parent { get; set; }

        public string FunctionName { get; set; }
        public string FunctionNameV { get; set; }
        public string FunctionNameE { get; set; }
        public string ModuleID { get; set; }
        public string Url { get; set; }
        public string Ascx { get; set; }
        public Nullable<int> Rank { get; set; }
        public Nullable<bool> Display { get; set; }
        public string Left_Horizontal { get; set; }
        public string cssClass { get; set; }
        public string TitleV { get; set; }
        public string TitleE { get; set; }
        public Nullable<bool> HaveSetup { get; set; }
        public Nullable<int> FunctionDefault { get; set; }
        public int MenuLevel { get; set; }
        public Nullable<int> LevelParent { get; set; }
        public string LookId { get; set; }
        public string AscxParent { get; set; }
        public string Tooltips { get; set; }

        public bool FView { get; set; }
        public bool FEdit { get; set; }
        public bool FDelete { get; set; }
        public bool FExport { get; set; }
    }
}
