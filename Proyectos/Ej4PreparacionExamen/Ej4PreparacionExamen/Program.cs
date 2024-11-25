using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ej4PreparacionExamen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Análisis de Datos en Matrices - Ventas Mensuales");

            // Ejemplo de datos de ventas
            int[,] ventas = {
                { 500, 700, 800, 600 },
                { 400, 300, 500, 450 },
                { 800, 900, 700, 850 },
                { 200, 400, 300, 250 }
            };

            int filas = ventas.GetLength(0); // Número de empleados
            int columnas = ventas.GetLength(1); // Número de meses

            // Menú
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Opciones:");
                Console.WriteLine("1. Calcular total de ventas por empleado");
                Console.WriteLine("2. Calcular total de ventas por mes");
                Console.WriteLine("3. Encontrar empleado con ventas más altas en un mes específico");
                Console.WriteLine("4. Identificar el mes con menor ventas globales");
                Console.WriteLine("5. Normalizar matriz de ventas");
                Console.WriteLine("6. Salir");
                Console.Write("Elige una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        TotalVentasPorEmpleado(ventas);
                        break;
                    case 2:
                        TotalVentasPorMes(ventas);
                        break;
                    case 3:
                        EmpleadoVentasAltasEnMes(ventas);
                        break;
                    case 4:
                        MesMenorVentasGlobales(ventas);
                        break;
                    case 5:
                        NormalizarMatriz(ventas);
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                        break;
                }

                if (opcion != 6)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            } while (opcion != 6);
        }

        static void TotalVentasPorEmpleado(int[,] ventas)
        {
            Console.Clear();
            Console.WriteLine("Total de ventas por empleado:");

            for (int i = 0; i < ventas.GetLength(0); i++)
            {
                int total = 0;
                for (int j = 0; j < ventas.GetLength(1); j++)
                {
                    total += ventas[i, j];
                }
                Console.WriteLine($"Empleado {i + 1}: {total}");
            }
        }

        static void TotalVentasPorMes(int[,] ventas)
        {
            Console.Clear();
            Console.WriteLine("Total de ventas por mes:");

            for (int j = 0; j < ventas.GetLength(1); j++)
            {
                int total = 0;
                for (int i = 0; i < ventas.GetLength(0); i++)
                {
                    total += ventas[i, j];
                }
                Console.WriteLine($"Mes {j + 1}: {total}");
            }
        }

        static void EmpleadoVentasAltasEnMes(int[,] ventas)
        {
            Console.Clear();
            Console.WriteLine("Empleado con ventas más altas en un mes específico:");
            Console.Write("Ingresa el número del mes (1 a 4): ");
            int mes = int.Parse(Console.ReadLine()) - 1;

            if (mes < 0 || mes >= ventas.GetLength(1))
            {
                Console.WriteLine("Mes inválido.");
                return;
            }

            int maxVentas = int.MinValue;
            int empleado = -1;

            for (int i = 0; i < ventas.GetLength(0); i++)
            {
                if (ventas[i, mes] > maxVentas)
                {
                    maxVentas = ventas[i, mes];
                    empleado = i;
                }
            }

            Console.WriteLine($"Empleado {empleado + 1} tuvo las ventas más altas ({maxVentas}) en el mes {mes + 1}.");
        }

        static void MesMenorVentasGlobales(int[,] ventas)
        {
            Console.Clear();
            Console.WriteLine("Mes con menor ventas globales:");

            int mesMenor = -1;
            int menorVentas = int.MaxValue;

            for (int j = 0; j < ventas.GetLength(1); j++)
            {
                int total = 0;
                for (int i = 0; i < ventas.GetLength(0); i++)
                {
                    total += ventas[i, j];
                }

                if (total < menorVentas)
                {
                    menorVentas = total;
                    mesMenor = j;
                }
            }

            Console.WriteLine($"El mes {mesMenor + 1} tuvo las menores ventas globales ({menorVentas}).");
        }

        static void NormalizarMatriz(int[,] ventas)
        {
            Console.Clear();
            Console.WriteLine("Normalizar matriz de ventas:");

            int min = int.MaxValue;
            int max = int.MinValue;

            // Encontrar valores mínimo y máximo
            foreach (int venta in ventas)
            {
                if (venta < min) min = venta;
                if (venta > max) max = venta;
            }

            Console.WriteLine($"Valor mínimo: {min}, Valor máximo: {max}");
            Console.WriteLine("\nMatriz normalizada (valores entre 0 y 1):");

            for (int i = 0; i < ventas.GetLength(0); i++)
            {
                for (int j = 0; j < ventas.GetLength(1); j++)
                {
                    double normalizado = (double)(ventas[i, j] - min) / (max - min);
                    Console.Write($"{normalizado:F2} ");
                }
                Console.WriteLine();
            }
        }
    }
}
