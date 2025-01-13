using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataGridPersonas
{
    internal class Persona
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public int edad {  get; set; } 
        public PersonasPersistance PM { get; set; }
        public Persona() { 
            PM = new PersonasPersistance();
        }
        public Persona(string nombre, int edad) { 
            PM = new PersonasPersistance();
            //this.id = PM.lastId(this);
            this.nombre = nombre;
            this.edad = edad;
        }
        public Persona(int id, string nombre, int edad) { 
            this.id = id;
            this.nombre=nombre;
            this.edad = edad;
            PM = new PersonasPersistance();
        }

        public void readP() { 
            PM.readPeople();
        }

        public List<Persona> getListPersona() {
            return PM.PersonaList;
        }

        public void insert()
        {
            PM.insertPeople(this);
        }

        public void last() {
        //     PM.lastId(this);
        }

        public void delete() {
            PM.deletePeople(this);
        }

        public void update() { 
            PM.modifyPeople(this);
        }
    }
}
