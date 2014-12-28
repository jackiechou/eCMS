using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
namespace Eagle.Repository
{
    /// <summary>
    /// Hiển thị danh sách các danh mục trong hệ thống theo từng phân hệ chức năng : HR, TS, TER....
    /// </summary>
    public class MasterDataRepository
    {
        public EntityDataContext context { get; set; }
        public MasterDataRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<MasterDataViewModel> ListMaster(string strModule, int LanguageId)
        {
            try
            {

                if (LanguageId == 124)
                {
                    return context.LS_tblMasterData
                        .Where(p => p.Module.Contains(strModule) || p.Display == 1)
                        .Select(p => new MasterDataViewModel()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            TableName = p.TableName,
                            Module = p.Module
                        }).ToList();
                }
                else
                {
                    return context.LS_tblMasterData
                        .Where(p => p.Module.Contains(strModule) || p.Display == 1)
                        .Select(p => new MasterDataViewModel()
                        {
                            ID = p.ID,
                            Name = p.VNName,
                            TableName = p.TableName,
                            Module = p.Module
                        }).ToList();
                }
            }
            catch
            {
                return new List<MasterDataViewModel>();
            }
        }
    }
}

