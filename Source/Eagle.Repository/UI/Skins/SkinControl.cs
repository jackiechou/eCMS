﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Eagle.Common.Settings;

namespace CommonLibrary.UI.Skins
{
    public class SkinControl 
    {
        private SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        private DataTable dt = new DataTable();  
        //private string _Width = "";
        //private string _SkinRoot;
        private string _SkinSrc;
        //private string _localResourceFile;
        private string _DefaultKey = "System";
        public string DefaultKey
        {
            get { return _DefaultKey; }
            set { _DefaultKey = value; }
        }
        //public string Width
        //{
        //    get { return Convert.ToString(ViewState["SkinControlWidth"]); }
        //    set { _Width = value; }
        //}
        //public string SkinRoot
        //{
        //    get { return Convert.ToString(ViewState["SkinRoot"]); }
        //    set { _SkinRoot = value; }
        //}
        public string SkinSrc
        {
            get
            {
                return _SkinSrc;
            }
            set { _SkinSrc = value; }
        }
        //public string LocalResourceFile
        //{
        //    get
        //    {
        //        string fileRoot;
        //        if (String.IsNullOrEmpty(_localResourceFile))
        //        {
        //            fileRoot = this.TemplateSourceDirectory + "/" + Services.Localization.Localization.LocalResourceDirectory + "/SkinControl.ascx";
        //        }
        //        else
        //        {
        //            fileRoot = _localResourceFile;
        //        }
        //        return fileRoot;
        //    }
        //    set { _localResourceFile = value; }
        //}
               
        //public void LoadSkins()
        //{
        //    string strRoot;
        //    string[] arrFolders;
        //    string[] arrFiles;
        //    string strLastFolder;
        //    string strSeparator = "----------------------------------------";
        //    //cboSkin.Items.Clear();
        //    if (optHost.Checked)
        //    {
        //        strLastFolder = "";
        //        strRoot = Common.Globals.HostMapPath + SkinRoot;
        //        if (Directory.Exists(strRoot))
        //        {
        //            arrFolders = Directory.GetDirectories(strRoot);
        //            foreach (string strFolder in arrFolders)
        //            {
        //                if (!strFolder.EndsWith(Globals.glbHostSkinFolder))
        //                {
        //                    arrFiles = Directory.GetFiles(strFolder, "*.ascx");
        //                    foreach (string strFile in arrFiles)
        //                    {
        //                        string folder = strFolder.Substring(strFolder.LastIndexOf("\\") + 1);
        //                        if (strLastFolder != folder)
        //                        {
        //                            if (!String.IsNullOrEmpty(strLastFolder))
        //                            {
        //                                cboSkin.Items.Add(new ListItem(strSeparator, ""));
        //                            }
        //                            strLastFolder = folder;
        //                        }
        //                        cboSkin.Items.Add(new ListItem(FormatSkinName(folder, Path.GetFileNameWithoutExtension(strFile)), "[G]" + SkinRoot + "/" + folder + "/" + Path.GetFileName(strFile)));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (optSite.Checked)
        //    {
        //        strLastFolder = "";
        //        strRoot = _objApplication.HomeDirectoryMapPath + SkinRoot;
        //        if (Directory.Exists(strRoot))
        //        {
        //            arrFolders = Directory.GetDirectories(strRoot);
        //            foreach (string strFolder in arrFolders)
        //            {
        //                arrFiles = Directory.GetFiles(strFolder, "*.ascx");
        //                foreach (string strFile in arrFiles)
        //                {
        //                    string folder = strFolder.Substring(strFolder.LastIndexOf("\\") + 1);
        //                    if (strLastFolder != folder)
        //                    {
        //                        if (!String.IsNullOrEmpty(strLastFolder))
        //                        {
        //                            cboSkin.Items.Add(new ListItem(strSeparator, ""));
        //                        }
        //                        strLastFolder = folder;
        //                    }
        //                    cboSkin.Items.Add(new ListItem(FormatSkinName(folder, Path.GetFileNameWithoutExtension(strFile)), "[L]" + SkinRoot + "/" + folder + "/" + Path.GetFileName(strFile)));
        //                }
        //            }
        //        }
        //    }
        //    if (cboSkin.Items.Count > 0)
        //    {
        //        cboSkin.Items.Insert(0, new ListItem(strSeparator, ""));
        //    }
        //    cboSkin.Items.Insert(0, new ListItem("<" + Services.Localization.Localization.GetString(DefaultKey, LocalResourceFile) + ">", ""));
        //    if (ViewState["SkinSrc"] != null)
        //    {
        //        for (int intIndex = 0; intIndex <= cboSkin.Items.Count - 1; intIndex++)
        //        {
        //            if (cboSkin.Items[intIndex].Value.ToLower() == ViewState["SkinSrc"].ToString().ToLower())
        //            {
        //                cboSkin.Items[intIndex].Selected = true;
        //                break;
        //            }
        //        }
        //    }
        //}
        
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    #region Bind Handles
        //    optHost.CheckedChanged += new EventHandler(optHost_CheckedChanged);
        //    optSite.CheckedChanged += new EventHandler(optSite_CheckedChanged);
        //    cmdPreview.Click += new EventHandler(cmdPreview_Click);
        //    #endregion
        //    try
        //    {
        //        ApplicationController objApplications = new ApplicationController();
        //        if (Request.QueryString["pid"] != null && (ApplicationSettings.ActiveTab.ParentId == ApplicationSettings.SuperTabId || UserController.GetCurrentUserInfo().IsSuperUser))
        //        {
        //            _objApplication = objApplications.GetApplication(Int32.Parse(Request.QueryString["pid"]));
        //        }
        //        else
        //        {
        //            _objApplication = objApplications.GetApplication(ApplicationSettings.ApplicationId);
        //        }
        //        if (!Page.IsPostBack)
        //        {
        //            ViewState["SkinControlWidth"] = _Width;
        //            ViewState["SkinRoot"] = _SkinRoot;
        //            ViewState["SkinSrc"] = _SkinSrc;
        //            if (!String.IsNullOrEmpty(_Width))
        //            {
        //                cboSkin.Width = System.Web.UI.WebControls.Unit.Parse(_Width);
        //            }
        //            if (!String.IsNullOrEmpty(_SkinSrc))
        //            {
        //                switch (_SkinSrc.Substring(0, 3))
        //                {
        //                    case "[L]":
        //                        optHost.Checked = false;
        //                        optSite.Checked = true;
        //                        break;
        //                    case "[G]":
        //                        optSite.Checked = false;
        //                        optHost.Checked = true;
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                string strRoot = _objApplication.HomeDirectoryMapPath + SkinRoot;
        //                if (Directory.Exists(strRoot) && Directory.GetDirectories(strRoot).Length > 0)
        //                {
        //                    optHost.Checked = false;
        //                    optSite.Checked = true;
        //                }
        //            }
        //            LoadSkins();
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        exc.ToString();
        //    }
        //}

