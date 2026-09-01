using Microsoft.EntityFrameworkCore;
using ProyectoSegundoParcialPrueba1.Datos;
using ProyectoSegundoParcialPrueba1.Models.Correo;
using ProyectoSegundoParcialPrueba1.Models.IA;
using ProyectoSegundoParcialPrueba1.Models.Transacciones;
using ProyectoSegundoParcialPrueba1.Models.Wasap;
using ProyectoSegundoParcialPrueba1.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class PagoCRUD
    {
        // --- CREAR ---
        public static void CrearPago()
        {
            Console.Clear();
            Console.WriteLine("********** Registrar Pago **********");

            using (var context = new HotelDbContext())
            {
                // 🔹 Listar reservas disponibles
                var reservas = context.Reservas
                                      .Include(r => r.Cliente)
                                      .Include(r => r.Habitacion)
                                      .ToList();

                if (reservas.Count == 0)
                {
                    Console.WriteLine("No hay reservas disponibles para pago.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("=== RESERVAS DISPONIBLES ===");
                foreach (var r in reservas)
                {
                    Console.WriteLine($"ID: {r.Id}, Cliente: {r.Cliente.Nombre}, Habitación: {r.Habitacion.Id} - {r.Habitacion.Tipo}, Inicio: {r.FechaInicio.ToShortDateString()}, Fin: {r.FechaFin.ToShortDateString()}");
                }

                // 🔹 Pedir ID de la reserva
                Console.Write("\nIngrese ID de la reserva: ");
                int idReserva = Convert.ToInt32(Console.ReadLine());

                Reserva reserva = context.Reservas
                                         .Include(r => r.Cliente)
                                         .Include(r => r.Habitacion)
                                         .FirstOrDefault(r => r.Id == idReserva);

                if (reserva == null)
                    throw new Exception("Reserva no encontrada.");

                Console.Write("Ingrese monto del pago: ");
                decimal monto = Convert.ToDecimal(Console.ReadLine());

                try
                {
                    Pago pago = new Pago(reserva, monto); // ✅ constructor sin ID
                    context.Pagos.Add(pago);
                    context.SaveChanges();                // ✅ guarda en SQL

                    Console.WriteLine("Pago registrado exitosamente!!");

                    // 🚀 Notificación al cliente
                    string correoCliente = reserva.Cliente.Email;
                    string numeroCliente = reserva.Cliente.Telefono;
                    string mensaje = $"Estimado {reserva.Cliente.Nombre}, su pago de {monto:C} se realizó correctamente y su reserva (Habitación {reserva.Habitacion.Id} - {reserva.Habitacion.Tipo}) está lista para el día {reserva.FechaInicio.ToShortDateString()}.";

                    var emailService = new EmailService();
                    emailService.EnviarCorreo(correoCliente, "Confirmación de Pago", mensaje);

                    var wsService = new WhatsAppService();
                    wsService.EnviarWhatsApp(numeroCliente, mensaje);

                    // ✅ Persistencia en SQL (historial de envíos)
                    using (var db = new ChatContext())
                    {
                        db.CorreosEnviados.Add(new CorreoEnviado
                        {
                            Destinatario = correoCliente,
                            Asunto = "Confirmación de Pago",
                            Cuerpo = mensaje,
                            FechaEnvio = DateTime.Now
                        });

                        db.WhatsAppsEnviados.Add(new WhatsAppEnviado
                        {
                            NumeroDestino = numeroCliente,
                            Mensaje = mensaje,
                            FechaEnvio = DateTime.Now
                        });

                        db.SaveChanges();
                    }
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
                var pagos = context.Pagos
                                   .Include(p => p.Reserva)
                                   .ThenInclude(r => r.Cliente)
                                   .Include(p => p.Reserva.Habitacion)
                                   .ToList();

                if (pagos.Count == 0)
                {
                    Console.WriteLine("No hay pagos registrados.");
                }
                else
                {
                    foreach (Pago p in pagos)
                    {
                        Console.WriteLine("********** Pago **********");
                        Console.WriteLine($"ID: {p.Id}");
                        Console.WriteLine($"Reserva: {p.Reserva.Id}");
                        Console.WriteLine($"Cliente: {p.Reserva.Cliente.Nombre}");
                        Console.WriteLine($"Habitación: {p.Reserva.Habitacion.Id} - {p.Reserva.Habitacion.Tipo}");
                        Console.WriteLine($"Monto: {p.Monto}");
                        Console.WriteLine($"Fecha de Pago: {p.FechaPago}");
                        Console.WriteLine("--------------------------------------");
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

            using (var context = new HotelDbContext())
            {
                // 🔹 Listar todos los pagos disponibles desde SQL
                var pagos = context.Pagos
                                   .Include(p => p.Reserva)
                                   .ThenInclude(r => r.Cliente)
                                   .Include(p => p.Reserva.Habitacion)
                                   .ToList();

                if (pagos.Count == 0)
                {
                    Console.WriteLine("No hay pagos registrados.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("=== PAGOS DISPONIBLES ===");
                foreach (var p in pagos)
                {
                    Console.WriteLine($"ID: {p.Id}, Cliente: {p.Reserva.Cliente.Nombre}, Habitación: {p.Reserva.Habitacion.Id} - {p.Reserva.Habitacion.Tipo}, Monto: {p.Monto}, Fecha Pago: {p.FechaPago}");
                }

                // 🔹 Pedir ID del pago a actualizar
                Console.Write("\nIngrese el ID del pago a actualizar: ");
                int idIngresado = Convert.ToInt32(Console.ReadLine());

                Pago objPago = pagos.FirstOrDefault(p => p.Id == idIngresado);

                if (objPago != null)
                {
                    objPago.Imprimir();

                    Console.Write("Ingrese nuevo monto: ");
                    objPago.Monto = Convert.ToDecimal(Console.ReadLine());

                    context.SaveChanges();   // ✅ guarda cambios en SQL
                    Console.WriteLine("Pago actualizado exitosamente!!");

                    // 🚀 Notificación al cliente
                    string correoCliente = objPago.Reserva.Cliente.Email;
                    string numeroCliente = objPago.Reserva.Cliente.Telefono;
                    string mensaje = $"Estimado {objPago.Reserva.Cliente.Nombre}, su pago ha sido actualizado correctamente. El nuevo monto es {objPago.Monto:C} y su reserva (Habitación {objPago.Reserva.Habitacion.Id} - {objPago.Reserva.Habitacion.Tipo}) sigue confirmada para el día {objPago.Reserva.FechaInicio.ToShortDateString()}.";

                    var emailService = new EmailService();
                    emailService.EnviarCorreo(correoCliente, "Actualización de Pago", mensaje);

                    var wsService = new WhatsAppService();
                    wsService.EnviarWhatsApp(numeroCliente, mensaje);

                    // ✅ Persistencia en SQL (historial de envíos)
                    using (var db = new ChatContext())
                    {
                        db.CorreosEnviados.Add(new CorreoEnviado
                        {
                            Destinatario = correoCliente,
                            Asunto = "Actualización de Pago",
                            Cuerpo = mensaje,
                            FechaEnvio = DateTime.Now
                        });

                        db.WhatsAppsEnviados.Add(new WhatsAppEnviado
                        {
                            NumeroDestino = numeroCliente,
                            Mensaje = mensaje,
                            FechaEnvio = DateTime.Now
                        });

                        db.SaveChanges();
                    }
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

            using (var context = new HotelDbContext())
            {
                // 🔹 Listar todos los pagos disponibles desde SQL
                var pagos = context.Pagos
                                   .Include(p => p.Reserva)
                                   .ThenInclude(r => r.Cliente)
                                   .Include(p => p.Reserva.Habitacion)
                                   .ToList();

                if (pagos.Count == 0)
                {
                    Console.WriteLine("No hay pagos registrados.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("=== PAGOS DISPONIBLES ===");
                foreach (var p in pagos)
                {
                    Console.WriteLine($"ID: {p.Id}, Cliente: {p.Reserva.Cliente.Nombre}, Habitación: {p.Reserva.Habitacion.Id} - {p.Reserva.Habitacion.Tipo}, Monto: {p.Monto}, Fecha Pago: {p.FechaPago}");
                }

                // 🔹 Pedir ID del pago a eliminar
                Console.Write("\nIngrese el ID del pago a eliminar: ");
                int idIngresado = Convert.ToInt32(Console.ReadLine());

                Pago objPago = pagos.FirstOrDefault(p => p.Id == idIngresado);

                if (objPago != null)
                {
                    objPago.Imprimir();
                    Console.WriteLine($"¿Estás seguro que quieres eliminar el pago ID {objPago.Id}? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        context.Pagos.Remove(objPago); // ✅ elimina en SQL
                        context.SaveChanges();
                        Console.WriteLine("Pago eliminado exitosamente!!");

                        // 🚀 Notificación al cliente
                        string correoCliente = objPago.Reserva.Cliente.Email;
                        string numeroCliente = objPago.Reserva.Cliente.Telefono;
                        string mensaje = $"Estimado {objPago.Reserva.Cliente.Nombre}, su pago ha sido eliminado del sistema. Si tiene dudas, por favor contacte al hotel.";

                        var emailService = new EmailService();
                        emailService.EnviarCorreo(correoCliente, "Eliminación de Pago", mensaje);

                        var wsService = new WhatsAppService();
                        wsService.EnviarWhatsApp(numeroCliente, mensaje);

                        // ✅ Persistencia en SQL (historial de envíos)
                        using (var db = new ChatContext())
                        {
                            db.CorreosEnviados.Add(new CorreoEnviado
                            {
                                Destinatario = correoCliente,
                                Asunto = "Eliminación de Pago",
                                Cuerpo = mensaje,
                                FechaEnvio = DateTime.Now
                            });

                            db.WhatsAppsEnviados.Add(new WhatsAppEnviado
                            {
                                NumeroDestino = numeroCliente,
                                Mensaje = mensaje,
                                FechaEnvio = DateTime.Now
                            });

                            db.SaveChanges();
                        }
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