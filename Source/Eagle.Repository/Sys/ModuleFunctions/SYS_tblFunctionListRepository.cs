using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;

namespace Eagle.Repository
{
    public class SYS_tblFunctionListRepository
    {
        public EntityDataContext context { get; set; }
        public SYS_tblFunctionListRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<FunctionListViewModel> GetAllModules(int LanguageId)
        {
            if (LanguageId == 124)
            {
                return context.SYS_tblFunctionList
                    .Where(p => p.Parent == null)
                    .Select(p => new FunctionListViewModel() { FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameE })
                    .ToList();
            }
            else
            {
                return context.SYS_tblFunctionList
                    .Where(p => p.Parent == null)
                    .Select(p => new FunctionListViewModel() { FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameV })
                    .ToList();
            }
        }
    }
}
