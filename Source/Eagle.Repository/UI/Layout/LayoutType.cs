using Eagle.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.UI.Layout
{
    public static class DesktopLayoutType
    {

        private static string _MainLayout = "~/Themes/Default/Views/Shared/DesktopLayouts/MainLayout.cshtml";
        public static string MainLayout
        {
            get { return _MainLayout; }
            set { _MainLayout = value; }
        }
    }

   
    public class LayoutType
    {
        #region  TemplateName ======================================================================================
        private static string _MainTemplateName = "Main Layout";
        private static string _FullTemplateName = "Full Main Layout";
        private static string _LoginTemplateName = "Login Layout";
        private static string _ErrorTemplateName = "Error Layout";
        private static string _FormTemplateName = "Form Layout";
        private static string _PopUpTemplateName = "PopUp Layout";

        public static string MainTemplateName
        {
            get { return _MainTemplateName; }
            set { _MainTemplateName = value; }
        }
        public static string FullTemplateName
        {
            get { return _FullTemplateName; }
            set { _FullTemplateName = value; }
        }
        public static string LoginTemplateName
        {
            get { return _LoginTemplateName; }
            set { _LoginTemplateName = value; }
        }

        public static string ErrorTemplateName
        {
            get { return _ErrorTemplateName; }
            set { _ErrorTemplateName = value; }
        }
        public static string FormTemplateName
        {
            get { return _FormTemplateName; }
            set { _FormTemplateName = value; }
        }
        public static string PopUpTemplateName
        {
            get { return _PopUpTemplateName; }
            set { _PopUpTemplateName = value; }
        }
        #endregion ================================================================================================

        #region  TemplateKey ======================================================================================
        private static string _MainTemplateKey = "MAIN-LAYOUT";
        private static string _FullTemplateKey = "FULL-MAIN-LAYOUT";
        private static string _LoginTemplateKey = "LOGIN-LAYOUT";
        private static string _ErrorTemplateKey = "ERROR-LAYOUT";
        private static string _FormTemplateKey = "FORM-LAYOUT";
        private static string _PopUpTemplateKey = "POPUP-LAYOUT";

        public static string MainTemplateKey
        {
            get { return _MainTemplateKey; }
            set { _MainTemplateKey = value; }
        }
        public static string FullTemplateKey
        {
            get { return _FullTemplateKey; }
            set { _FullTemplateKey = value; }
        }
        public static string LoginTemplateKey
        {
            get { return _LoginTemplateKey; }
            set { _LoginTemplateKey = value; }
        }

        public static string ErrorTemplateKey
        {
            get { return _ErrorTemplateKey; }
            set { _ErrorTemplateKey = value; }
        }
        public static string FormTemplateKey
        {
            get { return _FormTemplateKey; }
            set { _FormTemplateKey = value; }
        }
        public static string PopUpTemplateKey
        {
            get { return _PopUpTemplateKey; }
            set { _PopUpTemplateKey = value; }
        }
        #endregion ================================================================================================

        #region Layout ======================================================================================
        private static string _MasterLayout = "~/Themes/Default/Views/Shared/AdminLayouts/MasterLayout.cshtml";
        private static string _SharedLayout = "~/Themes/Default/Views/Shared/AdminLayouts/SharedLayout.cshtml";
        private static string _MainLayout = "~/Themes/Default/Views/Shared/AdminLayouts/MainLayout.cshtml";
        private static string _FullMainLayout = "~/Themes/Default/Views/Shared/AdminLayouts/FullMainLayout.cshtml";        
        private static string _LoginLayout = "~/Themes/Default/Views/Shared/AdminLayouts/LoginLayout.cshtml";
        private static string _ErrorLayout = "~/Themes/Default/Views/Shared/AdminLayouts/ErrorLayout.cshtml";
        private static string _FormLayout = "~/Themes/Default/Views/Shared/AdminLayouts/FormLayout.cshtml";
        private static string _PopUpLayout = "~/Themes/Default/Views/Shared/AdminLayouts/PopUpLayout.cshtml";
        private static string _ReportLayout = "~/Themes/Default/Views/Shared/AdminLayouts/ReportLayout.cshtml";


        public static string MasterLayout
        {
            get { return _MasterLayout; }
            set { _MasterLayout = value; }
        }

        public static string SharedLayout
        {
            get { return _SharedLayout; }
            set { _SharedLayout = value; }
        }

        public static string MainLayout
        {
            get { return _MainLayout; }
            set { _MainLayout = value; }
        }

        public static string FullMainLayout
        {
            get { return _FullMainLayout; }
            set { _FullMainLayout = value; }
        }

        public static string PopUpLayout
        {
            get { return _PopUpLayout; }
            set { _PopUpLayout = value; }
        }

        public static string ReportLayout
        {
            get { return _ReportLayout; }
            set { _ReportLayout = value; }
        }

        public static string FormLayout
        {
            get { return _FormLayout; }
            set { _FormLayout = value; }
        }

        public static string ErrorLayout
        {
            get { return _ErrorLayout; }
            set { _ErrorLayout = value; }
        }

        public static string LoginLayout
        {
            get { return _LoginLayout; }
            set { _LoginLayout = value; }
        }
        #endregion end Theme ==================================================
    }
}
