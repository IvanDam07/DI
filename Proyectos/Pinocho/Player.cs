using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pinocho
{
    internal class Player
    {

        //First, we start with the attributes of the class
        public String name {get; private set;} //Pinocho or Pepito
        public int lives {get; private set;}
        public int fishCaught {get; private set;}
        public int maxMoves {get; private set;}
        public int remainingMoves {get; private set;}
        public (int x, int y) position { get; private set;} //position on the board


        //Constructor
        public Player(string name)
        {
            this.name = name;

            //all this values will be default for this game to start
            this.lives = 3;
            this.fishCaught = 0;
            this.maxMoves = 18;
            this.remainingMoves = this.maxMoves;
            this.position = (0,0);
        }


        //Methods to do the helper class more legible

        //Move to the indicated position
        public void MoveTo(int x, int y)
        {
            position = (x, y);
            remainingMoves--;
        }

        //Catch a fish
        public void CatchFish()
        {
            fishCaught++;
        }

        //Loses a fish
        public void LoseFish()
        {
            if (fishCaught > 0)
            {
                fishCaught--;
            }
        }

        //Loses a life
        public void LoseLife()
        {
            if (lives > 0)
            {
                lives--;
            }
        }

        //boolean to check if its alive
        public bool IsAlive()
        { 
            return lives > 0;
        }

        //boolean to check if the player can still move
        public bool CanMove()
        {
            return remainingMoves > 0;
        }

        //to reset the position if necessary
        public void resetPosition()
        {
            position = (0,0);
        }

        public override string ToString()
        {
            return $"{name} - Lives: {lives}, Fishes: {fishCaught}, Remaining Moves: {remainingMoves}, Position: {position}";
        }
    }
}
