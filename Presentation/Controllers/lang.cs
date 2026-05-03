using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MOIProjectMIS.Controllers
{
 
    public class lang : Controller
    {
        public IActionResult changelanguage(string culture)
        {
            CultureInfo cInfo = new CultureInfo(culture);
            CultureInfo cInfoInvarian = new CultureInfo("en-GB");
            cInfoInvarian.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            cInfoInvarian.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            cInfo.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            cInfo.DateTimeFormat.LongDatePattern = "dd-MM-yyyy";

            cInfoInvarian.DateTimeFormat.Calendar = new GregorianCalendar();
            cInfo.DateTimeFormat.Calendar = new GregorianCalendar();

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                 new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}