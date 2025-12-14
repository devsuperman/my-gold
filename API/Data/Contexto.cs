using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Gasto> Gastos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}
