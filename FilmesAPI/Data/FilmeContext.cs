using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext
    {
        public DbSet<Filme> Filme { get; set; }
        public FilmeContext(DbContextOptions<FilmeContext> opts ) : base(opts)
        {

        }
    }
}
