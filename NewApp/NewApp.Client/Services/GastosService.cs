﻿using System.Net.Http.Json;
using Dominio.Interfaces;
using Dominio.Models;

namespace NewApp.Client.Services;

public class GastosService(HttpClient httpClient) : IGastosRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Gasto> Get(int id)
    {
        return await _httpClient.GetFromJsonAsync<Gasto>($"/api/gastos/{id}");
    }

    public async Task<List<Gasto>> ListAll(DateTime mesAno, int categoriaId)
    {
        var url = $"/api/gastos?mesAno={mesAno:yyyy-MM}&categoriaId={categoriaId}";
        return await _httpClient.GetFromJsonAsync<List<Gasto>>(url);
    }

    public async Task<List<Tuple<string, decimal>>> ListarPorCategoria(DateTime mesAno)
    {
        var url = $"/api/gastos/listarPorCategoria?mesAno={mesAno:yyyy-MM}";
        return await _httpClient.GetFromJsonAsync<List<Tuple<string, decimal>>>(url);
    }

    public async Task<Gasto> Upsert(Gasto categoria)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/gastos", categoria);
        var response = await client.Content.ReadFromJsonAsync<Gasto>();
        return response;
    }
}
