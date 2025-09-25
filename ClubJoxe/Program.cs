using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJoxe
{
    internal class Program
    {

        // Lista estática para almacenar todos los equipos
        private static List<Equipo> listaEquipos = new List<Equipo>();

        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        CrearEquipo();
                        break;

                    case "2":
                        ListarEquipos();
                        break;

                    case "3":
                        VerJugadoresDeEquipo();
                        break;

                    case "4":
                        JugarPartido();
                        break;

                    case "5":
                        Console.WriteLine("¡Gracias por usar el programa!");
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Por favor, intenta de nuevo.");
                        break;
                }

            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("1. Crear un nuevo equipo");
            Console.WriteLine("2. Listar todos los equipos");
            Console.WriteLine("3. Ver jugadores de un equipo");
            Console.WriteLine("4. Jugar partido");    
            Console.WriteLine("5. Salir");              

        }

        private static void CrearEquipo()
        {
            Console.Clear();
            Console.Write("Introduce el nombre del nuevo equipo: ");
            string nombreEquipo = Console.ReadLine();

            // Verificamos si ya existe un equipo con ese nombre
            if (listaEquipos.Any(e => e.NombreEquipo.Equals(nombreEquipo, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Ya existe un equipo con este nombre. Inténtalo de nuevo.");
                return;
            }

            try
            {
                Equipo nuevoEquipo = new Equipo(nombreEquipo);
                listaEquipos.Add(nuevoEquipo);
                Console.WriteLine($"\nEquipo '{nombreEquipo}' creado con éxito.");

                // Pedimos los jugadores
                Console.Write("¿Cuántos jugadores quieres agregar a este equipo?: ");
                int numJugadores;
                while (!int.TryParse(Console.ReadLine(), out numJugadores) || numJugadores < 0)
                {
                    Console.Write("Número no válido. Por favor, introduce un número positivo: ");
                }

                for (int i = 0; i < numJugadores; i++)
                {
                    Console.WriteLine($"\n--- Jugador {i + 1} ---");
                    Console.Write("Nombre: ");
                    string nombre = Console.ReadLine();

                    Console.Write("Dorsal: ");
                    int dorsal;
                    while (!int.TryParse(Console.ReadLine(), out dorsal) || dorsal <= 0)
                    {
                        Console.Write("Dorsal no válido. Introduce un número positivo: ");
                    }

                    Console.WriteLine("Posición (P, DF, MC, D): ");
                    string posString = Console.ReadLine().ToUpper();
                    ePosicion posicion;

                    while (!Enum.TryParse(posString, out posicion))
                    {
                        Console.Write("Posición no válida. Introduce P, DF, MC o D: ");
                        posString = Console.ReadLine().ToUpper();
                    }

                    Jugador nuevoJugador = new Jugador(nombre, dorsal, posicion);
                    nuevoEquipo.AgregarJugador(nuevoJugador);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error al crear el equipo: {ex.Message}");
            }
        }

        private static void ListarEquipos()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Equipos Creados ---");
            if (listaEquipos.Count == 0)
            {
                Console.WriteLine("No hay equipos creados aún.");
                return;
            }

            for (int i = 0; i < listaEquipos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaEquipos[i].NombreEquipo}");
            }
        }

        private static void VerJugadoresDeEquipo()
        {
            Console.Clear();
            ListarEquipos();
            Console.Write("Introduce el nombre del equipo para ver sus jugadores: ");
            string nombreEquipo = Console.ReadLine();

            // Buscamos el equipo en la lista
            var equipo = listaEquipos.FirstOrDefault(e => e.NombreEquipo.Equals(nombreEquipo, StringComparison.OrdinalIgnoreCase));

            if (equipo != null)
            {
                equipo.ListarJugadores();
            }
            else
            {
                Console.WriteLine($"El equipo '{nombreEquipo}' no fue encontrado.");
            }
        }
        private static void JugarPartido()
        {
            Console.Clear();

            if (listaEquipos.Count < 2)
            {
                Console.WriteLine("Necesitas al menos 2 equipos para jugar un partido.");
                return;
            }

            var partido = new Partido(listaEquipos);
            if (!partido.crearParido())
                return;

            partido.ResultadoFinal();
        }

    }

}

