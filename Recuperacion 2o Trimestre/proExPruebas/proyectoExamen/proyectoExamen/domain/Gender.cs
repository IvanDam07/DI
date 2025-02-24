using proyectoExamen.persistence.manages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoExamen.domain
{
    internal class Gender
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public GenderManager gm { get; set; }

        public Gender() 
        {
            gm = new GenderManager();
        }

        public Gender(int id, string name)
        {
            this.Id = id;
            this.Name = name;

            gm = new GenderManager();
        }

        public Gender(string name)
        {
            gm = new GenderManager();

            this.Id = gm.lastId();
            this.Name = name;
        }

        public List<Gender> readGenders()
        {
            return gm.readGenders();
        }

        public void insert()
        {
            gm.insertGender(this);
        }

        public void delete()
        {
            gm.deleteGender(this);
        }

        public void update()
        {
            gm.updateGender(this);
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
