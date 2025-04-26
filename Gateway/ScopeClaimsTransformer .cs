using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Gateway
{
    public class ScopeClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            if (identity == null)
                return Task.FromResult(principal);

            var scopeClaim = identity.FindFirst("scope");
            if (scopeClaim != null && scopeClaim.Value.Contains(' '))
            {
                var scopes = scopeClaim.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Zaten tek tek varsa yeniden eklemeyelim
                var existingScopes = identity.FindAll("scope").Select(x => x.Value).ToHashSet();

                foreach (var scope in scopes)
                    if (!existingScopes.Contains(scope))
                        identity.AddClaim(new Claim("scope", scope));
            }

            return Task.FromResult(principal);
        }
    }
}
