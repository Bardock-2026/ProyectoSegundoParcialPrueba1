using ProyectoSegundoParcialPrueba1.Models.CRUDs;

class Program
{
    static void Main(string[] args)
    {
        int opcion = 0;

        // --- Mensaje inicial serio ---
        Console.ForegroundColor = ConsoleColor.Magenta;
        string intro = "Bienvenidos, Gestione su hotel con precisión y excelencia.";
        foreach (char c in intro)
        {
            Console.Write(c);
            Thread.Sleep(40); // animación suave
        }
        Console.WriteLine("\n");
        Console.ResetColor();
        Thread.Sleep(500);

        do
        {
            Console.Clear();
            int ancho = Console.WindowWidth; // ancho de la consola

            // --- Título ASCII con colores arcoíris ---
            string[] titulo3D = {
                "██╗    ██╗███████╗██╗     ██████╗  ██████╗ ███╗   ███╗███████╗",
                "██║    ██║██╔════╝██║     ██╔═══╝ ██╔═══██╗████╗ ████║██╔════╝",
                "██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  ",
                "██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  ",
                "╚███╔███╔╝███████╗███████╗██████╗ ╚██████╔╝██║ ╚═╝ ██║███████╗",
                " ╚══╝╚══╝ ╚══════╝╚══════╝╚═════╝  ╚═════╝ ╚═╝     ╚═╝╚══════╝"
            };

            ConsoleColor[] colores = {
                ConsoleColor.Red,
                ConsoleColor.Yellow,
                ConsoleColor.Green,
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.Blue
            };

            int i = 0;
            foreach (string linea in titulo3D)
            {
                Console.ForegroundColor = colores[i % colores.Length];
                Console.WriteLine(linea.PadLeft((ancho + linea.Length) / 2));
                i++;
            }
            Console.ResetColor();

            // --- Caja con nombre del sistema centrado ---
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

            // --- Opciones divididas en 4 cajas horizontales ---
            string[] opciones = {
                "1. Crear Cliente",
                "2. Listar Clientes",
                "3. Buscar Cliente",
                "4. Actualizar Cliente",
                "5. Eliminar Cliente",
                "6. Crear Habitación",
                "7. Listar Habitaciones",
                "8. Buscar Habitación",
                "9. Actualizar Habitación",
                "10. Eliminar Habitación",
                "11. Crear Reserva",
                "12. Listar Reservas",
                "13. Buscar Reserva",
                "14. Actualizar Reserva",
                "15. Eliminar Reserva",
                "16. Registrar Pago",
                "17. Listar Pagos",
                "18. Buscar Pago",
                "19. Actualizar Pago",
                "20. Eliminar Pago",
                "0. Salir"
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

            // Opción Salir
            Console.ForegroundColor = ConsoleColor.Cyan;
            string salir = $"║ {opciones[20].PadRight(28)} ║";
            Console.WriteLine(salir.PadLeft((ancho + salir.Length) / 2));
            Console.ResetColor();

            // --- Mensaje debajo de las cajas ---
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