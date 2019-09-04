using System.Threading.Tasks;
using DotNetCore.CAP.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace iBlogs.Site.Core.Startup.Middleware
{
    public class CapDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public async Task<bool> AuthorizeAsync(DashboardContext dashBoardContext)
        {
            var context = ((CapDashboardContext)dashBoardContext).HttpContext;

            var authenticationCookieName = "Authorization";
            var cookie = context.Request.Cookies[authenticationCookieName];
            if (cookie != null)
            {
                context.Request.Headers.Append("Authorization", "Bearer " + cookie);
            }

            var schemes = (IAuthenticationSchemeProvider)context.RequestServices.GetService(typeof(IAuthenticationSchemeProvider));

            var authenticateSchemeAsync =await schemes.GetDefaultAuthenticateSchemeAsync();
            if (authenticateSchemeAsync == null) return true;
            var authenticateResult =await context.AuthenticateAsync(authenticateSchemeAsync.Name);
            return authenticateResult?.Principal != null;
        }
    }
}
