using ProyectoSegundoParcialPrueba1.Datos;
using ProyectoSegundoParcialPrueba1.Models.Personas;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class ClienteCRUD
    {
        // --- CREAR ---
        public static void CrearCliente()
        {
            Console.Clear();
            Console.WriteLine("********** Crear Cliente **********");

            Console.Write("Ingrese nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese cédula: ");
            string cedula = Console.ReadLine();
            Console.Write("Ingrese teléfono: ");
            string telefono = Console.ReadLine();
            Console.Write("Ingrese email: ");
            string email = Console.ReadLine();
            Console.Write("Ingrese ciudad: ");
            string ciudad = Console.ReadLine();

            try
            {
                using (var context = new HotelDbContext())
                {
                    Cliente objCliente = new Cliente(nombre, cedula, telefono, email, ciudad);
                    context.Clientes.Add(objCliente);   // ✅ se agrega al DbSet
                    context.SaveChanges();              // ✅ se guarda en SQL
                }
                Console.WriteLine("Cliente creado exitosamente!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        // --- LISTAR ---
        public static void ListarClientes()
        {
            Console.Clear();
            Console.WriteLine("********** Clientes Registrados **********");

            using (var context = new HotelDbContext())
            {
                var clientes = context.Clientes.ToList();  // ✅ trae de SQL

                if (clientes.Count == 0)
                {
                    Console.WriteLine("No hay clientes registrados.");
                }
                else
                {
                    foreach (Cliente c in clientes)
                    {
                        c.Imprimir();
                    }
                }
            }
            Console.ReadLine();
        }

        // --- BUSCAR ---
        public static void BuscarCliente()
        {
            Console.Clear();
            Console.WriteLine("********** Buscar Cliente **********");
            Console.Write("Ingrese el ID del cliente: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Cliente objCliente = context.Clientes.Find(idIngresado); // ✅ busca en SQL

                if (objCliente != null)
                {
                    Console.WriteLine("Cliente encontrado!!");
                    objCliente.Imprimir();
                }
                else
                {
                    Console.WriteLine("Cliente NO encontrado...");
                }
            }
            Console.ReadLine();
        }

        // --- ACTUALIZAR ---
        public static void ActualizarCliente()
        {
            Console.Clear();
            Console.WriteLine("********** Actualizar Cliente **********");
            Console.Write("Ingrese el ID del cliente a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Cliente objCliente = context.Clientes.Find(idIngresado);

                if (objCliente != null)
                {
                    objCliente.Imprimir();

                    Console.Write("Ingrese el nuevo nombre: ");
                    objCliente.Nombre = Console.ReadLine();
                    Console.Write("Ingrese la nueva cédula: ");
                    objCliente.Cedula = Console.ReadLine();
                    Console.Write("Ingrese el nuevo teléfono: ");
                    objCliente.Telefono = Console.ReadLine();
                    Console.Write("Ingrese el nuevo email: ");
                    objCliente.Email = Console.ReadLine();
                    Console.Write("Ingrese la nueva ciudad: ");
                    objCliente.Ciudad = Console.ReadLine();

                    context.SaveChanges();   // ✅ guarda cambios en SQL
                    Console.WriteLine("Cliente actualizado exitosamente!!");
                }
                else
                {
                    Console.WriteLine("Cliente NO encontrado...");
                }
            }
            Console.ReadLine();
        }

        // --- ELIMINAR ---
        public static void EliminarCliente()
        {
            Console.Clear();
            Console.WriteLine("********** Eliminar Cliente **********");
            Console.Write("Ingrese el ID del cliente a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Cliente objCliente = context.Clientes.Find(idIngresado);

                if (objCliente != null)
                {
                    objCliente.Imprimir();
                    Console.WriteLine($"¿Estás seguro que quieres eliminar al cliente {objCliente.Nombre}? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        context.Clientes.Remove(objCliente); // ✅ elimina en SQL
                        context.SaveChanges();
                        Console.WriteLine("Cliente eliminado exitosamente!!");
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada!!");
                    }
                }
                else
                {
                    Console.WriteLine("Cliente NO encontrado!!");
                }
            }
            Console.ReadLine();
        }
    }
}
