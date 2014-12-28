using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Content
{
    public class ContentItemViewModel
    {
        public Nullable<int> PageId { get; set; }
        public Nullable<int> ModuleId { get; set; }
        public int ContentTypeId { get; set; }
        public int ContentItemId { get; set; }
        public System.Guid ContentItemCode { get; set; }
        public string Content { get; set; }
        public string ContentKey { get; set; }
        public bool Indexed { get; set; }

        public string PageName { get; set; }
        public string ModuleName { get; set; }

    }
}
