using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Eagle.Repository.LS
{
    public class LS_tblOnlineProcessMailCcRespository
    {
        public EntityDataContext context { get; set; }

        public LS_tblOnlineProcessMailCcRespository(EntityDataContext context)
       { 
           this.context = context;
       }

        public static List<Dictionary<string, object>> GetEmpJsonListById(int DMQuiTrinhID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var list = (from c in context.LS_tblOnlineProcessMailCc
                            join e in context.HR_tblEmp on c.EmpID equals e.EmpID into empGroup
                            from emp in empGroup.DefaultIfEmpty()
                            where c.DMQuiTrinhID == DMQuiTrinhID
                            select new
                            {
                                EmpID = c.EmpID,
                                EmpName = emp.LastName + " " + emp.FirstName,
                                EmpEmail = emp.LastName + " " + emp.FirstName + " - " + emp.Email
                            }).ToList();

                List<Dictionary<string, object>> result_dict = new List<Dictionary<string, object>>();
                string json = string.Empty;
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        dict.Add("id", item.EmpID);
                        //dict.Add("name", item.EmpName);
                        dict.Add("text", item.EmpEmail);
                        result_dict.Add(dict);
                    }                   
                }
                return result_dict;
            }
        }

        public static string GetEmpListById(int DMQuiTrinhID, out string strIdList, out string strTextList)
        {
            using (EntityDataContext context = new EntityDataContext())
            {                
                var list = (from c in context.LS_tblOnlineProcessMailCc 
                          join e in context.HR_tblEmp on c.EmpID equals e.EmpID into empGroup
                          from emp in empGroup.DefaultIfEmpty()
                          where c.DMQuiTrinhID == DMQuiTrinhID select new {
                              EmpID = c.EmpID,
                              EmpName = emp.LastName + " " + emp.FirstName,
                              EmpEmail = emp.LastName + " " + emp.FirstName + " - " + emp.Email
                          }).ToList();
                //Dictionary<string, object> result_dict = new Dictionary<string, object>();
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //string json = string.Empty;
                //int index = 0;
                //if (list.Count > 0)
                //{
                //    foreach (var item in list)
                //    {
                //        Dictionary<string, object> dict = new Dictionary<string, object>();
                //        dict.Add("id", item.EmpID);
                //        dict.Add("text", item.EmpName);
                //        result_dict.Add(index.ToString(), dict);
                //        index++;
                //    }
                //    json = serializer.Serialize(result_dict);
                //}

                List<Dictionary<string, object>> result_dict = new List<Dictionary<string, object>>();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = string.Empty;
                strIdList = string.Empty;
                strTextList = string.Empty;

                if (list.Count > 0)
                {
                    List<int> idList = new List<int>();
                    List<string> textList = new List<string>();
                    foreach (var item in list)
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        dict.Add("id", item.EmpID);
                        dict.Add("name", item.EmpName);
                        dict.Add("text", item.EmpEmail);

                        idList.Add(item.EmpID);
                        textList.Add(item.EmpName);

                        result_dict.Add(dict);
                    }
                    strIdList = string.Join(",", idList.Select(x => x.ToString()).ToArray());
                    strTextList = string.Join(",", textList.Select(x => x.ToString()).ToArray());
                    json = serializer.Serialize(result_dict);
                }
                return json;
            }
        }

       public static IQueryable<LS_tblOnlineProcessMailCc> Find(int DMQuiTrinhID, int EmpID)
       {
           using (EntityDataContext context = new EntityDataContext())
           {
               var query = context.LS_tblOnlineProcessMailCc.Where(p => p.DMQuiTrinhID.Equals(DMQuiTrinhID)
                             && p.EmpID.Equals(EmpID));
               return query;
           }
       }

       public static bool IsExisted(int DMQuiTrinhID, int EmpID)
        {
           LS_tblOnlineProcessMailCc query = Find(DMQuiTrinhID, EmpID).SingleOrDefault();
           return (query != null);
        }

        public static bool InsertData(int DMQuiTrinhID, int EmpID)
        {            
            using (EntityDataContext context = new EntityDataContext())
            {
                bool result = false;
                try
                {                    
                    LS_tblOnlineProcessMailCc entity = new LS_tblOnlineProcessMailCc();
                    entity.DMQuiTrinhID = DMQuiTrinhID;
                    entity.EmpID = EmpID;

                    context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    int affectedRow = context.SaveChanges();
                    if (affectedRow > 0) 
                        result = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                }
                return result;
            }
        }

        public static bool UpdateData(int DMQuiTrinhID, List<string> EmpIDList)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                bool result = false;
                try
                {
                    result = DeleteData(DMQuiTrinhID);
                    if (result == true)
                    {
                        if (EmpIDList.Count > 0)
                        {
                            foreach (var EmpID in EmpIDList)
                            {
                                result = InsertData(DMQuiTrinhID, Convert.ToInt32(EmpID));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                }
                return result;
            }
        }

        public static bool DeleteData(int DMQuiTrinhID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                bool result = false;
                try
                {
                    List<LS_tblOnlineProcessMailCc> lst = context.LS_tblOnlineProcessMailCc.Where(p => p.DMQuiTrinhID.Equals(DMQuiTrinhID)).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var entity in lst)
                        {
                            context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                            context.SaveChanges();
                            result = true;
                        }
                    }
                    else
                        result = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                }
                return result;
            }
        }
    }
}
