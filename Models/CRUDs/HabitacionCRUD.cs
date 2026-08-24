using ProyectoSegundoParcialPrueba1.Datos;
using ProyectoSegundoParcialPrueba1.Models.Espacios;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class HabitacionCRUD
    {
        // --- CREAR ---
        public static void CrearHabitacion()
        {
            Console.Clear();
            Console.WriteLine("********** Crear Habitación **********");

            Console.Write("Ingrese tipo: ");
            string tipo = Console.ReadLine();

            Console.Write("Ingrese precio: ");
            decimal precio = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Ingrese estado (Disponible/Ocupada): ");
            string estado = Console.ReadLine();

            try
            {
                using (var context = new HotelDbContext())
                {
                    Habitacion objHabitacion = new Habitacion(tipo, precio, estado);
                    context.Habitaciones.Add(objHabitacion);   // ✅ se agrega al DbSet
                    context.SaveChanges();                     // ✅ se guarda en SQL
                }
                Console.WriteLine("Habitación creada exitosamente!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        // --- LISTAR ---
        public static void ListarHabitaciones()
        {
            Console.Clear();
            Console.WriteLine("********** Habitaciones Registradas **********");

            using (var context = new HotelDbContext())
            {
                var habitaciones = context.Habitaciones.ToList();  // ✅ trae de SQL

                if (habitaciones.Count == 0)
                {
                    Console.WriteLine("No hay habitaciones registradas.");
                }
                else
                {
                    foreach (Habitacion h in habitaciones)
                    {
                        h.Imprimir();
                    }
                }
            }
            Console.ReadLine();
        }

        // --- BUSCAR ---
        public static void BuscarHabitacion()
        {
            Console.Clear();
            Console.WriteLine("********** Buscar Habitación **********");
            Console.Write("Ingrese el ID de la habitación: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Habitacion objHabitacion = context.Habitaciones.Find(idIngresado); // ✅ busca en SQL

                if (objHabitacion != null)
                {
                    Console.WriteLine("Habitación encontrada!!");
                    objHabitacion.Imprimir();
                }
                else
                {
                    Console.WriteLine("Habitación NO encontrada...");
                }
            }
            Console.ReadLine();
        }

        // --- ACTUALIZAR ---
        public static void ActualizarHabitacion()
        {
            Console.Clear();
            Console.WriteLine("********** Actualizar Habitación **********");
            Console.Write("Ingrese el ID de la habitación a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Habitacion objHabitacion = context.Habitaciones.Find(idIngresado);

                if (objHabitacion != null)
                {
                    objHabitacion.Imprimir();

                    Console.Write("Ingrese nuevo tipo: ");
                    objHabitacion.Tipo = Console.ReadLine();

                    Console.Write("Ingrese nuevo precio: ");
                    objHabitacion.Precio = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Ingrese nuevo estado (Disponible/Ocupada): ");
                    objHabitacion.Estado = Console.ReadLine();

                    context.SaveChanges();   // ✅ guarda cambios en SQL
                    Console.WriteLine("Habitación actualizada exitosamente!!");
                }
                else
                {
                    Console.WriteLine("Habitación NO encontrada...");
                }
            }
            Console.ReadLine();
        }

        // --- ELIMINAR ---
        public static void EliminarHabitacion()
        {
            Console.Clear();
            Console.WriteLine("********** Eliminar Habitación **********");
            Console.Write("Ingrese el ID de la habitación a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Habitacion objHabitacion = context.Habitaciones.Find(idIngresado);

                if (objHabitacion != null)
                {
                    objHabitacion.Imprimir();
                    Console.WriteLine($"¿Estás seguro que quieres eliminar la habitación ID {objHabitacion.Id}? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        context.Habitaciones.Remove(objHabitacion); // ✅ elimina en SQL
                        context.SaveChanges();
                        Console.WriteLine("Habitación eliminada exitosamente!!");
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada!!");
                    }
                }
                else
                {
                    Console.WriteLine("Habitación NO encontrada!!");
                }
            }
            Console.ReadLine();
        }
    }
}