        //protected void optHost_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    LoadSkins();
        //}
        //protected void optSite_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    LoadSkins();
        //}
        //protected void cmdPreview_Click(object sender, System.EventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(SkinSrc))
        //    {
        //        string strType = SkinRoot.Substring(0, SkinRoot.Length - 1);
        //        string strURL = Globals.ApplicationURL() + "&" + strType + "Src=" + Globals.QueryStringEncode(SkinSrc.Replace(".ascx", ""));
        //        if (SkinRoot == SkinController.RootContainer)
        //        {
        //            if (Request.QueryString["ModuleId"] != null)
        //            {
        //                strURL += "&ModuleId=" + Request.QueryString["ModuleId"].ToString();
        //            }
        //        }
        //        Response.Redirect(strURL, true);
        //    }
        //}

        #region METHODS =======================================================================================
        public DataTable GetListBySkinId(int SkinId)
        {
            SqlCommand cmd = new SqlCommand("SkinControls_GetListBySkinId", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinId", SkinId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }


        public DataTable GetListBySkinPackageId(int SkinPackageId)
        {
            SqlCommand cmd = new SqlCommand("SkinControls_GetListBySkinPackageId", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };            
            cmd.Parameters.AddWithValue("@SkinPackageId", SkinPackageId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetails(int SkinControlId)
        {
            SqlCommand cmd = new SqlCommand("SkinControls_GetDetails", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinControlId", SkinControlId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int Insert(int SkinPackageId, string ControlKey, string ControlSrc, string IconFile)
        {
            SqlCommand cmd = new SqlCommand("SkinControls_Insert", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinPackageId", SkinPackageId);
            cmd.Parameters.AddWithValue("@ControlKey", ControlKey);
            cmd.Parameters.AddWithValue("@ControlSrc", ControlSrc);
            cmd.Parameters.AddWithValue("@IconFile", IconFile);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(int SkinControlId, int SkinPackageId, string ControlKey, string ControlSrc, string IconFile)
        {
            SqlCommand cmd = new SqlCommand("SkinControls_Update", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@SkinControlId", SkinControlId);
            cmd.Parameters.AddWithValue("@SkinPackageId", SkinPackageId);
            cmd.Parameters.AddWithValue("@ControlKey", ControlKey);
            cmd.Parameters.AddWithValue("@ControlSrc", ControlSrc);
            cmd.Parameters.AddWithValue("@IconFile", IconFile);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }   
        #endregion ====================================================================================
    }
}
