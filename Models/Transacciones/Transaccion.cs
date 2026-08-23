using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Transacciones
{
    public class Transaccion
    {
        private int id;
        private DateTime fecha;

        public int Id
        {
            get => id;
            set
            {
                if (value <= 0)
                    throw new Exception("El ID de la transacción debe ser mayor a 0.");
                id = value;
            }
        }

        public DateTime Fecha
        {
            get => fecha;
            set
            {
                if (value == null)
                    throw new Exception("La fecha de la transacción no puede ser nula.");
                fecha = value;
            }
        }

        public Transaccion(int id, DateTime fecha)
        {
            this.Id = id;
            this.Fecha = fecha;
        }


        // --- MÉTODO IMPRIMIR ---
        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Fecha: {this.Fecha}");
            Console.WriteLine("------------------------------------");
        }
    }
}
