using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJoxe
{
    internal class Program
    {
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
                        //Metodo crear equipo
                        break;
                    case "2":
                        //Metodo listar equipos
                        break;
                    case "3":
                        //Metodo listar jugadores de equipo
                        break;
                    case "4":
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
            Console.WriteLine("\n--- Menú Principal ---");
            Console.WriteLine("1. Crear un nuevo equipo");
            Console.WriteLine("2. Listar todos los equipos");
            Console.WriteLine("3. Ver jugadores de un equipo");
            Console.WriteLine("4. Salir");
            Console.Write("Selecciona una opción: ");
        }

        

            

    }
}
