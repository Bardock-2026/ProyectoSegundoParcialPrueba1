using Microsoft.EntityFrameworkCore;
using ProyectoSegundoParcialPrueba1.Models.Espacios;
using ProyectoSegundoParcialPrueba1.Models.Personas;
using ProyectoSegundoParcialPrueba1.Models.Transacciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Datos
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-DQDC13N\\SQLEXPRESS;Database=HOTELRESERVAS2DO;User Id=sa;Password=1234;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Nombres exactos de tablas ---
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Habitacion>().ToTable("Habitaciones");
            modelBuilder.Entity<Reserva>().ToTable("Reservas");
            modelBuilder.Entity<Pago>().ToTable("Pagos");

            // --- Relación 1 a muchos: Cliente -> Reservas ---
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Reservas)
                .WithOne(r => r.Cliente)
                .HasForeignKey("ClienteId")
                .OnDelete(DeleteBehavior.Cascade);

            // --- Relación 1 a muchos: Habitacion -> Reservas ---
            modelBuilder.Entity<Habitacion>()
                .HasMany(h => h.Reservas)
                .WithOne(r => r.Habitacion)
                .HasForeignKey("HabitacionId")
                .OnDelete(DeleteBehavior.Restrict);

            // --- Relación 1 a 1: Reserva -> Pago ---
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Pago)
                .WithOne(p => p.Reserva)
                .HasForeignKey<Pago>("ReservaId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
