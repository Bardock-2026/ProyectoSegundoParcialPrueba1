using Microsoft.EntityFrameworkCore;
using ProyectoSegundoParcialPrueba1.Datos;
using ProyectoSegundoParcialPrueba1.Models.Correo;
using ProyectoSegundoParcialPrueba1.Models.Espacios;
using ProyectoSegundoParcialPrueba1.Models.IA;
using ProyectoSegundoParcialPrueba1.Models.Personas;
using ProyectoSegundoParcialPrueba1.Models.Transacciones;
using ProyectoSegundoParcialPrueba1.Models.Wasap;
using ProyectoSegundoParcialPrueba1.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.CRUDs
{
    public static class ReservaCRUD
    {
        // --- CREAR ---
        public static void CrearReserva()
        {
            Console.Clear();
            Console.WriteLine("********** Crear Reserva **********");

            try
            {
                using (var context = new HotelDbContext())
                {
                    // ✅ Cambio: listar clientes mostrando si ya tienen reservas
                    Console.WriteLine("=== CLIENTES DISPONIBLES ===");
                    foreach (var cli in context.Clientes.Include(c => c.Reservas).ToList())
                    {
                        string infoReserva = cli.Reservas.Count > 0
                            ? $"(Ya tiene {cli.Reservas.Count} reserva(s))"
                            : "(Sin reservas)";
                        Console.WriteLine($"ID: {cli.Id}, Nombre: {cli.Nombre}, Email: {cli.Email} {infoReserva}");
                    }

                    // ✅ Cambio: listar habitaciones disponibles
                    Console.WriteLine("\n=== HABITACIONES DISPONIBLES ===");
                    foreach (var hab in context.Habitaciones.Where(h => h.Estado == "Disponible").ToList())
                    {
                        Console.WriteLine($"ID: {hab.Id}, Tipo: {hab.Tipo}, Precio: {hab.Precio}");
                    }

                    // ✅ Pedir selección
                    Console.Write("\nIngrese ID del cliente: ");
                    int idCliente = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Ingrese ID de la habitación: ");
                    int idHabitacion = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Ingrese fecha inicio (yyyy-mm-dd): ");
                    DateTime fechaInicio = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Ingrese fecha fin (yyyy-mm-dd): ");
                    DateTime fechaFin = Convert.ToDateTime(Console.ReadLine());

                    // ✅ Validaciones con throw new Exception
                    if (fechaInicio < DateTime.Today)
                        throw new Exception("La fecha de inicio no puede ser anterior a la fecha actual.");

                    if (fechaFin <= fechaInicio)
                        throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio.");

                    // ✅ Buscar cliente y habitación
                    Cliente cliente = context.Clientes.Include(c => c.Reservas).FirstOrDefault(c => c.Id == idCliente);
                    Habitacion habitacion = context.Habitaciones.Find(idHabitacion);

                    if (cliente == null)
                        throw new Exception("Cliente no encontrado.");

                    if (habitacion == null || habitacion.Estado == "Ocupada")
                        throw new Exception("La habitación no está disponible.");

                    // ✅ Aviso si el cliente ya tiene reservas
                    if (cliente.Reservas.Count > 0)
                    {
                        Console.WriteLine($"⚠ Atención: El cliente {cliente.Nombre} ya tiene {cliente.Reservas.Count} reserva(s). Se añadirá una nueva.");
                    }

                    // ✅ Crear reserva y guardar en SQL
                    Reserva objReserva = new Reserva(0, cliente, habitacion, fechaInicio, fechaFin);
                    context.Reservas.Add(objReserva);

                    habitacion.Estado = "Ocupada"; // actualizar estado en SQL

                    context.SaveChanges(); // ✅ persistencia en SQL
                    Console.WriteLine("Reserva creada exitosamente!!");

                    // 🚀 Notificación al cliente
                    string correoCliente = cliente.Email;
                    string numeroCliente = cliente.Telefono;
                    string fechaReserva = objReserva.FechaInicio.ToShortDateString();

                    string mensaje = $"Estimado {cliente.Nombre}, su reserva está lista para el día {fechaReserva}.";

                    var emailService = new EmailService();
                    emailService.EnviarCorreo(correoCliente, "Confirmación de Reserva", mensaje);

                    var wsService = new WhatsAppService();
                    wsService.EnviarWhatsApp(numeroCliente, mensaje);
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

                        // ✅ Cambio: contar cuántas reservas tiene el cliente
                        int totalReservasCliente = context.Reservas
                                                          .Count(res => res.Cliente.Id == r.Cliente.Id);

                        // ✅ Mostrar mensaje según cantidad
                        if (totalReservasCliente > 0)
                        {
                            Console.WriteLine($"--> El cliente {r.Cliente.Nombre} tiene {totalReservasCliente} reserva(s) en total.");
                        }
                        else
                        {
                            Console.WriteLine($"--> El cliente {r.Cliente.Nombre} no tiene reservas.");
                        }

                        Console.WriteLine("------------------------------------------");
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

            using (var context = new HotelDbContext())
            {
                // ✅ Listar todas las reservas con ID de cliente y habitación
                var reservas = context.Reservas
                                      .Include(r => r.Cliente)
                                      .Include(r => r.Habitacion)
                                      .ToList();

                if (reservas.Count == 0)
                {
                    Console.WriteLine("No hay reservas registradas.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("=== RESERVAS DISPONIBLES ===");
                foreach (var r in reservas)
                {
                    Console.WriteLine($"ID Reserva: {r.Id}, Cliente: {r.Cliente.Id} - {r.Cliente.Nombre}, Habitación: {r.Habitacion.Id} - {r.Habitacion.Tipo}, Inicio: {r.FechaInicio.ToShortDateString()}, Fin: {r.FechaFin.ToShortDateString()}");
                }

                // ✅ Pedir ID de la reserva a actualizar
                Console.Write("\nIngrese el ID de la reserva a actualizar: ");
                int idIngresado = Convert.ToInt32(Console.ReadLine());

                Reserva objReserva = reservas.FirstOrDefault(r => r.Id == idIngresado);

                if (objReserva != null)
                {
                    objReserva.Imprimir();

                    // ✅ Actualizar fechas
                    Console.Write("Ingrese nueva fecha inicio (yyyy-mm-dd): ");
                    DateTime nuevaFechaInicio = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Ingrese nueva fecha fin (yyyy-mm-dd): ");
                    DateTime nuevaFechaFin = Convert.ToDateTime(Console.ReadLine());

                    if (nuevaFechaInicio < DateTime.Today)
                        throw new Exception("La fecha de inicio no puede ser anterior a la fecha actual.");

                    if (nuevaFechaFin <= nuevaFechaInicio)
                        throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio.");

                    objReserva.FechaInicio = nuevaFechaInicio;
                    objReserva.FechaFin = nuevaFechaFin;

                    // ✅ Mostrar clientes disponibles
                    Console.WriteLine("\n=== CLIENTES DISPONIBLES ===");
                    foreach (var cli in context.Clientes.ToList())
                    {
                        Console.WriteLine($"ID Cliente: {cli.Id}, Nombre: {cli.Nombre}, Email: {cli.Email}");
                    }

                    Console.Write("Ingrese el ID del nuevo cliente: ");
                    int idCliente = Convert.ToInt32(Console.ReadLine());
                    Cliente nuevoCliente = context.Clientes.Find(idCliente);
                    if (nuevoCliente != null)
                    {
                        objReserva.Cliente = nuevoCliente;
                    }

                    // ✅ Mostrar habitaciones disponibles
                    Console.WriteLine("\n=== HABITACIONES DISPONIBLES ===");
                    foreach (var hab in context.Habitaciones.Where(h => h.Estado == "Disponible").ToList())
                    {
                        Console.WriteLine($"ID Habitación: {hab.Id}, Tipo: {hab.Tipo}, Precio: {hab.Precio}");
                    }

                    Console.Write("Ingrese el ID de la nueva habitación: ");
                    int idHabitacion = Convert.ToInt32(Console.ReadLine());
                    Habitacion nuevaHabitacion = context.Habitaciones.Find(idHabitacion);

                    if (nuevaHabitacion != null && nuevaHabitacion.Estado == "Disponible")
                    {
                        // ✅ Liberar la habitación anterior
                        objReserva.Habitacion.Estado = "Disponible";

                        // ✅ Asignar la nueva habitación
                        objReserva.Habitacion = nuevaHabitacion;
                        nuevaHabitacion.Estado = "Ocupada";
                    }
                    else
                    {
                        throw new Exception("La habitación seleccionada no está disponible.");
                    }

                    context.SaveChanges(); // ✅ guarda cambios en SQL
                    Console.WriteLine("Reserva actualizada exitosamente!!");

                    // 🚀 Notificación al cliente
                    string correoCliente = objReserva.Cliente.Email;
                    string numeroCliente = objReserva.Cliente.Telefono;
                    string mensaje = $"Estimado {objReserva.Cliente.Nombre}, su reserva ha sido actualizada correctamente. Nueva fecha: {objReserva.FechaInicio.ToShortDateString()} - {objReserva.FechaFin.ToShortDateString()}, Habitación: {objReserva.Habitacion.Tipo}.";

                    var emailService = new EmailService();
                    emailService.EnviarCorreo(correoCliente, "Actualización de Reserva", mensaje);

                    var wsService = new WhatsAppService();
                    wsService.EnviarWhatsApp(numeroCliente, mensaje);

                    // ✅ Persistencia en SQL (historial de envíos)
                    using (var db = new ChatContext())
                    {
                        db.CorreosEnviados.Add(new CorreoEnviado
                        {
                            Destinatario = correoCliente,
                            Asunto = "Actualización de Reserva",
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

            using (var context = new HotelDbContext())
            {
                // 🔹 Listar todas las reservas disponibles desde SQL
                var reservas = context.Reservas
                                      .Include(r => r.Cliente)
                                      .Include(r => r.Habitacion)
                                      .ToList();

                if (reservas.Count == 0)
                {
                    Console.WriteLine("No hay reservas registradas.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("=== RESERVAS DISPONIBLES ===");
                foreach (var r in reservas)
                {
                    Console.WriteLine($"ID: {r.Id}, Cliente: {r.Cliente.Nombre}, Habitación: {r.Habitacion.Id} - {r.Habitacion.Tipo}, Inicio: {r.FechaInicio.ToShortDateString()}, Fin: {r.FechaFin.ToShortDateString()}");
                }

                // 🔹 Pedir ID de la reserva a eliminar
                Console.Write("\nIngrese el ID de la reserva a eliminar: ");
                int idIngresado = Convert.ToInt32(Console.ReadLine());

                Reserva objReserva = reservas.FirstOrDefault(r => r.Id == idIngresado);

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

                        // 🚀 Notificación
                        string correoCliente = objReserva.Cliente.Email;
                        string numeroCliente = objReserva.Cliente.Telefono;
                        string mensaje = $"Estimado {objReserva.Cliente.Nombre}, lamentamos informarle que su reserva ha sido cancelada.";

                        var emailService = new EmailService();
                        emailService.EnviarCorreo(correoCliente, "Cancelación de Reserva", mensaje);

                        var wsService = new WhatsAppService();
                        wsService.EnviarWhatsApp(numeroCliente, mensaje);

                        // ✅ Persistencia en SQL
                        using (var db = new ChatContext())
                        {
                            db.CorreosEnviados.Add(new CorreoEnviado
                            {
                                Destinatario = correoCliente,
                                Asunto = "Cancelación de Reserva",
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
                    Console.WriteLine("Reserva NO encontrada!!");
                }
            }
            Console.ReadLine();
        }
    }
}
