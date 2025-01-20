using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioBrosGrafico
{
    internal class Helper
    {
        private static int lifes = 3;
        private static int potion = 0;


        //Generates a random number between two values
        public static int generateRandom(int min, int max)
        {
            int number = 0;

            //we put a seed to the random method to secure that we're using a different seed each time
            Random random = new Random(DateTime.Now.Millisecond);
            number = random.Next(min, max);

            return number;
        }


        //Fills the board with the string given, normally it'll be a letter (x)
        public static void fillBoard(string[,] board, string c)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = c;
                }
            }
        }

        //Fills the board with random numbers between 0 and 2 to make the boards game
        //public static void fillBoardGame(string[,] board)
        //{
        //    for (int i = 0; i < board.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < board.GetLength(1); j++)
        //        {
        //            //We put the random number as a string and put it in the board
        //            board[i, j] = Convert.ToString(generateRandom(0, 2));
        //        }
        //    }
        //}
        public static void fillBoardGame(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != "M") // Respeta la posición de Mario
                    {
                        board[i, j] = Convert.ToString(generateRandom(0, 2)); // Genera 0, 1 o 2
                    }
                }
            }
        }


        //Shows the board
        public static void showBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }


        //Shows the menu
        public static void showOptionsMenu()
        {
            Console.WriteLine("1. Move right");
            Console.WriteLine("2. Move left");
            Console.WriteLine("3. Move down");
            Console.WriteLine("4. Move up");
            Console.WriteLine("5. Exit game");
        }


        //public static void movements(String[,] board)
        //{
        //    try
        //    {
        //        //regiones de codigo para poder agrupar
        //        #region InicializacionVaraiables
        //        string option;
        //        int optionNum; //la creamos porque el tryparse nos lo mete como salida
        //        bool exit = false;
        //        #endregion

        //        #region logicaWhile
        //        while (!exit)
        //        {
        //            showOptionsMenu();

        //            option = Console.ReadLine();

        //            if (Int32.TryParse(option, out optionNum))
        //            {
        //                switch (optionNum)
        //                {
        //                    case 1: //mover a la derecha
        //                        move(board, 0, 1);
        //                        showBoard(board);
        //                        break;

        //                    case 2: //mover izq
        //                        move(board, 0, -1);
        //                        showBoard(board);
        //                        break;

        //                    case 3: //mover abajo
        //                        move(board, 1, 0);
        //                        showBoard(board);
        //                        break;

        //                    case 4: //mover arriba
        //                        move(board, -1, 0);
        //                        showBoard(board);
        //                        break;

        //                    case 5:
        //                        exit = true;
        //                        break;

        //                    default:
        //                        Console.WriteLine("Invalid option, only options between 1 and 5");
        //                        break;
        //                }
        //            }
        //        }
        //        #endregion
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Se ha producido una excepción" + e.ToString());
        //    }
        //}
        public static void movements(String[,] board, ref int lifes, ref int potion)
        {
            try
            {
                // Regiones de código para poder agrupar
                #region InicializacionVaraiables
                string option;
                int optionNum; // Variable que almacena el número de opción
                bool exit = false;
                #endregion

                #region LogicaWhile
                while (!exit)
                {
                    showOptionsMenu(); // Muestra las opciones del menú

                    option = Console.ReadLine(); // Lee la opción del usuario

                    if (Int32.TryParse(option, out optionNum)) // Verifica si la opción es un número válido
                    {
                        switch (optionNum)
                        {
                            case 1: // Mover a la derecha
                                move(board, 0, 1, ref lifes, ref potion);
                                showBoard(board);
                                break;

                            case 2: // Mover a la izquierda
                                move(board, 0, -1, ref lifes, ref potion);
                                showBoard(board);
                                break;

                            case 3: // Mover abajo
                                move(board, 1, 0, ref lifes, ref potion);
                                showBoard(board);
                                break;

                            case 4: // Mover arriba
                                move(board, -1, 0, ref lifes, ref potion);
                                showBoard(board);
                                break;

                            case 5: // Salir del juego
                                exit = true;
                                break;

                            default: // Opción inválida
                                Console.WriteLine("Opción no válida. Solo opciones entre 1 y 5.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Por favor, introduce un número válido.");
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine("Se ha producido una excepción: " + e.ToString());
            }
        }


        //Moves the element around the board
        //public static void move(string[,] board, int moveI, int moveJ)
        //{

        //    //We start with positions at -1 to initialize them but these doesn't exists
        //    int posI = -1, posJ = -1;

        //    //First we look for Mario in the board
        //    for (int i = 0; i < board.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < board.GetLength(1); j++)
        //        {
        //            //We look for the M
        //            if (board[i, j] == "M")
        //            {
        //                posI = i;
        //                posJ = j;
        //                break;
        //            }
        //        }
        //        if (posI != -1) break;//we go out of the loop if we've found Mario's position
        //    }
        //    if (posI == -1 || posJ == -1)
        //    {
        //        Console.WriteLine("Mario is not on the board");
        //        return; //We go out off the method
        //    }

        //    //We calculate the new position
        //    int newPosI = posI + moveI;
        //    int newPosJ = posJ + moveJ;

        //    //We check if it's in the limits
        //    if (newPosI >= 0 && newPosI < board.GetLength(0) && newPosJ >= 0 && newPosJ < board.GetLength(1))
        //    {
        //        fillBoardGame(board);

        //        if (board[newPosI, newPosJ] == "0")
        //        {
        //            Console.WriteLine("You've lost a life");
        //            lifes--;

        //            if (lifes == 0)
        //            {
        //                Console.WriteLine("You've died");
        //                initialize(board);
        //            }
        //        }
        //        else if (board[newPosI, newPosJ] == "1")
        //        {
        //            Console.WriteLine("You've won a life");
        //            lifes++;
        //        }
        //        else
        //        {
        //            Console.WriteLine("You've found 2ml of potion");
        //            potion += 2;

        //            if (potion >= 5)
        //            {
        //                Console.WriteLine("You've enough potion to save Peach");
        //            }
        //        }

        //        //After calculating the movement, we put all x in the board so the player can't see anything

        //        fillBoard(board, "X");

        //        //We put Mario in the new position
        //        board[newPosI, newPosJ] = "M";

        //        if (newPosI == 7 && newPosJ == 7)
        //        {
        //            Console.WriteLine("Congratulations! You're out!");
        //            initialize(board);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Please, check being on the limits");
        //    }

        //}
        // Corrige la lógica de movimiento de Mario.
        public static void move(string[,] board, int moveI, int moveJ, ref int lifes, ref int potion)
        {
            int posI = -1, posJ = -1;

            // Encuentra la posición actual de Mario
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == "M")
                    {
                        posI = i;
                        posJ = j;
                        break;
                    }
                }
                if (posI != -1) break; // Mario encontrado
            }

            if (posI == -1 || posJ == -1)
            {
                Console.WriteLine("Mario no está en el tablero");
                return;
            }

            // Calcula la nueva posición
            int newPosI = posI + moveI;
            int newPosJ = posJ + moveJ;

            // Verifica límites del tablero
            if (newPosI >= 0 && newPosI < board.GetLength(0) && newPosJ >= 0 && newPosJ < board.GetLength(1))
            {
                string newCell = board[newPosI, newPosJ];

                // Aplica efectos según el valor de la celda
                if (newCell == "0")
                {
                    Console.WriteLine("Has perdido una vida");
                    lifes--;

                    if (lifes == 0)
                    {
                        Console.WriteLine("Has muerto");
                        initialize(board);
                        lifes = 3; // Reinicia vidas
                        potion = 0; // Reinicia pócima
                        return;
                    }
                }
                else if (newCell == "1")
                {
                    Console.WriteLine("Has ganado una vida");
                    lifes++;
                }
                else if (newCell == "2")
                {
                    Console.WriteLine("Has encontrado 2 ml de pócima");
                    potion += 2;

                    if (potion >= 5)
                    {
                        Console.WriteLine("¡Tienes suficiente pócima para salvar a Peach!");
                        initialize(board);
                        return;
                    }
                }

                // Actualiza la posición de Mario
                board[posI, posJ] = "X"; // Limpia la posición anterior
                board[newPosI, newPosJ] = "M"; // Coloca a Mario en la nueva posición
            }
            else
            {
                Console.WriteLine("Movimiento fuera de límites");
            }
        }

        //Puts a specific string in a specific position in the board
        public static void setPosition(String[,] board, int i, int j, String c)
        {
            board[i, j] = c;
        }


        //Initializes the board
        public static void initialize(String[,] board)
        {

            fillBoard(board, "X");
            board[0, 0] = "M";

            Console.WriteLine("A NEW GAME HAS STARTED");

        }

        // Actualiza el tablero con números aleatorios pero respeta la posición de Mario y no reinicia todo.
        public static void updateBoardGame(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // Solo actualiza las celdas que no sean de Mario ni de otros valores ya establecidos
                    if (board[i, j] != "M")
                    {
                        board[i, j] = Convert.ToString(generateRandom(0, 3)); // Genera 0, 1 o 2
                    }
                }
            }
        }

    }
}
