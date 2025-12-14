using Microsoft.JSInterop;

namespace App3.Services;

public class LocalStorageToken(IJSRuntime jsRuntime)
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;
    private const string KEY = "token";

    public async Task<string> Get()
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", KEY);
    }

    public async Task Set(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", KEY, token);
    }
}
