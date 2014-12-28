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
        #region start Theme =================================================================================
        private static string _ThemeName = "Default";
        private static string _ThemeSrc = "~/Themes/Default";

        private static string _MasterLayout = "~/Themes/Default/Views/Shared/AdminLayouts/MasterLayout.cshtml";
        private static string _SharedLayout = "~/Themes/Default/Views/Shared/AdminLayouts/SharedLayout.cshtml";
        private static string _MainLayout = "~/Themes/Default/Views/Shared/AdminLayouts/MainLayout.cshtml";
        private static string _FullMainLayout = "~/Themes/Default/Views/Shared/AdminLayouts/FullMainLayout.cshtml";
        private static string _PopUpLayout = "~/Themes/Default/Views/Shared/AdminLayouts/PopUpLayout.cshtml";
        private static string _ReportLayout = "~/Themes/Default/Views/Shared/AdminLayouts/ReportLayout.cshtml";
        private static string _FormLayout = "~/Themes/Default/Views/Shared/AdminLayouts/FormLayout.cshtml";
        private static string _ErrorLayout = "~/Themes/Default/Views/Shared/AdminLayouts/ErrorLayout.cshtml";
        private static string _LoginLayout = "~/Themes/Default/Views/Shared/AdminLayouts/LoginLayout.cshtml";

        public static string ThemeName
        {
            get { return _ThemeName; }
            set { _ThemeName = value; }
        }
        public static string ThemeSrc
        {
            get { return _ThemeSrc; }
            set { _ThemeSrc = value; }
        }

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
