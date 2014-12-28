using System.Web;

namespace Eagle.Common.Helpers.CustomHelper
{
    public interface IAlertBox : IHtmlString, IAlertBoxFluentOptions
    {
        IAlertBoxFluentOptions Success();
        IAlertBoxFluentOptions Warning();
        IAlertBoxFluentOptions Info();
    }
}