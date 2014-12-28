using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingApprisalPartViewModel : LS_tblTrainingApprisalPart
    {        
        public string InitialTrainingApprisalPart { get; set; }      

        public string UsedInfo
        {
            get
            {
                return this.Used.Value == true ? "X" : String.Empty;
            }
        }
        
    }
}
