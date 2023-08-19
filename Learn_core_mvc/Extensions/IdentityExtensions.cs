using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Learn_core_mvc.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetEmail(this IIdentity identity)
        {
            Claim emailClaim = null;

            if (identity.GetType().Name == "ClaimsIdentity")
            {
                emailClaim = ((ClaimsIdentity)identity)
                     .Claims
                     .Where(c => c.Type.Contains("email"))
                     .FirstOrDefault();
            }

            return emailClaim != null ? emailClaim.Value : null;
        }

        public static string GetRole(this IIdentity identity)
        {
            Claim roleClaim = null;

            if (identity.GetType().Name == "ClaimsIdentity")
            {
                roleClaim = ((ClaimsIdentity)identity)
                     .Claims
                     .Where(c => c.Type.Contains("role"))
                     .FirstOrDefault();
            }

            return roleClaim != null ? roleClaim.Value : null;
        }
    }
}
