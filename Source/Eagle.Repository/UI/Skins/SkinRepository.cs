using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using Eagle.Common.Settings;
using Eagle.Core;
using System.Web.Mvc;
using System.Configuration;
using Eagle.Model.UI.Skins;
using Eagle.Repository.UI.Layout;
using System.Web;
namespace CommonLibrary.UI.Skins
{
    public class SkinRepository 
    {
        private SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        private DataTable dt = new DataTable();

        //private ArrayList _actionEventListeners;
        //private ModuleCommunicate _Communicator = new ModuleCommunicate();
        //private Control _ControlPanel;
        //private Dictionary<string, Pane> _panes;
        //private string _skinSrc;
        //public string PANE_LOAD_ERROR = Localization.GetString("PaneNotFound.Error");
        //public string CONTRACTEXPIRED_ERROR = Localization.GetString("ContractExpired.Error");
        //public string TABACCESS_ERROR = Localization.GetString("TabAccess.Error");
        //public string MODULEACCESS_ERROR = Localization.GetString("ModuleAccess.Error");
        //public string CRITICAL_ERROR = Localization.GetString("CriticalError.Error");
        //public string MODULELOAD_WARNING = Localization.GetString("ModuleLoadWarning.Error");
        //public string MODULELOAD_WARNINGTEXT = Localization.GetString("ModuleLoadWarning.Text");
        //public static string MODULELOAD_ERROR = Localization.GetString("ModuleLoad.Error");
        //public static string CONTAINERLOAD_ERROR = Localization.GetString("ContainerLoad.Error");
        //public static string MODULEADD_ERROR = Localization.GetString("ModuleAdd.Error");

       

        //internal Control ControlPanel
        //{
        //    get
        //    {
        //        if (_ControlPanel == null)
        //        {
        //            _ControlPanel = FindControl("ControlPanel");
        //        }
        //        return _ControlPanel;
        //    }
        //}
        //internal ModuleCommunicate Communicator
        //{
        //    get { return _Communicator; }
        //}
        //internal Dictionary<string, Pane> Panes
        //{
        //    get
        //    {
        //        if (_panes == null)
        //        {
        //            _panes = new Dictionary<string, Pane>();
        //        }
        //        return _panes;
        //    }
        //}
        //public ArrayList ActionEventListeners
        //{
        //    get
        //    {
        //        if (_actionEventListeners == null)
        //        {
        //            _actionEventListeners = new ArrayList();
        //        }
        //        return _actionEventListeners;
        //    }
        //    set { _actionEventListeners = value; }
        //}
        //public string SkinPath
        //{
        //    get { return this.TemplateSourceDirectory + "/"; }
        //}
        //public string SkinSrc
        //{
        //    get { return _skinSrc; }
        //    set { _skinSrc = value; }
        //}
        //private bool CheckExpired()
        //{
        //    bool blnExpired = false;
        //    if (ApplicationSettings.ExpiryDate != Null.NullDate)
        //    {
        //        if (Convert.ToDateTime(ApplicationSettings.ExpiryDate) < DateTime.Now && ApplicationSettings.ActiveTab.ParentId != ApplicationSettings.SuperTabId)
        //        {
        //            blnExpired = true;
        //        }
        //    }
        //    return blnExpired;
        //}

      

