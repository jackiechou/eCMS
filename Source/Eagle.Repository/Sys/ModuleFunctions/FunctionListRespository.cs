using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.Common;

namespace Eagle.Repository.SYS
{
    public class FunctionListRespository
    {
        public EntityDataContext context { get; set; }
        public FunctionListRespository(EntityDataContext context)
        {
            this.context = context;
        }

        public static List<FunctionEntity> PopulateHiearchialDataToDropDownList(int? LanguageId)
        {
            List<FunctionEntity> list = new List<FunctionEntity>();
            using (EntityDataContext context = new EntityDataContext())
            {
                if (LanguageId == 124)
                {
                    list = (context.SYS_tblFunctionList.Select(t => new FunctionEntity()
                    {
                        Id = t.FunctionID,
                        ParentId = t.Parent,                        
                        Depth = t.MenuLevel,
                        Name = t.TitleE,
                        DisplayName = String.Concat(Enumerable.Repeat("-", 4 * t.MenuLevel)) + " " + t.TitleE
                    })).ToList();
                }
                else
                {
                    list = (context.SYS_tblFunctionList.Select(t => new FunctionEntity()
                    {
                        Id = t.FunctionID,
                        ParentId = t.Parent,                       
                        Depth = t.MenuLevel,
                        Name = t.TitleV,
                        DisplayName = String.Concat(Enumerable.Repeat("-", 4 * t.MenuLevel)) + " " + t.TitleV
                    })).ToList();
                }
            }

            var parentList = list.Where(x => x.ParentId == null || x.ParentId == 0).ToList();
            
            foreach (var parentEntity in parentList)
            {
                BindHiearchialData(parentEntity, list);
            }
            return parentList;
        }

        public static void BindHiearchialData(FunctionEntity entity, List<FunctionEntity> list)
        {
            List<FunctionEntity> children = list.Where(x => x.ParentId == entity.Id).Select(
                x => new FunctionEntity { Id = x.Id, Name = x.DisplayName, ParentId = x.ParentId, Depth = x.Depth }).ToList();

            if (children.Count >0)
            {
                foreach (var child in children)
                {                   
                    BindHiearchialData(child, children);
                    entity.FunctionEntities.Add(child);
                }
            }
        }


        //public void GenearateHiearchicalNodes()
        //{
        //    // Make sure we get everything in a sensible order, parents before children
        //    var query = context.SYS_tblFunctionList.OrderBy(x => x.MenuLevel);

        //    var root = new TreeNode("Root", 0);
        //    foreach (var node in query)
        //    {       
        //        //var current = root;
        //        ////string part = node.HierarchyPath.Split(new[] {'/'},StringSplitOptions.RemoveEmptyEntries);
        //        //current = current.Children[]
        //        //foreach ()
        //        //{
        //        //    int parsedPart = int.Parse(part);
        //        //    current = current.Children[parsedPart - 1];
        //        //}
        //        current.Children.Add(new TreeNode(node.Name, node.Id));
        //    }
        //}

        //public static int GenerateRank()
        //{
        //    using (EntityDataContext context = new EntityDataContext())
        //    {
        //        int Rank_ID = (from u in context.SYS_tblFunctionList select u.Rank).DefaultIfEmpty(0).Max() + 1;
        //        return Rank_ID;
        //    }            
        //}


        public static bool IsIdExisted(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var result = context.SYS_tblFunctionList.FirstOrDefault(p => p.FunctionID.Equals(id));
                return (result != null);
            }
        }

