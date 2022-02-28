using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace AntDesignUIApp;

public class FakeAuthStateProvider : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonymous = new ClaimsIdentity();
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
    }
}
