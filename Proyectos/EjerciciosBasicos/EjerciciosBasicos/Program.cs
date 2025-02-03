using System;
using System.Text.RegularExpressions;

class program
{
    static void Main()
    {
        Console.WriteLine("Patrón (a):");
        PatternA(5);
        Console.WriteLine();



        //-----------------EJERCICIO 18----------------------

    }

    //Compara si mark es mayor o igual a 50. Imprime "PASS" si es verdadero, de lo contrario imprime "FAIL".
    static void ej1()
    {
        Console.Write("Introduce la nota (mark): ");
        int mark = int.Parse(Console.ReadLine());

        if (mark >= 50)
        {
            Console.WriteLine("PASS");
        }
        else
        {
            Console.WriteLine("FAIL");
        }
    }

    //Comprueba si el residuo al dividir entre 2 es 0 (número par). Si no, es impar.
    static void ej2()
    {
        Console.Write("Introduce un número: ");
        int numero = int.Parse(Console.ReadLine());

        if (numero % 2 == 0)
        {
            Console.WriteLine("El número es par");
        }
        else
        {
            Console.WriteLine("El número es impar");
        }
    }

    //Suma y media de números
    static void ej3BucleFor()
    {
        Console.Write("Introduce el límite superior: ");
        int limite = int.Parse(Console.ReadLine());

        int suma = 0;
        for (int i = 1; i <= limite; i++)
        {
            suma += i;
        }

        double media = (double)suma / limite;
        Console.WriteLine($"La suma es {suma}");
        Console.WriteLine($"La media es {media}");
    }

    static void ej3BucleWhile()
    {
        Console.Write("Introduce el límite superior: ");
        int limite = int.Parse(Console.ReadLine());

        int suma = 0, i = 1;
        while (i <= limite)
        {
            suma += i;
            i++;
        }

        double media = (double)suma / limite;
        Console.WriteLine($"La suma es {suma}");
        Console.WriteLine($"La media es {media}");
    }

    static void ej3BucleDoWhile()
    {
        Console.Write("Introduce el límite superior: ");
        int limite = int.Parse(Console.ReadLine());

        int suma = 0, i = 1;
        do
        {
            suma += i;
            i++;
        } while (i <= limite);

        double media = (double)suma / limite;
        Console.WriteLine($"La suma es {suma}");
        Console.WriteLine($"La media es {media}");
    }

    //Series armónicas: Se calcula la suma desde el menor al mayor y viceversa. Por precisión de coma flotante, la suma desde el menor es menos precisa.
    static void ej4()
    {
        int n = 50000;
        double sumaIzqADer = 0.0, sumaDerAIzq = 0.0;

        for (int i = 1; i <= n; i++)
        {
            sumaIzqADer += 1.0 / i;
        }

        for (int i = n; i >= 1; i--)
        {
            sumaDerAIzq += 1.0 / i;
        }

        Console.WriteLine($"Suma de izquierda a derecha: {sumaIzqADer}");
        Console.WriteLine($"Suma de derecha a izquierda: {sumaDerAIzq}");
        Console.WriteLine($"Diferencia: {sumaIzqADer - sumaDerAIzq}");
    }

    //Cálculo de π usando una serie
    //La serie alterna signos y converge lentamente a π multiplicando por 4
    static void ej5()
    {
        int terminos = 100000;
        double pi = 0.0;

        for (int i = 0; i < terminos; i++)
        {
            double termino = Math.Pow(-1, i) / (2 * i + 1);
            pi += termino;
        }

        pi *= 4;
        Console.WriteLine($"El valor aproximado de π es: {pi}");
    }

    /*
     * números Tribonacci son un secuencia de números T(n) similar a los números Fibonacci, 
     * excepto que un número es formado añadiendo los tres números previos. Por ejemplo , 
     * T(n)=T(n-1)+T(n-2)+T(n-3), T(1)=T(2)=1, y T(3)=2. 
     * Escribe un programa que calcule los primeros veinte números Tribonacci.
     */
    static void ej6()
    {
        int n = 20;
        int[] tribonacci = new int[n];
        tribonacci[0] = 1;
        tribonacci[1] = 1;
        tribonacci[2] = 2;

        for (int i = 3; i < n; i++)
        {
            tribonacci[i] = tribonacci[i - 1] + tribonacci[i - 2] + tribonacci[i - 3];
        }

        Console.WriteLine("Los primeros 20 números Tribonacci son:");
        Console.WriteLine(string.Join(", ", tribonacci));
    }

    //Escribe un programa que muestre multiplicación la tabla del 1 al 9 como se muestra usando dos bucles anidados.
    static void ej7()
    {
        Console.WriteLine("* |  1  2  3  4  5  6  7  8  9");
        Console.WriteLine("-------------------------------");

        for (int i = 1; i <= 9; i++)
        {
            Console.Write($"{i} |");
            for (int j = 1; j <= 9; j++)
            {
                Console.Write($" {i * j,2}");
            }
            Console.WriteLine();
        }
    }

