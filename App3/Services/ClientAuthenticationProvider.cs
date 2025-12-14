using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace App3.Services;

public class ClientAuthenticationProvider(LocalStorageToken tokenStorage, HttpClient http, ILogger<ClientAuthenticationProvider> logger) : AuthenticationStateProvider
{
    private readonly ILogger<ClientAuthenticationProvider> _logger = logger;
    private readonly LocalStorageToken _tokenStorage = tokenStorage;
    private readonly HttpClient _http = http;
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return await LoadAuthenticationState();
    }

    private async Task<AuthenticationState> LoadAuthenticationState()
    {
        var token = await _tokenStorage.Get();

        var identity = new ClaimsIdentity();

        if (!string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            _http.DefaultRequestHeaders.Authorization = null;
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        try
        {
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }
        catch (System.Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return state;
    }

    public async Task LoginAsync(string token)
    {
        await _tokenStorage.Set(token);
        await LoadAuthenticationState();
    }

    public async Task LogoutAsync()
    {
        await _tokenStorage.Set("");
        await LoadAuthenticationState();
    }
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
