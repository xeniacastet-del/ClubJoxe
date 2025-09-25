using System;
using System.Collections.Generic;

namespace ClubJoxe
{
    internal class Partido
    {
        private readonly List<Equipo> equiposRef; // referencia a la lista de equipos creada en Program
        private Equipo local;
        private Equipo visitante;
        private int golesLocal;
        private int golesVisitante;

        private static readonly Random rng = new Random();

        public Partido(List<Equipo> equipos)
        {
            // guardamos la referencia a la lista de equipos ya creados
            equiposRef = equipos ?? new List<Equipo>();
        }

        // Escoge dos equipos distintos de la lista (aleatoriamente)
        public bool crearParido()
        {
            if (equiposRef == null || equiposRef.Count < 2)
            {
                Console.WriteLine("Necesitas al menos 2 equipos para crear un partido.");
                return false;
            }

            int i1 = rng.Next(equiposRef.Count);
            int i2;
            do { i2 = rng.Next(equiposRef.Count); } while (i2 == i1);

            local = equiposRef[i1];
            visitante = equiposRef[i2];

            Console.WriteLine($"\nPartido seleccionado: {local.NombreEquipo} vs {visitante.NombreEquipo}");
            return true;
        }

        // Genera resultado aleatorio (0..5) y dice ganador o empate
        public void ResultadoFinal()
        {
            if (local == null || visitante == null)
            {
                Console.WriteLine("Primero debes crear el partido con crearParido().");
                return;
            }

            golesLocal = rng.Next(0, 6);
            golesVisitante = rng.Next(0, 6);

            Console.WriteLine($"{local.NombreEquipo} {golesLocal} - {golesVisitante} {visitante.NombreEquipo}");

            if (golesLocal > golesVisitante)
                Console.WriteLine($"Ganador: {local.NombreEquipo}");
            else if (golesVisitante > golesLocal)
                Console.WriteLine($"Ganador: {visitante.NombreEquipo}");
            else
                Console.WriteLine("Resultado: EMPATE");
        }
    }
}
