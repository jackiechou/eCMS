using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class PageSelectViewModel
    {
        public int PageId { get; set; }
        public int MenuId { get; set; }
        public string PageName { get; set; }
        public bool isSelected { get; set; }
    }
}
