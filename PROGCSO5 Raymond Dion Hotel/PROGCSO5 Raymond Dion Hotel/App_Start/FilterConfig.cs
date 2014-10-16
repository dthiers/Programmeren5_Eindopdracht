using System.Web;
using System.Web.Mvc;

namespace PROGCSO5_Raymond_Dion_Hotel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}