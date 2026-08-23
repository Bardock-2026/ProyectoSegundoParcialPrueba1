using ProyectoSegundoParcialPrueba1.Models.Espacios;
using ProyectoSegundoParcialPrueba1.Models.Personas;
using ProyectoSegundoParcialPrueba1.Models.Transacciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class ReservaCRUD
    {
        private static List<Reserva> reservas = new List<Reserva>();

        // --- CREAR ---
        public static void CrearReserva()
        {
            Console.Clear();
            Console.WriteLine("********** Crear Reserva **********");

            Console.Write("Ingrese ID de la reserva: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese ID del cliente: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());
            Cliente cliente = ClienteCRUD.ObtenerClientePorId(idCliente);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                Console.ReadLine();
                return;
            }

            Console.Write("Ingrese ID de la habitación: ");
            int idHabitacion = Convert.ToInt32(Console.ReadLine());
            Habitacion habitacion = HabitacionCRUD.ObtenerHabitacionPorId(idHabitacion);

            if (habitacion == null || habitacion.Estado == "Ocupada")
            {
                Console.WriteLine("La habitación no está disponible.");
                Console.ReadLine();
                return;
            }

            Console.Write("Ingrese fecha inicio (yyyy-MM-dd): ");
            DateTime inicio = DateTime.Parse(Console.ReadLine());
            Console.Write("Ingrese fecha fin (yyyy-MM-dd): ");
            DateTime fin = DateTime.Parse(Console.ReadLine());

            try
            {
                Reserva reserva = new Reserva(id, cliente, habitacion, inicio, fin);
                reservas.Add(reserva);

                habitacion.Estado = "Ocupada"; // marcar habitación como ocupada

                Console.WriteLine("Reserva creada exitosamente!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        // --- LISTAR ---
        public static void ListarReservas()
        {
            Console.Clear();
            Console.WriteLine("********** Reservas Registradas **********");

            if (reservas.Count == 0)
            {
                Console.WriteLine("No hay reservas registradas.");
            }
            else
            {
                foreach (Reserva r in reservas)
                {
                    r.Imprimir();
                }
            }
            Console.ReadLine();
        }

        // --- BUSCAR ---
        public static void BuscarReserva()
        {
            Console.Clear();
            Console.WriteLine("********** Buscar Reserva **********");
            Console.Write("Ingrese el ID de la reserva: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Reserva objReserva = reservas.Find(r => r.Id == idIngresado);

            if (objReserva != null)
            {
                Console.WriteLine("Reserva encontrada!!");
                objReserva.Imprimir();
            }
            else
            {
                Console.WriteLine("Reserva NO encontrada...");
            }
            Console.ReadLine();
        }

        // --- ACTUALIZAR ---
        public static void ActualizarReserva()
        {
            Console.Clear();
            Console.WriteLine("********** Actualizar Reserva **********");
            Console.Write("Ingrese el ID de la reserva a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Reserva objReserva = reservas.Find(r => r.Id == idIngresado);

            if (objReserva != null)
            {
                objReserva.Imprimir();

                Console.Write("Ingrese nueva fecha inicio (yyyy-MM-dd): ");
                objReserva.FechaInicio = DateTime.Parse(Console.ReadLine());

                Console.Write("Ingrese nueva fecha fin (yyyy-MM-dd): ");
                objReserva.FechaFin = DateTime.Parse(Console.ReadLine());

                Console.Write("Ingrese el ID del nuevo cliente: ");
                int idCliente = Convert.ToInt32(Console.ReadLine());
                Cliente nuevoCliente = ClienteCRUD.ObtenerClientePorId(idCliente);

                if (nuevoCliente != null)
                {
                    objReserva.Cliente = nuevoCliente;
                    Console.WriteLine("Cliente actualizado exitosamente!!");
                }

                Console.Write("Ingrese el ID de la nueva habitación: ");
                int idHabitacion = Convert.ToInt32(Console.ReadLine());
                Habitacion nuevaHabitacion = HabitacionCRUD.ObtenerHabitacionPorId(idHabitacion);

                if (nuevaHabitacion != null && nuevaHabitacion.Estado == "Disponible")
                {
                    objReserva.Habitacion.Estado = "Disponible"; // liberar la anterior
                    objReserva.Habitacion = nuevaHabitacion;
                    nuevaHabitacion.Estado = "Ocupada";
                    Console.WriteLine("Habitación actualizada exitosamente!!");
                }

                Console.WriteLine("Reserva actualizada exitosamente!!");
            }
            else
            {
                Console.WriteLine("Reserva NO encontrada...");
            }
            Console.ReadLine();
        }

        // --- ELIMINAR ---
        public static void EliminarReserva()
        {
            Console.Clear();
            Console.WriteLine("********** Eliminar Reserva **********");
            Console.Write("Ingrese el ID de la reserva a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Reserva objReserva = reservas.Find(r => r.Id == idIngresado);

            if (objReserva != null)
            {
                objReserva.Imprimir();
                Console.WriteLine($"¿Estás seguro que quieres eliminar la reserva ID {objReserva.Id}? S/N:");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    objReserva.Habitacion.Estado = "Disponible"; // liberar habitación
                    reservas.Remove(objReserva);

                    Console.WriteLine("Reserva eliminada y habitación liberada!!");
                }
                else
                {
                    Console.WriteLine("Operación cancelada!!");
                }
            }
            else
            {
                Console.WriteLine("Reserva NO encontrada!!");
            }
            Console.ReadLine();
        }

        // --- MÉTODO AUXILIAR ---
        public static Reserva ObtenerReservaPorId(int id)
        {
            return reservas.Find(r => r.Id == id);
        }
    }
}

