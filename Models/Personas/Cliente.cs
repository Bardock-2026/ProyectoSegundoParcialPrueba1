using ProyectoSegundoParcialPrueba1.Models.Transacciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Personas
{
    public class Cliente : Persona
    {
        // Propiedades de navegación (relación con reservas)
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        // --- CONSTRUCTOR VACÍO (EF Core) ---
        public Cliente() { }

        // --- CONSTRUCTOR CON PARÁMETROS ---
        public Cliente(string nombre, string cedula, string telefono, string email, string ciudad)
            : base(nombre, cedula, telefono, email, ciudad)
        {
        }

        // --- MÉTODO IMPRIMIR ---
        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Nombre: {this.Nombre}");
            Console.WriteLine($"Cédula: {this.Cedula}");
            Console.WriteLine($"Teléfono: {this.Telefono}");
            Console.WriteLine($"Email: {this.Email}");
            Console.WriteLine($"Ciudad: {this.Ciudad}");
            Console.WriteLine("------------------------------------");
        }
    }
}