using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Eagle.Repository.Common
{
    public static class DataProvider
    {
        const string DataContextKey = "EntityDataContext";
        public static EntityDataContext DataSources 
        {
            get
            {
                if (HttpContext.Current.Items[DataContextKey] == null)
                    HttpContext.Current.Items[DataContextKey] = new EntityDataContext();
                return (EntityDataContext)HttpContext.Current.Items[DataContextKey];
            }
        }
    }
}
