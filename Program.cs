using ProyectoSegundoParcialPrueba1.Models.CRUDs;

class Program
{
    static void Main(string[] args)
    {
        int opcion = 0;
        do
        {
            Console.Clear();
            int ancho = Console.WindowWidth; // ancho de la consola

            Console.ForegroundColor = ConsoleColor.Red;
            string[] titulo3D = {
                "██╗    ██╗███████╗██╗     ██████╗  ██████╗ ███╗   ███╗███████╗",
                "██║    ██║██╔════╝██║     ██╔═══╝ ██╔═══██╗████╗ ████║██╔════╝",
                "██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  ",
                "██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  ",
                "╚███╔███╔╝███████╗███████╗██████╗ ╚██████╔╝██║ ╚═╝ ██║███████╗",
                " ╚══╝╚══╝ ╚══════╝╚══════╝╚═════╝  ╚═════╝ ╚═╝     ╚═╝╚══════╝"
            };

            foreach (string linea in titulo3D)
                Console.WriteLine(linea.PadLeft((ancho + linea.Length) / 2));

            string subtitulo = "Chippin´in!";
            Console.WriteLine("=====================================".PadLeft((ancho + 35) / 2));
            Console.WriteLine(subtitulo.PadLeft((ancho + subtitulo.Length) / 2 - 1));
            Console.WriteLine("=====================================".PadLeft((ancho + 35) / 2));
            Console.ResetColor();

            // Caja de opciones
            Console.ForegroundColor = ConsoleColor.Yellow;
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

            string borde = "+-----------------------------+";
            Console.WriteLine(borde.PadLeft((ancho + borde.Length) / 2));
            foreach (string op in opciones)
            {
                string linea = $"| {op.PadRight(27)} |";
                Console.WriteLine(linea.PadLeft((ancho + linea.Length) / 2));
            }
            Console.WriteLine(borde.PadLeft((ancho + borde.Length) / 2));
            Console.ResetColor();

            // Mensaje debajo de la caja
            Console.ForegroundColor = ConsoleColor.Cyan;
            string mensaje = "Seleccione una opción:";
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
                    Console.WriteLine("\nGracias por usar el sistema. ¡Hasta pronto!".PadLeft((ancho + 40) / 2));
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
