using MicroservicioGranja.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MicroservicioGranja.Data
{
    public class GranjaDbContext : DbContext
    {
        public GranjaDbContext(DbContextOptions<GranjaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Granja> Granjas { get; set; }
        public DbSet<Animal> Animales { get; set; }
        public DbSet<AnimalVeterinario> AnimalesVeterinarios { get; set; }
        public DbSet<AnimalDieta> AnimalesDietas { get; set; }
        public DbSet<CertificacionSanitaria> CertificacionesSanitarias { get; set; }
        public DbSet<AnimalCertificacion> AnimalesCertificaciones { get; set; }
        public DbSet<LoteLeche> LotesLeche { get; set; }
        public DbSet<TipoAnalisis> TiposAnalisis { get; set; }
        public DbSet<LoteAnalisisCalidad> LotesAnalisisCalidad { get; set; }
        public DbSet<TratamientoSanitario> TratamientosSanitarios { get; set; }
        public DbSet<EntregaFabrica> EntregasFabrica { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AnimalVeterinario>()
                .HasKey(x => new
                {
                    x.IdAnimal,
                    x.IdVeterinario
                });

            modelBuilder.Entity<AnimalDieta>()
                .HasKey(x => new
                {
                    x.IdAnimal,
                    x.IdInsumo
                });

            modelBuilder.Entity<AnimalCertificacion>()
                .HasKey(x => new
                {
                    x.IdAnimal,
                    x.IdCertificacion
                });

            modelBuilder.Entity<LoteAnalisisCalidad>()
                .HasKey(x => new
                {
                    x.IdLote,
                    x.IdTipoAnalisis
                });

            modelBuilder.Entity<Animal>()
                .HasOne(x => x.Granja)
                .WithMany(x => x.Animales)
                .HasForeignKey(x => x.IdGranja)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoteLeche>()
                .HasOne(x => x.Granja)
                .WithMany(x => x.LotesLeche)
                .HasForeignKey(x => x.IdGranja)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimalVeterinario>()
                .HasOne(x => x.Animal)
                .WithMany(x => x.Veterinarios)
                .HasForeignKey(x => x.IdAnimal)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimalDieta>()
                .HasOne(x => x.Animal)
                .WithMany(x => x.Dietas)
                .HasForeignKey(x => x.IdAnimal)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimalCertificacion>()
                .HasOne(x => x.Animal)
                .WithMany(x => x.Certificaciones)
                .HasForeignKey(x => x.IdAnimal)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimalCertificacion>()
                .HasOne(x => x.CertificacionSanitaria)
                .WithMany(x => x.Animales)
                .HasForeignKey(x => x.IdCertificacion)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoteAnalisisCalidad>()
                .HasOne(x => x.LoteLeche)
                .WithMany(x => x.AnalisisCalidad)
                .HasForeignKey(x => x.IdLote)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoteAnalisisCalidad>()
                .HasOne(x => x.TipoAnalisis)
                .WithMany(x => x.Lotes)
                .HasForeignKey(x => x.IdTipoAnalisis)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TratamientoSanitario>()
                .HasOne(x => x.Animal)
                .WithMany(x => x.Tratamientos)
                .HasForeignKey(x => x.IdAnimal)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntregaFabrica>()
                .HasOne(x => x.LoteLeche)
                .WithMany(x => x.Entregas)
                .HasForeignKey(x => x.IdLote)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
