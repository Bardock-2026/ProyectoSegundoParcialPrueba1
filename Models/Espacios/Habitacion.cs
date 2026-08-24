using ProyectoSegundoParcialPrueba1.Models.Transacciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Espacios
{
    public class Habitacion
    {
        // --- CAMPOS PRIVADOS ---
        private string tipo;
        private decimal precio;
        private string estado;

        // --- PROPIEDADES ---
        public int Id { get; set; } // SQL lo asigna automáticamente

        public string Tipo
        {
            get => tipo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("El tipo no puede estar vacío.");
                tipo = value;
            }
        }

        public decimal Precio
        {
            get => precio;
            set
            {
                if (value <= 0)
                    throw new Exception("El precio debe ser mayor a 0.");
                precio = value;
            }
        }

        public string Estado
        {
            get => estado;
            set
            {
                if (value != "Disponible" && value != "Ocupada")
                    throw new Exception("El estado debe ser Disponible u Ocupada.");
                estado = value;
            }
        }

        // ✅ Relación con Reservas (1 a muchos)
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        // --- CONSTRUCTOR VACÍO (EF Core) ---
        public Habitacion() { }

        // --- CONSTRUCTOR CON PARÁMETROS ---
        public Habitacion(string tipo, decimal precio, string estado = "Disponible")
        {
            this.Tipo = tipo;
            this.Precio = precio;
            this.Estado = estado;
        }

        // --- MÉTODO IMPRIMIR ---
        public new void Imprimir() // usamos "new" porque oculta el heredado de Transaccion/Espacio
        {
            Console.WriteLine("********** Habitación **********");
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Tipo: {this.Tipo}");
            Console.WriteLine($"Precio: {this.Precio}");
            Console.WriteLine($"Estado: {this.Estado}");
            Console.WriteLine("------------------------------------");
        }
    }
}
