using ProyectoSegundoParcialPrueba1.Models.Espacios;
using ProyectoSegundoParcialPrueba1.Models.Personas;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Transacciones
{
    public class Reserva : Transaccion
    {
        private Cliente cliente;
        private Habitacion habitacion;
        private DateTime fechaInicio;
        private DateTime fechaFin;

        public Cliente Cliente { get; set; }
        public Habitacion Habitacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // ✅ Propiedad de navegación para EF Core
        public Pago Pago { get; set; }

        public Reserva() { }

        public Reserva(int id, Cliente cliente, Habitacion habitacion, DateTime fechaInicio, DateTime fechaFin)
            : base(id, fechaInicio)
        {
            this.Cliente = cliente;
            this.Habitacion = habitacion;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
        }

        public void Imprimir()
        {
            Console.WriteLine("********** Reserva **********");
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Cliente: {this.Cliente.Nombre}");
            Console.WriteLine($"Habitación ID: {this.Habitacion.Id}");
            Console.WriteLine($"Fecha Inicio: {this.FechaInicio.ToShortDateString()}");
            Console.WriteLine($"Fecha Fin: {this.FechaFin.ToShortDateString()}");
            Console.WriteLine("------------------------------------");
        }

    }
}
