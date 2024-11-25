using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinocho
{

    internal class PinochoGame
    {

        public static void Main(string[] args)
        {
            Helper helper = new Helper();
            helper.StartGame();

            Console.ReadKey();
        }

    }
}