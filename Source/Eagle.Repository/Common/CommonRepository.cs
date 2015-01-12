using Eagle.Core;
using Eagle.Model;
using Eagle.Model.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Repository
{
    public class CommonRepository
    {
        #region Variable Server
        public const string Language_vi = "vi";
        public const string Language_en = "en";
        public const int Successfull = 1;
        public const int Error = -1;

        public const int UnitDefault = 10001;
        public const int Role_Inventory = 10004;
        public const int PriceOfProduct = 10001;
        public const int PriceOfCustomer = 10002;
        public const int PriceOfChannel = 10003;

        public const long BRANCHISHO = 100001;
        public const long Admin = 100146;


        public enum Tables
        {
            M_Product,
            M_ProductSKU,
            M_ProductCustomer,
            M_ProductChannel,
            M_Customer,
            M_Inventory,
            M_VAT,
            M_Discount,
            M_SaleForce,
            M_UNIT
        }

        public enum ExportFile
        {
            Word,
            Excel,
            Pdf
        }

        public enum Process
        {
            Order = 1, // Bán Hàng
            Warranty = 2, // Trả Hàng
            Purchase = 3, // Mua Hàng
            ChangePrice = 4, // Đổi Giá
            BalanceStock = 5, // Cập nhật Đầu Kỳ
            FactInventory = 6, // Cập Nhật Thực Tế
            TransferStock = 7, // Chuyển Kho
            DefectStock = 8, // Bỏ Hàng
            LiquidationStock = 9 // Thanh Lý
        }

        public const string Url_HomeIndex = "/Admin/Home/Index";
        #endregion

        #region Variable Session And Cookied
        public const string Session_AccountInfo = "Session_AccountInfo";
        public const string Session_UserId = "UserId";
        public const string Session_UserName = "UserName";
        public const string Session_FullName = "FullName";
        public const string Session_Email = "Email";
        public const string Session_ObjectType = "ObjectType";
        public const string Session_BranchID = "BranchID";
        public const string Session_BranchName = "BranchName";
        public const string Session_DepartmentID = "DepartmentID";
        public const string Session_DepartmentName = "DepartmentName";
        public const string Session_KTVID = "KTVID";
        public const string Session_KTVName = "KTVName";
        public const string Session_DaiLyID = "DaiLyID";
        public const string Session_DaiLyName = "DaiLyName";
        public const string Session_RoleId = "RoleId";
        public const string Session_PageId = "PageId";
        public const string Session_PageUrl = "PageUrl";
        public const string Session_CountryId = "CountryId";
        public const string Session_ProvinceId = "ProvinceId";
        public const string Session_DistrictId = "DistrictId";
        public const string Session_PageAccess = "PageAccess";
        public const string Session_Read = "Read";
        public const string Session_Write = "Write";
        public const string Session_Edit = "Edit";
        public const string Session_BranchCover = "BranchCover";
        public const string Session_UserTypeName = "UserType";

        public const string Session_Menu = "Menu";
        public const string Session_Page = "Page";
        public const string Session_HtmlPage = "HtmlPage";
        public const string Session_ChangeLanguage = "ChangeLanguage";
        public const string Session_SiteMap = "SiteMap";

        public const string Session_Language = "Language";
        public const string Session_CultureLanguage = "CultureLanguage";

        public const string Cookies_userInfo = "userInfo";
        public const string Cookies_username = "username";
        public const string Cookies_password = "password";
        #endregion
        
        #region Variable Array
        /// <summary>
        /// 0: M/d/yyyy
        /// 1: M/d/yyyy h:mm:ss
        /// 2: M/d/yyyy h:mm:ss tt
        /// 3: M/d/yyyy h:mm tt
        /// 4: M/d/yyyy hh:mm tt
        /// 5: M/d/yyyy hh tt
        /// 6: M/d/yyyy h:mm
        /// 7: M/d/yy
        /// 8: MM/dd/yyyy
        /// 9: MM/dd/yyyy hh:mm:ss
        /// 10: MM/dd/yyyy hh:mm
        /// 11: M/dd/yyyy hh:mm
        /// 12: dd/MM/yyyy
        /// 13: dd-MMM-yy
        /// 14: dd/MM/yyyy hh:mm:ss tt
        /// 15: dd/MM/yyyy hh:mm:ss
        /// 16: dd/MM/yyyy hh:mm
        /// 17: yy/MM/dd
        /// 18: yyyy/MM/dd
        /// 19: yyyy/MM/dd hh:mm:ss tt
        /// 20: yyyy-MM-dd
        /// 21: yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string[] datetime_formats = {
                           "M/d/yyyy", "M/d/yyyy h:mm:ss","M/d/yyyy h:mm:ss tt","M/d/yyyy h:mm tt","M/d/yyyy hh:mm tt","M/d/yyyy hh tt","M/d/yyyy h:mm","M/d/yy",                            
                           "MM/dd/yyyy", "MM/dd/yyyy hh:mm:ss", "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",                            
                           "dd/MM/yyyy","dd-MMM-yy","dd/MM/yyyy hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss","dd/MM/yyyy hh:mm",                           
                           "yy/MM/dd","yyyy/MM/dd","yyyy/MM/dd hh:mm:ss tt","yyyy-MM-dd","yyyy-MM-dd HH:mm:ss"};

        public static string[] Color_Status_Process = { "#FF0000", "#00FF00", "#0000FF", "#FFFF00", "#FF00FF", "#00FFFF", "#FFFFFF" };
        public static string[] TransactionType = { "", "NCCToHO", "HOToBranch", "InventoryDelProduct", "Stock", "StockTransfer", "InventoryActual", "InventoryUpdate", "ChangePriceForProduct", "Promote", "SalesTargets", "", "", "PackagedProduct", "PackageImport", "PackageExport" };
        #endregion

        #region Variable Const
        public const string FormatViewddMMyyy = "dd/MM/yyyy";
        public const string FormatyyyyMMdd = "yyyyMMdd";
        #endregion

        #region Variable Class
        public const string Css_alert = "alert";
        public const string Css_alert_success = "alert alert-success";
        public const string Css_alert_info = "alert alert-info";
        public const string Css_alert_warning = "alert alert-warning";
        public const string Css_alert_error = "alert alert-error";
        public const string IconDefault = "halfling-globe";
        #endregion

        #region Variable Status
        public const int Delete = 2;
        public const int Alive = 1;
        #endregion

        #region Variabel DocumentType
        public enum DocumentType { HD, HS, NT, TA, BT, HT }

        #endregion
        public EntityDataContext context { get; set; }
        public CommonRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<SYS_spfrmApproveOnlineProcess_Result> GetApprovePermission(int? CurrentEmpId)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext.ApplicationInstance.Session["ApprovePermission"] == null)
            {
                List<SYS_spfrmApproveOnlineProcess_Result> list = context.SYS_spfrmApproveOnlineProcess(CurrentEmpId).ToList();
                httpContext.ApplicationInstance.Session["ApprovePermission"] = list;
                return list;
            }
            else
            {
                List<SYS_spfrmApproveOnlineProcess_Result> list = (List<SYS_spfrmApproveOnlineProcess_Result>)httpContext.ApplicationInstance.Session["ApprovePermission"];
                return list;
            }
        }
        // statusPlan, currentEmpId, empIDPlan => dùng để set quyền 3 nút reset, save và send
        // statusPlan, FunctionID, CurrentLevelApprove,maxLevelApprove, planID => dùng để set quyền 2 nút approve và unapprove
        public void CheckPermission(int statusPlan, int? currentEmpId, int empIDPlan, int FunctionID, int CurrentLevelApprove, int maxLevelApprove, int planID, ref bool DisabledSaveAndSend, ref bool DisabledApproveAndUnapprove, int StatusProcess = 1)
        {
            if (StatusProcess == 1)
            {
                #region//Dùng để phân quyền 3 nút "Reset", "Save" và "Send for approval"
                // nếu không phải người tạo xem thì ẩn luôn
                if (currentEmpId != empIDPlan)
                {
                    DisabledSaveAndSend = false;
                }
                //Kiểm tra 3 nút đầu
                //Nếu là bị cấp 1 không duyệt cho phép chỉnh sửa
                else if ((statusPlan == 3 || statusPlan == 5 || statusPlan == 7 || statusPlan == 9 || statusPlan == 11) && CurrentLevelApprove == 0)
                {
                    DisabledSaveAndSend = true;
                }
                //Nếu đã gửi đi thì không được quyền chỉnh sửa
                else if (planID != 0 && (statusPlan != 0 || (empIDPlan != currentEmpId)))
                {
                    DisabledSaveAndSend = false;
                }
                //Nếu chưa gửi được quyền chỉnh sửa
                else
                {
                    DisabledSaveAndSend = true;
                }
                #endregion
                #region//Kiểm tra 2 nút "Approve" và "Unapprove"

                // Bước 1 kiểm tra plan này khi đang là ở max level approve và đã được duyệt thì ẩn. không thì set tiếp
                if (CurrentLevelApprove == maxLevelApprove && ((maxLevelApprove * 2) == statusPlan || (maxLevelApprove * 2 + 1) == statusPlan))
                {
                    DisabledApproveAndUnapprove = false;
                }
                else
                {
                    CommonRepository _commonRepository = new CommonRepository(context);
                    var ListPermission = _commonRepository.GetApprovePermission(currentEmpId).ToList();
                    //Chức năng 456 = "Recruitment Plan"


                    //Các phòng được duyệt (Level2ID)
                    List<int> SectionPermisstion = new List<int>();
                    if (ListPermission != null && ListPermission.Count > 0)
                    {
                        SectionPermisstion = ListPermission.Where(p => p.FunctionID == FunctionID).Select(p => p.LSCompanyID).ToList();
                    }
                    //Nếu kế hoạch này được User thuộc phòng ban nằm trong list trên (1) 
                    // VÀ được quyền approve trùng với tình trạng approve của kế hoạch này (2)

                    var empModel = context.HR_tblEmp.Where(p => p.EmpID == empIDPlan).FirstOrDefault();
                    //1
                    if (empModel != null && SectionPermisstion.Contains(empModel.LSCompanyID))
                    {
                        // nếu mà được quyền approve phòng này thì set tiếp
                        //2
                        //Set xem có đúng là đang ở cấp approve
                        // user này có quyền approve ở các cấp nằm trong permisson
                        var permisson = ListPermission.Where(p => p.FunctionID == FunctionID && p.LSCompanyID == empModel.LSCompanyID).FirstOrDefault();
                        bool checkResult = false;
                        switch (CurrentLevelApprove)
                        {
                            case 1:
                                if (permisson.ApproveLevel1 == true)
                                {
                                    checkResult = true;
                                }
                                break;
                            case 2:
                                if (permisson.ApproveLevel2 == true)
                                {
                                    checkResult = true;
                                }
                                break;
                            case 3:
                                if (permisson.ApproveLevel3 == true)
                                {
                                    checkResult = true;
                                }
                                break;
                            case 4:
                                if (permisson.ApproveLevel4 == true)
                                {
                                    checkResult = true;
                                }
                                break;
                            case 5:
                                if (permisson.ApproveLevel5 == true)
                                {
                                    checkResult = true;
                                }
                                break;
                        }
                        DisabledApproveAndUnapprove = checkResult;
                    }
                    else
                    {
                        DisabledApproveAndUnapprove = false;
                    }
                }
                #endregion

                #region // Cập nhật nếu là cấp trên thì được "duyệt cấp 1"
                if (CurrentLevelApprove == 1)
                {
                    DisabledSaveAndSend = false;
                    var id = context.HR_tblEmp.Where(p => p.EmpID == empIDPlan).Select(p => p.LineManagerID).FirstOrDefault();
                    if (currentEmpId == id)
                    {
                        DisabledApproveAndUnapprove = true;
                    }
                    else
                    {
                        DisabledApproveAndUnapprove = false;
                    }
                }
                #endregion
            }            
            else
            {
                DisabledSaveAndSend = false;
                DisabledApproveAndUnapprove = false;
            }
        }

        public static Dictionary<int?, string> GetGenderList()
        {
            Dictionary<int?, string> ret = new Dictionary<int?, string>();
            ret.Add(0, Eagle.Resource.LanguageResource.Male);
            ret.Add(1, Eagle.Resource.LanguageResource.Female);
            ret.Add(-1, Eagle.Resource.LanguageResource.NonSpecified);
            return ret;
        }

        public static Dictionary<int?, string> GetWorkTypeList()
        {
            Dictionary<int?, string> ret = new Dictionary<int?, string>();
            ret.Add(1, Eagle.Resource.LanguageResource.FullTime);
            ret.Add(2, Eagle.Resource.LanguageResource.PartTime);
            return ret;
        }

        public static SelectList LoadMonthList(string SelectedValue, int LanguageId)
        {
            SelectList month_lst = new SelectList(new[] { new { Text = Eagle.Resource.LanguageResource.Select, Value = (string)SelectedValue } }.Concat(
                 Enumerable.Range(1, 12).Select(r => new
                 {
                     Text = ((LanguageId == 124) ? DateTimeFormatInfo.CurrentInfo.GetMonthName(r) : string.Format("{0} {1}", Eagle.Resource.LanguageResource.Month, r)),
                     Value = r.ToString()
                 })), "Value", "Text", SelectedValue);
            return month_lst;
        }

        public static SelectList LoadYearList(int NumberOfYears, string SelectedValue, bool IsShowSelectText = false)
        {
            //var months = Enumerable.Range(1, 12).Select(x => new SelectListItem { Value = x.ToString("0"), Text =System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x) });
            List<SelectListItem> list = new List<SelectListItem>();
            int startPoint = DateTime.Today.Year - NumberOfYears;
            list = Enumerable.Range(startPoint + 1, (DateTime.Now.Year - startPoint)).Reverse().Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList();
            if (list.Count == 0)
                list.Insert(0, new SelectListItem() { Value = "-1", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.None) });
            else
            {
                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem() { Value = "-1", Text = string.Format("-- {0} --", Eagle.Resource.LanguageResource.Select) });
            }
            var selectList = new SelectList(list, "Value", "Text", SelectedValue);
            return selectList;
        }


        public static SelectList PopulateArticleImageSizes(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.LargeImage, Value = "600×600" });
             lst.Add(new SelectListItem { Text = Resource.LanguageResource.MediumImage, Value = "360×360" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.SmallImage, Value = "200×200" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public static SelectList PopulateSummaryImageSizes(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.LargeImage, Value = "150×150" });
             lst.Add(new SelectListItem { Text = Resource.LanguageResource.MediumImage, Value = "100×100" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.SmallImage, Value = "50×50" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public static SelectList PopulatePermisionGroups(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.SystemPage, Value = "SYSTEM_PAGE" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.SystemModule, Value = "SYSTEM_MODULE" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.SystemMenu, Value = "SYSTEM_MENU" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public static SelectList PopulateScopeTypeList(string SelectedValue, bool IsShowSelectText = false)
        {
            //return new SelectList(new[] 
            //    {
            //        new SelectListItem{ Text = Resource.LanguageResource.Host, Value = "1" },
            //        new SelectListItem{ Text = Resource.LanguageResource.Site, Value = "2" },      
            //        new SelectListItem{ Text = Resource.LanguageResource.Desktop, Value = "3" }
            //    }, "ScopeTypeId", "ScopeTypeName");

            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Host, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Site, Value = "2" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Desktop, Value = "3" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public SelectList PopulateActionList(string SelectedValue)
        {
            //@Html.DropDownListFor(model=>model.ActionId, Model.ActionsList)
            ActionModel model = new ActionModel();
            IEnumerable<ActionType> actionTypes = Enum.GetValues(typeof(ActionType)).Cast<ActionType>();
            List<SelectListItem> lst = (from action in actionTypes
                                        select new SelectListItem
                                        {
                                            Text = action.ToString(),
                                            Value = ((int)action).ToString()
                                        }).ToList();
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }
     

        public static SelectList GetAlignmentList(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Left, Value = "Left" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Center, Value = "Center" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Right, Value = "Right" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public static SelectList PopulateLanguageTypes(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Vietnamese, Value = "vi-VN" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.English, Value = "en-Us" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }


        #region Status Mode ============================================================================================================

        public static List<DropdownListItem> WhyStatus()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "3", value = "Hư/Hỏng"}, 
                new DropdownListItem{key = "4", value = "Mất"}
            };
            return lst;
        }

        public static List<DropdownListItem> ACStatus()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "2", value = "Sử dụng"}, 
                new DropdownListItem{key = "3", value = "Hư hỏng"}, 
                new DropdownListItem{key = "4", value = "Mất"}, 
                new DropdownListItem{key = "5", value = "Phó Bản"}
            };
            return lst;
        }
        public static Dictionary<int?, string> Status()
        {
            Dictionary<int?, string> ret = new Dictionary<int?, string>();
            ret.Add(1, Eagle.Resource.LanguageResource.Open);
            ret.Add(2, Eagle.Resource.LanguageResource.Closed);
            return ret;
        }
   

        public static SelectList GenerateThreeStatusModeList()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Published, Value = "2" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            return new SelectList(lst, "Value", "Text", null);
        }

        public static SelectList GenerateThreeStatusModeList(string SelectedValue)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Published, Value = "2" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public static SelectList GenerateThreeStatusModeList(string SelectedValue = null, bool? IsShowSelectText = true)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Published, Value = "2" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            if (IsShowSelectText != null && IsShowSelectText == true)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", null);
        }

        public static SelectList GenerateThreeStatusModeListWithOptionText(string OptionText = null, bool? IsShowSelectText = true)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Published, Value = "2" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            if (IsShowSelectText != null && IsShowSelectText == true)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", string.IsNullOrEmpty(OptionText) ? Eagle.Resource.LanguageResource.Select : OptionText), Value = "" });
            return new SelectList(lst, "Value", "Text", null);
        }

        public static SelectList GenerateThreeStatusModeListWithOptionText(string SelectedValue = null, string OptionText = null, bool? IsShowSelectText = true)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Published, Value = "2" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            if (IsShowSelectText != null && IsShowSelectText == true)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", string.IsNullOrEmpty(OptionText) ? Eagle.Resource.LanguageResource.Select : OptionText), Value = "" });

            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public static SelectList GenerateTwoStatusModeList()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            return new SelectList(lst, "Value", "Text", null);
        }
        public static SelectList GenerateTwoStatusModeList(string SelectedValue)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }   
        public static SelectList GenerateTwoStatusModeList(string SelectedValue = null, bool? IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "True" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "False" });
            if (IsShowSelectText != null && IsShowSelectText == true)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }
        public static SelectList GenerateTwoStatusModeListWithOptionText(string OptionText = null, bool? IsShowSelectText = true)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            if (IsShowSelectText != null && IsShowSelectText == true)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", string.IsNullOrEmpty(OptionText) ? Eagle.Resource.LanguageResource.Select : OptionText), Value = "" });

            return new SelectList(lst, "Value", "Text", null);
        }
        public static SelectList GenerateTwoStatusModeListWithOptionText(string SelectedValue = null, string OptionText = null, bool? IsShowSelectText = true)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "0" });
            if (IsShowSelectText != null && IsShowSelectText == true)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", string.IsNullOrEmpty(OptionText) ? Eagle.Resource.LanguageResource.Select : OptionText), Value = "" });

            return new SelectList(lst, "Value", "Text", SelectedValue);
        }
        #endregion  Status Mode ===========================================================================================================

        public static SelectList PopulateLinkTargets(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.LoadInANewWindow, Value = "_blank" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.LoadInTheSameFrameAsItWasClicked, Value = "_self" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.LoadInTheParentFrameset, Value = "_parent" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.LoadInTheFullBodyOfTheWindow, Value = "_top" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }

        public static SelectList PopulateIsSecured(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.IsAdmin, Value = "1" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.IsDesktop, Value = "0" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }


        #region HR============================================================================================================
        public static Dictionary<int?, string> GetProjectResult()
        {
            Dictionary<int?, string> ret = new Dictionary<int?, string>();
            ret.Add(-1, Eagle.Resource.LanguageResource.All);
            ret.Add(0, Eagle.Resource.LanguageResource.ProjectResult_0);
            ret.Add(1, Eagle.Resource.LanguageResource.ProjectResult_1);
            ret.Add(2, Eagle.Resource.LanguageResource.ProjectResult_2);
            return ret;
        }

        public static Dictionary<int?, string> StatusForTransferCandidate()
        {
            Dictionary<int?, string> ret = new Dictionary<int?, string>();
            ret.Add(1, Eagle.Resource.LanguageResource.Result_1);
            ret.Add(2, Eagle.Resource.LanguageResource.Result_2);
            ret.Add(5, Eagle.Resource.LanguageResource.Result_5);
            return ret;
        }

        public static Dictionary<int?, string> GetCadidateResult()
        {
            Dictionary<int?, string> ret = new Dictionary<int?, string>();
            ret.Add(0, Eagle.Resource.LanguageResource.Result_0);
            ret.Add(1, Eagle.Resource.LanguageResource.Result_1);
            ret.Add(2, Eagle.Resource.LanguageResource.Result_2);
            ret.Add(6, Eagle.Resource.LanguageResource.Result_6);
            ret.Add(3, Eagle.Resource.LanguageResource.Result_3);
            ret.Add(5, Eagle.Resource.LanguageResource.Result_5);
            ret.Add(4, Eagle.Resource.LanguageResource.Result_4);
            return ret;
        }


        public static Dictionary<int?, string> GetReached()
        {
            Dictionary<int?, string> ret = new Dictionary<int?, string>();
            ret.Add(1, Eagle.Resource.LanguageResource.NotReached);
            ret.Add(2, Eagle.Resource.LanguageResource.Pass);
            ret.Add(3, Eagle.Resource.LanguageResource.NotPass);
            return ret;
        }

      
        //List Year đồng dung form Contract
        public static Dictionary<string, string> ListDataYearRevenue()
        {
            Dictionary<string, string> yearRevenue =
            new Dictionary<string, string>();
            int currenyear = DateTime.Now.Year;
            for (int i = currenyear - 3; i <= currenyear + 1; i++)
            {
                yearRevenue.Add(i.ToString(), i.ToString());
            }
            return yearRevenue;

        }
        //List Year đồng dung form Contract
        public static Dictionary<string, string> ListDataMonthRevenue()
        {
            Dictionary<string, string> monthRevenue =
            new Dictionary<string, string>();
            int currenyear = DateTime.Now.Year;
            for (int i = 1; i <= 12; i++)
            {
                monthRevenue.Add(i.ToString(), i.ToString());
            }
            return monthRevenue;

        }
        //List HTTT đồng dung form Contract
        public static List<DropdownListItem> ListDataHTTT()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "1", value = "Chuyển khoản" , desc = ""}, 
                new DropdownListItem{key = "2", value = "Tiền mặt" , desc = ""},                           
            };
            return lst;
        }
        //List loại tiền dung form Contract
        public static List<DropdownListItem> ListDataTypeMoney()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "1", value = "VND" , desc = "Đồng Việt Nam"}, 
                new DropdownListItem{key = "2", value = "USD" , desc = "Dola Mỹ"},   
                new DropdownListItem{key = "3", value = "SGD" , desc = "Dola Singapore"},          
            };
            return lst;

        }
        public static List<DropdownListItem> DataTypeContract()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "1", value = "VND" , desc = "Đồng Việt Nam"}, 
                new DropdownListItem{key = "2", value = "USD" , desc = "Dola Mỹ"},   
                new DropdownListItem{key = "3", value = "SGD" , desc = "Dola Singapore"},          
            };
            return lst;

        }

        //List Loại Hình Hợp đồng dung form Contract
        public static List<DropdownListItem> ListTypeHinh()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "1", value = "1" , desc = "Khai thác trực tiếp"}, 
                new DropdownListItem{key = "2", value = "2" , desc = "Khai thác qua đại lý"}, 
                new DropdownListItem{key = "3", value = "3" , desc = "Khai thác qua môi giới"},               
            };
            return lst;
        }
       

        /// <summary>
        /// Số chỗ ngồi
        /// </summary>
        /// <returns></returns>
        public static List<DropdownListItem> NumberOfSeats()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "2", value = "2"},
                new DropdownListItem{key = "4", value = "4"},
                new DropdownListItem{key = "5", value = "5"},
                new DropdownListItem{key = "6", value = "6"},
                new DropdownListItem{key = "7", value = "7"},
                new DropdownListItem{key = "8", value = "8"},
                new DropdownListItem{key = "9", value = "9"},
                new DropdownListItem{key = "10", value = "10"},
                new DropdownListItem{key = "11", value = "11"},
                new DropdownListItem{key = "12", value = "12"},
                new DropdownListItem{key = "13", value = "13"},
                new DropdownListItem{key = "14", value = "14"},
                new DropdownListItem{key = "15", value = "15"},
                new DropdownListItem{key = "16", value = "16"},
                new DropdownListItem{key = "17", value = "17"},
                new DropdownListItem{key = "18", value = "18"},
                new DropdownListItem{key = "19", value = "19"},
                new DropdownListItem{key = "20", value = "20"},
                new DropdownListItem{key = "21", value = "21"},
                new DropdownListItem{key = "22", value = "22"},
                new DropdownListItem{key = "23", value = "23"},
                new DropdownListItem{key = "24", value = "24"},
                new DropdownListItem{key = "25", value = "25"},
                new DropdownListItem{key = "26", value = "26"},
                new DropdownListItem{key = "27", value = "27"},
                new DropdownListItem{key = "28", value = "28"},
                new DropdownListItem{key = "29", value = "29"},
                new DropdownListItem{key = "30", value = "30"},
                new DropdownListItem{key = "31", value = "31"},
                new DropdownListItem{key = "32", value = "32"},
                new DropdownListItem{key = "33", value = "33"},
                new DropdownListItem{key = "34", value = "34"},
                new DropdownListItem{key = "35", value = "35"},
                new DropdownListItem{key = "36", value = "36"},
                new DropdownListItem{key = "37", value = "37"},
                new DropdownListItem{key = "38", value = "38"},
                new DropdownListItem{key = "39", value = "39"},
                new DropdownListItem{key = "40", value = "40"},
                new DropdownListItem{key = "42", value = "42"},
                new DropdownListItem{key = "43", value = "43"},
                new DropdownListItem{key = "44", value = "44"},
                new DropdownListItem{key = "45", value = "45"},
                new DropdownListItem{key = "46", value = "46"},
                new DropdownListItem{key = "47", value = "47"},
                new DropdownListItem{key = "48", value = "48"},
                new DropdownListItem{key = "50", value = "50"},
                new DropdownListItem{key = "51", value = "51"},
                new DropdownListItem{key = "55", value = "55"},
                new DropdownListItem{key = "60", value = "60"},
                new DropdownListItem{key = "80", value = "80"}
            };
            return lst;
        }

        public static List<DropdownListItem> GetGender()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "0", value = "Nữ"},
                new DropdownListItem{key = "1", value = "Nam"}
            };
            return lst;
        }

        public static List<DropdownListItem> GetTax()
        {
            List<DropdownListItem> lst = new List<DropdownListItem>() { 
                new DropdownListItem{key = "0", value = "0%"},
                new DropdownListItem{key = "5", value = "5%"},
                new DropdownListItem{key = "10", value = "10%"}
            };
            return lst;
        }


        #endregion ===========================================================================================================

        #region File ============================================================================================================

        public static List<DropdownListItem> GetFiles()
        {
            List<DropdownListItem> lstFile = new List<DropdownListItem>();
            lstFile.Add(new DropdownListItem { key = "ai", value = "/Images/ImageType/ai.png" });
            lstFile.Add(new DropdownListItem { key = "bmp", value = "/Images/ImageType/bmp.png" });
            lstFile.Add(new DropdownListItem { key = "doc", value = "/Images/ImageType/doc.png" });
            lstFile.Add(new DropdownListItem { key = "docx", value = "/Images/ImageType/doc.png" });
            lstFile.Add(new DropdownListItem { key = "xls", value = "/Images/ImageType/xls.png" });
            lstFile.Add(new DropdownListItem { key = "xlsx", value = "/Images/ImageType/xls.png" });
            lstFile.Add(new DropdownListItem { key = "ppt", value = "/Images/ImageType/ppt.png" });
            lstFile.Add(new DropdownListItem { key = "pptx", value = "/Images/ImageType/ppt.png" });
            lstFile.Add(new DropdownListItem { key = "eps", value = "/Images/ImageType/eps.png" });
            lstFile.Add(new DropdownListItem { key = "exe", value = "/Images/ImageType/exe.png" });
            lstFile.Add(new DropdownListItem { key = "gif", value = "/Images/ImageType/gif.png" });
            lstFile.Add(new DropdownListItem { key = "jpg", value = "/Images/ImageType/jpg.png" });
            lstFile.Add(new DropdownListItem { key = "jpeg", value = "/Images/ImageType/jpg.png" });
            lstFile.Add(new DropdownListItem { key = "pdf", value = "/Images/ImageType/pdf.png" });
            lstFile.Add(new DropdownListItem { key = "png", value = "/Images/ImageType/png.png" });
            lstFile.Add(new DropdownListItem { key = "psd", value = "/Images/ImageType/psd.png" });
            lstFile.Add(new DropdownListItem { key = "tiff", value = "/Images/ImageType/tiff.png" });
            lstFile.Add(new DropdownListItem { key = "Data", value = "/Images/ImageType/Data.png" });
            lstFile.Add(new DropdownListItem { key = "mp3", value = "/Images/ImageType/mp3.png" });
            lstFile.Add(new DropdownListItem { key = "rar", value = "/Images/ImageType/rar.png" });
            lstFile.Add(new DropdownListItem { key = "zip", value = "/Images/ImageType/rar.png" });
            lstFile.Add(new DropdownListItem { key = "mp4", value = "/Images/ImageType/video.png" });
            lstFile.Add(new DropdownListItem { key = "avi", value = "/Images/ImageType/video.png" });
            lstFile.Add(new DropdownListItem { key = "flv", value = "/Images/ImageType/video.png" });
            return lstFile;
        }
    
            #endregion ===========================================================================================================

        #region HIEARACHY ====================================================================================================
        public static List<TreeNode> RecursiveFillTreeNodes(List<TreeNode> elements, int? parentid)
        {
            List<TreeNode> items = new List<TreeNode>();
            List<TreeNode> children = elements.Where(p => p.parentid == ((parentid == 0) ? null : parentid)).Select(
               p => new TreeNode
               {
                   id = p.id,
                   text = p.text,
                   parentid = p.parentid,
                   depth = p.depth
               }).ToList();

            if (children.Count > 0)
            {
                foreach (var child in children)
                {
                    TreeNode node = new TreeNode()
                    {
                        id = child.id,
                        parentid = child.parentid,
                        text = child.text,
                        depth = child.depth,
                        children = RecursiveFillTreeNodes(elements, child.id)
                    };
                    items.Add(node);
                }
            }
            return items;
        }

        public static List<TreeEntity> RecursiveFillTreeEntities(List<TreeEntity> elements, int? parentid)
        {
            List<TreeEntity> items = new List<TreeEntity>();
            List<TreeEntity> children = elements.Where(p => p.parentid == ((parentid == 0) ? null : parentid)).Select(
               p => new TreeEntity
               {
                   key = p.key,
                   title = p.title,
                   parentid = p.parentid,
                   depth = p.depth,
                   folder = (p.depth > 1) ? true : false,
                   lazy = (p.depth > 1) ? true : false,
                   expanded = (p.depth > 1) ? true : false,
                   tooltip = p.tooltip
               }).ToList();

            if (children.Count > 0)
            {
                foreach (var child in children)
                {
                    TreeEntity node = new TreeEntity()
                    {
                        key = child.key,
                        parentid = child.parentid,
                        title = child.title,
                        depth = child.depth,
                        folder = (child.depth > 1) ? true : false,
                        lazy = (child.depth > 1) ? true : false,
                        expanded = (child.depth > 1) ? true : false,
                        tooltip = child.tooltip,
                        children = RecursiveFillTreeEntities(elements, child.key)
                    };
                    items.Add(node);
                }
            }
            return items;
        }
        #endregion ===========================================================================================================
    }
}
