using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClubJoxe
{
    public static class Persistencia
    {
        // Si la ruta esta en otro sitio modificar la RutaArchivo donde se guarda en cada ordenador
        // RUTA JORDI ORDENADOR MATI: @"C:\Users\Mati\ficherosNET\equiposLiga.txt"
        // RUTA XENIA ORDENADOR MATI: @"C:\Users\Mati\ficherosNET\equiposLiga.txt"
        private const string RutaArchivo = @"C:\Users\Mati\ficherosNET\equiposLiga.txt";

        //Caracter que delimita los valores del archivo de texto para que el programa lo pueda leer
        private const char Separador = '|';

        public static void GuardarEquipos(List<Equipo> equipos)
        {
            using (StreamWriter sw = new StreamWriter(RutaArchivo))
            {
                try
                {
                    foreach (var equipo in equipos)
                    {
                        sw.WriteLine($"E{Separador}{equipo.NombreEquipo}");

                        var jugadores = equipo.GetJugadores();

                        foreach (var jugador in jugadores)
                        {
                            string lineaJugador = $"J{Separador}{jugador.Nombre}{Separador}{jugador.Dorsal}{Separador}{jugador.Posicion}";
                            sw.WriteLine(lineaJugador);
                        }
                    }
                    Console.WriteLine($"\nDatos guardados correctamente en '{RutaArchivo}'");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError al guardar los datos en {RutaArchivo}: {ex.Message}");
                }
            }
        }

        public static List<Equipo> CargarEquipos()
        {
            var equiposCargados = new List<Equipo>();

            if (!File.Exists(RutaArchivo))
            {
                Console.WriteLine($"\nArchivo '{RutaArchivo}' no encontrado. Se inicia con una lista vacía.");
                return equiposCargados;
            }

            using (StreamReader sr = new StreamReader(RutaArchivo))
            {
                try
                {
                    string linea;
                    Equipo equipoActual = null;

                    while ((linea = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;

                        string[] partes = linea.Split(Separador);
                        if (partes.Length == 0 || partes[0].Length == 0) continue;

                        char tipoObjeto = partes[0][0];

                        if (tipoObjeto == 'E' && partes.Length >= 2)
                        {
                            string nombreEquipo = partes[1];
                            equipoActual = new Equipo(nombreEquipo);
                            equiposCargados.Add(equipoActual);
                        }
                        else if (tipoObjeto == 'J' && partes.Length >= 4 && equipoActual != null)
                        {
                            string nombre = partes[1];

                            if (int.TryParse(partes[2], out int dorsal) && Enum.TryParse(partes[3], out ePosicion posicion))
                            {
                                var jugador = new Jugador(nombre, dorsal, posicion);
                                equipoActual.AgregarJugadorAlCargar(jugador);
                            }
                        }
                    }

                    Console.WriteLine($"\nDatos cargados correctamente desde '{RutaArchivo}'");
                    return equiposCargados;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError al cargar los datos. Se inicia con una lista vacía: {ex.Message}");
                    return new List<Equipo>();
                }
            }
        }
    }
}