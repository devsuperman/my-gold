using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.DTOs;
using Dominio.Data;

namespace Dominio.Repositories;

public class GastosRepository(Contexto db) : IGastosRepository
{
    private readonly Contexto _db = db;

    public async Task<Gasto> Get(int id)
    {
        return await _db.Gastos.FindAsync(id);
    }

    public async Task<List<ListarGasto>> ListAll(DateTime mesAno, int categoriaId)
    {
        try
        {
            var query = _db.Gastos
                .AsNoTrackingWithIdentityResolution()
                .Include(a => a.Categoria)
                .Where(w =>
                    w.Fecha.Month == mesAno.Month &&
                    w.Fecha.Year == mesAno.Year);

            if (categoriaId > 0)
                query = query.Where(w => w.CategoriaId == categoriaId);

            var lista = await query
                .OrderByDescending(o => o.Fecha)
                .Select(s => new ListarGasto
                {
                    Id = s.Id,
                    Fecha = s.Fecha,
                    Nombre = s.Nombre,
                    Valor = s.Valor.Value,
                    CategoriaId = s.CategoriaId,
                    Categoria = s.Categoria.Nombre
                })
                .ToListAsync();

            return lista;
        }
        catch (Npgsql.PostgresException ex)
        {
            if (ex.Message.Contains("starting up"))
            {
                await Task.Delay(5000);
                return await ListAll(mesAno, categoriaId);
            }

            throw;
        }


    }

    public async Task<List<Tuple<string, decimal>>> ListarPorCategoria(DateTime mesAno)
    {
        try
        {
            var totaisPorCategoria = await _db.Gastos
                .Where(w =>
                    w.Fecha.Month == mesAno.Month &&
                    w.Fecha.Year == mesAno.Year)
                .Select(s => new
                {
                    Categoria = s.Categoria.Nombre,
                    s.Valor
                })
                .GroupBy(g => g.Categoria)
                .Select(s => new Tuple<string, decimal>(s.Key, s.Sum(d => d.Valor.Value)))
                .ToListAsync();

            totaisPorCategoria = totaisPorCategoria.OrderByDescending(o => o.Item2).ToList();

            return totaisPorCategoria;
        }
        catch (Npgsql.PostgresException ex)
        {
            if (ex.Message.Contains("starting up"))
            {
                await Task.Delay(5000);
                return await ListarPorCategoria(mesAno);
            }

            throw;
        }
    }

    public async Task<Gasto> Upsert(Gasto model)
    {
        if (model.Id == 0)
            await _db.AddAsync(model);
        else
            _db.Update(model);

        await _db.SaveChangesAsync();

        return model;
    }
}