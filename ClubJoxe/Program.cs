using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubJoxe
{
    internal class Program
    {
        // Carga de Datos al iniciar (usa Persistencia para leer equiposLiga.txt)
        private static List<Equipo> listaEquipos = Persistencia.CargarEquipos();

        static void Main(string[] args)
        {
            bool salir = false;

            // Instancia del gestor de partidos, recibe la lista cargada
            var gestorPartidos = new Partido(listaEquipos);

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
                        // Jugar partido
                        JugarPartido(gestorPartidos);
                        Pausa();
                        Console.Clear();
                        break;
                    case "5":
                        ClasificacionEquipos();
                        Pausa();
                        Console.Clear();
                        break;
                    case "6":
                        // Guardar datos antes de salir
                        Console.WriteLine("Guardando datos antes de salir...");
                        Persistencia.GuardarEquipos(listaEquipos);
                        Console.WriteLine("¡Gracias por usar el programa!");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, intenta de nuevo.");
                        break;
                }
            }
        }

        // Métodos de Menú y Utilidad

        static void MostrarMenu()
        {
            Console.WriteLine("\n--- Menú Principal ---");
            Console.WriteLine("1. Crear un nuevo equipo");
            Console.WriteLine("2. Listar todos los equipos");
            Console.WriteLine("3. Ver jugadores de un equipo");
            Console.WriteLine("4. Jugar partido");
            Console.WriteLine("5. Clasificacion de los equipos");
            Console.WriteLine("5. Salir (y Guardar datos)");
            Console.Write("Selecciona una opción: ");
        }

        private static void Pausa()
        {
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey(true);
        }

        // Lógica de Equipos y Jugadores

        private static void CrearEquipo()
        {
            Console.Clear();
            Console.Write("Introduce el nombre del nuevo equipo: ");
            string nombreEquipo = Console.ReadLine();

            if (listaEquipos.Any(e => e.NombreEquipo.Equals(nombreEquipo, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Ya existe un equipo con este nombre. Inténtalo de nuevo.");
                Pausa();
                Console.Clear();
                return;
            }

            try
            {
                Equipo nuevoEquipo = new Equipo(nombreEquipo);
                listaEquipos.Add(nuevoEquipo);
                Console.WriteLine($"\nEquipo '{nombreEquipo}' creado con éxito.");

                Console.Write("¿Cuántos jugadores quieres agregar a este equipo?: ");
                int numJugadores;
                if (!int.TryParse(Console.ReadLine(), out numJugadores) || numJugadores < 0)
                {
                    numJugadores = 0;
                    Console.WriteLine("Número no válido. Se agregarán 0 jugadores.");
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

                    Console.Write("Posición (P, DF, MC, D): ");
                    string posString = Console.ReadLine().ToUpper();

                    ePosicion posicion;

                    while (!Enum.TryParse(posString, true, out posicion))
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
            Pausa();
            Console.Clear();
        }

        private static void ListarEquipos()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Equipos Creados ---");
            if (listaEquipos.Count == 0)
            {
                Console.WriteLine("No hay equipos creados aún.");
                Pausa();
                Console.Clear();
                return;
            }

            for (int i = 0; i < listaEquipos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaEquipos[i].NombreEquipo}");
            }
            Pausa();
            Console.Clear();
        }

        private static void VerJugadoresDeEquipo()
        {
            Console.Clear();
            ListarEquiposParaBusqueda();
            Console.Write("\nIntroduce el nombre del equipo para ver sus jugadores: ");
            string nombreEquipo = Console.ReadLine();

            var equipo = listaEquipos.FirstOrDefault(e => e.NombreEquipo.Equals(nombreEquipo, StringComparison.OrdinalIgnoreCase));

            if (equipo != null)
            {
                equipo.ListarJugadores();
            }
            else
            {
                Console.WriteLine($"El equipo '{nombreEquipo}' no fue encontrado.");
            }
            Pausa();
            Console.Clear();
        }

        // Versión para mostrar la lista sin pausa ni clear
        private static void ListarEquiposParaBusqueda()
        {
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

        // Lógica de Partidos

        private static void JugarPartido(Partido gestorPartidos)
        {
            Console.Clear();

            if (listaEquipos.Count < 2)
            {
                Console.WriteLine("Necesitas al menos 2 equipos para jugar un partido.");
                return;
            }

            if (gestorPartidos.CrearPartido())
            {
                gestorPartidos.ResultadoFinal();
            }
        }

        //Metodo para mostrar la clasificacion de los equipos (no se guarda en el fichero, solo es informacion)
        private static void ClasificacionEquipos()
        {
            Console.Clear();
            Console.WriteLine("--- CLASIFICACIÓN DE EQUIPOS ---");

            if (listaEquipos.Count == 0)
            {
                Console.WriteLine("No hay equipos creados ni estadísticas para mostrar.");
                return;
            }

            // define el orden que se posiciona la informacion dependiendo de
            // los puntos, goles a favor, goles en contra y luego muestra la lista
            var clasificacion = listaEquipos
                .OrderByDescending(e => e.Puntos) 
                .ThenByDescending(e => e.GolesFavor) 
                .ThenBy(e => e.GolesContra) 
                .ToList();

            // forma de mostrar la informacion "bonita", el primer numero referencia a la variable
            // y el segundo a los caracteres que ocupa, el - define la alineacion
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("{0,-20} | {1,-3} | {2,-3} | {3,-3} | {4,-3} | {5,-3}",
                              "EQUIPO", "PJ", "PTS", "GF", "GC", "DIF");
            Console.WriteLine("----------------------------------------------------------------");

            
            foreach (var equipo in clasificacion)
            {
                int diferencia = equipo.GolesFavor - equipo.GolesContra; 

                Console.WriteLine("{0,-20} | {1,-3} | {2,-3} | {3,-3} | {4,-3} | {5,-3}",
                                  equipo.NombreEquipo,
                                  equipo.PartidosJugados,
                                  equipo.Puntos,
                                  equipo.GolesFavor,
                                  equipo.GolesContra,
                                  diferencia); 
            }
            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}

