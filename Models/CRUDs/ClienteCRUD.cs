using ProyectoSegundoParcialPrueba1.Models.Personas;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class ClienteCRUD
    {
        // Lista temporal para almacenar clientes (luego se reemplaza por SQL/EF Core)
        private static List<Cliente> clientes = new List<Cliente>();

        // --- CREAR ---
        public static void CrearCliente()
        {
            Console.Clear();
            Console.WriteLine("********** Crear Cliente **********");

            Console.Write("Ingrese ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
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
                Cliente objCliente = new Cliente(id, nombre, cedula, telefono, email, ciudad);
                clientes.Add(objCliente);
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
            Console.ReadLine();
        }

        // --- BUSCAR ---
        public static void BuscarCliente()
        {
            Console.Clear();
            Console.WriteLine("********** Buscar Cliente **********");
            Console.Write("Ingrese el ID del cliente: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Cliente objCliente = clientes.Find(c => c.Id == idIngresado);

            if (objCliente != null)
            {
                Console.WriteLine("Cliente encontrado!!");
                objCliente.Imprimir();
            }
            else
            {
                Console.WriteLine("Cliente NO encontrado...");
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

            Cliente objCliente = clientes.Find(c => c.Id == idIngresado);

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

                Console.WriteLine("Cliente actualizado exitosamente!!");
            }
            else
            {
                Console.WriteLine("Cliente NO encontrado...");
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

            Cliente objCliente = clientes.Find(c => c.Id == idIngresado);

            if (objCliente != null)
            {
                objCliente.Imprimir();
                Console.WriteLine($"¿Estás seguro que quieres eliminar al cliente {objCliente.Nombre}? S/N:");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    clientes.Remove(objCliente);
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
            Console.ReadLine();
        }

        // --- METODO AUXILIAR PARA OBTENER CLIENTE POR ID ---
        public static Cliente ObtenerClientePorId(int id)
        {
            return clientes.Find(c => c.Id == id);
        }
    
    }

}
