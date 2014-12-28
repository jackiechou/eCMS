using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Model
{
    public class TrainingPlanExpenseModelView : TR_tblTrainingPlanExpense
    {
        public string TrainingExpenseName { get; set; }
        public string TrainingCurrencyName { get; set; }        
    }
}
