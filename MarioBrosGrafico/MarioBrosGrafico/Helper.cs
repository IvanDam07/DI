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
        public static void fillBoardGame(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    //We put the random number as a string and put it in the board
                    board[i, j] = Convert.ToString(generateRandom(0, 2));
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


        public static void movements(String[,] board)
        {
            try
            {
                //regiones de codigo para poder agrupar
                #region InicializacionVaraiables
                string option;
                int optionNum; //la creamos porque el tryparse nos lo mete como salida
                bool exit = false;
                #endregion

                #region logicaWhile
                while (!exit)
                {
                    showOptionsMenu();

                    option = Console.ReadLine();

                    if (Int32.TryParse(option, out optionNum))
                    {
                        switch (optionNum)
                        {
                            case 1: //mover a la derecha
                                move(board, 0, 1);
                                showBoard(board);
                                break;

                            case 2: //mover izq
                                move(board, 0, -1);
                                showBoard(board);
                                break;

                            case 3: //mover abajo
                                move(board, 1, 0);
                                showBoard(board);
                                break;

                            case 4: //mover arriba
                                move(board, -1, 0);
                                showBoard(board);
                                break;

                            case 5:
                                exit = true;
                                break;

                            default:
                                Console.WriteLine("Invalid option, only options between 1 and 5");
                                break;
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine("Se ha producido una excepción" + e.ToString());
            }
        }


        //Moves the element around the board
        public static void move(string[,] board, int moveI, int moveJ)
        {

            //We start with positions at -1 to initialize them but these doesn't exists
            int posI = -1, posJ = -1;

            //First we look for Mario in the board
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    //We look for the M
                    if (board[i, j] == "M")
                    {
                        posI = i;
                        posJ = j;
                        break;
                    }
                }
                if (posI != -1) break;//we go out of the loop if we've found Mario's position
            }
            if (posI == -1 || posJ == -1)
            {
                Console.WriteLine("Mario is not on the board");
                return; //We go out off the method
            }

            //We calculate the new position
            int newPosI = posI + moveI;
            int newPosJ = posJ + moveJ;

            //We check if it's in the limits
            if (newPosI >= 0 && newPosI < board.GetLength(0) && newPosJ >= 0 && newPosJ < board.GetLength(1))
            {
                fillBoardGame(board);

                if (board[newPosI, newPosJ] == "0")
                {
                    Console.WriteLine("You've lost a life");
                    lifes--;

                    if (lifes == 0)
                    {
                        Console.WriteLine("You've died");
                        initialize(board);
                    }
                }
                else if (board[newPosI, newPosJ] == "1")
                {
                    Console.WriteLine("You've won a life");
                    lifes++;
                }
                else
                {
                    Console.WriteLine("You've found 2ml of potion");
                    potion += 2;

                    if (potion >= 5)
                    {
                        Console.WriteLine("You've enough potion to save Peach");
                    }
                }

                //After calculating the movement, we put all x in the board so the player can't see anything

                fillBoard(board, "X");

                //We put Mario in the new position
                board[newPosI, newPosJ] = "M";

                if (newPosI == 7 && newPosJ == 7)
                {
                    Console.WriteLine("Congratulations! You're out!");
                    initialize(board);
                }
            }
            else
            {
                Console.WriteLine("Please, check being on the limits");
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

    }
}
