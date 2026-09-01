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

            // ✅ Cambio: menú principal con dos opciones
            Console.WriteLine("Seleccione el modo de creación:");
            Console.WriteLine("1. Usar tipos predefinidos (Simple, Doble, Matrimonial, Familiar)");
            Console.WriteLine("2. Ingresar manualmente tipo y precio");

            Console.Write("\nIngrese opción (1-2): ");
            int modo = Convert.ToInt32(Console.ReadLine());

            string tipo;
            decimal precio;

            if (modo == 1)
            {
                // ✅ Cambio: menú de tipos predefinidos
                Console.WriteLine("\nSeleccione el tipo de habitación:");
                Console.WriteLine("1. Simple (60)");
                Console.WriteLine("2. Doble (100)");
                Console.WriteLine("3. Matrimonial (120)");
                Console.WriteLine("4. Familiar (150)");

                Console.Write("\nIngrese opción (1-4): ");
                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        tipo = "Simple";
                        precio = 60;
                        break;
                    case 2:
                        tipo = "Doble";
                        precio = 100;
                        break;
                    case 3:
                        tipo = "Matrimonial";
                        precio = 120;
                        break;
                    case 4:
                        tipo = "Familiar";
                        precio = 150;
                        break;
                    default:
                        throw new Exception("Opción inválida. Debe elegir entre 1 y 4.");
                }
            }
            else if (modo == 2)
            {
                // ✅ Cambio: ingreso manual de tipo y precio
                Console.Write("\nIngrese tipo de habitación: ");
                tipo = Console.ReadLine();

                Console.Write("Ingrese precio: ");
                precio = Convert.ToDecimal(Console.ReadLine());

                if (precio <= 0)
                    throw new Exception("El precio debe ser mayor a 0.");
            }
            else
            {
                throw new Exception("Opción inválida. Debe elegir 1 o 2.");
            }

            // ✅ Estado siempre será "Disponible"
            string estado = "Disponible";

            try
            {
                using (var context = new HotelDbContext())
                {
                    Habitacion objHabitacion = new Habitacion(tipo, precio, estado);
                    context.Habitaciones.Add(objHabitacion);   // ✅ se agrega al DbSet
                    context.SaveChanges();                     // ✅ se guarda en SQL
                }
                Console.WriteLine($"Habitación creada exitosamente!! Tipo: {tipo}, Precio: {precio}, Estado: {estado}");
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

            using (var context = new HotelDbContext())
            {
                // ✅ Cambio: listar todas las habitaciones con ID y datos completos
                var habitaciones = context.Habitaciones.ToList();

                if (habitaciones.Count == 0)
                {
                    Console.WriteLine("No hay habitaciones registradas.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("=== HABITACIONES DISPONIBLES ===");
                foreach (var h in habitaciones)
                {
                    Console.WriteLine($"ID Habitación: {h.Id}, Tipo: {h.Tipo}, Precio: {h.Precio}, Estado: {h.Estado}");
                }

                // ✅ Cambio: pedir ID de la habitación a actualizar
                Console.Write("\nIngrese el ID de la habitación a actualizar: ");
                int idIngresado = Convert.ToInt32(Console.ReadLine());

                Habitacion objHabitacion = habitaciones.FirstOrDefault(h => h.Id == idIngresado);

                if (objHabitacion != null)
                {
                    objHabitacion.Imprimir();

                    // ✅ Cambio: menú principal con dos opciones
                    Console.WriteLine("Seleccione el modo de actualización:");
                    Console.WriteLine("1. Usar tipos predefinidos (Simple, Doble, Matrimonial, Familiar)");
                    Console.WriteLine("2. Ingresar manualmente tipo y precio");

                    Console.Write("\nIngrese opción (1-2): ");
                    int modo = Convert.ToInt32(Console.ReadLine());

                    if (modo == 1)
                    {
                        // ✅ Cambio: menú de tipos con precios automáticos
                        Console.WriteLine("Seleccione el nuevo tipo de habitación:");
                        Console.WriteLine("1. Simple (60)");
                        Console.WriteLine("2. Doble (100)");
                        Console.WriteLine("3. Matrimonial (120)");
                        Console.WriteLine("4. Familiar (150)");

                        Console.Write("\nIngrese opción (1-4): ");
                        int opcion = Convert.ToInt32(Console.ReadLine());

                        switch (opcion)
                        {
                            case 1:
                                objHabitacion.Tipo = "Simple";
                                objHabitacion.Precio = 60;
                                break;
                            case 2:
                                objHabitacion.Tipo = "Doble";
                                objHabitacion.Precio = 100;
                                break;
                            case 3:
                                objHabitacion.Tipo = "Matrimonial";
                                objHabitacion.Precio = 120;
                                break;
                            case 4:
                                objHabitacion.Tipo = "Familiar";
                                objHabitacion.Precio = 150;
                                break;
                            default:
                                throw new Exception("Opción inválida. Debe elegir entre 1 y 4.");
                        }
                    }
                    else if (modo == 2)
                    {
                        // ✅ Cambio: ingreso manual de tipo y precio
                        Console.Write("Ingrese nuevo tipo: ");
                        objHabitacion.Tipo = Console.ReadLine();

                        Console.Write("Ingrese nuevo precio: ");
                        decimal nuevoPrecio = Convert.ToDecimal(Console.ReadLine());

                        if (nuevoPrecio <= 0)
                            throw new Exception("El precio debe ser mayor a 0.");

                        objHabitacion.Precio = nuevoPrecio;
                    }
                    else
                    {
                        throw new Exception("Opción inválida. Debe elegir 1 o 2.");
                    }

                    // ✅ Cambio: validación estricta del estado
                    Console.Write("Ingrese nuevo estado (Disponible/Ocupada): ");
                    string nuevoEstado = Console.ReadLine();

                    if (nuevoEstado != "Disponible" && nuevoEstado != "Ocupada")
                    {
                        throw new Exception("Estado inválido. Solo puede ser 'Disponible' u 'Ocupada'.");
                    }

                    objHabitacion.Estado = nuevoEstado;

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

            using (var context = new HotelDbContext())
            {
                // 🔹 Listar todas las habitaciones desde SQL
                var habitaciones = context.Habitaciones.ToList();

                if (habitaciones.Count == 0)
                {
                    Console.WriteLine("No hay habitaciones registradas.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("=== HABITACIONES DISPONIBLES ===");
                foreach (var h in habitaciones)
                {
                    Console.WriteLine($"ID: {h.Id}, Tipo: {h.Tipo}, Precio: {h.Precio}, Estado: {h.Estado}");
                }

                // 🔹 Pedir ID de la habitación a eliminar
                Console.Write("\nIngrese el ID de la habitación a eliminar: ");
                int idIngresado = Convert.ToInt32(Console.ReadLine());

                Habitacion objHabitacion = habitaciones.FirstOrDefault(h => h.Id == idIngresado);

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

