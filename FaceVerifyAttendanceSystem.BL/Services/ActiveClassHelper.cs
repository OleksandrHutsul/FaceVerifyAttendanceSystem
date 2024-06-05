using Microsoft.AspNetCore.Mvc.Rendering;

namespace FaceVerifyAttendanceSystem.BL.Services
{
    public static class ActiveClassHelper
    {
        public static string IsActive(this IHtmlHelper html, string controller, string action)
        {
            var routeData = html.ViewContext.RouteData;
            var routeAction = routeData.Values["action"]?.ToString();
            var routeController = routeData.Values["controller"]?.ToString();

            var returnActive = (controller == routeController && action == routeAction);
            return returnActive ? "active" : "";
        }

        public static string IsActive(this IHtmlHelper html, string controller)
        {
            var routeData = html.ViewContext.RouteData;
            var routeController = routeData.Values["controller"]?.ToString();

            var returnActive = (controller == routeController);
            return returnActive ? "active" : "";
        }
    }
}