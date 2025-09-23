using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJoxe
{
    internal class Equipo
    {
        // Atributos
        public string NombreEquipo { get; private set; }
        private List<Jugador> jugadores;

        // Constructor
        public Equipo(string nombreEquipo)
        {
            this.NombreEquipo = nombreEquipo;
            this.jugadores = new List<Jugador>();
        }

        // Método para agregar un jugador
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
    }
}
