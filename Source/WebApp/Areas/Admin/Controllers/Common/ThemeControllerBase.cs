using CommonLibrary.UI.Skins;
using Eagle.Common.Settings;
using Eagle.Model.UI.Skins;
using Eagle.Repository.UI.Layout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public abstract class ThemeControllerBase : BaseController
{
    protected override void Execute(System.Web.Routing.RequestContext requestContext)
    {
        string themeName = string.Empty, themeSrc = string.Empty;
        if (Session[SettingKeys.ThemeName] == null || Session[SettingKeys.ThemeName].ToString() == string.Empty)
        {
            SkinViewModel skin_obj = SkinRepository.GetSelectedTheme(ApplicationId);
            themeName = skin_obj.SkinPackageName;
            themeSrc = skin_obj.SkinPackageSrc;
        }
        else
        {
            themeName = Session[SettingKeys.ThemeName].ToString();
            themeSrc = Session[SettingKeys.ThemeSrc].ToString();
        }

        if (requestContext.HttpContext.Items[themeName] == null)
        {
            //first time load
            requestContext.HttpContext.Items[themeName] = requestContext.HttpContext.Request.Cookies.Get("theme").Value;
        }
        else
        {
            requestContext.HttpContext.Items[themeName] = themeSrc;

            var previewTheme = requestContext.RouteData.GetRequiredString("theme");

            if (!string.IsNullOrEmpty(previewTheme))
            {
                requestContext.HttpContext.Items[themeName] = previewTheme;
            }
        }

        base.Execute(requestContext);
    }
}