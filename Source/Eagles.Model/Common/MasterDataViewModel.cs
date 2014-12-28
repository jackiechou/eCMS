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
    public class MasterDataViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string VNName { get; set; }
        public string Module { get; set; }
        public string TableName { get; set; }
        public Nullable<int> Rank { get; set; }
        public string Note { get; set; }
        public Nullable<int> Display { get; set; }
    }
}