    /*
     * Escribe un programa cuyo usuario pueda introducir el radio ( tipo de dato double) 
     * y que calcule el volumen y el Surface del área de una esfera.
     */
    static void ej8()
    {
        Console.Write("Introduce el radio (en metros): ");
        double radio = double.Parse(Console.ReadLine());

        // Fórmulas para el volumen y el área superficial de una esfera
        double volumen = (4.0 / 3.0) * Math.PI * Math.Pow(radio, 3);
        double areaSuperficial = 4 * Math.PI * Math.Pow(radio, 2);

        // Resultados
        Console.WriteLine($"El volumen es {volumen:F2} m³");
        Console.WriteLine($"El área superficial es {areaSuperficial:F2} m²");
    }

    //Escribe un programa que invierta una cadena introducida por el usuario
    static void ej9()
    {
        Console.Write("Introduce una cadena: ");
        string cadena = Console.ReadLine();

        // Invertir la cadena
        char[] caracteres = cadena.ToCharArray();
        Array.Reverse(caracteres);
        string cadenaInvertida = new string(caracteres);

        Console.WriteLine($"La cadena al revés es: {cadenaInvertida}");
    }

    /* PALÍNDROMOS
     * Una palabra que puede ser leída igualmente de delante hacia atrás y 
     * de atrás a adelante se llama palíndromo. Ejemplo: “ama”,( caso no sensible a mayúsculas). 
     * Escribe un programa que introduciendo el usuario una palabra imprima “xxx” es/no es palíndromo
     */
    static void ej10()
    {
        Console.Write("Introduce una palabra o frase: ");
        string entrada = Console.ReadLine().ToLower();

        // Eliminar caracteres no alfanuméricos
        string limpio = Regex.Replace(entrada, "[^a-z0-9]", "");

        // Comprobar si es palíndromo
        char[] caracteres = limpio.ToCharArray();
        Array.Reverse(caracteres);
        string invertido = new string(caracteres);

        if (limpio == invertido)
        {
            Console.WriteLine("Es un palíndromo.");
        }
        else
        {
            Console.WriteLine("No es un palíndromo.");
        }
    }

    //Convierte una cadena binaria introducida por el usuario a su equivalente decimal.
    static void ej11()
    {
        Console.Write("Introduce una cadena binaria: ");
        string binario = Console.ReadLine();

        // Validar que la cadena sea binaria
        if (System.Text.RegularExpressions.Regex.IsMatch(binario, "^[01]+$"))
        {
            int decimalEquivalente = Convert.ToInt32(binario, 2);
            Console.WriteLine($"El equivalente decimal es: {decimalEquivalente}");
        }
        else
        {
            Console.WriteLine($"Error: la cadena binaria \"{binario}\" es inválida.");
        }
    }

    //Convierte una cadena hexadecimal introducida por el usuario a su equivalente decimal
    static void ej12()
    {
        Console.Write("Introduce una cadena hexadecimal: ");
        string hexadecimal = Console.ReadLine();

        try
        {
            int decimalEquivalente = Convert.ToInt32(hexadecimal, 16);
            Console.WriteLine($"El equivalente decimal para \"{hexadecimal}\" es: {decimalEquivalente}");
        }
        catch (FormatException)
        {
            Console.WriteLine($"Error: la cadena hexadecimal \"{hexadecimal}\" es inválida.");
        }
    }

    /*
     * Escribe un programa donde el usuario introduzca el número de estudiantes. 
     * Deberá introducirse esta información por línea de commandos y guardarla en 
     * una variable de tipo entera. Se deberá incluir, además el número de estudiantes , 
     * las notas de los mismos en una array, como se verá en el ejemplo siguiente y calcular la media. 
     * Tu programa deberá comprobar que la nota está entre 0 y 100.
     */
    static void ej13() {
        Console.Write("Introduce el número de estudiantes: ");
        int numEstudiantes = int.Parse(Console.ReadLine());
        double[] notas = new double[numEstudiantes];

        for (int i = 0; i < numEstudiantes; i++)
        {
            while (true)
            {
                Console.Write($"Introduce la nota del estudiante {i + 1}: ");
                double nota = double.Parse(Console.ReadLine());

                if (nota >= 0 && nota <= 100)
                {
                    notas[i] = nota;
                    break;
                }
                else
                {
                    Console.WriteLine("Nota inválida. Introdúcela de nuevo.");
                }
            }
        }

        double suma = 0;
        foreach (double nota in notas)
        {
            suma += nota;
        }

        Console.WriteLine($"La media es {suma / numEstudiantes:F2}");
    }

