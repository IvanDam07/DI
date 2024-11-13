using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioBros
{
    internal class MarioBros
    {
        /*
         * Tablero de 8x8. Rellenamos cada celda con números aleatorios de 0, 1 y 2
         * Mario necesita 5ml de pócima para salvar a peach.
         * El 0 quita una vida, el 1 le damos una vida y el 2 coge una pócima de 2 ml
         * Manejamos a Mario moviéndolo arriba, abajo, dechera o izquierda.
         * El juego nos indicará.
         * El juego se acaba cuando consigue los 5ml de pócima o se queda sin vidas.
         * Saldrá por la 8,8 solo cuando tenga toda la pócima
         * Pócima inicial: 0ml
         * Vidas iniciales: 3
         */

        static void Main(string[] args)
        {
            //Creates the board
            string[,] mushromKingdom = new String[8, 8];

            //We fill all the board with X and sets Mario in 0,0
            Helper.fillBoard(mushromKingdom, "X");
            Helper.setPosition(mushromKingdom, 0, 0, "M");

            Console.WriteLine("WELCOME TO MARIO GAME, LET'S SAVE PEACH AND SCAPE BEFORE YOU DIE!!!");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine();

            //We show the initial board
            Helper.showBoard(mushromKingdom);


            Console.WriteLine();

            //We run the game
            Helper.movements(mushromKingdom);
            





        }
    }
}
