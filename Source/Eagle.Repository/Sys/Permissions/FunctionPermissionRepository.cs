using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
using Eagle.Model.SYS;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Data;
using Eagle.Common.Data;
using Eagle.Model.SYS.Permission;
using Eagle.Repository.SYS.Roles;

namespace Eagle.Repository
{
    public class FunctionPermissionRepository
    {
        public EntityDataContext context { get; set; }
        public FunctionPermissionRepository(EntityDataContext context)
        {
            this.context = context;
        }

        //public MasterDataPermissionViewModel CheckMasterDataPermission(int? UserId, int? LanguageId, int? MasterDataID)
        //{
        //    MasterDataPermissionViewModel entity = new MasterDataPermissionViewModel();
        //    if (UserId != null)
        //    {
        //        List<MasterDataPermissionViewModel> lst = new List<MasterDataPermissionViewModel>();
        //        List<int> GroupList = UserRoleRepository.GetGroupListByUserID(UserId);
        //        lst = (from f in context.LS_tblMasterData
        //               join p in context.SYS_tblFunctionPermission
        //               on f.ID equals p.MasterDataID
        //               where p.MasterDataID != null &&
        //              (UserId == null || GroupList.Contains(p.GroupID)) && p.FView == true
        //               select new MasterDataPermissionViewModel
        //               {
        //                   //PermissionID = p.PermissionID,
        //                   //GroupID = p.GroupID,                       
        //                   //ModuleID = p.ModuleID,
        //                   //MasterDataID = p.MasterDataID,
        //                   //MasterDataName = (LanguageId != null && LanguageId == 124) ? f.Name : f.VNName,
        //                   FExport = p.FExport == true,
        //                   FDelete = p.FDelete == true,
        //                   FView = p.FView == true,
        //                   FEdit = p.FEdit == true
        //               }).ToList();
        //        if (MasterDataID != null)
        //            entity = lst.Where(p => p.MasterDataID == MasterDataID).FirstOrDefault();
        //    }
        //    return entity;
        //}


        public string CheckMasterDataPermissionResult(int? UserID, int MasterDataID)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();            
            string result = string.Empty, s = string.Empty;
            string sp = "[dbo].[SYS_spfrmCheckMasterDataPermission]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserID", UserID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@MasterDataID", MasterDataID));
            DataTable dt = sqlHandler.ExecuteAsDataSet(sp, ParamCollInput).Tables[0];
            if (dt.Rows.Count > 0)
            {
               
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dict.Add("FView", dt.Rows[0]["FView"].ToString());
                dict.Add("FEdit", dt.Rows[0]["FEdit"].ToString());
                dict.Add("FDelete", dt.Rows[0]["FDelete"].ToString());
                dict.Add("FExport", dt.Rows[0]["FExport"].ToString());
                s = serializer.Serialize(dict) + ",";
                result = s.Remove(s.LastIndexOf(","), 1);
            }
            return result;
        }

        public PermissionSettingViewModel CheckMasterDataPermission(int? UserID, int MasterDataID)
        {
            PermissionSettingViewModel entity = new PermissionSettingViewModel();
            string sp = "[dbo].[SYS_spfrmCheckMasterDataPermission]";
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserID", UserID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@MasterDataID", MasterDataID));
            DataTable dt = sqlHandler.ExecuteAsDataSet(sp, ParamCollInput).Tables[0];
            if (dt.Rows.Count > 0)
            {
                entity.View = (!string.IsNullOrEmpty(dt.Rows[0]["FView"].ToString()) && dt.Rows[0]["FView"].ToString().ToLower() == "true")? true : false ;
                entity.Edit = (!string.IsNullOrEmpty(dt.Rows[0]["FEdit"].ToString()) && dt.Rows[0]["FEdit"].ToString().ToLower() == "true")? true : false ;
                entity.Delete = (!string.IsNullOrEmpty(dt.Rows[0]["FDelete"].ToString()) && dt.Rows[0]["FDelete"].ToString().ToLower() == "true")? true : false ;
                entity.Export = (!string.IsNullOrEmpty(dt.Rows[0]["FExport"].ToString()) && dt.Rows[0]["FExport"].ToString().ToLower() == "true")? true : false ;
            }
            return entity;
        }
      

        public List<FunctionListViewModel> GetFunctionListByModuleIDAndUserID(int ModuleID, int? UserID, int? LanguageId)
        {
            List<FunctionListViewModel> lst = new List<FunctionListViewModel>();
            if (UserID != null)
            {
                List<int> GroupList = UserRoleRepository.GetGroupListByUserID(UserID);
                lst = (from f in context.SYS_tblFunctionList
                       join p in context.SYS_tblFunctionPermission
                       on f.FunctionID equals p.FunctionID
                       where GroupList.Contains(p.GroupID) && p.ModuleID == ModuleID && p.FView == true
                       select new FunctionListViewModel
                       {
                           FunctionID = f.FunctionID,
                           FunctionName = (LanguageId != null && LanguageId == 10002) ? f.FunctionNameV : f.FunctionNameE,
                           FunctionNameE = f.FunctionNameE,
                           FunctionNameV = f.FunctionNameV,
                           ModuleID = f.ModuleID,
                           Url = f.Url,
                           FExport = p.FExport == true,
                           FDelete = p.FDelete == true,
                           FView = p.FView == true,
                           FEdit = p.FEdit == true
                       }).ToList();
            }
            return lst;
        }

        #region Bind DropdownList
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = context.SYS_tblFunctionList
                           .Where(c => (c.ModuleID.Contains(searchTerm) || c.AscxParent.Contains(searchTerm)) && c.Parent ==  null && c.Display == true)
                           .OrderBy (c => c.ModuleID)
                           .Select(c => new
                           {
                               id = c.FunctionID,
                               name = c.ModuleID + " -- " + c.AscxParent ,
                               text = ""
                           });
            List<AutoCompleteViewModel> ret = new List<AutoCompleteViewModel>();
            foreach (var item in tmp)
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = item.id.ToString(),
                    name = item.name,
                    text = item.text
                };
                ret.Add(p);
            }
            return ret;
        }
        // dùng cho bind dropdownlist
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            return GetDropdownList(searchTerm).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
    }
}
