using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Core;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Eagle.Common.Utilities;
namespace Eagle.Model
{
    public class ScaleCreateViewModels : PR_tblSalaryScale
    {
        public string strUsed
        {
            get {
                return (Used == true ? "x" : "");
            }
        }
    }
}
