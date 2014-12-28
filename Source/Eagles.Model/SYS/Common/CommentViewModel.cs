using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class CommentViewModel
    {
        public DateTime? Time { get; set; }
        public string Comment { get; set; }
        public string StatusName { get; set; }
        public string EmpComment { get; set; }
        public int? AvatarId { get; set; }
    }
}
