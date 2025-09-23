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
    internal class Jugador
    {
        private int dorsal;
        private string nombre;
        private ePosicion posicion;

        public int Dorsal { get { return dorsal; } set { dorsal = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }

        public ePosicion Posicion { get { return posicion; } private set { posicion = value; } }
        public Jugador( int dorsal, string nombre, ePosicion posicion) 
        {
            this.Dorsal = dorsal;
            this.Nombre = nombre;
            this.Posicion = posicion;

        }
    }
}
