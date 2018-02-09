using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AppGameOfLife
{
    class AppGameOfLife
    {
        static int nFilas = Console.LargestWindowHeight / 3 * 2;

        static int nColumnas = Console.LargestWindowWidth / 3 * 2;

        static char celula = '█';

        //static char celula = '*';

        static int[,] habitat = new int[nFilas, nColumnas];

        static void Main(string[] args)
        {
            //Propiedades de la ventana
            Console.Title = "The Life Game"; //Título de la ventana
            Console.CursorVisible = false; //Ocultar cursor
            Console.WindowHeight = nFilas + 8; //Alto de la ventana
            Console.WindowWidth = nColumnas + 2; //Ancho de la ventana
            ConsoleKeyInfo tecla;
            CrearHabitatRandom(habitat, 30);
            DibujarHabitat(habitat);
            tecla = Console.ReadKey(true);
            //Console.ReadKey(true);
            //while (true)
            //{
            //    EvolucionarHabitat(habitat);
            //    DibujarHabitat(habitat);
            //}
            do
            {
                do
                {
                    switch (tecla.Key)
                    {
                        case ConsoleKey.I:
                            EvolucionarHabitat(habitat);
                            DibujarHabitat(habitat);
                            break;
                        case ConsoleKey.A:
                            EvolucionarHabitat(habitat);
                            DibujarHabitat(habitat);
                            break;
                        case ConsoleKey.R:
                            CrearHabitatRandom(habitat, 30);
                            DibujarHabitat(habitat);
                            break;
                        case ConsoleKey.S:
                            break;
                        case ConsoleKey.L:
                            break;
                        default:
                            break;
                    }


                } while (!Console.KeyAvailable && tecla.Key != ConsoleKey.I && tecla.Key != ConsoleKey.R);
                tecla = Console.ReadKey(true);
            } while (tecla.Key != ConsoleKey.Escape);

        }

        static void CrearHabitatPrueba1(int[,] lienzo)
        {
            lienzo[2, 1] = 1;
            lienzo[2, 2] = 1;
            lienzo[2, 3] = 1;
        }

        static void CrearHabitatRandom(int[,] lienzo, int probabilidadVida)
        {
            Random rnd = new Random();
            for (int i = 0; i < nFilas; i++)
            {
                for (int j = 0; j < nColumnas; j++)
                {
                    int calculoProbabilidad = rnd.Next(0, 100);
                    if (calculoProbabilidad < probabilidadVida)
                        lienzo[i, j] = 1;
                    else
                        lienzo[i, j] = 0;
                }
            }
        }

        static void DibujarHabitat(int[,] habitat)
        {
            Console.CursorLeft = 1;
            Console.CursorTop = 1;
            for (int i = 0; i < nFilas; i++)
            {
                Console.CursorLeft = 1;
                for (int j = 0; j < nColumnas; j++)
                {
                    if (habitat[i, j] == 1)
                        Console.Write(celula);
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static void EvolucionarHabitat(int[,] habitat)
        {
            sbyte[,] habitatEvolucionado = new sbyte[nFilas, nColumnas];
            for (int i = 0; i < nFilas; i++)
            {
                for (int j = 0; j < nColumnas; j++)
                {
                    if (habitat[i, j] == 1)
                    {
                        switch (ContarVecinos(habitat, i, j))
                        {
                            case 2:
                                habitatEvolucionado[i, j] = 1;
                                break;
                            case 3:
                                habitatEvolucionado[i, j] = 1;
                                break;
                            default:
                                habitatEvolucionado[i, j] = 0;
                                break;
                        }
                    }
                    else if (ContarVecinos(habitat, i, j) == 3)
                        habitatEvolucionado[i, j] = 1;
                }
            }

            for (int i = 0; i < nFilas; i++)
            {
                for (int j = 0; j < nColumnas; j++)
                {
                    habitat[i, j] = habitatEvolucionado[i, j];
                }
            }
        }

        static int ContarVecinos(int[,] habitat, int fila, int columna)
        {
            int contVecinos = 0;

            contVecinos = habitat[(fila + nFilas - 1) % nFilas, (columna + nColumnas - 1) % nColumnas]
                + habitat[(fila + nFilas - 1) % nFilas, columna]
                + habitat[(fila + nFilas - 1) % nFilas, (columna + nColumnas + 1) % nColumnas]
                + habitat[fila, (columna + nColumnas - 1) % nColumnas] + habitat[fila, (columna + nColumnas + 1) % nColumnas]
                + habitat[(fila + nFilas + 1) % nFilas, (columna + nColumnas - 1) % nColumnas]
                + habitat[(fila + nFilas + 1) % nFilas, columna]
                + habitat[(fila + nFilas + 1) % nFilas, (columna + nColumnas + 1) % nColumnas];
            return contVecinos;
        }
    }
}
