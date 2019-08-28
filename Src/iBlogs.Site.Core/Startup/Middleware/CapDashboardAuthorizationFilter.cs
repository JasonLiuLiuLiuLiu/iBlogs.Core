using DotNetCore.CAP.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace iBlogs.Site.Core.Startup.Middleware
{
    public class CapDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext dashBoardContext)
        {
            var context = ((CapDashboardContext)dashBoardContext).HttpContext;

            var authenticationCookieName = "Authorization";
            var cookie = context.Request.Cookies[authenticationCookieName];
            if (cookie != null)
            {
                context.Request.Headers.Append("Authorization", "Bearer " + cookie);
            }

            var schemes = (IAuthenticationSchemeProvider)context.RequestServices.GetService(typeof(IAuthenticationSchemeProvider));

            var authenticateSchemeAsync = schemes.GetDefaultAuthenticateSchemeAsync().Result;
            if (authenticateSchemeAsync == null) return true;
            var authenticateResult = context.AuthenticateAsync(authenticateSchemeAsync.Name).Result;
            return authenticateResult?.Principal != null;
        }
    }
}
