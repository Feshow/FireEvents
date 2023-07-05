using FireEvents.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FireEvents.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Evento> Eventos { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Evento>().HasData(
        //        new Evento()
        //        {
        //            Id = 1,
        //            Tema = "Evento teste",
        //            Endereco = "São Paulo",
        //            DataEvento = DateTime.Now,
        //            QtdPessoas = 100,
        //            Lote = 1,
        //            ImagemUrl = "imagem.png"
        //        });
        //}
    }
}
