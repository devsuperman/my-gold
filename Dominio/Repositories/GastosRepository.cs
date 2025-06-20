using Microsoft.EntityFrameworkCore;
using Dominio.Models;
using Dominio.Data;

namespace Dominio.Repositories;

public class GastosRepository(Contexto db)
{
    private readonly Contexto _db = db;

    public async Task<Gasto> Get(int id)
    {
        return await _db.Gastos.FindAsync(id);
    }

    public async Task<List<Gasto>> ListAll(DateTime mesAno)
    {
        try
        {
            var query = _db.Gastos
                .AsNoTrackingWithIdentityResolution()
                .Include(a => a.Categoria)
                .Where(w =>
                    w.Fecha.Month == mesAno.Month &&
                    w.Fecha.Year == mesAno.Year);

            var lista = await query
                .OrderByDescending(o => o.Fecha)
                .Select(s => new Gasto
                {
                    Id = s.Id,
                    Fecha = s.Fecha,
                    Nombre = s.Nombre,
                    Valor = s.Valor,
                    CategoriaId = s.CategoriaId,
                    CategoriaNome = s.Categoria.Nombre
                })
                .ToListAsync();

            return lista;
        }
        catch (Npgsql.PostgresException ex)
        {
            if (ex.Message.Contains("starting up"))
            {
                await Task.Delay(5000);
                return await ListAll(mesAno);
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