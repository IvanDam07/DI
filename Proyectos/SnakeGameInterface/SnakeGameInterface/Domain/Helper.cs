using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameInterface.Domain
{
    internal class Helper
    {


        public static int generateRandom(int min, int max)
        {
            int number = 0;

            //we put a seed to the random method to secure that we're using a different seed each time
            Random random = new Random();
            number = random.Next(min, max);

            return number;
        }

        public static void fillBoard(string[,] board, string c, int n)
        {

            for (int w = 0; w < n; w++)
            {
                int posI = generateRandom(0, 5);
                int posJ = generateRandom(0, 5);

                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[posI, posJ] == board[i, j])
                        {
                            if (board[posI, posJ] == " ")
                            {
                                board[posI, posJ] = c;
                            }
                        }
                    }
                }
            }
        }

        public static void fillBoardSnake(string[,] board, string c, int n)
        {
            for (int i = 0; i < n; i++)
            {
                board[0, i] = "S";
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
    }
}
