using System.Web;
using System.Web.Mvc;

namespace PassionProjectn01681774
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
