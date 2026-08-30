using ProyectoSegundoParcialPrueba1.Models.CRUDs;
using ProyectoSegundoParcialPrueba1.Models.IA;
using ProyectoSegundoParcialPrueba1.Services;

class Program
{
    static async Task Main(string[] args)
    {
        int opcion = 0;
        var asistente = new OpenAIService("gpt-4.1-mini");

        // --- Mensaje inicial serio ---
        Console.ForegroundColor = ConsoleColor.Magenta;
        string intro = "Bienvenidos, Gestione su hotel con precisión y excelencia.";
        foreach (char c in intro)
        {
            Console.Write(c);
            Thread.Sleep(40);
        }
        Console.WriteLine("\n");
        Console.ResetColor();
        Thread.Sleep(500);
        do
        {
            Console.Clear();
            int ancho = Console.WindowWidth;

            // --- Título ASCII ---
            string[] titulo3D = {
                "██╗    ██╗███████╗██╗     ██████╗  ██████╗ ███╗   ███╗███████╗",
                "██║    ██║██╔════╝██║     ██╔═══╝ ██╔═══██╗████╗ ████║██╔════╝",
                "██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  ",
                "██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  ",
                "╚███╔███╔╝███████╗███████╗██████╗ ╚██████╔╝██║ ╚═╝ ██║███████╗",
                " ╚══╝╚══╝ ╚══════╝╚══════╝╚═════╝  ╚═════╝ ╚═╝     ╚═╝╚══════╝"
            };

            ConsoleColor[] colores = {
                ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
                ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Blue
            };

            int i = 0;
            foreach (string linea in titulo3D)
            {
                Console.ForegroundColor = colores[i % colores.Length];
                Console.WriteLine(linea.PadLeft((ancho + linea.Length) / 2));
                i++;
            }
            Console.ResetColor();

            // --- Caja con nombre del sistema ---
            string nombreSistema = "Optima Hotel System";
            Console.ForegroundColor = ConsoleColor.Cyan;
            string bordeTop = "╔══════════════════════════════════════╗";
            string bordeBottom = "╚══════════════════════════════════════╝";
            Console.WriteLine(bordeTop.PadLeft((ancho + bordeTop.Length) / 2));

            int espacioInterno = 32;
            int paddingIzq = (espacioInterno - nombreSistema.Length) / 2;
            string lineaNombre = "║ " + nombreSistema.PadLeft(nombreSistema.Length + paddingIzq).PadRight(espacioInterno) + " ║";
            Console.WriteLine(lineaNombre.PadLeft((ancho + lineaNombre.Length) / 2));

            Console.WriteLine(bordeBottom.PadLeft((ancho + bordeBottom.Length) / 2));
            Console.ResetColor();

            // --- Opciones ---
            string[] opciones = {
                "1. Crear Cliente","2. Listar Clientes","3. Buscar Cliente","4. Actualizar Cliente","5. Eliminar Cliente",
                "6. Crear Habitación","7. Listar Habitaciones","8. Buscar Habitación","9. Actualizar Habitación","10. Eliminar Habitación",
                "11. Crear Reserva","12. Listar Reservas","13. Buscar Reserva","14. Actualizar Reserva","15. Eliminar Reserva",
                "16. Registrar Pago","17. Listar Pagos","18. Buscar Pago","19. Actualizar Pago","20. Eliminar Pago",
                "21. Asistente IA","0. Salir"
            };
            // Primera fila: Cliente + Habitación
            Console.ForegroundColor = ConsoleColor.Cyan;
            string clienteTitulo = "║          CLIENTE             ║";
            Console.ForegroundColor = ConsoleColor.Green;
            string habitacionTitulo = "║         HABITACIÓN           ║";

            Console.WriteLine(("╔══════════════════════════════╗   ╔══════════════════════════════╗").PadLeft((ancho + 70) / 2));
            Console.WriteLine((clienteTitulo + "   " + habitacionTitulo).PadLeft((ancho + 70) / 2));

            for (int j = 0; j < 5; j++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                string lineaCliente = $"║ {opciones[j].PadRight(28)} ║";
                Console.ForegroundColor = ConsoleColor.Green;
                string lineaHabitacion = $"║ {opciones[j + 5].PadRight(28)} ║";
                Console.WriteLine((lineaCliente + "   " + lineaHabitacion).PadLeft((ancho + 70) / 2));
            }
            Console.WriteLine(("╚══════════════════════════════╝   ╚══════════════════════════════╝").PadLeft((ancho + 70) / 2));
            Console.ResetColor();

            // Segunda fila: Reserva + Pago
            Console.ForegroundColor = ConsoleColor.Magenta;
            string reservaTitulo = "║          RESERVA             ║";
            Console.ForegroundColor = ConsoleColor.Yellow;
            string pagoTitulo = "║            PAGO              ║";

            Console.WriteLine(("╔══════════════════════════════╗   ╔══════════════════════════════╗").PadLeft((ancho + 70) / 2));
            Console.WriteLine((reservaTitulo + "   " + pagoTitulo).PadLeft((ancho + 70) / 2));

            for (int j = 10; j < 15; j++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                string lineaReserva = $"║ {opciones[j].PadRight(28)} ║";
                Console.ForegroundColor = ConsoleColor.Yellow;
                string lineaPago = $"║ {opciones[j + 5].PadRight(28)} ║";
                Console.WriteLine((lineaReserva + "   " + lineaPago).PadLeft((ancho + 70) / 2));
            }
            Console.WriteLine(("╚══════════════════════════════╝   ╚══════════════════════════════╝").PadLeft((ancho + 70) / 2));
            Console.ResetColor();

            // Tercera fila: Asistente IA
            Console.ForegroundColor = ConsoleColor.Blue;
            string iaTitulo = "║        ASISTENTE IA          ║";

            Console.WriteLine(("╔══════════════════════════════╗").PadLeft((ancho + 35) / 2));
            Console.WriteLine(iaTitulo.PadLeft((ancho + 35) / 2));

            // ✅ Aquí mostramos explícitamente "21. Sugerencias IA"
            string lineaIA = $"║ 21. Sugerencias IA           ║";
            Console.WriteLine(lineaIA.PadLeft((ancho + 35) / 2));

            Console.WriteLine(("╚══════════════════════════════╝").PadLeft((ancho + 35) / 2));
            Console.ResetColor();

            // Opción Salir centrada
            Console.ForegroundColor = ConsoleColor.Cyan;
            string salir = "0. Salir";
            Console.WriteLine(salir.PadLeft((ancho + salir.Length) / 2));
            Console.ResetColor();

            // Mensaje debajo centrado
            Console.ForegroundColor = ConsoleColor.Green;
            string mensaje = ">>> Seleccione una opción:";
            Console.WriteLine("\n" + mensaje.PadLeft((ancho + mensaje.Length) / 2));
            Console.ResetColor();

            try
            {
                opcion = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                opcion = -1;
            }
            switch (opcion)
            {
                case 1: ClienteCRUD.CrearCliente(); break;
                case 2: ClienteCRUD.ListarClientes(); break;
                case 3: ClienteCRUD.BuscarCliente(); break;
                case 4: ClienteCRUD.ActualizarCliente(); break;
                case 5: ClienteCRUD.EliminarCliente(); break;
                case 6: HabitacionCRUD.CrearHabitacion(); break;
                case 7: HabitacionCRUD.ListarHabitaciones(); break;
                case 8: HabitacionCRUD.BuscarHabitacion(); break;
                case 9: HabitacionCRUD.ActualizarHabitacion(); break;
                case 10: HabitacionCRUD.EliminarHabitacion(); break;
                case 11: ReservaCRUD.CrearReserva(); break;
                case 12: ReservaCRUD.ListarReservas(); break;
                case 13: ReservaCRUD.BuscarReserva(); break;
                case 14: ReservaCRUD.ActualizarReserva(); break;
                case 15: ReservaCRUD.EliminarReserva(); break;
                case 16: PagoCRUD.CrearPago(); break;
                case 17: PagoCRUD.ListarPagos(); break;
                case 18: PagoCRUD.BuscarPago(); break;
                case 19: PagoCRUD.ActualizarPago(); break;
                case 20: PagoCRUD.EliminarPago(); break;

                case 21: // ✅ Asistente IA
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("╔══════════════════════════════╗");
                    Console.WriteLine("║         ASISTENTE IA         ║");
                    Console.WriteLine("╚══════════════════════════════╝");
                    Console.ResetColor();

                    // Validación del nombre
                    string cliente;
                    do
                    {
                        Console.Write("Ingrese su nombre: ");
                        cliente = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(cliente))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: indique un nombre válido por favor.");
                            Console.ResetColor();
                        }
                    } while (string.IsNullOrWhiteSpace(cliente));

                    string continuar = "s";
                    while (continuar == "s")
                    {
                        // Pregunta libre
                        string textoPregunta;
                        do
                        {
                            Console.Write("\nEscriba su pregunta por favor: ");
                            textoPregunta = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(textoPregunta))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error: indique una pregunta válida por favor.");
                                Console.ResetColor();
                            }
                        } while (string.IsNullOrWhiteSpace(textoPregunta));

