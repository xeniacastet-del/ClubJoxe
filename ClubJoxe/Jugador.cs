using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJoxe
{
    public enum ePosicion
    {
        P,
        DF,
        MC,
        D
    }
    public class Jugador
    {
        private int dorsal;
        private string nombre;
        private ePosicion posicion;

        public int Dorsal { get { return dorsal; } set { dorsal = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }

        public ePosicion Posicion { get { return posicion; } private set { posicion = value; } }
        public Jugador(string nombre, int dorsal, ePosicion posicion) 
        {
            this.Nombre = nombre;
            this.Dorsal = dorsal;
            this.Posicion = posicion;

        }

        // Método para mostrar la información del jugador
        public void MostrarInformacion()
        {
            Console.WriteLine($"\tNombre: {this.Nombre}, Dorsal: {this.Dorsal}, Posición: {this.Posicion}");
        }
    }
}