    //Escribe un programa que convierta una cadena string en una cadena binaria equivalente
    static void ej14() {
        Console.Write("Introduce una cadena hexadecimal: ");
        string hexadecimal = Console.ReadLine();

        try
        {
            string binario = string.Join(" ", Convert.ToString(Convert.ToInt32(hexadecimal, 16), 2).PadLeft(4 * hexadecimal.Length, '0'));
            Console.WriteLine($"La cadena binaria equivalente para \"{hexadecimal}\" es: {binario}");
        }
        catch
        {
            Console.WriteLine($"Error: cadena hexadecimal inválida \"{hexadecimal}\".");
        }
    }

    /* Escribe un programa, que lea en n notas ( entre 0 y 100, incluido) y que muestre la media, 
     * el mínimo, el máximo y la desviación estandar. Este programa comprobará que las entradas son válidas. 
     * Debe almacenar las notas en un array y usar un métodos para cada operación anterior.
     */
    static void ej15()
    {
        Console.Write("Introduce el número de estudiantes: ");
        int n = int.Parse(Console.ReadLine());
        double[] notas = new double[n];

        for (int i = 0; i < n; i++)
        {
            while (true)
            {
                Console.Write($"Introduce la nota del estudiante {i + 1}: ");
                double nota = double.Parse(Console.ReadLine());

                if (nota >= 0 && nota <= 100)
                {
                    notas[i] = nota;
                    break;
                }
                else
                {
                    Console.WriteLine("Nota inválida. Introdúcela de nuevo.");
                }
            }
        }

        Console.WriteLine($"La media es {CalcularMedia(notas):F2}");
        Console.WriteLine($"El mínimo es {CalcularMinimo(notas):F2}");
        Console.WriteLine($"El máximo es {CalcularMaximo(notas):F2}");
        Console.WriteLine($"La desviación estándar es {CalcularDesviacionEstandar(notas):F6}");
    }

    static double CalcularMedia(double[] notas) => notas.Average();
    static double CalcularMinimo(double[] notas) => notas.Min();
    static double CalcularMaximo(double[] notas) => notas.Max();
    static double CalcularDesviacionEstandar(double[] notas)
    {
        double media = CalcularMedia(notas);
        return Math.Sqrt(notas.Average(n => Math.Pow(n - media, 2)));
    }






    //--------------MATRICES------------------------

    /*
     * Similar a la clase de matemáticas, escribe una librería de matrices que dé soporte 
     * a las operaciones con Matrices ( como la suma, resta, multiplicació) de matrices de dos dimensiones. 
     * Las operaciones serán para números enteros y reales.
     * Además, escribe una clase que compruebe que las operaciones desarrolladas funcionan.
     */
    static void ej16()
    {
        //ESTÁ TODO EN LA CLASE MATRIX.CS
    }

    /*
     * Escribe un método que imprima cada una de los siguientes patrones usando bucles anidados 
     * en una clase llamada PrintPatterns. A continuación se muestran los patrones.
     */

    static void ej17()
    {
        //MÉTODOS JUSTO ABAJO
    }

