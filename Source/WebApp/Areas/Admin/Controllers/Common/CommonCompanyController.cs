using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class TreeView
    {
        public int id { get; set; }
        public int? parentid { get; set; }
        public string text { get; set; }
        public int? companydefineid { get; set; }
        public List<TreeView> children { get; set; }
    }
    public class CommonCompanyController : BaseController
    {
        public JsonResult GetAllCompanyWithPlease()
        {
            var list = db.LS_tblCompany.Select(p => new TreeView() { id = p.LSCompanyID, text = LanguageId == 124 ? p.Name : p.VNName, parentid = p.Parent }).ToList();
            list.Insert(0, new TreeView() { id = 0, text = String.Format(Eagle.Resource.LanguageResource.PleaseSelect0, Eagle.Resource.LanguageResource.LastCompanyDefine), parentid = -1 });

            var parentList = list.Where(x => x.parentid == null || x.parentid == 0 || x.parentid == -1).ToList();            
            return base.Json(parentList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllWithPlease()
        {
            var list = db.LS_tblCompany.Select(p => new TreeView() { id = p.LSCompanyID, text = LanguageId == 124 ? p.Name : p.VNName, parentid = p.Parent }).ToList();
            list.Insert(0, new TreeView() { id = 0, text = String.Format(Eagle.Resource.LanguageResource.PleaseSelect0, Eagle.Resource.LanguageResource.LastCompanyDefine), parentid = -1 });

            var parentList = list.Where(x => x.parentid == null || x.parentid == 0 || x.parentid == -1).ToList();

            foreach (var parentEntity in parentList)
            {
                BindHiearchialData(parentEntity, list);
            }

            return base.Json(parentList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllWithSelectAll()
        {
            var list = db.LS_tblCompany.Select(p => new TreeView() { id = p.LSCompanyID, text = LanguageId == 124 ? p.Name : p.VNName, parentid = p.Parent }).ToList();
            list.Insert(0, new TreeView() { id = 0, text = Eagle.Resource.LanguageResource.SelectAll, parentid = -1 });

            var parentList = list.Where(x => x.parentid == null || x.parentid == 0 || x.parentid == -1).ToList();            

            foreach (var parentEntity in parentList)
            {
                BindHiearchialData(parentEntity, list);
            }

            return base.Json(parentList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var list = db.LS_tblCompany.Select(p => new TreeView() { id = p.LSCompanyID, text = LanguageId == 124 ? p.Name : p.VNName, parentid = p.Parent }).ToList();
            

            var parentList = list.Where(x => x.parentid == null || x.parentid == 0).ToList();

            foreach (var parentEntity in parentList)
            {
                BindHiearchialData(parentEntity, list);
            }

            return base.Json(parentList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNearLast()
        {
            List<int?> allDefine = db.LS_tblCompanyDefine.Select(p => p.Parent).ToList();
            var lastDefine = db.LS_tblCompanyDefine.Where(p => !allDefine.Contains(p.LSCompanyDefineID)).FirstOrDefault();

            if (lastDefine != null)
            {
                var list = db.LS_tblCompany
                    .Where(p => p.LSCompanyDefineID != lastDefine.LSCompanyDefineID)
                    .Select(p => new TreeView() { id = p.LSCompanyID, text = LanguageId == 124 ? p.Name : p.VNName, parentid = p.Parent, companydefineid = 5 }).ToList();
                var parentList = list.Where(x => x.parentid == null || x.parentid == 0).ToList();

                foreach (var parentEntity in parentList)
                {
                    BindHiearchialData(parentEntity, list);
                }

                return base.Json(parentList, JsonRequestBehavior.AllowGet);
            }

            return base.Json("", JsonRequestBehavior.AllowGet); ;

        }
        public JsonResult GetTo(int Parent)
        {
            
                var list = db.LS_tblCompany
                    .Where(p => p.LS_tblCompanyDefine.Parent < Parent)
                    .Select(p => new TreeView() { id = p.LSCompanyID, text = LanguageId == 124 ? p.Name : p.VNName, parentid = p.Parent }).ToList();
                var parentList = list.Where(x => x.parentid == null || x.parentid == 0).ToList();

                foreach (var parentEntity in parentList)
                {
                    BindHiearchialData(parentEntity, list);
                }

                return base.Json(parentList, JsonRequestBehavior.AllowGet);
            

        }

        public JsonResult CheckLSCompanyIDValid(int LSCompanyDefineID, int? LSCompanyID)
        {
            if (LSCompanyID == null || LSCompanyID == 0)
            {
                //Nếu tạo mới tổng công ty => không cần check
                return base.Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var define = db.LS_tblCompanyDefine.Find(LSCompanyDefineID);

                var result = db.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID && p.LSCompanyDefineID == define.Parent).FirstOrDefault();
                if (result != null)
                {
                    return base.Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string errorMessage = string.Format(Eagle.Resource.LanguageResource.ANotIsB
                        , Eagle.Resource.LanguageResource.ParentId, LanguageId == 124 ? define.Name : define.VNName);
                    return base.Json(errorMessage, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public static void BindHiearchialData(TreeView entity, List<TreeView> list)
        {
            if (entity.children == null)
            {
                entity.children = new List<TreeView>();
            }
            List<TreeView> children = list.Where(p => p.parentid == entity.id).Select(
                p => new TreeView
                {
                    id = p.id,
                    text = p.text,
                    parentid = p.parentid
                }).ToList();

            if (children.Count > 0)
            {
                foreach (var child in children)
                {
                    BindHiearchialData(child, list);
                    entity.children.Add(child);
                }
            }
        }

    }
}