                        Console.WriteLine($"\n>>> Pregunta ingresada: {textoPregunta}");

                        // Validación estricta de categoría
                        string categoria;
                        do
                        {
                            Console.Write("Categoría (Habitacion/Pago/Consejo): ");
                            categoria = Console.ReadLine()?.Trim().ToLower();

                            if (categoria != "habitacion" && categoria != "pago" && categoria != "consejo")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error: la categoría debe ser Habitacion, Pago o Consejo.");
                                Console.ResetColor();
                                categoria = null; // fuerza repetir
                            }
                        } while (string.IsNullOrWhiteSpace(categoria));

                        try
                        {
                            var pregunta = new PreguntaIA(cliente, textoPregunta, categoria);
                            var respuesta = await asistente.PreguntarAsync(pregunta);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n===== RESPUESTA IA =====");
                            Console.WriteLine(respuesta.Texto);
                            Console.WriteLine("=========================");
                            Console.ResetColor();

                            // 🚨 Validaciones extra: IA no gestiona directamente reservas, habitaciones ni pagos
                            string textoLower = textoPregunta.ToLower();
                            if (textoLower.Contains("reserva"))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\n⚠ El asistente IA no gestiona reservas directamente.");
                                Console.WriteLine("   Para crear o modificar una reserva, use las opciones del menú principal (11–15).");
                                Console.ResetColor();
                            }
                            else if (textoLower.Contains("habitacion") || textoLower.Contains("habitación"))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\n⚠ El asistente IA no gestiona habitaciones directamente.");
                                Console.WriteLine("   Para crear o modificar una habitación, use las opciones del menú principal (6–10).");
                                Console.ResetColor();
                            }
                            else if (textoLower.Contains("pago"))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\n⚠ El asistente IA no gestiona pagos directamente.");
                                Console.WriteLine("   Para registrar o modificar un pago, use las opciones del menú principal (16–20).");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }

                        Console.Write("\n¿Desea seguir preguntando al asistente? (s/n): ");
                        continuar = Console.ReadLine()?.ToLower();
                    }

                    break;

                case 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nSesión finalizada. Nos vemos en su próximo registro.".PadLeft((ancho + 60) / 2));
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Opción inválida. Intente nuevamente...".PadLeft((ancho + 40) / 2));
                    Console.ResetColor();
                    Console.ReadLine();
                    break;
            }
        } while (opcion != 0);
    }
}
