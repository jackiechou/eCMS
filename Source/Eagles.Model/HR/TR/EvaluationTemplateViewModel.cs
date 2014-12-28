using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Model
{
    public class EvaluationTemplateViewModel : TR_tblEvaluationTemplate
    {
        public List<EvaluationTemplateDetailViewModel> ListOfTemplateDetail = new List<EvaluationTemplateDetailViewModel>();        

        public string InitialEvaluationTemplate { get; set; }        
        
    }
}
