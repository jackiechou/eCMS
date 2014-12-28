using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class RecruitmentSourceSelectedViewModel
    {
        public int LSRecruitmentSourceID { get; set; }
        public string LSRecruitmentSourceName { get; set; }
        public bool isSelected { get; set; }
    }

    public class SelectedViewModel
    {
        public int ID { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public bool isSelected { get; set; }
    }
}
