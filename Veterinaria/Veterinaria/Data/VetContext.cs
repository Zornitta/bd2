using Veterinaria.Models;
using Microsoft.EntityFrameworkCore;
namespace Veterinaria.Data
{
    public class VetContext : DbContext
    {
        public VetContext(DbContextOptions<VetContext> options) : base(options)
        {
        }
        public DbSet<Animal> Animais { get; set; }
        public DbSet<Vacina> Vacinas { get; set; }
        public DbSet<TipoAnimal> TiposAnimais { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().ToTable("Animais");
            modelBuilder.Entity<Vacina>().ToTable("Vacinas");
        }
    }
}
