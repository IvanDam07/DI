using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProyect.Domain
{
    class Ability
    {
        public int id {  get; set; }
        public string name { get; set; }
        public int points { get; set; }

        public Ability(String name) { 
            this.name = name; 
            this.id = 0;
            this.points = 100;
        }

        public Ability(String name, int points)
        {
            this.name = name;
            this.points = points;
            this.id = 0;
        }
    }
}
