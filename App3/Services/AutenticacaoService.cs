using System.Net.Http.Json;
using App3.Models;

namespace App3.Services;

public class AutenticacaoService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<LoginResponse> LoginAsync(string password)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/autenticacao", new { password });
        var response = await client.Content.ReadFromJsonAsync<LoginResponse>();
        return response;
    }

}
