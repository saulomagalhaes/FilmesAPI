using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data;

public class AppDbContext : DbContext
{
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> opts ) : base(opts)
    {

    }
}