        public IEnumerable<FunctionListViewModel> List()
        {
            List<FunctionListViewModel> lst = new List<FunctionListViewModel>();           
            lst = (from p in context.SYS_tblFunctionList
                        select new FunctionListViewModel()
                        {
                            FunctionID = p.FunctionID,
                            Parent = p.Parent,
                            FunctionNameE = p.FunctionNameE,
                            FunctionNameV = p.FunctionNameV,
                            Rank = p.Rank,
                            ModuleID = p.ModuleID,
                            Url = p.Url,
                            Ascx = p.Ascx,
                            Display = p.Display,
                            Left_Horizontal = p.Left_Horizontal,
                            cssClass = p.cssClass,
                            TitleV = p.TitleV,
                            TitleE = p.TitleE,
                            HaveSetup = p.HaveSetup,
                            FunctionDefault = p.FunctionDefault,
                            MenuLevel = p.MenuLevel,
                            LevelParent = p.LevelParent,
                            LookId = p.LookId,
                            AscxParent = p.AscxParent,
                            Tooltips = p.Tooltips      
                        }).OrderBy(p=>p.Rank).ToList();           
            return lst;
        }
        public static SYS_tblFunctionList Find(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.SYS_tblFunctionList.Find(id);
            }
        }

        public FunctionListViewModel Details(int id)
        {
            try
            {
                FunctionListViewModel ret = context.SYS_tblFunctionList
                                        .Where(p => p.FunctionID == id)
                                        .Select(p => new FunctionListViewModel()
                                        {
                                            FunctionID = p.FunctionID,
                                            Parent = p.Parent,
                                            FunctionNameE = p.FunctionNameE,
                                            FunctionNameV = p.FunctionNameV,
                                            Rank = p.Rank,
                                            ModuleID = p.ModuleID,
                                            Url = p.Url,
                                            Ascx = p.Ascx,
                                            Display = p.Display,
                                            Left_Horizontal = p.Left_Horizontal,
                                            cssClass = p.cssClass,
                                            TitleV = p.TitleV,
                                            TitleE = p.TitleE,
                                            HaveSetup = p.HaveSetup,
                                            FunctionDefault = p.FunctionDefault,
                                            MenuLevel = p.MenuLevel,
                                            LevelParent = p.LevelParent,
                                            LookId = p.LookId,
                                            AscxParent = p.AscxParent,
                                            Tooltips = p.Tooltips
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new FunctionListViewModel();
            }

        }
      
        //public static bool Add(FunctionListViewModel model, out string messsage)
        //{
        //    bool result = false;
        //    messsage = string.Empty;
        //    try
        //    {
        //        if (IsIdExisted(model.FunctionID))
        //            messsage = Eagle.Resource.LanguageResource.ex;

        //        using (EntityDataContext context = new EntityDataContext())
        //        {
        //            SYS_tblFunctionList entity = new SYS_tblFunctionList();
        //            entity.Code = model.Code;
        //            entity.FunctionNameE = model.FunctionNameE;
        //            entity.VNFunctionNameE = model.VNFunctionNameE;
        //            entity.Rank = SYS_tblFunctionListRespository.GenerateRank();
        //            entity.Note = model.Note;
        //            entity.Display = true;
        //            context.Entry(entity).State = System.Data.Entity.EntityState.Added;
        //            int i = context.SaveChanges();
        //            if (i > 0)
        //            {
        //                result = true;
        //                messsage = Eagle.Resource.LanguageResource.CreateSuccess;
        //            }
        //        }
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        result = false;
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            messsage += "Entity of type \"{0}\" in state \"{1}\" has the following validation errors: " +  eve.Entry.Entity.GetType().FunctionNameE +" "+ eve.Entry.State;
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                messsage += "- Property: \"{0}\", Error: \"{1}\"" + ve.PropertyFunctionNameE + " " + ve.ErrorMessage;
        //            }
        //        }                
        //    }
        //    return result;
        //}
                
        //public static bool Update(FunctionListViewModel model, out string messsage)
        //{
        //    bool result = false;
        //    messsage = string.Empty;
        //    try
        //    {
        //        if (IsCodeExistedWithTypeID(model.Code, model.FunctionID))               
        //            messsage = Eagle.Resource.LanguageResource.ExistCode;

        //        using (EntityDataContext context = new EntityDataContext())
        //        {
        //            SYS_tblFunctionList entity = Find(model.FunctionID);
        //            entity.Code = model.Code;
        //            entity.FunctionNameE = model.FunctionNameE;
        //            entity.VNFunctionNameE = model.VNFunctionNameE;
        //            entity.Note = model.Note;
        //            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //            int i = context.SaveChanges();
        //            if (i == 1)
        //            {
        //                result = true;
        //                messsage = Eagle.Resource.LanguageResource.UpdateSuccess;
        //            }
        //        }  
        //    }
        //    catch (Exception ex)
        //    {
        //        messsage = ex.Message;
        //        result = false;
        //    }
        //    return result;
        //}

        //public static bool UpdateListOrder(int id, int listOrder, out string messsage)
        //{
        //    bool result = false;
        //    messsage = string.Empty;
        //    try
        //    {          
        //        using (EntityDataContext context = new EntityDataContext())
        //        {
        //            SYS_tblFunctionList entity = Find(id);
        //            entity.Rank = listOrder;
        //            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //            int i = context.SaveChanges();
        //            if (i == 1)
        //            {
        //                result = true;
        //                messsage = Eagle.Resource.LanguageResource.UpdateSuccess;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        messsage = ex.Message;
        //        result = false;
        //    }
        //    return result;
        //}

        public static bool UpdateStatus(int id, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    SYS_tblFunctionList entity = Find(id);
                    bool flag = (entity.Display == true) ? false : true;
                    entity.Display = flag;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        message = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                    else
                    {
                        result = false;
                        message = Eagle.Resource.LanguageResource.UpdateFailed;
                    }                        
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                result = false;
            }
            return result;
        }


        public static bool Delete(int id, out string message)
        {
            bool result = false;
            try
            {                
                using (EntityDataContext context = new EntityDataContext())
                {
                    SYS_tblFunctionList entity = Find(id);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        message = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                    else
                    {
                        result = false;
                        message = Eagle.Resource.LanguageResource.UpdateFailed;
                    }             
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                result = false;
            }
            return result;
        }
    }
}
