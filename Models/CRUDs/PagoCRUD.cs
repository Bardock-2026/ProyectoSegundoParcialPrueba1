using ProyectoSegundoParcialPrueba1.Datos;
using ProyectoSegundoParcialPrueba1.Models.Transacciones;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class PagoCRUD
    {
        // --- CREAR ---
        public static void CrearPago()
        {
            Console.Clear();
            Console.WriteLine("********** Registrar Pago **********");

            Console.Write("Ingrese ID de la reserva: ");
            int idReserva = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Reserva reserva = context.Reservas.Find(idReserva); // ✅ busca en SQL

                if (reserva == null)
                {
                    Console.WriteLine("Reserva no encontrada.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Ingrese monto del pago: ");
                decimal monto = Convert.ToDecimal(Console.ReadLine());

                try
                {
                    Pago pago = new Pago(reserva, monto); // ✅ constructor sin ID
                    context.Pagos.Add(pago);
                    context.SaveChanges();                // ✅ guarda en SQL

                    Console.WriteLine("Pago registrado exitosamente!!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.ReadLine();
        }

        // --- LISTAR ---
        public static void ListarPagos()
        {
            Console.Clear();
            Console.WriteLine("********** Pagos Registrados **********");

            using (var context = new HotelDbContext())
            {
                var pagos = context.Pagos.Include(p => p.Reserva).ToList(); // ✅ trae de SQL

                if (pagos.Count == 0)
                {
                    Console.WriteLine("No hay pagos registrados.");
                }
                else
                {
                    foreach (Pago p in pagos)
                    {
                        p.Imprimir();
                    }
                }
            }
            Console.ReadLine();
        }

        // --- BUSCAR ---
        public static void BuscarPago()
        {
            Console.Clear();
            Console.WriteLine("********** Buscar Pago **********");
            Console.Write("Ingrese el ID del pago: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Pago objPago = context.Pagos
                                      .Include(p => p.Reserva)
                                      .FirstOrDefault(p => p.Id == idIngresado);

                if (objPago != null)
                {
                    Console.WriteLine("Pago encontrado!!");
                    objPago.Imprimir();
                }
                else
                {
                    Console.WriteLine("Pago NO encontrado...");
                }
            }
            Console.ReadLine();
        }

        // --- ACTUALIZAR ---
        public static void ActualizarPago()
        {
            Console.Clear();
            Console.WriteLine("********** Actualizar Pago **********");
            Console.Write("Ingrese el ID del pago a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Pago objPago = context.Pagos.Find(idIngresado);

                if (objPago != null)
                {
                    objPago.Imprimir();

                    Console.Write("Ingrese nuevo monto: ");
                    objPago.Monto = Convert.ToDecimal(Console.ReadLine());

                    context.SaveChanges();   // ✅ guarda cambios en SQL
                    Console.WriteLine("Pago actualizado exitosamente!!");
                }
                else
                {
                    Console.WriteLine("Pago NO encontrado...");
                }
            }
            Console.ReadLine();
        }

        // --- ELIMINAR ---
        public static void EliminarPago()
        {
            Console.Clear();
            Console.WriteLine("********** Eliminar Pago **********");
            Console.Write("Ingrese el ID del pago a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new HotelDbContext())
            {
                Pago objPago = context.Pagos.Find(idIngresado);

                if (objPago != null)
                {
                    objPago.Imprimir();
                    Console.WriteLine($"¿Estás seguro que quieres eliminar el pago ID {objPago.Id}? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        context.Pagos.Remove(objPago); // ✅ elimina en SQL
                        context.SaveChanges();
                        Console.WriteLine("Pago eliminado exitosamente!!");
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada!!");
                    }
                }
                else
                {
                    Console.WriteLine("Pago NO encontrado!!");
                }
            }
            Console.ReadLine();
        }
    }
}