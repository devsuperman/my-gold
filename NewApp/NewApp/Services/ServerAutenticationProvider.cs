using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace NewApp.Services;

public class ServerAutenticationProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        }

        return Task.FromResult(new AuthenticationState(user));
    }    
}
