using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Annotations;

namespace HGV.AD.Web.Configuration
{
    public class ClaimsBasedAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string _type;
        private readonly string _value;

        public ClaimsBasedAuthorizationFilter(string type, string value)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (value == null) throw new ArgumentNullException("value");

            _type = type;
            _value = value;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext == null)
                return false;

            if (httpContext.User == null)
                return false;

            if (httpContext.User.Identity == null)
                return false;

            if (httpContext.User.Identity.IsAuthenticated == false)
                return false;

            var result = httpContext.User.Claims.Any(_ => _.Type == _type && _.Value == _value);
            return result;
        }
    }
}
