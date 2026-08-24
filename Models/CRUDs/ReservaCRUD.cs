using ProyectoSegundoParcialPrueba1.Datos;
using ProyectoSegundoParcialPrueba1.Models.Espacios;
using ProyectoSegundoParcialPrueba1.Models.Personas;
using ProyectoSegundoParcialPrueba1.Models.Transacciones;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class ReservaCRUD
    {
        // --- CREAR ---
        public static void CrearReserva()
        {
            Console.Clear();
            Console.WriteLine("********** Crear Reserva **********");

            Console.Write("Ingrese ID del cliente: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese ID de la habitación: ");
            int idHabitacion = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese fecha inicio (yyyy-mm-dd): ");
            DateTime fechaInicio = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Ingrese fecha fin (yyyy-mm-dd): ");
            DateTime fechaFin = Convert.ToDateTime(Console.ReadLine());

            try
            {
                using (var context = new HotelDbContext())
                {
                    Cliente cliente = context.Clientes.Find(idCliente);
                    Habitacion habitacion = context.Habitaciones.Find(idHabitacion);

                    if (cliente == null)
                    {
                        Console.WriteLine("Cliente no encontrado.");
                        Console.ReadLine();
                        return;
                    }

                    if (habitacion == null || habitacion.Estado == "Ocupada")
                    {
                        Console.WriteLine("La habitación no está disponible.");
                        Console.ReadLine();
                        return;
                    }

                    Reserva objReserva = new Reserva(0, cliente, habitacion, fechaInicio, fechaFin);
                    context.Reservas.Add(objReserva);

                    // Cambiar estado de la habitación
                    habitacion.Estado = "Ocupada";

                    context.SaveChanges(); // ✅ guarda en SQL
                    Console.WriteLine("Reserva creada exitosamente!!");
                }
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

            using (var context = new HotelDbContext())
            {
                var reservas = context.Reservas
                                      .Include(r => r.Cliente)
                                      .Include(r => r.Habitacion)
                                      .ToList();

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

            using (var context = new HotelDbContext())
            {
                Reserva objReserva = context.Reservas
                                            .Include(r => r.Cliente)
                                            .Include(r => r.Habitacion)
                                            .FirstOrDefault(r => r.Id == idIngresado);

                if (objReserva != null)
                {
                    Console.WriteLine("Reserva encontrada!!");
                    objReserva.Imprimir();
                }
                else
                {
                    Console.WriteLine("Reserva NO encontrada...");
                }
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

            using (var context = new HotelDbContext())
            {
                Reserva objReserva = context.Reservas
                                            .Include(r => r.Habitacion)
                                            .FirstOrDefault(r => r.Id == idIngresado);

                if (objReserva != null)
                {
                    objReserva.Imprimir();

                    Console.Write("Ingrese nueva fecha inicio (yyyy-mm-dd): ");
                    objReserva.FechaInicio = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Ingrese nueva fecha fin (yyyy-mm-dd): ");
                    objReserva.FechaFin = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Ingrese el ID del nuevo cliente: ");
                    int idCliente = Convert.ToInt32(Console.ReadLine());
                    Cliente nuevoCliente = context.Clientes.Find(idCliente);
                    if (nuevoCliente != null)
                    {
                        objReserva.Cliente = nuevoCliente;
                    }

                    Console.Write("Ingrese el ID de la nueva habitación: ");
                    int idHabitacion = Convert.ToInt32(Console.ReadLine());
                    Habitacion nuevaHabitacion = context.Habitaciones.Find(idHabitacion);

                    if (nuevaHabitacion != null && nuevaHabitacion.Estado == "Disponible")
                    {
                        objReserva.Habitacion.Estado = "Disponible"; // liberar anterior
                        objReserva.Habitacion = nuevaHabitacion;
                        nuevaHabitacion.Estado = "Ocupada";
                    }

                    context.SaveChanges(); // ✅ guarda cambios en SQL
                    Console.WriteLine("Reserva actualizada exitosamente!!");
                }
                else
                {
                    Console.WriteLine("Reserva NO encontrada...");
                }
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

            using (var context = new HotelDbContext())
            {
                Reserva objReserva = context.Reservas
                                            .Include(r => r.Habitacion)
                                            .FirstOrDefault(r => r.Id == idIngresado);

                if (objReserva != null)
                {
                    objReserva.Imprimir();
                    Console.WriteLine($"¿Estás seguro que quieres eliminar la reserva ID {objReserva.Id}? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        objReserva.Habitacion.Estado = "Disponible"; // liberar habitación
                        context.Reservas.Remove(objReserva);
                        context.SaveChanges();
                        Console.WriteLine("Reserva eliminada exitosamente!!");
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
            }
            Console.ReadLine();
        }
    }
}

