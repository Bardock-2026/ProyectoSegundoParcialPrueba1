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

        public Cliente Cliente
        {
            get => cliente;
            set
            {
                if (value == null)
                    throw new Exception("Debe seleccionar un cliente válido.");
                cliente = value;
            }
        }

        public Habitacion Habitacion
        {
            get => habitacion;
            set
            {
                if (value == null)
                    throw new Exception("Debe seleccionar una habitación válida.");
                habitacion = value;
            }
        }

        public DateTime FechaInicio
        {
            get => fechaInicio;
            set
            {
                if (value == null)
                    throw new Exception("La fecha de inicio no puede ser nula.");
                fechaInicio = value;
            }
        }

        public DateTime FechaFin
        {
            get => fechaFin;
            set
            {
                if (value <= fechaInicio)
                    throw new Exception("La fecha de fin debe ser mayor a la fecha de inicio.");
                fechaFin = value;
            }
        }

        public Reserva(int id, Cliente cliente, Habitacion habitacion, DateTime fechaInicio, DateTime fechaFin)
            : base(id, DateTime.Now)
        {
            this.Cliente = cliente;
            this.Habitacion = habitacion;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
        }

        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Cliente: {this.Cliente.Nombre}");
            Console.WriteLine($"Habitación ID: {this.Habitacion.Id}");
            Console.WriteLine($"Fecha Inicio: {this.FechaInicio.ToShortDateString()}");
            Console.WriteLine($"Fecha Fin: {this.FechaFin.ToShortDateString()}");
            Console.WriteLine("------------------------------------");
        }
    }

}
