using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Data;

namespace Dominio.Repositories;

public class CategoriasRepository(Contexto db) : ICategoriasRepository
{
    private readonly Contexto _db = db;

    public async Task<Categoria> Get(int id)
    {
        return await _db.Categorias.FindAsync(id);
    }

    public async Task<List<Categoria>> ListAll()
    {
        try
        {
            return await _db.Categorias.AsNoTracking().OrderBy(o => o.Nombre).ToListAsync();            
        }
        catch (Npgsql.PostgresException ex)
        {
            if (ex.Message.Contains("starting up"))
            {
                await Task.Delay(5000);
                return await ListAll();
            }

            throw;
        }
    }

    public async Task<Categoria> Upsert(Categoria categoria)
    {
        if (categoria.Id == 0)
            await _db.AddAsync(categoria);
        else
            _db.Update(categoria);

        await _db.SaveChangesAsync();

        return categoria;
    }
}