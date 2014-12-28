using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagles.Model
{
    public class ChangingInsuranceInformationMasterViewModel : SI_tblChangingInsuranceInformationMaster
    {
        public string strIsNotified { get { return isNotified == true ? "X" : ""; } }
    }
}
