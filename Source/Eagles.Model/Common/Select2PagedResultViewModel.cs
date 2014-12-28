using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Model
{
    public class Select2PagedResultViewModel
    {
        public int Total { get; set; }
        public List<Select2ResultViewModel> Results { get; set; }
    }
}
