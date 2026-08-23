using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Personas
{
    public class Cliente : Persona
    {
        public Cliente(int id, string nombre, string cedula, string telefono, string email, string ciudad)
            : base(id, nombre, cedula, telefono, email, ciudad)
        {
        }

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
