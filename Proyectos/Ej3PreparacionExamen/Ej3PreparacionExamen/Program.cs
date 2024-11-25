using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ej3PreparacionExamen
{
    internal class Program
    {
        // Clase para representar una reserva
        public class Reserva
        {
            public string NombreCliente { get; set; }
            public int NumeroPersonas { get; set; }

            public Reserva(string nombreCliente, int numeroPersonas)
            {
                NombreCliente = nombreCliente;
                NumeroPersonas = numeroPersonas;
            }

            public override string ToString()
            {
                return $"{NombreCliente} ({NumeroPersonas} personas)";
            }
        }

        static void Main(string[] args)
        {
            Queue<Reserva> colaReservas = new Queue<Reserva>();
            List<int> mesasOcupadas = new List<int>();
            int totalMesas = 10; // Número total de mesas disponibles en el restaurante
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("Sistema de Reservas del Restaurante");
                Console.WriteLine("1. Agregar nueva reserva");
                Console.WriteLine("2. Asignar mesa a la siguiente reserva");
                Console.WriteLine("3. Liberar una mesa");
                Console.WriteLine("4. Mostrar estado del sistema");
                Console.WriteLine("5. Salir");
                Console.Write("Elige una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarReserva(colaReservas);
                        break;
                    case 2:
                        AsignarMesa(colaReservas, mesasOcupadas, totalMesas);
                        break;
                    case 3:
                        LiberarMesa(mesasOcupadas);
                        break;
                    case 4:
                        MostrarEstado(colaReservas, mesasOcupadas, totalMesas);
                        break;
                    case 5:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 5);
        }

        static void AgregarReserva(Queue<Reserva> colaReservas)
        {
            Console.Clear();
            Console.WriteLine("Agregar nueva reserva");

            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();

            Console.Write("Número de personas: ");
            int numeroPersonas = int.Parse(Console.ReadLine());

            colaReservas.Enqueue(new Reserva(nombre, numeroPersonas));
            Console.WriteLine($"Reserva agregada para {nombre} ({numeroPersonas} personas).");
        }

        static void AsignarMesa(Queue<Reserva> colaReservas, List<int> mesasOcupadas, int totalMesas)
        {
            Console.Clear();
            Console.WriteLine("Asignar mesa a la siguiente reserva");

            if (colaReservas.Count == 0)
            {
                Console.WriteLine("No hay reservas pendientes.");
                return;
            }

            if (mesasOcupadas.Count >= totalMesas)
            {
                Console.WriteLine("Todas las mesas están ocupadas.");
                return;
            }

            Reserva reserva = colaReservas.Dequeue();
            int numeroMesa = ObtenerMesaDisponible(mesasOcupadas, totalMesas);

            mesasOcupadas.Add(numeroMesa);
            Console.WriteLine($"Mesa {numeroMesa} asignada a {reserva.NombreCliente} ({reserva.NumeroPersonas} personas).");
        }

        static void LiberarMesa(List<int> mesasOcupadas)
        {
            Console.Clear();
            Console.WriteLine("Liberar mesa");

            if (mesasOcupadas.Count == 0)
            {
                Console.WriteLine("No hay mesas ocupadas para liberar.");
                return;
            }

            Console.WriteLine("Mesas ocupadas:");
            for (int i = 0; i < mesasOcupadas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Mesa {mesasOcupadas[i]}");
            }

            Console.Write("Elige el número de la mesa que deseas liberar: ");
            int opcion = int.Parse(Console.ReadLine());

            if (opcion < 1 || opcion > mesasOcupadas.Count)
            {
                Console.WriteLine("Opción inválida.");
                return;
            }

            int mesaLiberada = mesasOcupadas[opcion - 1];
            mesasOcupadas.RemoveAt(opcion - 1);
            Console.WriteLine($"Mesa {mesaLiberada} liberada exitosamente.");
        }

        static void MostrarEstado(Queue<Reserva> colaReservas, List<int> mesasOcupadas, int totalMesas)
        {
            Console.Clear();
            Console.WriteLine("Estado actual del sistema");

            Console.WriteLine("\nReservas pendientes:");
            if (colaReservas.Count > 0)
            {
                foreach (var reserva in colaReservas)
                {
                    Console.WriteLine($"- {reserva}");
                }
            }
            else
            {
                Console.WriteLine("No hay reservas pendientes.");
            }

            Console.WriteLine("\nMesas ocupadas:");
            if (mesasOcupadas.Count > 0)
            {
                foreach (var mesa in mesasOcupadas)
                {
                    Console.WriteLine($"- Mesa {mesa}");
                }
            }
            else
            {
                Console.WriteLine("No hay mesas ocupadas.");
            }

            Console.WriteLine($"\nMesas disponibles: {totalMesas - mesasOcupadas.Count}");
        }

        static int ObtenerMesaDisponible(List<int> mesasOcupadas, int totalMesas)
        {
            for (int i = 1; i <= totalMesas; i++)
            {
                if (!mesasOcupadas.Contains(i))
                {
                    return i;
                }
            }
            return -1; // Este caso no debería ocurrir si verificamos antes la disponibilidad
        }
    }
}
