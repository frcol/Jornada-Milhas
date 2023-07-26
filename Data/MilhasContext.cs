using Alura_JornadaMilhas.Models;
using Microsoft.EntityFrameworkCore;

namespace Alura_JornadaMilhas.Data;

public class MilhasContext : DbContext
{
    public DbSet<Depoimento> Depoimentos { get; set; }
    public DbSet<Destino> Destinos { get; set; }

    public MilhasContext(DbContextOptions<MilhasContext> options) : base(options)
    {

    }
}