        //private bool InjectControlPanel()
        //{
        //    if (Request.QueryString["dnnprintmode"] != "true")
        //    {
        //        ControlPanelBase objControlPanel = ControlUtilities.LoadControl<ControlPanelBase>(this, Host.ControlPanel);
        //        if (ControlPanel == null)
        //        {
        //            HtmlForm objForm = (HtmlForm)Parent.FindControl("Form");
        //            if (objForm != null)
        //            {
        //                objForm.Controls.AddAt(0, objControlPanel);
        //            }
        //            else
        //            {
        //                Page.Controls.AddAt(0, objControlPanel);
        //            }
        //        }
        //        else
        //        {
        //            ControlPanel.Controls.Add(objControlPanel);
        //        }
        //    }
        //    return true;
        //}
        //private void LoadPanes()
        //{
        //    HtmlContainerControl objPaneControl;
        //    foreach (Control ctlControl in Controls)
        //    {
        //        if (ctlControl is HtmlContainerControl)
        //        {
        //            objPaneControl = ctlControl as HtmlContainerControl;
        //            if (objPaneControl != null && !string.IsNullOrEmpty(objPaneControl.ID))
        //            {
        //                switch (objPaneControl.TagName.ToLowerInvariant())
        //                {
        //                    case "td":
        //                    case "div":
        //                    case "span":
        //                    case "p":
        //                        if (objPaneControl.ID.ToLower() != "controlpanel")
        //                        {
        //                            ApplicationSettings.ActiveTab.Panes.Add(objPaneControl.ID);
        //                            Panes.Add(objPaneControl.ID.ToLowerInvariant(), new Pane(objPaneControl));
        //                        }
        //                        else
        //                        {
        //                            _ControlPanel = objPaneControl;
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //}
        //private bool ProcessMasterModules()
        //{
        //    bool bSuccess = true;
        //    if (TabPermissionController.CanViewPage())
        //    {
        //        if (!CheckExpired())
        //        {
        //            if ((ApplicationSettings.ActiveTab.StartDate < DateTime.Now && ApplicationSettings.ActiveTab.EndDate > DateTime.Now) || Globals.IsLayoutMode())
        //            {
        //                if (ApplicationSettings.ActiveTab.Modules.Count > 0)
        //                {
        //                    foreach (ModuleInfo objModule in ApplicationSettings.ActiveTab.Modules)
        //                    {
        //                        if (ModulePermissionController.CanViewModule(objModule) && objModule.IsDeleted == false)
        //                        {
        //                            if ((objModule.StartDate < DateTime.Now && objModule.EndDate > DateTime.Now) || Common.Globals.IsLayoutMode() || Common.Globals.IsEditMode())
        //                            {
        //                                Pane pane = null;
        //                                bool bFound = Panes.TryGetValue(objModule.PaneName.ToLowerInvariant(), out pane);
        //                                if (!bFound)
        //                                {
        //                                    bFound = Panes.TryGetValue(Common.Globals.glbDefaultPane.ToLowerInvariant(), out pane);
        //                                }
        //                                if (bFound)
        //                                {
        //                                    bSuccess = InjectModule(pane, objModule);
        //                                }
        //                                else
        //                                {
        //                                    ModuleLoadException lex;
        //                                    lex = new ModuleLoadException(PANE_LOAD_ERROR);
        //                                    Controls.Add(new ErrorContainer(ApplicationSettings, MODULELOAD_ERROR, lex).Container);
        //                                    Exceptions.LogException(lex);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                AddPageMessage(this, "", TABACCESS_ERROR, UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
        //            }
        //        }
        //        else
        //        {
        //            AddPageMessage(this, "", string.Format(CONTRACTEXPIRED_ERROR, ApplicationSettings.ApplicationName, Common.Globals.GetMediumDate(ApplicationSettings.ExpiryDate.ToString()), ApplicationSettings.Email), UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect(Common.Globals.AccessDeniedURL(TABACCESS_ERROR), true);
        //    }
        //    return bSuccess;
        //}
        //private void ProcessPanes()
        //{
        //    foreach (KeyValuePair<string, Pane> kvp in Panes)
        //    {
        //        kvp.Value.ProcessPane();
        //    }
        //}
        //private bool ProcessSlaveModule()
        //{
        //    ModuleController objModules = new ModuleController();
        //    ModuleInfo objModule = null;
        //    ModuleInfo slaveModule = null;
        //    int moduleId = -1;
        //    string key = "";
        //    bool bSuccess = true;
        //    if (Request.QueryString["mid"] != null)
        //    {
        //        Int32.TryParse(Request.QueryString["mid"], out moduleId);
        //    }
        //    if (Request.QueryString["ctl"] != null)
        //    {
        //        key = Request.QueryString["ctl"];
        //    }
        //    if (Request.QueryString["moduleid"] != null && (key.ToLower() == "module" || key.ToLower() == "help"))
        //    {
        //        Int32.TryParse(Request.QueryString["moduleid"], out moduleId);
        //    }
        //    if (moduleId != -1)
        //    {
        //        objModule = objModules.GetModule(moduleId, ApplicationSettings.ActiveTab.TabID, false);
        //        if (objModule != null)
        //        {
        //            slaveModule = objModule.Clone();
        //        }
        //    }
        //    if (slaveModule == null)
        //    {
        //        slaveModule = new ModuleInfo();
        //        slaveModule.ModuleID = moduleId;
        //        slaveModule.ModuleDefID = -1;
        //        slaveModule.TabID = ApplicationSettings.ActiveTab.TabID;
        //    }
        //    if (Request.QueryString["moduleid"] != null && (key.ToLower() == "module" || key.ToLower() == "help"))
        //    {
        //        slaveModule.ModuleDefID = -1;
        //    }
        //    if (Request.QueryString["dnnprintmode"] != "true")
        //    {
        //        slaveModule.ModuleTitle = "";
        //    }
        //    slaveModule.Header = "";
        //    slaveModule.Footer = "";
        //    slaveModule.StartDate = DateTime.MinValue;
        //    slaveModule.EndDate = DateTime.MaxValue;
        //    slaveModule.PaneName = Common.Globals.glbDefaultPane;
        //    slaveModule.Visibility = VisibilityState.None;
        //    slaveModule.Color = "";
        //    slaveModule.Border = "";
        //    slaveModule.DisplayTitle = true;
        //    slaveModule.DisplayPrint = false;
        //    slaveModule.DisplaySyndicate = false;
        //    slaveModule.ContainerSrc = ApplicationSettings.ActiveTab.ContainerSrc;
        //    if (string.IsNullOrEmpty(slaveModule.ContainerSrc))
        //    {
        //        slaveModule.ContainerSrc = ApplicationSettings.DefaultApplicationContainer;
        //    }
        //    slaveModule.ContainerSrc = SkinController.FormatSkinSrc(slaveModule.ContainerSrc, ApplicationSettings);
        //    slaveModule.ContainerPath = SkinController.FormatSkinPath(slaveModule.ContainerSrc);
        //    Pane pane = null;
        //    bool bFound = Panes.TryGetValue(Common.Globals.glbDefaultPane.ToLowerInvariant(), out pane);
        //    ModuleControlInfo objModuleControl = ModuleControlController.GetModuleControlByControlKey(key, slaveModule.ModuleDefID);
        //    if (objModuleControl != null)
        //    {
        //        slaveModule.ModuleControlId = objModuleControl.ModuleControlID;
        //        slaveModule.IconFile = objModuleControl.IconFile;
        //        if (ModulePermissionController.HasModuleAccess(slaveModule.ModuleControl.ControlType, Null.NullString, slaveModule))
        //        {
        //            bSuccess = InjectModule(pane, slaveModule);
        //        }
        //        else
        //        {
        //            Response.Redirect(Common.Globals.AccessDeniedURL(MODULEACCESS_ERROR), true);
        //        }
        //    }
        //    return bSuccess;
        //}
        //protected override void OnInit(System.EventArgs e)
        //{
        //    base.OnInit(e);
        //    bool bSuccess = true;
        //    LoadPanes();
        //    if (!Common.Globals.IsAdminControl())
        //    {
        //        bSuccess = ProcessMasterModules();
        //    }
        //    else
        //    {
        //        bSuccess = ProcessSlaveModule();
        //    }
        //    InjectControlPanel();
        //    ProcessPanes();
        //    if (Request.QueryString["error"] != null)
        //    {
        //        AddPageMessage(this, CRITICAL_ERROR, Server.HtmlEncode(Request.QueryString["error"]), UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
        //    }
        //    if (!TabPermissionController.CanAdminPage())
        //    {
        //        if (!bSuccess)
        //        {
        //            AddPageMessage(this, MODULELOAD_WARNING, string.Format(MODULELOAD_WARNINGTEXT, ApplicationSettings.Email), UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
        //        }
        //    }
        //    foreach (SkinEventListener listener in AppContext.Current.SkinEventListeners)
        //    {
        //        if (listener.EventType == SkinEventType.OnSkinInit)
        //        {
        //            listener.SkinEvent.Invoke(this, new SkinEventArgs(this));
        //        }
        //    }
        //}
        //protected override void OnLoad(System.EventArgs e)
        //{
        //    base.OnLoad(e);
        //    foreach (SkinEventListener listener in AppContext.Current.SkinEventListeners)
        //    {
        //        if (listener.EventType == SkinEventType.OnSkinLoad)
        //        {
        //            listener.SkinEvent.Invoke(this, new SkinEventArgs(this));
        //        }
        //    }
        //}
        //protected override void OnPreRender(System.EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    foreach (SkinEventListener listener in AppContext.Current.SkinEventListeners)
        //    {
        //        if (listener.EventType == SkinEventType.OnSkinPreRender)
        //        {
        //            listener.SkinEvent.Invoke(this, new SkinEventArgs(this));
        //        }
        //    }
        //}
        //protected override void OnUnload(EventArgs e)
        //{
        //    base.OnUnload(e);
        //    foreach (SkinEventListener listener in AppContext.Current.SkinEventListeners)
        //    {
        //        if (listener.EventType == SkinEventType.OnSkinUnLoad)
        //        {
        //            listener.SkinEvent.Invoke(this, new SkinEventArgs(this));
        //        }
        //    }
        //}
        //public bool InjectModule(Pane objPane, ModuleInfo objModule)
        //{
        //    bool bSuccess = true;
        //    try
        //    {
        //        objPane.InjectModule(objModule);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        bSuccess = false;
        //    }
        //    return bSuccess;
        //}
        //public void RegisterModuleActionEvent(int ModuleID, ActionEventHandler e)
        //{
        //    ActionEventListeners.Add(new ModuleActionEventListener(ModuleID, e));
        //}
        //private static Skin LoadSkin(PageBase Page, string SkinPath)
        //{
        //    Skin ctlSkin = null;
        //    try
        //    {
        //        string SkinSrc = SkinPath;
        //        if (SkinPath.ToLower().IndexOf(Common.Globals.ApplicationPath) != -1)
        //        {
        //            SkinPath = SkinPath.Remove(0, Common.Globals.ApplicationPath.Length);
        //        }
        //        ctlSkin = ControlUtilities.LoadControl<Skin>(Page, SkinPath);
        //        ctlSkin.SkinSrc = SkinSrc;
        //        ctlSkin.DataBind();
        //    }
        //    catch (Exception exc)
        //    {
        //        PageLoadException lex = new PageLoadException("Unhandled error loading page.", exc);
        //        if (TabPermissionController.CanAdminPage())
        //        {
        //            Label SkinError = (Label)Page.FindControl("SkinError");
        //            SkinError.Text = string.Format(Localization.GetString("SkinLoadError", Localization.GlobalResourceFile), SkinPath, Page.Server.HtmlEncode(exc.Message));
        //            SkinError.Visible = true;
        //        }
        //        Exceptions.LogException(lex);
        //    }
        //    return ctlSkin;
        //}
        //private static void AddModuleMessage(Control objControl, string Heading, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType, string IconSrc)
        //{
        //    if (objControl != null)
        //    {
        //        if (!String.IsNullOrEmpty(Message))
        //        {
        //            PlaceHolder MessagePlaceHolder = (PlaceHolder)objControl.Parent.FindControl("MessagePlaceHolder");
        //            if (MessagePlaceHolder != null)
        //            {
        //                MessagePlaceHolder.Visible = true;
        //                UI.Skins.Controls.ModuleMessage objModuleMessage;
        //                objModuleMessage = GetModuleMessageControl(Heading, Message, objModuleMessageType, IconSrc);
        //                MessagePlaceHolder.Controls.Add(objModuleMessage);
        //            }
        //        }
        //    }
        //}
        //private static void AddPageMessage(Control objControl, string Heading, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType, string IconSrc)
        //{
        //    if (!String.IsNullOrEmpty(Message))
        //    {
        //        Control ContentPane = (Control)objControl.FindControl(Common.Globals.glbDefaultPane);
        //        if (ContentPane != null)
        //        {
        //            UI.Skins.Controls.ModuleMessage objModuleMessage;
        //            objModuleMessage = GetModuleMessageControl(Heading, Message, objModuleMessageType, IconSrc);
        //            ContentPane.Controls.AddAt(0, objModuleMessage);
        //        }
        //    }
        //}
        //public static void AddModuleMessage(ApplicationModuleBase objControl, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType)
        //{
        //    AddModuleMessage(objControl, "", Message, objModuleMessageType, Null.NullString);
        //}
        //public static void AddModuleMessage(ApplicationModuleBase objControl, string Heading, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType)
        //{
        //    AddModuleMessage(objControl, Heading, Message, objModuleMessageType, Null.NullString);
        //}
        //public static void AddModuleMessage(Control objControl, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType)
        //{
        //    AddModuleMessage(objControl, "", Message, objModuleMessageType, Null.NullString);
        //}
        //public static void AddModuleMessage(Control objControl, string Heading, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType)
        //{
        //    AddModuleMessage(objControl, Heading, Message, objModuleMessageType, Null.NullString);
        //}
        //public static void AddPageMessage(Page objPage, string Heading, string Message, string IconSrc)
        //{
        //    AddPageMessage(objPage, Heading, Message, ModuleMessage.ModuleMessageType.GreenSuccess, IconSrc);
        //}
        //public static void AddPageMessage(UI.Skins.Skin objSkin, string Heading, string Message, string IconSrc)
        //{
        //    AddPageMessage(objSkin, Heading, Message, ModuleMessage.ModuleMessageType.GreenSuccess, IconSrc);
        //}
        //public static void AddPageMessage(UI.Skins.Skin objSkin, string Heading, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType)
        //{
        //    AddPageMessage(objSkin, Heading, Message, objModuleMessageType, Null.NullString);
        //}
        //public static void AddPageMessage(Page objPage, string Heading, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType)
        //{
        //    AddPageMessage(objPage, Heading, Message, objModuleMessageType, Null.NullString);
        //}
        //public static UI.Skins.Controls.ModuleMessage GetModuleMessageControl(string Heading, string Message, string IconImage)
        //{
        //    return GetModuleMessageControl(Heading, Message, ModuleMessage.ModuleMessageType.GreenSuccess, IconImage);
        //}
        //public static UI.Skins.Controls.ModuleMessage GetModuleMessageControl(string Heading, string Message, UI.Skins.Controls.ModuleMessage.ModuleMessageType objModuleMessageType)
        //{
        //    return GetModuleMessageControl(Heading, Message, objModuleMessageType, Null.NullString);
        //}
        //public static UI.Skins.Controls.ModuleMessage GetModuleMessageControl(string Heading, string Message, Controls.ModuleMessage.ModuleMessageType objModuleMessageType, string IconImage)
        //{
        //    UI.Skins.Controls.ModuleMessage objModuleMessage;
        //    UI.Skins.Skin s = new UI.Skins.Skin();
        //    objModuleMessage = (UI.Skins.Controls.ModuleMessage)s.LoadControl("~/admin/skins/ModuleMessage.ascx");
        //    objModuleMessage.Heading = Heading;
        //    objModuleMessage.Text = Message;
        //    objModuleMessage.IconImage = IconImage;
        //    objModuleMessage.IconType = objModuleMessageType;
        //    return objModuleMessage;
        //}
        //public static UI.Skins.Skin GetParentSkin(ApplicationModuleBase objModule)
        //{
        //    return GetParentSkin(objModule as System.Web.UI.Control);
        //}
        //public static UI.Skins.Skin GetParentSkin(System.Web.UI.Control objControl)
        //{
        //    if (objControl != null)
        //    {
        //        System.Web.UI.Control MyParent = objControl.Parent;
        //        bool FoundSkin = false;
        //        while (MyParent != null)
        //        {
        //            if (MyParent is Skin)
        //            {
        //                FoundSkin = true;
        //                break;
        //            }
        //            MyParent = MyParent.Parent;
        //        }
        //        if (FoundSkin)
        //        {
        //            return (Skin)MyParent;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //public static Skin GetSkin(PageBase Page)
        //{
        //    Skin ctlSkin = null;
        //    string skinSource = Null.NullString;
        //    if ((Page.Request.QueryString["SkinSrc"] != null))
        //    {
        //        skinSource = SkinController.FormatSkinSrc(Common.Globals.QueryStringDecode(Page.Request.QueryString["SkinSrc"]) + ".ascx", Page.ApplicationSettings);
        //        ctlSkin = LoadSkin(Page, skinSource);
        //    }
        //    if (ctlSkin == null)
        //    {
        //        if (Page.Request.Cookies["_SkinSrc" + Page.ApplicationSettings.ApplicationId.ToString()] != null)
        //        {
        //            if (!String.IsNullOrEmpty(Page.Request.Cookies["_SkinSrc" + Page.ApplicationSettings.ApplicationId.ToString()].Value))
        //            {
        //                skinSource = SkinController.FormatSkinSrc(Page.Request.Cookies["_SkinSrc" + Page.ApplicationSettings.ApplicationId.ToString()].Value + ".ascx", Page.ApplicationSettings);
        //                ctlSkin = LoadSkin(Page, skinSource);
        //            }
        //        }
        //    }
        //    if (ctlSkin == null)
        //    {
        //        if (Common.Globals.IsAdminSkin())
        //        {
        //            skinSource = SkinController.FormatSkinSrc(Page.ApplicationSettings.DefaultAdminSkin, Page.ApplicationSettings);
        //        }
        //        else
        //        {
        //            skinSource = Page.ApplicationSettings.ActiveTab.SkinSrc;
        //        }
        //        if (!String.IsNullOrEmpty(skinSource))
        //        {
        //            skinSource = SkinController.FormatSkinSrc(skinSource, Page.ApplicationSettings);
        //            ctlSkin = LoadSkin(Page, skinSource);
        //        }
        //    }
        //    if (ctlSkin == null)
        //    {
        //        skinSource = SkinController.FormatSkinSrc(SkinController.GetDefaultApplicationSkin(), Page.ApplicationSettings);
        //        ctlSkin = LoadSkin(Page, skinSource);
        //    }
        //    Page.ApplicationSettings.ActiveTab.SkinPath = SkinController.FormatSkinPath(skinSource);
        //    ctlSkin.ID = "dnn";
        //    return ctlSkin;
        //}

