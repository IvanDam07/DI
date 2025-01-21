using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Helper
    {
        public static void Ex1(int size)
        {
            String[,] board = new string[size, size];

            fillBoardDefault(board, " ");

            //Tamaño serpiente
            int snakeSize = 4;
            
            //Se coloca la serpiente
            board[0, 0] = "s";
            board[0, 1] = "s";
            board[0, 2] = "s";
            board[0, 3] = "s";


            int walls = generateRandom(1, 5);
            //Se ponen las paredes
            fillBoard(board, "w", walls);

            //Se ponen los ratones
            int mices = generateRandom(1,5);
            //Cambiar a metodo para poner solo los ratones
            fillBoard(board, "m", mices);            

            //Se muestra la matriz
            showBoard(board);


            //Posicion actual cabeza
            int snakeRowH = 0;
            int snakeColH = 3;

            //Posicion actual cola
            int snakeRowC = 0;
            int snakeColC = 0;
        }

        //-----------AUX METHODS--------------
        public static void fillBoard(string[,] board, string c, int n)
        {

            for (int w = 0; w < n; w++)
            {
                int posI = generateRandom(0, 4);
                int posJ = generateRandom(0, 4);

                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[posI,posJ] == board[i,j])
                        {
                            if (board[posI,posJ] == " ")
                            {
                                board[posI, posJ] = c;
                            }
                        }
                    }
                }
            }
        }

        public static void fillBoardDefault(string[,] board, string c)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = c;
                }
            }
        }

        /*
         * CON EL GENERATE RANDOM HACER QUE ME DÉ DOS NÚMEROS numW veces para colocar las paredes o ratones donde sea necesario
         */

        public static int generateRandom(int min, int max)
        {
            int number = 0;

            //we put a seed to the random method to secure that we're using a different seed each time
            Random random = new Random();
            number = random.Next(min, max);

            return number;
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

        //Mover serpiente
        public static void move(string[,] board, int snakeRowH, int snakeColH, int snakeRowC, int snakeColC)
        {
            Boolean dead = false;

            do
            {
                String movement = askMove();
                movement.ToUpper();

                switch (movement)
                {
                    case "W":
                        if (snakeRowH > 0)
                        {
                            snakeRowH = snakeRowH - 1;

                        } else
                        {
                            Console.WriteLine("Invalid movement, try again.");
                            askMove();
                        }
                        break;
                    case "A":

                        break;
                    case "D":
                        if (snakeColH < 4)
                        {
                            if (go(board, snakeRowH, snakeColH+1))
                            {
                                snakeColH++;
                                print(board, snakeRowH, snakeColH); //Pinta el nuevo lugar de la cabeza
                                delete(board, snakeRowC, snakeColC); //Borra el lugar de la cola
                            }
                        } else
                        {
                            Console.WriteLine("Invalid movement, try again.");
                            //Pide otro movimiento
                            askMove();
                        }
                        break;
                    case "S":
                        if (snakeRowH < 4)
                        {
                            if (go(board, snakeRowH+1, snakeColH))
                            {
                                snakeRowH++;
                                print(board, snakeRowH, snakeColH);
                                delete(board, snakeRowC, snakeColC); //Borra el lugar de la cola
                            }
                        }
                        break;
                }
            } while (!dead);
        }

        public static void print(String[,] board, int snakeRow, int snakeCol)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j] == board[snakeRow,snakeCol])
                    {
                        board[i, j] = "s";
                    }
                }
            }
        }

        public static void delete(String[,] board, int snakeRowC, int snakeColC)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == board[snakeRowC, snakeColC])
                    {
                        board[i, j] = " ";
                    }
                }
            }
        }

        public static Boolean go(String[,] board, int posI, int posJ)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == board[posI, posJ])
                    {
                        if (board[i,j] == "w")
                        {
                            Console.WriteLine("You can't go. There is a wall");
                            return false;
                        } else if (board[i,j] == "m")
                        {
                            Console.WriteLine("You eat a mice!");
                            mice--;
                            delete(board, i, j); //Borra el lugar donde estaba el ratón
                            return true;
                        } else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        public static Boolean moveRL(String[,] board, int initialPosI, int initialPosJ, int moveI, int moveJ)
        {
            return true;
        }
        public static String askMove()
        {
            Console.WriteLine("What is your next move?");
            Console.WriteLine("Press W for go up");
            Console.WriteLine("Press A for go left");
            Console.WriteLine("Press D for go right");
            Console.WriteLine("Press S for go down");

            String move = Console.ReadLine();
            return move;
        }
    }
}
