using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Gateway.Handlers
{
    public class DiscountAudienceValidationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = request.Headers.Authorization;
            if (authorizationHeader != null && authorizationHeader.Scheme == "Bearer")
            {
                var token = authorizationHeader.Parameter;
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var hasAudience = jwtToken.Audiences.Contains("exchange.api");

                var scopes = jwtToken.Claims.Where(c => c.Type == "scope")!.First();

                var scope = scopes.Value;
                if (!scope.Contains("profile")) return new HttpResponseMessage(HttpStatusCode.Forbidden);

                if (!hasAudience) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
