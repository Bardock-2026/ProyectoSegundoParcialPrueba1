using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.IA
{
    // ✅ Clase entidad
    public class Conversacion
    {
        private int _id;
        private string _cliente;
        private string _categoria;
        private string _pregunta;
        private string _respuesta;
        private DateTime _fecha;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0) throw new Exception("El Id debe ser mayor a cero.");
                _id = value;
            }
        }

        public string Cliente
        {
            get { return _cliente; }
            set
            {
                if (value == null || value == "") throw new Exception("El cliente no puede estar vacío.");
                _cliente = value;
            }
        }

        public string Categoria
        {
            get { return _categoria; }
            set
            {
                if (value == null || value == "") throw new Exception("La categoría no puede estar vacía.");
                _categoria = value;
            }
        }

        public string Pregunta
        {
            get { return _pregunta; }
            set
            {
                if (value == null || value == "") throw new Exception("La pregunta no puede estar vacía.");
                _pregunta = value;
            }
        }

        public string Respuesta
        {
            get { return _respuesta; }
            set
            {
                if (value == null || value == "") throw new Exception("La respuesta no puede estar vacía.");
                _respuesta = value;
            }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set
            {
                if (value == null) throw new Exception("La fecha no puede ser nula.");
                _fecha = value;
            }
        }
    }

    // ✅ Clase DbContext
    public class ChatContext : DbContext
    {
        public DbSet<Conversacion> Conversaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-DQDC13N\\SQLEXPRESS;Database=HOTELRESERVAS2DO;User Id=sa;Password=1234;TrustServerCertificate=True;",
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    )
                );
            }
        }
    }
}