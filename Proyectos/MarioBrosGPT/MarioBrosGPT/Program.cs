/*
 * Mario Bros 
 * 
 * Tenemos un tablero de 8x8. podemos rellenar aleatorio cada celda con valores que pueden ser 0/1/2. 
 * 
 * Mario necesita minimo 5 de pócima para salvar a Peach. El 0 quita una vida, el 1 le damos una vida y el 2 coge pócima ( 2 ml). 
 * 
 * Nosotros manejaremos a Mario moviendolo de arriba a abajo , de derecha a izquierda.  
 * 
 * El juego nos indicará:
 * 
 * El juego se acaba cuando consigue los 5 ml de pócima o se quede sin vidas.  
 * Pócima inicial: 0 ml 
 * Vidas iniciales: 3  
 */
using System;

class MarioBros
{
    static void Main()
    {
        // Inicialización
        int[,] tablero = new int[8, 8];
        Random random = new Random();

        // Rellenar el tablero con valores aleatorios (0, 1, 2)
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                tablero[i, j] = random.Next(0, 3);
            }
        }

        // Posición inicial de Mario
        int marioFila = 0;
        int marioColumna = 0;

        // Variables del juego
        int vidas = 3;
        int pocima = 0;

        // Mostrar las instrucciones
        Console.WriteLine("¡Bienvenido al juego de Mario Bros!");
        Console.WriteLine("Tablero de 8x8: Valores 0, 1, 2.");
        Console.WriteLine("0: Pierdes 1 vida, 1: Ganas 1 vida, 2: Obtienes 2 ml de pócima.");
        Console.WriteLine("Movimientos: W (arriba), S (abajo), A (izquierda), D (derecha).");
        Console.WriteLine("El juego termina cuando consigues al menos 5 ml de pócima o pierdes todas tus vidas.\n");

        while (vidas > 0 && pocima < 5)
        {
            // Mostrar el estado actual
            Console.WriteLine($"Posición actual: ({marioFila}, {marioColumna})");
            Console.WriteLine($"Vidas: {vidas} | Pócima: {pocima} ml\n");

            // Pedir movimiento
            Console.Write("Introduce tu movimiento (W/A/S/D): ");
            char movimiento = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            // Actualizar posición de Mario
            switch (movimiento)
            {
                case 'W': // Arriba
                    if (marioFila > 0) marioFila--;
                    else Console.WriteLine("Movimiento inválido: No puedes salir del tablero.");
                    break;

                case 'S': // Abajo
                    if (marioFila < 7) marioFila++;
                    else Console.WriteLine("Movimiento inválido: No puedes salir del tablero.");
                    break;

                case 'A': // Izquierda
                    if (marioColumna > 0) marioColumna--;
                    else Console.WriteLine("Movimiento inválido: No puedes salir del tablero.");
                    break;

                case 'D': // Derecha
                    if (marioColumna < 7) marioColumna++;
                    else Console.WriteLine("Movimiento inválido: No puedes salir del tablero.");
                    break;

                default:
                    Console.WriteLine("Movimiento inválido. Usa W, A, S, D.");
                    continue;
            }

            // Acción según el valor de la celda
            int celda = tablero[marioFila, marioColumna];
            if (celda == 0)
            {
                vidas--;
                Console.WriteLine("¡Oh no! Pierdes una vida.");
            }
            else if (celda == 1)
            {
                vidas++;
                Console.WriteLine("¡Bien! Ganas una vida.");
            }
            else if (celda == 2)
            {
                pocima += 2;
                Console.WriteLine("¡Genial! Obtienes 2 ml de pócima.");
            }

            // Eliminar el contenido de la celda para evitar repetir efectos
            tablero[marioFila, marioColumna] = -1;
        }

        // Resultado final
        if (pocima >= 5)
        {
            Console.WriteLine("\n¡Felicidades! Mario ha conseguido suficiente pócima para salvar a Peach.");
        }
        else if (vidas <= 0)
        {
            Console.WriteLine("\nMario se ha quedado sin vidas. ¡Juego terminado!");
        }
    }
}
