using System;
using System.Media;

namespace Pinocho
{
    internal class Helper
    {
        private Player pinocho;
        private Player grillo;

        private string pinochoPath;
        private string grilloPath;

        private string[,] board;

        //A random for each player to avoid problems
        private Random randomPinocho;
        private Random randomGrillo;

        public Helper()
        {
            pinocho = new Player("Pinocho");
            grillo = new Player("Pepito Grillo");

            pinochoPath = $"({pinocho.position.x},{pinocho.position.y})";
            grilloPath = $"({grillo.position.x},{grillo.position.y})";

            //We initialize the board with a size 8x8
            board = new string[8, 8];
            InitializeBoard();

            //Diferents unique seed for each player to avoid problems
            randomPinocho = new Random(Guid.NewGuid().GetHashCode()); 
            randomGrillo = new Random(Guid.NewGuid().GetHashCode()); 
        }


        private void InitializeBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = "~"; //Represents water
                }
            }
        }


        public void StartGame()
        {
            Console.WriteLine("¡Comienza la competición de pesca entre Pinocho y Pepito Grillo!");

            while ((pinocho.CanMove() && pinocho.IsAlive()) || (grillo.CanMove() && grillo.IsAlive()))
            {
                //Pinocho turn and then grillo turn
                if (pinocho.IsAlive())
                {
                    PlayTurn(pinocho, ref pinochoPath, grillo, randomPinocho);
                }

                if (grillo.IsAlive())
                {
                    PlayTurn(grillo, ref grilloPath, pinocho, randomGrillo);
                }

                PrintBoard(pinocho, grillo);

                //If someone catches 5 fishes, we finish the game
                if (pinocho.fishCaught >= 5 || grillo.fishCaught >= 5)
                {
                    break;

                //If someone is at the exit, the game finishes
                } else if (pinocho.position == (board.Length - 1, board.Length - 1) || grillo.position == (board.Length - 1, board.Length - 1))
                {
                    //I don't take this as a win, I only control it, for me, the win is obtaining 5 fishes or being better than the other player
                    if (pinocho.position == (board.Length - 1, board.Length - 1))
                    {
                        Console.WriteLine("Pinocho ha salido");
                    }
                    else
                    {
                        Console.WriteLine("Grillo ha salido");
                    }

                    break;
                }
            }

            PrintGameSummary();
        }

        //Logic for one turn
        private void PlayTurn(Player player, ref string path, Player otherPlayer, Random random)
        {
            var (dx, dy) = GenerateRandomMove(random);
            int newX = player.position.x + dx;
            int newY = player.position.y + dy;

            //Check if the move is valid and if it doesn't go to the position of the other player
            if (IsValidMove(player, dx, dy) && !(newX == otherPlayer.position.x && newY == otherPlayer.position.y))
            {
                player.MoveTo(newX, newY);
                ProcessCell(player, random);

                path += $" -> ({player.position.x},{player.position.y})";//add to the path
            }
            else if (newX == otherPlayer.position.x && newY == otherPlayer.position.y)
            {
                Console.WriteLine($"{player.name} intentó moverse a la posición de {otherPlayer.name}. No pierde un movimiento.");
            }
            else
            {
                Console.WriteLine($"{player.name} intentó moverse fuera del tablero. No pierde un movimiento.");
            }
        }

        // Generar un movimiento aleatorio
        public (int, int) GenerateRandomMove(Random random)
        {
            int move = random.Next(1, 5);

            switch (move)
            {
                case 1: return (-1, 0); //up
                case 2: return (1, 0);  //down
                case 3: return (0, -1); //left
                case 4: return (0, 1);  //right
            }

            return (0, 0);//invalid move
        }

        //Check if a movement is valid
        public bool IsValidMove(Player player, int dx, int dy)
        {
            int newX = player.position.x + dx;
            int newY = player.position.y + dy;

            return (newX >= 0 && newX < 8 && newY >= 0 && newY < 8);
        }

        //Process the actual cell of the player
        public void ProcessCell(Player player, Random random)
        {
            int cell = random.Next(0, 4);

            switch (cell)
            {
                case 0: //piranha
                    player.LoseLife();
                    Console.WriteLine($"{player.name} ha encontrado una piraña y ha perdido una vida.");
                    break;

                case 1: //water
                    Console.WriteLine($"{player.name} ha encontrado agua.");
                    break;

                case 2: //stone
                    Console.WriteLine($"{player.name} ha encontrado una piedra, pero se cayó un pez.");
                    player.LoseFish();
                    break;

                case 3: //fish
                    Console.WriteLine($"{player.name} ha pescado un pez.");
                    player.CatchFish();
                    break;
            }
        }

        //Print the board
        public void PrintBoard(Player pinocho, Player grillo)
        {
            string[,] displayBoard = (string[,])board.Clone();

            //Checks if both of them are in the same position (this will never happen, just in case)
            if (pinocho.position.x == grillo.position.x && pinocho.position.y == grillo.position.y)
            {
                displayBoard[pinocho.position.x, pinocho.position.y] = "PG";
            }
            else
            {
                displayBoard[pinocho.position.x, pinocho.position.y] = "P";
                displayBoard[grillo.position.x, grillo.position.y] = "G";
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(displayBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Imprimir resumen del juego
        public void PrintGameSummary()
        {
            Console.WriteLine("\nResumen del juego:");
            Console.WriteLine($"Recorrido de Pinocho: {pinochoPath}");
            Console.WriteLine($"Recorrido de Pepito Grillo: {grilloPath}");
            Console.WriteLine($"Pinocho pescó {pinocho.fishCaught} peces, {pinocho.remainingMoves} movimientos restantes, {pinocho.lives} vidas.");
            Console.WriteLine($"Pepito Grillo pescó {grillo.fishCaught} peces, {grillo.remainingMoves} movimientos restantes, {grillo.lives} vidas.");

            // Determinar el ganador
            if (pinocho.fishCaught >= 5)
            {
                Console.WriteLine("¡Pinocho gana la competencia!");
            }
            else if (grillo.fishCaught >= 5)
            {
                Console.WriteLine("¡Pepito Grillo gana la competencia!");
            }
            else if (!pinocho.IsAlive() && grillo.IsAlive())
            {
                Console.WriteLine("¡Pepito Grillo gana la competencia!");
            }
            else if (pinocho.IsAlive() && !grillo.IsAlive())
            {
                Console.WriteLine("¡Pinocho gana la competencia!");
            }
            else
            {
                //Fishes, remaining moves and lifes
                if (pinocho.fishCaught > grillo.fishCaught)
                {
                    Console.WriteLine("¡Pinocho gana la competencia!");
                }
                else if (grillo.fishCaught > pinocho.fishCaught)
                {
                    Console.WriteLine("¡Pepito Grillo gana la competencia!");
                }
                else
                {
                    //Remaining moves
                    if (pinocho.remainingMoves > grillo.remainingMoves)
                    {
                        Console.WriteLine("¡Pinocho gana la competencia!");
                    }
                    else if (grillo.remainingMoves > pinocho.remainingMoves)
                    {
                        Console.WriteLine("¡Pepito Grillo gana la competencia!");
                    }
                    else
                    {
                        //Lives
                        if (pinocho.lives > grillo.lives)
                        {
                            Console.WriteLine("¡Pinocho gana la competencia!");
                        }
                        else if (grillo.lives > pinocho.lives)
                        {
                            Console.WriteLine("¡Pepito Grillo gana la competencia!");
                        }
                        else
                        {
                            Console.WriteLine("¡Es un empate!");
                        }
                    }
                }
            }
        }
    }
}
