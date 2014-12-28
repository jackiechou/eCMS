using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Model.Contents;
using Eagle.Model.Contents.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Contents.News
{
    public class NewsRepository
    {
        public EntityDataContext context { get; set; }

        public NewsRepository(EntityDataContext context)
        {
            this.context = context;
        }
        string ip = NetworkUtils.GetIP4Address();

        //public static string[] PopulateArticleKeywordList(string prefixText)
        //{
        //    using (ArticleEntities dbContext = new ArticleLibrary.ArticleEntities())
        //    {
        //        if (dbContext.Connection.State != ConnectionState.Closed)
        //            dbContext.Connection.Open();
        //        string LowerKeywords = @prefixText.ToLower();

        //        var query = (from x in dbContext.Articles
        //                    where x.Headline.ToLower().Contains(LowerKeywords)
        //                    || x.Alias.Replace("-", " ").Contains(LowerKeywords)
        //                    || x.Authors.Contains(LowerKeywords)
        //                    || x.Abstract.Contains(LowerKeywords)                          
        //                    || x.Source.Contains(LowerKeywords)
        //                    || x.Tags.Contains(LowerKeywords)
        //                    && x.Status == "2"
        //                    orderby x.ArticleId descending
        //                     select x).ToList();

        //        string[] items = new string[query.Count];
        //        items = query.Select(o => o.ToString()).ToArray();
        //        return items;
        //    }

        //}

        public static List<NewsViewModel> SearchByKeywords(string Keywords, int iTotalItemCount, string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                string LowerKeywords = Keywords.ToLower();

                var query = from x in context.News
                            join y in context.NewsCategories on x.CategoryCode equals y.CategoryCode into catelist
                            from cate in catelist.DefaultIfEmpty()
                            where x.Headline.ToLower().Contains(LowerKeywords)
                            || x.Alias.Replace("-", " ").Contains(LowerKeywords)
                            || x.Authors.Contains(LowerKeywords)
                            || x.Summary.Contains(LowerKeywords)
                            || x.MainText.Contains(LowerKeywords)
                            || x.Source.Contains(LowerKeywords)
                            || x.Tags.Contains(LowerKeywords)
                            && x.LanguageCode == LanguageCode && x.Status == 2
                            orderby x.NewsId descending
                            select new NewsViewModel
                            {
                                CategoryName = cate.CategoryName,
                                CategoryAlias = cate.Alias,
                                CategoryImage = cate.CategoryImage,
                                ApplicationId = x.ApplicationId,
                                NewsId = x.NewsId,
                                CategoryCode = x.CategoryCode,
                                LanguageCode = x.LanguageCode,
                                Title = x.Title,
                                Headline = x.Headline,
                                Alias = x.Alias,
                                Summary = x.Summary,
                                FrontImage = x.FrontImage,
                                MainImage = x.MainImage,
                                MainText = x.MainText,
                                NavigateUrl = x.NavigateUrl,
                                Authors = x.Authors,
                                ListOrder = x.ListOrder,
                                Tags = x.Tags,
                                Source = x.Source,
                                TotalRates = x.TotalRates,
                                TotalViews = x.TotalViews,
                                Status = x.Status,
                                CreatedOnDate = x.CreatedOnDate,
                                LastModifiedOnDate = x.LastModifiedOnDate,
                                CreatedByUserId = x.CreatedByUserId,
                                LastModifiedByUserId = x.LastModifiedByUserId,
                                IPLog = x.IPLog,
                                IPLastUpdated = x.IPLastUpdated
                            };
                int TotalItemCount = 0;
                if (query.Count() > 0)
                {
                    if (iTotalItemCount <= query.Count())
                        TotalItemCount = iTotalItemCount;
                    else
                        TotalItemCount = query.Count();
                }
                return query.Take(TotalItemCount).ToList();
            }
        }

        public static List<NewsViewModel> GetListByTotalViewsFixedNum(string Code, int Status, int iTotalItemCount, string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = from x in context.News
                            join y in context.NewsCategories on x.CategoryCode equals y.CategoryCode into catelist
                            from cate in catelist.DefaultIfEmpty()
                            where x.CategoryCode == Code
                                && x.LanguageCode == LanguageCode
                                && x.Status == Status
                                && x.TotalViews > 0
                            orderby x.TotalViews descending
                            select new NewsViewModel
                            {
                                CategoryName = cate.CategoryName,
                                CategoryAlias = cate.Alias,
                                CategoryImage = cate.CategoryImage,
                                ApplicationId = x.ApplicationId,
                                NewsId = x.NewsId,
                                CategoryCode = x.CategoryCode,
                                LanguageCode = x.LanguageCode,
                                Title = x.Title,
                                Headline = x.Headline,
                                Alias = x.Alias,
                                Summary = x.Summary,
                                FrontImage = x.FrontImage,
                                MainImage = x.MainImage,
                                MainText = x.MainText,
                                NavigateUrl = x.NavigateUrl,
                                Authors = x.Authors,
                                ListOrder = x.ListOrder,
                                Tags = x.Tags,
                                Source = x.Source,
                                TotalRates = x.TotalRates,
                                TotalViews = x.TotalViews,
                                Status = x.Status,
                                CreatedOnDate = x.CreatedOnDate,
                                LastModifiedOnDate = x.LastModifiedOnDate,
                                CreatedByUserId = x.CreatedByUserId,
                                LastModifiedByUserId = x.LastModifiedByUserId,
                                IPLog = x.IPLog,
                                IPLastUpdated = x.IPLastUpdated
                            };
                int TotalItemCount = 0;
                if (query.Count() > 0)
                {
                    if (iTotalItemCount <= query.Count())
                        TotalItemCount = iTotalItemCount;
                    else
                        TotalItemCount = query.Count();
                }
                return query.Take(TotalItemCount).ToList();
            }
        }

        public static NewsViewModel GetCompanyInfo(string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var entity = (from x in context.News
                              join y in context.NewsCategories on x.CategoryCode equals y.CategoryCode into catelist
                              from cate in catelist.DefaultIfEmpty()
                              where x.CategoryCode == "COMPANY_INFO"
                                  && x.LanguageCode == LanguageCode
                                  && x.Status == (int)ThreeStatusMode.Published
                              orderby x.NewsId descending
                              select new NewsViewModel
                              {
                                  CategoryName = cate.CategoryName,
                                  CategoryAlias = cate.Alias,
                                  CategoryImage = cate.CategoryImage,
                                  ApplicationId = x.ApplicationId,
                                  NewsId = x.NewsId,
                                  CategoryCode = x.CategoryCode,
                                  LanguageCode = x.LanguageCode,
                                  Title = x.Title,
                                  Headline = x.Headline,
                                  Alias = x.Alias,
                                  Summary = x.Summary,
                                  FrontImage = x.FrontImage,
                                  MainImage = x.MainImage,
                                  MainText = x.MainText,
                                  NavigateUrl = x.NavigateUrl,
                                  Authors = x.Authors,
                                  ListOrder = x.ListOrder,
                                  Tags = x.Tags,
                                  Source = x.Source,
                                  TotalRates = x.TotalRates,
                                  TotalViews = x.TotalViews,
                                  Status = x.Status,
                                  CreatedOnDate = x.CreatedOnDate,
                                  LastModifiedOnDate = x.LastModifiedOnDate,
                                  CreatedByUserId = x.CreatedByUserId,
                                  LastModifiedByUserId = x.LastModifiedByUserId,
                                  IPLog = x.IPLog,
                                  IPLastUpdated = x.IPLastUpdated
                              }).FirstOrDefault();
                return entity;
            }
        }

        public static List<NewsViewModel> GetListByFixedNumCode(string CategoryCode, int Status, int iTotalItemCount, string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = from x in context.News
                            join y in context.NewsCategories on x.CategoryCode equals y.CategoryCode into catelist
                            from cate in catelist.DefaultIfEmpty()
                            where x.CategoryCode == CategoryCode
                                && x.LanguageCode == LanguageCode
                                && x.Status == Status
                            orderby x.NewsId descending
                            select new NewsViewModel
                            {
                                CategoryName = cate.CategoryName,
                                CategoryAlias = cate.Alias,
                                CategoryImage = cate.CategoryImage,
                                ApplicationId = x.ApplicationId,
                                NewsId = x.NewsId,
                                CategoryCode = x.CategoryCode,
                                LanguageCode = x.LanguageCode,
                                Title = x.Title,
                                Headline = x.Headline,
                                Alias = x.Alias,
                                Summary = x.Summary,
                                FrontImage = x.FrontImage,
                                MainImage = x.MainImage,
                                MainText = x.MainText,
                                NavigateUrl = x.NavigateUrl,
                                Authors = x.Authors,
                                ListOrder = x.ListOrder,
                                Tags = x.Tags,
                                Source = x.Source,
                                TotalRates = x.TotalRates,
                                TotalViews = x.TotalViews,
                                Status = x.Status,
                                CreatedOnDate = x.CreatedOnDate,
                                LastModifiedOnDate = x.LastModifiedOnDate,
                                CreatedByUserId = x.CreatedByUserId,
                                LastModifiedByUserId = x.LastModifiedByUserId,
                                IPLog = x.IPLog,
                                IPLastUpdated = x.IPLastUpdated
                            };
                int TotalItemCount = 0;
                if (query.Count() > 0)
                {
                    if (iTotalItemCount <= query.Count())
                        TotalItemCount = iTotalItemCount;
                    else
                        TotalItemCount = query.Count();
                }
                return query.Take(TotalItemCount).ToList();
            }
        }

        public static List<NewsViewModel> GetListByFixedRecords(int Status, int iTotalItemCount)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<NewsViewModel> lst = new List<NewsViewModel>();
                lst = (from x in context.News
                       join y in context.NewsCategories on x.CategoryCode equals y.CategoryCode into catelist
                       from cate in catelist.DefaultIfEmpty()
                       where x.Status == Status
                       orderby x.NewsId descending
                       select new NewsViewModel
                       {
                           CategoryName = cate.CategoryName,
                           CategoryAlias = cate.Alias,
                           CategoryImage = cate.CategoryImage,
                           ApplicationId = x.ApplicationId,
                           NewsId = x.NewsId,
                           CategoryCode = x.CategoryCode,
                           LanguageCode = x.LanguageCode,
                           Title = x.Title,
                           Headline = x.Headline,
                           Alias = x.Alias,
                           Summary = x.Summary,
                           FrontImage = x.FrontImage,
                           MainImage = x.MainImage,
                           MainText = x.MainText,
                           NavigateUrl = x.NavigateUrl,
                           Authors = x.Authors,
                           ListOrder = x.ListOrder,
                           Tags = x.Tags,
                           Source = x.Source,
                           TotalRates = x.TotalRates,
                           TotalViews = x.TotalViews,
                           Status = x.Status,
                           CreatedOnDate = x.CreatedOnDate,
                           LastModifiedOnDate = x.LastModifiedOnDate,
                           CreatedByUserId = x.CreatedByUserId,
                           LastModifiedByUserId = x.LastModifiedByUserId,
                           IPLog = x.IPLog,
                           IPLastUpdated = x.IPLastUpdated
                       }).ToList();

                int TotalItemCount = 0;
                if (lst.Count() > 0)
                {
                    if (iTotalItemCount <= lst.Count())
                        TotalItemCount = iTotalItemCount;
                    else
                        TotalItemCount = lst.Count();
                }
                return lst.Take(TotalItemCount).ToList();
            }
        }


        //public DataTable GetDataByConditions(int ApplicationId, string culturecode, string begindate, string enddate, string code, string status)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetDataByConditions]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
        //    cmd.Parameters.AddWithValue("@culturecode", culturecode);
        //    cmd.Parameters.AddWithValue("@begindate", begindate);
        //    cmd.Parameters.AddWithValue("@enddate", enddate);
        //    cmd.Parameters.AddWithValue("@code", code);
        //    cmd.Parameters.AddWithValue("@status", status);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)) { dt.Load(dr); dr.Close(); }
        //    con.Close();
        //    return dt;
        //}

        //public DataTable GetDetailByAlias(string alias)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetDetailByAlias]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@alias", alias);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); dr.Close(); }
        //    con.Close();
        //    return dt;
        //}

        //public DataTable GetDetailByCodeAlias(string alias, string code, string lang)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetDetailByCodeAlias]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@alias", alias);
        //    cmd.Parameters.AddWithValue("@code", code);
        //    cmd.Parameters.AddWithValue("@lang", lang);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); dr.Close(); }
        //    con.Close();
        //    return dt;
        //}

        //public DataTable GetDetailByID(int idx)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetDetailById]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); dr.Close(); }
        //    con.Close();
        //    return dt;
        //}

        //public string GetFrontImageByID(int idx)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetFrontImageById]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    string retunvalue = (string)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}

        //public string GetMainImageByID(int idx)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetMainImageById]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    string retunvalue = (string)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}

        //public string[] GetFrontMainImages(int id)
        //{
        //    string[] array_list = new string[2];
        //    DataTable dt = GetDetailByID(id);
        //    array_list[0] = dt.Rows[0]["MainImage"].ToString();
        //    array_list[1] = dt.Rows[0]["FrontImage"].ToString();
        //    return array_list;
        //}

        //public DataSet ShowPaging(int current_page, int record_per_page, int page_size)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_Paging]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@current_page", current_page);
        //    cmd.Parameters.AddWithValue("@record_per_page", record_per_page);
        //    cmd.Parameters.AddWithValue("@page_size", page_size);
        //    con.Open();
        //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds);
        //    con.Close();
        //    return ds;
        //}

        //public DataTable GetListByCode(string code)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetListByCode]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@code", code);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
        //    con.Close();
        //    return dt;
        //}

        //public DataTable GetListByNumCode(string code, int num_rows, string lang)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetListByNumCode]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@num_rows", num_rows);
        //    cmd.Parameters.AddWithValue("@code", code);
        //    cmd.Parameters.AddWithValue("@lang", lang);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); dr.Close(); }
        //    con.Close();
        //    return dt;
        //}

        //#region GET OTHER NEWS LIST DEPEND ON CATEGORY CODE AND ID OR ALIAS IN DETAIL PAGE =============
        //public DataTable GetTopListByCodeSelectId(string code, int idx, int record)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetTopListByCodeSelectId]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@code", code);
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.AddWithValue("@record", record);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
        //    con.Close();
        //    return dt;
        //}

        //public DataTable GetTopNewListByCodeSelectedId(string code, int idx, int record)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetTopNewListByCodeSelectedId]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@code", code);
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.AddWithValue("@record", record);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
        //    con.Close();
        //    return dt;
        //}

        //public DataTable GetTopOldListByCodeSelectedId(string code, int idx, int record)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetTopOldListByCodeSelectedId]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@code", code);
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.AddWithValue("@record", record);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
        //    con.Close();
        //    return dt;
        //}
        //#endregion =====================================================================================

        //public DataTable GetListTotalViewsByNumRecords(int num_rows)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetListTotalViewsByNumRecords]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@num_rows", num_rows);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); dr.Close(); }
        //    con.Close();
        //    return dt;
        //}

        //public DataTable GetRandomList(int records)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_GetRandomList]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@records", records);
        //    con.Open();
        //    using (var dr = cmd.ExecuteReader()) { dt.Load(dr); dr.Close(); }
        //    con.Close();
        //    return dt;
        //}

        //public int Insert(string userid, int ApplicationId, string culturecode, string code, string title, string headline, string abstract_info,
        //    string frontimage, string mainimage, string maintext, string source, string navigateurl, string status)
        //{

        //    /*** PostedDate ******************************************************************************/
        //    //System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US");
        //    //customCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
        //    //customCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
        //    //System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        //    //System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;
        //    //string PostedDate = System.DateTime.Now.ToString("G");
        //    /*********************************************************************************************/

        //    string alias = StringUtils.convertTitle2Link(headline);
        //    maintext = StringUtils.UTF8_Encode(maintext);
        //    string tags = StringUtils.createTags(headline);

        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@userid", userid);
        //    cmd.Parameters.AddWithValue("@ip", ip);
        //    cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
        //    cmd.Parameters.AddWithValue("@culturecode", culturecode);
        //    cmd.Parameters.AddWithValue("@code", code);
        //    cmd.Parameters.AddWithValue("@title", title);
        //    cmd.Parameters.AddWithValue("@headline", headline);
        //    cmd.Parameters.AddWithValue("@alias", alias);
        //    cmd.Parameters.AddWithValue("@abstract", abstract_info);
        //    cmd.Parameters.AddWithValue("@frontimage", frontimage);
        //    cmd.Parameters.AddWithValue("@mainimage", mainimage);
        //    cmd.Parameters.AddWithValue("@maintext", maintext);
        //    cmd.Parameters.AddWithValue("@tags", tags);
        //    cmd.Parameters.AddWithValue("@source", source);
        //    cmd.Parameters.AddWithValue("@navigateurl", navigateurl);
        //    cmd.Parameters.AddWithValue("@status", status);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    int i = cmd.ExecuteNonQuery();
        //    int retunvalue = (int)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}

        //public int Update(string userid, int ApplicationId, string culturecode, int idx, string code, string title, string headline, string abstract_info, string frontimage, string mainimage, string maintext, string source, string navigateurl, string status)
        //{
        //    string alias = StringUtils.convertTitle2Link(headline);
        //    maintext = StringUtils.UTF8_Encode(maintext);
        //    string tags = StringUtils.createTags(headline);

        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@userid", userid);
        //    cmd.Parameters.AddWithValue("@ip", ip);
        //    cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
        //    cmd.Parameters.AddWithValue("@culturecode", culturecode);
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.AddWithValue("@code", code);
        //    cmd.Parameters.AddWithValue("@title", title);
        //    cmd.Parameters.AddWithValue("@headline", headline);
        //    cmd.Parameters.AddWithValue("@alias", alias);
        //    cmd.Parameters.AddWithValue("@abstract", abstract_info);
        //    cmd.Parameters.AddWithValue("@frontimage", frontimage);
        //    cmd.Parameters.AddWithValue("@mainimage", mainimage);
        //    cmd.Parameters.AddWithValue("@maintext", maintext);
        //    cmd.Parameters.AddWithValue("@tags", tags);
        //    cmd.Parameters.AddWithValue("@source", source);
        //    cmd.Parameters.AddWithValue("@navigateurl", navigateurl);
        //    cmd.Parameters.AddWithValue("@status", status);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    int i = cmd.ExecuteNonQuery();
        //    int retunvalue = (int)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}

        //public int UpdateSortKey(int option, int idx)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_UpdateSortKey]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@option", option);
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    int retunvalue = (int)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}

        //public int Update_Total_View(int idx, int totalview)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_UpdateTotalView]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.AddWithValue("@total", totalview);
        //    cmd.Parameters.AddWithValue("@ip", ip);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    int retunvalue = (int)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}

        //public int UpdateTotalView(int ArticleId, int NewView)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_UpdateTotalView]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@ArticleId", ArticleId);
        //    cmd.Parameters.AddWithValue("@NewView", NewView);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    int retunvalue = (int)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}

        //public int UpdateStatus(string userid, int idx, string status)
        //{
        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_UpdateStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@userid", userid);
        //    cmd.Parameters.AddWithValue("@ip", ip);
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.AddWithValue("@status", status);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    int retunvalue = (int)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}

        //public int Delete(string userid, int idx, string front_dir_path, string main_dir_path)
        //{            
        //    string MainImage = string.Empty, FrontImage = string.Empty;
        //    string[] array_list = new string[2];
        //    array_list = GetFrontMainImages(idx);
        //    MainImage = array_list[0];
        //    FrontImage = array_list[1];

        //    if (FrontImage != string.Empty)
        //        FileUtils.DeleteFileWithFileNameAndPredefinedPath(FrontImage, front_dir_path);
        //    if (MainImage != string.Empty)
        //        FileUtils.DeleteFileWithFileNameAndPredefinedPath(MainImage, main_dir_path);


        //    SqlCommand cmd = new SqlCommand("[Articles].[sp_Articles_Delete]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = Settings.CommandTimeout };
        //    cmd.Parameters.AddWithValue("@userid", userid);
        //    cmd.Parameters.AddWithValue("@ip", ip);
        //    cmd.Parameters.AddWithValue("@idx", idx);
        //    cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    int retunvalue = (int)cmd.Parameters["@o_return"].Value;
        //    con.Close();
        //    return retunvalue;
        //}


        //public static int AddArticleContentToModule(int ModuleId, string UserId, int ApplicationId, string CultureCode, string CategoryCode, string Title, string Headline, string Abstract,
        //    string FrontImage, string MainImage, string MainText, string Source, string NavigateUrl, string Status)
        //{
        //    using (ArticleEntities dbContext = new ArticleLibrary.ArticleEntities())
        //    {
        //        int returnValue = 0;
        //        int ArticleId = AddArticle(UserId, ApplicationId, CultureCode, CategoryCode, Title, Headline, Abstract, FrontImage, MainImage, MainText, Source, NavigateUrl, Status);
        //        if(ArticleId > 0)
        //        {
        //            aspnet_ModuleHtmlText entity = new aspnet_ModuleHtmlText();
        //            entity.ArticleId = ArticleId;
        //            entity.ModuleId = ModuleId;
        //            dbContext.AddToaspnet_ModuleHtmlText(entity);
        //            returnValue = dbContext.SaveChanges();                  
        //        }
        //        return returnValue;
        //    }
        //}

        //public static int AddArticle(string UserId, int ApplicationId, string CultureCode, string CategoryCode, string Title, string Headline, string Abstract,
        //    string FrontImage, string MainImage, string MainText, string Source, string NavigateUrl, string Status)
        //{
        //    int returnValue = 0;
        //    using (ArticleEntities dbContext = new ArticleLibrary.ArticleEntities())
        //    {
        //        using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
        //        {
        //            dbContext.CommandTimeout = Settings.CommandTimeout;
        //            int SortKey = (from u in dbContext.Articles select u.SortKey).DefaultIfEmpty(0).Max() + 1;

        //            Article entity = new Article();
        //            entity.ApplicationId = ApplicationId;
        //            entity.CultureCode = CultureCode;
        //            entity.CategoryCode = CategoryCode;
        //            entity.Title = Title;
        //            entity.Headline = Headline;
        //            entity.Alias = StringUtils.GenerateFriendlyString(Headline);
        //            entity.Abstract = Abstract;
        //            entity.NavigateUrl = NavigateUrl;
        //            entity.FrontImage = FrontImage;
        //            entity.MainImage = MainImage;
        //            entity.MainText = StringUtils.UTF8_Encode(MainText);
        //            entity.Tags = StringUtils.createTags(Headline);
        //            entity.SortKey = SortKey;
        //            entity.Source = Source;
        //            entity.IPLog = ip;
        //            entity.PostedBy = new Guid(UserId);
        //            entity.DateCreated = System.DateTime.Now;
        //            entity.Status = Status;

        //            dbContext.AddToArticles(entity);
        //            returnValue = dbContext.SaveChanges();
        //            transcope.Complete();
        //        }
        //    }
        //    return returnValue;
        //}
    }
}
