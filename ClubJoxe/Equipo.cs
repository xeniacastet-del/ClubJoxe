using System;
using System.Collections.Generic;

namespace ClubJoxe
{
    public class Equipo
    {
        // Atributos
        public string NombreEquipo { get; private set; }
        private List<Jugador> jugadores;

        // Constructor
        public Equipo(string nombreEquipo)
        {
            if (string.IsNullOrWhiteSpace(nombreEquipo))
            {
                throw new ArgumentException("El nombre del equipo es obligatorio.");
            }
            this.NombreEquipo = nombreEquipo;
            this.jugadores = new List<Jugador>();
        }

        // Método principal para agregar un jugador desde el menú
        public void AgregarJugador(Jugador nuevoJugador)
        {
            this.jugadores.Add(nuevoJugador);
            Console.WriteLine($"\tJugador {nuevoJugador.Nombre} agregado al equipo {this.NombreEquipo}.");
        }

        // Método para listar los jugadores del equipo
        public void ListarJugadores()
        {
            Console.WriteLine($"\n--- Jugadores del equipo {this.NombreEquipo} ---");
            if (this.jugadores.Count == 0)
            {
                Console.WriteLine("\tEl equipo no tiene jugadores.");
                return;
            }

            foreach (var jugador in this.jugadores)
            {
                jugador.MostrarInformacion();
            }
        }

        // Metodos requeridos para la persistencia
        // Añade los jugadores creados al fichero y los del fichero cargados
        public List<Jugador> GetJugadores()
        {
            return this.jugadores;
        }

        public void AgregarJugadorAlCargar(Jugador jugador)
        {
            this.jugadores.Add(jugador);
        }
    }
}