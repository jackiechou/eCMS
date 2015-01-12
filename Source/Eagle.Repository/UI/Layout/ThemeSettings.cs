using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.UI.Layout
{
    public class ThemeSettings
    {
        private static string _ThemeName = "Default";
        private static string _ThemeSrc = "~/Themes/Default";
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

    }
}
