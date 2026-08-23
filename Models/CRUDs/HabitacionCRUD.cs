using ProyectoSegundoParcialPrueba1.Models.Espacios;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class HabitacionCRUD
    {
        private static List<Habitacion> habitaciones = new List<Habitacion>();

        // --- CREAR ---
        public static void CrearHabitacion()
        {
            Console.Clear();
            Console.WriteLine("********** Crear Habitación **********");

            Console.Write("Ingrese ID de la habitación: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese tipo: ");
            string tipo = Console.ReadLine();

            Console.Write("Ingrese precio: ");
            decimal precio = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Ingrese estado (Disponible/Ocupada): ");
            string estado = Console.ReadLine();

            try
            {
                Habitacion objHabitacion = new Habitacion(id, tipo, precio, estado);
                habitaciones.Add(objHabitacion);

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
            Console.ReadLine();
        }

        // --- BUSCAR ---
        public static void BuscarHabitacion()
        {
            Console.Clear();
            Console.WriteLine("********** Buscar Habitación **********");
            Console.Write("Ingrese el ID de la habitación: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Habitacion objHabitacion = habitaciones.Find(h => h.Id == idIngresado);

            if (objHabitacion != null)
            {
                Console.WriteLine("Habitación encontrada!!");
                objHabitacion.Imprimir();
            }
            else
            {
                Console.WriteLine("Habitación NO encontrada...");
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

            Habitacion objHabitacion = habitaciones.Find(h => h.Id == idIngresado);

            if (objHabitacion != null)
            {
                objHabitacion.Imprimir();

                Console.Write("Ingrese nuevo tipo: ");
                objHabitacion.Tipo = Console.ReadLine();

                Console.Write("Ingrese nuevo precio: ");
                objHabitacion.Precio = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Ingrese nuevo estado (Disponible/Ocupada): ");
                objHabitacion.Estado = Console.ReadLine();

                Console.WriteLine("Habitación actualizada exitosamente!!");
            }
            else
            {
                Console.WriteLine("Habitación NO encontrada...");
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

            Habitacion objHabitacion = habitaciones.Find(h => h.Id == idIngresado);

            if (objHabitacion != null)
            {
                objHabitacion.Imprimir();
                Console.WriteLine($"¿Estás seguro que quieres eliminar la habitación ID {objHabitacion.Id}? S/N:");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    habitaciones.Remove(objHabitacion);
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
            Console.ReadLine();
        }

        // --- MÉTODO AUXILIAR ---
        public static Habitacion ObtenerHabitacionPorId(int id)
        {
            return habitaciones.Find(h => h.Id == id);
        }
    }
}

