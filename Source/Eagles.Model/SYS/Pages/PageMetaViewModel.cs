using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    public class PageMetaViewModel
    {
        public Nullable<System.Guid> PageCode { get; set; }
        public int MetaId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
        public string Title { get; set; }
    }
}
