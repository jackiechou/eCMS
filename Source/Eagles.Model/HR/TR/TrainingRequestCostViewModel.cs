using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Model
{
    public class TrainingRequestCostViewModel : TR_tblTrainingRequestCost
    {
        public Nullable<int> LSTrainingExpenseIDAllowNull { get; set; }
        public Nullable<int> LSCurrencyIDAllowNull { get; set; }
        public string TrainingExpenseName { get; set; }
        public string TrainingCurrencyName { get; set; }
    }
}
