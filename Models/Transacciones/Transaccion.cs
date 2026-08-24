using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Transacciones
{
    public class Transaccion
    {
        // --- CAMPOS PRIVADOS ---
        private int id;
        private DateTime fecha;

        // --- PROPIEDADES ---
        public int Id
        {
            get => id;
            set
            {
                // ✅ EF Core asigna el Id automáticamente, no validamos > 0
                id = value;
            }
        }

        public DateTime Fecha
        {
            get => fecha;
            set
            {
                if (value == default(DateTime))
                    throw new Exception("La fecha de la transacción no puede ser nula.");
                fecha = value;
            }
        }

        // --- CONSTRUCTOR VACÍO (EF Core) ---
        public Transaccion() { }

        // --- CONSTRUCTOR CON PARÁMETROS ---
        public Transaccion(int id, DateTime fecha)
        {
            this.Id = id; // EF Core lo asignará
            this.Fecha = fecha == default(DateTime) ? DateTime.Now : fecha;
        }

        // --- MÉTODO IMPRIMIR ---
        public void Imprimir()
        {
            Console.WriteLine("********** Transacción **********");
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Fecha: {this.Fecha}");
            Console.WriteLine("------------------------------------");
        }
    }
}
