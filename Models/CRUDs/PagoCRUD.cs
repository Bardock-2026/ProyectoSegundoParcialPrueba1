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
        // --- CREAR ---
        public static void CrearPago()
        {
            Console.Clear();
            Console.WriteLine("********** Registrar Pago **********");

            using (var context = new HotelDbContext())
            {
                // ✅ Cambio: listar solo reservas que no tengan pagos registrados
                var reservas = context.Reservas
                                      .Include(r => r.Cliente)
                                      .Include(r => r.Habitacion)
                                      .Where(r => !context.Pagos.Any(p => p.Reserva.Id == r.Id)) // 🔹 filtro
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
                    Console.WriteLine($"ID: {r.Id}, Cliente: {r.Cliente.Nombre}, Habitación: {r.Habitacion.Id} - {r.Habitacion.Tipo}, Precio: {r.Habitacion.Precio}, Inicio: {r.FechaInicio.ToShortDateString()}, Fin: {r.FechaFin.ToShortDateString()}");
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

                // ✅ Mostrar precio de la habitación
                Console.WriteLine($"Precio de la habitación seleccionada: {reserva.Habitacion.Precio}");

                // ✅ Opción de pago total o abono
                Console.WriteLine("\nSeleccione tipo de pago:");
                Console.WriteLine("1. Pagar totalidad");
                Console.WriteLine("2. Abonar");

                Console.Write("Ingrese opción (1-2): ");
                int opcion = Convert.ToInt32(Console.ReadLine());

                decimal monto;
                string mensajePago;

                if (opcion == 1)
                {
                    monto = reserva.Habitacion.Precio;
                    mensajePago = $"Estimado {reserva.Cliente.Nombre}, usted ha pagado su reserva completamente ({monto:C}).";
                }
                else if (opcion == 2)
                {
                    Console.Write("Ingrese monto a abonar: ");
                    monto = Convert.ToDecimal(Console.ReadLine());

                    if (monto <= 0 || monto > reserva.Habitacion.Precio)
                        throw new Exception("El abono debe ser mayor a 0 y no puede superar el precio total.");

                    mensajePago = $"Estimado {reserva.Cliente.Nombre}, usted ha abonado {monto:C} para su reserva (Precio total {reserva.Habitacion.Precio:C}).";
                }
                else
                {
                    throw new Exception("Opción inválida. Debe elegir 1 o 2.");
                }

                try
                {
                    Pago pago = new Pago(reserva, monto); // ✅ constructor sin ID
                    context.Pagos.Add(pago);
                    context.SaveChanges();                // ✅ guarda en SQL

                    Console.WriteLine("Pago registrado exitosamente!!");

                    // 🚀 Notificación al cliente
                    string correoCliente = reserva.Cliente.Email;
                    string numeroCliente = reserva.Cliente.Telefono;

                    var emailService = new EmailService();
                    emailService.EnviarCorreo(correoCliente, "Confirmación de Pago", mensajePago);

                    var wsService = new WhatsAppService();
                    wsService.EnviarWhatsApp(numeroCliente, mensajePago);

                    // ✅ Persistencia en SQL (historial de envíos)
                    using (var db = new ChatContext())
                    {
                        db.CorreosEnviados.Add(new CorreoEnviado
                        {
                            Destinatario = correoCliente,
                            Asunto = "Confirmación de Pago",
                            Cuerpo = mensajePago,
                            FechaEnvio = DateTime.Now
                        });

                        db.WhatsAppsEnviados.Add(new WhatsAppEnviado
                        {
                            NumeroDestino = numeroCliente,
                            Mensaje = mensajePago,
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
                        Console.WriteLine($"Monto: {p.Monto:C}");
                        Console.WriteLine($"Precio Habitación: {p.Reserva.Habitacion.Precio:C}");
                        Console.WriteLine($"Fecha de Pago: {p.FechaPago}");

                        // ✅ Comparar monto vs precio
                        if (p.Monto == p.Reserva.Habitacion.Precio)
                        {
                            Console.WriteLine("--> Pago COMPLETO de la reserva.");
                        }
                        else if (p.Monto < p.Reserva.Habitacion.Precio)
                        {
                            decimal faltante = p.Reserva.Habitacion.Precio - p.Monto;
                            Console.WriteLine($"--> Pago ABONADO. Falta por pagar: {faltante:C}");
                        }
                        else
                        {
                            Console.WriteLine("--> Pago excedente (monto mayor al precio).");
                        }

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
                    Console.WriteLine($"ID: {p.Id}, Cliente: {p.Reserva.Cliente.Nombre}, Habitación: {p.Reserva.Habitacion.Id} - {p.Reserva.Habitacion.Tipo}, Monto: {p.Monto}, Precio Habitación: {p.Reserva.Habitacion.Precio}, Fecha Pago: {p.FechaPago}");
                }

                Console.Write("\nIngrese el ID del pago a actualizar: ");
                int idIngresado = Convert.ToInt32(Console.ReadLine());

                Pago objPago = pagos.FirstOrDefault(p => p.Id == idIngresado);

                if (objPago != null)
                {
                    objPago.Imprimir();

                    decimal precioHabitacion = objPago.Reserva.Habitacion.Precio;
                    decimal montoActual = objPago.Monto;
                    decimal faltante = precioHabitacion - montoActual;

                    Console.WriteLine($"\nPrecio total habitación: {precioHabitacion:C}");
                    Console.WriteLine($"Monto actual abonado: {montoActual:C}");
                    Console.WriteLine($"Faltante: {faltante:C}");

                    if (faltante <= 0)
                    {
                        Console.WriteLine("La reserva ya está pagada en su totalidad. Solo puede modificar el monto manualmente si es necesario.");
                        Console.Write("Ingrese nuevo monto (o presione Enter para no cambiar): ");
                        string input = Console.ReadLine();

                        if (!string.IsNullOrEmpty(input))
                        {
                            decimal nuevoMonto = Convert.ToDecimal(input);
                            if (nuevoMonto > precioHabitacion)
                                throw new Exception("El monto no puede superar el precio total de la habitación.");

                            objPago.Monto = nuevoMonto;
                            context.SaveChanges();
                            Console.WriteLine("Pago actualizado exitosamente!!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nSeleccione opción:");
                        Console.WriteLine("1. Completar pago automáticamente (sumar lo que falta)");
                        Console.WriteLine("2. Completar pago manualmente (ingresar cuánto abonar)");
                        Console.WriteLine("3. Modificar monto directamente");

                        Console.Write("Ingrese opción (1-3): ");
                        int opcion = Convert.ToInt32(Console.ReadLine());

                        if (opcion == 1)
                        {
                            objPago.Monto = precioHabitacion; // completar automáticamente
                        }
                        else if (opcion == 2)
                        {
                            Console.Write("Ingrese monto a abonar: ");
                            decimal abono = Convert.ToDecimal(Console.ReadLine());

                            if (montoActual + abono > precioHabitacion)
                                throw new Exception("El abono supera el precio total de la habitación.");

                            objPago.Monto += abono; // sumar al monto actual
                        }
                        else if (opcion == 3)
                        {
                            Console.Write("Ingrese nuevo monto: ");
                            decimal nuevoMonto = Convert.ToDecimal(Console.ReadLine());

                            if (nuevoMonto > precioHabitacion)
                                throw new Exception("El monto no puede superar el precio total de la habitación.");

                            objPago.Monto = nuevoMonto;
                        }
                        else
                        {
                            throw new Exception("Opción inválida. Debe elegir 1, 2 o 3.");
                        }

                        context.SaveChanges();
                        Console.WriteLine("Pago actualizado exitosamente!!");
                    }

                    // 🚀 Notificación al cliente
                    string correoCliente = objPago.Reserva.Cliente.Email;
                    string numeroCliente = objPago.Reserva.Cliente.Telefono;
                    string mensaje;

                    if (objPago.Monto >= precioHabitacion)
                    {
                        mensaje = $"Estimado {objPago.Reserva.Cliente.Nombre}, su pago ha sido completado exitosamente. Usted ha pagado su reserva completamente ({objPago.Monto:C}).";
                    }
                    else
                    {
                        decimal nuevoFaltante = precioHabitacion - objPago.Monto;
                        mensaje = $"Estimado {objPago.Reserva.Cliente.Nombre}, su pago ha sido actualizado. Usted ha abonado {objPago.Monto:C}, faltan {nuevoFaltante:C} para completar su reserva.";
                    }

                    var emailService = new EmailService();
                    emailService.EnviarCorreo(correoCliente, "Actualización de Pago", mensaje);

                    var wsService = new WhatsAppService();
                    wsService.EnviarWhatsApp(numeroCliente, mensaje);

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