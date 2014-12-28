using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    public class PageModuleViewModel
    {
        public int PageModuleId { get; set; }
        public int PageId { get; set; }
        public int ModuleId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Alignment")]
        public string Alignment { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Border")]
        public string Border { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Color")]
        public string Color { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Icon")]
        public Nullable<int> Icon { get; set; }
        public Color SelectedColor { get; set; }
        public Nullable<int> ModuleOrder { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Pane")]
        public string Pane { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsVisible")]
        public Nullable<bool> IsVisible { get; set; }
    }
}
