using Gestpro_MateoMolina.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Gestpro_MateoMolina.domain
{
    internal class Proyecto
    {
        public int id;
        public String codigo;
        public String nombre;
        public DateTime fecIncio;
        public DateTime fecFin;
        public proyectoPersistence pm;

        public Proyecto(string codigo, string nombre, DateTime fecIncio, DateTime fecFin)
        {

            pm = new proyectoPersistence();

            this.id = pm.lastId(this);
            this.codigo = codigo;
            this.nombre = nombre;
            this.fecIncio = fecIncio;
            this.fecFin = fecFin;

        }

        public Proyecto(int id, string codigo, string nombre, DateTime fecIni, DateTime fecFin)
        {
            pm = new proyectoPersistence();

            this.id = id;
            this.codigo = codigo;
            this.nombre = nombre;
            this.fecIncio = fecIni;
            this.fecFin = fecFin;

        }

        public Proyecto() 
        {
            
            pm = new proyectoPersistence();
            this.id = pm.lastId(this);
        }


        public int Id { get => id; set => id = value; }
        public String Codigo { get => codigo; set => codigo = value; }
        public String Nombre { get => nombre; set => nombre = value; }
        public DateTime FecInicio { get => fecIncio; set => fecIncio = value; }
        public DateTime FecFin { get => fecFin; set => fecFin = value; }
        public proyectoPersistence PM { get => pm; set => pm = value; }


        public void readProyectos()
        {
            pm.readProyectos();
        }

        public List<Proyecto> getListaProyectos()
        {
            return pm.proyectoList;
        }

        public void insert()
        {
            pm.insertProyectos(this);
        }

        public void delete()
        {
            pm.deleteProyectos(this);
        }

        public void update()
        {
            pm.modifyProyecto(this);
        }

        public void last()
        {
            pm.lastId(this);
        }




        public override string ToString()
        {
            return nombre;
        }

    }
}