    // Patrón (a): Triángulo creciente
    public static void PatternA(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= i; j++)
                Console.Write(j + " ");
            Console.WriteLine();
        }
    }

    // Patrón (b): Triángulo decreciente
    public static void PatternB(int n)
    {
        for (int i = n; i >= 1; i--)
        {
            for (int j = 1; j <= i; j++)
                Console.Write(j + " ");
            Console.WriteLine();
        }
    }

    // Patrón (c): Triángulo invertido alineado a la derecha
    public static void PatternC(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            Console.Write(new string(' ', (n - i) * 2));
            for (int j = 1; j <= i; j++)
                Console.Write(j + " ");
            Console.WriteLine();
        }
    }

    // Patrón (d): Pirámide de números
    public static void PatternD(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            Console.Write(new string(' ', (n - i) * 2));
            for (int j = 1; j <= i; j++)
                Console.Write(j + " ");
            for (int j = i - 1; j >= 1; j--)
                Console.Write(j + " ");
            Console.WriteLine();
        }
    }

    //Escribe un método que compute sen(x) y cos(x) usando las siguientes series, en una clase llamada TrigonometricSeries.
    //ejercicio 18
    static double CalcularSeno(double x)
    {
        double suma = 0;
        double termino;
        int n = 0;

        do
        {
            termino = Math.Pow(-1, n) * Math.Pow(x, 2 * n + 1) / Factorial(2 * n + 1);
            suma += termino;
            n++;
        } while (Math.Abs(termino) > 1e-10);

        return suma;
    }

    static double CalcularCoseno(double x)
    {
        double suma = 0;
        double termino;
        int n = 0;

        do
        {
            termino = Math.Pow(-1, n) * Math.Pow(x, 2 * n) / Factorial(2 * n);
            suma += termino;
            n++;
        } while (Math.Abs(termino) > 1e-10);

        return suma;
    }

    static double Factorial(int n)
    {
        double resultado = 1;
        for (int i = 2; i <= n; i++)
            resultado *= i;
        return resultado;
    }

    //EJERCICIO 19
    /*
     * Escribe un programa que muestre un juego de adivinanzas de números. 
     * El programa generará aleatoriamente un número entre 0 y 99. El jugador introducirá
     * su respuesta, y el programa responderá con “Más alto” , “más bajo” o Tu tiene el número.
     */
    static void ej19()
    {
        Random random = new Random();
        int numeroSecreto = random.Next(0, 100);
        int intentos = 0;
        int apuesta;

        Console.WriteLine("¡Adivina el número entre 0 y 99!");

        do
        {
            intentos++;
            Console.Write("Introduce tu apuesta: ");
            apuesta = int.Parse(Console.ReadLine());

            if (apuesta < numeroSecreto)
            {
                Console.WriteLine("Más alto.");
            }
            else if (apuesta > numeroSecreto)
            {
                Console.WriteLine("Más bajo.");
            }
            else
            {
                Console.WriteLine($"¡Correcto! Lo conseguiste en {intentos} intentos.");
            }
        } while (apuesta != numeroSecreto);
    }

    //Programa para adivinar una palabra intentando con letras o la palabra completa.
    static void ej20()
    {
        string palabraSecreta = "testing";
        char[] progreso = new string('_', palabraSecreta.Length).ToCharArray();
        int intentos = 0;

        Console.WriteLine("¡Adivina la palabra secreta!");

        while (new string(progreso) != palabraSecreta)
        {
            Console.Write($"Intento {++intentos}: {new string(progreso)}\nIntroduce una letra o palabra: ");
            string entrada = Console.ReadLine();

            if (entrada.Length == 1)
            {
                char letra = entrada[0];
                for (int i = 0; i < palabraSecreta.Length; i++)
                {
                    if (palabraSecreta[i] == letra)
                        progreso[i] = letra;
                }
            }
            else if (entrada == palabraSecreta)
            {
                break;
            }
        }

        Console.WriteLine($"¡Correcto! Adivinaste la palabra '{palabraSecreta}' en {intentos} intentos.");
    }

    /*
     * Un entero positive es llamado número perfecto si la suma de todos sus factores 
     * ( excluidos el propio número es un posible divisor. Es igual que su valor. 
     * Por ejemplo, el número 6 es perfecto porque permite los divisores 1,2 y 3 y 6=1+2+3; 
     * pero el número 10 no es perfecto porque permite divisores 1,2 y 5 y 10≠1+2+5.
     * 
     * Un entero positivo es llamado número deficiente si la suma de todos sus divisores es menor que su valor.
     * Por ejemplo, 10 es deficiente porque 1+2+5<10; mientras que 12 no porque 1+2+3+4+6>12.
     * 
     * Escribe un método que tomando un número positivo, retorno verdadero si el número es perfecto. 
     * Similarmente, escribe un método para comprobar números deficientes.
     * 
     * Usando los métodos, escribe un programa que permita al usuario establecer un límite ( número entero positivo), 
     * y lista todos los números perfectos menor que o igual que el limite establecido. 
     * Además, se debe listar todos los números que no son deficientes ni perfectos
     */
    static void ej21()
    {
        Console.Write("Introduce un límite positivo: ");
        int limite = int.Parse(Console.ReadLine());

        Console.WriteLine("Números perfectos:");
        for (int i = 1; i <= limite; i++)
        {
            if (EsPerfecto(i))
                Console.Write($"{i} ");
        }

        Console.WriteLine("\nNúmeros no deficientes ni perfectos:");
        for (int i = 1; i <= limite; i++)
        {
            if (!EsPerfecto(i) && !EsDeficiente(i))
                Console.Write($"{i} ");
        }
    }

    static bool EsPerfecto(int num)
    {
        int suma = 0;
        for (int i = 1; i < num; i++)
        {
            if (num % i == 0)
                suma += i;
        }
        return suma == num;
    }

    static bool EsDeficiente(int num)
    {
        int suma = 0;
        for (int i = 1; i < num; i++)
        {
            if (num % i == 0)
                suma += i;
        }
        return suma < num;
    }

    //Recuerda que el máximo común divisor (MCD) de dos enteros A y B es el entero más grande que divide tanto a A como a B.
    //El algoritmo de Euclides es una técnica para encontrar rápidamente el MCD de dos enteros.
    static void ej22()
    {
        Console.Write("Introduce el primer número: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Introduce el segundo número: ");
        int b = int.Parse(Console.ReadLine());

        int mcd = CalcularMCD(a, b);
        Console.WriteLine($"El MCD de {a} y {b} es: {mcd}");
    }

    static int CalcularMCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
