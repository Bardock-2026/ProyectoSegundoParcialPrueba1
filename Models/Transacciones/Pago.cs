using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Transacciones
{
    public class Pago : Transaccion
    {
        private Reserva reserva;
        private decimal monto;
        private DateTime fechaPago;

        // --- PROPIEDADES CON VALIDACIÓN ---
        public Reserva Reserva
        {
            get => reserva;
            set
            {
                if (value == null)
                    throw new Exception("La reserva asociada al pago no existe.");
                reserva = value;
            }
        }

        public decimal Monto
        {
            get => monto;
            set
            {
                if (value <= 0)
                    throw new Exception("El monto debe ser mayor a 0.");
                monto = value;
            }
        }

        public DateTime FechaPago
        {
            get => fechaPago;
            set
            {
                if (value == default(DateTime))
                    throw new Exception("La fecha de pago no puede ser nula.");
                fechaPago = value;
            }
        }

        // --- CONSTRUCTOR VACÍO (EF Core) ---
        public Pago() { }

        // --- CONSTRUCTOR COMPLETO (cuando quieras pasar todo manualmente) ---
        public Pago(int id, Reserva reserva, decimal monto, DateTime fechaPago)
            : base(id, fechaPago) // pasamos id y fecha a Transaccion
        {
            Reserva = reserva;
            Monto = monto;
            FechaPago = fechaPago;
        }

        // --- CONSTRUCTOR SIMPLIFICADO (para CRUD con EF Core) ---
        public Pago(Reserva reserva, decimal monto)
            : base(0, DateTime.Now) // EF Core asigna el ID, y la fecha se pone automáticamente
        {
            Reserva = reserva;
            Monto = monto;
            FechaPago = DateTime.Now;
        }

        // --- MÉTODO IMPRIMIR ---
        public void Imprimir()
        {
            Console.WriteLine("********** Pago **********");
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Reserva: {this.Reserva.Id}");
            Console.WriteLine($"Monto: {this.Monto}");
            Console.WriteLine($"Fecha de Pago: {this.FechaPago}");
            Console.WriteLine("------------------------------------");
        }
    }
}
