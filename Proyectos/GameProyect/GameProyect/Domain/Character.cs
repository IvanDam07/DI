using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameProyect.Domain
{
    /// <summary>
    /// Character 
    /// </summary>
    class Character
    {
        public int id { get; set; }
        public string name { get; set; }
        public int points { get; set; }

        int idCounter {  get; set; }
        /// <summary>
        /// Constructor for a Character when id, name and points know information
        /// </summary>
        /// <param name="id">Unique identificator for the character</param>
        /// <param name="name">Description name for the character</param>
        /// <param name="points">Total points for the character, default 0</param>
        public Character(int id, string name, int points = 0) //Si no recibe el dato de puntos
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show($"\"{nameof(name)}\" Can't be null.", nameof(name));
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null.", nameof(name));
            }

            this.id = id;
            this.name = name;
            this.points = points;
            //Update the vaue only when the counter is greater than the current id
            if (id > idCounter)
            {
                idCounter = id;
            }
        }

        public Character(string name, int points = 0) //Si no recibe el dato de puntos
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show($"\"{nameof(name)}\" Can't be null.", nameof(name));
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null.", nameof(name));
            }
            this.id = this.idCounter++;
            this.name = name;
            this.points = points;
        }
    }
}
