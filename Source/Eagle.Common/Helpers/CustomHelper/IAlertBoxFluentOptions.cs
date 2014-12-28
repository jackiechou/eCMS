using System.Web;

namespace Eagle.Common.Helpers.CustomHelper
{
    public interface IAlertBoxFluentOptions : IHtmlString
    {
        IAlertBoxFluentOptions HideCloseButton(bool hideCloseButton = true);
        IAlertBoxFluentOptions Attributes(object htmlAttributes);
    }
}