using System.Net.Http.Json;
using App3.Models;

namespace App3.Services;

public class GastosService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;
    private List<Gasto> _gastos = [];

    public async Task CarregarGastos(DateTime mesAno, bool force = false)
    {
        var url = $"/api/gastos?mesAno={mesAno:yyyy-MM}";

        if (!_gastos.Any(a => a.Fecha.Month == mesAno.Month) || force)
            _gastos = await _httpClient.GetFromJsonAsync<List<Gasto>>(url);
    }

    public async Task<Gasto> Get(int id)
    {
        return _gastos.FirstOrDefault(f => f.Id == id);
    }


    public async Task<List<Gasto>> ListAll(DateTime mesAno, bool force = false)
    {
        await CarregarGastos(mesAno, force);
        return _gastos;
    }

    public async Task<List<Tuple<string, decimal>>> ListarPorCategoria(DateTime mesAno)
    {
        await CarregarGastos(mesAno);

        return _gastos.GroupBy(g => g.CategoriaNome)
                      .Select(g => new Tuple<string, decimal>(g.Key, g.Sum(s => s.Valor)))
                      .ToList();
    }

    public async Task<Gasto> Upsert(Gasto gasto)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/gastos", gasto);
        var response = await client.Content.ReadFromJsonAsync<Gasto>();

        await CarregarGastos(gasto.Fecha, force: true);

        return response;
    }
}