        public static List<Skin> SkinListBy()
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Skins select x;
                return query.ToList();
            }
        }

        //public static int AddSkin(int skinPackageID, string skinSrc)
        //{
        //    return DataProvider.Instance().AddSkin(skinPackageID, skinSrc);
        //}
        //public static int AddSkinPackage(SkinPackageInfo skinPackage)
        //{
        //    Services.Log.EventLog.EventLogController objEventLog = new Services.Log.EventLog.EventLogController();
        //    objEventLog.AddLog(skinPackage, PortalController.GetCurrentPortalSettings(), UserController.GetCurrentUserInfo().UserID, "", CommonLibrary.Services.Log.EventLog.EventLogController.EventLogType.SKINPACKAGE_CREATED);
        //    return DataProvider.Instance().AddSkinPackage(skinPackage.PackageID, skinPackage.PortalID, skinPackage.SkinName, skinPackage.SkinType, UserController.GetCurrentUserInfo().UserID);
        //}
        //public static bool CanDeleteSkin(string strFolderPath, string portalHomeDirMapPath)
        //{
        //    string strSkinType;
        //    string strSkinFolder;
        //    bool canDelete = true;
        //    if (strFolderPath.ToLower().IndexOf(Common.Globals.HostMapPath.ToLower()) != -1)
        //    {
        //        strSkinType = "G";
        //        strSkinFolder = strFolderPath.ToLower().Replace(Common.Globals.HostMapPath.ToLower(), "").Replace("\\", "/");
        //    }
        //    else
        //    {
        //        strSkinType = "L";
        //        strSkinFolder = strFolderPath.ToLower().Replace(portalHomeDirMapPath.ToLower(), "").Replace("\\", "/");
        //    }
        //    PortalSettings portalSettings = PortalController.GetCurrentPortalSettings();
        //    string skin = "[" + strSkinType.ToLowerInvariant() + "]" + strSkinFolder.ToLowerInvariant();
        //    if (strSkinFolder.ToLowerInvariant().Contains("skins"))
        //    {
        //        if (Host.DefaultAdminSkin.ToLowerInvariant().StartsWith(skin) || Host.DefaultPortalSkin.ToLowerInvariant().StartsWith(skin) || portalSettings.DefaultAdminSkin.ToLowerInvariant().StartsWith(skin) || portalSettings.DefaultPortalSkin.ToLowerInvariant().StartsWith(skin))
        //        {
        //            canDelete = false;
        //        }
        //    }
        //    else
        //    {
        //        if (Host.DefaultAdminContainer.ToLowerInvariant().StartsWith(skin) || Host.DefaultPortalContainer.ToLowerInvariant().StartsWith(skin) || portalSettings.DefaultAdminContainer.ToLowerInvariant().StartsWith(skin) || portalSettings.DefaultPortalContainer.ToLowerInvariant().StartsWith(skin))
        //        {
        //            canDelete = false;
        //        }
        //    }
        //    if (canDelete)
        //    {
        //        canDelete = DataProvider.Instance().CanDeleteSkin(strSkinType, strSkinFolder);
        //    }
        //    return canDelete;
        //}
        //public static void DeleteSkin(int skinID)
        //{
        //    DataProvider.Instance().DeleteSkin(skinID);
        //}
        //public static void DeleteSkinPackage(SkinPackageInfo skinPackage)
        //{
        //    DataProvider.Instance().DeleteSkinPackage(skinPackage.SkinPackageID);
        //    Services.Log.EventLog.EventLogController objEventLog = new Services.Log.EventLog.EventLogController();
        //    objEventLog.AddLog(skinPackage, PortalController.GetCurrentPortalSettings(), UserController.GetCurrentUserInfo().UserID, "", CommonLibrary.Services.Log.EventLog.EventLogController.EventLogType.SKINPACKAGE_DELETED);
        //}
        //public static string FormatMessage(string Title, string Body, int Level, bool IsError)
        //{
        //    string Message = Title;
        //    if (IsError)
        //    {
        //        Message = "<font class=\"NormalRed\">" + Title + "</font>";
        //    }
        //    switch (Level)
        //    {
        //        case -1:
        //            Message = "<hr><br><b>" + Message + "</b>";
        //            break;
        //        case 0:
        //            Message = "<br><br><b>" + Message + "</b>";
        //            break;
        //        case 1:
        //            Message = "<br><b>" + Message + "</b>";
        //            break;
        //        default:
        //            Message = "<br><li>" + Message;
        //            break;
        //    }
        //    return Message + ": " + Body + Environment.NewLine;
        //}
        //public static string FormatSkinPath(string SkinSrc)
        //{
        //    string strSkinSrc = SkinSrc;
        //    if (!String.IsNullOrEmpty(strSkinSrc))
        //    {
        //        strSkinSrc = strSkinSrc.Substring(0, strSkinSrc.LastIndexOf("/") + 1);
        //    }
        //    return strSkinSrc;
        //}
        //public static string FormatSkinSrc(string SkinSrc, PortalSettings PortalSettings)
        //{
        //    string strSkinSrc = SkinSrc;
        //    if (!String.IsNullOrEmpty(strSkinSrc))
        //    {
        //        switch (strSkinSrc.ToLowerInvariant().Substring(0, 3))
        //        {
        //            case "[g]":
        //                strSkinSrc = Regex.Replace(strSkinSrc, "\\[g]", Common.Globals.HostPath, RegexOptions.IgnoreCase);
        //                break;
        //            case "[l]":
        //                strSkinSrc = Regex.Replace(strSkinSrc, "\\[l]", PortalSettings.HomeDirectory, RegexOptions.IgnoreCase);
        //                break;
        //        }
        //    }
        //    return strSkinSrc;
        //}
        //public static string GetDefaultAdminContainer()
        //{
        //    SkinDefaults defaultContainer = SkinDefaults.GetSkinDefaults(SkinDefaultType.ContainerInfo);
        //    return "[G]" + SkinController.RootContainer + defaultContainer.Folder + defaultContainer.AdminDefaultName;
        //}
        //public static string GetDefaultAdminSkin()
        //{
        //    SkinDefaults defaultSkin = SkinDefaults.GetSkinDefaults(SkinDefaultType.SkinInfo);
        //    return "[G]" + SkinController.RootSkin + defaultSkin.Folder + defaultSkin.AdminDefaultName;
        //}
        //public static string GetDefaultPortalContainer()
        //{
        //    SkinDefaults defaultContainer = SkinDefaults.GetSkinDefaults(SkinDefaultType.ContainerInfo);
        //    return "[G]" + SkinController.RootContainer + defaultContainer.Folder + defaultContainer.DefaultName;
        //}
        //public static string GetDefaultPortalSkin()
        //{
        //    SkinDefaults defaultSkin = SkinDefaults.GetSkinDefaults(SkinDefaultType.SkinInfo);
        //    return "[G]" + SkinController.RootSkin + defaultSkin.Folder + defaultSkin.DefaultName;
        //}
        //public static SkinPackageInfo GetSkinByPackageID(int packageID)
        //{
        //    return CBO.FillObject<SkinPackageInfo>(DataProvider.Instance().GetSkinByPackageID(packageID));
        //}
        //public static SkinPackageInfo GetSkinPackage(int portalId, string skinName, string skinType)
        //{
        //    return CBO.FillObject<SkinPackageInfo>(DataProvider.Instance().GetSkinPackage(portalId, skinName, skinType));
        //}
        //public static bool IsGlobalSkin(string SkinSrc)
        //{
        //    return SkinSrc.Contains(Common.Globals.HostPath);
        //}
        //public static void SetSkin(string SkinRoot, int PortalId, UI.Skins.SkinType SkinType, string SkinSrc)
        //{
        //    Entities.Host.HostSettingsController objHostSettings = new Entities.Host.HostSettingsController();
        //    switch (SkinRoot)
        //    {
        //        case "Skins":
        //            if (SkinType == Skins.SkinType.Admin)
        //            {
        //                if (PortalId == Null.NullInteger)
        //                {
        //                    objHostSettings.UpdateHostSetting("DefaultAdminSkin", SkinSrc);
        //                }
        //                else
        //                {
        //                    PortalController.UpdatePortalSetting(PortalId, "DefaultAdminSkin", SkinSrc);
        //                }
        //            }
        //            else
        //            {
        //                if (PortalId == Null.NullInteger)
        //                {
        //                    objHostSettings.UpdateHostSetting("DefaultPortalSkin", SkinSrc);
        //                }
        //                else
        //                {
        //                    PortalController.UpdatePortalSetting(PortalId, "DefaultPortalSkin", SkinSrc);
        //                }
        //            }
        //            break;
        //        case "Containers":
        //            if (SkinType == Skins.SkinType.Admin)
        //            {
        //                if (PortalId == Null.NullInteger)
        //                {
        //                    objHostSettings.UpdateHostSetting("DefaultAdminContainer", SkinSrc);
        //                }
        //                else
        //                {
        //                    PortalController.UpdatePortalSetting(PortalId, "DefaultAdminContainer", SkinSrc);
        //                }
        //            }
        //            else
        //            {
        //                if (PortalId == Null.NullInteger)
        //                {
        //                    objHostSettings.UpdateHostSetting("DefaultPortalContainer", SkinSrc);
        //                }
        //                else
        //                {
        //                    PortalController.UpdatePortalSetting(PortalId, "DefaultPortalContainer", SkinSrc);
        //                }
        //            }
        //            break;
        //    }
        //}
        //public static void UpdateSkin(int skinID, string skinSrc)
        //{
        //    DataProvider.Instance().UpdateSkin(skinID, skinSrc);
        //}
        //public static void UpdateSkinPackage(SkinPackageInfo skinPackage)
        //{
        //    DataProvider.Instance().UpdateSkinPackage(skinPackage.SkinPackageID, skinPackage.PackageID, skinPackage.PortalID, skinPackage.SkinName, skinPackage.SkinType, UserController.GetCurrentUserInfo().UserID);
        //    Services.Log.EventLog.EventLogController objEventLog = new Services.Log.EventLog.EventLogController();
        //    objEventLog.AddLog(skinPackage, PortalController.GetCurrentPortalSettings(), UserController.GetCurrentUserInfo().UserID, "", CommonLibrary.Services.Log.EventLog.EventLogController.EventLogType.SKINPACKAGE_UPDATED);
        //    foreach (KeyValuePair<int, string> kvp in skinPackage.Skins)
        //    {
        //        UpdateSkin(kvp.Key, kvp.Value);
        //    }
        //}
        //public static string UploadLegacySkin(string RootPath, string SkinRoot, string SkinName, Stream objInputStream)
        //{
        //    ZipInputStream objZipInputStream = new ZipInputStream(objInputStream);
        //    ZipEntry objZipEntry;
        //    string strExtension;
        //    string strFileName;
        //    FileStream objFileStream;
        //    int intSize = 2048;
        //    byte[] arrData = new byte[2048];
        //    string strMessage = "";
        //    ArrayList arrSkinFiles = new ArrayList();
        //    PortalSettings ResourcePortalSettings = CommonLibrary.Common.Globals.GetPortalSettings();
        //    string BEGIN_MESSAGE = Localization.GetString("BeginZip", ResourcePortalSettings);
        //    string CREATE_DIR = Localization.GetString("CreateDir", ResourcePortalSettings);
        //    string WRITE_FILE = Localization.GetString("WriteFile", ResourcePortalSettings);
        //    string FILE_ERROR = Localization.GetString("FileError", ResourcePortalSettings);
        //    string END_MESSAGE = Localization.GetString("EndZip", ResourcePortalSettings);
        //    string FILE_RESTICTED = Localization.GetString("FileRestricted", ResourcePortalSettings);
        //    strMessage += FormatMessage(BEGIN_MESSAGE, SkinName, -1, false);
        //    objZipEntry = objZipInputStream.GetNextEntry();
        //    while (objZipEntry != null)
        //    {
        //        if (!objZipEntry.IsDirectory)
        //        {
        //            strExtension = objZipEntry.Name.Substring(objZipEntry.Name.LastIndexOf(".") + 1);
        //            if (("," + strExtension.ToUpper()).IndexOf(",ASCX,HTM,HTML,CSS,SWF,RESX,XAML,JS," + Host.FileExtensions.ToUpper()) != 0)
        //            {
        //                if (objZipEntry.Name.ToLower() == SkinController.RootSkin.ToLower() + ".zip")
        //                {
        //                    MemoryStream objMemoryStream = new MemoryStream();
        //                    intSize = objZipInputStream.Read(arrData, 0, arrData.Length);
        //                    while (intSize > 0)
        //                    {
        //                        objMemoryStream.Write(arrData, 0, intSize);
        //                        intSize = objZipInputStream.Read(arrData, 0, arrData.Length);
        //                    }
        //                    objMemoryStream.Seek(0, SeekOrigin.Begin);
        //                    strMessage += UploadLegacySkin(RootPath, SkinController.RootSkin, SkinName, (Stream)objMemoryStream);
        //                }
        //                else if (objZipEntry.Name.ToLower() == SkinController.RootContainer.ToLower() + ".zip")
        //                {
        //                    MemoryStream objMemoryStream = new MemoryStream();
        //                    intSize = objZipInputStream.Read(arrData, 0, arrData.Length);
        //                    while (intSize > 0)
        //                    {
        //                        objMemoryStream.Write(arrData, 0, intSize);
        //                        intSize = objZipInputStream.Read(arrData, 0, arrData.Length);
        //                    }
        //                    objMemoryStream.Seek(0, SeekOrigin.Begin);
        //                    strMessage += UploadLegacySkin(RootPath, SkinController.RootContainer, SkinName, (Stream)objMemoryStream);
        //                }
        //                else
        //                {
        //                    strFileName = RootPath + SkinRoot + "\\" + SkinName + "\\" + objZipEntry.Name;
        //                    if (!Directory.Exists(Path.GetDirectoryName(strFileName)))
        //                    {
        //                        strMessage += FormatMessage(CREATE_DIR, Path.GetDirectoryName(strFileName), 2, false);
        //                        Directory.CreateDirectory(Path.GetDirectoryName(strFileName));
        //                    }
        //                    if (File.Exists(strFileName))
        //                    {
        //                        File.SetAttributes(strFileName, FileAttributes.Normal);
        //                        File.Delete(strFileName);
        //                    }
        //                    objFileStream = File.Create(strFileName);
        //                    strMessage += FormatMessage(WRITE_FILE, Path.GetFileName(strFileName), 2, false);
        //                    intSize = objZipInputStream.Read(arrData, 0, arrData.Length);
        //                    while (intSize > 0)
        //                    {
        //                        objFileStream.Write(arrData, 0, intSize);
        //                        intSize = objZipInputStream.Read(arrData, 0, arrData.Length);
        //                    }
        //                    objFileStream.Close();
        //                    switch (Path.GetExtension(strFileName))
        //                    {
        //                        case ".htm":
        //                        case ".html":
        //                        case ".ascx":
        //                        case ".css":
        //                            if (strFileName.ToLower().IndexOf(Globals.glbAboutPage.ToLower()) < 0)
        //                            {
        //                                arrSkinFiles.Add(strFileName);
        //                            }
        //                            break;
        //                    }
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                strMessage += string.Format(FILE_RESTICTED, objZipEntry.Name, Host.FileExtensions.ToString(), ",", ", *.").Replace("2", "true");
        //            }
        //        }
        //        objZipEntry = objZipInputStream.GetNextEntry();
        //    }
        //    strMessage += FormatMessage(END_MESSAGE, SkinName + ".zip", 1, false);
        //    objZipInputStream.Close();
        //    UI.Skins.SkinFileProcessor NewSkin = new UI.Skins.SkinFileProcessor(RootPath, SkinRoot, SkinName);
        //    strMessage += NewSkin.ProcessList(arrSkinFiles, SkinParser.Portable);
        //    try
        //    {
        //        Services.Log.EventLog.LogInfo objEventLogInfo = new Services.Log.EventLog.LogInfo();
        //        objEventLogInfo.LogTypeKey = Services.Log.EventLog.EventLogController.EventLogType.HOST_ALERT.ToString();
        //        objEventLogInfo.LogProperties.Add(new CommonLibrary.Services.Log.EventLog.LogDetailInfo("Install Skin:", SkinName));
        //        Array arrMessage = strMessage.Split(new string[] { "<br>" }, StringSplitOptions.None);
        //        foreach (string strRow in arrMessage)
        //        {
        //            objEventLogInfo.LogProperties.Add(new CommonLibrary.Services.Log.EventLog.LogDetailInfo("Info:", HtmlUtils.StripTags(strRow, true)));
        //        }
        //        Services.Log.EventLog.EventLogController objEventLog = new Services.Log.EventLog.EventLogController();
        //        objEventLog.AddLog(objEventLogInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return strMessage;
        //}
        public static string RootSkin
        {
            get { return "Skins"; }
        }
        public static string RootContainer
        {
            get { return "Containers"; }
        }

        #region Theme Helper ================================================================================================================================================
        public static IList<SelectListItem> GetSelectedList(int ApplicationId, string SelectedTheme, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = (from s in context.Skins
                             join p in context.SkinPackages on s.SkinPackageId equals p.SkinPackageId into package_lst
                             from ps in package_lst.DefaultIfEmpty()
                             where s.SkinStatus == true && ps.ApplicationId == ApplicationId
                             select new { ps.SkinPackageName, s.SkinId }).AsEnumerable();

                List<SelectListItem> list = query.Select(s => new SelectListItem
                {
                    Text = s.SkinPackageName,
                    Value = s.SkinId.ToString()
                }).ToList();

                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.NoneSpecified), Value = "" });

                //var list = new List<SelectListItem>
                //                {new SelectListItem {Text = "Default", Value = "Default"},
                //                new SelectListItem {Text = "Red", Value = "Red"},
                //                new SelectListItem {Text = "Green", Value = "Green"}};

                foreach (var selectListItem in list.Where(selectListItem => selectListItem.Value.Equals(SelectedTheme)))
                {
                    selectListItem.Selected = true;
                }
                return list;
            }

        }

        public static SkinViewModel GetSelectedTheme(int ApplicationId)
        {
            string[] strArray = new string[2];
            using (EntityDataContext context = new EntityDataContext())
            {
                var entity = (from s in context.Skins
                             join p in context.SkinPackages on s.SkinPackageId equals p.SkinPackageId into package_lst
                             from ps in package_lst.DefaultIfEmpty()
                             where s.SkinStatus == true && ps.ApplicationId == ApplicationId
                             select new SkinViewModel
                             {
                                 SkinPackageName = ps.SkinPackageName,
                                 SkinPackageSrc = ps.SkinPackageSrc
                             }).FirstOrDefault();

                if (entity == null)
                {
                    entity = new SkinViewModel()
                    {
                        SkinPackageName = ThemeSettings.ThemeName,
                        SkinPackageSrc = ThemeSettings.ThemeSrc
                    };
                }
                HttpContext.Current.Session[SettingKeys.ThemeName] = entity.SkinPackageName;
                HttpContext.Current.Session[SettingKeys.ThemeSrc] = entity.SkinPackageSrc;
                return entity;
            }
        }
        public static string GetCssWithTheme(ViewContext viewContext)
        {
            var themeName = ConfigurationManager.AppSettings["themeName"];

            return string.Format("~/Themes/{0}/Content/css/style.css", viewContext.HttpContext.Items[themeName]);
        }

        public static SelectList PopulateActiveSkinSelectList(string SelectedValue = null, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = (from s in context.Skins
                             join p in context.SkinPackages on s.SkinPackageId equals p.SkinPackageId into package_lst
                             from ps in package_lst.DefaultIfEmpty()
                             where s.SkinStatus == true
                             select new { ps.SkinPackageName, s.SkinId }).AsEnumerable();

                List<SelectListItem> list = query.Select(s => new SelectListItem
                {
                    Text = s.SkinPackageName,
                    Value = s.SkinId.ToString()
                }).ToList();

                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.NoneSpecified), Value = "" });
                return new SelectList(list, "Value", "Text", SelectedValue);
            }
        }

        public static SelectList PopulateActiveSkinSelectList(int ApplicationId, string SelectedValue = null, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = (from s in context.Skins
                             join p in context.SkinPackages on s.SkinPackageId equals p.SkinPackageId into package_lst
                             from ps in package_lst.DefaultIfEmpty()
                             where s.SkinStatus == true && ps.ApplicationId == ApplicationId
                             select new { ps.SkinPackageName, s.SkinId }).AsEnumerable();

                List<SelectListItem> list = query.Select(s => new SelectListItem
                {
                    Text = s.SkinPackageName,
                    Value = s.SkinId.ToString()
                }).ToList();

                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.NoneSpecified), Value = "" });
                return new SelectList(list, "Value", "Text", SelectedValue);
            }
        }
        public static SelectList PopulateActiveSkinSelectList(int ApplicationId, int SkinTypeId, string SelectedValue = null, bool IsShowSelectText = false)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = (from s in context.Skins
                             join p in context.SkinPackages on s.SkinPackageId equals p.SkinPackageId into package_lst
                             from ps in package_lst.DefaultIfEmpty()
                             where s.SkinStatus == true && ps.ApplicationId == ApplicationId && s.SkinTypeId == SkinTypeId
                             select new { ps.SkinPackageName, s.SkinId }).AsEnumerable();

                List<SelectListItem> list = query.Select(s => new SelectListItem
                {
                    Text = s.SkinPackageName,
                    Value = s.SkinId.ToString()
                }).ToList();

                if (IsShowSelectText)
                    list.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.NoneSpecified), Value = "" });
                return new SelectList(list, "Value", "Text", SelectedValue);
            }
        }

        #endregion ==========================================================================================================================================================



        //=======================================================================================================================

        #region Skin - Template-Related Method ============================================================================================
      

        public static List<Skin> GetSkinListByApplicationId(int ApplicationId, bool? Status)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = from x in context.Skins
                            join p in context.SkinPackages on x.SkinPackageId equals p.SkinPackageId into pl
                            from pkl in pl.DefaultIfEmpty()
                            where pkl.ApplicationId == ApplicationId && (x.SkinStatus == Status || Status == null)
                            select x;
                return query.ToList();
            }
        }

        public DataTable GetListByApplicationIdSkinType(int ApplicationId, int SkinType)
        {
            SqlCommand cmd = new SqlCommand("Skins_GetListByApplicationIdSkinType", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
            cmd.Parameters.AddWithValue("@SkinType", SkinType);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByApplicationIdSkinPackageId(int ApplicationId, int SkinPackageId)
        {
            SqlCommand cmd = new SqlCommand("Skins_GetListByApplicationIdSkinPackageId", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
            cmd.Parameters.AddWithValue("@SkinPackageId", SkinPackageId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListBySkinType(string SkinType)
        {
            SqlCommand cmd = new SqlCommand("Skins_GetListBySkinType", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinType", SkinType);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int SelectSkin(int SkinId)
        {
            SqlCommand cmd = new SqlCommand("Skins_Select", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinId", SkinId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public string GetSkinSrcBySkinId(int SkinId)
        {            
            DataTable dt = GetDetails(SkinId);
            string SkinSrc = string.Empty;
            if(dt.Rows.Count>0)
                SkinSrc = dt.Rows[0]["SkinSrc"].ToString();
            return SkinSrc;
        }


        public DataTable GetDetails(int SkinId)
        {
            SqlCommand cmd = new SqlCommand("Skins_GetDetails", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinId", SkinId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int Insert(int SkinPackageId, string SkinSrc, string UserId)
        {
            SqlCommand cmd = new SqlCommand("Skins_Insert", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinPackageId", SkinPackageId);
            cmd.Parameters.AddWithValue("@SkinSrc", SkinSrc);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(int SkinId, int SkinPackageId, string SkinSrc, string UserId)
        {
            SqlCommand cmd = new SqlCommand("Skins_Update", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinId", SkinId);
            cmd.Parameters.AddWithValue("@SkinPackageId", SkinPackageId);
            cmd.Parameters.AddWithValue("@SkinSrc", SkinSrc);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }   
        //public static List<Template> GetTemplates()
        //{
        //    DirectoryInfo dInfo = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("../../Templates/Front_Templates"));
        //    DirectoryInfo[] dArrInfo = dInfo.GetDirectories();
        //    List<Template> list = new List<Template>();
        //    foreach (DirectoryInfo sDirectory in dArrInfo)
        //    {
        //        Template temp = new Template(sDirectory.Name);

        //        list.Add(temp);
        //    }
        //    return list;
        //}

        //public static List<Template> GetTemplates_Admin()
        //{
        //    DirectoryInfo dInfo = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("../../Templates/Admin_Templates"));
        //    DirectoryInfo[] dArrInfo = dInfo.GetDirectories();
        //    List<Template> list = new List<Template>();
        //    foreach (DirectoryInfo sDirectory in dArrInfo)
        //    {
        //        Template temp = new Template(sDirectory.Name);

        //        list.Add(temp);
        //    }
        //    return list;
        //}
        #endregion =====================================================================================================================
        
    }
}
