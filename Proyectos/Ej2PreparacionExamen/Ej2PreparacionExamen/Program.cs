class Program
{
    static void Main(string[] args)
    {
        // Diccionario para almacenar las calificaciones de los estudiantes
        Dictionary<string, List<double>> calificaciones = new Dictionary<string, List<double>>();
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("Sistema de Calificaciones");
            Console.WriteLine("1. Agregar estudiante y calificaciones");
            Console.WriteLine("2. Calcular promedio de un estudiante");
            Console.WriteLine("3. Mostrar estudiante con promedio más alto y más bajo");
            Console.WriteLine("4. Mostrar estudiantes con calificaciones bajo un umbral");
            Console.WriteLine("5. Salir");
            Console.Write("Elige una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    AgregarEstudiante(calificaciones);
                    break;
                case 2:
                    CalcularPromedio(calificaciones);
                    break;
                case 3:
                    MostrarPromediosExtremos(calificaciones);
                    break;
                case 4:
                    MostrarEstudiantesConBajoPromedio(calificaciones);
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

    static void AgregarEstudiante(Dictionary<string, List<double>> calificaciones)
    {
        Console.Clear();
        Console.WriteLine("Agregar un nuevo estudiante");

        Console.Write("Nombre del estudiante: ");
        string nombre = Console.ReadLine();

        if (calificaciones.ContainsKey(nombre))
        {
            Console.WriteLine("El estudiante ya existe. Agregando nuevas calificaciones.");
        }
        else
        {
            calificaciones[nombre] = new List<double>();
        }

        Console.Write("¿Cuántas calificaciones deseas agregar?: ");
        int cantidad = int.Parse(Console.ReadLine());

        for (int i = 0; i < cantidad; i++)
        {
            Console.Write($"Introduce la calificación #{i + 1}: ");
            double calificacion = double.Parse(Console.ReadLine());
            calificaciones[nombre].Add(calificacion);
        }

        Console.WriteLine("Calificaciones agregadas exitosamente.");
    }

    static void CalcularPromedio(Dictionary<string, List<double>> calificaciones)
    {
        Console.Clear();
        Console.WriteLine("Calcular promedio de un estudiante");

        Console.Write("Nombre del estudiante: ");
        string nombre = Console.ReadLine();

        if (calificaciones.ContainsKey(nombre))
        {
            var promedio = calificaciones[nombre].Average();
            Console.WriteLine($"El promedio de {nombre} es: {promedio:F2}");
        }
        else
        {
            Console.WriteLine("El estudiante no existe.");
        }
    }

    static void MostrarPromediosExtremos(Dictionary<string, List<double>> calificaciones)
    {
        Console.Clear();
        Console.WriteLine("Estudiantes con promedios más alto y más bajo");

        if (calificaciones.Count == 0)
        {
            Console.WriteLine("No hay estudiantes en el sistema.");
            return;
        }

        var promedios = calificaciones.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Average()
        );

        var maxPromedio = promedios.MaxBy(kvp => kvp.Value);
        var minPromedio = promedios.MinBy(kvp => kvp.Value);

        Console.WriteLine($"Estudiante con el promedio más alto: {maxPromedio.Key} ({maxPromedio.Value:F2})");
        Console.WriteLine($"Estudiante con el promedio más bajo: {minPromedio.Key} ({minPromedio.Value:F2})");
    }

    static void MostrarEstudiantesConBajoPromedio(Dictionary<string, List<double>> calificaciones)
    {
        Console.Clear();
        Console.WriteLine("Estudiantes con promedios bajo un umbral");

        Console.Write("Introduce el umbral de promedio: ");
        double umbral = double.Parse(Console.ReadLine());

        var estudiantes = calificaciones
            .Where(kvp => kvp.Value.Average() < umbral)
            .ToList();

        if (estudiantes.Count > 0)
        {
            Console.WriteLine("Estudiantes con promedio bajo el umbral:");
            foreach (var estudiante in estudiantes)
            {
                Console.WriteLine($"{estudiante.Key}: Promedio = {estudiante.Value.Average():F2}");
            }
        }
        else
        {
            Console.WriteLine("No hay estudiantes con promedio bajo el umbral especificado.");
        }
    }
}