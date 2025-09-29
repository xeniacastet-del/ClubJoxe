using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubJoxe
{
    internal class Partido
    {
        private readonly List<Equipo> EquiposRef; // referencia a la lista de equipos creada en Program
        private Equipo Local;
        private Equipo Visitante;
        private int GolesLocal;
        private int GolesVisitante;

        private static readonly Random Rng = new Random();

        public Partido(List<Equipo> equipos)
        {
            // guardamos la referencia a la lista de equipos ya creados
            EquiposRef = equipos ?? throw new ArgumentNullException(nameof(equipos));
        }

        // Escoge dos equipos distintos de la lista (aleatoriamente)
        public bool CrearPartido()
        {
            if (EquiposRef.Count < 2)
            {
                Console.WriteLine("Necesitas al menos 2 equipos para crear un partido.");
                return false;
            }

            int i1 = Rng.Next(EquiposRef.Count);
            int i2;
            do { i2 = Rng.Next(EquiposRef.Count); } while (i2 == i1);

            Local = EquiposRef[i1];
            Visitante = EquiposRef[i2];

            Console.WriteLine($"\nPartido seleccionado: {Local.NombreEquipo} vs {Visitante.NombreEquipo}");
            return true;
        }

        // Genera resultado aleatorio (0..5) y dice ganador o empate
        public void ResultadoFinal()
        {
            if (Local == null || Visitante == null)
            {
                Console.WriteLine("Primero debes crear el partido con CrearPartido().");
                return;
            }

            GolesLocal = Rng.Next(0, 6);
            GolesVisitante = Rng.Next(0, 6);

            Console.WriteLine($"\nResultado Final:");
            Console.WriteLine($"{Local.NombreEquipo} {GolesLocal} - {GolesVisitante} {Visitante.NombreEquipo}");

            if (GolesLocal > GolesVisitante)
                Console.WriteLine($"Ganador: {Local.NombreEquipo}");
            else if (GolesVisitante > GolesLocal)
                Console.WriteLine($"Ganador: {Visitante.NombreEquipo}");
            else
                Console.WriteLine("Resultado: EMPATE");

            Local.ActualizarEstadisticas(GolesLocal, GolesVisitante);
            Visitante.ActualizarEstadisticas(GolesVisitante, GolesLocal);
        }
    }
}
