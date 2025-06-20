using System.Net.Http.Json;
using Dominio.Models;

namespace App3.Services;

public class CategoriasService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;
    private List<Categoria> _categorias = [];

    public async Task CarregarCategorias(bool force = false)
    {
        if (!_categorias.Any() || force)
            _categorias = await _httpClient.GetFromJsonAsync<List<Categoria>>("/api/categorias");
    }
    public async Task<Categoria> Get(int id)
    {
        await CarregarCategorias();
        return _categorias.FirstOrDefault(c => c.Id == id);
    }    

    public async Task<List<Categoria>> ListAll()
    {
        await CarregarCategorias();
        return _categorias;
    }

    public async Task<Categoria> Upsert(Categoria categoria)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/categorias", categoria);
        var response = await client.Content.ReadFromJsonAsync<Categoria>();

        await CarregarCategorias(force: true);

        return response;
    }
}
