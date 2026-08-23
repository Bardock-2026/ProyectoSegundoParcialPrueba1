using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Espacios
{
    public class Habitacion
    {
        private int id;
        private string tipo;
        private decimal precio;
        private string estado;

        public int Id
        {
            get => id;
            set
            {
                if (value <= 0)
                    throw new Exception("El ID debe ser mayor a 0.");
                id = value;
            }
        }

        public string Tipo
        {
            get => tipo;
            set
            {
                if (value == null || value == "")
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

        public Habitacion(int id, string tipo, decimal precio, string estado = "Disponible")
        {
            this.Id = id;
            this.Tipo = tipo;
            this.Precio = precio;
            this.Estado = estado;
        }

        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Tipo: {this.Tipo}");
            Console.WriteLine($"Precio: {this.Precio}");
            Console.WriteLine($"Estado: {this.Estado}");
            Console.WriteLine("------------------------------------");
        }

    }
}
